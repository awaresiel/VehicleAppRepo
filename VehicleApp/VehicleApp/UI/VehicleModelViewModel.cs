﻿using Autofac;
using Repository;
using Service;
using System;
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
    public interface IVehicleModelViewModel
    {
        //VehicleModelViewModel GetVehicleModelViewModel();
         Task getVehicleModelList(string name);
        Task<bool> CreateVehicleModel();
        void IsEditVehicleModel(bool isEdit);
        int ListCount();
        System.Windows.Input.ICommand GetCommand();

    }
   public class VehicleModelViewModel : BaseViewModel, IVehicleModelViewModel
    {
        public Command LoadItemsCommand { get; set; }


        public string Name { get; set; }
        public string Abbrivation { get; set; }

        public int Id { get; set; }
        public int MakeId { get; set; }

        public bool IsEdit { get; set; }

        public ObservableCollection<VehicleModel> VehicleModelList { get; private set; }



        string VehicleMakeName;
        
        public VehicleModelViewModel(string name)
        {
            Debug.WriteLine("==============VehicleModelViewModel================name= " + name);
            VehicleModelList = new ObservableCollection<VehicleModel>();
            Title = "PageMakeVehicleModel";
            VehicleMakeName = name;
           
            LoadItemsCommand = new Command(async () => await getVehicleModelList(name));
            
            Debug.WriteLine("==============VehicleModelViewModel after LoadItemsCmd================name= " + name);
        }

      async public Task getVehicleModelList( string name)
        {
            Debug.WriteLine("==============getVehicleModelList================ + " + name);

            if (IsBusy) return;

            IsBusy = true;
            try
            {
                VehicleModelList.Clear();
                var list = await iVehicleModelService.getVehicleModelListAsync(name);

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
            Debug.WriteLine("==============CreateVehicleModel================");
            if (!IsEdit)
            {
                if (Name == null || Id == 0 || Abbrivation == null)
                {
                    return isAdded;
                }
                else
                {
                   isAdded = await iVehicleModelService.CreateVehicleModel(VehicleMakeName, Id,MakeId,Name,Abbrivation);

                    Debug.WriteLine("==============CreateVehicleModel================ isAdded= " +isAdded);
                }
            }


            return isAdded;
        }

        public void IsEditVehicleModel(bool isEdit)
        {
            Debug.WriteLine("==============IsEditVehicleModel================");
            IsEdit = isEdit;
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

        


    }


}
