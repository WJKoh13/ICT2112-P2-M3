namespace ProRental.Domain.Entities;

public class OrganizationFootprint
{
    private string _organizationalFootprintId;
    private string _organizationalId;
    private string _organizationalName;
    private float _volume;
    private float _toxicPercentage;

    public float GetVolume()
    {
        return _volume;
    }

    private void SetVolume(float volume)
    {
        _volume = volume;
    }

    public float GetToxicPercentage()
    {
        return _toxicPercentage;
    }

    private void SetToxicPercentage(float percentage)
    {
        _toxicPercentage = percentage;
    }
}
