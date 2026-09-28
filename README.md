Mini ERP
Una aplicación web sencilla para llevar el control de los empleados de una empresa: quiénes son, en qué área trabajan y qué puesto tienen.
Se piensa en ella como una libreta de contactos, pero para una empresa: puedes ver, buscar y dar de alta empleados está hecha con Blazor y .NET 10, y guarda todo en una base de datos de SQL Server.
 ¿Qué puedo hacer con el Mini ERP?
La app tiene tres pantallas, que se abren desde el menú de la izquierda.
Inicio
Es la pantalla de bienvenida. Trae un botón para ir directo a los empleados y un botón de "Probar interacción", que solo es una demo para ver cómo reacciona la página cuando le das clic. No guarda nada.
Empleados
Aquí está lo principal. En esta pantalla puedes:
•	Ver el directorio completo. Arriba salen tres números: cuántos empleados hay en total, cuántos puestos distintos existen y cuántos resultados se están mostrando ahorita.
•	Buscar en vivo. Conforme escribes en el buscador, la lista se va filtrando sola. Busca por nombre, puesto, correo, número de empleado, CURP, teléfono o género. Si escribes solo 1 letra, la pantalla te sugiere poner al menos 2 para que la búsqueda sea útil.
•	Cambiar cómo los ves. Hay dos vistas: tarjetas (más visual, con las iniciales de cada persona) y tabla (con todos los datos: número, nombre, género, cumpleaños, CURP, teléfono, puesto y correo).
•	Moverte por páginas. Se muestran 4 empleados a la vez, con botones de "Anterior" y "Siguiente".
•	Agregar un empleado nuevo. Hay un formulario abajo donde capturas sus datos. Te va avisando si algo está mal escrito (ver la siguiente sección).
Departamentos
Aquí armas la estructura de la empresa:
•	Agregar áreas (por ejemplo: Sistemas, Ventas, Recursos Humanos).
•	Agregar puestos dentro de cada área (por ejemplo: dentro de Sistemas, "Programador").
•	Ver todas las áreas con los puestos que tiene cada una.
•	No te deja repetir: si ya existe un área con ese nombre, o ya existe ese puesto dentro de esa misma área, te avisa y no lo guarda doble.
Ojo: primero hay que crear áreas y puestos, porque el formulario de empleados los necesita. La base de datos arranca vacía, sin nada precargado.
 ¿Qué revisa el formulario de empleados?
Antes de guardar a alguien, la app se asegura de que los datos tengan sentido:
Dato	Qué se le pide
Número de empleado	Solo números, entre 4 y 8 dígitos. Y no puede estar repetido.
Nombre	Solo letras y espacios (acepta acentos y ñ). Máximo 60 caracteres.
Género	Femenino, Masculino o No especificar.
Fecha de cumpleaños	Obligatoria y no puede ser una fecha futura.
CURP	Con el formato oficial de 18 caracteres. La app la pasa a mayúsculas sola.
Teléfono	Exactamente 10 dígitos, sin guiones ni espacios.
Área	Se elige de la lista de departamentos que ya creaste.
Puesto	Se elige de la lista, y solo salen los puestos del área que escogiste.
Correo	Que tenga forma de correo (algo@algo.com). Máximo 100 caracteres.
Si algo no cumple, te sale un mensajito en rojo debajo del campo diciéndote qué arreglar. Además, si intentas registrar un número de empleado que ya existe, te avisa.

