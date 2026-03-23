using ProRental.Domain.Module3.P2_5;

namespace ProRental.Interfaces.Module3.P2_5;

public interface IPackagingProfilerControl
{
    PackagingProfile CreatePackagingProfile(string orderId, float volume, string fragilityLevel);
    PackagingConfiguration CreatePackagingConfiguration(PackagingProfile profile);
}
