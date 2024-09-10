using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2VSIXProject.ClassesDef
{
    /// <summary>
    /// 取得應用程式邏輯類別描述定義
    /// </summary>
    public class ApplicationClassDef
    {
        /// <summary>
        /// 取得應用程式邏輯介面描述定義
        /// </summary>
        public static string GetApplicationUseCaseInterfaceTemplate
        {
            get
            {
                return @"//using Domain.Lab120240821;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $(NAMESPACE_DEF)$.port.In
{
    /// <summary>
    /// RentalCar 租車服務的主要 UseCase Services
    /// </summary>
    public interface $(INTERFACE_NAME)$
    {
        /// <summary>
        /// 進行車輛租用作業
        /// </summary>
        /// <param name=""car""></param>
        /// <returns></returns>
        bool ToRentCar(IVehicle car);
    }
}
";
            }
        }

        public static string GetApplicationRepositoryInterfaceTemplate
        {
            get
            {
                return @"//using Domain.Lab120240821;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $(NAMESPACE_DEF)$.port.Out
{
    public interface $(INTERFACE_NAME)$
    {
        IEnumerable<IVehicle> GetAllCars();
        bool AddCar(IVehicle car);
        bool RemoveCar(IVehicle car);
        bool UpdateCar(IVehicle car);
    }
}
";
            }
        }

        public static string GetApplicationServiceTemplate
        {
            get
            {
                return @"$(USING)$
//using Domain.Lab120240821;

namespace $(NAMESPACE_DEF)$
{
    public class $(DOMAIN_CLASS)$: $(INTERFACE_NAME)$
    {
        private readonly IRentalCarRepository _rentalCarRepository;

        public $(DOMAIN_CLASS)$(IRentalCarRepository rentalCarRepository)
        {
            _rentalCarRepository = rentalCarRepository;
        }
        public IEnumerable<Car>? GetAllCars()
        {
            return _rentalCarRepository.GetAllCars() as IEnumerable<Car>;
        }

        public bool ToRentCar(IVehicle car)
        {
            Car mycar = new Car(car.Model) { CC = car.CC, Model = car.Model };

            return _rentalCarRepository.AddCar(car);
        }
    }
}
";
            }
        }
    }
}
