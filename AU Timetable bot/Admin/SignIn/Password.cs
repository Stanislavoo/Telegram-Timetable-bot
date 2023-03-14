using Telegram.Bot.Types;
using Telegram.Bot;
using Timetablebot.Database;
using Timetablebot.Menu.Groups;
using Timetablebot.Menu.Start;
using Timetablebot.Admin.RecoverAuthorizatoinData;
using Timetablebot.Menu.Confirm;

namespace Timetablebot.Admin.SignIn
{
    public class Password
    {
        public static async Task GetPassword(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (IsPasswordTrue(message.Text!, currentUser) == true)
            {
                if (currentUser.CurrentMessage == "методист")
                {
                    currentUser.IsMethodist = true;
                    Database.User.UpdateUser(currentUser);
                    await StartMenu.MethodistClick(botClient, message, currentUser);
                    return;
                }
                currentUser.IsHeadman = true;
                Database.User.UpdateUser(currentUser);
                await GroupMenu.HeadmanClick(botClient, message, currentUser);
                return;
            }

            if (message.Text!.ToLower() == "восстановить пароль")//вынести в функцию
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
            await botClient.SendTextMessageAsync(message.Chat.Id, text: "Неверный пароль. Попробуй еще раз или вернись в меню.", replyMarkup: RecoverData.GetRecoverPasswordMenu());
        }

        private static bool IsPasswordTrue(string loginMessage, Database.User currentUser)
        {
            if (currentUser.CurrentMessage == "методист")
                return IsMethodistPasswordTrue(loginMessage, currentUser);
            return IsHeadmanPasswordTrue(loginMessage, currentUser);
        }

        private static bool IsMethodistPasswordTrue(string passwordMessage, Database.User currentUser)
        {
            var methodist = Methodist.GetCurentMethodist(currentUser.EnteredLogin!);
            string hashedPassword = HashData.GetHashData(passwordMessage, methodist!.Salt!);
            if (hashedPassword == methodist.Password)
                return true;
            return false;
        }

        private static bool IsHeadmanPasswordTrue(string passwordMessage, Database.User currentUser)
        {
            var headman = Headman.GetCurentHeadman(currentUser.EnteredLogin!);
            string hashedPassword = HashData.GetHashData(passwordMessage, headman!.Salt!);
            if (hashedPassword == headman.Password)
                return true;
            return false;
        }

        public static async Task ChangePasswordAdvice(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            bool isPasswordChanged = false;
            if (currentUser.CurrentMessage == "методист")
            {
                var methodist = Methodist.GetCurentMethodist(currentUser.EnteredLogin!);
                isPasswordChanged = methodist!.IsPasswordChanged;
            }
            if (currentUser.CurrentMessage == "староста")
            {
                var headman = Headman.GetCurentHeadman(currentUser.EnteredLogin!);
                isPasswordChanged = headman!.IsPasswordChanged;
            }

            if (isPasswordChanged == false)
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, установлен стандартный пароль. Для большей безопасности рекоммендую изменить его.");
            return;
        }
    }
}
