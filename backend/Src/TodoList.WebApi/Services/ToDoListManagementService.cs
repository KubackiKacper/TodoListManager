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
        
        public List<ToDoListAssignment> GetAllAssignemnts()
        {
            List<ToDoListAssignment> response = _context.ToDoListAssignments.Select(toDo => new ToDoListAssignment
            {
                Id = toDo.Id,
                Description= toDo.Description
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
        public bool DeleteAssignmentFromToDo(int id) 
        {
            ToDoListAssignment? removeToDo = _context.ToDoListAssignments.Find(id);
            if (removeToDo == null)
            {
                return false;
            }
            _context.ToDoListAssignments.Remove(removeToDo);
            _context.SaveChanges();
            return true;
        }

        public async Task<ToDoListAssignmentDTO?> UpdateToDoAssignment(int id, ToDoListAssignmentDTO toDoDto)
        {
            var existingNote = await _context.ToDoListAssignments.FindAsync(id);

            if (existingNote == null)
            {
                return null;
            }

            existingNote.Description = toDoDto.Description;
            await _context.SaveChangesAsync();

            return new ToDoListAssignmentDTO
            {
                Description = existingNote.Description
            };
        }
    }
}
