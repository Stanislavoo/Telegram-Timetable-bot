using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Database;

namespace Timetablebot.Menu.Back
{
    public class BackMenu
    {
        public static ReplyKeyboardMarkup GetBackButton()
        {
            ReplyKeyboardMarkup backButton = new(new[] { new KeyboardButton[] { "Назад" } })
            {
                ResizeKeyboard = true
            };
            return backButton;
        }

        public static ReplyKeyboardMarkup GetBackMenu()
        {
            ReplyKeyboardMarkup backMenu = new(new[] { new KeyboardButton[] { "Меню", "Назад" } })
            {
                ResizeKeyboard = true
            };
            return backMenu;
        }

        public static void BackClick(Database.User currentUser)
        {
            switch (currentUser.MessagePriority)
            {
                case 1:
                    currentUser.CurrentMessage = "startpage";
                    break;

                case 2:
                    if (currentUser.IsMethodist == true)
                    {
                        currentUser.CurrentMessage = "методист";
                        break;
                    }
                    currentUser.CurrentMessage = "студент";
                    break;

                case 3:
                    currentUser.CurrentMessage = currentUser.Course.ToString() + " курс";
                    break;

                case 4:
                    currentUser.CurrentMessage = currentUser.StudGroup!;
                    break;

                case 5:
                    if (currentUser.IsHeadman == true)
                    {
                        currentUser.CurrentMessage = "староста";
                        break;
                    }
                    currentUser.CurrentMessage = "меню";
                    break;

                case 6:
                    if (Button.FindButton(currentUser.CurrentMessage!, "MonthMenu") == true)
                    {
                        currentUser.CurrentMessage = "заявления";
                        break;
                    }
                    currentUser.CurrentMessage = "предметы";
                    break;

                case 7:
                    currentUser.CurrentMessage = currentUser.CurrentSubject!;
                    break;
            }
        }
    }
}
