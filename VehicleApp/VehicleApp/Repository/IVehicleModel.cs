using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
   public interface IVehicleModel
    {
       VehicleModel GetVehicleModel(int id, int makeID, string name, string abbreviation);
    }
}
