using BattleShip;
using BattleShip.Field;
using BattleShip.Models;
using Application.Services;
using DAL.InMemoryDB;
using System.Net.WebSockets;
using API.Extensions;
using FluentValidation.AspNetCore;
using API.Errorhandler;
namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddAuthenticationCore();
            builder.Services.AddSingleton<IUserRepository<PlayingField, Ship>, UserRepository<PlayingField, Ship>>();
            builder.Services.AddSingleton<IRepository<PlayingField>, Repository<PlayingField>>();
            builder.Services.AddSingleton<IRepository<User>, Repository<User>>();
            builder.Services.AddSingleton<IRepository<Ship>, Repository<Ship>>();
            builder.Services.AddSingleton<IUserSetupService, UserSetupService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IShipService, ShipService>();
            builder.Services.AddSingleton<IPlayingFieldService, PlayingFieldService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            
            app.UseWebSockets();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseFieldValidation();

            app.UsePostShipValidation();

            app.UsePutShipvalidator();

            app.UseUserValidation();

            app.UseExceptionHandler();

            app.MapControllerRoute(name: "default",
               pattern: "{controller=WebSocket}/{action=fieldState}/{id?}");

            app.MapControllers();


            app.Run();
        }
    }
}
