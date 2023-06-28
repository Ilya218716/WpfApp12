using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using WebApplication2;
using WebApplication2.Context;
using Microsoft.EntityFrameworkCore;
public class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();

    }
    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();



}