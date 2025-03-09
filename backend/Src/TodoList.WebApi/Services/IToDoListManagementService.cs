using Microsoft.AspNetCore.Mvc;
using TodoList.WebApi.DataTransferObjects;

namespace TodoList.WebApi.Services
{
    public interface IToDoListManagementService
    {
        List<ToDoListAssignmentDTO> GetAllAssignemnts();
        Task <ToDoListAssignmentDTO> AddNewToDoAssignment(ToDoListAssignmentDTO toDoDto);
        bool DeleteAssignmentFromToDo(int id);
    }
}
