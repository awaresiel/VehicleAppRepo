using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Service
{

    public class VehicleMakeService: IVehicleMakeService
    {
     

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
          
            var item = await Task.FromResult( vehiclesList.Any( v => v.Id == id));
            
            if (item)
            {
                List<VehicleMake> list = await Task.FromResult(vehiclesList.FindAll( (v) =>  v.Id == id).ToList());
                

                return list;
            }

            return null;
            
        }

        public async Task<List<VehicleMake>> UpdateVehicle(int id,VehicleMake vehicleMake)
        {


            var index = await Task.FromResult(vehiclesList.FindIndex(v => v.Id == id));

            if (index !=-1)
            {
               
                vehiclesList[index] = vehicleMake;
            
                return vehiclesList;
            }

            return null;

        }

        public async Task<List<VehicleMake>> GetVehiclesAsync(bool ascOrDesc)
        {
            var list =  SortVehiclesASC_DESC(ascOrDesc);
            
                return await list;
            
        }

        public async Task<bool> DeleteVehicle(string name)
        {
            int number = await Task.FromResult( vehiclesList.RemoveAll((v) => v.Name == name));
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

            var list = await Task.Run(() => tempList.FindAll(v => v.Name == name));

            tempList.Clear();
            foreach (var item in tempList) tempList.Add(item);

            return tempList;
        }


        async public Task<List<VehicleMake>> SortVehiclesASC_DESC(bool ascOrDesc)
        {
            List<VehicleMake> templist = vehiclesList;

            if (ascOrDesc)
            {

                
             //       var tl = Task.Run(() =>
             //      {
             //          var stopwatch = new Stopwatch();
             //           stopwatch.Start();
             //          System.Threading.Thread.Sleep(2000);
             //          Debug.WriteLine("===== async: Running for {0} seconds, Thread name= {1}", stopwatch.Elapsed.TotalSeconds, System.Threading.Thread.CurrentThread.Name);
             //          stopwatch.Stop();

             //          return templist.OrderBy(v => v.Name).ToList();
             //      });
             //       Debug.WriteLine("== Another thread name main threard = " + System.Threading.Thread.CurrentThread.ToString());
                    
               
                //var tl = await Task.Run(()=>  templist.OrderBy(v => v.Name).ToList());
                var tl =  Task.FromResult( templist.OrderBy(v => v.Name).ToList());

                return await tl;
                
            }
            else
            {
               // var tl =  Task.Run(()=> templist.OrderByDescending(v1 => v1.Name).ToList());
                var tl = await Task.FromResult( templist.OrderByDescending(v1 => v1.Name).ToList());
          
                return  tl;
            }
            
        }

       async public Task<bool> MakeVehicle(int id, string name, string abbr)
        {
            var vehicle = iVehicleMake.MakeVehicle(id, name, abbr);
            lock (this)
            {

                if (vehiclesList.Any(v=> v.Id == id))
                {
                    vehiclesList.RemoveAll(v => v.Id == id);
                    
                }
             
                    vehiclesList.Add(vehicle);



            }

            return await Task.FromResult(vehiclesList.Contains(vehicle));
        }

       
    }
}
