using Autofac;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VehicleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleModelPage : ContentPage
    {
       
        IVehicleModelViewModel viewModel;
        IVehicleModel VehicleModel;
       List<VehicleModel> VehicleModelList;
        public VehicleModelPage(string vehicleMakeName)
        {
         
            InitializeComponent();

            using (var lifeTime =
          App.Container.BeginLifetimeScope())
            {

                viewModel = App.Container.Resolve<IVehicleModelViewModel>();
                VehicleModel = App.Container.Resolve<IVehicleModel>();
            };

            initView(vehicleMakeName);


        }
        public VehicleModelPage()
        {
            InitializeComponent();
            //IVehicleMake make = App.Container.Resolve<IVehicleMake>();
            //IVehicleModel model = App.Container.Resolve<IVehicleModel>();
            //var dictionary = App.Container.Resolve<IVehicleModelViewModel>().GetVehicleModelViewModel().getVehicleModelList(model,"").Result;
            //VehicleModelList = dictionary;
            //BindingContext = this;
            
        }

        async private void initView(string name)
        {
            var dictionary = await viewModel.GetVehicleModelViewModel().getVehicleModelList(VehicleModel, name);
            VehicleModelList = dictionary;
            VehicleModelList.ForEach(s => { Debug.Write("==========" + s.Name + " " + s.MakeId); });
            BindingContext = this;
            VehicleModelListView.ItemsSource = VehicleModelList;
        }
    }
}