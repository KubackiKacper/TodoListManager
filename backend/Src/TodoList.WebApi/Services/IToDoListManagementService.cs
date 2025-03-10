using Microsoft.AspNetCore.Mvc;
using TodoList.WebApi.DataTransferObjects;
using TodoList.WebApi.Models;
namespace TodoList.WebApi.Services
{
    public interface IToDoListManagementService
    {
        List<ToDoListAssignment> GetAllAssignemnts();
        Task <ToDoListAssignmentDTO> AddNewToDoAssignment(ToDoListAssignmentDTO toDoDto);
        Task<ToDoListAssignmentDTO> UpdateToDoAssignment(int id, ToDoListAssignmentDTO toDoDto);
        bool DeleteAssignmentFromToDo(int id);
    }
}
