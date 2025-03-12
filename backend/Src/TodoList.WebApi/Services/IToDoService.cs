using Microsoft.AspNetCore.Mvc;
using TodoList.WebApi.DataTransferObjects;
using TodoList.WebApi.Models;
namespace TodoList.WebApi.Services
{
    public interface IToDoService
    {
        Task <ToDoListAssignmentDTO[]> GetAll();
        Task <ToDoListAssignmentDTO> AddToDo(SaveToDoListAssignmentDTO toDoDto);
        Task <SaveToDoListAssignmentDTO> UpdateToDo(int id, SaveToDoListAssignmentDTO toDoDto);
        Task <bool> DeleteToDo(int id);
    }
}
