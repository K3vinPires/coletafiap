using ColetaApi.Controllers;
using ColetaApi.Data.Contexts;
using ColetaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ColetaApi.Tests
{
    public class ColetaControllerTests : IClassFixture<WebApplicationFactory<ColetaApi.Startup>>
    {
        private readonly WebApplicationFactory<ColetaApi.Startup> _factory;

        public ColetaControllerTests(WebApplicationFactory<ColetaApi.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Coleta/Index")]
        [InlineData("/Coleta/Create")]
        [InlineData("/Coleta/Update/1")]
        [InlineData("/Coleta/Delete/1")]
        public async Task Get_EndpointsReturnSuccessAnd200Status(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
