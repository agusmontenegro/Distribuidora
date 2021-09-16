﻿use DISTRIBUIDORA;
go

/*---------------------------------------------------*/
/*----------------ELIMINACIÓN DE TABLAS--------------*/
/*---------------------------------------------------*/ 

if object_id('Combo','U') is not null
	drop table Combo;

if object_id('Stock','U') is not null
	drop table Stock;

if object_id('Item_Venta','U') is not null
	drop table Item_Venta;

if object_id('Producto','U') is not null
	drop table Producto;

if object_id('Rubro','U') is not null
	drop table Rubro;

if object_id('Venta','U') is not null
	drop table Venta;

if object_id('Alerta','U') is not null
	drop table Alerta;

if object_id('Tipo_Alerta','U') is not null
	drop table Tipo_Alerta;	

if object_id('Producto_View', 'V') is not null
	drop view Producto_View;

if object_id('Combo_View', 'V') is not null
	drop view Combo_View;	

if object_id('HayStockDisponible', 'FN') is not null
	drop function HayStockDisponible;	

if object_id('LlegoAPuntoDeReposicion', 'FN') is not null
	drop function LlegoAPuntoDeReposicion;	

if object_id('InsertarVenta', 'P') is not null
	drop procedure InsertarVenta;

if object_id('InsertarItem', 'P') is not null
	drop procedure InsertarItem;
	
if object_id('ReponerStock', 'P') is not null
	drop procedure ReponerStock;		

if object_id('InsertarProducto', 'P') is not null
	drop procedure InsertarProducto;	

if object_id('InsertarComponente', 'P') is not null
	drop procedure InsertarComponente;		

if object_id('ActualizarProducto', 'P') is not null
	drop procedure ActualizarProducto;	
	
if object_id('ActualizarStock', 'P') is not null
	drop procedure ActualizarStock;		
	
if object_id('EmitirAlertaDeReposicion', 'P') is not null
	drop procedure EmitirAlertaDeReposicion;	
	
if object_id('QuitarAlertaDeReposicion', 'P') is not null
	drop procedure QuitarAlertaDeReposicion;			

/*---------------------------------------------------*/
/*----------------CREACIÓN DE TABLAS-----------------*/
/*---------------------------------------------------*/

create table Producto (
	prod_codigo int not null identity(1,1),
	prod_detalle nvarchar(255) not null,
	prod_rubro int not null,
	prod_precio decimal(12,2),
	prod_activo bit not null
);

create table Rubro (
	rubr_codigo int not null identity(1,1),
	rubr_detalle text not null
);

create table Combo (
	comb_codigo int not null,
	comb_componente int not null,
	comb_cantidad int not null
);

create table Stock (
	stoc_producto int not null,
	stoc_cantidad_actual int,
	stoc_cantidad_minima int,
	stoc_ultima_reposicion smalldatetime
);

create table Venta (
	vent_codigo int not null identity(1,1),
	vent_fecha smalldatetime not null,
	vent_precio_total decimal(12,2) not null
);

create table Item_Venta (
	item_codigo int not null identity(1,1),
	item_venta int not null,
	item_producto int not null,
	item_precio decimal(12,2) not null,
	item_cantidad int not null
);

create table Alerta (
	aler_codigo int not null identity(1,1),
	aler_objeto int not null,
	aler_detalle text not null,
	aler_tipo int not null,
	aler_fecha smalldatetime not null
);

create table Tipo_Alerta(
	tale_codigo int not null identity(1,1),
	tale_detalle text not null
);

go

/*---------------------------------------------------------*/
/*----------------CREACIÓN DE PRIMARY KEYS-----------------*/
/*---------------------------------------------------------*/

alter table Combo
add constraint Combo_PK primary key (comb_codigo, comb_componente);

alter table Producto
add constraint Producto_PK primary key (prod_codigo);

alter table Rubro
add constraint Rubro_PK primary key (rubr_codigo);

alter table Stock
add constraint Stock_PK primary key (stoc_producto);

alter table Venta
add constraint Venta_PK primary key (vent_codigo);

