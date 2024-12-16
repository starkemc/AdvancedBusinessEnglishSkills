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
    public int TopMenu { get; set; }
    public int? SubMenuId { get; set; }
    public int OrderBy { get; set; }
    public ImageSource Image { get; set; }
   
}
