//using Directives

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BestRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace BestRestaurant.Controllers //Define NameSpace
{
  public class RestaurantsController : Controller // Instantiate Restaurant controller class
  {
    private readonly BestRestaurantContext _db; // Declares private and readonly field of type BestRestaurantContext

    public RestaurantsController(BestRestaurantContext db) // Constructor allowing us ot set new _db property to our BestRestaurantContext
    {
      _db = db;
    }

    public ActionResult Index() // Method that routes to the Index view for our restaurants.
    {
      List<Restaurant> model = _db.Restaurants.Include(restaurant => restaurant.Cuisine).ToList(); //Accessing All restaurants in our database.
      ViewBag.PageTitle = "View All Restaurants";
      return View(model);
    }

    public ActionResult Create() // This method GETS properties from Cuisine
    {
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Name"); // Populating dropdown menu with cuisines
      return View();
    }

    [HttpPost] // This method POSTS new Restaurant info to our Database then redirects user to Views/Restaurants/Index
    public ActionResult Create(Restaurant restaurant)
    {
      _db.Restaurants.Add(restaurant); // Add new restaurant to restaurant database
      _db.SaveChanges(); // Save new restaurant to database
      return RedirectToAction("Index"); // Redirect user to restaurant index.cshtml file
    }

    public ActionResult Details(int id) // This method locates a specific restaurant by ID, then directs user to that restaurant's particular view
    {
      Restaurant thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    public ActionResult Edit(int id) // This method routes to a form, updating the restaurant info.
    {
      var thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id); // Finds specfic restaurant & sets that restaurant to thisRestaurant variable
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Name"); // Populating dropdown menu with cuisines
      return View(thisRestaurant);
    }

    [HttpPost] 
    public ActionResult Edit(Restaurant restaurant) // This method actually updates the restaurant.
    {
      _db.Entry(restaurant).State = EntityState.Modified; // Find and update all properties of the restaurant
      _db.SaveChanges(); // Saves the update
      return RedirectToAction("Index"); // Redirects to restaurant index.cshmtl
    }

    public ActionResult Delete(int id) // This method finds a particular restaurant by Id and routes to a view that deletes the restaurant
    {
      var thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id); // Finds specfic restaurant & sets that restaurant to thisRestaurant variable
      return View(thisRestaurant);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id) // This method actually deletes the restaurant.
    {
      var thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id); // Finds specfic restaurant & sets that restaurant to thisRestaurant variable
      _db.Restaurants.Remove(thisRestaurant); // This line removes the restaurant
      _db.SaveChanges(); // This line saves the changes
      return RedirectToAction("Index"); // This line redirects back to the restaurant index
    }
  }
}