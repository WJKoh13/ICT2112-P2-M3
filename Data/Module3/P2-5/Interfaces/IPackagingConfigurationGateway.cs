using ProRental.Domain.Module3.P2_5;

namespace ProRental.Data.Module3.P2_5.Interfaces;

public interface IPackagingConfigurationGateway
{
    void SaveConfiguration(PackagingConfiguration config);
    void SaveMaterials(string configurationId, List<PackagingMaterial> items, string category, int quantity);
    PackagingConfiguration FindByConfigurationId(string configurationId);
    List<PackagingConfiguration> FindByProfileId(string profileId);
}
