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


    public QuizViewModel()
    {
        ItemTapped = new Command<int>(OnItemSelected);
        NextQuestion = new Command<int>(OnNextQuestion);

        showCorrect = false;
        showIncorrect = false;
    }

    public Command<int> ItemTapped { get; }
    public Command<int> NextQuestion { get; set; }

    void OnItemSelected(int id)
    {
        //get current question
        var answer = Data[0].Answers.FirstOrDefault(a => a.Id == id);

        if (answer != null && answer.IsCorrect == 1)
        {
            ShowCorrect = true;
            ShowIncorrect = false;
        }
        else
        {
            ShowIncorrect = true;
            ShowCorrect = false;
        }
    }

    void OnNextQuestion(int questionId)
    {
        Data.Clear();

        int numberOfQuestions = _questions.Count;
        int nextQuestion = Convert.ToInt32(questionId);
        //int nextQuestion = 


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

    public async Task LoadData(int menuId)
    {
        _questions = await _dbContext.Question_GetByMenuId(menuId);
        var answers = await _dbContext.Answers_GetByMenuId(menuId);

        if(_questions.Any())
        {
            _questions.ForEach(question =>
            {
                question.Answers = answers.Where(q => q.QuestionId == question.Id).ToList();
            });

            //set the first question
            Data.Add(_questions[0]);
        }
    }


}
