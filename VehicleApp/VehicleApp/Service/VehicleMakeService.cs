using Autofac;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp;
using VehicleApp.Repository;

namespace Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        public List<VehicleMake> vehiclesList { get; private set; }

       private Irepository<VehicleMakeEntity> database;
        public VehicleMakeService(Irepository<VehicleMakeEntity> d)
        {
            database = d;
        }
        private VehicleMake MapToVehicleMake(VehicleMakeEntity vehicle)
        {
           return App.Mapper.Map<VehicleMakeEntity, VehicleMake>(vehicle);
        }
        private List<VehicleMake> MapToVehicleMakeList(List<VehicleMakeEntity> vehicle)
        {
            return App.Mapper.Map<List<VehicleMakeEntity>,List< VehicleMake>>(vehicle);
        }
        private VehicleMakeEntity MapToVehicleMakeEntity(VehicleMake vehicle)
        {
            return App.Mapper.Map<VehicleMake, VehicleMakeEntity>(vehicle);
        }
       

        public async Task<VehicleMake> GetVehicleByID(int id)
        {   
           var vehicle= await database.GetVehicleAsync(id);
            return MapToVehicleMake(vehicle);
         
        }
        public async Task<int> UpdateVehicle(VehicleMake vehicleMake)
        {
            return await database.SaveVehicleAsync(MapToVehicleMakeEntity(vehicleMake));
        }
        public async Task<List<VehicleMake>> GetVehiclesAsync(bool ascOrDesc)
        {
            var list = await SortVehiclesASC_DESC(ascOrDesc);
            return list;
        }
        public async Task<bool> DeleteVehicle(VehicleMake make)
        {
            int number = await database.DeleteVehicleAsync(make.dataBaseId);
            return number > 0;
        }
        public async Task<bool> DeleteVehicleAsyncWithSameName(string make)
        {
            int number = await database.DeleteVehicleAsyncWithSameName(make);
            return number > 0;
        }
        async public Task<List<VehicleMake>> SortVehiclesASC_DESC(bool ascOrDesc)
        {
            var list = await database.GetVehiclesAsync(ascOrDesc, "");
            var sorted=  MapToVehicleMakeList(list);
            return sorted;
        }
        async public Task<bool> InsertVehicle(int id, string name, string abbr)
        {
            var vehicle = new VehicleMake(id, name, abbr);
            var entity = MapToVehicleMakeEntity(vehicle);
            var result = await database.SaveVehicleAsync(entity);
            return result > 0;
        }
    }
}
