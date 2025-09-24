using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacketsController(IPacketRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var packet = await repo.GetByIdAsync(id);
        return packet is null ? NotFound() : Ok(packet);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePacketDto dto)
    {
        // Генерируем новый пакет
        var packet = new Packet
        {
            PacketId = Guid.NewGuid(),
            PacketTypeId = dto.PacketTypeId,
            PacketStatusId = dto.PacketStatusId,
            PacketKey = string.IsNullOrWhiteSpace(dto.PacketKey)
                ? Guid.NewGuid().ToString("N") // если ключ пустой, генерируем уникальный
                : dto.PacketKey,
            AddDate = DateTime.UtcNow,
            EndDate = dto.EndDate,
            PacketType = null,
            PacketStatus = null
        };

        // Сохраняем пакет
        await repo.AddAsync(packet);

        // Возвращаем Created с ссылкой на новый пакет
        return CreatedAtAction(nameof(GetById), new { id = packet.PacketId }, packet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CreatePacketDto dto)
    {
        var packet = await repo.GetByIdAsync(id);
        if (packet is null)
        {
            return NotFound();
        }

        packet.PacketTypeId = dto.PacketTypeId;
        packet.PacketStatusId = dto.PacketStatusId;
        packet.PacketKey = dto.PacketKey;
        packet.EndDate = dto.EndDate;

        await repo.UpdateAsync(packet);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}