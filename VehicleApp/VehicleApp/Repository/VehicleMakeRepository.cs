using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using System.Threading.Tasks;
using SQLitePCL;
using System.Diagnostics;
using System.IO;

namespace VehicleApp.Repository
{
    public class VehicleMakeRepository : Irepository<VehicleMakeEntity>
    {
        public readonly SQLiteAsyncConnection database;
        public VehicleMakeRepository(SQLiteAsyncConnection database)
        {
            this.database = database;
           
        }
        public async Task<List<VehicleMakeEntity>> GetVehiclesAsync(bool ascOrDesc, string makeName)
        {
            await PopulateVehicleMakeRepository();
         
            if (ascOrDesc)
            {
              
                return await database.Table<VehicleMakeEntity>().OrderBy(v => v.Name).ToListAsync();
            }
            else
            {
              
                return await database.Table<VehicleMakeEntity>().OrderByDescending(v => v.Name).ToListAsync();
            }
        }
        public async Task<VehicleMakeEntity> GetVehicleAsync(int id)
        {
            return await database.Table<VehicleMakeEntity>().Where(v => v.dataBaseId == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveVehicleAsync(VehicleMakeEntity vehicle)
        {
            if (vehicle.dataBaseId != 0)
            { 
                return await database.UpdateAsync(vehicle);
            }
            else
            {
                return await database.InsertAsync(vehicle);
            }
           
        }
        public async Task<int> DeleteVehicleAsync(int id)
        {
            return await database.Table<VehicleMakeEntity>().DeleteAsync(v => v.dataBaseId == id);
        }
        public async Task<int> DeleteVehicleAsyncWithSameName(string name)
        {
            return await database.Table<VehicleMakeEntity>().DeleteAsync(v => v.Name.Equals(name));
        }

        public async Task<int> UpdateVehiclesAsync(List<VehicleMakeEntity> modelsList)
        {
            return await database.InsertAllAsync(modelsList);
        }

        public async Task<int> DeleteVehiclesAsync(string makeName)
        {
            return await Task.FromResult(-1);
        }

        public async Task PopulateVehicleMakeRepository()
        {
            List<VehicleMakeEntity> vehiclesList = new List<VehicleMakeEntity>(){
             // taken from https://nhts.ornl.gov/2009/pub/2009FARSMakeModel.pdf
                new VehicleMakeEntity(54, "Acura", "(ACUR)"),
                new VehicleMakeEntity(31, "Alfa Romeo", "(ALFA)"),
                new VehicleMakeEntity(03, "AM General", "(AMGN)"),
                new VehicleMakeEntity(01, "American Motors", "(AMER)"),
                new VehicleMakeEntity(32, "Audi", "(AUDI)")
            };
           
                int numberOfRows = await database.Table<VehicleMakeEntity>().CountAsync();
                if (numberOfRows == 0) { await database.InsertAllAsync(vehiclesList); }
           
        }

    }
}
