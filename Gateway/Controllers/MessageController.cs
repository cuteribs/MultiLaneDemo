using Microsoft.AspNetCore.Mvc;

namespace MultiLane.Gateway.Controllers;

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
        var lane = this.HttpContext.Request.Headers["x-lane"].ToString();
        client.DefaultRequestHeaders.Add("x-lane", lane);
        var messages = new[] { message, $"Gateway from '{lane}'" };
        var response = await client.PostAsJsonAsync("message", messages);
        return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
    }
}
