namespace ProRental.Domain.Module3.P2_5;

public class PackagingProfile
{
    private string _profileId = null!;
    private string _orderId = null!;
    private float _volume;
    private string _fragilityLevel = null!;

    public string GetProfileId() => _profileId;
    public void SetProfileId(string profileId) => _profileId = profileId;

    public int GetOrderId() => int.TryParse(_orderId, out var id) ? id : 0;
    public void SetOrderId(string orderId) => _orderId = orderId;

    public float GetVolume() => _volume;
    public void SetVolume(float volume) => _volume = volume;

    public string GetFragilityLevel() => _fragilityLevel;
    public void SetFragilityLevel(string level) => _fragilityLevel = level;

    public PackagingMaterial DeterminePrimaryPackaging()
    {
        var material = new PackagingMaterial();
        var level = _fragilityLevel?.ToLower() ?? "low";

        if (level == "high")
        {
            material.SetMaterialId("primary-foam");
            material.SetName("Foam Padding");
            material.SetMaterialType("Foam");
            material.SetRecyclable(false);
            material.SetReusable(false);
        }
        else if (level == "medium")
        {
            material.SetMaterialId("primary-bubble");
            material.SetName("Bubble Wrap");
            material.SetMaterialType("Plastic");
            material.SetRecyclable(false);
            material.SetReusable(true);
        }
        else
        {
            material.SetMaterialId("primary-paper");
            material.SetName("Kraft Paper");
            material.SetMaterialType("Paper");
            material.SetRecyclable(true);
            material.SetReusable(false);
        }

        return material;
    }
    
    public List<PackagingMaterial> DetermineSecondaryPackaging()
    {
        var materials = new List<PackagingMaterial>();

        var box = new PackagingMaterial();
        box.SetMaterialId("secondary-box");
        box.SetName("Cardboard Box");
        box.SetMaterialType("Cardboard");
        box.SetRecyclable(true);
        box.SetReusable(false);
        materials.Add(box);

        if (_volume > 0.5f)
        {
            var filler = new PackagingMaterial();
            filler.SetMaterialId("secondary-filler");
            filler.SetName("Paper Filler");
            filler.SetMaterialType("Paper");
            filler.SetRecyclable(true);
            filler.SetReusable(false);
            materials.Add(filler);
        }

        if (_volume > 2.0f)
        {
            var wrap = new PackagingMaterial();
            wrap.SetMaterialId("secondary-stretch");
            wrap.SetName("Stretch Wrap");
            wrap.SetMaterialType("Plastic");
            wrap.SetRecyclable(false);
            wrap.SetReusable(false);
            materials.Add(wrap);
        }

        return materials;
    }
}
