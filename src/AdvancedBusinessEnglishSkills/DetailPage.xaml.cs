using AdvancedBusinessEnglishSkills.Models;

namespace AdvancedBusinessEnglishSkills;

public partial class DetailPage : ContentPage
{
    private int _menuId = 0;
    private Listen _listen;
    private Phrasing _phrasing;
    private Practice _practice;

    public DetailPage(int menuId)
	{
        InitializeComponent();

        _menuId = menuId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _listen = new Listen(_menuId);
        await _listen.LoadDataAsync();
                
        var tabListening = this.FindByName("tabListening") as Syncfusion.Maui.TabView.SfTabItem;
        tabListening.Content = _listen;

        var quiz = new Quiz(_menuId);
        await quiz.LoadDataAsync();

        var tabQuiz = this.FindByName("tabQuiz") as Syncfusion.Maui.TabView.SfTabItem;
        tabQuiz.Content = quiz;

        _phrasing = new Phrasing(_menuId);
        await _phrasing.LoadDataAsync();

        var tabPhrasing = this.FindByName("tabPhrasing") as Syncfusion.Maui.TabView.SfTabItem;
        tabPhrasing.Content = _phrasing;

        _practice = new Practice(_menuId);
        await _practice.LoadDataAsync();
        var tabPractice = this.FindByName("tabPractice") as Syncfusion.Maui.TabView.SfTabItem;
        tabPractice.Content = _practice;



        //var listenControl = this.FindByName("listen") as Listen;
        //listenControl.MenuId = _menuId;

        //var quizControl = this.FindByName("quiz") as Quiz;
        //quizControl.MenuId = _menuId;

        //var practiceControl = this.FindByName("practice") as Practice;
        //practiceControl.MenuId = _menuId;

        //var phrasingControl = this.FindByName("phrasing") as Phrasing;
        //phrasingControl.MenuId = _menuId;
    }

    private void SfTabView_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
    {
        _listen.StopPlayer();
        _phrasing.StopPlayer();
        _practice.StopPlayer();
    }
}