alter table Item_Venta
add constraint Item_Venta_PK primary key (item_codigo);

alter table Alerta
add constraint Alerta_PK primary key (aler_codigo);

alter table Tipo_Alerta
add constraint Tipo_Alerta_PK primary key (tale_codigo);

go

/*---------------------------------------------------------*/
/*----------------CREACIÓN DE FOREIGN KEYS-----------------*/
/*---------------------------------------------------------*/


alter table Combo
add constraint ComboProducto_Producto foreign key (comb_codigo)
references Producto (prod_codigo);

alter table Combo
add constraint ComboComponente_Producto foreign key (comb_componente)
references Producto (prod_codigo);

alter table Item_Venta
add constraint ItemVenta_Venta foreign key (item_venta)
references Venta (vent_codigo);

alter table Item_Venta
add constraint ItemVenta_Producto foreign key (item_producto)
references Producto (prod_codigo);

alter table Producto
add constraint Producto_Rubro foreign key (prod_rubro)
references Rubro (rubr_codigo);

alter table Stock
add constraint Stock_Producto foreign key (stoc_producto)
references Producto (prod_codigo);

alter table Alerta
add constraint Alerta_TipoAlerta foreign key (aler_tipo)
references Tipo_Alerta (tale_codigo);

go

/*---------------------------------------------------------*/
/*----------------CREACIÓN DE VISTAS-----------------*/
/*---------------------------------------------------------*/

create view Producto_View 
as
	select prod_codigo, prod_detalle, prod_precio, rubr_codigo, rubr_detalle, stoc_cantidad_actual, stoc_cantidad_minima, stoc_ultima_reposicion
	from Producto
	join Rubro on rubr_codigo = prod_rubro
	join Stock on stoc_producto = prod_codigo
	where prod_activo = 1
go

create view Combo_View 
as
	select p.prod_codigo Producto, 
		   p.prod_detalle Detalle, 
		   p.prod_precio Precio, 
		   c.comb_componente CodigoComponente,
		   p2.prod_detalle DetalleComponente,
		   c.comb_cantidad CantidadComponente

	from Producto p
	join Combo c on c.comb_codigo = p.prod_codigo
	join Producto p2 on p2.prod_codigo = c.comb_componente
	where p.prod_activo = 1
go

/*---------------------------------------------------*/
/*----------------CREACIÓN DE FUNCIONES--------------*/
/*---------------------------------------------------*/

create function LlegoAPuntoDeReposicion (@producto int)
returns bit
begin

	if(select count(*) from Combo where comb_codigo = @producto) > 0
	begin
		declare @comb_componente char(8)
		declare @llego_a_punto_de_reposicion bit
		declare componentesCursor cursor for select comb_componente
											 from Combo
											 where comb_codigo = @producto

		open componentesCursor
		fetch next from componentesCursor into @comb_componente
		set @llego_a_punto_de_reposicion = 1

		while @@fetch_status = 0​
		begin
			set @llego_a_punto_de_reposicion = @llego_a_punto_de_reposicion & dbo.LlegoAPuntoDeReposicion(@comb_componente)

			fetch next from componentesCursor into @comb_componente
		end
		
		close componentesCursor​
		deallocate componentesCursor

		return @llego_a_punto_de_reposicion	
	end
	if (select count(*) from Stock where stoc_producto = @producto and stoc_cantidad_actual <= stoc_cantidad_minima) > 0
		return 1

	return 0
end
go

create function HayStockDisponible (@producto int, @cantidad int)
returns bit
begin

	if(select count(*) from Combo where comb_codigo = @producto) > 0
	begin
		declare @comb_componente char(8)
		declare @comb_cantidad int
		declare @hay_stock bit
		declare componentesCursor cursor for select comb_componente, comb_cantidad
											 from Combo
											 where comb_codigo = @producto

		open componentesCursor
		fetch next from componentesCursor into @comb_componente, @comb_cantidad
		set @hay_stock = 1

		while @@fetch_status = 0​
		begin
			declare @cantAcum int
			set @cantAcum = @comb_cantidad * @cantidad;

			set @hay_stock = @hay_stock & dbo.HayStockDisponible(@comb_componente, @cantAcum)

			fetch next from componentesCursor into @comb_componente, @comb_cantidad
		end
		
		close componentesCursor​
		deallocate componentesCursor

		return @hay_stock	
	end
	if (select count(*) from Stock where stoc_producto = @producto and stoc_cantidad_actual >= @cantidad) > 0
		return 1

	return 0
