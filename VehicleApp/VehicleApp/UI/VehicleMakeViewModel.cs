using Autofac;
using AutoMapper;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace VehicleApp.UI
{

    public class VehicleMakeViewModel : BaseViewModel, IVehicleMakeViewModel
    {

        public Command LoadItemsCommand { get; set; }
        
        string TemporarlyOriginalName = string.Empty;

        private int id;
        public int Id { get { return id; } set { SetProperty(ref id, value);  } }
        private string name;
        public string Name { get { return name; } set { SetProperty(ref name,value); } }
        private string abrv;
        public string Abbreviation { get { return abrv; } set { SetProperty(ref abrv, value); } }


        public ObservableCollection<VehicleMake> VehicleMakeList { get; private set; }

        public VehicleMakeViewModel()
        {
            Title = "Vehicle Makes";
            VehicleMakeList = new ObservableCollection<VehicleMake>();
            LoadItemsCommand = new Command(execute: async () => await InitalizeList());
            OrderName = Order ? "DESC" : "ASC";
         }

        async Task InitalizeList()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                VehicleMakeList.Clear();
                var list = await iVehicleMakeService.GetVehiclesAsync(Order);

                foreach (var item in list)
                {
                    VehicleMakeList.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("======exception==== " + ex);
            }
            finally
            {
               IsBusy = false;
            }

        }

        async public Task<bool> CreateVehicleMake()
        {
            bool isAdded = false;
            if (Name == null || Id == 0 || Id < 0 || Abbreviation == null)
            {
                return isAdded;
            }

            if (IdForEditing ==-1)
            {
                    // IS THIS CORRECT USE OF AUTOMAPPER FOR THIS PROJECT?
                    VehicleMake vehicleMake = App.Mapper.Map<VehicleMakeViewModel, VehicleMake>(this);
                    isAdded = await iVehicleMakeService.MakeVehicle(vehicleMake.Id, vehicleMake.Name, vehicleMake.Abbreviation);
             }
            else
            {    
                VehicleMake vehicleMake = App.Mapper.Map<VehicleMakeViewModel, VehicleMake>(this);
                var flag= await iVehicleMakeService.UpdateVehicle(IdForEditing, vehicleMake);
               
               await iVehicleModelService.UpdateDictionaryKey(TemporarlyOriginalName, vehicleMake.Name);
                
                isAdded = flag == null ? false : true;
               
            }
                 return isAdded;
        }

        async public void IsEditVehicleMake(int id)
        {
            
            IdForEditing = id;
            if (IdForEditing != -1)
            {
                var vehicles = await iVehicleMakeService.GetVehicleByID(id);
                if (vehicles!=null)
                {
                    Name = vehicles[0].Name;
                    TemporarlyOriginalName = Name;
                    Abbreviation = vehicles[0].Abbreviation;
                    Id = vehicles[0].Id;
                
                }
               
            }
        }

        public int ListCount()
        {
            if (VehicleMakeList != null)
            {
                return VehicleMakeList.Count();
            }
            return 0;
        }

        public ICommand GetCommand()
        {
            return LoadItemsCommand;
        }

        public bool GetOrder()
        {
            return Order;
        }

        async public void SetOrder(bool order)
        {
            Order = order;
            OrderName = Order ? "DESC" : "ASC";
            await InitalizeList();

        }

       async public Task<bool> DeleteVehicleMake(string name)
        {
            var deleted = await iVehicleMakeService.DeleteVehicle(name);
            await InitalizeList();
            return  deleted;
        }
    }

}
