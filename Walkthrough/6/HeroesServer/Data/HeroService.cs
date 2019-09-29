using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using HeroesModel;
using System.Text.Json;
using System.Text;

namespace HeroesCore
{
    public class HeroService : IHeroService
    {
        readonly IMessageService _messageService;
        readonly HttpClient _client;
        readonly string _baseAddress;
        public HeroService(IMessageService messageService, IHttpClientFactory clientFactory)
        {
            _messageService = messageService;
            _client = clientFactory.CreateClient();
            _baseAddress = "http://localhost:8000/api/Heroes";
        }

        public async Task<List<Hero>> GetHeroesAsync()
        {
            var response = await _client.GetAsync($"{_baseAddress}");
            var heroesJson = await response.Content.ReadAsStringAsync();
            var heroes = JsonSerializer.Deserialize<List<Hero>>(heroesJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            _messageService.Add($"HeroService: Fetched {heroes.Count} Heroes");
            return heroes;
        }

        public async Task<Hero> GetHeroAsync(int id)
        {
            var response = await _client.GetAsync($"{_baseAddress}/{id}");
            var heroJson = await response.Content.ReadAsStringAsync();
            var hero = JsonSerializer.Deserialize<Hero>(heroJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            _messageService.Add($"HeroService: fetched hero #{hero.Id}");
            return hero;
        }

        public async Task UpdateHeroAsync(Hero hero)
        {
            _messageService.Add($"HeroService: Updated Hero #{hero.Id}");
            var content = new StringContent(JsonSerializer.Serialize(hero), Encoding.UTF8, "application/json");
            await _client.PutAsync($"{_baseAddress}/{hero.Id}", content);
        }

        public async Task DeleteHeroAsync(int id)
        {
            _messageService.Add($"HeroService: Deleted Hero #{id}");
            await _client.DeleteAsync($"{_baseAddress}/{id}");
        }

        public async Task AddHeroAsync(Hero hero)
        {
            _messageService.Add($"HeroService: Added new hero");
            var content = new StringContent(JsonSerializer.Serialize(hero), Encoding.UTF8, "application/json");
            await _client.PostAsync($"{_baseAddress}", content);
        }

        public async Task<List<Hero>> SearchHeroes(string term)
        {
            if (string.IsNullOrEmpty(term))
                return new List<Hero>();

            var response = await _client.GetAsync($"{_baseAddress}/search?searchTerm={term}");
            var heroesJson = await response.Content.ReadAsStringAsync();
            var heroes = JsonSerializer.Deserialize<List<Hero>>(heroesJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            _messageService.Add($"HeroService: Found Heroes matching {term}");
            return heroes;
        }
    }
}