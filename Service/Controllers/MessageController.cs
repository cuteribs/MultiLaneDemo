using Microsoft.AspNetCore.Mvc;

namespace MultiLane.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly ILogger _logger;

    public MessageController(ILogger<MessageController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IEnumerable<string>?> Post(
        [FromHeader(Name = "x-lane")] string lane,
        [FromBody] IEnumerable<string> messages
    )
    {
        messages = messages.Append($"➡ {Program.ServiceName} on {Environment.GetEnvironmentVariable("HOSTNAME")}");

        if (Program.TargetService1 == null && Program.TargetService2 == null) return messages;

        messages = await this.SendMessage(Program.TargetService1, messages, lane);
        messages = await this.SendMessage(Program.TargetService2, messages, lane);
        return messages;
    }

    private async Task<IEnumerable<string>> SendMessage(
        string? targetService,
        IEnumerable<string> messages,
        string lane
    )
    {
        if (targetService == null) return messages;

        var client = this.HttpContext.RequestServices
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient(targetService);
        client.DefaultRequestHeaders.Add("x-lane", lane);
        var url = $"{client.BaseAddress}/message";
        _logger.LogWarning(url);
        var response = await client.PostAsJsonAsync(url, messages);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var returnMessages = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
            return returnMessages!;
        }

        throw new Exception(await response.Content.ReadAsStringAsync());
    }
}
