using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Service
{
    public interface IVehicleMakeService
    {
         Task<VehicleMake> GetVehicleByID(int id);
         Task<int> UpdateVehicle(VehicleMake vehicle);
         Task<List<VehicleMake>> GetVehiclesAsync(bool ascOrDesc);
         Task<List<VehicleMake>> SortVehiclesASC_DESC(bool ascOrDesc);
         Task<bool> InsertVehicle(int id, string name, string abbr);
         Task<bool> DeleteVehicle(VehicleMake v);
        public Task<bool> DeleteVehicleAsyncWithSameName(string make);
    }
}
