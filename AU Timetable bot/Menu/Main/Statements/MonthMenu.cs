using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Back;
using Timetablebot.Menu.Edit;

namespace Timetablebot.Menu.Main.Statements
{
    public class MonthMenu
    {
        public static ReplyKeyboardMarkup GetOddSemesterMonthMenu()
        {
            ReplyKeyboardMarkup oddSemesterMonthMenu = new(new[]
            {
                new KeyboardButton[]{"Сентябрь", "Октябрь"},
                new KeyboardButton[]{"Ноябрь", "Декабрь"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return oddSemesterMonthMenu;
        }

        public static ReplyKeyboardMarkup GetEvenSemesterMonthMenu()
        {
            ReplyKeyboardMarkup evenSemesterMonthMenu = new(new[]
            {
                new KeyboardButton[]{"Февраль", "Март"},
                new KeyboardButton[]{"Апрель", "Май"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return evenSemesterMonthMenu;
        }

        public static async Task MonthClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.IsHeadman == true)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: await FileEdit.GetTextFromFile(currentUser), replyMarkup: EditMenu.GetFileEditMenu());
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: await FileEdit.GetTextFromFile(currentUser), replyMarkup: BackMenu.GetBackMenu());
        }
    }
}
