using Autofac;
using Repository;
using System;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace VehicleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMakeVehicleModel : ContentPage
    {
        IViewModel<VehicleModel> vm;
        public PageMakeVehicleModel(IViewModel<VehicleModel> vm)
        {
            InitializeComponent();
            this.vm = vm;
            BindingContext = vm;
        }
         private void Button_Clicked(object sender, EventArgs e)
        {
            vm.CreateVehicleCommand.Execute(null);
        }
    }
}