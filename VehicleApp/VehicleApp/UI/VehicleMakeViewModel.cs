using Autofac;
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
    public interface IVehicleMakeViewModel
    {
        // VehicleMakeViewModel getVehicleMakeViewModel();
        Task<bool> CreateVehicleMake();
        void IsEditVehicleMake(bool isEdit);
        int ListCount();
        System.Windows.Input.ICommand GetCommand();
    }
    public class VehicleMakeViewModel : BaseViewModel, IVehicleMakeViewModel
    {

        public Command LoadItemsCommand { get; set; }


        public string Name { get; set; }
        public string Abbrivation { get; set; }

        public int Id { get; set; }

        public bool IsEdit { get; set; }

        public ObservableCollection<VehicleMake> VehicleMakeList { get; private set; }



        public VehicleMakeViewModel()
        {
            Title = "VehicleMakePage";
            VehicleMakeList = new ObservableCollection<VehicleMake>();
            LoadItemsCommand = new Command(async () => await InitalizeList());



        }

        async public Task InitalizeList()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                VehicleMakeList.Clear();
                var list = await iVehicleMakeService.GetVehiclesAsync();
                foreach (var item in list)
                {
                    System.Diagnostics.Debug.WriteLine("=InitalizeList============ " + item.Name);
                    VehicleMakeList.Add(item);
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


        async public Task<bool> CreateVehicleMake()
        {
           
            bool isAdded = false;

            if (!IsEdit)
            {
                if (Name == null || Id == 0 || Abbrivation == null)
                {
                    return isAdded;
                }
                else
                {
                    isAdded=  await iVehicleMakeService.MakeVehicle(Id, Name, Abbrivation);
                  
                }
            }


            return isAdded;
        }

        public void IsEditVehicleMake(bool isEdit)
        {
            IsEdit = isEdit;
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
            Debug.WriteLine(" GET LoadItemsCommand====");
            return LoadItemsCommand;
        }


    }

}
