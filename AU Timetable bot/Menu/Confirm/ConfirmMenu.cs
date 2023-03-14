using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Main;
using Timetablebot.Menu.Groups;
using Timetablebot.Menu.Start;
using Timetablebot.Menu.Courses;

namespace Timetablebot.Menu.Confirm
{
    public class ConfirmMenu
    {
        public static ReplyKeyboardMarkup GetCanselRecoveringButton()
        {
            ReplyKeyboardMarkup loginingOutButton = new(new[]
            {
                new KeyboardButton[] {"Отменить восстановление данных"}
            })
            {
                ResizeKeyboard = true
            };
            return loginingOutButton;
        }

        public static ReplyKeyboardMarkup GetCanselAuthorizationEditingButton()
        {
            ReplyKeyboardMarkup authorizationOutButton = new(new[]
            {
                new KeyboardButton[] {"Отменить изменение"}
            })
            {
                ResizeKeyboard = true
            };
            return authorizationOutButton;
        }

        public static ReplyKeyboardMarkup GetCanselFileEditingMenu()
        {
            ReplyKeyboardMarkup canselFileEditingButton = new(new[]
            {
                new KeyboardButton[]{"Удалить информацию", "Отменить редактирование"}
            })
            {
                ResizeKeyboard = true
            };
            return canselFileEditingButton;
        }

        public static ReplyKeyboardMarkup GetCanselFileAddingButton()
        {
            ReplyKeyboardMarkup canselFileAddingButton = new(new[]
            {
                new KeyboardButton[] { "Отменить загрузку" }
            })
            {
                ResizeKeyboard = true
            };
            return canselFileAddingButton;
        }

        public static ReplyKeyboardMarkup GetConfirmDeletingMenu()
        {
            ReplyKeyboardMarkup confirmDeletingMenu = new(new[]
            {
                new KeyboardButton[]{"Подтвердить удаление", "Отменить удаление"}
            })
            {
                ResizeKeyboard = true
            };
            return confirmDeletingMenu;
        }

        public static ReplyKeyboardMarkup GetConfirmDeletingButton()
        {
            ReplyKeyboardMarkup confirmDeletingButton = new(new[]
            {
                new KeyboardButton[]{ "Отменить удаление"}
            })
            {
                ResizeKeyboard = true
            };
            return confirmDeletingButton;
        }

        public static async void CanselRecoveringClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            currentUser.IsRecovering = false;
            Database.User.UpdateUser(currentUser);
            if (currentUser.CurrentMessage == "startpage")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Восстановление отменено. Выход в меню.", replyMarkup: StartMenu.GetStartMenu());
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: "Восстановление отменено. Выход в меню.", replyMarkup: GroupMenu.GetGroupMenu());
        }

        public static async void CanselAuthorizationEditingClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            currentUser.IsEditing = false;
            currentUser.IsUserConfirmed = false;
            if (currentUser.IsRecovering == true)
            {
                currentUser.IsRecovering = false;
                currentUser.CurrentMessage = currentUser.CurrentMessage == "методист" ? "startpage" : currentUser.StudGroup!;
                Database.User.UpdateUser(currentUser);
                message.Text = "назад";
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Восстановление отменено. Выход назад в меню.");
                return;
            }
            Database.User.UpdateUser(currentUser);
            if(currentUser.CurrentMessage == "методист")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Изменения отменены. Выход в меню.", replyMarkup: CoursesMenu.GetMethodistCoursesMenu());
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: "Изменения отменены. Выход в меню.", replyMarkup: MainMenu.GetHeadmanMenu());
        }
    }
}
