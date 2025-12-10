using Application.DTOs;
using MediatR;

namespace Application.Features.Auth.Commands;

public record LoginCommand(LoginDto Dto) : IRequest<LoginResult>;
