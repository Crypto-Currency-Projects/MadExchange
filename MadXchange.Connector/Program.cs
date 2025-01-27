using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using ServiceStack;
using System;
using System.IO;
using ServiceStack.Host.NetCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Funq;


namespace MadXchange.Connector
{
    public class Program
    {
        
        public static int Main(string[] args)
        {
                                
            Log.Logger = MyWebHostExtensions.CreateSerilogLogger(MyWebHostExtensions.GetConfiguration());
            try
            {
                //use ServiceStack Smart AppHostHttpListener for higher request throughput
                //var appHost = new AppHost.AppHost(); 
                //Since request throughput will be one big(small) bottleneck further optimizations will be required
                //to push the limits in parallel order execution.
                //An approach that obtrudes here is request batching. It still needs to be implemented, but since already supported by 
                //servicestack will be easy peasy
                var hostbuilder = CreateHostBuilder(args);
                MyWebHostExtensions.LogPackagesVersionInfo();
                Log.Information("Configuring web host ({ApplicationContext})...", MyWebHostExtensions.AppName);
                var host = hostbuilder.Build();
                var proc = new System.Diagnostics.Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = "http://localhost:5000/";
                proc.Start();
                Log.Information("Starting web host ({ApplicationContext})...", MyWebHostExtensions.AppName);
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", MyWebHostExtensions.AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
         
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)                                 
                   .UseSerilog(Log.Logger)
                   .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                   .CaptureStartupErrors(true)
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseStartup<Startup>()
                   .UseKestrel()
                   .UseSockets(configureOptions: o => o.NoDelay = true);
                              
                
    
    }
   
}




