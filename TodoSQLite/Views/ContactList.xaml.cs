using System.Collections.ObjectModel;
using ContactPersonenApp.Data;
using ContactPersonenApp.Models;

namespace ContactPersonenApp.Views;

public partial class ContactList : ContentPage
{
    ContactsDatabase database;
    public ObservableCollection<Persoon> Items { get; set; } = new();
    public ContactList(ContactsDatabase todoItemDatabase)
	{
		InitializeComponent();
        database = todoItemDatabase;
        BindingContext = this;

    }


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);

        });
    }
    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewContact), true, new Dictionary<string, object>
        {
            ["Item"] = new Persoon()

        });

    }

    private async void  CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Persoon item)
            return;

        await Shell.Current.GoToAsync(nameof(NewContact), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Geen zoekterm, toon alle items
            var items = await database.GetItemsAsync();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
        else
        {
            // Zoekterm aanwezig, filter de items op basis van de zoekterm
            var filteredItems = await database.GetItemsBySearchAsync(searchText);
            Items.Clear();
            foreach (var item in filteredItems)
            {
                Items.Add(item);
            }
        }
    }
}

