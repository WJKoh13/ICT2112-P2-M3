using Microsoft.EntityFrameworkCore;
using ProRental.Data.Module3.P2_5.Interfaces;
using ProRental.Data.UnitOfWork;
using ProRental.Domain.Module3.P2_5;

namespace ProRental.Data.Module3.P2_5.Gateways;

public class PackagingProfileGateway : IPackagingProfileGateway
{
    private readonly AppDbContext _db;

    public PackagingProfileGateway(AppDbContext db)
    {
        _db = db;
    }

    public void Save(PackagingProfile profile)
    {
        _db.Database.ExecuteSqlRaw(
            "INSERT INTO packagingprofile (orderid, volume, fragilitylevel) VALUES ({0}, {1}, {2})",
            profile.GetOrderId(),
            profile.GetVolume(),
            profile.GetFragilityLevel()
        );
    }

    public PackagingProfile FindByProfileId(string profileId)
    {
        if (!int.TryParse(profileId, out var id)) return null!;

        var row = _db.Packagingprofiles
            .Where(p => EF.Property<int>(p, "Profileid") == id)
            .Select(p => new
            {
                ProfileId      = EF.Property<int>(p, "Profileid"),
                OrderId        = EF.Property<int>(p, "Orderid"),
                Volume         = EF.Property<double>(p, "Volume"),
                FragilityLevel = EF.Property<string?>(p, "Fragilitylevel")
            })
            .FirstOrDefault();

        return row == null ? null! : MapToDomain(row.ProfileId, row.OrderId, row.Volume, row.FragilityLevel);
    }

    public PackagingProfile FindByOrderId(string orderId)
    {
        if (!int.TryParse(orderId, out var id)) return null!;

        var row = _db.Packagingprofiles
            .Where(p => EF.Property<int>(p, "Orderid") == id)
            .Select(p => new
            {
                ProfileId      = EF.Property<int>(p, "Profileid"),
                OrderId        = EF.Property<int>(p, "Orderid"),
                Volume         = EF.Property<double>(p, "Volume"),
                FragilityLevel = EF.Property<string?>(p, "Fragilitylevel")
            })
            .FirstOrDefault();

        return row == null ? null! : MapToDomain(row.ProfileId, row.OrderId, row.Volume, row.FragilityLevel);
    }

    public List<PackagingProfile> FindAll()
    {
        return _db.Packagingprofiles
            .Select(p => new
            {
                ProfileId      = EF.Property<int>(p, "Profileid"),
                OrderId        = EF.Property<int>(p, "Orderid"),
                Volume         = EF.Property<double>(p, "Volume"),
                FragilityLevel = EF.Property<string?>(p, "Fragilitylevel")
            })
            .AsEnumerable()
            .Select(r => MapToDomain(r.ProfileId, r.OrderId, r.Volume, r.FragilityLevel))
            .ToList();
    }

    public List<dynamic> GetAllFootprintDetails()
    {
        var rows = _db.Packagingprofiles
            .Join(_db.Packagingconfigurations,
                profile => EF.Property<int>(profile, "Profileid"),
                config  => EF.Property<int>(config,  "Profileid"),
                (profile, config) => new { profile, config })
            .Join(_db.Packagingconfigmaterials,
                pc  => EF.Property<int>(pc.config, "Configurationid"),
                pcm => EF.Property<int>(pcm,       "Configurationid"),
                (pc, pcm) => new { pc.profile, pc.config, pcm })
            .Join(_db.Packagingmaterials,
                pcmRow   => EF.Property<int>(pcmRow.pcm, "Materialid"),
                material => EF.Property<int>(material,   "Materialid"),
                (pcmRow, material) => new
                {
                    OrderId         = EF.Property<int>(pcmRow.profile,    "Orderid"),
                    ProfileId       = EF.Property<int>(pcmRow.profile,    "Profileid"),
                    FragilityLevel  = EF.Property<string?>(pcmRow.profile,"Fragilitylevel"),
                    Volume          = EF.Property<double>(pcmRow.profile, "Volume"),
                    Category        = EF.Property<string?>(pcmRow.pcm,    "Category"),
                    Quantity        = EF.Property<int>(pcmRow.pcm,        "Quantity"),
                    MaterialName    = EF.Property<string>(material,       "Name"),
                    MaterialType    = EF.Property<string?>(material,      "Type"),
                    Recyclable      = EF.Property<bool>(material,         "Recyclable"),
                    Reusable        = EF.Property<bool>(material,         "Reusable")
                })
            .AsEnumerable()
            .Cast<dynamic>()
            .ToList();

        return rows;
    }

    private static PackagingProfile MapToDomain(int profileId, int orderId, double volume, string? fragilityLevel)
    {
        var dm = new PackagingProfile();
        dm.SetProfileId(profileId.ToString());
        dm.SetOrderId(orderId.ToString());
        dm.SetVolume((float)volume);
        dm.SetFragilityLevel(fragilityLevel ?? "low");
        return dm;
    }
}
