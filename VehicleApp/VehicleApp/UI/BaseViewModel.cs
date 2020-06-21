using Autofac;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace VehicleApp.UI
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IVehicleMakeService iVehicleMakeService = App.Container.Resolve<IVehicleMakeService>();


        public IVehicleModelService iVehicleModelService = App.Container.Resolve<IVehicleModelService>();

        public BaseViewModel()
        {
            MessagingCenter.Subscribe<MainPage, string>(this, "Delete", async (s, arg) =>
            {
               await iVehicleModelService.DeleteVehicleModel(arg, 0, true);
             
            });
        }

        bool isBusy=false;
        public bool IsBusy
        {
            get { return isBusy; }
            set 
            {    
                SetProperty(ref isBusy, value);
               
            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public int IdForEditing { get; set; }

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

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            //Debug.WriteLine("=backingStore= " + backingStore + " =value= " + value);
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
            Debug.WriteLine("OnPropertyChanged====propertyName============== " + propertyName);
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
