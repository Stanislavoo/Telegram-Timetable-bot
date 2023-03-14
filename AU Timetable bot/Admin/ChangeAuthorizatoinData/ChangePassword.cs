using Telegram.Bot.Types;
using Telegram.Bot;
using Timetablebot.Database;
using Timetablebot.Menu.Courses;
using Timetablebot.Menu.Confirm;
using Timetablebot.Menu.Main;
using Timetablebot.Admin.SignIn;

namespace Timetablebot.Admin.ChangeAuthorizatoinData
{
    public class ChangePassword
    {
        public static void ChangePasswordData(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.CurrentMessage == "методист")
                ChangeMethodistPassword(botClient, message, currentUser);
            if (currentUser.CurrentMessage == "староста")
                ChangeHeadmanPassword(botClient, message, currentUser);
        }

        private static async void ChangeMethodistPassword(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            var methodist = Methodist.GetCurentMethodist(currentUser.EnteredLogin!);

            if (message.Text!.ToLower() == "отменить изменение")
            {
                ConfirmMenu.CanselAuthorizationEditingClick(botClient, message, currentUser);
                return;
            }

            if (currentUser.IsUserConfirmed == false)
            {
                string enteredPassword = HashData.GetHashData(message.Text, methodist!.Salt!);
                if (enteredPassword == methodist.Password)
                {
                    currentUser.IsUserConfirmed = true;
                    Database.User.UpdateUser(currentUser);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи новый пароль, соответствующий следующим требованиям:\n" +
                         $" - длина от 8 до 20 символов;\n - присутствие хотя бы 1 цифры;\n - присутствие хотя бы 1 большой буквы;\n - непостимо присутствие пробелов;" +
                        $"\n - недопустимо присутсвие следующих спецсимволов: \"<\", \">\", \"@\", \"#\", \"%\", \"/\";", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Неверный пароль. Выход назад в меню.", replyMarkup: CoursesMenu.GetMethodistCoursesMenu());
                currentUser.IsEditing = false;
                Database.User.UpdateUser(currentUser);
                return;
            }

            string passwordMistakes = CheckAuthorizationDataPattern.CheckPasswordPattern(message.Text);
            if(passwordMistakes != "success")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{passwordMistakes}", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }

            string hashedEnteredNewPassword = HashData.GetHashData(message.Text, methodist!.Salt!);
            if(hashedEnteredNewPassword == methodist.Password)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, пароль совпадает со старым. Придумай новый.", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }

            methodist.Password = hashedEnteredNewPassword;
            methodist.IsPasswordChanged = true;
            Methodist.UpdateMethodist(methodist);

            currentUser.IsEditing = false;
            currentUser.IsUserConfirmed = false;
            if (currentUser.IsRecovering == true)
            {
                currentUser.IsRecovering = false;
                currentUser.CurrentMessage = "startpage";
            }
            Database.User.UpdateUser(currentUser);
            message.Text = "назад";
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, твой пароль был успешно изменен.\nВойди, используя новые данные.");
        }

        private static async void ChangeHeadmanPassword(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            var headman = Headman.GetCurentHeadman(currentUser.EnteredLogin!);

            if (message.Text!.ToLower() == "отменить изменение")
            {
                ConfirmMenu.CanselAuthorizationEditingClick(botClient, message, currentUser);
                return;
            }

            if (currentUser.IsUserConfirmed == false)
            {
                string enteredPassword = HashData.GetHashData(message.Text, headman!.Salt!);
                if (enteredPassword == headman.Password)
                {
                    currentUser.IsUserConfirmed = true;
                    Database.User.UpdateUser(currentUser);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи новый логин, соответствующий следующим требованиям:\n" +
                         $" - длина от 8 до 20 символов;\n - присутствие хотя бы 1 цифры;\n - присутствие хотя бы 1 большой буквы;\n - непостимо присутствие пробелов;" +
                        $"\n - недопустимо присутсвие следующих спецсимволов: \"<\", \">\", \"@\", \"#\", \"%\", \"/\";", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Неверный пароль. Выход назад в меню.", replyMarkup: MainMenu.GetHeadmanMenu());
                currentUser.IsEditing = false;
                Database.User.UpdateUser(currentUser);
                return;
            }

            string passwordMistakes = CheckAuthorizationDataPattern.CheckPasswordPattern(message.Text);
            if (passwordMistakes != "success")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{passwordMistakes}", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }

            string hashedEnteredNewPassword = HashData.GetHashData(message.Text, headman!.Salt!);
            if (hashedEnteredNewPassword == headman.Password)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, пароль совпадает со старым. Придумай новый.", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }

            headman.Password = hashedEnteredNewPassword;
            headman.IsPasswordChanged = true;
            Headman.UpdateHeadman(headman);

            currentUser.IsEditing = false;
            currentUser.IsUserConfirmed = false;
            if(currentUser.IsRecovering == true)
            {
                currentUser.IsRecovering = false;
                currentUser.CurrentMessage = currentUser.StudGroup!;
            }
            Database.User.UpdateUser(currentUser);
            message.Text = "назад";
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, твой пароль был успешно изменен.\nВойди, используя новые данные.");
        }
    }
}
