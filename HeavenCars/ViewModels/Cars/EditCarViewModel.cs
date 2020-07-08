using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeavenCars.ViewModels.Cars
{
    public class EditCarViewModel : CreateCarViewModel
    {
        public int EditCarId { get; set; }
        public string ExistingPhotoCar { get; set; }
        public DateTime UpdateDate { get; set; }

       
    }
}
