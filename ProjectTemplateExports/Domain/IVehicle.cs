using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    /// <summary>
    /// 車輛介面
    /// </summary>
    public interface IVehicle
    {
        int CalculateRentalCost(int daysRented);
        TimeSpan ChoiseRentalTime(DateTime start, DateTime end);
        VehicleType GetVehicleType();
        ModelType Model { get; set; }
        string CC { get; set; }
    }
    /// <summary>
    /// 車輛類型
    /// </summary>
    public enum VehicleType { Car, RV }
    /// <summary>
    /// 車輛型號
    /// </summary>
    public enum ModelType { Toyota = 100, Lexus = 150 }
}
