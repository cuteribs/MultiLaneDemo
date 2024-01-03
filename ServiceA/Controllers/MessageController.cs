using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MultiLane.ServiceA.Controllers;

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
        messages = messages.Append($"ServiceA {Path.GetFileName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))}");
        var response = await client.PostAsJsonAsync("message", messages);
        return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
    }
}
