using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HeroesModel;

namespace HeroesApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {
        [HttpGet]
        public IEnumerable<Hero> Heroes([FromQuery] string searchTerm)
        {
            if (searchTerm == null || searchTerm == String.Empty)
                return SampleData.Heroes;
            else
                return SampleData.Heroes.FindAll(h => h.Name.ToUpper().StartsWith(searchTerm.ToUpper()));
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Hero([FromRoute] int id)
        {
            var hero = SampleData.Heroes.FirstOrDefault(t => t.Id == id);
            return Ok(hero);
        }
        [HttpPut]
        [Route("{id}")]
        public void PutHero([FromBody] Hero hero)
        {
            var h = SampleData.Heroes.FirstOrDefault(t => t.Id == hero.Id);
            h.Name = hero.Name;
        }
        [HttpPost]
        public void PostHero([FromBody] Hero hero)
        {
            var newId = SampleData.Heroes.Max(h => h.Id) + 1;
            SampleData.Heroes.Add(new Hero(){
                Id = newId,
                Name = hero.Name
            });
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteHero([FromRoute] int id)
        {
            var indexToDelete = SampleData.Heroes.FindIndex(h => h.Id == id);
            SampleData.Heroes.RemoveAt(indexToDelete);
        }
    }
}