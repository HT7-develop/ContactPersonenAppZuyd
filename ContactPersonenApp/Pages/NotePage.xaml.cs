using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.Windows.Input;
using System;

namespace ContactPersonenApp
{
    public partial class NotePage : ContentPage
    {
        //lijst van test contacten
        // er is nog een probleem met image path verkrijgen voor photos die worden opgeslagen,
        // deze moeten in de resources map worden gedumpt eigenlijk.
        List<Contact> contactList = new List<Contact>(){};

        private const string contactsFileName = "contactsNieuw.json";

        public Contact SelectedContact { get; set; }

        private string GetDownloadsFolderPath()
        {
            string downloadsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            downloadsFolderPath = Path.Combine(downloadsFolderPath, "Downloads");
            return downloadsFolderPath;
        }

        // navigeer naar nieuwcontact pagina
        async void navigateNewContact(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NieuwContactPage)
            );
        }

        //hier word de lijst met contacten gebonden aan de xaml element
        //zodat de template de data voor de items heeft
        private List<Contact> LoadContacts()
        {
            string json = LoadJsonFile();
            if (json != null)
            {
                contactList = JsonConvert.DeserializeObject<List<Contact>>(json);
            }
            ContactsListView.ItemsSource = contactList;
            return contactList;
        }


        public NotePage()
        {
            InitializeComponent();
            LoadContacts();
            BindingContext = this;
        }


        private string LoadJsonFile()
        {
            string downloadsFolderPath = GetDownloadsFolderPath();
            string filePath = Path.Combine(downloadsFolderPath, contactsFileName);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return json;
            }
            else
            {
                return null;
            }
        }

        private async void ContactsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Contact selectedContact)
            {
                ContactsListView.SelectedItem = null;

                await Navigation.PushAsync(new NieuwContactPage(selectedContact));
            }
        }

    }
}
