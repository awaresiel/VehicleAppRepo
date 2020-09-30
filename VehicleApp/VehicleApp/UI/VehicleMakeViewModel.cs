
using Autofac;
using Repository;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace VehicleApp.UI
{
    public class VehicleMakeViewModel : BaseViewModel, IViewModel<VehicleMake>
    {
        private VehicleMake temporaryVehicleMake;
        private int id;
        public int Id { get { return id; } set { SetProperty(ref id, value); } }
        private string name;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }
        private string abrv;
        public string Abbreviation { get { return abrv; } set { SetProperty(ref abrv, value); } }
        public ObservableCollection<VehicleMake> VehicleMakeList { get; private set; }
        public VehicleMakeViewModel(IVehicleMakeService s1,IVehicleModelService s2):base(s1,s2)
        {
            Title = "Vehicle Makes";
            VehicleMakeList = new ObservableCollection<VehicleMake>();
            LoadItemsCommand = new Command(execute: async () => await InitalizeList());
            CreateVehicleCommand = new Command(execute: async () => await CreateVehicleMake());
            OnItemClickedCommand = new Command<VehicleMake>(execute: async v => await PushModelPage(v));
            OnAddVehicleCommand = new Command(execute: async () => await OnAddNewVehicleClicked());
            DeleteItemCommand = new Command<VehicleMake>(execute: async v => await OnDeleteClicked(v) );
            OnMoreCommand = new Command<VehicleMake>(execute: async v => await OnMoreClicked(v) );
            OnSortOrderCommand = new Command(execute: () =>  SortList() );

            OrderName = Order ? "DESC" : "ASC";
        }
       private async Task InitalizeList()
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
               Xamarin.Forms.Device.BeginInvokeOnMainThread(()=>IsBusy = false);
            }
        }
        async public Task CreateVehicleMake()
        {
            if (Name == null || Id == 0 || Id < 0 || Abbreviation == null)
            {
                return;
            }
            await iVehicleMakeService.DeleteVehicleAsyncWithSameName(Name);
            bool isAdded;
            if (temporaryVehicleMake == null)
            {
                VehicleMake vehicleMake = new VehicleMake(Id, Name, Abbreviation);
             
                isAdded = await iVehicleMakeService.InsertVehicle(vehicleMake.makeID, vehicleMake.Name, vehicleMake.Abbreviation);
            }
            else
            {
                VehicleMake vehicleMake = new VehicleMake(Id, Name, Abbreviation);
                 vehicleMake.dataBaseId = temporaryVehicleMake.dataBaseId;
                var flag = await iVehicleMakeService.UpdateVehicle(vehicleMake);
                await iVehicleModelService.UpdateModelKeys(temporaryVehicleMake.Name, vehicleMake.Name, vehicleMake.makeID);
                isAdded = flag == 0 ? false : true;
            }
            showResultOfVehicleCreation(isAdded);
        }

       
         public  void IsEditVehicleMake(VehicleMake vehicle)
        {
            if (vehicle != null)
                {
                    Name = vehicle.Name;
                    Abbreviation = vehicle.Abbreviation;
                    Id = vehicle.makeID;
                    temporaryVehicleMake = vehicle;
                }
                else
                {
                    Name = "";
                    Abbreviation = "";
                    Id = 0;
                 temporaryVehicleMake = null;
            }
            
        }
        
         public void SetOrder(bool order)
        {
            Order = order;
            OrderName = Order ? "DESC" : "ASC";
           LoadItemsCommand.Execute(null);
           
        }
    
        async public Task<bool> DeleteVehicleMake(VehicleMake v)
        {
            var deleted = await iVehicleMakeService.DeleteVehicle(v);
            LoadItemsCommand.Execute(null);
            return deleted;
        }


        async private Task PushModelPage(VehicleMake v)
        {
            var vm = App.Container.Resolve<IViewModel<VehicleModel>>(new NamedParameter("VehicleName", v));
            var page = App.Container.Resolve<VehicleModelPage>(new TypedParameter(typeof(IViewModel<VehicleModel>), vm));
            await PushNewPage(page);
        }

         private async Task OnAddNewVehicleClicked()
        {
            IsEditVehicleMake(null);
            PageMakeVehicle page = App.Container.Resolve<PageMakeVehicle>(new TypedParameter(typeof(IViewModel<VehicleMake>), this));
            await PushNewPage(page);
        }

         private void SortList()
        {
            if (!Order)
            {
              SetOrder(true);
            }
            else
            {
              SetOrder(false);
            }
        }

        private async Task OnMoreClicked(VehicleMake v)
        {
            IsEditVehicleMake(v);
            PageMakeVehicle page = App.Container.Resolve<PageMakeVehicle>(new TypedParameter(typeof(IViewModel<VehicleMake>), this));
           await PushNewPage(page);
        }

        private async Task OnDeleteClicked(VehicleMake vehicle)
        {
            bool deleted = await DeleteVehicleMake(vehicle);
            if (deleted)
            {
                MessagingCenter.Send(this, "Delete", vehicle.Name);
             
               await DisplayAlert("Alert", "Vehicle " + vehicle.Name + " deleted", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Vehicle " + vehicle.Name + " not deleted", "OK");
            }
        }




    }

}
