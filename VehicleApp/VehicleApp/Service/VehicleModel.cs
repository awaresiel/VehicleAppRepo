
using SQLite;
using System.Diagnostics;

namespace Repository
{
    public class VehicleModel
    {
        public int dataBaseId { get; set; }
        public int Id {get;set;}
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string Abbreviation { get; set; }

        public VehicleModel(int id, int makeID, string makeName, string name, string abbreviation)
        {
            Id = id;
            MakeId = makeID;
            ModelName = name;
            Abbreviation = abbreviation;
            MakeName = makeName;
        }

        public VehicleModel() { }
    }
}
