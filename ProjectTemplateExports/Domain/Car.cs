
namespace $safeprojectname$
{
    public class Car : IVehicle
    {
        private ModelType _modelName;
		private string _cc;

		public ModelType Model { get => _modelName; set => value = _modelName; }
		public string CC { get => _cc; set => _cc = value; }
		public Car(ModelType modelName)
		{
			_modelName = modelName;
		}
		public int CalculateRentalCost(int daysRented)
		{
			return daysRented * (int)_modelName;
		}

		public TimeSpan ChoiseRentalTime(DateTime start, DateTime end)
		{
			return end - start;
		}

		public VehicleType GetVehicleType() => VehicleType.Car;
    }
}
