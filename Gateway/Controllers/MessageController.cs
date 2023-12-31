using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    public async Task<IEnumerable<string>?> Post([FromBody] string message)
    {
        var client = this.HttpContext.RequestServices
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient("ServiceA");
        client.DefaultRequestHeaders.Add("x-lane", this.HttpContext.Request.Headers["x-lane"].ToString());
        var messages = new[] { message, $"Gateway {Environment.GetEnvironmentVariable("HOSTNAME")}" };
        var response = await client.PostAsJsonAsync("message", messages);
        return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
    }
}
