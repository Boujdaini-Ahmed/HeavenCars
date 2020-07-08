using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HeavenCars.DataAccessLayer.Models.Cars;
using HeavenCars.DataAccessLayer.Repositories.Cars;
using HeavenCars.ViewModels.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;


namespace HeavenCars.Controllers.Cars
{
    
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CarsController(ICarRepository carRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _carRepository = carRepository;
            _webHostEnvironment = webHostEnvironment;
        }
   
        public IActionResult ListCars(string search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var foundCars = _carRepository.SearchCars(search);
                return View(foundCars);
            }
            var cars = _carRepository.GetAllCars();
            return View(cars);
        }

      
        public ViewResult Details(int Id)

        {
            var car = _carRepository.GetCar(Id);

            if (car == null)
            {
                Response.StatusCode = 404;
                return View("CarNotFound", Id);
            }

            DetailCarViewModel detailCarViewModel = new DetailCarViewModel()
            {
                Car = car,
                PageTitle = "Car Detail"
            };
            return View(detailCarViewModel);
        }

        [Authorize]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateCarViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);

                Car newCar = new Car
                {

                    Name = model.Name,
                    MinLeeftijd = model.MinLeeftijd,
                    Prijs = model.Prijs,
                    Kw = model.Kw,
                    PhotoCar = uniqueFileName,
                    CreatedDate = DateTime.Now,

                };

                _carRepository.Add(newCar);
                return RedirectToAction("Details", "Cars", new { id = newCar.Id });
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Car car = _carRepository.GetCar(id);
            EditCarViewModel editCarViewModel = new EditCarViewModel
            {
                EditCarId = car.Id,
                Name = car.Name,
                MinLeeftijd = car.MinLeeftijd,
                Prijs = car.Prijs,
                Kw = car.Kw,
                ExistingPhotoCar = car.PhotoCar,
                UpdateDate = DateTime.Now


            };
            return View(editCarViewModel);
        }

        [Authorize]
        [HttpPost]

        public IActionResult Edit(EditCarViewModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = _carRepository.GetCar(model.Id);
                car.Name = model.Name;
                car.MinLeeftijd = model.MinLeeftijd;
                car.Prijs = model.Prijs;
                car.Kw = model.Kw;
                car.UpdateDate = DateTime.Now;


                if (model.Photo != null)
                {
                    if (model.ExistingPhotoCar != null)

                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath,
                            "images", model.ExistingPhotoCar);
                        System.IO.File.Delete(filePath);
                    }

                    car.PhotoCar = ProcessUploadFile(model);

                }

                _carRepository.Update(car);
                return RedirectToAction("ListCars", "Cars");
            }
            return View();

        }

        private string ProcessUploadFile(CreateCarViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }


        [Authorize]
        public IActionResult Delete(int Id)
        {
            var car = _carRepository.GetCar(Id);

            car.Delete = true;

            var response = _carRepository.Delete(car);



            if (response != null && response.Id != 0)
            {
                return RedirectToAction("ListCars");
            }

            return View("Delete", car);
        }
    }
    }

