using FCG.API;
using FCG.API.Infra;
using FCG.API.Infra.Config;
using FCG.API.Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Title = "FIAP Cloud Games (FCG)",
        Version = "V1",
        Description =
            "Plataforma de venda de jogos digitais e gestão de servidores para partidas online \n\n" +
            "Contatos:\n\n" +
            "• Fernando Ribeiro\n" +
            "• João Paulo\n" +
            "• Lucas Nunes RM 369391\n" +
            "• Marcos Antonio RM 368502\n" +
            "• Oberdan RM 369592\n",
        Contact = new OpenApiContact
        {
            Name = "Equipe FCG",
            Email = "teste@teste.com"
        },

    });
});

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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "FIAP Cloud Games (FCG) V1");
    });

    app.UseReDoc(c =>
    {
        c.DocumentTitle = "FIAP Cloud Games (FCG)";
        c.SpecUrl = "/swagger/V1/swagger.json";
    });
}

app.UseLogMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
