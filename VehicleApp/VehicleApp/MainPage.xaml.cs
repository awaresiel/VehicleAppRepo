using Autofac;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VehicleApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
     
        IVehicleMakeViewModel viewModel;
      
       
        public MainPage()
        {   InitializeComponent();
            viewModel = App.Container.Resolve<IVehicleMakeViewModel>();
            BindingContext = viewModel;
           

        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            System.Diagnostics.Debug.WriteLine("==============OnAppearing()============");

            if (viewModel.ListCount() == 0)
                viewModel.GetCommand().Execute(null);
            else viewModel.GetCommand().Execute(viewModel);
        }

        async private void VehicleListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var vehicleMake = e.SelectedItem as VehicleMake;
            if (vehicleMake == null)
            {
                return;
            }
            await Navigation.PushModalAsync(new NavigationPage(new VehicleModelPage(vehicleMake.Name)));

            VehicleListView.SelectedItem = null;
        }

        async private void ToolbarItem_Clicked_Add_Vehicle(object sender, EventArgs e)
        {
            PageMakeVehicle page = new PageMakeVehicle(false);
            
            await Navigation.PushModalAsync(page);
        
            
        }
    }
}
