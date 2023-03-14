using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Back;
using Timetablebot.Menu.Main.Statements;
using Timetablebot.Menu.Main.Subjects;
using Timetablebot.Menu.Edit;

namespace Timetablebot.Menu.Main
{
    public class MainMenu
    {
        public static ReplyKeyboardMarkup GetStudentAndMethodistMenu()
        {
            ReplyKeyboardMarkup studentAndMethodistMenu = new(new[]
            {
                new KeyboardButton[] { "Расписание", "Заявления", "Предметы" },
                new KeyboardButton[] { "Назад" }
            })
            {
                ResizeKeyboard = true
            };
            return studentAndMethodistMenu;
        }

        public static ReplyKeyboardMarkup GetHeadmanMenu()
        {
            ReplyKeyboardMarkup headmanMenu = new(new[]
            {
                new KeyboardButton[] { "Расписание", "Заявления", "Предметы" },
                new KeyboardButton[] { "Изменить логин", "Изменить пароль" },
                new KeyboardButton[] { "Назад" }
            })
            {
                ResizeKeyboard = true
            };
            return headmanMenu;
        }

        public static async Task TimetableClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.IsHeadman == true)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: GetTodayData(message) + await FileEdit.GetTextFromFile(currentUser), replyMarkup: EditMenu.GetFileEditButton());
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: GetTodayData(message) + await FileEdit.GetTextFromFile(currentUser), replyMarkup: BackMenu.GetBackButton());
        }

        public static async Task StatementsClick(ITelegramBotClient botClient, Message message)
        {
            if (message.Date.Month >= 2 && message.Date.Month <= 7)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, какой месяц тебя интересует?", replyMarkup: MonthMenu.GetEvenSemesterMonthMenu());
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, какой месяц тебя интересует?", replyMarkup: MonthMenu.GetOddSemesterMonthMenu());
        }

        public static async Task SubjectsClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, какой предмет тебя интересует?", replyMarkup: SubjectsButtons.GetMenuType(message.Date.Month, currentUser));
        }

        private static string GetTodayData(Message message)
        {
            string year = GetPresentYear(message);
            string month = GetPresentMonth(message);
            string day = GetPresentDay(message);
            string dayOfWeek = GetPresentDayOfWeek(message);
            return $"{message.Chat.FirstName}, сегодня " + dayOfWeek + ", " + day + "." + month + "." + year + ".\n";
        }

        private static string GetPresentYear(Message message)
        {
            return message.Date.Year.ToString(); 
        }

        private static string GetPresentMonth(Message message)
        {
            if (message.Date.Month < 10)
                return "0" + message.Date.Month.ToString();
            return message.Date.Month.ToString();
        }

        private static string GetPresentDay(Message message)
        {
            if (message.Date.Day < 10 && message.Date.Day > 0)
                return "0" + message.Date.Day.ToString();
            return message.Date.Day.ToString();
        }

        private static string GetPresentDayOfWeek(Message message)
        {
            var russian = new System.Globalization.CultureInfo("ru");
            return russian.DateTimeFormat.GetDayName(message.Date.DayOfWeek).ToString();
        }      
    }
}
