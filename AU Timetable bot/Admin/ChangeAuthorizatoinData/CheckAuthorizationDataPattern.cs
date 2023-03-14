using System.Text.RegularExpressions;

namespace Timetablebot.Admin.ChangeAuthorizatoinData
{
    public class CheckAuthorizationDataPattern
    {
        public static string CheckPasswordPattern(string inputPassword)
        {
            string passwordMistakes = "";
            passwordMistakes += CheckPasswordLenth(inputPassword);
            passwordMistakes += CheckLowercaseLettersPresense(inputPassword);
            passwordMistakes += CheckNumbersPresence(inputPassword);
            passwordMistakes += CheckUppercaseLettersPresense(inputPassword);
            passwordMistakes += CheckSpacesPresence(inputPassword);
            passwordMistakes += CheckBannedSpecialSymbolsPresence(inputPassword);

            if (passwordMistakes != "")
                return passwordMistakes;
            return "success";
        }

        public static string CheckLoginPattern(string inputLogin)
        {
            string loginMistakes = "";
            loginMistakes += CheckLoginLenth(inputLogin);
            loginMistakes += CheckLowercaseLettersPresense(inputLogin, isLogin: true);
            loginMistakes += CheckSpacesPresence(inputLogin, isLogin: true);
            loginMistakes += CheckBannedSpecialSymbolsPresence(inputLogin, isLogin: true);

            if (loginMistakes != "")
                return loginMistakes;
            return "success";
        }

        private static string CheckPasswordLenth(string inputPassword)
        {
            if (inputPassword.Length < 8)
                return "- Длина пароля не должна быть менее 8 символов.\n";
            if (inputPassword.Length > 20)
                return "- Длина пароля не должна первышать 20 символов.\n";
            return "";
        }

        private static string CheckLoginLenth(string inputLogin)
        {
            if (inputLogin.Length < 5)
                return "- Длина логина не должна быть менее 5 символов.\n";
            if (inputLogin.Length > 20)
                return "- Длина логина не должна первышать 20 символов.\n";
            return "";
        }

        private static string CheckLowercaseLettersPresense(string inputLoginingData, bool isLogin = false)
        {
            Regex lowercaseEnglishLettersPresensePattern = new(@"(?=.*[a-z])");
            bool lowercaseEnglishLettersPresense = lowercaseEnglishLettersPresensePattern.IsMatch(inputLoginingData);

            Regex lowercaseRussianLettersPresensePattern = new(@"(?=.*[а-я])|(?=.*[А-Я])");
            bool lowercaseRussianLettersPresense = lowercaseRussianLettersPresensePattern.IsMatch(inputLoginingData);
            if (lowercaseEnglishLettersPresense == false && lowercaseRussianLettersPresense == false)
                return isLogin == true ? ("- Логин должен содержать буквы.\n") : ("- Пароль должен содержать буквы.\n");
            if (lowercaseRussianLettersPresense == true)
                return isLogin == true ? ("- Логин не должен содержать кириллицу.\n") : ("- Пароль не должен содержать кириллицу.\n");
            return "";
        }

        private static string CheckNumbersPresence(string inputPassword)
        {
            Regex numbersPresencePattern = new(@"(?=.*[0-9])");
            bool numbersPresence = numbersPresencePattern.IsMatch(inputPassword);
            if (numbersPresence == false)
                return "- Пароль должен содержать хотя бы 1 цифру.\n";
            return "";
        }

        private static string CheckUppercaseLettersPresense(string inputPassword)
        {
            Regex uppercaseLettersPresencePattern = new(@"(?=.*[A-Z])");
            bool uppercaseLettersPresence = uppercaseLettersPresencePattern.IsMatch(inputPassword);
            if (uppercaseLettersPresence == false)
                return "- Пароль должен содержать хотя бы 1 большую букву.\n";
            return "";
        }

        private static string CheckSpacesPresence(string inputLoginingData, bool isLogin = false)
        {
            Regex checkSpacesPresencePattern = new(@"(?=.* )");
            bool checkSpacesPresence = checkSpacesPresencePattern.IsMatch(inputLoginingData);
            if (checkSpacesPresence == true)
                return isLogin == true ? ("- Логин не должен содержать пробелы.\n") : ("- Пароль не должен содержать пробелы.\n");
            return "";
        }

        private static string CheckBannedSpecialSymbolsPresence(string inputLoginingData, bool isLogin = false)
        {
            Regex bannedSpecialSymbolsPresencePattern = new(@"(?=.*[<>@#%/])");
            bool bannedSpecialSymbolsPresence = bannedSpecialSymbolsPresencePattern.IsMatch(inputLoginingData);
            if (bannedSpecialSymbolsPresence == true)
                return isLogin == true ? ("- Логин не должен содержать следющие специальные символы: \"<\", \">\", \"@\", \"#\", \"%\", \"/\".\n") : ("- Пароль не должен содержать следющие специальные символы: \"<\", \">\", \"@\", \"#\", \"%\", \"/\".\n");
            return "";
        }
    }
}
