using Domain.Lab120240821;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lab20240821.port.Out
{
    public interface IRentalCarRepository
    {
        IEnumerable<IVehicle> GetAllCars();
        bool AddCar(IVehicle car);
        bool RemoveCar(IVehicle car);
        bool UpdateCar(IVehicle car);
    }
}
