# WebApiRest
Web api rest básica hecha con el lenguaje de programación C# con los frameworks ASP.NET y .NET, y la arquitectura MVC. 
Ocupa una base de datos de Mysql con las tablas Usuario y Registro, que además cuentan con Store Procedures para
registrar, listar, obtener, editar y eliminar las entidades antes mencionadas.

# Acerca de...

El proyecto cuenta con las carpetas Data, Models y Controllers.

-Data: En esta carpeta se aloja la clase Conexion.cs donde está el string de conección a la base de datos. Además, cuenta con las clases
UsuarioData.cs y RegistroData.cs, dónde están los métodos para registrar, listar, obtener, editar y eliminar las entidades Usuario y Registro
respectivamente.

-Models: En esta carpeta se alojan las clases Usuario.cs y Registro.cs , que funcionan como plantillas de las entidades Usuario y 
Registro respectivamente.

-Controllers: Aquí se alojan las clases controladores UsuarioController.cs y RegistroController.cs, dónde están los métodos GET, POST,
PUT y DELETE de cada entidad.

# Dependencias

-Microsoft.Asp.Net.Mvc

-Microsoft.Asp.Net.Cors

-Microsoft.Asp.Net.Razor
