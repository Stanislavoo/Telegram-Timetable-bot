using Telegram.Bot;
using Telegram.Bot.Types;

namespace Timetablebot.Menu.Start
{
    public class StartCommands
    {
        public static async Task StartMessagingClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            SetMethodistToFalse(currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Привет, {message.Chat.FirstName}!\nКак я могу тебе помочь?", replyMarkup: StartMenu.GetStartMenu());
        }

        public static async Task BackToStartMenu(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            SetMethodistToFalse(currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, как я могу тебе помочь?", replyMarkup: StartMenu.GetStartMenu());
        }

        private static void SetMethodistToFalse(Database.User currentUser)
        {
            currentUser.IsMethodist = false;
            currentUser.EnteredLogin = null;
            currentUser.CurrentMessage = "startpage";
            Database.User.UpdateUser(currentUser);
        }
    }
}
