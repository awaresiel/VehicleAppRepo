using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.UI
{
    class VehicleModelViewModel
    {

        IVehicleModelService VehicleModelService;
        

        public VehicleModelViewModel(IVehicleModelService VehicleModelService)
        {
            this.VehicleModelService = VehicleModelService;
        }
        public VehicleModelViewModel()
        {
            this.VehicleModelService = new VehicleModelService(new VehicleModel());
        }

      async public Task<List<VehicleModel>> getVehicleModelList(IVehicleModel model, string name)
        {
            return await Task.FromResult( VehicleModelService.GetVehicleModelService(model)).Result.Result.getVehicleModelListAsync(name);
        }
    }
}
