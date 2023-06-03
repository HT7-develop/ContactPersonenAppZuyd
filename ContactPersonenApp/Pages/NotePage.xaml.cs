using Newtonsoft.Json;

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
                ContactImage = "contact1.jpg"
            },
            new Contact()
            {
                Voornaam = "John",
                Achternaam = "Doe",
                Telefoonnummer = "123456789",
                ManChecked = true,
                VrouwChecked = false,
                OnbekendChecked = false,
                ContactImage = "contact1.jpg"
            },
            new Contact()
            {
                Voornaam = "John",
                Achternaam = "Doe",
                Telefoonnummer = "123456789",
                ManChecked = true,
                VrouwChecked = false,
                OnbekendChecked = false,
                ContactImage = "contact1.jpg"
            },
            new Contact()
            {
                Voornaam = "John",
                Achternaam = "Doe",
                Telefoonnummer = "123456789",
                ManChecked = true,
                VrouwChecked = false,
                OnbekendChecked = false,
                ContactImage = "contact1.jpg"
            },
            new Contact()
            {
                Voornaam = "John",
                Achternaam = "Doe",
                Telefoonnummer = "123456789",
                ManChecked = true,
                VrouwChecked = false,
                OnbekendChecked = false,
                ContactImage = "contact1.jpg"
            },
            new Contact()
            {
                Voornaam = "John",
                Achternaam = "Doe",
                Telefoonnummer = "123456789",
                ManChecked = true,
                VrouwChecked = false,
                OnbekendChecked = false,
                ContactImage = "contact1.jpg"
            },
            // Add more contacts here if needed
        };

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
    }
}
