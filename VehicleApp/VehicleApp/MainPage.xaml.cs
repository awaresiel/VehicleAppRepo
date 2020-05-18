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
       public List<VehicleMake> VehicleMakeList { get; private set; }
        IVehicleMakeViewModel viewModel;
      
        IVehicleMake ivehicleMake;
        public MainPage()
        {   InitializeComponent();

            BindingContext = this;
            VehicleMakeList = new List<VehicleMake>();

            InitList();
           
        }

        async private void InitList()
        {
            using (var lifeTime =
            App.Container.BeginLifetimeScope()) {
                
                viewModel = App.Container.Resolve<IVehicleMakeViewModel>();
                ivehicleMake = App.Container.Resolve<IVehicleMake>();
            };
            VehicleMakeList = await viewModel.getVehicleMakeViewModel().getVehicleMakeList(ivehicleMake);

            VehicleListView.ItemsSource = VehicleMakeList;
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
    }
}
