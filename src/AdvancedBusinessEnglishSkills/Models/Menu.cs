using SQLite;
using System;

namespace AdvancedBusinessEnglishSkills.Models;

public class Menu
{
    [PrimaryKey]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}
