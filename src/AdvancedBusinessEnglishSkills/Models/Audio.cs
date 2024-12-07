using SQLite;
using System;

namespace AdvancedBusinessEnglishSkills.Models;

public class Audio
{
    [PrimaryKey]
    public int Id { get; set; }
    public int Menuid { get; set; }
    public int AudioTypeId { get; set; }
    public string Name { get; set; }    
}
