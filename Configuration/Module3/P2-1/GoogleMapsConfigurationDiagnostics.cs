using Microsoft.Extensions.Configuration;

namespace ProRental.Configuration.Module3.P2_1;

internal static class GoogleMapsConfigurationDiagnostics
{
    internal const string ApiKeyConfigPath = "GoogleMaps:ApiKey";

    internal static string? GetMissingApiKeyWarning(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        return string.IsNullOrWhiteSpace(configuration[ApiKeyConfigPath])
            ? "Google Maps API key is not configured. Shipping route generation will fail until 'GoogleMaps__ApiKey' or the user-secrets key 'GoogleMaps:ApiKey' is set."
            : null;
    }
}
