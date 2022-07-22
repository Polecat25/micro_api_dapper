

using Micro_Helper.MicroServicies.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvcCore().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//conexiones 
var MysqlConnectionConfig = new ConexionConfiguracion.MySqlConfiguracion(builder.Configuration.GetConnectionString("MysqlConexion"));
var SqlServerConnectionConfig = new ConexionConfiguracion.SqlserverConfiguracion(builder.Configuration.GetConnectionString("SQLServerConexion"));//para la conexion al sql server
builder.Services.AddSingleton(MysqlConnectionConfig);
builder.Services.AddSingleton(SqlServerConnectionConfig);
// interfaces y repositorios, aqui van todos los repositorios y sus respectivas interfaces
builder.Services.AddScoped<Ipersonas, personaRepositorio>();
builder.Services.AddScoped<Iusuario, usuarioRepositorio>();

builder.Services.AddScoped<IusuarioServicio, UsuarioServicio>();


//JWT
var appSettingsSeccion = builder.Configuration.GetSection("apiSec");
builder.Services.Configure<appSettings>(appSettingsSeccion);

var getllave = appSettingsSeccion.Get<appSettings>();
var llave = Encoding.ASCII.GetBytes(getllave.keySecret);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(d =>
{
    d.RequireHttpsMetadata = false;
    d.SaveToken = true;
    d.TokenValidationParameters = new TokenValidationParameters
    {
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(llave),
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateLifetime = true,
    };
    
});


//CORS

builder.Services.AddCors(optios =>
{
    optios.AddPolicy(name: "_CORS", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("_CORS");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
