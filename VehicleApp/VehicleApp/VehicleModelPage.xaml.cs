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
        string VehicleMakeName = "";
      
        public VehicleModelPage(string vehicleMakeName)
        {
            VehicleMakeName = vehicleMakeName;
            InitializeComponent();
            //viewModel = App.Container.Resolve<IVehicleModelViewModel>();
            viewModel = App.Container.Resolve<IVehicleModelViewModel>(new TypedParameter(typeof(string), vehicleMakeName));

            initView(vehicleMakeName);


        }
        public VehicleModelPage()
        {
            InitializeComponent();
           
        }

          private void initView(string vehicleMakeName )
        {
           
            viewModel.getVehicleModelList(vehicleMakeName);
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


        async private void ToolbarItem_Clicked_Add_Vehicle_Make(object sender, EventArgs e)
        {
            PageMakeVehicleModel page = new PageMakeVehicleModel(VehicleMakeName,false);

            await Navigation.PushModalAsync(page);
            
        }
    }
}