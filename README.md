# Gestión de Ventas - C# (Modelo de 4 Capas)

## Descripción

Este proyecto es una aplicación de **Gestión de Ventas** desarrollada en C# utilizando el **modelo de 4 capas**. El objetivo del sistema es permitir la gestión eficiente de ventas, productos, clientes y reportes, proporcionando una arquitectura robusta y escalable.

## Arquitectura

El proyecto sigue el patrón de **modelo de 4 capas**, dividiendo la aplicación en las siguientes capas:

1. **Capa de Presentación (UI):**
   - Proporciona la interfaz de usuario donde los usuarios interactúan con el sistema.
   - Implementa controles gráficos y vistas utilizando Windows Forms/WPF.
   
2. **Capa de Negocio (BLL):**
   - Contiene la lógica de negocio.
   - Aquí se manejan las reglas, validaciones y transformaciones de datos para su procesamiento.

3. **Capa de Acceso a Datos (DAL):**
   - Se encarga de la comunicación con la base de datos.
   - Implementa las operaciones CRUD (Crear, Leer, Actualizar, Eliminar) a través de consultas y procedimientos almacenados.

4. **Capa de Entidades (Entities):**
   - Define las clases que representan las entidades del negocio (productos, clientes, ventas, etc.).
   - Sirve como puente entre la capa de negocio y la capa de datos.

## Características

- **Gestión de Perfiles y Roles:** Crear, editar y eliminar Usuarios ademas de asignar sus respectivos roles.
- **Gestión de Productos y Categorias:** Manejo de inventarios, registro de productos, y control de stock.
- **Gestión de Ventas:** Registro de ventas, generación de recibos y gestión de pedidos.
- **Reportes:** Generación de reportes detallados de ventas y stock de productos.
- **Interfaz amigable:** Aplicación intuitiva para facilitar la navegación y uso por parte de los usuarios.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o cualquier otro gestor de bases de datos compatible con Entity Framework.
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) o superior.

## Tecnologías Utilizadas

- **C# .NET 8**
- **Entity Framework Core**
- **SQL Server**
- **Windows Forms/WPF (para UI)**

## Instalación

1. Clona el repositorio en tu máquina local:
   ```bash
   git clone https://github.com/tu-usuario/gestion-ventas.git
   
2. Descarga los íconos: La carpeta Icons que contiene los íconos necesarios para el aplicativo no está incluida en el repositorio. Debes descargarla desde el siguiente enlace y colocarla en el directorio raíz del proyecto(Solucion):
-[https://drive.google.com/drive/folders/1YoPVv27IOjtK9dfCQYMLRlAnb0hXNM26?usp=sharing]
