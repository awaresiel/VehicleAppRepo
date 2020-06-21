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
            var item = (MenuItem)sender;
            int id = int.Parse(item.CommandParameter.ToString());

            if (id == 0)
            {
                id = -1;
            }

            PageMakeVehicleModel page = new PageMakeVehicleModel(VehicleMakeName,id);

            await Navigation.PushModalAsync(page);
            
        }

        private void ToolbarItem_Order_Clicked(object sender, EventArgs e)
        {
            if (!viewModel.GetOrder())
            {
                viewModel.SetOrder(true);
                viewModel.GetCommand().Execute(viewModel);
            }
            else
            {
                viewModel.SetOrder(false);
                viewModel.GetCommand().Execute(viewModel);
            }
        }



        async private void OnMore(object sender, EventArgs e)
        {

            var item = (MenuItem)sender;

            int id = int.Parse(item.CommandParameter.ToString());
            PageMakeVehicleModel page = new PageMakeVehicleModel(VehicleMakeName,id);
            await Navigation.PushModalAsync(page);
        }



        async private void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            
            if (item == null)
            {
                return;
            }
            int id = int.Parse(item.CommandParameter.ToString());

            var deletedVehicleName = await viewModel.DeleteVehicleModel(VehicleMakeName, id,false);
            viewModel.GetCommand().Execute(viewModel);

            if (deletedVehicleName!=null)
            {
             //  MessagingCenter.Send(this, "Delete", deletedVehicleName);
               
                await DisplayAlert("Alert", "Vehicle " + deletedVehicleName + " deleted", "OK");
            }
            else
            {
                
                await DisplayAlert("Alert", "Vehicle not deleted", "OK");
            }


        }



    }
}