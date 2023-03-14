using Telegram.Bot.Types;
using Telegram.Bot;
using Timetablebot.Database;
using Timetablebot.Menu.Courses;
using Timetablebot.Menu.Confirm;
using Timetablebot.Menu.Main;
using Timetablebot.Admin.SignIn;

namespace Timetablebot.Admin.ChangeAuthorizatoinData
{
    public class ChangeLogin
    {
        public static void ChangeLoginData(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.CurrentMessage == "методист")
                ChangeMethodistLogin(botClient, message, currentUser);
            if (currentUser.CurrentMessage == "староста")
                ChangeHeadmanLogin(botClient, message, currentUser);
        }

        private static async void ChangeMethodistLogin(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            var methodist = Methodist.GetCurentMethodist(currentUser.EnteredLogin!);

            if(message.Text!.ToLower() == "отменить изменение")
            {
                ConfirmMenu.CanselAuthorizationEditingClick(botClient, message, currentUser);
                return;
            }

            if (currentUser.IsUserConfirmed == false)
            {
                string enteredPassword = HashData.GetHashData(message.Text!, methodist!.Salt!);
                if(enteredPassword == methodist.Password)
                {
                    currentUser.IsUserConfirmed = true;
                    Database.User.UpdateUser(currentUser);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи новый логин, соответствующий следующим требованиям:\n" +
                        $" - длина от 5 до 20 символов;\n - непостимо присутствие пробелов;\n - недопустимо присутсвие следующих спецсимволов: \"<\", \">\", \"@\", \"#\", \"%\", \"/\".", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Неверный пароль. Выход назад в меню.", replyMarkup: CoursesMenu.GetMethodistCoursesMenu());
                currentUser.IsEditing = false;
                Database.User.UpdateUser(currentUser);
                return;
            }

            string loginMistakes = CheckAuthorizationDataPattern.CheckLoginPattern(message.Text!);
            if (loginMistakes != "success")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{loginMistakes}", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }

            var sameMethodistLogin = Methodist.GetCurentMethodist(message.Text!);
            if(sameMethodistLogin != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, такой логин уже существует. Придумай новый.", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }

            methodist!.Login = message.Text;
            Methodist.UpdateMethodist(methodist);

            currentUser.EnteredLogin = methodist.Login;
            currentUser.IsEditing = false;
            currentUser.IsUserConfirmed = false;
            Database.User.UpdateUser(currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, твой логин был успешно изменен на \"{message.Text}\".\n", replyMarkup: CoursesMenu.GetMethodistCoursesMenu());
        }

        private static async void ChangeHeadmanLogin(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            var headman = Headman.GetCurentHeadman(currentUser.EnteredLogin!);

            if (message.Text!.ToLower() == "отменить изменение")
            {
                ConfirmMenu.CanselAuthorizationEditingClick(botClient, message, currentUser);
                return;
            }

            if (currentUser.IsUserConfirmed == false)
            {
                string enteredPassword = HashData.GetHashData(message.Text!, headman!.Salt!);
                if (enteredPassword == headman.Password)
                {
                    currentUser.IsUserConfirmed = true;
                    Database.User.UpdateUser(currentUser);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи новый логин, соответствующий следующим требованиям:\n" +
                        $" - длина от 5 до 20 символов;\n - непостимо присутствие пробелов;\n - недопустимо присутсвие следующих спецсимволов: \"<\", \">\", \"@\", \"#\", \"%\", \"/\".", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Неверный пароль. Выход назад в меню.", replyMarkup: MainMenu.GetHeadmanMenu());
                currentUser.IsEditing = false;
                Database.User.UpdateUser(currentUser);
                return;
            }

            string loginMistakes = CheckAuthorizationDataPattern.CheckLoginPattern(message.Text!);
            if (loginMistakes != "success")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{loginMistakes}", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }

            var sameHeadmanLogin = Headman.GetCurentHeadman(message.Text);
            if (sameHeadmanLogin != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, этот логин уже занят. Придумай новый.", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                return;
            }

            headman!.Login = message.Text;
            Headman.UpdateHeadman(headman);

            currentUser.EnteredLogin = headman.Login;
            currentUser.IsEditing = false;
            currentUser.IsUserConfirmed = false;
            Database.User.UpdateUser(currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, твой логин был успешно изменен на \"{message.Text}\".\n", replyMarkup: MainMenu.GetHeadmanMenu());
        }
    }
}
