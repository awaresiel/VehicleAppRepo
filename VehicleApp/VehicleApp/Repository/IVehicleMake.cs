using Service;
using System;
using System.Collections.Generic;
using System.Text;

 
namespace Repository
{
   public interface IVehicleMake
    {
        VehicleMake MakeVehicle(int id, string name,string abbr);
        
    }
}
