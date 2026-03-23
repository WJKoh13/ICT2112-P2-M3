using ProRental.Domain.Module3.P2_5;

namespace ProRental.Data.Module3.P2_5.Interfaces;

public interface IPackagingMaterialGateway
{
    PackagingMaterial FindById(string materialId);
    List<PackagingMaterial> FindAll();
    void Save(PackagingMaterial material);
}
