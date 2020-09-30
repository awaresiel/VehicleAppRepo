
using Autofac;
using Repository;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace VehicleApp.UI
{
    public class VehicleModelViewModel : BaseViewModel, IViewModel<VehicleModel>
    {
       
        private string name;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }
        private string abrv;
        public string Abbrivation { get { return abrv; } set { SetProperty(ref abrv,value); } }
        private int id;
        public int Id { get { return id; } set { SetProperty(ref id, value); } }
        private int makeID;
        public int MakeId { get { return makeID; } set { SetProperty(ref makeID, value); } }
        public ObservableCollection<VehicleModel> VehicleModelList { get; private set; }
       private string VehicleMakeName;
        private VehicleModel temp;
      
        public VehicleModelViewModel(VehicleMake make, IVehicleMakeService s1, IVehicleModelService s2) : base(s1, s2)
        {
             VehicleModelList = new ObservableCollection<VehicleModel>();

            LoadItemsCommand = new Command(async () => await getVehicleModelList());
            CreateVehicleCommand = new Command(execute: async () => await CreateVehicleModel());
            OnAddVehicleCommand = new Command(execute: async () => await OnAddNewVehicleClicked());
            DeleteItemCommand = new Command<VehicleModel>(execute: async v => await OnDeleteClicked(v));
            OnMoreCommand = new Command<VehicleModel>(execute: async v => await OnMoreClicked(v));
            OnSortOrderCommand = new Command(execute:  () =>  SortList());

            Title = "PageMakeVehicleModel";
            VehicleMakeName = make.Name;
            MakeId = make.makeID;
           
            OrderName = Order ? "DESC" : "ASC";
        }
        async public Task getVehicleModelList()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                VehicleModelList.Clear();
                var list = await iVehicleModelService.getVehicleModelListAsync(VehicleMakeName, Order);
                if (list == null || !list.Any()) return;
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
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => IsBusy = false);
                  //IsBusy = false;
            }
        }
        async public Task CreateVehicleModel()
        {
            bool isAdded = false;
            if (Name == null || Id == 0 || Id < 0 || Abbrivation == null|| MakeId==0)
            {
                return;
            }
            if (temp != null)
            {
                isAdded = await iVehicleModelService.CreateVehicleModel(VehicleMakeName, temp.dataBaseId, MakeId, Id, Name, Abbrivation);
            }
            else
            {
                isAdded = await iVehicleModelService.CreateVehicleModel(VehicleMakeName, 0, MakeId, Id, Name, Abbrivation);
            }
             showResultOfVehicleCreation(isAdded);
        }
        public void IsEditVehicleModel( VehicleModel vehicle)
        {
                if (vehicle != null)
                {
                    Name = vehicle.ModelName;
                    Abbrivation = vehicle.Abbreviation;
                    Id = vehicle.Id;
                    MakeId = vehicle.MakeId;
                }
                else
                {
                    Name = "";
                    Abbrivation = "";
                    Id = 0;
                    temp = null;
                   
                }
        }
        
      
        public void SetOrder(bool order)
        {
            Order = order;
            OrderName = Order ? "DESC" : "ASC";
            LoadItemsCommand.Execute(null);
        }
        async public Task<string> DeleteVehicleModel( int databaseID, bool deleteAll)
        {
            var model = await iVehicleModelService.GetVehicleModel(databaseID);
            var isDeleted = await iVehicleModelService.DeleteVehicleModel(model.MakeName,model.dataBaseId, deleteAll);
           
            if (isDeleted)
            {
                return model.ModelName;
            }
            else
            {
                return null;
            }
        }

        async public Task OnAddNewVehicleClicked()
        {
            IsEditVehicleModel(null);
            var page = App.Container.Resolve<PageVehicleModel>(new TypedParameter(typeof(IViewModel<VehicleModel>), this));
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

        private async Task OnMoreClicked(VehicleModel vehicle)
        {
            temp = vehicle;
            IsEditVehicleModel(vehicle);
            var page = App.Container.Resolve<PageVehicleModel>(new TypedParameter(typeof(IViewModel<VehicleModel>), this));
            await PushNewPage(page);
        }

        private async Task OnDeleteClicked(VehicleModel v)
        {
            var deletedVehicleName = await DeleteVehicleModel(v.dataBaseId, false);

            if (deletedVehicleName != null)
            {
               
                await DisplayAlert("Alert", "Vehicle " + deletedVehicleName + " deleted", "OK");
                LoadItemsCommand.Execute(null);
            }
            else
            {
                await DisplayAlert("Alert", "Vehicle not deleted", "OK");
                LoadItemsCommand.Execute(null);
            }
        }

    }
}
