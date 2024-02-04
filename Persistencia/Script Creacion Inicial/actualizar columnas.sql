USE [DISTRIBUIDORA]
GO

EXEC sp_rename 'dbo.Producto.prod_codigo', 'prod_id', 'COLUMN';
EXEC sp_rename 'dbo.Combo.comb_codigo', 'comb_id', 'COLUMN';

USE [DISTRIBUIDORA]
GO
ALTER TABLE [dbo].[Producto]
ADD prod_codigo nvarchar(5);
GO

USE [DISTRIBUIDORA]
GO
update [dbo].[Producto]
set prod_codigo = prod_id
GO

ALTER TABLE [dbo].[Producto]
ADD prod_ultima_modificacion smalldatetime;
GO

USE [DISTRIBUIDORA]
GO

/****** Object:  UserDefinedFunction [dbo].[HayStockDisponible]    Script Date: 26/12/2022 17:39:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER function [dbo].[HayStockDisponible] (@producto int, @cantidad int)
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

alter function [dbo].[InformarStockFaltante] (@producto int, @cantidad int)
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

USE [DISTRIBUIDORA]
GO

/****** Object:  UserDefinedFunction [dbo].[LlegoAPuntoDeReposicion]    Script Date: 26/12/2022 17:40:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER function [dbo].[LlegoAPuntoDeReposicion] (@producto int)
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

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  StoredProcedure [dbo].[QuitarAlertaDeReposicion]    Script Date: 26/12/2022 17:42:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[QuitarAlertaDeReposicion] (@producto int) as
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

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  StoredProcedure [dbo].[InsertarProducto]    Script Date: 26/12/2022 17:43:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[InsertarProducto] (@codigo nvarchar(5), @detalle nvarchar(255), @precioUnitario decimal(12,2), @rubro int, @stockMinimo int, @id int output) as
begin

	insert into Producto values (		
		@detalle,
		@rubro,
		@precioUnitario,
		1,
		@codigo, 
		getdate()
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

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  StoredProcedure [dbo].[EmitirAlertaDeReposicion]    Script Date: 26/12/2022 17:44:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[EmitirAlertaDeReposicion] (@producto int) as
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

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  StoredProcedure [dbo].[ActualizarStock]    Script Date: 26/12/2022 17:44:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[ActualizarStock] (@codigo int, @cantidad int) as
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

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 26/12/2022 17:44:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[ActualizarProducto] (@id int, @codigo nvarchar(5), @detalle nvarchar(255), @precioUnitario decimal(12,2), @rubro int, @stockMinimo int) as
begin

	update Producto
	set prod_detalle = @detalle, prod_precio = @precioUnitario, prod_rubro = @rubro, prod_codigo = @codigo, prod_ultima_modificacion = getdate()
	where prod_id = @id;

	update Stock
	set stoc_cantidad_minima = @stockMinimo
	where stoc_producto = @id;

end

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  StoredProcedure [dbo].[InsertarComponente]    Script Date: 26/12/2022 20:09:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[InsertarComponente] (@idProducto int, @idComponente int, @cantidad int) as
begin

	insert into Combo values (
		@idProducto,
		@idComponente,
		@cantidad
	);

end

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  StoredProcedure [dbo].[ReponerStock]    Script Date: 28/12/2022 0:56:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[ReponerStock] (@reposicion int, @idProducto int, @cantidadAReponer int) as
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

GO


USE [DISTRIBUIDORA]
GO

/****** Object:  StoredProcedure [dbo].[InsertarItem]    Script Date: 29/12/2022 21:21:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[InsertarItem] (@codigoVenta int, @producto int, @precioUnitario decimal(12,2), @cantidad int) as
begin

	insert into Item_Venta values (
		@codigoVenta,
		@producto,
		@precioUnitario,
		@cantidad
	);
	
	exec ActualizarStock @producto, @cantidad;

end

GO



USE [DISTRIBUIDORA]
GO

/****** Object:  View [dbo].[Venta_View]    Script Date: 26/12/2022 17:50:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER view [dbo].[Venta_View] 
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

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  View [dbo].[Reposicion_View]    Script Date: 26/12/2022 17:51:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER view [dbo].[Reposicion_View] 
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

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  View [dbo].[Producto_View]    Script Date: 26/12/2022 17:51:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*---------------------------------------------------------*/
/*----------------CREACIÓN DE VISTAS-----------------*/
/*---------------------------------------------------------*/

ALTER view [dbo].[Producto_View] 
as
	select prod_id Id,
		   prod_codigo Codigo, 
		   prod_detalle Detalle, 
		   prod_precio Precio, 
		   rubr_codigo RubroCodigo, 
		   rubr_detalle RubroDetalle, 
		   stoc_cantidad_actual StockActual, 
		   stoc_cantidad_minima PtoReposicion, 
		   stoc_ultima_reposicion UltimaReposicion,
		   prod_ultima_modificacion UltimaModificacion

	from Producto
	join Rubro on rubr_codigo = prod_rubro
	join Stock on stoc_producto = prod_id
	where prod_activo = 1

GO

USE [DISTRIBUIDORA]
GO

/****** Object:  View [dbo].[Combo_View]    Script Date: 26/12/2022 17:51:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER view [dbo].[Combo_View] 
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

GO

delete
from Alerta
where aler_objeto in (select comb_id from Combo)