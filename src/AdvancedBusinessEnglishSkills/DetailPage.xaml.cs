using AdvancedBusinessEnglishSkills.Models;

namespace AdvancedBusinessEnglishSkills;

public partial class DetailPage : ContentPage
{
    private int _menuId = 0;

    public DetailPage(int menuId)
	{
        InitializeComponent();

        _menuId = menuId;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var listenControl = this.FindByName("listen") as Listen;
        listenControl.MenuId = _menuId;
    }
}