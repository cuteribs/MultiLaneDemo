using Microsoft.AspNetCore.Mvc;

namespace MultiLane.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    public async Task<IEnumerable<string>?> Post([FromBody] IEnumerable<string> messages)
    {
        messages = messages.Append($"Service {Program.ServiceName}");

        var client = this.HttpContext.RequestServices
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient("default");

        if (Program.TargetServiceName == "localhost") return messages;

        var lane = this.HttpContext.Request.Headers["x-lane"].ToString();
        client.DefaultRequestHeaders.Add("x-lane", lane);
        var response = await client.PostAsJsonAsync("message", messages);
        return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
    }
}
