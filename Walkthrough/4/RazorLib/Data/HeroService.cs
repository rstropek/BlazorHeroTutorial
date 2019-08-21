using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using HeroesModel;

namespace HeroesCore
{
    public class HeroService : IHeroService
    {
        IMessageService _messageService;

        public HeroService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public List<Hero> GetHeroes()
        {
            var heroes = MockHeroes.Heroes;
            _messageService.Add($"HeroService: Fetched {heroes.Count} Heroes");
            return heroes;
        }
    }
}