using Microsoft.EntityFrameworkCore;

namespace Timetablebot.Database
{
    public class Button
    {
        public int Id { get; set; }
        public string? StartButtons { get; set; }
        public string? StartMenu { get; set; }
        public string? CoursesMenu { get; set; }
        public string? GroupsMenu { get; set; }
        public string? GroupMainMenu { get; set; }
        public string? GroupMenu { get; set; }
        public string? MonthMenu { get; set; }
        public string? Gup1Semester { get; set; }
        public string? Gup2Semester { get; set; }
        public string? Gup3Semester { get; set; }
        public string? Gup4Semester { get; set; }
        public string? Gup5Semester { get; set; }
        public string? Gup6Semester { get; set; }
        public string? Gup7Semester { get; set; }
        public string? Gue1Semester { get; set; }
        public string? Gue2Semester { get; set; }
        public string? Gue3Semester { get; set; }
        public string? Gue4Semester { get; set; }
        public string? Gue5Semester { get; set; }
        public string? Gue6Semester { get; set; }
        public string? Gue7Semester { get; set; }
        public string? Uir1Semester { get; set; }
        public string? Uir2Semester { get; set; }
        public string? Uir3Semester { get; set; }
        public string? Uir4Semester { get; set; }
        public string? Uir5Semester { get; set; }
        public string? Uir6Semester { get; set; }
        public string? Uir7Semester { get; set; }
        public string? SubjectMenu { get; set; }
        public string? AdminButtons { get; set; }
        public string? MethodistButtons { get; set; }
        public string? FileEditButton { get; set; }
        public string? MaterialsEditButtons { get; set; }
        public string? SupportingButtons { get; set; }

        public static bool IsMessageButton(string message)
        {
            using ApplicationContext db = new();

            var findMessage = db.Buttons.FromSqlRaw($"SELECT * FROM Buttons WHERE '{message}' IN (StartButtons, StartMenu, CoursesMenu, GroupsMenu, GroupMainMenu, GroupMenu, MonthMenu, " +
                $"Gup1Semester, Gup2Semester, Gup3Semester, Gup4Semester, Gup5Semester, Gup6Semester, Gup7Semester, " +
                $"Gue1Semester, Gue2Semester, Gue3Semester, Gue4Semester, Gue5Semester, Gue6Semester, Gue7Semester, " +
                $"Uir1Semester, Uir2Semester, Uir3Semester, Uir4Semester, Uir5Semester, Uir6Semester, Uir7Semester, SubjectMenu)").ToList();

            if (findMessage.Count > 0)
                return true;
            return false;
        }

        public static bool FindButton(string message, string colomnName)
        {
            using ApplicationContext db = new();

            var findMessage = db.Buttons.FromSqlRaw($"SELECT * FROM Buttons WHERE '{message}' IN ({colomnName})").ToList();
            if (findMessage.Count > 0)
                return true;
            return false;
        }
    }
}
