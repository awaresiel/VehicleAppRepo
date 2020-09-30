using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;

namespace Service
{
    public interface IVehicleModelService
    {
        Task<List<VehicleModel>> getVehicleModelListAsync(string name, bool ascOrDesc );
        Task<bool> CreateVehicleModel(string vehicleMakeName,int databseID,int id, int makeID, string name, string abbreviation);
        Task<List<VehicleModel>> SortVehicleModelsASC_DESC(string nameOfModel, bool ascOrDesc);
        Task<VehicleModel> GetVehicleModel(int databaseID);
        Task<bool> DeleteVehicleModel(string vehicleName, int ModelDatabaseID, bool deleteAllVehicleMake);
        Task<bool> UpdateModelKeys(string oldKey, string newKey, int makeId);
    }
}
