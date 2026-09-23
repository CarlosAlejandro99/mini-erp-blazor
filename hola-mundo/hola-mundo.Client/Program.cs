using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using hola_mundo.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();

builder.Services.AddScoped<IPuestoService, PuestoService>();

await builder.Build().RunAsync();