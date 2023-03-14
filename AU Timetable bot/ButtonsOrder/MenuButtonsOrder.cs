using Telegram.Bot.Types;
using Timetablebot.Database;

namespace Timetablebot.ButtonsOrder
{
    public class MenuButtonsOrder
    {
        public static void GetMessagePriority(int priority, Database.User currentUser)
        {
            currentUser.MessagePriority = priority;
            Database.User.UpdateUser(currentUser);
        }

        public static bool CheckMessagePriority(Message message, Database.User currentUser)
        {
            string messageText = message.Text!.ToLower();

            if (messageText == "/start")
                return true;

            if (messageText == "назад" && currentUser.MessagePriority != 0)
                return true;

            if ((messageText == "меню" && currentUser.MessagePriority == 7) || (messageText == "меню" && Button.FindButton(currentUser.CurrentMessage!, "MonthMenu") == true))
                return true;

            if ((message.Text == "материалы" || message.Text == "преподаватели") && Button.FindButton(currentUser.CurrentMessage!, "MonthMenu") == true)//
                return false;

            if (currentUser.MessagePriority++ > 7)
                return false;

            string[] foundMenu = Database.ButtonsOrder.GetMenuName(currentUser.MessagePriority++);
            for (int i = 0; i < foundMenu.Length; i++)
            {
                if ((currentUser.CurrentMessage == "заявления" && foundMenu[i] == "Semester") || (currentUser.CurrentMessage == "предметы" && foundMenu[i] == "MonthMenu"))//
                    continue;

                if (foundMenu[i] == "Semester")
                    foundMenu[i] = GetSemesterMenuGroupPart(currentUser.StudGroup!) + GetSemesterMenuSemensterNunberPart(message.Date.Month, currentUser.Course) + foundMenu[i];
                
                if (Button.FindButton(messageText, foundMenu[i]) == true)
                    return true;
            }
            return false;
        }

        private static string GetSemesterMenuGroupPart(string group)
        {
            switch (group)
            {
                case "гуп-1":
                case "гуп-2":
                case "гуп-3":
                case "гуп-4":
                    group = "Gup";
                    break;

                case "гуэ-1":
                case "гуэ-2":
                case "гуэ-3":
                    group = "Gue";
                    break;

                case "уир-1":
                case "уир-2":
                    group = "Uir";
                    break;
            }
            return group;
        }

        private static string GetSemesterMenuSemensterNunberPart(int month, int course)
        {
            if (month >= 2 && month <= 7)
            {
                if (course == 1)
                    return "2";

                if (course == 2)
                    return "4";

                if (course == 3)
                    return "6";
            }

            if (course == 1)
                return "1";

            if (course == 2)
                return "3";

            if (course == 3)
                return "5";

            return "7";
        }

        public static void UpdateCurrentMessage(string message, Database.User currentUser)
        {
            if (Button.IsMessageButton(message) == true)
            {
                currentUser.CurrentMessage = message;
                Database.User.UpdateUser(currentUser);
            }
        }

        public static void GetEditingTypePriority(int priority, Database.User currentUser)
        {
            currentUser.EditingTypePriority = priority;
            Database.User.UpdateUser(currentUser);
        }

        public static bool CheckEditingTypePriority(Database.User currentUser)//дописать
        {
            if (currentUser.EditingTypePriority == 8 && ((currentUser.MessagePriority == 1 && currentUser.IsMethodist == true) || (currentUser.MessagePriority == 4 && currentUser.IsHeadman == true && currentUser.IsMethodist == false)))
                return true;
            if (currentUser.EditingTypePriority == 9 && currentUser.MessagePriority == 1 && currentUser.IsMethodist == true)
                return true;
            if (currentUser.EditingTypePriority == 10 && currentUser.IsHeadman == true && ((currentUser.MessagePriority == 5 && currentUser.CurrentMessage == "расписание") || (currentUser.MessagePriority == 6 && Button.FindButton(currentUser.CurrentMessage!, "MonthMenu") == true) || (currentUser.MessagePriority == 7 && currentUser.CurrentMessage == "преподаватели")))
                return true;
            if (currentUser.EditingTypePriority == 11 && currentUser.IsHeadman == true && currentUser.MessagePriority == 7 && currentUser.CurrentMessage == "материалы")
                return true;

            GetEditingTypePriority(0, currentUser);
            return false;
        }
    }
}
