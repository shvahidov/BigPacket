using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands;

public record CreateUserCommand(CreateUserDto Dto) : IRequest<User>;