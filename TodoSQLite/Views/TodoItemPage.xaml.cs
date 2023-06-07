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

        /*        if (Item.Geslacht != null)
                {
                    if (Item.Geslacht.ToString() == "Man")
                    {
                        radiobuttonman.IsChecked = true;
                    }
                    if (Item.Geslacht.ToString() == "Vrouw")
                    {
                        radiobuttonvrouw.IsChecked = true;
                    }
                }
                else
                {
                    radiobuttononbekend.IsChecked = true;
                }*/

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

        //await DisplayAlert("geslacht", $"{Item.Geslacht}", "OK");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (await this.DisplayAlert(

                "Verwijderen",
                "Weet u het zeker?",
                "Ja",
                "Nee")

            )
        {
            try
            {
                if (Item.ID == 0)
                    return;
                await database.DeleteItemAsync(Item);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception)
            {
                // Other error has occurred.
                await DisplayAlert("Foutmelding", "Er is iets misgegaan, probeer opnieuw.", "OK");
            }
        }

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

                var fileInfo = new FileInfo(photo?.FullPath);
                Item.Afbeelding = fileInfo.ToString();

            }
        }

    }

    private async void GallaryPhotoButton_Clicked(object sender, EventArgs e)
    {
        // ... Marcel vragen (hoe afbeelding opslaan in database)

        var selectphoto = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images
        });

        if (selectphoto == null)
            return;

        var stream = await selectphoto.OpenReadAsync();

        ImageSource image = ImageSource.FromStream(() => stream);

        var fileInfo = new FileInfo(selectphoto?.FullPath);
        Item.Afbeelding = fileInfo.ToString();

    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton button = sender as RadioButton;
        Item.Geslacht = button.Content.ToString();


        if (Item.IsMan)
        {
            radiobuttonman.IsChecked = true;
        }        
        else if (Item.IsVrouw)
        {
            radiobuttonvrouw.IsChecked = true;
        }          
        else if (Item.IsOnbekend)
        {
            radiobuttononbekend.IsChecked = true;
        }
            
    }

}