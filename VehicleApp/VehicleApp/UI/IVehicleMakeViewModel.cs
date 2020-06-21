using System.Threading.Tasks;
using System.Windows.Input;

namespace VehicleApp.UI
{
    public interface IVehicleMakeViewModel
    {
        Task<bool> CreateVehicleMake();
        Task<bool> DeleteVehicleMake(string name);
        void IsEditVehicleMake(int id);
        int ListCount();
        ICommand GetCommand();

        bool GetOrder();
        void SetOrder(bool order);
    }

}
