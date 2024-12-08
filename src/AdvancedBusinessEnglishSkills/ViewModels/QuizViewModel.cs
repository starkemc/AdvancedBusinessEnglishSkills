using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AdvancedBusinessEnglishSkills.Data;

namespace AdvancedBusinessEnglishSkills.ViewModels;

public class QuizViewModel : BaseViewModel
{
    private DBContext _dbContext = new();
    private int _menuId;

    public ObservableCollection<Models.Question> Data { get; set; } = new();

    private List<Models.Question> _questions = new();

    bool showCorrect;
    bool showIncorrect;

    public Command<string> ItemTapped { get; }
    public Command<string> NextQuestion { get; set; }

    public QuizViewModel(int menuId)
    {
        _menuId = menuId;

        ItemTapped = new Command<string>(OnItemSelected);
        NextQuestion = new Command<string>(OnNextQuestion);

        showCorrect = false;
        showIncorrect = false;
    }

    void OnItemSelected(string id)
    {
        //get current question
        //var question = Data[0] as Models.Question;

        //if (id.Equals(question.CorrectId))
        //{
        //    ShowCorrect = true;
        //    ShowIncorrect = false;
        //}
        //else
        //{
        //    ShowIncorrect = true;
        //    ShowCorrect = false;
        //}
    }

    void OnNextQuestion(string questionId)
    {
        Data.Clear();

        int numberOfQuestions = _questions.Count;
        int nextQuestion = Convert.ToInt32(questionId);

        if (nextQuestion >= numberOfQuestions)
        {
            //start over, go back to the first question
            Data.Add(_questions[0]);
        }
        else
        {
            //go to next question
            Data.Add(_questions[nextQuestion]);
        }

        ShowCorrect = false;
        ShowIncorrect = false;
    }

    public bool ShowCorrect
    {
        get => this.showCorrect;
        set => SetProperty(ref this.showCorrect, value);
    }

    public bool ShowIncorrect
    {
        get => this.showIncorrect;
        set => SetProperty(ref this.showIncorrect, value);
    }

    public async Task LoadData()
    {
        _questions = await _dbContext.Question_GetByMenuId(_menuId);
        var answers = await _dbContext.Answers_GetByMenuId(_menuId);

        _questions.ForEach(question => 
        {
            question.Answers = answers.Where(q => q.QuestionId == question.Id).ToList();
        });

        //set the first question
        Data.Add(_questions[0]);
    }
}
