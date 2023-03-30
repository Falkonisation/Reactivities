using Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Activities;
using MediatR;
using Application.Core;
using API.Extensions;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
/*
        builder.Services.AddControllers();

        builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",policy=>{
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });
        builder.Services.AddMediatR(typeof(List.Handler).Assembly);
        builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
*/      ApplicationServiceExtensions.Addservices(builder);
        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var serv = scope.ServiceProvider;
        try
        {
            var context = serv.GetRequiredService<DataContext>();
           await context.Database.MigrateAsync();
            await Seed.SeedData(context);
        }
        catch (Exception exc)
        {
            var logger = serv.GetRequiredService<Logger<Program>>();
            logger.LogError(exc, "There is ERROR occured during migration");
        }

        app.Run();
    }
}