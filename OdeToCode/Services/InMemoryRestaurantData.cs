using OdeToCode.Models;
using System.Collections.Generic;
using System;

namespace OdeToCode.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> _restaurant = new List<Restaurant>();

        public InMemoryRestaurantData()
        {
            _restaurant = new List<Restaurant>()
            {
                new Restaurant(){ ID = 1, Name = "Goodness Gracious"},
                new Restaurant(){ ID = 2, Name = "Rishi Dabha" }
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurant;
        }
    }
}
