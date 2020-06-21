using Autofac;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
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
     
        IVehicleMakeViewModel viewModel;
      
       
        public MainPage()
        {   InitializeComponent();
            viewModel = App.Container.Resolve<IVehicleMakeViewModel>();
            BindingContext = viewModel;

          

        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            System.Diagnostics.Debug.WriteLine("==============OnAppearing()============");

            if (viewModel.ListCount() == 0)
            {
               
                viewModel.GetCommand().Execute(null);
             
            }

            else {
                viewModel.GetCommand().Execute(viewModel);
            }
            
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

        async private void ToolbarItem_Clicked_Add_Vehicle(object sender, EventArgs e)
        {
            PageMakeVehicle page = new PageMakeVehicle(-1);
            
            await Navigation.PushModalAsync(page);
        
            
        }

        private void ToolbarItem_Order_Clicked(object sender, EventArgs e)
        {
            if (!viewModel.GetOrder())
            {
                viewModel.SetOrder(true);
              
            }
            else
            {
                viewModel.SetOrder(false);
               
            }
        }

        async private void OnMore(object sender, EventArgs e)
        {
            
           var item = (MenuItem)sender;

           Debug.WriteLine("============ item ======  "+ item.ToString());
          Debug.WriteLine("============ item.Text ======  "+ item.CommandParameter.ToString());
            int id = int.Parse( item.CommandParameter.ToString());
           PageMakeVehicle page = new PageMakeVehicle(id);
            await Navigation.PushModalAsync(page);
        }

       

        async private void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender; 
           
                if (item == null)
                {
                    return;
                }

                  bool deleted = await viewModel.DeleteVehicleMake(item.CommandParameter.ToString());
        
            
                if (deleted)
                {
                   MessagingCenter.Send(this, "Delete", item.CommandParameter.ToString());

                   await DisplayAlert("Alert", "Vehicle "+ item.CommandParameter.ToString()+ " deleted", "OK");
                }
                else
                {
                    await DisplayAlert("Alert", "Vehicle " + item.CommandParameter.ToString() + " not deleted", "OK");
                }
          
            
        }

        
    }
}
