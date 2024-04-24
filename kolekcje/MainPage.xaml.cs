using System.Diagnostics;

namespace kolekcje
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void onAddCollectionClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new addCollectionPage());
        }

        private void refreshCollectionList()
        {
            collectionStackLayout.Children.Clear();

            string folderPath = Path.Combine(FileSystem.Current.AppDataDirectory, "collectionOrganizer");

            if (Directory.Exists(folderPath)) 
            {
                var collectionFiles = Directory.GetFiles(folderPath,"*.txt");

                foreach (var collectionFile in collectionFiles) 
                {

                    var collectionName = Path.GetFileNameWithoutExtension(collectionFile);

                    var contentView = new ContentView();
                    var label = new Label { Text = collectionName };
                    label.StyleClass ??= new List<string>();
                    label.StyleClass.Add("collectionLabel");

                    var deleteButton = new Button { Text = "Usuń" };
                    deleteButton.StyleClass ??= new List<string>();
                    deleteButton.StyleClass.Add("delButton");

                    contentView.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => showCollectionItems(collectionFile))
                    });

                    deleteButton.Clicked += async (sender, e) =>
                    {
                        var result = await DisplayAlert("Potwierdzenie", $"Czy na pewno chcesz usunąć kolekcję {collectionName}?", "Tak", "Anuluj");
                        if (result)
                        {
                            File.Delete(collectionFile);
                            refreshCollectionList();
                        }
                    };

                    var stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(deleteButton);

                    contentView.Content = stackLayout;

                    collectionStackLayout.Children.Add(contentView);
                }
            }
        }

        private async void showCollectionItems(string filePath)
        {
            var collectionName = Path.GetFileNameWithoutExtension(filePath);

            var options = await DisplayActionSheet($"Opcje dla kolekcji {collectionName}", "Anuluj", null, "Wyświetl zawartość kolekcji", "Edytuj zawartość kolekcji");

            switch (options)
            {
                case "Wyświetl zawartość kolekcji":
                    string[] collectionItems = File.ReadAllLines(filePath);

                    var cListPage = new collectionItemsListPage(collectionName, collectionItems);

                    await Navigation.PushAsync(cListPage);
                    break;

                case "Edytuj zawartość kolekcji":
                    var eCollectionPage = new editCollectionItemsListPage(filePath);
                    await Navigation.PushAsync(eCollectionPage);
                    break;

                default:
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refreshCollectionList();
            Debug.WriteLine("Ścieżka do danych aplikacji: " + FileSystem.AppDataDirectory);
        }
    }

}
