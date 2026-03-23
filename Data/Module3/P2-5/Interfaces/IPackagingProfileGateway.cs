using ProRental.Domain.Module3.P2_5;

namespace ProRental.Data.Module3.P2_5.Interfaces;

public interface IPackagingProfileGateway
{
    void Save(PackagingProfile profile);
    PackagingProfile FindByProfileId(string profileId);
    PackagingProfile FindByOrderId(string orderId);
    List<PackagingProfile> FindAll();
    List<dynamic> GetAllFootprintDetails();
}
