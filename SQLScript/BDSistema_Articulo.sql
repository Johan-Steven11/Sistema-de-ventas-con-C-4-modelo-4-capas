--create database dbsistema
use dbsistema
-- Tabla categoría 

create table categoria (
	idcategoria integer primary key identity,
	nombre varchar(50) not null unique,
	descripcion varchar(255) null, 
	estado bit default(1)

);

go 
insert into categoria (nombre,descripcion) values ('Dispositivos de cómputo','Todos los dispositivos de cómputo')

select * from categoria


create table articulo (
	idarticulo integer primary key identity,
	idcategoria integer not null,
	codigo varchar(50) null,
	nombre varchar(100) not null unique,
	precio_venta decimal(11,2) not null,
	stock integer not null,
	descripcion varchar(255) null,
	imagen varchar(20) null,
	estado bit default (1),
	FOREIGN KEY (idcategoria) REFERENCES categoria (idcategoria)
);
go


--Table Persona

create table persona (
	idpersona integer primary key identity,
	tipo_persona varchar(50) not null,
	nombre varchar(100) not null,
	tipo_documento varchar(20) null,
	num_documento varchar(20) null,
	direccion varchar(70) null,
	talefono varchar(50) null,
	email varchar(50) null
);
go


--Tabla rol

create table rol (
	id_rol integer primary key identity,
	nombre varchar(30) not null,
	descripcion varchar(255) null,
	estado bit default(1)
);
go

--Tabla Usuario

create table  usuario(

	id_usuario integer primary key identity,
	idrol integer not null,
	nombre varchar(100) not null,
	tipo_documento varchar(20) null,
	num_documento varchar(20) null,
	direccion varchar(70) null,
	telefono varchar(20) null,
	email varchar (50),
	clave varbinary(MAX) not null,
	estado bit default(1),
	FOREIGN KEY (idrol) REFERENCES rol (id_rol)
);
go



--Tabla ingreso

create table ingreso (

	idIngreso integer primary key identity,
	idProveedor integer not null,
	idUsuario integer not null,
	tipo_comprobante varchar(20) not null,
	serie_comprobante varchar(7) null ,
	num_comprobante varchar(10) not null,
	fecha datetime not null, 
	impuesto decimal (4,2) not null,
	total decimal (11,2),
	estado varchar(20) not null,

	FOREIGN KEY (idProveedor) REFERENCES persona  (idpersona),
	FOREIGN KEY (idUsuario) REFERENCES usuario (id_usuario)
);
go


--Table detalle_Ingreso

create table detalle_ingreso (
	idDetalle_ingreso integer primary key identity,
	idIngreso integer not null,
	idArticulo integer not null,
	cantidad integer not null,
	precio decimal (11,2) not null,

	FOREIGN KEY (idIngreso) REFERENCES ingreso (idIngreso) ON DELETE CASCADE,
	FOREIGN KEY (idArticulo) REFERENCES articulo (idarticulo) 
);
go


--Tabla Vneta


create table venta (

	idVenta integer primary key identity,
	idCliente integer not null,
	idUsuario integer not null,
	tipo_documento varchar(20) not null,
	serie_comprobante varchar(7) not null,
	num_comprobante varchar(10) not null,
	fecha datetime not null,
	impuesto decimal (4,2) not null,
	total decimal (11,2) not null,
	estado varchar(20) not null ,


	FOREIGN KEY (idCliente) REFERENCES persona (idpersona),
	FOREIGN KEY (idUsuario) REFERENCES  usuario (id_usuario)

);
go

--Tbale datelle_Venta


create table detalle_venta (
	idDetalle_Venta integer primary key identity,
	idVenta integer not null,
	idArtculo integer not null,
	cantidad integer not null,
	precio decimal (11,2) not null,
	descuento decimal (11,2) not null,

	FOREIGN KEY (idVenta) REFERENCES venta (idVenta),
	FOREIGN KEY (idArtculo) REFERENCES articulo (idarticulo)
);
go


------Procedimientos almacenados-------

------------------Listar---------------

create procedure categoria_listar
as 
select idcategoria as ID, nombre as Nombre, descripcion as Descripcion,estado as Estado 
from categoria order by idcategoria desc
go

-------------------Buscar----------------
create procedure categoria_Buscar
@valor Varchar(50)
as 
select idcategoria as ID, nombre as Nombre, descripcion as Descripcion,estado as Estado 
from categoria 
where nombre like '%' + @valor + '%'  or descripcion like '%' + @valor + '%'  order by idcategoria desc
go

