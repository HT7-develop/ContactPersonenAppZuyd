using Newtonsoft.Json;

namespace ContactPersonenApp
{
    public class Contact
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Telefoonnummer { get; set; }
        public bool ManChecked { get; set; }
        public bool VrouwChecked { get; set; }
        public bool OnbekendChecked { get; set; }
        public string ContactImage { get; set; }
    }


    public class ImageSourceConverterFactory : JsonConverter<ImageSource>
    {
        public override ImageSource ReadJson(JsonReader reader, Type objectType, ImageSource existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, ImageSource value, JsonSerializer serializer)
        {
            if (value is FileImageSource fileImageSource)
            {
                writer.WriteValue(fileImageSource.File);
            }
            else if (value is UriImageSource uriImageSource)
            {
                writer.WriteValue(uriImageSource.Uri.ToString());
            }
            else if (value is StreamImageSource streamImageSource)
            {
                var stream = streamImageSource.Stream.Invoke(CancellationToken.None).Result;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    writer.WriteValue(memoryStream.ToArray());
                }
            }
            else
            {
                writer.WriteNull();
            }
        }
    }

    public class ContactsManager
    {
        private List<Contact> contacts;
        private const string contactsFilePath = "contacts.json";

        public ContactsManager()
        {
            contacts = LoadContacts();
        }

        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
            SaveContacts();
        }

        private List<Contact> LoadContacts()
        {
            if (File.Exists(contactsFilePath))
            {
                string json = File.ReadAllText(contactsFilePath);
                return JsonConvert.DeserializeObject<List<Contact>>(json, new ImageSourceConverterFactory());
            }
            else
            {
                return new List<Contact>();
            }
        }

        private void SaveContacts()
        {
            string json = JsonConvert.SerializeObject(contacts, new ImageSourceConverterFactory());
            File.WriteAllText(contactsFilePath, json);
        }
    }

    public partial class NieuwContactPage : ContentPage
    {
        private ContactsManager contactsManager;

        public NieuwContactPage()
        {
            InitializeComponent();
            contactsManager = new ContactsManager();
        }
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(voornaamEditor.Text) ||
                string.IsNullOrEmpty(achternaamEditor.Text) ||
                string.IsNullOrEmpty(telefoonnummerEditor.Text))
            {
                DisplayAlert("Warning", "Vul voornaam, achternaam en telefoonnummer in om een nieuw contact aan te maken.", "OK");
            }
            else
            {
                // Create a new Contact object with the entered values
                Contact newContact = new Contact
                {
                    Voornaam = voornaamEditor.Text,
                    Achternaam = achternaamEditor.Text,
                    Telefoonnummer = telefoonnummerEditor.Text,
                    ManChecked = manCheckbox.IsChecked,
                    VrouwChecked = vrouwCheckbox.IsChecked,
                    OnbekendChecked = onbekendCheckbox.IsChecked,
                    ContactImage = contactImage.Source?.ToString() // Store the image source as a string
                };

                // Get the app-specific directory for storing the file
                string directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(directory, "contacts.json");

                try
                {
                    // Serialize the newContact object to JSON
                    string json = JsonConvert.SerializeObject(newContact);

                    // Write the JSON data to the file
                    File.WriteAllText(filePath, json);

                    // Display the success popup with the name of the new contact
                    DisplayAlert("Success", $"Contact saved: {newContact.Voornaam} {newContact.Achternaam}", "OK");

                    // Clear the editor fields and image after successful save
                    voornaamEditor.Text = string.Empty;
                    achternaamEditor.Text = string.Empty;
                    telefoonnummerEditor.Text = string.Empty;
                    manCheckbox.IsChecked = false;
                    vrouwCheckbox.IsChecked = false;
                    onbekendCheckbox.IsChecked = false;
                    contactImage.Source = null;
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", $"Failed to save contact: {ex.Message}", "OK");
                }
            }
        }


        private async void MaakFotoButton_Clicked(object sender, EventArgs e)
        {
            var photo = await MediaPicker.CapturePhotoAsync();

            contactImage.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
        }

        private async void KiesFotoButton_Clicked(object sender, EventArgs e)
        {
            var photo = await MediaPicker.PickPhotoAsync();

            contactImage.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
        }
    }
}
