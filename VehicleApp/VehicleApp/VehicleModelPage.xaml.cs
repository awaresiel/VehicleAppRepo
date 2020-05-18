using Autofac;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VehicleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleModelPage : ContentPage
    {
        VehicleMake vehicleMake;
        VehicleModelViewModel viewModel;
        IVehicleModel VehicleModel;
       List<VehicleModel> VehicleModelList;
        public VehicleModelPage(VehicleMake vehicleMake)
        {
            this.vehicleMake = vehicleMake;
            Debug.Write("==============" + this.vehicleMake.Name);
            InitializeComponent();

            using (var lifeTime =
          App.Container.BeginLifetimeScope())
            {

                viewModel = App.Container.Resolve<VehicleModelViewModel>();
                VehicleModel = App.Container.Resolve<IVehicleModel>();
            };

            initView();


        }
        public VehicleModelPage()
        {
            InitializeComponent();
            vehicleMake = new VehicleMake();
            var dictionary = new VehicleModelService(new VehicleModel()).modelsList;
            VehicleModelList = dictionary[vehicleMake.Name];
            BindingContext = this;
            
        }

        async private void initView()
        {
            var dictionary = await viewModel.getVehicleModelList(VehicleModel, vehicleMake.Name);
            VehicleModelList = dictionary;
            VehicleModelList.ForEach(s => { Debug.Write("==========" + s.Name + " " + s.MakeId); });
            BindingContext = this;
            VehicleModelListView.ItemsSource = VehicleModelList;
        }
    }
}