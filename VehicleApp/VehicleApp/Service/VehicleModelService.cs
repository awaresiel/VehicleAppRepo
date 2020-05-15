using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Linq;

namespace Service
{
    public interface IVehicleModelService
    {
        Task<VehicleModelService> GetVehicleModelService(IVehicleModel s);

    }
    public class VehicleModelService : IVehicleModelService
    {
       async public Task<VehicleModelService> GetVehicleModelService(IVehicleModel s)
        {
            return await Task.FromResult(new VehicleModelService(s));
        }


        IVehicleModel iVehicleModel;
       public Dictionary<string, List<VehicleModel>> modelsList { get; private set; }

        public VehicleModelService(IVehicleModel iVehicleModel)
        {
            this.iVehicleModel = iVehicleModel;
            modelsList = new Dictionary<string, List<VehicleModel>>();

            // taken from https://nhts.ornl.gov/2009/pub/2009FARSMakeModel.pdf


            modelsList.Add("Acura", new List<VehicleModel> { new VehicleModel(031,54,"Integra","(ACUR)"),
                                                        new VehicleModel(032,54,"Legend","(ACUR)" ),
                                                        new VehicleModel(033,54,"NSX","(ACUR)" ),
                                                        new VehicleModel(034,54,"Vigor","(ACUR)" ),
                                                        new VehicleModel(035,54,"TL","(ACUR)" ),
            });
            modelsList.Add("Alfa Romeo", new List<VehicleModel> { new VehicleModel(031,31,"Spider","(ALFA)"),
                                                        new VehicleModel(032,31,"Sports Sedan","(ALFA)" ),
                                                        new VehicleModel(033,31,"Sprint/Special","(ALFA)" ),
                                                        new VehicleModel(034,31,"GTV-6","(ALFA)" ),
                                                        new VehicleModel(035,31,"Alpha 164","(ALFA)" ),
            });

            modelsList.Add("AM General", new List<VehicleModel> { new VehicleModel(401,03,"Dispatcher","(AMGN)"),
                                                        new VehicleModel(402,03,"Hummer","(AMGN)" ),
                                                        new VehicleModel(421,03,"Hummer(SUV)","(AMGN)" ),
                                                      
            });

            modelsList.Add("American Motors", new List<VehicleModel> { new VehicleModel(001,01,"Rambler","(AMER)"),
                                                        new VehicleModel(002,01,"Rebel/Matador/Marlin","(ALFA)" ),
                                                        new VehicleModel(003,01,"Pacer","(ALFA)" ),
                                                        new VehicleModel(004,01,"AMX","(ALFA)" ),
                                                       
            });

            modelsList.Add("Audi", new List<VehicleModel> { new VehicleModel(031,32,"Super90","(AUDI)"),
                                                        new VehicleModel(032,32,"100","(AUDI)" ),
                                                        new VehicleModel(033,32,"Fox","(AUDI)" ),
                                                        new VehicleModel(034,32,"4000","(AUDI)" ),

            });


        }

       

        public VehicleModel GetVehicleModel(int id, int makeID, string name, string abbreviation)
        {
            return iVehicleModel.GetVehicleModel(id,makeID,name,abbreviation);
        }

        public async Task<List<VehicleModel>> SortVehicleModelsASC_DESC(string nameOfModel,bool ascOrDesc)
        {
            List<VehicleModel> templist = modelsList[nameOfModel];

            if (ascOrDesc)
            {
                var tl = await Task.Run(() => templist.OrderBy(v => v.Name).ToList());
                return tl;
            }
            else
            {
                var tl = await Task.Run(() => templist.OrderByDescending(v1 => v1.Name).ToList());
                return tl;
            }

        }

        public async Task<List<VehicleModel>> GetVehicleModel(string name,int id)
        {

            var item = await Task.Run(() => modelsList[name].Any(v => v.Id == id));

            if (item)
            {
               var list = await Task.Run(() => modelsList[name].FindAll((v) => v.Id == id).ToList());

                return list;
            }

            return null;

        }

        public async Task<bool> DeleteVehicleModel(string name,int id)
        {
            int number = await Task.Run(() => modelsList[name].RemoveAll((v) => v.Id == id));
            if (number > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}
