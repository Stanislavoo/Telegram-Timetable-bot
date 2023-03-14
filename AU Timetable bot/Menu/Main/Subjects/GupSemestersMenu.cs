using Telegram.Bot.Types.ReplyMarkups;

namespace Timetablebot.Menu.Main.Subjects
{
    public class GupSemestersMenu
    {
        public static ReplyKeyboardMarkup Get1SemesterMenu()
        {
            ReplyKeyboardMarkup firstSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"ИЯ", "ФИЛ", "СП", "ИБГ", "БЯ"},
                new KeyboardButton[]{"ИГПЗС", "ИГПБ", "ТГП", "ПОЛ"},
                new KeyboardButton[]{"ИТВУД", "БЖЧ", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ОМ", "ГУ", "ИЯ", "ИГПЗС"},
                new KeyboardButton[]{"ИГПБ", "ТГП", "КП", "ЛР"},
                new KeyboardButton[]{"ИППУ", "РЧП", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ДИЯ", "ПУ", "ГУ", "ЭО"},
                new KeyboardButton[]{"КП", "КПЗС", "АП", "ТП"},
                new KeyboardButton[]{"ГП", "ЭДОИВ", "ФК", "КЧ"},
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
                new KeyboardButton[]{"АДПИП", "ГП", "ТП", "ДЭиПК"},
                new KeyboardButton[]{"ДИЯ", "НиРЭБ", "УП", "СУД"},
                new KeyboardButton[]{"ЖилПр", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ДИЯ", "ИРРБ", "ФНП", "УП"},
                new KeyboardButton[]{"УПР", "ГП", "ГХПР", "ПРАЗО"},
                new KeyboardButton[]{"СемП", "ПСО", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ДИЯ", "МЧП", "ПН", "КРИМ"},
                new KeyboardButton[]{"ГП", "ГХПР", "ЭП", "ТамП"},
                new KeyboardButton[]{"ПК", "БИП", "ФК", "КЧ"},
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
                new KeyboardButton[]{"УИС", "ХП", "ИПР", "КРИМ"},
                new KeyboardButton[]{"ЖилПр", "КПР", "ОНД", "СтрП"},
                new KeyboardButton[]{"КвП", "СЭ", "УИП", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return seventhSemesterMenu;
        }
    }
}
