﻿using System;
using SQLite;

namespace AdvancedBusinessEnglishSkills.Models;

public class Practice
{
    [PrimaryKey]
    public int Id { get; set; }
    public int MenuId { get; set; }
    public string Name { get; set; }
    public string Audio { get; set; }
    public List<PracticeDetail> Items { get; set; } = new();
}

public class PracticeDetail
{
    [PrimaryKey]
    public int Id { get; set; }
    public int PracticeId { get; set; }
    public string Text { get; set; }
}