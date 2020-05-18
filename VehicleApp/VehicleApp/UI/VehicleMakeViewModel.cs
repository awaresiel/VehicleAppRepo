using Autofac;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace VehicleApp.UI
{
    public interface IVehicleMakeViewModel
    {
        VehicleMakeViewModel getVehicleMakeViewModel();
    }
    public class VehicleMakeViewModel: IVehicleMakeViewModel
    {
        IVehicleMakeService iVehicleMakeService;


        public VehicleMakeViewModel(IVehicleMakeService VehicleMakeService)
        {
            this.iVehicleMakeService = VehicleMakeService;

        }
        public VehicleMakeViewModel()
        {
            this.iVehicleMakeService = App.Container.Resolve<IVehicleMakeService>();
        }

        async public Task<List<VehicleMake>> getVehicleMakeList(IVehicleMake s)
        {
           
            return await Task.FromResult(iVehicleMakeService.GetVehicleMakeService(s).Result.GetVehiclesAsync().Result);
        }

        public VehicleMakeViewModel getVehicleMakeViewModel()
        {
            return new VehicleMakeViewModel();
        }
    }

}
