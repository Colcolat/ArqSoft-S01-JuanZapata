using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Http;    
using System.IO;                    
using System;                       
using System.Threading.Tasks;       
using System.Linq;
using System.Collections.Generic;
using SillyCats.Models;

namespace SillyCats.Controllers;

public class CatalogController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    
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
        },
        new Item
        {
            Id = 3,
            Name = "Brother Calm",
            Breed = "Domestic Shorthair",
            Gender = "Unnecessary, he has no earthly desires",
            Personality = "Has no need for a personality, for his inner eye sees all.",
            Description = "Brother Calm (淡定哥) is a mysterious being (possibly a cat) who is known to have appeared only once on camera. His feeder is unknown and speculated to be shut down. He will most likely not be seen again. Brother Calm is known for a viral clip of him sitting patiently at the feeder with his eyes closed. This is the only footage of him available. This behaviour is known as the Brother Calm pose in the community. Many cats can be seen imitating said pose.",
            ImageUrl = "/images/creatures/300px-Brother-calm-mr-calm.gif"
        },
        new Item
        {
            Id = 4,
            Name = "The Gluttonous Beast",
            Breed = "Domestic Shorthair",
            Gender = "Male",
            Personality = "Eating for extended periods of time, the destroyer of the kibble. ",
            Description = "The Gluttonous Beast (also called Mr. Glutton or Mr. Speed) is an orange and white ticked tabby, whose distinct trait is his pure hunger due to being observed eating kibbles with an average of 2-3 munches per second (averaging to 150 munches per minute). He is the fastest and longest lasting kibble destroyer to be spotted in Mr. Fresh's Feeder.",
            ImageUrl = "/images/creatures/Tgb_screaming_in_pain.png"
        },
        new  Item
        {
            Id = 5,
            Name = "Mr. Shock",
            Breed = "Domestic Shorthair",
            Gender = "Male",
            Personality = "Unfamiliar with Technology, Dumb, Stupid, Easily shocked",
            Description = "Mr. Shock (懵逼弟), is a cat from House of Compassion by the Strait. He got his name from the iconic clip of him having a very shocked expression when he noticed food being dispensed. He repeated this action as more food was dispensed (with a less shocked expression), making his actions resemble that of a livestreamer reading their stream's live chat. He is often confused with Mr. Fresh despite looking nothing like him.",
            ImageUrl = "/images/creatures/300px-Mr.Shock-ezgif.com-optimize.gif"
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