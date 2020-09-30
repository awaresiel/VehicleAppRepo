
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;
using System.Linq;
using System.Diagnostics;
using VehicleApp.Repository;
using Autofac;
using System.IO;
using System;
using VehicleApp;

namespace Service
{
    public class VehicleModelService : IVehicleModelService
    {
       
       private Irepository<VehicleModelEntity> database;
        public VehicleModelService(Irepository<VehicleModelEntity> d)
        {
            database = d;  
        }

        private VehicleModel MapToVehicleModel(VehicleModelEntity vehicle)
        {
            return App.Mapper.Map<VehicleModelEntity, VehicleModel>(vehicle);
        }
        private List<VehicleModel> MapToVehicleModelList(List<VehicleModelEntity> vehicle)
        {
            return App.Mapper.Map<List<VehicleModelEntity>, List<VehicleModel>>(vehicle);
        }
        async public Task<List<VehicleModel>> getVehicleModelListAsync(string name, bool order)
        {
            var list = await Task.FromResult(await SortVehicleModelsASC_DESC(name, order));
            return list;
        }
        public async Task<bool> UpdateModelKeys(string oldName, string newName,int makeId)
        {
           var vehicles = await database.GetVehiclesAsync(false, oldName);
            foreach (var v in vehicles)
            {
                v.MakeName = newName;
                v.MakeId = makeId;
            }
            var result= await database.UpdateVehiclesAsync(vehicles);
           
            return result > 0;
        }
        async public Task<bool> CreateVehicleModel(string vehicleMakeName,int databseID, int makeID, int id, string modelName, string abbreviation)
        {
            VehicleModelEntity vm = new VehicleModelEntity(id, makeID, vehicleMakeName, modelName, abbreviation);
            vm.dataBaseId = databseID;
            var result= await database.SaveVehicleAsync(vm);
            return result > 0;
           
        }
        public async Task<List<VehicleModel>> SortVehicleModelsASC_DESC(string nameOfModel, bool ascOrDesc)
        {
            var list = await database.GetVehiclesAsync(ascOrDesc, nameOfModel);
            return MapToVehicleModelList(list);
           
        }
        public async Task<VehicleModel> GetVehicleModel(int databaseID)
        {
          var vehicle=  await database.GetVehicleAsync(databaseID);
            return MapToVehicleModel(vehicle);
         
        }
        
        public async Task<bool> DeleteVehicleModel(string BrandName,int ModelDatabaseID, bool deleteEntireVehicleMake)
        {
            if (deleteEntireVehicleMake)
            {
                var result = await database.DeleteVehiclesAsync(BrandName);
                return result>0;
            }
            else
            {
                var result = await database.DeleteVehicleAsync(ModelDatabaseID);
                return result > 0;
            }

        
        }
    }
}
