using AdvancedBusinessEnglishSkills.ViewModels;

namespace AdvancedBusinessEnglishSkills;

public partial class Quiz : ContentView
{
    private int _menuId;
    private QuizViewModel _quizViewModel;

    public Quiz() 
    {
        InitializeComponent();
    }
    

    public int MenuId
    {
        set
        {
            //BindingContext = new QuizViewModel(value);
            _menuId = value;
        }

    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        if (this.Parent != null) // The ContentView is now in the visual tree
        {
            var quizViewModel = new QuizViewModel(_menuId);
            BindingContext = quizViewModel;
            await quizViewModel.LoadData();
        }

    }
}