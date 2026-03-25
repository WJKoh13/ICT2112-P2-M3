namespace ProRental.Domain.Entities;

public partial class OrganizationalFootprint
{
    private string _organizationalFootprintId = string.Empty;
    private string _organizationalId = string.Empty;
    private string _organizationName = string.Empty;
    private float _volume;
    private float _toxicPercentage;

    private string OrganizationalFootprintId
    {
        get => _organizationalFootprintId;
        set => _organizationalFootprintId = value;
    }

    private string OrganizationalId
    {
        get => _organizationalId;
        set => _organizationalId = value;
    }

    private string OrganizationName
    {
        get => _organizationName;
        set => _organizationName = value;
    }

    private float Volume
    {
        get => _volume;
        set => _volume = value;
    }

    private float ToxicPercentage
    {
        get => _toxicPercentage;
        set => _toxicPercentage = value;
    }

    private string GetOrganizationalFootprintId() => _organizationalFootprintId;
    private void SetOrganizationalFootprintId(string organizationalFootprintId) => _organizationalFootprintId = organizationalFootprintId;
    private string GetOrganizationalId() => _organizationalId;
    private void SetOrganizationalId(string organizationalId) => _organizationalId = organizationalId;
    private string GetOrganizationName() => _organizationName;
    private void SetOrganizationName(string organizationName) => _organizationName = organizationName;
    private float GetVolume() => _volume;
    private void SetVolume(float volume) => _volume = volume;
    private float GetToxicPercentage() => _toxicPercentage;
    private void SetToxicPercentage(float percentage) => _toxicPercentage = percentage;
}
