using System.Threading.Tasks;

namespace VehicleApp.UI
{
    public interface IVehicleModelViewModel
    {
        
        Task getVehicleModelList(string name);
        Task<bool> CreateVehicleModel();
        void IsEditVehicleModel(string name,int id);
        int ListCount();
        System.Windows.Input.ICommand GetCommand();
        bool GetOrder();
        void SetOrder(bool order);
        Task<string> DeleteVehicleModel(string name, int id, bool deleteAll);

    }
}
