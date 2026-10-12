using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obt.Api.Master.Dto;
using Obt.Api.Security;

namespace Obt.Api.Master;

[ApiController]
[Authorize]
[Route("api/master")]
public class MasterController : ControllerBase
{
    private readonly MasterService _masterService;

    public MasterController(MasterService masterService)
    {
        _masterService = masterService;
    }

    [HttpGet("customers")]
    public IReadOnlyList<CustomerDto> Customers() => _masterService.ListCustomers();

    [HttpGet("contracts")]
    public IReadOnlyList<ContractDto> Contracts()
    {
        var user = JwtService.FromPrincipal(User);
        return _masterService.ListContracts(user);
    }
}
