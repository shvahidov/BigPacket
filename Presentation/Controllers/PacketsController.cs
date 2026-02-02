using Application.DTOs;
using Application.Features.Packets.Commands;
using Application.Features.Packets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.CustomAttributes;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacketsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Role("Admin")]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllPacketsQuery()));

    [HttpGet("{id}")]
    [Role("Admin")]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await mediator.Send(new GetPacketByIdQuery(id)));

    [HttpPost]
    [Roles("User", "Admin")]
    public async Task<IActionResult> Create(CreatePacketCommand command)
        => Ok(await mediator.Send(command));

    [HttpPost("{id}/activate")]
    [Role("Admin")]
    public async Task<IActionResult> Activate(Guid id)
    {
        await mediator.Send(new ActivatePacketCommand(id));
        return Ok("Packet activated");
    }

    [HttpPost("{id}/disable")]
    [Role("Admin")]
    public async Task<IActionResult> Disable(Guid id)
    {
        await mediator.Send(new DisablePacketCommand(id));
        return Ok("Packet disabled");
    }

    [HttpPatch("{id}/end-date")]
    [Role("Admin")]
    public async Task<IActionResult> ChangeEndDate(Guid id, [FromBody] ChangePacketEndDateDto dto)
    {
        await mediator.Send(new ChangePacketEndDateCommand
        {
            Id = id,
            NewEndDate = dto.NewEndDate
        });

        return Ok("End date updated");
    }

    [HttpPatch("{id}/type")]
    [Role("Admin")]
    public async Task<IActionResult> ChangeType(Guid id, [FromBody] ChangePacketTypeDto dto)
    {
        await mediator.Send(new ChangePacketTypeCommand
        {
            Id = id,
            PacketTypeId = dto.PacketTypeId
        });

        return Ok("Packet type updated");
    }
}