using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        await _accountService.TransferAsync(request.FromAccountId, request.ToAccountId, request.Amount);
        return Ok("Transfer successful");
    }
}

public class TransferRequest
{
    public TransferRequest(int fromAccountId, int toAccountId, decimal amount)
    {
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }

    public int FromAccountId { get; }

    public int ToAccountId { get; }

    public decimal Amount { get; set; }
}