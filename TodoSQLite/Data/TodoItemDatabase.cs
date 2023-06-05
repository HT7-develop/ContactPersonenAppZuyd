using SQLite;
using TodoSQLite.Models;

namespace TodoSQLite.Data;

public class TodoItemDatabase
{
    SQLiteAsyncConnection Database;
    public TodoItemDatabase()
    {
    }
    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Persoon>();
    }

    public async Task<List<Persoon>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<Persoon>().ToListAsync();
    }

/*    public async Task<List<TodoItem>> GetItemsNotDoneAsync()
    {
        await Init();
        return await Database.Table<TodoItem>().Where(t => t.Done).ToListAsync();
        
        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }*/

    public async Task<Persoon> GetItemAsync(int id)
    {
        await Init();
        return await Database.Table<Persoon>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Persoon item)
    {
        await Init();
        if (item.ID != 0)
        {
            return await Database.UpdateAsync(item);
        }
        else
        {
            return await Database.InsertAsync(item);
        }
    }

    public async Task<int> DeleteItemAsync(Persoon item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }

    public async Task<List<Persoon>> GetItemsBySearchAsync(string search)
    {
        return await Database.Table<Persoon>().Where(p =>
            p.VoorNaam.ToLower().Contains(search.ToLower()) ||
            p.AchterNaam.ToLower().Contains(search.ToLower()))
            .ToListAsync();
    }

}