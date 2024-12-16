using AdvancedBusinessEnglishSkills.Data;
using AdvancedBusinessEnglishSkills.Models;
using System.Collections.ObjectModel;

namespace AdvancedBusinessEnglishSkills;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Menu> MenuItems = new();

    private List<Models.Menu> _menus = new();

    DBContext _database;

    public MainPage()
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;

        _database = new DBContext();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        MenuItems.Clear();

        _menus = await _database.Menu_GetAllAsync();

        _menus.ForEach(item => {

            if (item.TopMenu == 1)
            {
                item.Image = item.Icon;
                MenuItems.Add(item);
            }            
        });

        collectionMenu.ItemsSource = MenuItems;
    }

    public async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //get the menu item
        var menuItem = (e.CurrentSelection.FirstOrDefault() as Models.Menu);
       
        //determine if its a submenu
        if(menuItem.TopMenu == 1 && menuItem.SubMenuId == null)
            await Navigation.PushAsync(new DetailPage(menuItem.Id));
        else
        {
            await Navigation.PushAsync(new SubMenuPage((int)menuItem.SubMenuId, menuItem.Name));
        }
    }
}
