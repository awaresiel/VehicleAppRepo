using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VehicleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleModelPage : ContentPage
    {
        VehicleMake vehicleMake;
       List<VehicleModel> VehicleModelList;
        public VehicleModelPage(VehicleMake vehicleMake)
        {
            this.vehicleMake = vehicleMake;
            Debug.Write("==============" + vehicleMake.Name);
            InitializeComponent();
            
            var dictionary = new VehicleModelService(new VehicleModel()).modelsList;
            VehicleModelList = dictionary[this.vehicleMake.Name];
            VehicleModelList.ForEach(s => { Debug.Write("=========="+s.Name +" " + s.MakeId); });
             BindingContext = this;
            VehicleModelListView.ItemsSource = VehicleModelList;


        }
        public VehicleModelPage()
        {
            InitializeComponent();
            vehicleMake = new VehicleMake();
            var dictionary = new VehicleModelService(new VehicleModel()).modelsList;
            VehicleModelList = dictionary[this.vehicleMake.Name];
            BindingContext = this;
            
        }
    }
}