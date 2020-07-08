using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace HeavenCars.DataAccessLayer.Models.Cars
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string MinLeeftijd { get; set; }

        public string Prijs { get; set; }

        public string PhotoCar { get; set; }

        public string Kw { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Delete { get; set; }

        
    }
}
