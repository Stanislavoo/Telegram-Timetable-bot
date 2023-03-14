using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Timetablebot.Menu.Groups
{
    public class AllGroupsMenu
    {
        public static ReplyKeyboardMarkup GetStudentGroupsMenu()
        {
            ReplyKeyboardMarkup studentGroupsKeyboard = new(new[]
            {
                new KeyboardButton[]{"ГУП-1", "ГУП-2", "ГУП-3","ГУП-4"},
                new KeyboardButton[]{"ГУЭ-1", "ГУЭ-2", "ГУЭ-3"},
                new KeyboardButton[]{"УИР-1","УИР-2"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return studentGroupsKeyboard;
        }

        public static async Task GroupClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            if ((currentUser.IsHeadman == true && currentUser.IsMethodist == false) || (currentUser.IsHeadman == false && currentUser.IsMethodist == false && currentUser.RecoveringType != "восстановить логин"))
                currentUser.EnteredLogin = null;
            currentUser.IsHeadman = false;
            Database.User.UpdateUser(currentUser);
            UpdateGroup(message, currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, как я могу тебе помочь?", replyMarkup: GroupMenu.GetGroupMenu());
        }

        private static void UpdateGroup(Message message, Database.User currentUser)
        {
            currentUser.StudGroup = message.Text!.ToLower();
            Database.User.UpdateUser(currentUser);
        }
    }
}
