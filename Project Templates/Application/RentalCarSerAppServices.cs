using Application.Lab20240821.port.In;
using Application.Lab20240821.port.Out;
using Domain.Lab120240821;

namespace Application.Lab20240821
{
    public class RentalCarSerAppServices: IRentalCarUseCase
    {
        private readonly IRentalCarRepository _rentalCarRepository;

        public RentalCarSerAppServices(IRentalCarRepository rentalCarRepository)
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
