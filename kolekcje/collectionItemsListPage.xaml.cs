namespace kolekcje;

public partial class collectionItemsListPage : ContentPage
{
    private Dictionary<string, CheckBox> checkBoxes = new Dictionary<string, CheckBox>();
    public collectionItemsListPage(string collectionName, string[] collectionItems)
	{
		InitializeComponent();
        Headbar.Text = "Kolekcja: "+collectionName;

        int itemIndex = 1;
        foreach (var item in collectionItems)
        {
            var itemsLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            string[] itemVals = item.Split(',');

            var idLabel = new Label { Text = itemIndex.ToString() };
            idLabel.StyleClass ??= new List<string>();
            idLabel.StyleClass.Add("collectionName");

            var nazwaLabel = new Label { Text = itemVals[0] };
            nazwaLabel.StyleClass ??= new List<string>();
            nazwaLabel.StyleClass.Add("collectionName");

            var itemStateLabel = new Label { Text = itemVals[1] };
            itemStateLabel.StyleClass ??= new List<string>();
            itemStateLabel.StyleClass.Add("collectionName");

            var checkBox = new CheckBox();
            checkBoxes.Add(item, checkBox);

            itemsLayout.Children.Add(idLabel);
            itemsLayout.Children.Add(nazwaLabel);
            itemsLayout.Children.Add(itemStateLabel);
            itemsLayout.Children.Add(checkBox);

            collectionItemsLayout.Children.Add(itemsLayout);

            itemIndex++;
        }
    }

    private void clearSelectedItems(object sender, EventArgs e)
    {
        foreach (var checkBox in checkBoxes.Values)
        {
            checkBox.IsChecked = false;
        }
    }
}