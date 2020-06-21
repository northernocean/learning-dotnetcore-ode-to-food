﻿using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurants();
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant restaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {

        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant {Id = 1, Name = "Jazz Pizza", Location="Maryland", Cuisine=CuisineType.Italian},
                new Restaurant {Id = 2, Name = "Taj Mahal", Location="New York", Cuisine=CuisineType.Indian},
                new Restaurant {Id = 3, Name = "Senor Taco", Location="New Jersey", Cuisine=CuisineType.Mexican}
            };
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            return GetRestaurantsByName(null);
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var restaurantInDB = restaurants.SingleOrDefault(r => r.Id == restaurant.Id);
            if (restaurantInDB is null)
                return null;
            restaurantInDB.Name = restaurant.Name;
            restaurantInDB.Location = restaurant.Location;
            restaurantInDB.Cuisine = restaurant.Cuisine;
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
