using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using Repository;
using System.Threading.Tasks;
using SQLitePCL;
using System.Diagnostics;

namespace VehicleApp.Repository
{
    class VehicleModelRepository : Irepository<VehicleModelEntity>
    {
       
        public readonly SQLiteAsyncConnection database;
        public VehicleModelRepository(SQLiteAsyncConnection database)
        {
            this.database = database;
           
        }

        public async Task<List<VehicleModelEntity>> GetVehiclesAsync(bool ascOrDesc, string makeName)
        {
            await PopulateVehicleModelRepository();

            if (ascOrDesc)
            {      
                return await database.Table<VehicleModelEntity>().Where(v => v.MakeName == makeName).OrderBy(v => v.ModelName).ToListAsync();
            }
            else
            {
                return await database.Table<VehicleModelEntity>().Where(v => v.MakeName == makeName).OrderByDescending(v => v.ModelName).ToListAsync();
            }
        }
        public async Task<VehicleModelEntity> GetVehicleAsync(int id)
        {
            return await database.Table<VehicleModelEntity>().Where(v => v.dataBaseId == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveVehicleAsync(VehicleModelEntity vehicle)
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
            return await database.Table<VehicleModelEntity>().DeleteAsync(v=> v.dataBaseId == id);
        }

        public async Task<int> DeleteVehiclesAsync(string makeName)
        {
            return await database.ExecuteAsync(" DELETE FROM VehicleModelEntity WHERE makeName=? ",makeName);
        }


        public async Task<int> DeleteVehicleAsyncWithSameName(string name)
        {
            return await Task.FromResult(0);
        }

        public async Task<int> UpdateVehiclesAsync(List<VehicleModelEntity> modelsList)
        {  
          return  await database.UpdateAllAsync(modelsList);
        }


        public async Task PopulateVehicleModelRepository()
        {

            // taken from https://nhts.ornl.gov/2009/pub/2009FARSMakeModel.pdf
            List<VehicleModelEntity> modelsList = new List<VehicleModelEntity>()
            {
                new VehicleModelEntity(001, 01, "American Motors", "Rambler", "(AMER)"),
                new VehicleModelEntity(002, 01, "American Motors", "Rebel/Matador/Marlin", "(ALFA)"),
                new VehicleModelEntity(003, 01, "American Motors", "Pacer", "(ALFA)"),
                new VehicleModelEntity(004, 01, "American Motors", "AMX", "(ALFA)"),
                new VehicleModelEntity(031, 31, "Alfa Romeo", "Spider", "(ALFA)"),
                new VehicleModelEntity(032, 31, "Alfa Romeo", "Sports Sedan", "(ALFA)"),
                new VehicleModelEntity(033, 31, "Alfa Romeo", "Sprint/Special", "(ALFA)"),
                new VehicleModelEntity(034, 31, "Alfa Romeo", "GTV-6", "(ALFA)"),
                new VehicleModelEntity(035, 31, "Alfa Romeo", "Alpha 164", "(ALFA)"),
                new VehicleModelEntity(031, 32, "Audi", "Super90", "(AUDI)"),
                new VehicleModelEntity(032, 32, "Audi", "100", "(AUDI)"),
                new VehicleModelEntity(033, 32, "Audi", "Fox", "(AUDI)"),
                new VehicleModelEntity(034, 32, "Audi", "4000", "(AUDI)"),
                new VehicleModelEntity(031, 54, "Acura", "Integra", "(ACUR)"),
                new VehicleModelEntity(032, 54, "Acura", "Legend", "(ACUR)"),
                new VehicleModelEntity(033, 54, "Acura", "NSX", "(ACUR)"),
                new VehicleModelEntity(034, 54, "Acura", "Vigor", "(ACUR)"),
                new VehicleModelEntity(035, 54, "Acura", "TL", "(ACUR)"),
                new VehicleModelEntity(401, 03, "AM General", "Dispatcher", "(AMGN)"),
                new VehicleModelEntity(402, 03, "AM General", "Hummer", "(AMGN)"),
                new VehicleModelEntity(421, 03, "AM General", "Hummer(SUV)", "(AMGN)"),
             };
            int numberOfRows = await database.Table<VehicleModelEntity>().CountAsync();
            if (numberOfRows == 0) { await database.InsertAllAsync(modelsList); }
        }

    }
}

