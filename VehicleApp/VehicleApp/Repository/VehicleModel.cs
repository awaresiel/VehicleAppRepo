using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
   public class VehicleModel:IVehicleModel
    {
        public int Id {get;set;}

        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public VehicleModel(int id, int makeID, string name, string abbreviation)
        {
            Id = id;
            MakeId = makeID;
            Name = name;
            Abbreviation = abbreviation;
        }

        public VehicleModel()
        {

        }

        public VehicleModel GetVehicleModel(int id, int mkId,string name, string abbreviation)
        {
            return new VehicleModel(id, mkId, name,abbreviation);
        }
    }
}
