using LaboRevision.BLL.Services;
using LaboRevision.Converters;
using LaboRevision.DAL.Repositories;
using LaboRevision.Hubs;
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