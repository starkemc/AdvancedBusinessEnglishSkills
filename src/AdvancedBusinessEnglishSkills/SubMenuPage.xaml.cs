using AdvancedBusinessEnglishSkills.Data;
using AdvancedBusinessEnglishSkills.Models;
using System.Collections.ObjectModel;

namespace AdvancedBusinessEnglishSkills;

public partial class SubMenuPage : ContentPage
{
    public ObservableCollection<Menu> MenuItems = new();
    DBContext _dbContext = new();
	public int _subMenuId;

	public SubMenuPage(int subMenuId, string _menuText)
	{
		InitializeComponent();

		_subMenuId = subMenuId;

        (this.FindByName("MenuName") as Label).Text = _menuText;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        MenuItems.Clear();

        var subMenus =  await _dbContext.Menu_GetBySubMenuIdAsync(_subMenuId);

        subMenus.ForEach(item => {

            if (item.TopMenu == 0)
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

        await Navigation.PushAsync(new DetailPage(menuItem.Id));       
    }
}