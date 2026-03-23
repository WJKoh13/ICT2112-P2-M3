using ProRental.Data.Module3.P2_5.Interfaces;
using ProRental.Domain.Module3.P2_5;
using ProRental.Interfaces.Module3.P2_5;

namespace ProRental.Domain.Module3.P2_5.Controls;

public class PackagingProfilerControl : IPackagingProfilerControl
{
    private readonly IPackagingProfileGateway _profileGateway;
    private readonly IPackagingConfigurationGateway _configGateway;
    private readonly IPackagingMaterialGateway _materialGateway;

    public PackagingProfilerControl(
        IPackagingProfileGateway profileGateway,
        IPackagingConfigurationGateway configGateway,
        IPackagingMaterialGateway materialGateway)
    {
        _profileGateway  = profileGateway;
        _configGateway   = configGateway;
        _materialGateway = materialGateway;
    }
    
    public PackagingProfile CreatePackagingProfile(string orderId, float volume, string fragilityLevel)
    {
        var profile = new PackagingProfile();
        profile.SetOrderId(orderId);
        profile.SetVolume(volume);
        profile.SetFragilityLevel(fragilityLevel);

        _profileGateway.Save(profile);

        var saved = _profileGateway.FindByOrderId(orderId);
        return saved ?? profile;
    }

    public PackagingConfiguration CreatePackagingConfiguration(PackagingProfile profile)
    {
        var config = new PackagingConfiguration();
        config.SetProfileId(profile.GetProfileId());

        // Determine the required material types from profile business rules
        var primaryMaterial    = profile.DeterminePrimaryPackaging();
        var secondaryMaterials = profile.DetermineSecondaryPackaging();

        config.AddMaterial("primary", primaryMaterial, 1);
        foreach (var material in secondaryMaterials)
            config.AddMaterial("secondary", material, 1);

        // Persist the configuration shell
        _configGateway.SaveConfiguration(config);

        // Retrieve the persisted configuration to get the DB-assigned configurationid
        var saved = _configGateway.FindByProfileId(profile.GetProfileId()).FirstOrDefault();
        if (saved == null) return config;

        // Load all real DB materials so we can match by type and get valid integer IDs
        var allMaterials = _materialGateway.FindAll();

        // Resolve primary material — find first DB material matching the required type
        var resolvedPrimary = allMaterials
            .FirstOrDefault(m => m.GetMaterialType().Equals(
                primaryMaterial.GetMaterialType(), StringComparison.OrdinalIgnoreCase))
            ?? allMaterials.FirstOrDefault();

        if (resolvedPrimary != null)
            _configGateway.SaveMaterials(
                saved.GetConfigurationId(),
                new List<PackagingMaterial> { resolvedPrimary },
                "primary", 1);

        // Resolve each secondary material — match by type, fall back to first available
        foreach (var secondary in secondaryMaterials)
        {
            var resolved = allMaterials
                .FirstOrDefault(m => m.GetMaterialType().Equals(
                    secondary.GetMaterialType(), StringComparison.OrdinalIgnoreCase))
                ?? allMaterials.FirstOrDefault();

            if (resolved != null)
                _configGateway.SaveMaterials(
                    saved.GetConfigurationId(),
                    new List<PackagingMaterial> { resolved },
                    "secondary", 1);
        }

        return saved;
    }
}
