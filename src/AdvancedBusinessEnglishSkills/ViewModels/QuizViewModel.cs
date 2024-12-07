using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AdvancedBusinessEnglishSkills.Data;

namespace AdvancedBusinessEnglishSkills.ViewModels;

public class QuizViewModel : BaseViewModel
{
    private DBContext _dbContext = new();
    private int _menuId;

    public QuizViewModel(int menuId)
    {
        _menuId = menuId;

        //db lookups
        //var questions = _dbContext.Question_GetByMenuId(menuId);

    }

    public async Task LoadData()
    {
        var questions = await _dbContext.Question_GetByMenuId(_menuId);
        var answers = await _dbContext.Answers_GetByMenuId(_menuId);

        questions.ForEach(question => 
        {
            question.Answers = answers.Where(q => q.QuestionId == question.Id).ToList();
        });
    }
}
