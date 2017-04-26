using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab_9.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        PetContext db;
        public PetsController(PetContext context)
        {
            this.db = context;
            if (!db.Pets.Any())
            {
                db.Pets.Add(new Pet { Name = "Patty", Breed = "Cat" });
                db.Pets.Add(new Pet { Name = "Sally", Breed = "Parrot" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Pet> Get()
        {
            return db.Pets.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Pet pet = db.Pets.FirstOrDefault(x => x.Id == id);
            if (pet == null)
                return NotFound();
            return new ObjectResult(pet);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Pet pet)
        {
            if (pet?.Name == null || pet?.Breed == null)
            {
                ModelState.AddModelError("", "Не указаны данные о петомце");
                return BadRequest();
            }
            if (pet.Name == "Hitler")
            {
                ModelState.AddModelError("Name", "Смените кличку животного");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Pets.Add(pet);
            db.SaveChanges();
            return Ok(pet);
        }

        [HttpPut]
        public IActionResult Put([FromBody]Pet pet)
        {
            if (pet?.Name == null || pet?.Breed == null)
            {
                ModelState.AddModelError("", "Не указаны данные о петомце");
                return BadRequest();
            }

            if (pet.Name.ToLower() == "hitler")
            {
                ModelState.AddModelError("Name", "Смените кличку животного");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!db.Pets.Any(x => x.Id == pet.Id))
            {
                return NotFound();
            }

            db.Update(pet);
            db.SaveChanges();
            return Ok(pet);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pet pet = db.Pets.FirstOrDefault(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            db.Pets.Remove(pet);
            db.SaveChanges();
            return Ok(pet);
        }


    }
}
