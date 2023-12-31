using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    public Task<IEnumerable<string>> Post([FromBody] IEnumerable<string> messages)
    {
        messages = messages.Append($"ServiceB {Environment.GetEnvironmentVariable("HOSTNAME")}");
        return Task.FromResult(messages);
    }
}
