using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GenerationClient
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static async Task Main(string[] args)
        {
            string grpcUrl = GetGrpcUrl();

            if (grpcUrl == null)
            {
                Environment.Exit(404);
            }

            GenerationDiscountCodeClient client = new GenerationDiscountCodeClient(grpcUrl);
            GreetClient greetClient = new GreetClient(grpcUrl);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"DiscountCode gRPC server will be contacted on: {grpcUrl}");
                Console.WriteLine($"\r\nGreet received: {await greetClient.DoTheGreet("gRPC Developer")}");
                Console.WriteLine("\r\nMake your choice");
                Console.WriteLine("\t1. Generat DiscountCode");
                Console.WriteLine("\t2. Use DiscountCode");
                Console.WriteLine("\t3. Get All DiscountCode");
                Console.WriteLine("\t4. Delete All DiscountCode");
                Console.WriteLine("\t0. EXIT");
                Console.Write("\r\nEnter your choice: ");
                var choice = Console.ReadKey();
                Console.Clear();

                switch (choice.KeyChar)
                {
                    case '1':
                        await client.GenerateCode();
                        break;
                    case '2':
                        await client.UseDiscountCode();
                        break;
                    case '3':
                        await client.GetAllDiscountCode();
                        break;
                    case '4':
                        await client.DeleteAllDiscount();
                        break;
                    case '0':
                        Console.WriteLine();
                        Console.WriteLine("Thank you");
                        await Task.Delay(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
                Console.ReadLine();
            }
        }
        static string GetGrpcUrl()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

           Configuration = builder.Build();

            try
            {
                return Configuration.GetConnectionString("GrpcServer");
            }
            catch
            {
                Console.WriteLine("Could not find GRPC url in the settings");
                Console.ReadLine();
                return null;
            }
        }
    }
}
