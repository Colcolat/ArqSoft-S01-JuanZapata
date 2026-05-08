using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting; // Necesario para IWebHostEnvironment
using Microsoft.AspNetCore.Http;    // Necesario para IFormFile
using System.IO;                    // Necesario para manejo de archivos y rutas
using System;                       // Necesario para Guid
using System.Threading.Tasks;       // Necesario para programación asíncrona (Task)
using System.Linq;
using System.Collections.Generic;
using SillyCats.Models;

namespace SillyCats.Controllers;

public class CatalogController : Controller
{
    // 1. Declaramos la variable para acceder al entorno (wwwroot)
    private readonly IWebHostEnvironment _webHostEnvironment;

    // 2. Inyectamos la dependencia en el constructor
    public CatalogController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    private static List<Item> _items = new()
    {
        new Item
        {
            Id = 1,
            Name = "Mr. Fresh",
            Breed = "Domestic Shorthair",
            Gender = "Neutered Male",
            Personality = "Patient, picky, untrusting",
            Description = "Mr. Fresh is a famous internet cat. He has risen to fame thanks to a video of him giving the camera a side-eye. He was named Mr. Fresh after his patient yet picky behavior regarding the food he will eat.",
            ImageUrl = "/images/creatures/Freshguygif.gif"
        },
        new Item
        {
            Id = 2,
            Name = "Stupid Idiot",
            Breed = "Domestic Shorthair",
            Gender = "Male",
            Personality = "Stupid & an Idiot",
            Description = "He gained notoriety for an incident in which his feeder completely flooded with water. During this time, Stupid Idiot was unable to eat from it. A human tried to offer him 10 hotdogs, as well as some cat food, but Stupid Idiot rejected every one.",
            ImageUrl = "/images/creatures/Idiot.png"
        }
    };

    public IActionResult Index(String? Name)
    {
        var result = string.IsNullOrEmpty(Name)
            ? _items
            : _items.Where(i => i.Name.ToUpper() == Name.ToUpper()).ToList(); // Para ahorrar complicaciones
        
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
    public async Task<IActionResult> Add(Item item, IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "creatures");
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            
            item.ImageUrl = "/images/creatures/" + uniqueFileName;
        }
        else
        {
            item.ImageUrl = "/images/creatures/default.png";
        }
        
        item.Id = _items.Count > 0 ? _items.Max(i => i.Id) + 1 : 1; 
        
        _items.Add(item);
        
        return RedirectToAction("Index");
    }
}