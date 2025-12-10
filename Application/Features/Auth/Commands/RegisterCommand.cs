using Application.DTOs;
using MediatR;

namespace Application.Features.Auth.Commands;

public record RegisterCommand(RegisterDto Dto) : IRequest<string>;