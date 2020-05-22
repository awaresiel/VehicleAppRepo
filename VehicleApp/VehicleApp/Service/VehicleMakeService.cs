using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Service
{
    public interface IVehicleMakeService
    {
         Task<VehicleMakeService> GetVehicleMakeService(IVehicleMake s);
         Task<List<VehicleMake>> GetVehicleByID(int id);
         Task<List<VehicleMake>> GetVehiclesAsync();
         Task<List<VehicleMake>> FilterVehicles(string name);
         Task<List<VehicleMake>> SortVehiclesASC_DESC(bool ascOrDesc);
         Task<bool> MakeVehicle(int id, string name, string abbr);

    }

    public class VehicleMakeService: IVehicleMakeService
    {
       async public Task<VehicleMakeService> GetVehicleMakeService(IVehicleMake s)
        {
            return await Task.FromResult(new VehicleMakeService(s));
        }


        public static List<VehicleMake> vehiclesList { get; private set; } = new List<VehicleMake>(){

             // taken from https://nhts.ornl.gov/2009/pub/2009FARSMakeModel.pdf
                new VehicleMake(54, "Acura", "(ACUR)"),
                new VehicleMake(31, "Alfa Romeo", "(ALFA)"),
                new VehicleMake(03, "AM General", "(AMGN)"),
                new VehicleMake(01, "American Motors", "(AMER)"),
                new VehicleMake(32, "Audi", "(AUDI)")

            };
        IVehicleMake iVehicleMake;
        public  VehicleMakeService(IVehicleMake iVehicleMake)
        {
           
            this.iVehicleMake = iVehicleMake;
            
        }

      


        public async Task<List<VehicleMake>> GetVehicleByID(int id)
        {
          
            var item = await Task.Run(()=> vehiclesList.Any( v => v.Id == id));
            
            if (item)
            {
                List<VehicleMake> list = await Task.Run(()=>vehiclesList.ToList().FindAll( (v) =>  v.Id == id).ToList());
                

                return list;
            }

            return null;
            
        }

        public async Task<List<VehicleMake>> GetVehiclesAsync()
        {
            return await Task.FromResult(vehiclesList);
        }

        public async Task<bool> DeleteVehicleByID(int id)
        {
            int number = await Task.Run(() => vehiclesList.ToList().RemoveAll((v) => v.Id == id));
            if (number>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<VehicleMake>> FilterVehicles(string name)
        {
            List<VehicleMake> tempList = vehiclesList;

            var list = await Task.Run(() => tempList.ToList().FindAll(v => v.Name == name));

            tempList.Clear();
            foreach (var item in tempList) tempList.Add(item);

            return tempList;
        }


        public async Task<List<VehicleMake>> SortVehiclesASC_DESC(bool ascOrDesc)
        {
            List<VehicleMake> templist = vehiclesList;

            if (ascOrDesc)
            {
                var tl = await Task.Run(()=> templist.OrderBy(v => v.Name).ToList());
               
                return tl;
            }
            else
            {
                var tl = await Task.Run(()=> templist.OrderByDescending(v1 => v1.Name).ToList());
               
                return tl;
            }
            

           
        }

       async public Task<bool> MakeVehicle(int id, string name, string abbr)
        {
            var vehicle = iVehicleMake.MakeVehicle(id, name, abbr);
            lock (this)
            {
                vehiclesList.Add(vehicle);
              
              
            }

            return await Task.FromResult(vehiclesList.Contains(vehicle));
        }

       
    }
}
