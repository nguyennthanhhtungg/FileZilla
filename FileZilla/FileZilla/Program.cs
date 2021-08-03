using FileZilla.Models;
using FileZilla.Repositories;
using FileZilla.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace FileZilla
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Server is running...!");

            RegisterServices();
            IServiceScope scope = serviceProvider.CreateScope();
            if (args.Length == 0)
            {
                string fileData = await scope.ServiceProvider.GetRequiredService<IOrderService>().ExportOrderSheetToCsvFileData();
                await scope.ServiceProvider.GetRequiredService<IFileZillaService>().UploadFile(fileData, "OrderList.csv");
            }
            else
            {
                string fileData = await scope.ServiceProvider.GetRequiredService<IEmployeeService>().ExportSalarySheetToCsvFileDataByMonthYear(Int32.Parse(args[0]), Int32.Parse(args[1]));
                await scope.ServiceProvider.GetRequiredService<IFileZillaService>().UploadFile(fileData, "SalarySheet.csv");
            }

            DisposeServices();
        }

        //Register Services
        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            //Add Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IExtraFeeRepository, ExtraFeeRepository>();

            //Add Services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IFileZillaService, FileZillaService>();

            serviceProvider = services.BuildServiceProvider(true);
        }

        //Dispose Services
        private static void DisposeServices()
        {
            if (serviceProvider == null)
            {
                return;
            }

            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
