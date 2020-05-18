using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace VehicleApp.UI
{
    class VehicleMakeViewModel
    {
        IVehicleMakeService iVehicleMakeService;


        public VehicleMakeViewModel(IVehicleMakeService VehicleMakeService)
        {
            this.iVehicleMakeService = VehicleMakeService;

        }
        public VehicleMakeViewModel()
        {
            this.iVehicleMakeService = new VehicleMakeService(new VehicleMake());
        }

        async public Task<List<VehicleMake>> getVehicleMakeList(IVehicleMake s)
        {
           
            return await Task.FromResult(iVehicleMakeService.GetVehicleMakeService(s).Result.GetVehiclesAsync().Result);
        }
    }

}
