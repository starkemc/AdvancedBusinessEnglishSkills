using SQLite;
using System;

namespace AdvancedBusinessEnglishSkills.Models;

public class Question
{
    [PrimaryKey]
    public int Id { get; set; }
    public int MenuId { get; set; }
    public string Text { get; set; }
    public int OrderBy { get; set; }

    public List<Answer> Answers { get; set; } = new();
}
