using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.Windows.Input;

namespace ContactPersonenApp
{
    public partial class NotePage : ContentPage
    {
        //lijst van test contacten
        // er is nog een probleem met image path verkrijgen voor photos die worden opgeslagen,
        // deze moeten in de resources map worden gedumpt eigenlijk.
        List<Contact> contactList = new List<Contact>()
        {
            new Contact()
            {
                Voornaam = "John",
                Achternaam = "Doe",
                Telefoonnummer = "123456789",
                ManChecked = true,
                VrouwChecked = false,
                OnbekendChecked = false,
                ContactImage = "Image/person.png"
            },
            new Contact()
                {
                    Voornaam = "Jane",
                    Achternaam = "Smith",
                    Telefoonnummer = "987654321",
                    ManChecked = false,
                    VrouwChecked = true,
                    OnbekendChecked = false,
                    ContactImage = "Image/woman.png"
                },
                new Contact()
                {
                    Voornaam = "Michael",
                    Achternaam = "Johnson",
                    Telefoonnummer = "555555555",
                    ManChecked = true,
                    VrouwChecked = false,
                    OnbekendChecked = false,
                    ContactImage = "contact2.jpg"
                },
                new Contact()
                {
                    Voornaam = "Emily",
                    Achternaam = "Brown",
                    Telefoonnummer = "111111111",
                    ManChecked = false,
                    VrouwChecked = true,
                    OnbekendChecked = false,
                    ContactImage = "contact3.jpg"
                },
                new Contact()
                {
                    Voornaam = "David",
                    Achternaam = "Wilson",
                    Telefoonnummer = "999999999",
                    ManChecked = true,
                    VrouwChecked = false,
                    OnbekendChecked = false,
                    ContactImage = "contact4.jpg"
                },
                new Contact()
                {
                    Voornaam = "Olivia",
                    Achternaam = "Miller",
                    Telefoonnummer = "222222222",
                    ManChecked = false,
                    VrouwChecked = true,
                    OnbekendChecked = false,
                    ContactImage = "contact5.jpg"
                },
                new Contact()
                {
                    Voornaam = "James",
                    Achternaam = "Taylor",
                    Telefoonnummer = "444444444",
                    ManChecked = true,
                    VrouwChecked = false,
                    OnbekendChecked = false,
                    ContactImage = "contact6.jpg"
                }
        };

        public Contact SelectedContact { get; set; }
        //hier word de lijst met contacten gebonden aan de xaml element
        //zodat de template de data voor de items heeft
        private List<Contact> LoadContacts()
        {
            ContactsListView.ItemsSource = contactList;
            return contactList;
        }

        public NotePage()
        {
            InitializeComponent();
            LoadContacts();
            BindingContext = this;
        }


        //hier word gewerkt met de json file ,die op het moment een verkeerde indeling heeft (object, array) 
        //private void LoadContacts()
        //{
        //    string json = LoadJsonFile();
        //    contactList = JsonConvert.DeserializeObject<List<Contact>>(json);
        //    ContactsListView.ItemsSource = contactList;
        //}

        // in deze functie wordt daadwerkelijke file gevonden op de telefoon
        private string LoadJsonFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.json");
            string json = File.ReadAllText(filePath);
            return json;
        }

        // functie voor onclick contact, navigeert naar singlecontactpage
        //public void ContactsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem is Contact selectedContact)
        //    {
        //        ContactsListView.SelectedItem = null;

        //        Navigation.PushAsync(new NieuwContactPage(selectedContact));
        //    }
        //}

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
