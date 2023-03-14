using Timetablebot.Database;

namespace Timetablebot.Menu.Edit
{
    public class PathSelection
    {
        private static readonly string startFolderPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string GetPath(User currentUser)
        {
            string path = startFolderPath;

            path = Path.Combine(path, "academy");
            path = GetCourseFolderPath(path, currentUser);
            path = FolderEnsureExists(path, ButtonsTranslation.GetEnglishName(currentUser.StudGroup!));
            if (currentUser.CurrentMessage == "расписание")
            {
                path = FolderEnsureExists(path, "timetable");
                path = FileEnsureExists(path, "timetable.txt");
            }
            if (Button.FindButton(currentUser.CurrentMessage!, "MonthMenu") == true)//statements
            {
                path = FolderEnsureExists(path, "statements");
                path = FileEnsureExists(path, ButtonsTranslation.GetEnglishName(currentUser.CurrentMessage!) + ".txt");
            }
            if(currentUser.CurrentMessage == "преподаватели" || currentUser.CurrentMessage == "материалы")
            {
                path = FolderEnsureExists(path, "subjects");
                path = FolderEnsureExists(path, ButtonsTranslation.GetEnglishName(currentUser.CurrentSubject!));
                if (currentUser.CurrentMessage == "преподаватели")
                    path = FileEnsureExists(path, "info.txt");
                if (currentUser.CurrentMessage == "материалы")
                    path = FolderEnsureExists(path, "matherials");
            }
            return path;
        }

        private static string FolderEnsureExists(string path, string folderName)
        {
            path = Path.Combine(path, folderName);
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            return path;
        }

        private static string FileEnsureExists(string path, string fileName)
        {
            path = Path.Combine(path, fileName);
            if (File.Exists(path) == false)
                File.Create(path).Close();
            return path;
        }

        private static string GetCourseFolderPath(string path, User currentUser)
        {
            switch (currentUser.Course)
            {
                case 1:
                    path = FolderEnsureExists(path, "1 course");
                    break;

                case 2:
                    path = FolderEnsureExists(path, "2 course");
                    break;

                case 3:
                    path = FolderEnsureExists(path, "3 course");
                    break;

                case 4:
                    path = FolderEnsureExists(path, "4 course");
                    break;
            }
            return path;
        }
    }
}
