using Microsoft.AspNetCore.Mvc;
using TodoList.WebApi.DataTransferObjects;
using TodoList.WebApi.Models;
namespace TodoList.WebApi.Services
{
    public interface IToDoService
    {
        Task <GetToDoListAssignmentDTO[]> GetAll();
        Task <ToDoListAssignmentDTO> AddToDo(ToDoListAssignmentDTO toDoDto);
        Task <ToDoListAssignmentDTO> UpdateToDo(int id, ToDoListAssignmentDTO toDoDto);
        Task <bool> DeleteToDo(int id);
    }
}
