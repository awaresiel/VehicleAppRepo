using Autofac;
using Repository;
using Service;
using System;
using System.ComponentModel;
using System.Diagnostics;
using VehicleApp.Repository;
using VehicleApp.UI;
using Xamarin.Forms;
namespace VehicleApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IViewModel<VehicleMake> viewModel;
        public MainPage(IViewModel<VehicleMake> vm)
        {
            InitializeComponent();
            viewModel =vm;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
          viewModel.LoadItemsCommand.Execute(null);
        }
         private void VehicleListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var vehicleMake = e.SelectedItem as VehicleMake;
            if (vehicleMake == null)
            {
                return;
            }
           
            viewModel.OnItemClickedCommand.Execute(vehicleMake);
            VehicleListView.SelectedItem = null;
        }
         private void ToolbarItem_Clicked_Add_Vehicle(object sender, EventArgs e)
        {
            viewModel.OnAddVehicleCommand.Execute(null);
        }
        private void ToolbarItem_Order_Clicked(object sender, EventArgs e)
        {
            viewModel.OnSortOrderCommand.Execute(null);
        }
         private void OnMore(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            var vehicle = item.CommandParameter as VehicleMake;
            viewModel.OnMoreCommand.Execute(vehicle);
        }
         private void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            if (item == null)
            {
                return;
            }
            var vehicle = item.CommandParameter as VehicleMake;
            viewModel.DeleteItemCommand.Execute(vehicle);
        }
    }
}
