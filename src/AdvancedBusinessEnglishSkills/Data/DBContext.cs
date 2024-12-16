using SQLite;
using System;
using AdvancedBusinessEnglishSkills.Models;

namespace AdvancedBusinessEnglishSkills.Data;

public class DBContext
{
    public SQLiteAsyncConnection _database;
    public const string fileName = "abe.db3";
    public static string DbPath { get; } = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

    public DBContext()
    {
        _database = new SQLiteAsyncConnection(DbPath);
        _database.CreateTableAsync<Menu>();
    }

    #region Menu

    public async Task<List<Menu>> Menu_GetAllAsync()
    {
        return await _database.Table<Menu>().ToListAsync();
    }

    public async Task<Menu> Menu_GetByIdAsync(int id)
    {
        return await _database.Table<Menu>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Menu>> Menu_GetBySubMenuIdAsync(int id)
    {
        return await _database.Table<Menu>().Where(d => d.SubMenuId == id).ToListAsync();
    }

    #endregion

    #region Listen

    public async Task<List<Models.Listen>> Listen_GetByMenuId(int id)
    {
        return await _database.Table<Models.Listen>().Where(d => d.MenuId == id).ToListAsync();
    }

    #endregion

    #region Audio

    public async Task<Models.Audio> Audio_GetListenByMenuId(int menuId)
    {
        return await _database.Table<Models.Audio>().Where(d => d.Menuid == menuId && d.AudioTypeId == 1).FirstAsync();
    }

    public async Task<Models.Audio> Audio_GetPhrasingByMenuId(int menuId)
    {
        return await _database.Table<Models.Audio>().Where(d => d.Menuid == menuId && d.AudioTypeId == 2).FirstAsync();
    }

    #endregion

    #region Questions

    public async Task<List<Models.Question>> Question_GetByMenuId(int id)
    {
        return await _database.Table<Models.Question>().Where(d => d.MenuId == id).ToListAsync();
    }

    #endregion

    #region Answers

    public async Task<List<Models.Answer>> Answers_GetByMenuId(int id)
    {
        return await _database.Table<Models.Answer>().Where(d => d.MenuId == id).ToListAsync();
    }


    #endregion

    #region Practice

    public async Task<List<Models.Practice>> Practice_GetByMenuId(int id)
    {
        return await _database.Table<Models.Practice>().Where(d => d.MenuId == id).ToListAsync();
    }

    public async Task<List<Models.PracticeDetail>> PracticeDetail_GetByPracticeId(int id)
    {
        return await _database.Table<Models.PracticeDetail>().Where(d => d.PracticeId == id).ToListAsync();
    }

    #endregion

    #region Phrasing

    public async Task<List<Models.Phrasing>> Phrasing_GetByMenuId(int id)
    {
        return await _database.Table<Models.Phrasing>().Where(d => d.MenuId == id).ToListAsync();
    }

    #endregion
}
