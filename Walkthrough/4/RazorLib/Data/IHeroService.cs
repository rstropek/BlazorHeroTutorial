using System.Collections.Generic;
using System.Threading.Tasks;
using HeroesModel;

namespace HeroesCore
{
    public interface IHeroService
    {
        List<Hero> GetHeroes();
    }
}