using AdvancedBusinessEnglishSkills.Data;

namespace AdvancedBusinessEnglishSkills;

public partial class Listen : ContentView
{
    DBContext _database;

    public Listen()
	{
		InitializeComponent();

        _database = new DBContext();

        //var items = _database.Listen_GetByMenuId(MenuId);
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        if (this.Parent != null) // The ContentView is now in the visual tree
        {
            await LoadDataAsync();
        }
    }

    private async Task LoadDataAsync()
    {
        var items = await _database.Listen_GetByMenuId(MenuId);   
    }

    public int MenuId { get; set; }      
	
}