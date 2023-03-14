using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Main;
using Timetablebot.Admin.SignIn;

namespace Timetablebot.Menu.Groups
{
    public class GroupMenu
    {
        public static ReplyKeyboardMarkup GetGroupMenu()
        {
            ReplyKeyboardMarkup groupKeyboard = new(new[]
            {
                new KeyboardButton[]{"Меню","Староста"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return groupKeyboard;
        }

        public static async Task MenuClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.IsHeadman == true && currentUser.IsMethodist == false)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, что тебя интересует?", replyMarkup: MainMenu.GetHeadmanMenu());
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, что тебя интересует?", replyMarkup: MainMenu.GetStudentAndMethodistMenu());
        }

        public static async Task HeadmanClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        { 
            if (currentUser.IsMethodist == true)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, что тебя интересует?", replyMarkup: MainMenu.GetStudentAndMethodistMenu());
                return;
            }
            await Password.ChangePasswordAdvice(botClient, message, currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, что тебя интересует?", replyMarkup: MainMenu.GetHeadmanMenu());
        }
    }
}
