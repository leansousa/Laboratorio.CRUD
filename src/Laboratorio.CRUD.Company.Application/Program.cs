using Laboratorio.CRUD.Company.Application.Extensions;
using Laboratorio.CRUD.Company.Infra.Data.Context;
using Laboratorio.CRUD.Company.Infra.Data.DBClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//EF
string connection = builder.Configuration["DBConnection:SQLServerConnectionString"] ?? "";
builder.Services.AddDbContext<SqlServerContext>(options =>
{
    options.UseSqlServer(connection);
});
//EF FIM

builder.Services.AddSingleton(new SqlServerConnection(connection));

builder.Services.AddControllers();

builder.Services.ConfigureDI();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var urlCorsAllowed = builder.Configuration.GetValue<string>("Custom:URL_CORS_ALLOWED") ?? "https://localhost";

app.UseCors(builder =>
{
    builder
    .WithOrigins(urlCorsAllowed)
    //.SetIsOriginAllowed(_ => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Migrate latest database changes during startup

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<SqlServerContext>();

    dbContext.Database.Migrate();
}

app.Run();
