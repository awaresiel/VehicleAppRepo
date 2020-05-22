﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VehicleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMakeVehicle : ContentPage
    {
       IVehicleMakeViewModel vm;
        public PageMakeVehicle(bool isEdit)
        {
            InitializeComponent();
            vm = App.Container.Resolve<IVehicleMakeViewModel>();
            vm.IsEditVehicleMake(isEdit);
            BindingContext = vm;
        }

       async private void Button_Clicked(object sender, EventArgs e)
        {
          
            bool isCreated = await vm.CreateVehicleMake();

            if (!isCreated)
            {
                await DisplayAlert("Alert", "Fields Cant be empty", "OK");

            }
            else
            {
                
                await Navigation.PopModalAsync();
            }

        }
    }
}