using Microsoft.EntityFrameworkCore;

namespace Timetablebot.Database
{
    public class ButtonsOrder
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string? MenuOrder { get; set; }

        public static string[] GetMenuName(int priority)
        {
            using ApplicationContext db = new();

            var findButton = db.ButtonsOrder.FromSqlRaw($"SELECT * FROM ButtonsOrder WHERE Priority LIKE '{priority}'").ToList();

            string[] findMenu = new string[findButton.Count];
            for (int i = 0; i < findButton.Count; i++)
            {
                findMenu[i] = findButton[i].MenuOrder!;
            }
            return findMenu;
        }
    }
}
