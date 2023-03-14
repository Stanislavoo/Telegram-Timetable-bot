using Telegram.Bot.Types;
using Telegram.Bot;
using Timetablebot.Database;
using Timetablebot.Menu.Confirm;
using Timetablebot.Admin.ChangeAuthorizatoinData;
using Timetablebot.Admin.SignIn;

namespace Timetablebot.Admin.RecoverAuthorizatoinData
{
    public class RecoverPassword
    {
        public static void RecoverPasswordData(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.CurrentMessage == "методист")
                RecoverMethodistPassword(botClient, message, currentUser);
            if (currentUser.CurrentMessage == "староста")
                RecoverHeadmanPassword(botClient, message, currentUser);
        }

        private static async void RecoverMethodistPassword(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (message.Text!.ToLower() == "отменить восстановление данных")//вынести в функцию
            {
                currentUser.CurrentMessage = "startpage";
                currentUser.MessagePriority = 0;
                currentUser.EnteredLogin = null;
                currentUser.IsUserConfirmed = false;
                Database.User.UpdateUser(currentUser);
                ConfirmMenu.CanselRecoveringClick(botClient, message, currentUser);
                return;
            }

            if(currentUser.IsUserConfirmed == false)
            {
                var methodist = Methodist.GetCurentMethodist(currentUser.EnteredLogin!);
                string inputRecoveryCode = HashData.GetHashData(message.Text, methodist!.Salt!);
                if (inputRecoveryCode == methodist.RecoveryCode)
                {
                    currentUser.IsUserConfirmed = true;
                    Database.User.UpdateUser(currentUser);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи новый пароль, соответствующий следующим требованиям:\n" +
                    $" - длина от 8 до 20 символов;\n - присутствие хотя бы 1 цифры;\n - присутствие хотя бы 1 большой буквы;\n - непостимо присутствие пробелов;" +
                    $"\n - недопустимо присутсвие следующих спецсимволов: \"<\", \">\", \"@\", \"#\", \"%\", \"/\";", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Неверный код восстановления. Попробуй другой.", replyMarkup: ConfirmMenu.GetCanselRecoveringButton());
            }

            if (currentUser.IsUserConfirmed == true)
                ChangePassword.ChangePasswordData(botClient, message, currentUser);
        }

        private static async void RecoverHeadmanPassword(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (message.Text!.ToLower() == "отменить восстановление данных")//вынести в функцию
            {
                currentUser.CurrentMessage = currentUser.StudGroup!;
                currentUser.MessagePriority = 3;
                currentUser.EnteredLogin = null;
                currentUser.IsUserConfirmed = false;
                Database.User.UpdateUser(currentUser);
                ConfirmMenu.CanselRecoveringClick(botClient, message, currentUser);
                return;
            }

            if(currentUser.IsUserConfirmed == false)
            {
                var headman = Headman.GetCurentHeadman(currentUser.EnteredLogin!);
                string inputRecoveryCode = HashData.GetHashData(message.Text, headman!.Salt!);
                if (inputRecoveryCode == headman.RecoveryCode)
                {
                    currentUser.IsUserConfirmed = true;
                    Database.User.UpdateUser(currentUser);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи новый пароль, соответствующий следующим требованиям:\n" +
                    $" - длина от 8 до 20 символов;\n - присутствие хотя бы 1 цифры;\n - присутствие хотя бы 1 большой буквы;\n - непостимо присутствие пробелов;" +
                    $"\n - недопустимо присутсвие следующих спецсимволов: \"<\", \">\", \"@\", \"#\", \"%\", \"/\";", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Неверный код восстановления. Попробуй другой.", replyMarkup: ConfirmMenu.GetCanselRecoveringButton());
            }

            if (currentUser.IsUserConfirmed == true)
                ChangePassword.ChangePasswordData(botClient, message, currentUser);
        }
    }
}