end
go

/*---------------------------------------------------*/
/*----------------CREACIÓN DE STORED PROCEDURES------*/
/*---------------------------------------------------*/

create procedure InsertarVenta (@precioTotal decimal(12,2), @codigo int output) as
begin

	declare @fecha smalldatetime;
	set @fecha = (select GETDATE());

	insert into Venta values (
		@fecha,
		@precioTotal	
	);

	set @codigo = (select top 1 vent_codigo from Venta where vent_fecha = @fecha);

end
go

create procedure EmitirAlertaDeReposicion (@producto int) as
begin

	if(select count(*) from Combo where comb_codigo = @producto) > 0
	begin
		declare @comb_componente char(8)
		declare componentesCursor cursor for select comb_componente
											 from Combo
											 where comb_codigo = @producto

		open componentesCursor
		fetch next from componentesCursor into @comb_componente

		while @@fetch_status = 0​
		begin
			exec dbo.EmitirAlertaDeReposicion @comb_componente;

			fetch next from componentesCursor into @comb_componente
		end
		
		close componentesCursor​
		deallocate componentesCursor	
	end

	if (select count(*) from Stock where stoc_producto = @producto and stoc_cantidad_actual <= stoc_cantidad_minima) > 0
	begin
		if (select count(*) from Alerta where aler_objeto = @producto) = 0
		begin
			declare @texto varchar(max)
			declare @detalle_producto varchar(max)
			declare @cant_actual int
			declare @punto_reposicion int

			select @detalle_producto = p.prod_detalle, @cant_actual = s.stoc_cantidad_actual, @punto_reposicion = s.stoc_cantidad_minima
			from Producto p
			join Stock s on s.stoc_producto = p.prod_codigo
			where p.prod_codigo = @producto;

			set @texto = 'Se debe reponer el producto ' + (select cast(@producto as varchar(max))) + ' - ' 
						 + @detalle_producto + '. Stock actual: ' + (select cast(@cant_actual as varchar(max))) 
						 + ', punto de reposición: ' + (select cast(@punto_reposicion as varchar(max)));

			insert into Alerta values (@producto, @texto, 1, (select GETDATE()));
		end		
	end
end
go

create procedure QuitarAlertaDeReposicion (@producto int) as
begin

	if(select count(*) from Combo where comb_codigo = @producto) > 0
	begin
		declare @comb_componente char(8)
		declare componentesCursor cursor for select comb_componente
											 from Combo
											 where comb_codigo = @producto

		open componentesCursor
		fetch next from componentesCursor into @comb_componente

		while @@fetch_status = 0​
		begin
			exec dbo.QuitarAlertaDeReposicion @comb_componente;

			fetch next from componentesCursor into @comb_componente
		end
		
		close componentesCursor​
		deallocate componentesCursor	
	end

	if (select count(*) from Stock where stoc_producto = @producto and stoc_cantidad_actual > stoc_cantidad_minima) > 0
	begin
		if (select count(*) from Alerta where aler_objeto = @producto) > 0
		begin
			delete from Alerta where aler_objeto = @producto;
		end		
	end
end
go

