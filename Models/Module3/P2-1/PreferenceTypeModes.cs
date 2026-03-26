using ProRental.Domain.Enums;

namespace ProRental.Models.Module3.P2_1;

/// <summary>
/// Allowed routing modes for each customer-facing shipping preference.
/// by: ernest
/// </summary>
public static class PreferenceTypeModes
{
    public static IReadOnlyList<TransportMode> ResolveAllowedModes(PreferenceType preferenceType, bool isSameCountry)
    {
        return preferenceType switch
        {
            PreferenceType.FAST when isSameCountry => [TransportMode.TRAIN],
            PreferenceType.CHEAP when isSameCountry => [TransportMode.TRUCK],
            PreferenceType.GREEN when isSameCountry => [TransportMode.TRAIN],
            PreferenceType.FAST => [TransportMode.PLANE],
            PreferenceType.CHEAP => [TransportMode.SHIP],
            _ => [TransportMode.TRAIN, TransportMode.SHIP]
        };
    }

    public static string GetAllowedModesLabel(PreferenceType preferenceType, bool isSameCountry)
    {
        return string.Join(" + ", ResolveAllowedModes(preferenceType, isSameCountry));
    }
}
