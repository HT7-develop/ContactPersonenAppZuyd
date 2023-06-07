using SQLite;
using TodoSQLite.Models;
using Bogus;

namespace TodoSQLite.Data;

public class ContactsDatabase
{
    SQLiteAsyncConnection Database;

    public class PersonenDatabase
    {
        SQLiteAsyncConnection Database;

        public PersonenDatabase()
        {
            Init().Wait(); // Wait for initialization to complete before generating sample contacts
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Persoon>();

            // Check if the Persoon table is empty
            var contactsCount = await Database.Table<Persoon>().CountAsync();
            if (contactsCount == 0)
            {
                // Generate sample contacts
                GenerateSampleContacts();
            }
        }

        void GenerateSampleContacts()
        {
            // Set the random seed for consistent results
            var random = new Random(8675309);

            // Create a Faker instance for generating fake data
            var faker = new Faker("nl");

            // Generate a list of 20 people with fake mobile numbers
            var sampleContacts = new List<Persoon>();
            for (int i = 0; i < 20; i++)
            {
                var contact = new Persoon
                {
                    VoorNaam = faker.Name.FirstName(),
                    AchterNaam = faker.Name.LastName(),
                    Afbeelding = "",
                    TelefoonNummer = GenerateFakeMobileNumber(random),
                    Geslacht = faker.PickRandom("Male", "Female"),
                    IsMan = faker.Random.Bool(),
                    IsVrouw = faker.Random.Bool(),
                    IsOnbekend = faker.Random.Bool()
                };
                sampleContacts.Add(contact);
            }

            // Insert the sample contacts into the database
            Database.InsertAllAsync(sampleContacts);
        }

        public static string GenerateFakeMobileNumber(Random random)
        {
            // Generate a random mobile number starting with "06" followed by 8 random digits
            string mobileNumber = "06";
            for (int i = 0; i < 8; i++)
            {
                mobileNumber += random.Next(10);
            }
            return mobileNumber;
        }

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