using Telegram.Bot.Types;
using Telegram.Bot;
using Timetablebot.Database;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InputFiles;
using Timetablebot.Menu.Confirm;
using Timetablebot.Admin.SignIn;
using Timetablebot.Menu.Courses;

namespace Timetablebot.Menu.Edit
{
    public class StorageEdit
    {
        public static async void GetFilesFromStorage(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            string path = PathSelection.GetPath(currentUser);
            var filesList = Directory.GetFiles(path);
            if(filesList.Length == 0)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Материалы по {currentUser.CurrentSubject!.ToUpper()} отсутствуют.");
                return;
            }

            foreach (var file in filesList)
            {
                using FileStream stream = System.IO.File.Open(file, FileMode.Open);
                await botClient.SendDocumentAsync(message.Chat, document: new InputOnlineFile(stream, Path.GetFileName(file)));
                stream.Close();
            }
        }
        
        public static async void DownloadFile(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if(message.Text?.ToLower() == "отменить загрузку")
            {
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Загрузка файла отменена.", replyMarkup: EditMenu.GetMaterialsEditMenu());
                return;
            }

            var document = message.Document;
            if (document == null)
            {
                await botClient.SendTextMessageAsync(message.Chat, text: "Неверный тип вложения.", replyMarkup: ConfirmMenu.GetCanselFileAddingButton());
                return;
            }

            EditMenu.UpdateEditing(isEditing: false, currentUser);
            var file = await botClient.GetFileAsync(document.FileId);
            string path = Path.Combine(PathSelection.GetPath(currentUser), document.FileName!);
            using FileStream download = new(path, FileMode.Create);
            await botClient.DownloadFileAsync(file.FilePath!, download);
            download.Close();
            await botClient.SendTextMessageAsync(message.Chat, text: $"Файл \"{document.FileName}\" успешно загружен", replyMarkup: EditMenu.GetMaterialsEditMenu());
            GetFilesFromStorage(botClient, message, currentUser);
        }

        public static async void GetFilesListToDelete(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            string path = PathSelection.GetPath(currentUser);
            var filesList = Directory.GetFiles(path);
            if(filesList.Length == 0)
            {
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Материалы по {currentUser.CurrentSubject!.ToUpper()} отвутсвуют.");
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, выбыри файл, который нужно удалить.\nЧтобы выйти назад нажми \"отменить удаление\".", replyMarkup: GetFilesKeyboard(filesList));
        }

        private static ReplyKeyboardMarkup GetFilesKeyboard(string[] filesList)
        {
            var filesButtonsList = new List<KeyboardButton[]>();
            foreach (var file in filesList)
            {
                string fileName = Path.GetFileName(file);
                filesButtonsList.Add(new KeyboardButton[] { $"{fileName}" });
            }
            filesButtonsList.Add(new KeyboardButton[] { "Отменить удаление" });
            return new ReplyKeyboardMarkup(filesButtonsList) { ResizeKeyboard = true };
        }

        public static async void DeleteChosenFile(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (message.Text?.ToLower() == "отменить удаление")
            {
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Удаление отменено.", replyMarkup: EditMenu.GetMaterialsEditMenu());
                return;
            }

            if (message.Text != null)
            {
                string fileName = message.Text;
                string path = Path.Combine(PathSelection.GetPath(currentUser), fileName);
                if (System.IO.File.Exists(path) == false)
                {
                    EditMenu.UpdateEditing(isEditing: false, currentUser);
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Ошибка. Файл не найден.", replyMarkup: EditMenu.GetMaterialsEditMenu());
                    return;
                }
                System.IO.File.Delete(path);
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $" Файл успешно удален.", replyMarkup: EditMenu.GetMaterialsEditMenu());
                GetFilesFromStorage(botClient, message, currentUser);
            }
        }

        public static async void ClearMatherials(ITelegramBotClient botClient, Message message, Database.User currentUser)//пределать(?) переопрределить
        {
            if (message.Text?.ToLower() == "отменить удаление")
            {
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Удаление отменено.", replyMarkup: EditMenu.GetMaterialsEditMenu());
                return;
            }

            if (message.Text?.ToLower() == "подтвердить удаление")
            {
                string path = PathSelection.GetPath(currentUser);
                Directory.Delete(path, true);
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Материалы успешно очищены.", replyMarkup: EditMenu.GetMaterialsEditMenu());
                GetFilesFromStorage(botClient, message, currentUser);
            }
        }

        public static async void ClearAllInfo(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (message.Text?.ToLower() == "отменить удаление")
            {
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Удаление отменено.", replyMarkup: CoursesMenu.GetMethodistCoursesMenu());
                return;
            }

            var methodist = Methodist.GetCurentMethodist(currentUser.EnteredLogin!);
            string enteredPassword = HashData.GetHashData(message.Text!, methodist!.Salt!);
            if(enteredPassword != methodist.Password)
            {
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Неверный пароль. Удаление отменено.", replyMarkup: CoursesMenu.GetMethodistCoursesMenu());
                return;
            }

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "academy");
            Directory.Delete(path, true);
            EditMenu.UpdateEditing(isEditing: false, currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Удаление подвержено. Все данные очищены.", replyMarkup: CoursesMenu.GetMethodistCoursesMenu());
        }
    }
}
