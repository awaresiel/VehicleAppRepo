using Autofac;
using Repository;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace VehicleApp.UI
{
    public class VehicleModelViewModel : BaseViewModel, IVehicleModelViewModel
    {
        public Command LoadItemsCommand { get; set; }

        public string Name { get; set; }
 
        public string Abbrivation { get; set; }

        public int Id { get; set; }
        public int MakeId { get; set; }

        public ObservableCollection<VehicleModel> VehicleModelList { get; private set; }



        string VehicleMakeName;

        public VehicleModelViewModel(string name)
        {
            VehicleModelList = new ObservableCollection<VehicleModel>();
            Title = "PageMakeVehicleModel";
            VehicleMakeName = name;
            LoadItemsCommand = new Command(async () => await getVehicleModelList(name));
            OrderName = Order ? "DESC" : "ASC";

        }

        async public Task getVehicleModelList(string name)
        {
            Debug.WriteLine("==============getVehicleModelList================ + " + name);

            if (IsBusy) return;

            IsBusy = true;
            try
            {
                VehicleModelList.Clear();
                var list = await iVehicleModelService.getVehicleModelListAsync(name, Order);

                foreach (var item in list)
                {
                    VehicleModelList.Add(item);

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }



        async public Task<bool> CreateVehicleModel()
        {
            bool isAdded = false;

            if (Name == null || Id == 0 || Id<0 || Abbrivation == null)
            {
                return isAdded;
            }
            
           
            if (IdForEditing == -1)
            {
                    // VehicleModel vehicleModel = App.Mapper.Map<VehicleModelViewModel, VehicleModel>(this);

                    isAdded = await iVehicleModelService.CreateVehicleModel(VehicleMakeName,IdForEditing, Id, MakeId, Name, Abbrivation);
 
            }
            else
            {
                    isAdded = await iVehicleModelService.CreateVehicleModel(VehicleMakeName,IdForEditing, Id, MakeId, Name, Abbrivation);
            }


            return isAdded;
        }

        public async void IsEditVehicleModel(string name,int id)
        {
            Debug.WriteLine("==============IsEditVehicleModel================ " + id);
            IdForEditing = id;
            if (id != -1)
            {
                var vehicles = await iVehicleModelService.GetVehicleModel(name,id);
                if (vehicles[0] != null)
                {
                    Name = vehicles[0].Name;
                    Abbrivation = vehicles[0].Abbreviation;
                    Id = vehicles[0].Id;
                    MakeId = vehicles[0].MakeId;

                    Debug.WriteLine("name === " + Name);
                   

                }
               

            }
            
        }

        public int ListCount()
        {
            if (VehicleModelList != null)
            {
                Debug.WriteLine("==============List count not null================");
                return VehicleModelList.Count();
            }
            return 0;
        }

        public ICommand GetCommand()
        {
            Debug.WriteLine("==============getcommand()================");
            return LoadItemsCommand;
        }

        public bool GetOrder()
        {
            return Order;
        }

        public void SetOrder(bool order)
        {
            Order = order;
            OrderName = Order ? "DESC" : "ASC";
        }

        async public Task<string> DeleteVehicleModel(string name, int id, bool deleteAll)
        {
            var model = await iVehicleModelService.GetVehicleModel(name,id);
            Debug.WriteLine("== model[0].Name=="+model[0].Name);
            var isDeleted= await iVehicleModelService.DeleteVehicleModel(name, model[0].Id, deleteAll);

            if (isDeleted)
            {
                return model[0].Name;
            }
            else
            {
                return null;
            }
            
        }


    }
}
