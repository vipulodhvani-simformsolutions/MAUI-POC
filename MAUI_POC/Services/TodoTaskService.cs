using MAUI_POC.Models;
using SQLite;

namespace MAUI_POC.Services
{
    public class TodoTaskService : ITodoTaskService, IAsyncDisposable
    {

        private const string DbName = "TodoTask.db3";
        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database =>
            (_connection ??= new SQLiteAsyncConnection(DbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

        private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
        }

        public async Task<List<TodoTask>> GetTasks(int currentPage,int pageSize)
        {
            await CreateTableIfNotExists<TodoTask>();
            int pageIndex = currentPage * pageSize;
            return await _connection.Table<TodoTask>().Skip(pageIndex).Take(pageSize).ToListAsync();
        }
        public async Task<List<TodoTask>> GetTasks()
        {
            await CreateTableIfNotExists<TodoTask>();
            return await _connection.Table<TodoTask>().ToListAsync();
        }

        public async Task<TodoTask> GetTask(int Id)
        {
            await CreateTableIfNotExists<TodoTask>();
            return await _connection.Table<TodoTask>().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> AddTask(TodoTask task)
        {
            await CreateTableIfNotExists<TodoTask>();
            return await _connection.InsertAsync(task);
        }

        public async Task<int> UpdateTask(TodoTask task)
        {
            await CreateTableIfNotExists<TodoTask>();
            return await _connection.UpdateAsync(task);
        }

        public async Task<int> DeleteTask(int Id)
        {
            var task = await GetTask(Id);
            return await _connection.DeleteAsync(task);
        }

        public async ValueTask DisposeAsync() => await _connection?.CloseAsync();
    }
}
