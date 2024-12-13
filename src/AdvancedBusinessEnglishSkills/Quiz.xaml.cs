using AdvancedBusinessEnglishSkills.ViewModels;

namespace AdvancedBusinessEnglishSkills;

public partial class Quiz : ContentView
{
    private int _menuId;
    private QuizViewModel _quizViewModel = new();

    public Quiz(int menuId)
    {
        InitializeComponent();

        BindingContext = _quizViewModel;

        _menuId = menuId;

        //OnParentSet();
    }

    public async Task LoadDataAsync()
    {
        await _quizViewModel.LoadData(_menuId);
    }

    public Quiz() 
    {
        InitializeComponent();

        BindingContext = _quizViewModel;
    }
    

    public int MenuId
    {
        set
        {            
            _menuId = value;
        }

    }

    //protected override async void OnParentSet()
    //{
    //    base.OnParentSet();

    //    if (this.Parent != null) // The ContentView is now in the visual tree
    //    {
    //        await _quizViewModel.LoadData(_menuId);
    //    }

    //}
}