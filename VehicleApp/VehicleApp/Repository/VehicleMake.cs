
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    //Id,MakeId,Name,Abrv

        
   public class VehicleMake: IVehicleMake
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public VehicleMake(int id, string name, string abbreviation)
        {
            Id = id;
            Name = name;
            Abbreviation = abbreviation;
        }
        public VehicleMake()
        {

        }
        public VehicleMake MakeVehicle(int id,  string name, string abbr)
        {
           
            return new VehicleMake(id,name,abbr);
        }
    }


}
