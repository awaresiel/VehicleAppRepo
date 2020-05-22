using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public interface IVehicleMake
    {
        VehicleMake MakeVehicle(int id, string name,string abbr);
        
    }
}
