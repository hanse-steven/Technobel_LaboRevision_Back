using LaboRevision.BLL.Services;
using LaboRevision.DAL.Repositories;
using LaboRevision.Hubs;

namespace LaboRevision;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddScoped<ProductRepository>();
        builder.Services.AddScoped<ProductService>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSignalR();
        
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
                .WithOrigins("http://localhost:4200");
        }));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseCors();
        
        app.UseAuthorization();

        app.UseSession();

        app.MapControllers();

        app.MapHub<CartHub>("/hub/Cart");

        app.Run();
    }
}