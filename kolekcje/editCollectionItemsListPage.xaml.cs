using kolekcje.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace kolekcje;

public partial class editCollectionItemsListPage : ContentPage
{
	private ObservableCollection<Collection> Collection { get; set; }
    private string filePath { get; set; }

    public editCollectionItemsListPage(string fPath)
	{
		InitializeComponent();
        filePath = fPath;
        Collection = new ObservableCollection<Collection>();

        string[] collectionItems = File.ReadAllLines(filePath);

        foreach (var item in collectionItems)
        {
            string[] itemVals = item.Split(",");
            Collection.Add(new Collection { ItemName = itemVals[0], ItemState = itemVals[1] });
        }

        collectionItemsListView.ItemsSource = Collection;

        updateCollectionItemIndex();
    }

    private async void addNewItem(object sender, EventArgs e)
    {
        string newItemName = newItemNameEntry.Text;
        string newItemState = newItemStateEntry.Text;

        if (string.IsNullOrWhiteSpace(newItemName) == false || string.IsNullOrWhiteSpace(newItemState) == false)
        {
            Collection.Add(new Collection { ItemName = newItemName,ItemState = newItemState });
            newItemNameEntry.Text = string.Empty;
            newItemStateEntry.Text = string.Empty;

            updateCollectionItemIndex();
        }
        else
        {
            await DisplayAlert("B≥πd", "Wprowadü wartoúci nowego przedmiotu", "OK");
        }
    }

    private async void editCollectionItem(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var collectionItem = (Collection)button.BindingContext;

        var collectionitemName = collectionItem.ItemName;
        var collectionItemState = collectionItem.ItemState;

        var newItemName = await DisplayPromptAsync("Edytuj nazwÍ przedmiotu", "Wprowadü nowπ nazwÍ:", initialValue: collectionitemName);
        var newItemState = await DisplayPromptAsync("Edytuj stan przedmiotu", "Wprowadü nowy stan:", initialValue: collectionItemState);
        if (string.IsNullOrWhiteSpace(newItemName) == false)
        {
            collectionItem.ItemName = newItemName;
            collectionItem.ItemState = newItemState;
        }
    }

    private void deleteCollectionItem(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var collectionItem = (Collection)button.BindingContext;
        Collection.Remove(collectionItem);

        updateCollectionItemIndex();
    }

    private void updateCollectionItemIndex()
    {
        for (int i = 0; i < Collection.Count; i++)
        {
            Collection[i].Id = i + 1;
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        File.WriteAllLines(filePath, Collection.Select(collectionItem => collectionItem.ItemName + ","+collectionItem.ItemState));
    }
}