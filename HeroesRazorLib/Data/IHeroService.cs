using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesCore
{
    public interface IHeroService
    {
        Task<List<Hero>> GetHeroesAsync();
        Task<Hero> GetHeroAsync(int id);
        Task UpdateHeroAsync(Hero hero);
        Task DeleteHeroAsync(int id);
        Task AddHeroAsync(Hero hero);
        Task<List<Hero>> SearchHeroes(string term);
    }
}