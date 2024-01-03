using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MultiLane.ServiceB.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    public Task<IEnumerable<string>> Post([FromBody] IEnumerable<string> messages)
    {
        messages = messages.Append($"ServiceB {Path.GetFileName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))}");
        return Task.FromResult(messages);
    }
}
