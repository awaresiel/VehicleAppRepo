using Autofac;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VehicleApp.Repository;
using Xamarin.Forms;
namespace VehicleApp.UI
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IVehicleMakeService iVehicleMakeService;
        public IVehicleModelService iVehicleModelService;

        public ICommand LoadItemsCommand { get; set; }
        public ICommand CreateVehicleCommand { get; set; }
        public ICommand OnItemClickedCommand { get; set; }
        public ICommand OnAddVehicleCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand OnMoreCommand { get; set; }
        public ICommand OnSortOrderCommand { get; set; }

         bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set {  SetProperty(ref isBusy, value); }
        }
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
       
        bool order = true;
        public bool Order
        {
            get { return order; }
            set { SetProperty(ref order, value); }
        }
        string orderName = string.Empty;
        public string OrderName
        {
            get { return orderName; }
            set { SetProperty(ref orderName, value); }
        }

        public BaseViewModel(IVehicleMakeService iVehicleMakeService, IVehicleModelService iVehicleModelService)
        {
            this.iVehicleMakeService = iVehicleMakeService;
            this.iVehicleModelService = iVehicleModelService;
            MessagingCenter.Subscribe<VehicleMakeViewModel, string>(this, "Delete", async (s, arg) =>
            {
                await iVehicleModelService.DeleteVehicleModel(arg,0, true);
            });
        }

        public async void showResultOfVehicleCreation(bool isCreated)
        {
            if (!isCreated)
            {
                await DisplayAlert("Alert", "Fields Cant be empty", "OK");
            }
            else
            {
                await PopCurrentPage();
            }
        }
        async public Task PushNewPage(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(page));
        }
        async public Task DisplayAlert(string title, string alert, string cancelButtonString)
        {
            await Application.Current.MainPage.DisplayAlert(title, alert, cancelButtonString);
        }

        async public Task PopCurrentPage()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
