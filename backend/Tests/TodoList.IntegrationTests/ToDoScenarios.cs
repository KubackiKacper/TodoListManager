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
using TodoList.WebApi.DataTransferObjects;

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
        public async Task GetAllToDos_ReturnResposneOkIfNotNull()
        {
            using HttpClient httpClient = _factory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync("todo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateToDo_ReturnResponseOkIfCreated()
        {
            using HttpClient httpClient = _factory.CreateClient();
            var newToDo = new ToDoListAssignmentDTO
            {
                Description = "testDescription",
                CreatedDate = new DateTime(2025, 3, 12, 0, 0, 0),
                CompletionStatus = true
            };

            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(newToDo);
            var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("todo",content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteToDo_ReturnsOk()
        {
            using HttpClient httpClient = _factory.CreateClient();
            var deleteToDo = new ToDoListAssignmentDTO
            {
                Description = "TestToDelete",
                CreatedDate = new DateTime(2025, 3, 13, 0, 0, 0),
                CompletionStatus = false
            };

            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(deleteToDo);
            var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            HttpResponseMessage createResponse = await httpClient.PostAsync("todo", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage getResponse = await httpClient.GetAsync("todo");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var todos = Newtonsoft.Json.JsonConvert.DeserializeObject<GetToDoListAssignmentDTO[]>(await getResponse.Content.ReadAsStringAsync());
            var createdToDo = todos.FirstOrDefault(t => t.Description == "TestToDelete");
            createdToDo.Should().NotBeNull();

            HttpResponseMessage deleteResponse = await httpClient.DeleteAsync($"todo/{createdToDo.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateToDo_ReturnsOk()
        {
            using HttpClient httpClient = _factory.CreateClient();

            var updateToDo = new ToDoListAssignmentDTO
            {
                Description = "TestToUpdate",
                CreatedDate = new DateTime(2025, 3, 13, 0, 0, 0),
                CompletionStatus = false
            };

            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(updateToDo);
            var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            HttpResponseMessage createResponse = await httpClient.PostAsync("todo", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage getResponse = await httpClient.GetAsync("todo");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var todos = Newtonsoft.Json.JsonConvert.DeserializeObject<GetToDoListAssignmentDTO[]>(await getResponse.Content.ReadAsStringAsync());
            var createdToDo = todos.FirstOrDefault(t => t.Description == "TestToUpdate");

            createdToDo.Should().NotBeNull();

            var updatedToDo = new ToDoListAssignmentDTO
            {
                Description = "Updated Description",
                CreatedDate = createdToDo.CreatedDate,
                CompletionStatus = true
            };

            var updatedSerializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(updatedToDo);
            var updateContent = new StringContent(updatedSerializedObject, Encoding.UTF8, "application/json");
            HttpResponseMessage updateResponse = await httpClient.PutAsync($"todo/{createdToDo.Id}", updateContent);
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        }
    }
}
