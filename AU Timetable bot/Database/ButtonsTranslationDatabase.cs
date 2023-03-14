using Microsoft.EntityFrameworkCore;

namespace Timetablebot.Database
{
    public class ButtonsTranslation
    {
        public int Id { get; set; }
        public string? RussianButtonName { get; set; }
        public string? EnglishButtonName { get; set; }

        public static string GetEnglishName(string pressedButtonName)
        {
            using ApplicationContext db = new();

            var findTranslation = db.ButtonsTranslation.FromSqlRaw($"SELECT * FROM ButtonsTranslation WHERE RussianButtonName LIKE '{pressedButtonName}'").ToList();
            return findTranslation.First().EnglishButtonName!;
        }
    }
}
