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
using TodoList.WebApi.Models;

namespace TodoList.IntegrationTests
{
    
    public class ToDoScenarios: IClassFixture<ToDoWebApplicationFactory<Program>>
    {
        private readonly ToDoWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        
        public ToDoScenarios(ToDoWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            using var scope = _factory.Services.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            this._httpClient = _factory.CreateClient();
            _context.ToDoListAssignments.RemoveRange(_context.ToDoListAssignments);
            _context.Database.ExecuteSqlRaw($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{nameof(_context.ToDoListAssignments)}';");
            _context.ToDoListAssignments.AddRange(
                [
                    new ToDoListAssignment { Id = 1, Description = "This is example ToDo task", CreatedDate= new DateTime(2025, 3, 11, 0, 0, 0), CompletionStatus = false},
                    new ToDoListAssignment { Id = 2, Description = "This is example ToDo task 2", CreatedDate = new DateTime(2025, 3, 12, 0, 0, 0), CompletionStatus = true }
                ]
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllToDos_ReturnResposneOkIfNotNull()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("todo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseTodos = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDoListAssignmentDTO[]>(await response.Content.ReadAsStringAsync());
            responseTodos.Should().HaveCount(2);
            responseTodos.Should().BeEquivalentTo(
                [
                    new ToDoListAssignment 
                    { 
                        Id = 1, 
                        Description = "This is example ToDo task", 
                        CreatedDate= new DateTime(2025, 3, 11, 0, 0, 0), 
                        CompletionStatus = false
                    },
                    new ToDoListAssignment 
                    { 
                        Id = 2, 
                        Description = "This is example ToDo task 2", 
                        CreatedDate = new DateTime(2025, 3, 12, 0, 0, 0), 
                        CompletionStatus = true }
                ]
            );
        }

        [Fact]
        public async Task CreateToDo_ReturnResponseOkIfCreated()
        {
            using var scope = _factory.Services.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var newToDo = new SaveToDoListAssignmentDTO
            {
                Description = "testDescription",
                CreatedDate = DateTime.Now,
                CompletionStatus = false
            };

            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(newToDo);
            var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("todo",content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var addedTodo = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDoListAssignmentDTO>(await response.Content.ReadAsStringAsync());
            addedTodo.Should().NotBeNull();
            addedTodo.Description.Should().Be("testDescription");
            addedTodo.Id.Should().Be(3);
            addedTodo.CompletionStatus.Should().Be(false);
            ToDoListAssignment expectedToDo = _context.ToDoListAssignments
                .FirstOrDefault(x => x.Id == addedTodo.Id)
                .Should()
                .NotBeNull()
                .And
                .BeOfType<ToDoListAssignment>()
                .Subject;
            
            expectedToDo.Description.Should().Be(addedTodo.Description);
            expectedToDo.CompletionStatus.Should().Be(addedTodo.CompletionStatus);
            expectedToDo.CreatedDate.Should().Be(addedTodo.CreatedDate);
        }

        [Fact]
        public async Task DeleteToDo_ReturnsOk()
        {
            using var scope = _factory.Services.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            ToDoListAssignment deleteToDo = _context.ToDoListAssignments
                .FirstOrDefault(t => t.Id == 2)
                .Should().NotBeNull()
                .And
                .BeOfType<ToDoListAssignment>()
                .Subject;
            HttpResponseMessage deleteResponse = await _httpClient.DeleteAsync($"todo/{deleteToDo.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            _context.ToDoListAssignments
                .FirstOrDefault(t => t.Id == 2)
                .Should().BeNull();
        }

        [Fact]
        public async Task UpdateToDo_ReturnsOk()
        {
            using var scope = _factory.Services.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            ToDoListAssignment updateToDo = _context.ToDoListAssignments
                .FirstOrDefault(t => t.Id == 1)
                .Should().NotBeNull()
                .And
                .BeOfType<ToDoListAssignment>()
                .Subject;

            updateToDo.Description = "This is description after update";
            var updatedSerializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(updateToDo);
            var updateContent = new StringContent(updatedSerializedObject, Encoding.UTF8, "application/json");
            HttpResponseMessage updateResponse = await _httpClient.PutAsync($"todo/{updateToDo.Id}", updateContent);
            updateResponse
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
           
            ToDoListAssignment afterUpdateobj = _context.ToDoListAssignments
                .FirstOrDefault(t => t.Id == 1)
                .Should().NotBeNull()
                .And
                .BeOfType<ToDoListAssignment>()
                .Subject;
            afterUpdateobj.Description.Should().Be("This is description after update");
        }
    }
}
