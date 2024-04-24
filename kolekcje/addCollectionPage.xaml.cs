namespace kolekcje;

public partial class addCollectionPage : ContentPage
{
	public addCollectionPage()
	{
		InitializeComponent();
	}

    private void addCollection(object sender, EventArgs e)
    {

        string collectionName = collectionNameEntry.Text;

        string folderPath = Path.Combine(FileSystem.Current.AppDataDirectory, "collectionOrganizer");

        if (Directory.Exists(folderPath) == false)
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, collectionName + ".txt");

        using (StreamWriter writer = File.CreateText(filePath))

            DisplayAlert("Sukces", "Kolekcja zosta³a dodana pomyœlnie.", "OK");
        ClearForm();

        Navigation.PopAsync();
    }
    private void ClearForm()
    {
        collectionNameEntry.Text = string.Empty;
    }
}