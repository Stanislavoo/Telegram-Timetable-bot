using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Confirm;

namespace Timetablebot.Admin.RecoverAuthorizatoinData
{
    public class RecoverData
    {
        public static ReplyKeyboardMarkup GetRecoverLoginingDataMenu()
        {
            ReplyKeyboardMarkup recoverLoginingDataMenu = new(new[]
            {
                new KeyboardButton[] { "Восстановить логин", "Восстановить логин и пароль"},
                new KeyboardButton[] { "Вернуться в меню" }
            })
            {
                ResizeKeyboard = true
            };
            return recoverLoginingDataMenu;
        }

        public static ReplyKeyboardMarkup GetRecoverPasswordMenu()
        {
            ReplyKeyboardMarkup recoverPasswordMenu = new(new[]
            {
                new KeyboardButton[] { "Восстановить пароль", "Вернуться в меню" }
            })
            {
                ResizeKeyboard = true
            };
            return recoverPasswordMenu;
        }

        public static async void BackToMenuClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            message.Text = "назад";
            currentUser.EnteredLogin = null;
            Database.User.UpdateUser(currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: "Вход прерван. Выход назад.");
        }

        public static void StartRecovering(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            switch (currentUser.RecoveringType)
            {
                case "восстановить логин":
                    RecoverLogin.RecoverLoginData(botClient, message, currentUser);
                    break;

                case "восстановить пароль":
                    RecoverPassword.RecoverPasswordData(botClient, message, currentUser);
                    break;

                case "восстановить логин и пароль":
                    RecoverAllData(botClient, message, currentUser);
                    break;
            }
        }

        private static async void RecoverAllData(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.EnteredLogin == null)
            {
                RecoverLogin.RecoverLoginData(botClient, message, currentUser);
                if(currentUser.IsRecovering == true && currentUser.IsUserConfirmed == true)
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи новый пароль, соответствующий следующим требованиям:\n" +
                    $" - длина от 8 до 20 символов;\n - присутствие хотя бы 1 цифры;\n - присутствие хотя бы 1 большой буквы;\n - непостимо присутствие пробелов;" +
                    $"\n - недопустимо присутсвие следующих спецсимволов: \"<\", \">\", \"@\", \"#\", \"%\", \"/\";", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }
            RecoverPassword.RecoverPasswordData(botClient, message, currentUser);
        }
    }
}
