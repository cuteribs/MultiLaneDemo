using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    public async Task<IEnumerable<string>?> Post([FromBody] IEnumerable<string> messages)
    {
        var client = this.HttpContext.RequestServices
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient("ServiceB");
        client.DefaultRequestHeaders.Add("x-lane", this.HttpContext.Request.Headers["x-lane"].ToString());
        messages = messages.Append($"ServiceA {Environment.GetEnvironmentVariable("HOSTNAME")}");
        var response = await client.PostAsJsonAsync("message", messages);
        return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
    }
}
