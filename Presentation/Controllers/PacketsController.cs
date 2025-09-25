using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacketsController(PacketService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var packet = await service.GetByIdAsync(id);
        return packet is null ? NotFound() : Ok(packet);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePacketDto dto)
    {
        var packet = await service.CreatePacketAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = packet.PacketId }, packet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CreatePacketDto dto)
    {
        var packet = await service.UpdatePacketAsync(id, dto);
        return packet is null ? NotFound() : NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}