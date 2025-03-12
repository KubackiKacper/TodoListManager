using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public HttpClient _httpClient;
        public ToDoScenarios(ToDoWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            this._httpClient = _factory.CreateClient();
            
        }

        [Fact]
        public async Task GetAllToDos_ReturnResposneOkIfNotNull()
        {
            
            HttpResponseMessage response = await _httpClient.GetAsync("todo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateToDo_ReturnResponseOkIfCreated()
        {
            
            var newToDo = new ToDoListAssignmentDTO
            {
                Description = "testDescription",
                CreatedDate = new DateTime(2025, 3, 12, 0, 0, 0),
                CompletionStatus = true
            };

            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(newToDo);
            var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("todo",content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage getResponseAfterAdd = await _httpClient.GetAsync("todo");
            getResponseAfterAdd.StatusCode.Should().Be(HttpStatusCode.OK);

            var addedTodos = Newtonsoft.Json.JsonConvert.DeserializeObject<GetToDoListAssignmentDTO[]>(await getResponseAfterAdd.Content.ReadAsStringAsync());
            var addedTask = addedTodos.FirstOrDefault(t => t.Description == newToDo.Description);

            addedTask.Should().NotBeNull();
            addedTask.Description.Should().Be("testDescription");
            
        }

        [Fact]
        public async Task DeleteToDo_ReturnsOk()
        {
            
            var deleteToDo = new ToDoListAssignmentDTO
            {
                Description = "TestToDelete",
                CreatedDate = new DateTime(2025, 3, 13, 0, 0, 0),
                CompletionStatus = false
            };

            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(deleteToDo);
            var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            HttpResponseMessage createResponse = await _httpClient.PostAsync("todo", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage getResponse = await _httpClient.GetAsync("todo");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var todos = Newtonsoft.Json.JsonConvert.DeserializeObject<GetToDoListAssignmentDTO[]>(await getResponse.Content.ReadAsStringAsync());
            var createdToDo = todos.FirstOrDefault(t => t.Description == "TestToDelete");
            createdToDo.Should().NotBeNull();

            HttpResponseMessage deleteResponse = await _httpClient.DeleteAsync($"todo/{createdToDo.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage getAfterDeleteResponse = await _httpClient.GetAsync("todo");
            getAfterDeleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var todosAfterDelete = Newtonsoft.Json.JsonConvert.DeserializeObject<GetToDoListAssignmentDTO[]>(await getAfterDeleteResponse.Content.ReadAsStringAsync());
            todosAfterDelete.Any(t => t.Id == createdToDo.Id).Should().BeFalse();
        }

        [Fact]
        public async Task UpdateToDo_ReturnsOk()
        {

            var updateToDo = new ToDoListAssignmentDTO
            {
                Description = "TestToUpdate",
                CreatedDate = new DateTime(2025, 3, 13, 0, 0, 0),
                CompletionStatus = false
            };

            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(updateToDo);
            var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            HttpResponseMessage createResponse = await _httpClient.PostAsync("todo", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage getResponse = await _httpClient.GetAsync("todo");
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
            HttpResponseMessage updateResponse = await _httpClient.PutAsync($"todo/{createdToDo.Id}", updateContent);
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage getUpdatedResponse = await _httpClient.GetAsync("todo");
            getUpdatedResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedTodos = Newtonsoft.Json.JsonConvert.DeserializeObject<GetToDoListAssignmentDTO[]>(await getUpdatedResponse.Content.ReadAsStringAsync());
            var updatedTask = updatedTodos.FirstOrDefault(t => t.Id == createdToDo.Id);

            updatedTask.Should().NotBeNull();
            updatedTask.Description.Should().Be("Updated Description");
            updatedTask.CompletionStatus.Should().BeTrue();

        }
    }
}
