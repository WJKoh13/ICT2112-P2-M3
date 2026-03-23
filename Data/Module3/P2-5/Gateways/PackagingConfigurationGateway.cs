using Microsoft.EntityFrameworkCore;
using ProRental.Data.Module3.P2_5.Interfaces;
using ProRental.Data.UnitOfWork;
using ProRental.Domain.Module3.P2_5;

namespace ProRental.Data.Module3.P2_5.Gateways;

public class PackagingConfigurationGateway : IPackagingConfigurationGateway
{
    private readonly AppDbContext _db;

    public PackagingConfigurationGateway(AppDbContext db)
    {
        _db = db;
    }

    public void SaveConfiguration(PackagingConfiguration config)
    {
        if (!int.TryParse(config.GetProfileId(), out var profileId)) return;

        _db.Database.ExecuteSqlRaw(
            "INSERT INTO packagingconfiguration (profileid) VALUES ({0})",
            profileId
        );
    }

    public void SaveMaterials(string configurationId, List<PackagingMaterial> items, string category, int quantity)
    {
        if (!int.TryParse(configurationId, out var configId)) return;

        foreach (var material in items)
        {
            if (!int.TryParse(material.GetMaterialId(), out var materialId)) continue;

            _db.Database.ExecuteSqlRaw(
                "INSERT INTO packagingconfigmaterials (configurationid, materialid, category, quantity) VALUES ({0}, {1}, {2}, {3})",
                configId, materialId, category, quantity
            );
        }
    }

    public PackagingConfiguration FindByConfigurationId(string configurationId)
    {
        if (!int.TryParse(configurationId, out var id)) return null!;

        var row = _db.Packagingconfigurations
            .Where(c => EF.Property<int>(c, "Configurationid") == id)
            .Select(c => new
            {
                ConfigId  = EF.Property<int>(c, "Configurationid"),
                ProfileId = EF.Property<int>(c, "Profileid"),
                Materials = c.Packagingconfigmaterials.Select(cm => new
                {
                    Category   = EF.Property<string?>(cm, "Category"),
                    Quantity   = EF.Property<int>(cm, "Quantity"),
                    MaterialId = EF.Property<int>(cm.Material, "Materialid"),
                    Name       = EF.Property<string>(cm.Material, "Name"),
                    Type       = EF.Property<string?>(cm.Material, "Type"),
                    Recyclable = EF.Property<bool>(cm.Material, "Recyclable"),
                    Reusable   = EF.Property<bool>(cm.Material, "Reusable")
                }).ToList()
            })
            .FirstOrDefault();

        if (row == null) return null!;
        return BuildConfiguration(row.ConfigId, row.ProfileId, row.Materials.Select(m => (m.Category, m.Quantity, m.MaterialId, m.Name, m.Type, m.Recyclable, m.Reusable)));
    }

    public List<PackagingConfiguration> FindByProfileId(string profileId)
    {
        if (!int.TryParse(profileId, out var id)) return new List<PackagingConfiguration>();

        return _db.Packagingconfigurations
            .Where(c => EF.Property<int>(c, "Profileid") == id)
            .Select(c => new
            {
                ConfigId  = EF.Property<int>(c, "Configurationid"),
                ProfileId = EF.Property<int>(c, "Profileid"),
                Materials = c.Packagingconfigmaterials.Select(cm => new
                {
                    Category   = EF.Property<string?>(cm, "Category"),
                    Quantity   = EF.Property<int>(cm, "Quantity"),
                    MaterialId = EF.Property<int>(cm.Material, "Materialid"),
                    Name       = EF.Property<string>(cm.Material, "Name"),
                    Type       = EF.Property<string?>(cm.Material, "Type"),
                    Recyclable = EF.Property<bool>(cm.Material, "Recyclable"),
                    Reusable   = EF.Property<bool>(cm.Material, "Reusable")
                }).ToList()
            })
            .AsEnumerable()
            .Select(row => BuildConfiguration(row.ConfigId, row.ProfileId, row.Materials.Select(m => (m.Category, m.Quantity, m.MaterialId, m.Name, m.Type, m.Recyclable, m.Reusable))))
            .ToList();
    }

    private static PackagingConfiguration BuildConfiguration(
        int configId,
        int profileId,
        IEnumerable<(string? Category, int Quantity, int MaterialId, string Name, string? Type, bool Recyclable, bool Reusable)> materials)
    {
        var config = new PackagingConfiguration();
        config.SetConfigurationId(configId.ToString());
        config.SetProfileId(profileId.ToString());

        foreach (var m in materials)
        {
            var material = new PackagingMaterial();
            material.SetMaterialId(m.MaterialId.ToString());
            material.SetName(m.Name);
            material.SetMaterialType(m.Type ?? string.Empty);
            material.SetRecyclable(m.Recyclable);
            material.SetReusable(m.Reusable);

            config.AddMaterial(m.Category ?? "secondary", material, m.Quantity);
        }

        return config;
    }
}
