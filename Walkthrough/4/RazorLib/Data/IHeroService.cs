using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesCore
{
    public interface IHeroService
    {
        List<Hero> GetHeroes();
    }
}