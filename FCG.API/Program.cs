using FCG.API;
using FCG.API.Infra;
using FCG.API.Infra.Config;
using FCG.API.Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [JWT] Configuração do JWT

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

builder.Services.AddControllers();


#endregion


#region [DB] Configuração do Banco de Dados
string postgresConnectionString = builder.Configuration.GetSection("Connections:PostgreSQL").Value;

//builder.Services.AddHealthChecks()
//    .AddNpgSql(
//    postgresConnectionString,
//    name: "PostgreSQL",
//    failureStatus: HealthStatus.Unhealthy,
//    tags: new[] { "db", "sql", "postgres" });

#endregion


#region [DI] Injeção de dependência
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("Connections"));
builder.Services.AddSingleton<DbConnectionProvider>();
builder.Services.AddScoped<GamesRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//#region [Healthcheck]
//app.UseHealthChecks("/health", new HealthCheckOptions
//{
//    Predicate = _ => true,
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,

//});

app.UseLogMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
