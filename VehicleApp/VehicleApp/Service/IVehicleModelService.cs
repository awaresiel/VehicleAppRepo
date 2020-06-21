using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;

namespace Service
{
    public interface IVehicleModelService
    {
        Task<List<VehicleModel>> getVehicleModelListAsync(string name, bool ascOrDesc );
        Task<bool> CreateVehicleModel(string vehicleMakeName, int oldId,int id, int makeID, string name, string abbreviation);
      
        Task<List<VehicleModel>> SortVehicleModelsASC_DESC(string nameOfModel, bool ascOrDesc);
        Task<List<VehicleModel>> GetVehicleModel(string name, int id);
        Task<bool> DeleteVehicleModel(string name, int id,bool deleteAllVehicleMake);

        Task<bool> UpdateDictionaryKey(string oldKey, string newKey);
    }
}
