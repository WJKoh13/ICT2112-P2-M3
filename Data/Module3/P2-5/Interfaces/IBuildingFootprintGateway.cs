using ProRental.Domain.Entities;

namespace ProRental.Interfaces.Module3;

public interface IBuildingFootprintGateway
{
    void Save(OrganizationalFootprintResult result);
    OrganizationalFootprintResult FindByBuildingCarbonFootprintId(string buildingCarbonFootprintId);
}
