using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp.Repository
{
    [Table("VehicleModelEntity")]
   public class VehicleModelEntity
    {
        [PrimaryKey,AutoIncrement]
        public int dataBaseId { get; set; }
        public int Id { get; set; }
        public int MakeId { get; set; }
        [Collation("NOCASE")]
        public string MakeName { get; set; }
        [Collation("NOCASE")]
        public string ModelName { get; set; }
        public string Abbreviation { get; set; }

        public VehicleModelEntity(int id, int makeID, string makeName, string name, string abbreviation)
        {
            Id = id;
            MakeId = makeID;
            ModelName = name;
            Abbreviation = abbreviation;
            MakeName = makeName;

        }

        public VehicleModelEntity() { }
    }
}