create procedure ActualizarStock (@codigo int, @cantidad int) as
begin

	if(select count(*) from Combo where comb_codigo = @codigo) > 0
	begin
		declare @comb_componente int;
		declare @comb_cantidad int;

		declare componentesCursor cursor local for select comb_componente, comb_cantidad
												   from Combo
												   where comb_codigo = @codigo;

		open componentesCursor;
		fetch next from componentesCursor into @comb_componente, @comb_cantidad;

		while @@FETCH_STATUS = 0
		begin
			declare @cantAcum int;
			set @cantAcum = @comb_cantidad * @cantidad;

			exec ActualizarStock @comb_componente, @cantAcum;

			fetch next from componentesCursor into @comb_componente, @comb_cantidad;
		end

		close componentesCursor​
		deallocate componentesCursor
	end
	else
	begin
		update Stock
		set stoc_cantidad_actual = stoc_cantidad_actual - @cantidad
		where stoc_producto = @codigo;
	end
end
go

create procedure InsertarItem (@codigoVenta int, @producto int, @precioUnitario decimal(12,2), @cantidad int) as
begin

	insert into Item_Venta values (
		@codigoVenta,
		@producto,
		@precioUnitario,
		@cantidad
	);
	
	exec ActualizarStock @producto, @cantidad;

end
go

create procedure ReponerStock (@codigoProducto int, @cantidadAReponer int) as
begin

	update Stock
	set stoc_cantidad_actual = stoc_cantidad_actual + @cantidadAReponer, stoc_ultima_reposicion = (select GETDATE())
	where stoc_producto = @codigoProducto;

end
go

create procedure InsertarProducto (@detalle nvarchar(255), @precioUnitario decimal(12,2), @rubro int, @stockMinimo int, @codigoProducto int output) as
begin

	insert into Producto values (
		@detalle,
		@rubro,
		@precioUnitario,
		1
	);

	set @codigoProducto = (select top 1 prod_codigo 
						   from Producto 
						   where prod_detalle = @detalle and
								 prod_precio = @precioUnitario and
								 prod_rubro = @rubro);

	insert into Stock values (
		@codigoProducto,
		0,
		@stockMinimo,
		null
	);	

end
go

create procedure ActualizarProducto (@codigo int, @detalle nvarchar(255), @precioUnitario decimal(12,2), @rubro int, @stockMinimo int) as
begin

	update Producto
	set prod_detalle = @detalle, prod_precio = @precioUnitario, prod_rubro = @rubro
	where prod_codigo = @codigo;

	update Stock
	set stoc_cantidad_minima = @stockMinimo
	where stoc_producto = @codigo;

end
go

create procedure InsertarComponente (@codigoProducto int, @codigoComponente int, @cantidad int) as
begin

	insert into Combo values (
		@codigoProducto,
		@codigoComponente,
		@cantidad
	);

end
go

/*---------------------------------------------------*/
/*----------------INSERCIÓN DE DATOS-----------------*/
/*---------------------------------------------------*/

insert into Rubro values
('Artículos de limpieza'),
('Perfumería'),
('Bebidas sin alcohol'),
('Bebidas con alcohol'),
('Almacén/Cocina'),
('Congelados'),
('Librería'),
('Golosinas'),
('Helados'),
('Galletitas');

insert into Tipo_Alerta values
('Reposición de stock');

/*insert into Producto values
('Coca Cola Light 2.25L', 3, 150, 1),
('Coca Cola 2.25L', 3, 180, 1),
('Coca Cola 3L', 3, 200, 1),
('Fernet Branca 1L', 4, 800, 1),
('Smirnoff 850 mL', 4, 550, 1),
('Speed (lata chica)', 3, 60, 1),
('Speed (lata grande)', 3, 100, 1),
('Manaos 2.25L', 3, 120, 1),
('Absolut 1L', 4, 1200, 1),
('Harina 4 ceros', 5, 150, 1),
('Quilmes (lata grande)', 5, 90, 1);

insert into Stock values
(1, 152, 50, (select GETDATE())),
(2, 89, 70, (select GETDATE())),
(3, 170, 40, (select GETDATE())),
(5, 100, 15, (select GETDATE())),
(4, 120, 20, (select GETDATE())),
(6, 130, 30, (select GETDATE())),
(7, 105, 20, (select GETDATE())),
(8, 251, 70, (select GETDATE())),
(9, 40, 10, (select GETDATE())),
(10, 80, 25, (select GETDATE())),
(11, 82, 25, (select GETDATE()));*/