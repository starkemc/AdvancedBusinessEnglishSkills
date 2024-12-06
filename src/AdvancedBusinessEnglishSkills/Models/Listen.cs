using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedBusinessEnglishSkills.Models;

public class Listen
{
    [PrimaryKey]
    public int Id { get; set; }
    public int MenuId { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }

}
