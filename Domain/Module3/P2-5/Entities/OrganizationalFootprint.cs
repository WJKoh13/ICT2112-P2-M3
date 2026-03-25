using System;

namespace ProRental.Domain.Entities;

public partial class OrganizationalFootprint
{
    public static OrganizationalFootprint Create(
        string organizationalId,
        string organizationalName,
        float volume,
        float toxicPercentage)
    {
        var footprint = new OrganizationalFootprint();
        footprint._organizationalFootprintId = Guid.NewGuid().ToString();
        footprint._organizationalId = organizationalId;
        footprint._organizationName = organizationalName;
        footprint._volume = volume;
        footprint._toxicPercentage = toxicPercentage;
        return footprint;
    }

    public string ReadOrganizationalFootprintId() => _organizationalFootprintId;
    public string ReadOrganizationalId() => _organizationalId;
    public string ReadOrganizationName() => _organizationName;
    public float ReadVolume() => _volume;
    public float ReadToxicPercentage() => _toxicPercentage;
}
