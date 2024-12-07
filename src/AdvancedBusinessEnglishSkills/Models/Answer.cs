using System;
using SQLite;

namespace AdvancedBusinessEnglishSkills.Models;

public class Answer
{
    [PrimaryKey]
    public int Id { get; set; }
    public int MenuId { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; }
    public int IsCorrect { get; set; }
}
