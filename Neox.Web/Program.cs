using Neox.Repositories;
using Neox.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();  // Add SeriLog
                                // Add services to the container.
    builder.Services.AddControllers();



    Log.Information("Starting web application");

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //  Inyeccion de dependencias - Servicios de aplicacion
    builder.Services.AddTransient<IClientService, ClientService>();
    //  Inyeccion de dependencias - Repositorios (infraestructura)
    builder.Services.AddTransient<IClientRepository, ClientSqlLiteRepository>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
