using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace VehicleApp.UI
{
   public interface IViewModel<T>
    {
        public ICommand LoadItemsCommand { get; set; }
        public ICommand CreateVehicleCommand { get; set; }
        public ICommand OnItemClickedCommand { get; set; }
        public ICommand OnAddVehicleCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand OnMoreCommand { get; set; }
        public ICommand OnSortOrderCommand { get; set; }
    }
}
