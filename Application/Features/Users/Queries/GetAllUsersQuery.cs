using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries;

public record GetAllUsersQuery : IRequest<List<User>>;