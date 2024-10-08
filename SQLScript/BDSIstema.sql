--create database dbsistema
create database dbsistema
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
update articulo set idcategoria = @idcategoria, codigo=@codigo, nombre=@nombre, precio_venta=@precio_venta, 
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



	--Insertar Roles


	insert into rol (nombre) values ('Administrador');
	insert into rol (nombre) values ('Vendedor');
	insert into rol (nombre) values ('Almacenero');


	---Procedimientos almacenados Roles----

	-------Procedimiento Listar----
	go
	create procedure Listar_Rol 
	as
	select id_rol,nombre from rol where estado = 1
	go


	---Procedimientos almacenados Usuarios---
	---Procedimineto Listar---
	create procedure Usuario_Listar
	as 
	select u.id_usuario as Id, u.idrol as Id_Rol, 
	r.nombre as Rol, 
	u.nombre as  Nombre,
	u.tipo_documento as Tipo_Documento,
	u.num_documento as Numero_Documento,
	u.direccion as Direccion,
	u.telefono as Telefono,
	u.email as Email,
	u.estado as Estado from usuario u inner join rol r on u.idrol=r.id_rol
	order by u.id_usuario desc
	go
	---Procedimineto Buscar---
	create procedure Usuario_Buscar
	@Valor varchar(50)
	as 
	select u.id_usuario as Id, u.idrol as Id_Rol, 
	r.nombre as Rol, 
	u.nombre as  Nombre,
	u.tipo_documento as Tipo_Documento,
	u.num_documento as Numero_Documento,
	u.direccion as Direccion,
	u.telefono as Telefono,
	u.email as Email,
	u.estado as Estado from usuario u inner join rol r on u.idrol=r.id_rol
	where u.nombre like '%' +@Valor + '%' Or u.email like '%' +@Valor + '%'
	order by u.nombre asc
	go
	---Procedimineto Insertar---
	create procedure Usuario_Insertar
	@IdRol integer,
	@Nombre varchar(100),
	@Tipo_documento varchar(20),
	@Num_Documento varchar(20),
	@Direccion varchar(70),
	@Telefono varchar(20),
	@Email varchar(50),
	@Clave varchar(50)
	as
	insert into usuario (idrol,nombre,tipo_documento,num_documento,direccion,telefono,email,clave)
	values(@IdRol,@Nombre,@Tipo_documento,@Num_Documento,@Direccion,@Telefono,@Email,HASHBYTES('SHA2_256',@Clave))
	go
	---Procedimineto Actualizar---
	create procedure Usuario_Actualizar
	@idUsuario Integer, 
	@idRol integer,
	@Nombre varchar(100),
	@Tipo_Documento varchar(20),
	@Numero_Documento varchar(20),
	@Direccion varchar(70),
	@Telefono varchar(20),
	@Email varchar(50),
	@Clave varchar(50)
	as 
	if @Clave<>''
	Update usuario set 
	idrol=@idRol, 
	nombre = @Nombre, 
	tipo_documento = @Tipo_Documento,
	num_documento=@Numero_Documento,
	direccion=@Direccion,
	telefono=@Telefono,
	email=@Email,
	clave=HASHBYTES('SHA2_256',@Clave)
	Where id_usuario = @idUsuario
	else
	Update usuario set 
	idrol=@idRol, 
	nombre = @Nombre, 
	tipo_documento = @Tipo_Documento,
	num_documento=@Numero_Documento,
	direccion=@Direccion,
	telefono=@Telefono,
	email=@Email
	where id_usuario = @idUsuario
	go
	---Procedimineto Eliminar---
	create procedure Eliminar_Usuario
	@Id_Usuario integer
	as
	Delete  from usuario where id_usuario=@Id_Usuario
	go
	---Procedimineto Desactivar---
	create procedure Desactivar_Usuario
	@Id_Usuario Integer
	as 
	Update usuario set estado=0 where id_usuario = @Id_Usuario
	go
	---Procedimineto Activar---
	create procedure Activar_Usuario
	@Id_Usuario Integer
	as 
	Update usuario set estado=1 where id_usuario = @Id_Usuario
	go
	---Procedimiento Existe---

	create procedure usuario_existe
	@valor varchar(100),
	@existe bit output 
	as
		if exists (select email from usuario where email = ltrim(trim(@valor)))
			begin
				set @existe=1
			end
		else
			begin 
				set @existe=0
			end
go

------Procedimiento almacenado Usuario Login-----------

