using Autofac;
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
    public partial class PageMakeVehicleModel : ContentPage
    {
        IVehicleModelViewModel vm;
        string VehicleMakeName;
        public PageMakeVehicleModel(string VehicleMake,bool isEdit)
        {
            InitializeComponent();
            this.VehicleMakeName = VehicleMake;
            using (var scope = App.Container.BeginLifetimeScope())
            {
                vm = App.Container.Resolve<IVehicleModelViewModel>(new TypedParameter(typeof(string), VehicleMake));
            }
            vm.IsEditVehicleModel(isEdit);
            BindingContext = vm;
        }

       async private void Button_Clicked(object sender, EventArgs e)
        {
            bool isCreated = await vm.CreateVehicleModel();

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