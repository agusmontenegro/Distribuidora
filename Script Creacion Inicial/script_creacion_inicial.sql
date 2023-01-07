use DISTRIBUIDORA;
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

if object_id('Reposicion_Producto','U') is not null
	drop table Reposicion_Producto;

if object_id('Reposicion','U') is not null
	drop table Reposicion;

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

if object_id('Venta_View', 'V') is not null
	drop view Venta_View;

if object_id('Reposicion_View', 'V') is not null
	drop view Reposicion_View;	

if object_id('HayStockDisponible', 'FN') is not null
	drop function HayStockDisponible;	

if object_id('InformarStockFaltante', 'FN') is not null
	drop function InformarStockFaltante;

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
	
if object_id('GuardarReposicion', 'P') is not null
	drop procedure GuardarReposicion;				

/*---------------------------------------------------*/
/*----------------CREACIÓN DE TABLAS-----------------*/
/*---------------------------------------------------*/

create table Producto (
	prod_id int not null identity(1,1),
	prod_codigo nvarchar(5) not null,
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
	comb_id int not null,
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

create table Tipo_Alerta (
	tale_codigo int not null identity(1,1),
	tale_detalle text not null
);

create table Reposicion (
	repo_codigo int not null identity(1,1),
	repo_fecha smalldatetime not null
);

create table Reposicion_Producto (
	rpro_reposicion int not null,
	rpro_producto int not null,
	rpro_cantidad_vieja int,
	rpro_cantidad_nueva int
);

go

/*---------------------------------------------------------*/
/*----------------CREACIÓN DE PRIMARY KEYS-----------------*/
/*---------------------------------------------------------*/

alter table Combo
add constraint Combo_PK primary key (comb_id, comb_componente);

alter table Producto
add constraint Producto_PK primary key (prod_id);

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

alter table Reposicion
add constraint Reposicion_PK primary key (repo_codigo);

alter table Reposicion_Producto
add constraint Reposicion_Producto_PK primary key (rpro_reposicion, rpro_producto);

go

/*---------------------------------------------------------*/
/*----------------CREACIÓN DE FOREIGN KEYS-----------------*/
/*---------------------------------------------------------*/


alter table Combo
add constraint ComboProducto_Producto foreign key (comb_id)
references Producto (prod_id);

alter table Combo
add constraint ComboComponente_Producto foreign key (comb_componente)
references Producto (prod_id);

alter table Item_Venta
add constraint ItemVenta_Venta foreign key (item_venta)
references Venta (vent_codigo);

alter table Item_Venta
add constraint ItemVenta_Producto foreign key (item_producto)
references Producto (prod_id);

alter table Producto
add constraint Producto_Rubro foreign key (prod_rubro)
references Rubro (rubr_codigo);

alter table Stock
add constraint Stock_Producto foreign key (stoc_producto)
references Producto (prod_id);

alter table Alerta
add constraint Alerta_TipoAlerta foreign key (aler_tipo)
references Tipo_Alerta (tale_codigo);

alter table Reposicion_Producto
add constraint ReposicionProducto_Reposicion foreign key (rpro_reposicion)
references Reposicion (repo_codigo);

alter table Reposicion_Producto
add constraint ReposicionProducto_Producto foreign key (rpro_producto)
references Producto (prod_id);

go

/*---------------------------------------------------------*/
/*----------------CREACIÓN DE VISTAS-----------------*/
/*---------------------------------------------------------*/

create view Producto_View 
as
	select prod_id Id,
		   prod_codigo Codigo, 
		   prod_detalle Detalle, 
		   prod_precio Precio, 
		   rubr_codigo RubroCodigo, 
		   rubr_detalle RubroDetalle, 
		   stoc_cantidad_actual StockActual, 
		   stoc_cantidad_minima PtoReposicion, 
		   stoc_ultima_reposicion UltimaReposicion

	from Producto
	join Rubro on rubr_codigo = prod_rubro
	join Stock on stoc_producto = prod_id
	where prod_activo = 1
go

create view Combo_View 
as
	select p.prod_id Id,
		   p.prod_codigo Codigo, 
		   p.prod_detalle Detalle, 
		   p.prod_precio Precio, 
		   c.comb_componente IdComponente,
		   p2.prod_detalle DetalleComponente,
		   c.comb_cantidad CantidadComponente,
		   p2.prod_codigo CodigoComponente

	from Producto p
	join Combo c on c.comb_id = p.prod_id
	join Producto p2 on p2.prod_id = c.comb_componente
	where p.prod_activo = 1
go

create view Venta_View 
as
	select p.prod_id Producto, 
		   p.prod_detalle Detalle, 
		   i.item_precio Precio, 
		   i.item_cantidad Cantidad,
		   i.item_precio * i.item_cantidad Subtotal,
		   v.vent_codigo Codigo,
		   v.vent_fecha Fecha,
		   v.vent_precio_total Total

	from Venta v
	join Item_Venta i on i.item_venta = v.vent_codigo
	join Producto p on p.prod_id = i.item_producto
go

create view Reposicion_View 
as
	select p.prod_id Producto, 
		   p.prod_detalle Detalle, 
		   rp.rpro_cantidad_vieja CantidadAnterior, 
		   rp.rpro_cantidad_nueva CantidadActual,
		   r.repo_fecha Fecha,
		   rp.rpro_reposicion Codigo

	from Reposicion r
	join Reposicion_Producto rp on rp.rpro_reposicion = r.repo_codigo
	join Producto p on p.prod_id = rp.rpro_producto
go

/*---------------------------------------------------*/
/*----------------CREACIÓN DE FUNCIONES--------------*/
/*---------------------------------------------------*/

create function [dbo].[LlegoAPuntoDeReposicion] (@producto int)
returns bit
begin

	--if(select count(*) from Combo where comb_id = @producto) > 0
	--begin
	--	declare @comb_componente char(8)
	--	declare @llego_a_punto_de_reposicion bit
	--	declare componentesCursor cursor for select comb_componente
	--										 from Combo
	--										 where comb_id = @producto

	--	open componentesCursor
	--	fetch next from componentesCursor into @comb_componente
	--	set @llego_a_punto_de_reposicion = 1

	--	while @@fetch_status = 0​
	--	begin
	--		set @llego_a_punto_de_reposicion = @llego_a_punto_de_reposicion & dbo.LlegoAPuntoDeReposicion(@comb_componente)

	--		fetch next from componentesCursor into @comb_componente
	--	end
		
	--	close componentesCursor​
	--	deallocate componentesCursor

	--	return @llego_a_punto_de_reposicion	
	--end
	if (select count(*) from Stock where stoc_producto = @producto and stoc_cantidad_actual <= stoc_cantidad_minima) > 0
		return 1

	return 0
end

create function [dbo].[HayStockDisponible] (@producto int, @cantidad int)
returns bit
begin

	if(select count(*) from Combo where comb_id = @producto) > 0
	begin
		declare @comb_componente char(8)
		declare @comb_cantidad int
		declare @hay_stock bit
		declare componentesCursor cursor for select c.comb_componente, c.comb_cantidad
											 from Combo c
											 join Producto p on c.comb_id = p.prod_id
											 where p.prod_id = @producto

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
	if (select count(*) 
	    from Stock s
		join Producto p on p.prod_id = s.stoc_producto
		where p.prod_id = @producto and s.stoc_cantidad_actual >= @cantidad) > 0
		return 1

	return 0
end

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  UserDefinedFunction [dbo].[HayStockDisponible]    Script Date: 26/12/2022 17:39:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[InformarStockFaltante] (@producto int, @cantidad int)
returns varchar(max)
begin	

	declare @codigoProducto nvarchar(5)
	declare @detalleProducto varchar(max)
	declare @cantidadFaltante int

	if(select count(*) from Combo where comb_id = @producto) > 0
	begin
		declare @comb_componente char(8)
		declare @comb_cantidad int
		declare @texto varchar(max)
		declare componentesCursor cursor for select c.comb_componente, c.comb_cantidad
											 from Combo c
											 join Producto p on c.comb_id = p.prod_id
											 where p.prod_id = @producto

		open componentesCursor
		fetch next from componentesCursor into @comb_componente, @comb_cantidad
		set @texto = ''

		while @@fetch_status = 0​
		begin
			declare @cantAcum int
			set @cantAcum = @comb_cantidad * @cantidad;
			set @texto = @texto + dbo.InformarStockFaltante (@comb_componente, @cantAcum)
			fetch next from componentesCursor into @comb_componente, @comb_cantidad
		end
		
		close componentesCursor​
		deallocate componentesCursor

		return @texto	
	end

	select @cantidadFaltante = @cantidad - s.stoc_cantidad_actual, 
		   @codigoProducto = p.prod_codigo,
		   @detalleProducto = p.prod_detalle

	from Stock s
	join Producto p on p.prod_id = s.stoc_producto
	where p.prod_id = @producto

	if (@cantidadFaltante) > 0
		return 'Se debe reponer el producto ' + 
		(select cast(@codigoProducto as varchar(max))) + ' - ' + 
		(select cast(@detalleProducto as varchar(max))) + '. Cantidad faltante: ' + (select cast(@cantidadFaltante as varchar(max)))

	return ''
end

GO

/*---------------------------------------------------*/
/*----------------CREACIÓN DE STORED PROCEDURES------*/
/*---------------------------------------------------*/

create procedure GuardarReposicion (@codigo int output) as
begin

	declare @fecha smalldatetime;
	set @fecha = (select GETDATE());

	insert into Reposicion values (
		@fecha
	);

	set @codigo = (select top 1 repo_codigo from Reposicion where repo_fecha = @fecha);

end
go

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

create procedure [dbo].[EmitirAlertaDeReposicion] (@producto int) as
begin

	if(select count(*) from Combo where comb_id = @producto) > 0
	begin
		declare @comb_componente char(8)
		declare componentesCursor cursor for select comb_componente
											 from Combo
											 where comb_id = @producto

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
			declare @codigo_producto nvarchar(5)
			declare @cant_actual int
			declare @punto_reposicion int

			select @detalle_producto = p.prod_detalle, 
				   @cant_actual = s.stoc_cantidad_actual, 
				   @punto_reposicion = s.stoc_cantidad_minima,
				   @codigo_producto = p.prod_codigo

			from Producto p
			join Stock s on s.stoc_producto = p.prod_id
			where p.prod_id = @producto;

			set @texto = 'Se debe reponer el producto ' + (select cast(@codigo_producto as varchar(max))) + ' - ' 
						 + (select cast(@detalle_producto as varchar(max))) + '. Stock actual: ' + (select cast(@cant_actual as varchar(max))) 
						 + ', Stock mínimo: ' + (select cast(@punto_reposicion as varchar(max)));

			insert into Alerta values (@producto, @texto, 1, (select GETDATE()));
		end		
	end
end

create procedure [dbo].[QuitarAlertaDeReposicion] (@producto int) as
begin

	--if(select count(*) from Combo where comb_id = @producto) > 0
	--begin
	--	declare @comb_componente char(8)
	--	declare componentesCursor cursor for select comb_componente
	--										 from Combo
	--										 where comb_id = @producto

	--	open componentesCursor
	--	fetch next from componentesCursor into @comb_componente

	--	while @@fetch_status = 0​
	--	begin
	--		exec dbo.QuitarAlertaDeReposicion @comb_componente;

	--		fetch next from componentesCursor into @comb_componente
	--	end
		
	--	close componentesCursor​
	--	deallocate componentesCursor	
	--end

	if (select count(*) from Stock where stoc_producto = @producto and stoc_cantidad_actual >= stoc_cantidad_minima) > 0
	begin
		if (select count(*) from Alerta where aler_objeto = @producto) > 0
		begin
			delete from Alerta where aler_objeto = @producto;
		end		
	end
end

create procedure [dbo].[ActualizarStock] (@codigo int, @cantidad int) as
begin

	if(select count(*) from Combo where comb_id = @codigo) > 0
	begin
		declare @comb_componente int;
		declare @comb_cantidad int;

		declare componentesCursor cursor local for select comb_componente, comb_cantidad
												   from Combo
												   where comb_id = @codigo;

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

create procedure [dbo].[InsertarItem] (@codigoVenta int, @producto int, @precioUnitario decimal(12,2), @cantidad int) as
begin

	insert into Item_Venta values (
		@codigoVenta,
		@producto,
		@precioUnitario,
		@cantidad
	);
	
	exec ActualizarStock @producto, @cantidad;

end

create procedure [dbo].[ReponerStock] (@reposicion int, @idProducto int, @cantidadAReponer int) as
begin

	declare @cantidad_vieja int;

	set @cantidad_vieja = (select top 1 stoc_cantidad_actual
						   from Stock
						   where stoc_producto = @idProducto);

	update Stock
	set stoc_cantidad_actual = stoc_cantidad_actual + @cantidadAReponer, stoc_ultima_reposicion = (select GETDATE())
	where stoc_producto = @idProducto;

	insert into Reposicion_Producto values
	(@reposicion, @idProducto, @cantidad_vieja, @cantidad_vieja + @cantidadAReponer)

end

create procedure [dbo].[InsertarProducto] (@codigo nvarchar(5), @detalle nvarchar(255), @precioUnitario decimal(12,2), @rubro int, @stockMinimo int, @id int output) as
begin

	insert into Producto values (		
		@detalle,
		@rubro,
		@precioUnitario,
		1,
		@codigo
	);

	set @id = (select top 1 prod_id 
			   from Producto 
			   where prod_detalle = @detalle and
					 prod_precio = @precioUnitario and
								 prod_rubro = @rubro);

	insert into Stock values (
		@id,
		0,
		@stockMinimo,
		null
	);	

end

create procedure [dbo].[ActualizarProducto] (@id int, @codigo nvarchar(5), @detalle nvarchar(255), @precioUnitario decimal(12,2), @rubro int, @stockMinimo int) as
begin

	update Producto
	set prod_detalle = @detalle, prod_precio = @precioUnitario, prod_rubro = @rubro, prod_codigo = @codigo
	where prod_id = @id;

	update Stock
	set stoc_cantidad_minima = @stockMinimo
	where stoc_producto = @id;

end

create procedure [dbo].[InsertarComponente] (@idProducto int, @idComponente int, @cantidad int) as
begin

	insert into Combo values (
		@idProducto,
		@idComponente,
		@cantidad
	);

end

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