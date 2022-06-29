using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero {
                    Id = 2 ,
                    Name = "Substitute Soul Reaper",
                    FirstName = "Ichigo",
                    LastName = "Kurosaki" ,
                    Place = "Karakura Town"
                }
            };

        public DataContext Context { get; }

        public SuperHeroController(DataContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await Context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = (await Context.SuperHeroes.FindAsync(id));
            if (hero == null)
                return BadRequest("Hero doesn't exist");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            Context.SuperHeroes.Add(hero);
            await Context.SaveChangesAsync();
            return Ok(await Context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = (await Context.SuperHeroes.FindAsync(request.Id));
            if (dbHero == null)
                return BadRequest("Hero doesn't exist");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await Context.SaveChangesAsync();
           
            return Ok(await Context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = (await Context.SuperHeroes.FindAsync(id));
            if (dbHero == null)
                return BadRequest("Hero doesn't exist");
            Context.SuperHeroes.Remove(dbHero);
            await Context.SaveChangesAsync();

            return Ok(await Context.SuperHeroes.ToListAsync());
        }
    }
}
