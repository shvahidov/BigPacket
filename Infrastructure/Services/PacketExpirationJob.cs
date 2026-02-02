using Application.Interfaces;

namespace Infrastructure.Services;

public class PacketExpirationJob
{
    private readonly IPacketRepository _repo;

    public PacketExpirationJob(IPacketRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute()
    {
        var packets = await _repo.GetActivePacketsAsync();

        foreach (var packet in packets)
        {
            packet.CheckExpiration(DateTime.UtcNow);
        }

        await _repo.SaveChangesAsync();
    }
}
