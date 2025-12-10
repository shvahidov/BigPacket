using Application.DTOs;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.CustomAttributes;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Role("Admin")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<List<User>> GetAll() => await mediator.Send(new GetAllUsersQuery());

    [HttpGet("{id}")]
    public async Task<User?> GetById(Guid id) => await mediator.Send(new GetUserByIdQuery(id));

    [HttpPost]
    public async Task<User> Create(CreateUserDto dto) => await mediator.Send(new CreateUserCommand(dto));

    [HttpDelete("{id}")]
    public async Task<bool> Delete(Guid id) => await mediator.Send(new DeleteUserCommand(id));
}
