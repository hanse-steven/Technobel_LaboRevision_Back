using System.Text;
using LaboRevision.BLL.Services;
using LaboRevision.Converters;
using LaboRevision.DAL.Repositories;
using LaboRevision.Hubs;
using LaboRevision.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Npgsql;

namespace LaboRevision;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddTransient<NpgsqlConnection>(s =>
        {
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString);
        });

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddScoped<ProductRepository>();
        builder.Services.AddScoped<ProductService>();
        
        builder.Services.AddScoped<CartRepository>();
        builder.Services.AddScoped<CartService>();

        builder.Services.AddScoped<InvoiceRepository>();
        builder.Services.AddScoped<InvoiceService>();

        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<UserRepository>();

        builder.Services.AddScoped<JwtService>();
        
        builder.Services.AddSignalR().AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.Converters.Add(new ProductShortDTOConverter());
        });
        
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(o =>
        {
            o.IdleTimeout = TimeSpan.FromSeconds(1800);
            o.Cookie.IsEssential = true;
            o.Cookie.Name = "LaboRevision";
        });
        
        builder.Services.AddAuthentication(option =>
            {
                // Indique que le système d'authentification et de permission va se baser sur le schema du JWT Bearer
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
                {
                    // Configure la validation du token
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Vérifie que la clé utilisée pour signer le token est valide (TRUE ! Important !)
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                        // Vérifie que le token provient du bon émetteur (optionnel)
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        // Vérifie que le token provient du bon public (optionnel)
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        // Vérifie que le token n'a pas encore expiré
                        ValidateLifetime = true,
                        //ClockSkew = TimeSpan.Zero
                    };
                }
            );

        builder.Services.AddCors(c => c.AddDefaultPolicy(o =>
        {
            o.AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:4200", "http://192.168.0.201:4200");
        }));

        var app = builder.Build();
        
        app.UseHttpsRedirection();

        app.UseCors();
        
        app.UseAuthorization();

        app.UseSession();

        app.MapControllers();

        app.MapHub<CartHub>("/hub/Cart");
        app.MapHub<ProductHub>("/hub/Product");

        app.Run();
    }
}