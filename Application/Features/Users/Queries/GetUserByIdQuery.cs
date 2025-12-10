using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<User?>;