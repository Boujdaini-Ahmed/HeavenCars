using HeavenCars.DataAccessLayer.Models.Cars;
using System.Collections.Generic;

namespace HeavenCars.DataAccessLayer.Repositories.Cars
{
    public interface ICarRepository
    {
        Car GetCar(int Id);
        IEnumerable<Car> GetAllCars();
        Car Add(Car car);
        Car Update(Car carNew);
        Car Delete(Car car);

        List<Car> SearchCars(string search);
    }
}
