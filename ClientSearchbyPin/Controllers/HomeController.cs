using ClientSearchbyPin.DB;
using ClientSearchbyPin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClientSearchbyPin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OracleDb db = new OracleDb();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string pinCode)
        {
            MainViewModel model = new MainViewModel();
            model.mushteriMelumat = db.GetClientByPinCode(pinCode);
            model.mushteriHesabMelumat = db.GetAccountsByPinCode(pinCode);

            return View("ClientResult", model);
        }

        public IActionResult ClientResult()
        {


            return View();
        }

        public IActionResult ClientHesab(string M_QEYDN)
        {
            MainViewModel model = new MainViewModel();
            model.mushteriHesabMelumat = db.GetAccountsByPinCode(M_QEYDN);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}