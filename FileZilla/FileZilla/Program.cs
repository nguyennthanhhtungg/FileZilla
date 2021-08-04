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
                Console.WriteLine("args is empty!");
            }
            else
            {
                if(args[0] == "OrderList.csv")
                {
                    string fileData = await scope.ServiceProvider.GetRequiredService<IOrderService>().ExportOrderSheetToCsvFileData();
                    await scope.ServiceProvider.GetRequiredService<IFileZillaService>().UploadFile(fileData, "OrderList.csv");
                }
                else if (args[0] == "SalaryList.csv")
                {
                    string fileData = await scope.ServiceProvider.GetRequiredService<IEmployeeService>().ExportSalarySheetToCsvFileDataByMonthYear(Int32.Parse(args[1]), Int32.Parse(args[2]));
                    await scope.ServiceProvider.GetRequiredService<IFileZillaService>().UploadFile(fileData, "SalaryList.csv");
                }
                else if (args[0] == "ExpiredProductList.csv")
                {
                    string fileData = await scope.ServiceProvider.GetRequiredService<IProductService>().ExportExpiredProductListToCsvFileData();
                    await scope.ServiceProvider.GetRequiredService<IFileZillaService>().UploadFile(fileData, "ExpiredProductList.csv");
                }
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
            services.AddScoped<IProductRepository, ProductRepository>();

            //Add Services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IFileZillaService, FileZillaService>();
            services.AddScoped<IProductService, ProductService>();

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
