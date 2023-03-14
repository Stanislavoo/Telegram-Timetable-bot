using Telegram.Bot.Types.ReplyMarkups;

namespace Timetablebot.Menu.Main.Subjects
{
    public class GueSemestersMenu
    {
        public static ReplyKeyboardMarkup Get1SemesterMenu()
        {
            ReplyKeyboardMarkup firstSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"СП", "ИБГ", "ИЯ", "ВМ"},
                new KeyboardButton[]{"МТ", "ЭТ", "ДЭПГС", "ПК"},
                new KeyboardButton[]{"БЯ", "БЖЧ", "ФК", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return firstSemesterMenu;
        }

        public static ReplyKeyboardMarkup Get2SemesterMenu()
        {
            ReplyKeyboardMarkup secondSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"ФИЛ", "ИЯ", "ВМ"},
                new KeyboardButton[]{"МТ", "ЭТ", "ИТВУД"},
                new KeyboardButton[]{"ОП", "ФК", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return secondSemesterMenu;
        }

        public static ReplyKeyboardMarkup Get3SemesterMenu()
        {
            ReplyKeyboardMarkup thirdSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"МСЭ", "ПУ", "ЭТ", "ГУ"},
                new KeyboardButton[]{"ЭО", "ФСГ", "ПолК", "ДИЯ"},
                new KeyboardButton[]{"ЭДОИВ", "СЭС", "ФК", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return thirdSemesterMenu;
        }

        public static ReplyKeyboardMarkup Get4SemesterMenu()
        {
            ReplyKeyboardMarkup fourthSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"МСЭ", "ЭО", "ЦО", "ДИЯ", "ТП"},
                new KeyboardButton[]{"ГЗ", "НиН", "ГРИД", "ИСиТ"},
                new KeyboardButton[]{"ППЭ", "ОУИС", "ФК", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return fourthSemesterMenu;
        }

        public static ReplyKeyboardMarkup Get5SemesterMenu()
        {
            ReplyKeyboardMarkup fifthSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"ДИЯ", "УО", "ГРЭ", "УВД"},
                new KeyboardButton[]{"БУиА", "ФСГ", "ЦО", "УЛП"},
                new KeyboardButton[]{"ОГЗ", "ФК", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return fifthSemesterMenu;
        }

        public static ReplyKeyboardMarkup Get6SemesterMenu()
        {
            ReplyKeyboardMarkup sixthSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"ДИЯ", "ПУ", "УИС", "УО"},
                new KeyboardButton[]{"ФМ", "ЭкП", "ЭЭ", "ИП"},
                new KeyboardButton[]{"СБиБТ", "ФК", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return sixthSemesterMenu;
        }

        public static ReplyKeyboardMarkup Get7SemesterMenu()
        {
            ReplyKeyboardMarkup seventhSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"ЭИ", "ДОУД", "АХД", "ГЧП"},
                new KeyboardButton[]{"СМ", "РЦБ", "НиН"},
                new KeyboardButton[]{"ППВД", "ВПРБ", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return seventhSemesterMenu;
        }
    }
}
