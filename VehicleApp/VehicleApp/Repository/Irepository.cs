using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Repository
{
   public interface Irepository<T>
    {
        public Task<List<T>> GetVehiclesAsync(bool ascOrDesc,string name);
        public Task<T> GetVehicleAsync(int id);
        public Task<int> SaveVehicleAsync(T vehicle);
        public Task<int> DeleteVehicleAsync(int id);
        public Task<int> UpdateVehiclesAsync(List<T> modelsList);
        public Task<int> DeleteVehiclesAsync(string makeName);
        public Task<int> DeleteVehicleAsyncWithSameName(string name);
    }
}
