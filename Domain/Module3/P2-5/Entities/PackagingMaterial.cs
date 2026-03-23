namespace ProRental.Domain.Module3.P2_5;

public class PackagingMaterial
{
    private string _materialId = null!;
    private string _name = null!;
    private string _type = null!;
    private bool _recyclable;
    private bool _reusable;

    public string GetMaterialId() => _materialId;
    public void SetMaterialId(string materialId) => _materialId = materialId;

    public string GetName() => _name;
    public void SetName(string name) => _name = name;

    public string GetMaterialType() => _type;
    public void SetMaterialType(string type) => _type = type;

    public bool GetRecyclable() => _recyclable;
    public void SetRecyclable(bool recyclable) => _recyclable = recyclable;

    public bool GetReusable() => _reusable;
    public void SetReusable(bool reusable) => _reusable = reusable;
}
