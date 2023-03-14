using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Courses;
using Timetablebot.Admin.SignIn;

namespace Timetablebot.Menu.Start
{
    public class StartMenu
    {
        public static ReplyKeyboardMarkup GetStartMenu()
        {
            ReplyKeyboardMarkup startKeyboard = new(new[] { new KeyboardButton[] { "Студент", "Методист" }, })
            {
                ResizeKeyboard = true
            };
            return startKeyboard;
        }

        public static async Task StudentClick(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, на каком курсе ты обучаешься?", replyMarkup: CoursesMenu.GetStudentCoursesMenu());
        }

        public static async Task MethodistClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            await Password.ChangePasswordAdvice(botClient, message, currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, выбери нужный курс.", replyMarkup: CoursesMenu.GetMethodistCoursesMenu());
        }
    }
}
