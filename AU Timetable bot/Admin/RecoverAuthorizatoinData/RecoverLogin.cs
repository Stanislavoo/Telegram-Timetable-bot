using Telegram.Bot.Types;
using Telegram.Bot;
using Timetablebot.Database;
using Timetablebot.Menu.Confirm;
using Timetablebot.Admin.SignIn;

namespace Timetablebot.Admin.RecoverAuthorizatoinData
{
    public class RecoverLogin
    {
        public static void RecoverLoginData(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.CurrentMessage == "методист")
                RecoverMethodistLogin(botClient, message, currentUser);
            if (currentUser.CurrentMessage == "староста")
                RecoverHeadmanLogin(botClient, message, currentUser);
        }

        private static async void RecoverMethodistLogin(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (message.Text!.ToLower() == "отменить восстановление данных")//вынести в функцию
            {
                currentUser.CurrentMessage = "startpage";
                currentUser.MessagePriority = 0;
                Database.User.UpdateUser(currentUser);
                ConfirmMenu.CanselRecoveringClick(botClient, message, currentUser);
                return;
            }
            //доделать проверку кода на шаблон 6 цифр(?)
            var methodists = Methodist.GetMethodistsList();
            foreach (var methodist in methodists)
            {
                string inputRecoveryCode = HashData.GetHashData(message.Text, methodist.Salt!);
                if (inputRecoveryCode == methodist.RecoveryCode)
                {
                    if (currentUser.RecoveringType == "восстановить логин и пароль")
                    {
                        currentUser.IsUserConfirmed = true;
                        currentUser.EnteredLogin = methodist.Login;
                    }
                    if (currentUser.RecoveringType != "восстановить логин и пароль")
                    {
                        currentUser.IsRecovering = false;
                        currentUser.CurrentMessage = "startpage";
                        message.Text = "назад";
                    }
                    Database.User.UpdateUser(currentUser);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, твой логин \"{methodist.Login}\"");
                    return;
                }
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Неверный код восстановления. Попробуй другой.", replyMarkup: ConfirmMenu.GetCanselRecoveringButton());
        }

        private static async void RecoverHeadmanLogin(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (message.Text!.ToLower() == "отменить восстановление данных")//вынести в функцию
            {
                currentUser.CurrentMessage = currentUser.StudGroup!;
                currentUser.MessagePriority = 3;
                Database.User.UpdateUser(currentUser);
                ConfirmMenu.CanselRecoveringClick(botClient, message, currentUser);
                return;
            }

            var headman = Headman.GetCurentHeadman(currentUser.Course, currentUser.StudGroup!);
            string inputRecoveryCode = HashData.GetHashData(message.Text, headman!.Salt!);
            if (inputRecoveryCode != headman.RecoveryCode)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Неверный код восстановления. Попробуй другой.", replyMarkup: ConfirmMenu.GetCanselRecoveringButton());
                return;
            }

            if (currentUser.RecoveringType == "восстановить логин и пароль")
            {
                currentUser.IsUserConfirmed = true;
                currentUser.EnteredLogin = headman.Login;
            }
            if (currentUser.RecoveringType != "восстановить логин и пароль")
            {
                currentUser.IsRecovering = false;
                currentUser.CurrentMessage = currentUser.StudGroup!;
                message.Text = "назад";
            }
            Database.User.UpdateUser(currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, твой логин \"{headman.Login}\"");
        }
    }
}