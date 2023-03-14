using Microsoft.EntityFrameworkCore;

namespace Timetablebot.Database
{
    public class User
    {
        public User(string chatId)
        {
            ChatId = chatId;
            MessagePriority = 0;
            IsMethodist = false;
            IsHeadman = false;
            IsEditing = false;
            IsUserConfirmed = false;
        }

        public int Id { get; set; }
        public string ChatId { get; set; }
        public int Course { get; set; }
        public string? StudGroup { get; set; }
        public string? CurrentMessage { get; set; }
        public int MessagePriority { get; set; }
        public string? CurrentSubject { get; set; }
        public bool IsMethodist { get; set; }
        public bool IsHeadman { get; set; }
        public string? EnteredLogin { get; set; }
        public string? EditingType { get; set; }
        public int EditingTypePriority { get; set; }
        public bool IsEditing { get; set; }
        public string? RecoveringType { get; set; }
        public bool IsRecovering { get; set; }
        public bool IsUserConfirmed { get; set; }

        public static User GetUser(string chatId)
        {
            using ApplicationContext db = new();

            var user = db.Users.FromSqlRaw($"SELECT * FROM Users WHERE ChatId LIKE {chatId}").ToList();//если вдруг перестанет работать запрос, взять в одинарные ковычки то что после лайк

            if(user.Count == 0)
            {
                User newUser = new(chatId);
                db.Users.Add(newUser);
                db.SaveChanges();
                return newUser;
            }

            return user.First();
        }

        public static void UpdateUser(User currentUser)
        {
            using ApplicationContext db = new();
            db.Users.Update(currentUser);
            db.SaveChanges();
        }
    }
}
