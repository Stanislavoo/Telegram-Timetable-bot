using Microsoft.EntityFrameworkCore;

namespace Timetablebot.Database
{
    public class Methodist
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? RecoveryCode { get; set; }
        public string? Salt { get; set; }
        public bool IsPasswordChanged { get; set; }

        public static Methodist? GetCurentMethodist(string enteredLogin)
        {
            using ApplicationContext db = new();

            var methodist = db.MethodistsData.FromSqlRaw($"SELECT * FROM MethodistsData WHERE Login LIKE '{enteredLogin}' COLLATE Latin1_General_CS_AS").ToList();
            if (methodist.Count > 0)
                return methodist.First();
            return null;
        }

        public static List<Methodist> GetMethodistsList()
        {
            using ApplicationContext db = new();
            var methodists = db.MethodistsData.FromSqlRaw($"SELECT * FROM MethodistsData").ToList();
            return methodists;
        }

        public static void UpdateMethodist(Methodist methodist)
        {
            using ApplicationContext db = new();
            db.MethodistsData.Update(methodist);
            db.SaveChanges();
        }
    }
}
