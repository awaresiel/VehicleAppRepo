using Autofac;
using Repository;
using System;
using System.Diagnostics;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace VehicleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMakeVehicle : ContentPage
    {
        IViewModel<VehicleMake> vm;
        public PageMakeVehicle(IViewModel<VehicleMake> v)
        {
            vm = v;
            InitializeComponent();
            BindingContext = vm;
        }
         private void Button_Clicked(object sender, EventArgs e)
        {
            vm.CreateVehicleCommand.Execute(null);
        }
    }
}