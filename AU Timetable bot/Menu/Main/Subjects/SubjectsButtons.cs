using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Timetablebot.Menu.Main.Subject;

namespace Timetablebot.Menu.Main.Subjects
{
    public class SubjectsButtons
    {
        public static async Task SubjectsButtonsClick(ITelegramBotClient botClient, Message message, Database.User currentUser)
        {
            UpdateSubject(currentUser);
            await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, что тебя интересует по {currentUser.CurrentSubject!.ToUpper()}?", replyMarkup: SubjectMenu.GetSubjestMenu());
        }

        private static void UpdateSubject(Database.User currentUser)
        {
            currentUser.CurrentSubject = currentUser.CurrentMessage;
            Database.User.UpdateUser(currentUser);
        }

        public static ReplyKeyboardMarkup GetMenuType(int month, Database.User currentUser)
        {
            string group = currentUser.StudGroup!.Substring(0, 3);
            if (group == "гуп")
                return GetGupSubjectsMenu(month, currentUser);

            if (group == "гуэ")
                return GetGueSubjectsMenu(month, currentUser);

            return GetUirSubjectsMenu(month, currentUser);
        }

        private static ReplyKeyboardMarkup GetGupSubjectsMenu(int month, Database.User currentUser)
        {
            if (month >= 2 && month <= 7)
            {
                if (currentUser.Course == 1)
                    return GupSemestersMenu.Get2SemesterMenu();

                if (currentUser.Course == 2)
                    return GupSemestersMenu.Get4SemesterMenu();

                if (currentUser.Course == 3)
                    return GupSemestersMenu.Get6SemesterMenu();
            }

            if (currentUser.Course == 1)
                return GupSemestersMenu.Get1SemesterMenu();

            if (currentUser.Course == 2)
                return GupSemestersMenu.Get3SemesterMenu();

            if (currentUser.Course == 3)
                return GupSemestersMenu.Get5SemesterMenu();

            return GupSemestersMenu.Get7SemesterMenu();
        }

        private static ReplyKeyboardMarkup GetGueSubjectsMenu(int month, Database.User currentUser)
        {
            if (month >= 2 && month <= 7)
            {
                if (currentUser.Course == 1)
                    return GueSemestersMenu.Get2SemesterMenu();

                if (currentUser.Course == 2)
                    return GueSemestersMenu.Get4SemesterMenu();

                if (currentUser.Course == 3)
                    return GueSemestersMenu.Get6SemesterMenu();
            }

            if (currentUser.Course == 1)
                return GueSemestersMenu.Get1SemesterMenu();

            if (currentUser.Course == 2)
                return GueSemestersMenu.Get3SemesterMenu();

            if (currentUser.Course == 3)
                return GueSemestersMenu.Get5SemesterMenu();

            return GueSemestersMenu.Get7SemesterMenu();
        }

        private static ReplyKeyboardMarkup GetUirSubjectsMenu(int month, Database.User currentUser)
        {
            if (month >= 2 && month <= 7)
            {
                if (currentUser.Course == 1)
                    return UirSemestersMenu.Get2SemesterMenu();

                if (currentUser.Course == 2)
                    return UirSemestersMenu.Get4SemesterMenu();

                if (currentUser.Course == 3)
                    return UirSemestersMenu.Get6SemesterMenu();
            }

            if (currentUser.Course == 1)
                return UirSemestersMenu.Get1SemesterMenu();

            if (currentUser.Course == 2)
                return UirSemestersMenu.Get3SemesterMenu();

            if (currentUser.Course == 3)
                return UirSemestersMenu.Get5SemesterMenu();

            return UirSemestersMenu.Get7SemesterMenu();
        }
    }
}
