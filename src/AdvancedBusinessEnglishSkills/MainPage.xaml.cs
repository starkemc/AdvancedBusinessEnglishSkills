using AdvancedBusinessEnglishSkills.Data;
using AdvancedBusinessEnglishSkills.Models;
using System.Collections.ObjectModel;

namespace AdvancedBusinessEnglishSkills;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Menu> MenuItems = new();

    DBContext _database;

    public MainPage()
    {
        InitializeComponent();

        _database = new DBContext();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        MenuItems.Clear();

        var menu = await _database.Menu_GetAllAsync();

        menu.ForEach(item => {

            item.Image = item.Icon;
            MenuItems.Add(item);
        });

        collectionMenu.ItemsSource = MenuItems;
    }

    public async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var menuItem = (e.CurrentSelection.FirstOrDefault() as Models.Menu);
       
        await Navigation.PushAsync(new DetailPage(menuItem.Id));
    }
}
