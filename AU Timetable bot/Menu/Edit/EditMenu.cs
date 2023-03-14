using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Admin.ChangeAuthorizatoinData;

namespace Timetablebot.Menu.Edit
{
    public class EditMenu
    {
        public static ReplyKeyboardMarkup GetFileEditButton()
        {
            ReplyKeyboardMarkup fileEditButton = new(new[] { new KeyboardButton[] { "Редактировать", "Назад" } })
            {
                ResizeKeyboard = true
            };
            return fileEditButton;
        }

        public static ReplyKeyboardMarkup GetFileEditMenu()
        {
            ReplyKeyboardMarkup fileEditMenu = new(new[] 
            { 
                new KeyboardButton[] {"Редактировать" },
                new KeyboardButton[] { "Меню", "Назад" } 
            })
            {
                ResizeKeyboard = true
            };
            return fileEditMenu;
        }

        public static ReplyKeyboardMarkup GetMaterialsEditMenu()
        {
            ReplyKeyboardMarkup materialsEditMenu = new(new[]
            {
                new KeyboardButton[] {"Загрузить файл", "Удалить файл", "Удалить все файлы" },
                new KeyboardButton[] { "Меню", "Назад" }
            })
            {
                ResizeKeyboard = true
            };
            return materialsEditMenu;
        }

        public static void UpdateEditing(bool isEditing, Database.User currentUser, string editingType = "cansel editing")
        {
            currentUser.IsEditing = isEditing;
            if(editingType!= "cansel editing")
                currentUser.EditingType = editingType;
            Database.User.UpdateUser(currentUser);
        }

        public static void StartEditing(ITelegramBotClient botClient, Message message, Database.User currentUser)//дописать
        {
            switch (currentUser.EditingType)
            {
                case "изменить логин":
                    ChangeLogin.ChangeLoginData(botClient, message, currentUser);
                    break;

                case "изменить пароль":
                    ChangePassword.ChangePasswordData(botClient, message, currentUser);
                    break;

                case "очистить данные всех курсов":
                    StorageEdit.ClearAllInfo(botClient, message, currentUser);
                    break;

                case "редактировать":
                    FileEdit.EditFileInfo(botClient, message, currentUser);
                    break;

                case "загрузить файл":
                    StorageEdit.DownloadFile(botClient, message, currentUser);
                    break;

                case "удалить файл":
                    StorageEdit.DeleteChosenFile(botClient, message, currentUser);
                    break;

                case "удалить все файлы":
                    StorageEdit.ClearMatherials(botClient, message, currentUser);
                    break;
            }
        }
    }
}