¿Qué necesito para correrlo en mi compu?
1.	.NET 10 SDK (el programa para poder compilar y ejecutar la app).
2.	SQL Server funcionando (puede ser una instalación normal, Express o en Docker).
3.	Un editor como Visual Studio o VS Code (opcional, pero ayuda).
Paso 1: revisa la conexión a tu base de datos
Abre el archivo hola-mundo/hola-mundo/appsettings.json y busca esta línea:
"DefaultConnection": "Data Source=localhost,11435;Initial Catalog=MiniErp;Integrated Security=true;trustServerCertificate=true"
Esa es la "dirección" de tu SQL Server. Cámbiala si el tuyo está en otro puerto, tiene otro nombre o usa usuario y contraseña.
Tip: si no quieres modificar el archivo, puedes definir una variable de entorno llamada ConnectionStrings__DefaultConnection con la dirección, y la app usará esa en lugar de la del archivo.
Paso 2: crea las tablas en la base de datos
Desde la carpeta hola-mundo/hola-mundo (la del servidor), corre:
dotnet tool install --global dotnet-ef   # solo la primera vez
dotnet ef database update
Eso crea la base de datos MiniErp con sus tablas de empleados, departamentos y puestos.
Paso 3: arranca la app
En esa misma carpeta:
dotnet run
Y abre en tu navegador http://localhost:5189 (o https://localhost:7297).
¿Cómo está armado por dentro? (versión fácil)
Imagina un restaurante con tres partes:
•	El comedor (lo que ves en el navegador): las pantallas, botones y formularios. Vive en la carpeta hola-mundo.Client.
•	La cocina (el servidor): recibe los pedidos del comedor, revisa que estén bien y los guarda o los busca. Vive en la carpeta hola-mundo, en Controllers.
•	La bodega (SQL Server): donde de verdad se guardan los datos para que no se pierdan.
Cuando agregas un empleado, pasa esto:
1.	Llenas el formulario y le das a Agregar.
2.	La pantalla revisa que los datos estén bien escritos.
3.	Le pregunta al servidor si ese número de empleado ya existe.
4.	Si no existe, el servidor lo guarda en SQL Server.
5.	La lista se recarga y el nuevo empleado aparece.
Un vistazo a las carpetas
hola-mundo/
├── hola-mundo.Client/        ← Lo que se ve en el navegador
│   ├── Pages/                ← Las pantallas: Inicio, Empleados y Departamentos
│   ├── Shared/               ← Piezas reutilizables: formulario, tabla y tarjeta de empleado
│   ├── Models/               ← Cómo es un Empleado, un Departamento y un Puesto (y sus reglas)
│   └── Services/             ← Los "mensajeros" que le piden cosas al servidor
│
└── hola-mundo/               ← El servidor
    ├── Controllers/          ← Reciben los pedidos: empleados, departamentos y puestos
    ├── Data/                 ← Conexión con SQL Server y forma de las tablas
    ├── Migrations/           ← Instrucciones para crear/actualizar las tablas
    └── Components/Layout/    ← El menú lateral y el marco general de la página
Si quieres tocar algo puntual, casi siempre es aquí:
Quiero cambiar...	Voy a...
Cuántos empleados salen por página	Models/DirectorioEmpleados.cs (la constante EmpleadosPorPagina)
Qué campos se usan en la búsqueda	Models/DirectorioEmpleados.cs
Las reglas de validación (CURP, teléfono, etc.)	Models/Empleado.cs
El formulario de alta de empleados	Shared/FormularioEmpleado.razor
El diseño de la tabla	Shared/TablaEmpleados.razor
La conexión a la base de datos	appsettings.json

Cosas que conviene saber (pendientes conocidos)
Para ser honestos, estas cosas hoy no funcionan como uno esperaría:
•	El correo puede salir vacío al volver a cargar la lista. El servidor guarda el correo con el nombre Email, pero la pantalla lo lee con el nombre Correo, y al no coincidir no se enlazan. Se arregla poniéndoles el mismo nombre en ambos lados.
•	Al empleado se le guarda el nombre del puesto, no el área. Aunque en el formulario eliges área y puesto, en la base de datos solo se queda el texto del puesto. Por eso hoy no se puede saber, a partir de un empleado, en qué área está.
•	No se pueden editar ni borrar empleados, áreas ni puestos: por ahora solo se puede consultar y agregar.
•	No hay inicio de sesión. Cualquiera que entre a la página puede ver y agregar información.
Tecnologías usadas
•	Blazor WebAssembly (la parte que corre en el navegador)
•	ASP.NET Core (el servidor y su API)
•	Entity Framework Core (el traductor entre C# y la base de datos)
•	SQL Server (la base de datos)
•	Bootstrap (estilos base de botones, tabla y demás)

