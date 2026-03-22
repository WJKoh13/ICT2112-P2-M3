using ProRental.Domain.Entities;

namespace ProRental.Interfaces.Module3;

public interface IStaffFootprintGateway
{
    void Save(OrganizationalFootprintResult result);
    OrganizationalFootprintResult FindByStaffCarbonFootprintId(string staffCarbonFootprintId);
}
