using System.Net.Http.Json;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Providers.Models;
using Providers.Services;

namespace Providers.Controllers;

[ApiController]
[Route("provider")]
public class ProviderController(IProviderService providerService): ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Provider>> Get(string id){
        return await providerService.GetProvider(id);
    }
}