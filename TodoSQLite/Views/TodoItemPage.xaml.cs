using Microsoft.Maui.Graphics;
using TodoSQLite.Data;
using TodoSQLite.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace TodoSQLite.Views;

[QueryProperty("Item", "Item")]
public partial class TodoItemPage : ContentPage
{
	Persoon item;

	public Persoon Item
	{
		get => BindingContext as Persoon;
		set => BindingContext = value;
	}
    TodoItemDatabase database;
    public TodoItemPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.VoorNaam))
        {
            await DisplayAlert("Naam Benodigd", "Vul een voornaam in alsjeblieft.", "OK");
            return;
        }

        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Item.ID == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    async void CallButton_Clicked(object sender, EventArgs e)
    {
        if (await this.DisplayAlert(

            "Telefoonnummer Bellen",
            "Wilt u "+ Item.VoorNaam +" "+ Item.AchterNaam +" bellen?",
            "Ja",
            "Nee")

            )
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(Item.TelefoonNummer);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Foutmelding Telefoonnummer", "Het gekozen telefoonnummer is niet geldig.", "OK");
            }
            catch (Exception)
            {
                // Other error has occurred.
                await DisplayAlert("Overige Foutmelding", "Bellen via de telefoon is mislukt, probeer opnieuw.", "OK");
            }
        }
    }

    private async void PhotoButton_Clicked(object sender, EventArgs e)
    {

        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                // save the file into the database
                
                // ... Marcel vragen (hoe afbeelding opslaan in database)

            }
        }

    }

    private async void GallaryPhotoButton_Clicked(object sender, EventArgs e)
    {
        // ... Marcel vragen (hoe afbeelding opslaan in database)

        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
            return;

        var stream = await result.OpenReadAsync();

        ImageSource image = ImageSource.FromStream(() => stream);

        LblAfbeelding.Source = image; //dit werk niet.
    }
}