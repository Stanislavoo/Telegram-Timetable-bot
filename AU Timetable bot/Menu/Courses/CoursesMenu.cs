using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Groups;

namespace Timetablebot.Menu.Courses
{
    public class CoursesMenu
    {
        public static ReplyKeyboardMarkup GetStudentCoursesMenu()
        {
            ReplyKeyboardMarkup studentCoursesKeyborad = new(new[]
            {
                new KeyboardButton[]{"1 курс","2 курс"},
                new KeyboardButton[]{"3 курс","4 курс"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return studentCoursesKeyborad;
        }

        public static ReplyKeyboardMarkup GetMethodistCoursesMenu()
        {
            ReplyKeyboardMarkup methodistCoursesKeyborad = new(new[]
            {
                new KeyboardButton[]{"1 курс","2 курс"},
                new KeyboardButton[]{"3 курс","4 курс"},
                new KeyboardButton[]{"Изменить логин","Изменить пароль"},
                new KeyboardButton[]{"Очистить данные всех курсов", "Назад" }
            })
            {
                ResizeKeyboard = true
            };
            return methodistCoursesKeyborad;
        }

        public static async Task CourseClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            UpdateCource(message, currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, выбери свою группу.", replyMarkup: AllGroupsMenu.GetStudentGroupsMenu());
        }

        private static void UpdateCource(Message message, Database.User currentUser)
        {
            string courseNumber = message.Text!.Substring(0,1);
            currentUser.Course = int.Parse(courseNumber);
            Database.User.UpdateUser(currentUser);
        }
    }
}
