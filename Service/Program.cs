namespace MultiLane.Service;

public class Program
{
    public static string ServiceName;
    public static string TargetServiceName;

    public static void Main(string[] args)
    {
        ServiceName = args[0];
        TargetServiceName = args[1];

        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpClient("default", (sp, client) =>
        {
            client.BaseAddress = new Uri($"http://{TargetServiceName}");
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();

        app.Run();
    }
}

