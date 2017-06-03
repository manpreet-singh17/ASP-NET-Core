using Microsoft.AspNetCore.Mvc;
using OdeToCode.Models;
using OdeToCode.Services;

namespace OdeToCode.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }
        public IActionResult Index()
        {
            //List<string> List = new List<string>
            //{
            //    "Manpreet",
            //    "Singh"
            //};
            //return "Hello from controller";
            //return new ObjectResult(List);

            Restaurant model = new Restaurant()
            {
                ID = 1,
                Name = "Goodness Gracious"
            };
            return View(_restaurantData.GetAll());
        }
    }
}
