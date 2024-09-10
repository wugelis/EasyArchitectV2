using Domain.Lab120240821;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lab20240821.port.In
{
    /// <summary>
    /// RentalCar 租車服務的主要 UseCase Services
    /// </summary>
    public interface IRentalCarUseCase
    {
        /// <summary>
        /// 進行車輛租用作業
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        bool ToRentCar(IVehicle car);
    }
}