-------------------Insertar----------------

create procedure categoria_insertar
@NOMBRE varchar(50),
@DESCRIPCION VARCHAR(255)
as
insert into categoria (nombre,descripcion) values (@NOMBRE,@DESCRIPCION)
go


-------------------Actualizar-----------------
create procedure categoria_Actualizar
@ID int,
@Nombre VARCHAR(50),
@DESCRIPCION VARCHAR(255)
as
update categoria set nombre = @Nombre, descripcion = @DESCRIPCION where idcategoria = @ID
go

-------------------Eliminar-----------------
create procedure categoria_Eliminar

@ID int
as

delete from categoria where idcategoria = @ID
go

-------------------Desactivar-----------------
create procedure categoria_Desactivar
@ID int 
as 
update categoria set estado=0 where idcategoria = @ID
go
-------------------Activar-----------------
create procedure categoria_Activar
@ID int 
as 
update categoria set estado=1 where idcategoria = @ID
go


--------------Existencia---------------------

create procedure categoria_existe

@valor varchar(100),
@existe bit output
as

if exists (select nombre from categoria where nombre = ltrim(rtrim(@valor)))
	begin
	set @existe = 1
	end
else
	begin
	set @existe= 0
	end

	Exec categoria_listar
--------Procedimientos almacenados para la tabla articulo--------------------
------Procedimiento Listar--------------
go
create procedure articulo_listar
as
select a.idarticulo 
as ID, 
a.idcategoria,c.nombre 
as Categoria,a.codigo 
as Codigo,a.nombre 
as Nombre,a.precio_venta 
as Precio_venta,a.stock 
as Cantidad,a.descripcion 
as Descripcion, a.imagen 
as Imagen,a.estado 
as Estado
from articulo a inner join categoria c on a.idcategoria=c.idcategoria
order by a.idarticulo desc
go
----Procedimiento Buscar---------------
create procedure articulo_buscar
@valor varchar(50)
as
select a.idarticulo 
as ID, 
a.idcategoria,c.nombre 
as Categoria,a.codigo 
as Codigo,a.nombre 
as Nombre,a.precio_venta 
as Precio_venta,a.stock 
as Cantidad,a.descripcion 
as Descripcion, a.imagen 
as Imagen,a.estado 
as Estado
from articulo a inner join categoria c on a.idcategoria=c.idcategoria
where a.nombre like '%' + @valor+ '%' or a.descripcion like '%' + @valor+ '%'
order by a.idarticulo desc 
go
-----Procedimiento Insertar----------
create procedure articulo_insertar
@idcategoria integer,
@codigo varchar(50),
@nombre varchar(100),
@precio_venta decimal (11,2),
@stock integer,
@descripcion varchar(255),
@imagen varchar(20)
as 
insert into articulo (idcategoria,codigo,nombre,precio_venta,stock,descripcion,imagen) 
values (@idcategoria,@codigo,@nombre,@precio_venta,@stock,@descripcion,@imagen)
go
---Procedimiento actualizar----------
create proc articulo_actualizar
@idarticulo integer,
@idcategoria integer,
@codigo varchar(50),
@nombre varchar(100),
@precio_venta decimal (11,2),
@stock integer,
@descripcion varchar(255),
@imagen varchar(20)
as 
update articulo set idcategoria = @idcategoria, codigo=@codigo, nombre=@codigo, precio_venta=@precio_venta, 
stock=@stock, descripcion=@descripcion, imagen=@imagen where idarticulo=@idarticulo
go

-----Procedimiento eliminar-------
create procedure articulo_eliminar
@idarticulo int
as 
delete from articulo where idarticulo = @idarticulo
go

------Procedimiento desactivar---------
create procedure desactivar_articulo
@idarticulo int
as
Update articulo set estado=0 where idarticulo=@idarticulo
go
------Procedimiento Activar---------
create procedure activar_articulo
@idarticulo int
as
Update articulo set estado=1 where idarticulo=@idarticulo
go


--------procedimiento existe------------
create procedure articulo_existe
@valor varchar(50),
@existe bit output
as
if exists (select nombre from articulo where nombre = ltrim(rtrim(@valor)))
	begin
		set @existe=1
	end
else
	begin
		set @existe=0
	end
