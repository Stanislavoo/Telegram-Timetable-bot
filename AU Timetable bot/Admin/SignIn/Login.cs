using Telegram.Bot.Types;
using Telegram.Bot;
using Timetablebot.Database;
using Timetablebot.Menu.Confirm;
using Timetablebot.Admin.RecoverAuthorizatoinData;

namespace Timetablebot.Admin.SignIn
{
    public class Login
    {
        public static async Task StartLoginingClick(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Чтобы прекратить вход нажми \"Вернуться в меню\".");
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи логин:", replyMarkup: RecoverData.GetRecoverLoginingDataMenu());
        }

        public static async Task GetLogin(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (IsLoginTrue(message.Text!, currentUser) == true)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Чтобы прекратить вход нажми \"Вернуться в меню\".");
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи пароль:", replyMarkup: RecoverData.GetRecoverPasswordMenu());
                return;
            }

            if (message.Text!.ToLower() == "восстановить логин" || message.Text!.ToLower() == "восстановить логин и пароль")//вынести в функцию
            {
                currentUser.IsRecovering = true;
                currentUser.RecoveringType = message.Text.ToLower();
                Database.User.UpdateUser(currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи шестизначный код восстановления:", replyMarkup: ConfirmMenu.GetCanselRecoveringButton());
                return;
            }

            if (message.Text!.ToLower() == "вернуться в меню")
            {
                RecoverData.BackToMenuClick(botClient, message, currentUser);
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: "Несуществующий логин. Попробуй еще раз или вернись в меню.", replyMarkup: RecoverData.GetRecoverLoginingDataMenu());
        }

        private static bool IsLoginTrue(string loginMessage, Database.User currentUser)
        {
            if (currentUser.CurrentMessage == "методист")
                return IsMethodistLoginTrue(loginMessage, currentUser);
            return IsHeadmanLoginTrue(loginMessage, currentUser);
        }

        private static bool IsMethodistLoginTrue(string loginMessage, Database.User currentUser)
        {
            var methodist = Methodist.GetCurentMethodist(loginMessage);

            if (methodist == null)
                return false;

            currentUser.EnteredLogin = methodist.Login;
            Database.User.UpdateUser(currentUser);
            return true;
        }

        private static bool IsHeadmanLoginTrue(string loginMessage, Database.User currentUser)
        {
            var headman = Headman.GetCurentHeadman(loginMessage);
            if (headman == null)
                return false;
            if (headman.Course == currentUser.Course && headman.StudGroup == currentUser.StudGroup)
            {
                currentUser.EnteredLogin = headman.Login;
                Database.User.UpdateUser(currentUser);
                return true;
            }
            return false;
        }
    }
}
