using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController(UserService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(User user) =>
        Ok(await service.CreateAsync(user));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}
