using Microsoft.AspNetCore.Mvc;
using TodoList.WebApi.Data;
using TodoList.WebApi.Models;
using TodoList.WebApi.DataTransferObjects;

namespace TodoList.WebApi.Services

{
    public class ToDoListManagementService : IToDoListManagementService
    {
        private readonly ApplicationDbContext _context;
        public ToDoListManagementService(ApplicationDbContext context) 
        {
            _context = context;
        }
        
        public List<ToDoListAssignmentDTO> GetAllAssignemnts()
        {
            List<ToDoListAssignmentDTO> response = _context.ToDoListAssignments.Select(toDoDTO => new ToDoListAssignmentDTO
            {
                Id= toDoDTO.Id,
                Description= toDoDTO.Description
            }).ToList();
            return response;
        }
        public async Task <ToDoListAssignmentDTO> AddNewToDoAssignment(ToDoListAssignmentDTO toDoListAssignmentDTO)
        {
            ToDoListAssignment addToDo = new ToDoListAssignment
            {
                Description = toDoListAssignmentDTO.Description
            };
            _context.ToDoListAssignments.Add(addToDo);
            await _context.SaveChangesAsync();

            return new ToDoListAssignmentDTO
            {
                Description = addToDo.Description
            };
        }
    }
}
