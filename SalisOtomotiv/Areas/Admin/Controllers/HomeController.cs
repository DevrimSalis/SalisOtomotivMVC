using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalisOtomotiv.Context;
using SalisOtomotiv.Models;

namespace SalisOtomotiv.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cars =  _context.Cars
        .Include(c => c.Brand)
        .ToList();

            var carsWithBrandNames = cars.Select(c => new Car
            {
                Id = c.Id,
                CreatedDate = c.CreatedDate,
                Title = c.Title,
                Image = c.Image,
                ModelYear = c.ModelYear,
                Price = c.Price,
                BrandId = c.BrandId,
                Brand = new Brand { Name = c.Brand.Name },
            }).ToList();
            return View(cars);
        }


        // GET: Cars/Create
        //public IActionResult Create()
        //{
        //    ViewBag.BrandList = new SelectList(_context.Brands, "Id", "Name");
        //    ViewBag.ColorList = new SelectList(_context.Colors, "Id", "Name");
        //    ViewBag.TransmissionList = new SelectList(_context.Transmissions, "Id", "Name");
        //    ViewBag.DoorList = new SelectList(_context.Doors, "Id", "Name");
        //    ViewBag.FuelTypeList = new SelectList(_context.FuelTypes, "Id", "Name");
        //    ViewBag.BodyTypeList = new SelectList(_context.BodyTypes, "Id", "Name");
        //    ViewBag.ConditionList = new SelectList(_context.Conditions, "Id", "Name");

        //    return View();
        //}

        //// POST: Cars/Create
        //[HttpPost]
        //public async Task<IActionResult> Create([Bind("Id,CreatedDate,Title,Image,ModelYear,KM,Price,EnginePower,Description,EngineSize,BrandId,ColorId,TransmissionId,DoorId,FuelTypeId,BodyTypeId,ConditionId")] Car car)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(car);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewBag.BrandList = new SelectList(_context.Brands, "Id", "Name");
        //    ViewBag.ColorList = new SelectList(_context.Colors, "Id", "Name");
        //    ViewBag.TransmissionList = new SelectList(_context.Transmissions, "Id", "Name");
        //    ViewBag.DoorList = new SelectList(_context.Doors, "Id", "Name");
        //    ViewBag.FuelTypeList = new SelectList(_context.FuelTypes, "Id", "Name");
        //    ViewBag.BodyTypeList = new SelectList(_context.BodyTypes, "Id", "Name");
        //    ViewBag.ConditionList = new SelectList(_context.Conditions, "Id", "Name");

        //    return View(car);
        //}


        public IActionResult Create()
        {
            PopulateDropdownLists();
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,Title,Image,ModelYear,KM,Price,EnginePower,Description,EngineSize,BrandId,ColorId,TransmissionId,DoorId,FuelTypeId,BodyTypeId,ConditionId")] Car car, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                // Veritabanına aracı kaydetme işlemleri
                _context.Add(car);
                await _context.SaveChangesAsync();

                // Resim ekleme işlemi
                foreach (var image in images)
                {
                    // Resimleri kaydetmek için gerekli işlemleri gerçekleştirin (örneğin, disk üzerinde kaydetme veya bulut depolama hizmeti kullanma)

                    // Örnek olarak, resimleri disk üzerine kaydetme
                    var uniqueFileName = GetUniqueFileName(image.FileName);
                    var filePath = Path.Combine("uploads", uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    // Resimlerin dosya yollarını ve diğer bilgilerini veritabanına kaydetme işlemleri
                    // Örnek olarak, resim yollarını veritabanına kaydetme
                    var carImage = new CarImage
                    {
                        CarId = car.Id,
                        FilePath = filePath
                    };

                    // carImage nesnesini veritabanına kaydedin
                    _context.CarImages.Add(carImage);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdownLists();
            return View(car);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        private void PopulateDropdownLists()
        {
            ViewBag.BrandList = new SelectList(_context.Brands, "Id", "Name");
            ViewBag.ColorList = new SelectList(_context.Colors, "Id", "Name");
            ViewBag.TransmissionList = new SelectList(_context.Transmissions, "Id", "Name");
            ViewBag.DoorList = new SelectList(_context.Doors, "Id", "Name");
            ViewBag.FuelTypeList = new SelectList(_context.FuelTypes, "Id", "Name");
            ViewBag.BodyTypeList = new SelectList(_context.BodyTypes, "Id", "Name");
            ViewBag.ConditionList = new SelectList(_context.Conditions, "Id", "Name");
        }
    }
}
