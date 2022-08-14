using API.Data;
using API.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                DatabaseInitializer.Init(scope.ServiceProvider);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    //Класс, который заполняет БД данными 
    internal static class DatabaseInitializer
    {
        internal static void Init(IServiceProvider serviceProvider)
        {
            var appDbContext = serviceProvider.GetService<AppDbContext>();

            var booksArray = new Book[]
            {
                new Book{ Title = "Молоко с кровью", Author = "Люко Дашвар", Date = new DateTime(2008, 1, 1)},
                new Book{ Title = "Элмет", Author = "Фиона Мозли", Date = new DateTime(2017, 1, 1)},
                new Book{ Title = "Пробуждение Левиафана", Author = "Дэниел Абрахам", Date = new DateTime(2011, 1, 1)},
                new Book{ Title = "Прикоснись ко мне", Author = "Джус Аккардо", Date = new DateTime(2011, 1, 1)},
                new Book{ Title = "Леди Полночь", Author = "Кассандра Клэр", Date = new DateTime(2016, 1, 1)},
                new Book{ Title = "Шерше ля нефть. Почему наш Стабилизационный фонд находится ТАМ?", Author = "Николай Стариков", Date = new DateTime(2009, 1, 1)},
                new Book{ Title = "Я, Майя Плисецкая", Author = "Майя Плисецкая", Date = new DateTime(1994, 1, 1)},
                new Book{ Title = "Мойдодыр", Author = "Корней Чуковский", Date = new DateTime(1921, 1, 1)},
                new Book{ Title = "Грузовик дяди Отто", Author = "Стивен Кинг", Date = new DateTime(1983, 1, 1)},
                new Book{ Title = "Семь смертных грехов", Author = "Кори Тейлор", Date = new DateTime(2011, 1, 1)},
                new Book{ Title = "Дядюшкин сон", Author = "Федор Достоевский", Date = new DateTime(1859, 1, 1)},
                new Book{ Title = "Ночные кошмары (авторский сборник)", Author = "Стивен Кинг", Date = new DateTime(2003, 1, 1)}
            };

            appDbContext.books.AddRange(booksArray);
            appDbContext.SaveChanges();
        }
    }
}
