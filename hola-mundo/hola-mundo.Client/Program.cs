using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using hola_mundo.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

await builder.Build().RunAsync();