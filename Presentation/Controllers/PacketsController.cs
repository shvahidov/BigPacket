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

    [HttpPut("{id}")]
    [Role("Admin")]
    public async Task<IActionResult> Update(Guid id, UpdatePacketCommand command)
        => Ok(await mediator.Send(command with { Id = id }));

    [HttpDelete("{id}")]
    [Role("Admin")]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await mediator.Send(new DeletePacketCommand(id)));
}