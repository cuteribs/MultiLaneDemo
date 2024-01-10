using Hellang.Middleware.ProblemDetails;

namespace MultiLane.Service;

public class Program
{
    public static string? ServiceName;
    public static string? TargetService1;
    public static string? TargetService2;

    public static void Main(string[] args)
    {
        ServiceName = args.ElementAtOrDefault(0);
        TargetService1 = args.ElementAtOrDefault(1);
        TargetService2 = args.ElementAtOrDefault(2);

        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddProblemDetails();

        var baseUrl = builder.Configuration["BaseUrl"];

        if (TargetService1 != null)
        {
            services.AddHttpClient(TargetService1, (sp, client) => client.BaseAddress = new Uri($"{baseUrl}/{TargetService1}"));
        }

        if (TargetService2 != null)
        {
            services.AddHttpClient(TargetService2, (sp, client) => client.BaseAddress = new Uri($"{baseUrl}/{TargetService2}"));
        }

        var app = builder.Build();

        app.UseProblemDetails();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();

        app.Run();
    }
}

