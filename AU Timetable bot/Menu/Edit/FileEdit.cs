using Telegram.Bot;
using Telegram.Bot.Types;

namespace Timetablebot.Menu.Edit
{
    public class FileEdit
    {
        public static async Task<string> GetTextFromFile(Database.User currentUser)
        {
            string path = PathSelection.GetPath(currentUser);
            string textFromFile = await System.IO.File.ReadAllTextAsync(path);
            if(textFromFile == "")
                return GetInfoAbsenceMessage(currentUser);
            return textFromFile;
        }

        private static string GetInfoAbsenceMessage(Database.User currentUser)
        {
            if (currentUser.CurrentMessage == "расписание")
                return "Расписание не загружено.";
            if (currentUser.CurrentMessage == "преподаватели")
                return "Информация отсутствует.";
            return "Информация о заявлениях отсутствует.";
        }

        public static async void EditFileInfo(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if(message.Text!.ToLower() == "отменить редактирование")
            {
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                message.Text = currentUser.CurrentMessage;
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Изменение отменено.");
                return;
            }

            string path = PathSelection.GetPath(currentUser);

            if (message.Text!.ToLower() == "удалить информацию")
            {
                System.IO.File.WriteAllText(path, string.Empty);
                EditMenu.UpdateEditing(isEditing: false, currentUser);
                message.Text = currentUser.CurrentMessage;
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Информация успешно удалена.");
                return;
            }

            System.IO.File.WriteAllText(path, message.Text);
            EditMenu.UpdateEditing(isEditing: false, currentUser);
            message.Text = currentUser.CurrentMessage;
            await botClient.SendTextMessageAsync(message.Chat.Id, text: "Изменения успешно внесены.");
        }
    }
}
