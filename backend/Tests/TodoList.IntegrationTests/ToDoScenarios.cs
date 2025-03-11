using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.WebApi.Data;

namespace TodoList.IntegrationTests
{
    
    public class ToDoScenarios: IClassFixture<ToDoWebApplicationFactory<Program>>
    {
        private readonly ToDoWebApplicationFactory<Program> _factory;
        
        public ToDoScenarios(ToDoWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
        }

        [Fact]
        public async Task GetAllToDos_ReturnResposneOk()
        {
            
            using HttpClient httpClient = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync("todo");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
