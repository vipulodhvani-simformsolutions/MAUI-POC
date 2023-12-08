using MAUI_POC.Models;

namespace MAUI_POC.Services
{
    public interface ITodoTaskService
    {
        Task<List<TodoTask>> GetTasks(int currentPage, int pageSize);
        Task<List<TodoTask>> GetTasks();
        Task<TodoTask> GetTask(int Id);
        Task<int> AddTask(TodoTask task);
        Task<int> UpdateTask(TodoTask task);
        Task<int> DeleteTask(int Id);
    }
}
