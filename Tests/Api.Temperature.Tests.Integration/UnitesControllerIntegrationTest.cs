﻿using Api.Temeprature.Business.DTO;
using Api.Temeprature.IoC.Tests;
using Api.Temperature.Data.Context.Contract;
using Api.Temperature.Tests.Common.ScenarioDatas;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Api.Temperature.Tests.Integration
{
    public class UnitesControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>> , IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        protected readonly IMeteoFranceDBContext _context;
        public UnitesControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                {
                    services.ConfigureDBContextTest()
                    .ConfigureInjectionDependencyRepositoryTest()
                    .ConfigureInjectionDependencyServiceTest();
                })
            );

            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            _context = scopeFactory.ServiceProvider.GetService<IMeteoFranceDBContext>();

            _client = _factory.CreateClient();

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetUnites_ReturnAllUnites()
        {
            // Arrange
            _context.CreateUnites();

            // Act
            var unites = await _client.GetFromJsonAsync<List<UniteDTO>>("/api/Unites");

            // Assert
            Assert.NotNull(unites);
            Assert.NotEmpty(unites);
            Assert.Equal(2, unites.Count);

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
