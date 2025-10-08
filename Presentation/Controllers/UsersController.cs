using Application.DTOs;
using Application.Services;
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
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var createdUser = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = createdUser.UserId }, createdUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}
