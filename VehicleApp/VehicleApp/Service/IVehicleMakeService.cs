using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IVehicleMakeService
    {
         Task<List<VehicleMake>> GetVehicleByID(int id);
         Task<List<VehicleMake>> UpdateVehicle(int id,VehicleMake vehicle);
         Task<List<VehicleMake>> GetVehiclesAsync(bool ascOrDesc);
         Task<List<VehicleMake>> FilterVehicles(string name);
         Task<List<VehicleMake>> SortVehiclesASC_DESC(bool ascOrDesc);
         Task<bool> MakeVehicle(int id, string name, string abbr);
         Task<bool> DeleteVehicle(string name);
    }
}
