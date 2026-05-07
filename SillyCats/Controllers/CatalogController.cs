using Microsoft.AspNetCore.Mvc;
using SillyCats.Models;

namespace SillyCats.Controllers;

public class CatalogController : Controller
{
    private static List<Item> _items = new()
    {
        new Item
        {
            Id = 1,
            Name = "Mr. Fresh",
            Breed = "Domestic Shorthair",
            Gender = "Neutered Male",
            Personality = "Patient, picky, untrusting",
            Description =
                "Mr. Fresh is a famous internet cat. He has risen to fame thanks to a video of him giving the camera a side-eye. He was named Mr. Fresh after his patient yet picky behavior regarding the food he will eat."
        },
        new Item
        {
            Id = 2,
            Name = "Stupid Idiot",
            Breed = "Domestic Shorthair",
            Gender = "Male",
            Personality = "Stupid & an Idiot",
            Description =
                "He gained notoriety for an incident in which his feeder completely flooded with water. During this time, Stupid Idiot was unable to eat from it. A human tried to offer him 10 hotdogs, as well as some cat food, but Stupid Idiot rejected every one."
        }
    };

    public IActionResult Index(String? Name)
    {
        var result = string.IsNullOrEmpty(Name)
            ? _items
            : _items.Where(i => i.Name.ToUpper() == Name.ToUpper()).ToList(); //Para ahorrar complicaciones
        
        ViewBag.Names = _items.Select(it => it.Name).Distinct().ToList();
        ViewBag.CurrentName = Name;
        return View(result);
    }

    public IActionResult Details(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        return item == null ? View("NotFound") : View(item);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Item item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
        return RedirectToAction("Index");
    }

}