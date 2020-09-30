using System;
using SQLite;

namespace Repository
{
    //Id,MakeId,Name,Abrv
    public class VehicleMake
    {
        public int dataBaseId { get; set; }
        public int makeID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public VehicleMake(int id, string name, string abbreviation)
        {
            makeID = id;
            Name = name;
            Abbreviation = abbreviation;
        }
        public VehicleMake() { }

    }


}
