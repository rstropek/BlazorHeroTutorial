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
        readonly IMessageService _messageService;
        readonly HttpClient _client;
        readonly string _baseAddress;
        public HeroService(IMessageService messageService, HttpClient client)
        {
            _messageService = messageService;
            _client = client;
            // Sets api address based on execution environment
            #if DOCKER
            _baseAddress = "http://webapi/api/Heroes";
            #else
            _baseAddress = "https://localhost:8001/api/Heroes";
            #endif
        }

        public async Task<List<Hero>> GetHeroesAsync()
        {
            var heroes = await _client.GetJsonAsync<List<Hero>>($"{_baseAddress}");
            _messageService.Add($"HeroService: Fetched {heroes.Count} Heroes");
            return heroes;
        }

        public async Task<Hero> GetHeroAsync(int id)
        {
            var hero = await _client.GetJsonAsync<Hero>($"{_baseAddress}/{id}");
            _messageService.Add($"HeroService: fetched hero #{hero.Id}");
            return hero;
        }

        public async Task UpdateHeroAsync(Hero hero)
        {
            _messageService.Add($"HeroService: Updated Hero #{hero.Id}");
            await _client.PutJsonAsync($"{_baseAddress}/{hero.Id}", hero);
        }

        public async Task DeleteHeroAsync(int id)
        {
            _messageService.Add($"HeroService: Deleted Hero #{id}");
            await _client.DeleteAsync($"{_baseAddress}/{id}");
        }

        public async Task AddHeroAsync(Hero hero)
        {
            _messageService.Add($"HeroService: Added new hero");
            await _client.PostJsonAsync($"{_baseAddress}", hero);
        }

        public async Task<List<Hero>> SearchHeroes(string term)
        {
            if (term == String.Empty)
                return new List<Hero>();
            var heroes = await _client.GetJsonAsync<List<Hero>>($"{_baseAddress}/search?searchTerm={term}");
            _messageService.Add($"HeroService: Found Heroes matching {term}");
            return heroes;
        }
    }
}