using Microsoft.EntityFrameworkCore;
using ProRental.Data.Module3.P2_5.Interfaces;
using ProRental.Data.UnitOfWork;
using ProRental.Domain.Module3.P2_5;

namespace ProRental.Data.Module3.P2_5.Gateways;

public class PackagingMaterialGateway : IPackagingMaterialGateway
{
    private readonly AppDbContext _db;

    public PackagingMaterialGateway(AppDbContext db)
    {
        _db = db;
    }

    public PackagingMaterial FindById(string materialId)
    {
        if (!int.TryParse(materialId, out var id)) return null!;

        var row = _db.Packagingmaterials
            .Where(m => EF.Property<int>(m, "Materialid") == id)
            .Select(m => new
            {
                MaterialId = EF.Property<int>(m, "Materialid"),
                Name      = EF.Property<string>(m, "Name"),
                Type      = EF.Property<string?>(m, "Type"),
                Recyclable = EF.Property<bool>(m, "Recyclable"),
                Reusable   = EF.Property<bool>(m, "Reusable")
            })
            .FirstOrDefault();

        if (row == null) return null!;
        return MapToDomain(row.MaterialId, row.Name, row.Type, row.Recyclable, row.Reusable);
    }

    public List<PackagingMaterial> FindAll()
    {
        return _db.Packagingmaterials
            .Select(m => new
            {
                MaterialId = EF.Property<int>(m, "Materialid"),
                Name       = EF.Property<string>(m, "Name"),
                Type       = EF.Property<string?>(m, "Type"),
                Recyclable = EF.Property<bool>(m, "Recyclable"),
                Reusable   = EF.Property<bool>(m, "Reusable")
            })
            .AsEnumerable()
            .Select(r => MapToDomain(r.MaterialId, r.Name, r.Type, r.Recyclable, r.Reusable))
            .ToList();
    }

    public void Save(PackagingMaterial material)
    {
        _db.Database.ExecuteSqlRaw(
            "INSERT INTO packagingmaterial (name, type, recyclable, reusable) VALUES ({0}, {1}, {2}, {3})",
            material.GetName(),
            material.GetMaterialType(),
            material.GetRecyclable(),
            material.GetReusable()
        );
    }

    private static PackagingMaterial MapToDomain(int id, string name, string? type, bool recyclable, bool reusable)
    {
        var dm = new PackagingMaterial();
        dm.SetMaterialId(id.ToString());
        dm.SetName(name);
        dm.SetMaterialType(type ?? string.Empty);
        dm.SetRecyclable(recyclable);
        dm.SetReusable(reusable);
        return dm;
    }
}
