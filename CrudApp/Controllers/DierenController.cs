using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Cache;

namespace CrudApp.Controllers
{
    public class DierenController : Controller
    {
        private static int DierenId = 0;
        private static List<Dieren> dieren = new List<Dieren>()
        {
            new Dieren()
            {
                Id = 0,
                Age = 1,
                Name = "Pasha",
                Species = "Kat",
                Vaccinated = true,
            }
        };

        private Dieren LookForDier(int id)
        {
            return dieren.FirstOrDefault(d => d.Id == id);
        }

        private Dieren AddNewDier(Dieren dier)
        {
            DierenId++;
            dier.Id = DierenId;
            dier.BirthYear = DateTime.Now.Year - dier.Age;
            dieren.Add(dier);
            return dier;
        }

        private void RemoveDier(int id)
        {
            Dieren toBeDeleted = LookForDier(id);
            if (toBeDeleted != null)
            {
                dieren.Remove(toBeDeleted);
            }
        }

        public IActionResult Index()
        {
            return View(dieren);
        }

        public IActionResult Details(int id)
        {
            var dier = LookForDier(id);
            if (dier == null)
            {
                return NotFound();
            }
            return View(dier);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Dieren dier)
        {
            if (ModelState.IsValid)
            {
                AddNewDier(dier);
                return RedirectToAction("Index");
            }
            return View(dier);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dier = LookForDier(id);
            if (dier == null)
            {
                return NotFound();
            }
            return View(dier);
        }

        [HttpPost]
        public IActionResult Edit(int id, Dieren updatedDier)
        {
            var dier = LookForDier(id);
            if (dier == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                dier.Species = updatedDier.Species;
                dier.Name = updatedDier.Name;
                dier.Age = updatedDier.Age;
                return RedirectToAction("Index");
            }
            return View(dier);
        }

        public IActionResult Delete(int id)
        {
            var dier = LookForDier(id);
            if (dier == null)
            {
                return NotFound();
            }

            RemoveDier(id);
            Console.WriteLine($"Dier met ID {id} Verwijderd."); 
            return RedirectToAction("Index");
        }
    }
}
