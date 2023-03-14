using Microsoft.EntityFrameworkCore;

namespace Timetablebot.Database
{
    public class Headman
    {
        public int Id { get; set; }
        public int Course { get; set; }
        public string? StudGroup { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? RecoveryCode { get; set; }
        public string? Salt { get; set; }
        public bool IsPasswordChanged { get; set; }

        public static Headman? GetCurentHeadman(string loginMessage)
        {
            using ApplicationContext db = new();

            var headman = db.HeadmansData.FromSqlRaw($"SELECT * FROM HeadmansData WHERE Login LIKE '{loginMessage}' COLLATE Latin1_General_CS_AS").ToList();
            if (headman.Count > 0)
                return headman.First();
            return null;
        }

        public static Headman? GetCurentHeadman(int course, string group)
        {
            using ApplicationContext db = new();

            var headman = db.HeadmansData.FromSqlRaw($"SELECT * FROM HeadmansData WHERE (Course LIKE '{course}') AND (StudGroup LIKE '{group}')").ToList();
            if (headman.Count > 0)
                return headman.First();
            return null;
        }

        public static void UpdateHeadman(Headman headman)
        {
            using ApplicationContext db = new();
            db.HeadmansData.Update(headman);
            db.SaveChanges();
        }
    }
}
