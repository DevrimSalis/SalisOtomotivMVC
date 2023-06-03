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


        //GET: Cars/Create
        public IActionResult Create()
        {
            ViewBag.BrandList = new SelectList(_context.Brands, "Id", "Name");
            ViewBag.ColorList = new SelectList(_context.Colors, "Id", "Name");
            ViewBag.TransmissionList = new SelectList(_context.Transmissions, "Id", "Name");
            ViewBag.DoorList = new SelectList(_context.Doors, "Id", "Name");
            ViewBag.FuelTypeList = new SelectList(_context.FuelTypes, "Id", "Name");
            ViewBag.BodyTypeList = new SelectList(_context.BodyTypes, "Id", "Name");
            ViewBag.ConditionList = new SelectList(_context.Conditions, "Id", "Name");

            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,Title,Image,ModelYear,KM,Price,EnginePower,Description,EngineSize,BrandId,ColorId,TransmissionId,DoorId,FuelTypeId,BodyTypeId,ConditionId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.BrandList = new SelectList(_context.Brands, "Id", "Name");
            ViewBag.ColorList = new SelectList(_context.Colors, "Id", "Name");
            ViewBag.TransmissionList = new SelectList(_context.Transmissions, "Id", "Name");
            ViewBag.DoorList = new SelectList(_context.Doors, "Id", "Name");
            ViewBag.FuelTypeList = new SelectList(_context.FuelTypes, "Id", "Name");
            ViewBag.BodyTypeList = new SelectList(_context.BodyTypes, "Id", "Name");
            ViewBag.ConditionList = new SelectList(_context.Conditions, "Id", "Name");

            return View(car);
        }

    }
}