create procedure usuario_login 
@email varchar(50),
@clave varchar(50)
as
select u.id_usuario, u.idrol, r.nombre as Rol, u.nombre, u.estado  
from usuario u inner join rol r on u.idrol = r.id_rol
where u.email = @email and u.clave = HASHBYTES('SHA2_256', @clave)
go

-----Procedimientos almacenados Personas (Clientes o Proveedores)-----

--Procedimiento Listar--
create  procedure Persona_Listar
as
select idpersona as ID, 
tipo_persona as Tipo_Persona, 
nombre as Nombre, 
tipo_documento as  Tipo_Documento,
num_documento as Documento,
direccion as Direccion,
talefono as Telefono ,email as Email from persona order by idpersona desc
go


--Procedimiento Listar Proveedor--
create procedure Persona_Listar_Proveedores
as
select idpersona as ID, 
tipo_persona as Tipo_Persona, 
nombre as Nombre, 
tipo_documento as  Tipo_Documento,
num_documento as Documento,
direccion as Direccion,
talefono as Telefono,
email as Email from persona where tipo_persona='Proveedor' order by idpersona desc
go


--Procedimineto Listar Clientes--
create procedure Persona_Listar_Clientes
as
select idpersona as ID, 
tipo_persona as Tipo_Persona, 
nombre as Nombre, 
tipo_documento as  Tipo_Documento,
num_documento as Documento,
direccion as Direccion,
talefono as Telefono,
email as Email from persona where tipo_persona='Clientes' order by idpersona desc
go
--Procedimiento Buscar--
create procedure Persona_Buscar
@Valor Varchar(50)
as
select idpersona as ID, 
tipo_persona as Tipo_Persona, 
nombre as Nombre, 
tipo_documento as  Tipo_Documento,
num_documento as Documento,
direccion as Direccion,
talefono as Telefono,
email as Email from  persona
where nombre like '%' + @Valor + '%' Or email like '%' + @valor + '%'
order by nombre asc
go
--Procedimineto Buscar Proveedores--
create procedure Persona_Buscar_Proveedores
@Valor Varchar(50)
as
select idpersona as ID, 
tipo_persona as Tipo_Persona, 
nombre as Nombre, 
tipo_documento as  Tipo_Documento,
num_documento as Documento,
direccion as Direccion,
talefono as Telefono,
email as Email from  persona
where (nombre like '%' + @Valor + '%' Or email like '%' + @valor + '%') and tipo_persona='Proveedor'
order by nombre asc
go
--Procedimiento Buscar Cliente--
create procedure Persona_Buscar_Clientes
@Valor Varchar(50)
as
select idpersona as ID, 
tipo_persona as Tipo_Persona, 
nombre as Nombre, 
tipo_documento as  Tipo_Documento,
num_documento as Documento,
direccion as Direccion,
talefono as Telefono,
email as Email from  persona
where (nombre like '%' + @Valor + '%' Or email like '%' + @valor + '%') and tipo_persona='Clientes'
order by nombre asc
go

--Procedimiento Insertar--
create procedure persona_insertar
@tipo_persona varchar(20),
@Nombre varchar(100),
@tipo_documento varchar(20),
@num_documento varchar(20),
@direccion varchar(70),
@telefono varchar(20),
@email varchar(50)
as 
insert into persona (tipo_persona,nombre,tipo_documento,num_documento,direccion,talefono,email) values
(@tipo_persona,@Nombre,@tipo_documento,@num_documento,@direccion,@telefono,@email)
go

--Procedimiento Actualizar--
create procedure persona_Actualizar
@idpersona integer,
@tipo_persona varchar(20),
@Nombre varchar(100),
@tipo_documento varchar(20),
@num_documento varchar(20),
@direccion varchar(70),
@telefono varchar(20),
@email varchar(50)
as
update persona set tipo_persona=@tipo_persona, nombre=@Nombre, tipo_documento=@tipo_documento,num_documento= @num_documento,direccion=@direccion, 
talefono = @telefono , email = @email where idpersona = @idpersona
go
--Procedimiento Eliminar--
create procedure persona_Eliminar

@idPersona integer
as 
delete  from persona where idpersona = @idPersona
go

--Procedimiento Existe--
create procedure persona_Existe
@valor varchar(100),
@existe bit output
as
		if exists (select nombre from persona where nombre = ltrim(trim(@valor)))
			begin
				set @existe=1
			end
		else
			begin 
				set @existe=0
			end
go


