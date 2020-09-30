using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp.Repository
{
    [Table("VehicleMakeEntity")]
   public class VehicleMakeEntity
    {
        //Id,MakeId,Name,Abrv

        [PrimaryKey, AutoIncrement]
        public int dataBaseId { get; set; }
        public int makeID { get; set; }
        [Collation("NOCASE")]
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public VehicleMakeEntity(int id, string name, string abbreviation)
        {
            makeID = id;
            Name = name;
            Abbreviation = abbreviation;
        }
        public VehicleMakeEntity() { }

    }
}

