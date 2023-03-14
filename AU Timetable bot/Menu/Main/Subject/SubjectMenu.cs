using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Back;
using Timetablebot.Menu.Edit;

namespace Timetablebot.Menu.Main.Subject
{
    public class SubjectMenu
    {
        public static ReplyKeyboardMarkup GetSubjestMenu()
        {
            ReplyKeyboardMarkup subjestMenu = new(new[]
            {
                new KeyboardButton[] {"Преподаватели", "Материалы"},
                new KeyboardButton[] {"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return subjestMenu;
        }

        public static async Task LecturersClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if(currentUser.IsHeadman == true)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: await FileEdit.GetTextFromFile(currentUser), replyMarkup: EditMenu.GetFileEditMenu());
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: await FileEdit.GetTextFromFile(currentUser), replyMarkup: BackMenu.GetBackMenu());
        }

        public static async Task MaterialsClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if (currentUser.IsHeadman == true)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Материалы по {currentUser.CurrentSubject!.ToUpper()}:", replyMarkup: EditMenu.GetMaterialsEditMenu());
                StorageEdit.GetFilesFromStorage(botClient, message, currentUser);
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Материалы по {currentUser.CurrentSubject!.ToUpper()}:", replyMarkup: BackMenu.GetBackMenu());
            StorageEdit.GetFilesFromStorage(botClient, message, currentUser);
        }
    }
}
