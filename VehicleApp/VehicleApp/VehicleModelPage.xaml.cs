using Autofac;
using System;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Repository;
using System.Diagnostics;

namespace VehicleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleModelPage : ContentPage
    {
        IViewModel<VehicleModel> viewModel;
     
        public VehicleModelPage(IViewModel<VehicleModel> vm)
        {   InitializeComponent();
            viewModel = vm;
            viewModel.LoadItemsCommand.Execute(null);
            BindingContext = viewModel;
        }
        public VehicleModelPage()
        {
            InitializeComponent();
        }
      
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadItemsCommand.Execute(null);
        }
         private void ToolbarItem_Clicked_Add_Vehicle_Make(object sender, EventArgs e)
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
            VehicleModel vehicle = item.CommandParameter as VehicleModel;
            viewModel.OnMoreCommand.Execute(vehicle);
        }
         private void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            if (item == null)
            {
                return;
            }
            var vehicle = item.CommandParameter as VehicleModel;
            viewModel.DeleteItemCommand.Execute(vehicle);
            
        }
    }
}