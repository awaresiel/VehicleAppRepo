using Autofac;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.UI
{
    public interface IVehicleModelViewModel
    {
        VehicleModelViewModel GetVehicleModelViewModel();
    }
   public class VehicleModelViewModel : IVehicleModelViewModel
    {

        IVehicleModelService VehicleModelService;
        

        public VehicleModelViewModel(IVehicleModelService VehicleModelService)
        {
            this.VehicleModelService = VehicleModelService;
        }
        public VehicleModelViewModel()
        {
            this.VehicleModelService = App.Container.Resolve<IVehicleModelService>();
        }

      async public Task<List<VehicleModel>> getVehicleModelList(IVehicleModel model, string name)
        {
            return await Task.FromResult( VehicleModelService.GetVehicleModelService(model)).Result.Result.getVehicleModelListAsync(name);
        }

        public VehicleModelViewModel GetVehicleModelViewModel()
        {
            return new VehicleModelViewModel();
        }
    }
}
