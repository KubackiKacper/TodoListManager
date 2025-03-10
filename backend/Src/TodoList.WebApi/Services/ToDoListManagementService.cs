using Microsoft.AspNetCore.Mvc;
using TodoList.WebApi.Data;
using TodoList.WebApi.Models;
using TodoList.WebApi.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace TodoList.WebApi.Services

{
    public class ToDoService : IToDoService
    {
        private readonly ApplicationDbContext _context;
        public ToDoService(ApplicationDbContext context) 
        {
            _context = context;
        }
        
        public async Task<GetToDoListAssignmentDTO[]> GetAll()
        {
            GetToDoListAssignmentDTO[] response = await _context.ToDoListAssignments.Select(toDo => new GetToDoListAssignmentDTO
            {
                Id = toDo.Id,
                Description= toDo.Description
            }).ToArrayAsync();
            return response;
        }
        public async Task <ToDoListAssignmentDTO> AddToDo(ToDoListAssignmentDTO toDoListAssignmentDTO)
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
        public async Task <bool> DeleteToDo(int id) 
        {
            ToDoListAssignment removeToDo = await _context.ToDoListAssignments.FindAsync(id);
            if (removeToDo==null)
            {
                throw new NullReferenceException($"Could not find id: {id} ");
            }
            _context.ToDoListAssignments.Remove(removeToDo);
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<ToDoListAssignmentDTO> UpdateToDo(int id, ToDoListAssignmentDTO toDoDto)
        {
            ToDoListAssignment existingNote = await _context.ToDoListAssignments.FindAsync(id);
            if (existingNote == null)
            {
                throw new NullReferenceException($"Could not find id: {id} ");
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
