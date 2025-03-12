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
        
        public async Task<ToDoListAssignmentDTO[]> GetAll()
        {
            ToDoListAssignmentDTO[] response = await _context.ToDoListAssignments.Select(toDo => new ToDoListAssignmentDTO
            {
                Id = toDo.Id,
                Description= toDo.Description,
                CreatedDate = toDo.CreatedDate,
                CompletionStatus = toDo.CompletionStatus,
            }).ToArrayAsync();
            return response;
        }
        public async Task<ToDoListAssignmentDTO> AddToDo(SaveToDoListAssignmentDTO saveToDoListAssignmentDTO )
        {
            ToDoListAssignment addToDo = new ToDoListAssignment
            {
                Description = saveToDoListAssignmentDTO.Description,
                CreatedDate = DateTime.Now,
                CompletionStatus = false
            };
            _context.ToDoListAssignments.Add(addToDo);
            await _context.SaveChangesAsync();

            return new ToDoListAssignmentDTO
            {
                Id= addToDo.Id,
                Description = addToDo.Description,
                CreatedDate = addToDo.CreatedDate,
                CompletionStatus = false
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

        public async Task<SaveToDoListAssignmentDTO> UpdateToDo(int id, SaveToDoListAssignmentDTO toDoDto)
        {
            ToDoListAssignment existingTask = await _context.ToDoListAssignments.FindAsync(id);
            if (existingTask == null)
            {
                throw new NullReferenceException($"Could not find id: {id} ");
            }
            existingTask.Description = toDoDto.Description;
            existingTask.CompletionStatus = toDoDto.CompletionStatus;
            await _context.SaveChangesAsync();

            return new SaveToDoListAssignmentDTO
            {
                Description = existingTask.Description,
                CompletionStatus = toDoDto.CompletionStatus
            };
        }
    }
}
