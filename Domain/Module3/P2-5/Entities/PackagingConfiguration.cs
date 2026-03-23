namespace ProRental.Domain.Module3.P2_5;

public class PackagingConfiguration
{
    private string _configurationId = null!;
    private string _profileId = null!;
    private Dictionary<PackagingMaterial, int> _primaryMaterials = new();
    private Dictionary<PackagingMaterial, int> _secondaryMaterials = new();

    public string GetConfigurationId() => _configurationId;
    public void SetConfigurationId(string configurationId) => _configurationId = configurationId;

    public string GetProfileId() => _profileId;
    public void SetProfileId(string profileId) => _profileId = profileId;

    public Dictionary<PackagingMaterial, int> GetPrimaryMaterials() => _primaryMaterials;
    public Dictionary<PackagingMaterial, int> GetSecondaryMaterials() => _secondaryMaterials;
    
    public void AddMaterial(string category, PackagingMaterial material, int quantity)
    {
        if (category.Equals("primary", StringComparison.OrdinalIgnoreCase))
            _primaryMaterials[material] = quantity;
        else
            _secondaryMaterials[material] = quantity;
    }
}
