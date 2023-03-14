using Telegram.Bot.Types.ReplyMarkups;

namespace Timetablebot.Menu.Main.Subjects
{
    public class UirSemestersMenu
    {
        public static ReplyKeyboardMarkup Get1SemesterMenu()
        {
            ReplyKeyboardMarkup firstSemesterMenu = new(new[]
            {
                new KeyboardButton[]{"СП", "ИБГ", "ИЯ", "ВМ"},
                new KeyboardButton[]{"ЭТ", "ОМ", "АВС", "АиП"},
                new KeyboardButton[]{"ПИ", "БЯ", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ФИЛ", "ВМ", "ИЯ", "ЭТ", "ПОЛ"},
                new KeyboardButton[]{"ВОВ", "ЭО", "СИиКПО", "АИП"},
                new KeyboardButton[]{"КГ", "БЖЧ", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ТВиМС", "НиРЭБ", "ПУ", "ИР"},
                new KeyboardButton[]{"ДЭиПК", "ДИЯ", "ЭО", "ДМ"},
                new KeyboardButton[]{"ОС", "АиП", "ФК", "КЧ"},
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
                new KeyboardButton[]{"УITП", "ИР", "ТИ", "ДИЯ"},
                new KeyboardButton[]{"КС", "ТРП", "ЭТС", "САМУР"},
                new KeyboardButton[]{"ГМРПО", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ДИЯ", "УО", "ПОИД", "АХД"},
                new KeyboardButton[]{"ДОУД", "УИС", "АИП", "СБД"},
                new KeyboardButton[]{"ИнфСиТ", "САМУР", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ГУ", "УО", "ДИЯ", "СБД"},
                new KeyboardButton[]{"АИС", "ИнфСиТ", "ЭК-КА", "ИСвЭ"},
                new KeyboardButton[]{"ЭИРТС", "ФК", "КЧ"},
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
                new KeyboardButton[]{"ПУ", "WT", "УИБ", "ПРИС"},
                new KeyboardButton[]{"ИМ", "ИАД", "УИГО", "КЧ"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            return seventhSemesterMenu;
        }
    }
}
