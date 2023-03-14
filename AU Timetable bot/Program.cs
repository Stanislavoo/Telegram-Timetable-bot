using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Timetablebot.Database;
using Timetablebot.Menu.Back;
using Timetablebot.Menu.Start;
using Timetablebot.Menu.Courses;
using Timetablebot.Menu.Groups;
using Timetablebot.Menu.Main;
using Timetablebot.Menu.Main.Statements;
using Timetablebot.Menu.Main.Subjects;
using Timetablebot.Menu.Main.Subject;
using Timetablebot.ButtonsOrder;
using Timetablebot.Menu.Edit;
using Timetablebot.Menu.Confirm;
using Timetablebot.Admin.RecoverAuthorizatoinData;
using Timetablebot.Admin.SignIn;

namespace TimetableBot
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("TOKEN");
        static void Main()
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Message == null)
                return;
            Timetablebot.Database.User currentUser = Timetablebot.Database.User.GetUser(update.Message!.Chat.Id.ToString());
            
            //загрузка файла
            if (update!.Type == UpdateType.Message && update.Message?.Document != null && currentUser.IsEditing == true && Button.FindButton(currentUser.EditingType!, "MaterialsEditButtons"))
            {
                var message = update.Message;
                EditMenu.StartEditing(botClient, message, currentUser);
            }

            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                var message = update.Message;
                if(currentUser.CurrentMessage == null && message.Text.ToLower() != "/start")
                {
                    message.Text = "/start";
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, бот был перезагружен. Выход в главное меню.");
                }

                if (Button.IsMessageButton(message.Text.ToLower()) == true || message.Text.ToLower() == "назад")
                    if (MenuButtonsOrder.CheckMessagePriority(message, currentUser) == false)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, нажми кнопку текущего меню.");
                        return;
                    }

                if(currentUser.IsRecovering == true)
                {
                    RecoverData.StartRecovering(botClient, message, currentUser);
                    if (currentUser.IsRecovering == true)
                        return;
                }

                if ((currentUser.CurrentMessage == "методист" && currentUser.IsMethodist == false) || (currentUser.CurrentMessage == "староста" && currentUser.IsHeadman == false && currentUser.IsMethodist == false))
                {
                    bool isLoginCheked = false;
                    if (currentUser.EnteredLogin == null)
                    {
                        await Login.GetLogin(botClient, message, currentUser);
                        isLoginCheked = true;
                    }
                    if (currentUser.EnteredLogin != null && isLoginCheked == false)
                        await Password.GetPassword(botClient, message, currentUser);
                }

                if (currentUser.IsEditing == true)//протестировать чтобы ничего не ломало
                {
                    EditMenu.StartEditing(botClient, message, currentUser);
                    if (currentUser.IsEditing == true)
                        return;
                }

                    if (message.Text.ToLower() == "назад")
                {
                    BackMenu.BackClick(currentUser);
                    message.Text = currentUser.CurrentMessage;
                    await HandleMessage(botClient, message, currentUser);
                    return;
                }

                MenuButtonsOrder.UpdateCurrentMessage(message.Text.ToLower(), currentUser);
                await HandleMessage(botClient, message, currentUser);
                return;
            }
        }

        static async Task HandleMessage(ITelegramBotClient botClient, Message message, Timetablebot.Database.User currentUser)
        {
            switch (message.Text?.ToLower())
            {
                case "/start":
                    MenuButtonsOrder.GetMessagePriority(0, currentUser);
                    await StartCommands.StartMessagingClick(botClient, message, currentUser);
                    break;

                case "startpage":
                    MenuButtonsOrder.GetMessagePriority(0, currentUser);
                    await StartCommands.BackToStartMenu(botClient, message, currentUser);
                    break;

                case "студент":
                    MenuButtonsOrder.GetMessagePriority(1, currentUser);
                    await StartMenu.StudentClick(botClient, message);
                    break;

                case "методист":
                    MenuButtonsOrder.GetMessagePriority(1, currentUser);
                    if(currentUser.EnteredLogin != null)
                    {
                        await StartMenu.MethodistClick(botClient, message, currentUser);
                        break;
                    }
                    await Login.StartLoginingClick(botClient, message);
                    break;

                case "1 курс":
                case "2 курс":
                case "3 курс":
                case "4 курс":
                    MenuButtonsOrder.GetMessagePriority(2, currentUser);
                    await CoursesMenu.CourseClick(botClient, message, currentUser);
                    break;

                case "гуп-1":
                case "гуп-2":
                case "гуп-3":
                case "гуп-4":
                case "гуэ-1":
                case "гуэ-2":
                case "гуэ-3":
                case "уир-1":
                case "уир-2":
                    MenuButtonsOrder.GetMessagePriority(3, currentUser);
                    await AllGroupsMenu.GroupClick(botClient, message, currentUser);
                    break;

                case "меню":
                    MenuButtonsOrder.GetMessagePriority(4, currentUser);
                    await GroupMenu.MenuClick(botClient, message, currentUser);
                    break;

                case "староста":
                    MenuButtonsOrder.GetMessagePriority(4, currentUser);
                    if (currentUser.EnteredLogin != null)
                    {
                        currentUser.IsHeadman = true;
                        Timetablebot.Database.User.UpdateUser(currentUser);
                        await GroupMenu.HeadmanClick(botClient, message, currentUser);
                        break;
                    }
                    await Login.StartLoginingClick(botClient, message);
                    break;
                    
                case "расписание":
                    MenuButtonsOrder.GetMessagePriority(5, currentUser);
                    await MainMenu.TimetableClick(botClient, message, currentUser);
                    break;

                case "заявления":
                    MenuButtonsOrder.GetMessagePriority(5, currentUser);
                    await MainMenu.StatementsClick(botClient, message);
                    break;

                case "предметы":
                    MenuButtonsOrder.GetMessagePriority(5, currentUser);
                    await MainMenu.SubjectsClick(botClient, message, currentUser);
                    break;

                case "сентябрь":
                case "октябрь":
                case "ноябрь":
                case "декабрь":
                case "февраль":
                case "март":
                case "апрель":
                case "май":
                    MenuButtonsOrder.GetMessagePriority(6, currentUser);
                    await MonthMenu.MonthClick(botClient, message, currentUser);
                    break;

                case "ия":
                case "фил":
                case "сп":
                case "ибг":
                case "игпзс":
                case "игпб":
                case "тгп":
                case "пол":
                case "итвуд":
                case "бя":
                case "бжч":
                case "фк":
                case "кч":
                case "дия":
                case "пу":
                case "гу":
                case "эо":
                case "кп":
                case "кпзс":
                case "ап":
                case "тп":
                case "гп":
                case "эдоив":
                case "иррб":
                case "фнп":
                case "уп":
                case "упр":
                case "гхпр":
                case "празо":
                case "семп":
                case "псо":
                case "уис":
                case "хп":
                case "ипр":
                case "крим":
                case "жилпр":
                case "кпр":
                case "онд":
                case "стрп":
                case "квп":
                case "сэ":
                case "уип":
                case "вм":
                case "мт":
                case "эт":
                case "дэпгс":
                case "пк":
                case "мсэ":
                case "фсг":
                case "полк":
                case "сэс":
                case "уо":
                case "грэ":
                case "увд":
                case "буиа":
                case "цо":
                case "улп":
                case "огз":
                case "эи":
                case "доуд":
                case "ахд":
                case "гчп":
                case "см":
                case "рцб":
                case "нин":
                case "ппвд":
                case "впрб":
                case "ом":
                case "авс":
                case "аип":
                case "пи":
                case "твимс":
                case "нирэб":
                case "ир":
                case "дэипк":
                case "дм":
                case "ос":
                case "поид":
                case "сбд":
                case "инфсит":
                case "самур":
                case "wt":
                case "уиб":
                case "прис":
                case "им":
                case "иад":
                case "уиго":
                case "лр":
                case "иппу":
                case "рчп":
                case "адпип":
                case "суд":
                case "мчп":
                case "пн":
                case "эп":
                case "тамп":
                case "бип":
                case "оп":
                case "гз":
                case "грид":
                case "исит":
                case "ппэ":
                case "оуис":
                case "фм":
                case "экп":
                case "ээ":
                case "ип":
                case "сбибт":
                case "вов":
                case "сиикпо":
                case "кг":
                case "уitп":
                case "ти":
                case "кс":
                case "тпр":
                case "этс":
                case "гмрпо":
                case "аис":
                case "эк-ка":
                case "исвэ":
                case "эиртс":
                    MenuButtonsOrder.GetMessagePriority(6, currentUser);
                    await SubjectsButtons.SubjectsButtonsClick(botClient, message, currentUser);
                    break;

                case "преподаватели":
                    MenuButtonsOrder.GetMessagePriority(7, currentUser);
                    await SubjectMenu.LecturersClick(botClient, message, currentUser);
                    break;

                case "материалы":
                    MenuButtonsOrder.GetMessagePriority(7, currentUser);
                    await SubjectMenu.MaterialsClick(botClient, message, currentUser);
                    break;

                case "изменить логин":
                    MenuButtonsOrder.GetEditingTypePriority(8, currentUser);
                    if(MenuButtonsOrder.CheckEditingTypePriority(currentUser) == true)
                    {
                        EditMenu.UpdateEditing(isEditing: true, currentUser, editingType: message.Text.ToLower());
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи пароль, чтобы начать изменение.", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                        break;
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Ошибка доступа. {message.Chat.FirstName}, нажми кнопку текущего меню.");
                    break;

                case "изменить пароль":
                    MenuButtonsOrder.GetEditingTypePriority(8, currentUser);
                    if (MenuButtonsOrder.CheckEditingTypePriority(currentUser) == true)
                    {
                        EditMenu.UpdateEditing(isEditing: true, currentUser, editingType: message.Text.ToLower());
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи старый пароль, чтобы начать изменение.", replyMarkup: ConfirmMenu.GetCanselAuthorizationEditingButton());
                        break;
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Ошибка доступа. {message.Chat.FirstName}, нажми кнопку текущего меню.");
                    break;

                case "редактировать":
                    MenuButtonsOrder.GetEditingTypePriority(10, currentUser);
                    if (MenuButtonsOrder.CheckEditingTypePriority(currentUser) == true)
                    {
                        EditMenu.UpdateEditing(isEditing: true, currentUser, editingType: message.Text.ToLower());
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, информация, отправленная твоим следующим сообщением, будет сохранена.", replyMarkup: ConfirmMenu.GetCanselFileEditingMenu());
                        break;
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Ошибка доступа. {message.Chat.FirstName}, нажми кнопку текущего меню.");
                    break;

                case "загрузить файл":
                    MenuButtonsOrder.GetEditingTypePriority(11, currentUser);
                    if(MenuButtonsOrder.CheckEditingTypePriority(currentUser) == true)
                    {
                        EditMenu.UpdateEditing(isEditing: true, currentUser, editingType: message.Text.ToLower());
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, отправь вложение, которое нужно загузить.\n" +
                            $"При отправке нескольких вложений будет загружено только первое из них.\n" +
                            $"Чтобы загрузить фото или видео отправь их в виде документа или убери галочку с \"отправить в сжатом виде\".", replyMarkup: ConfirmMenu.GetCanselFileAddingButton());
                        break;
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Ошибка доступа. {message.Chat.FirstName}, нажми кнопку текущего меню.");
                    break;

                case "удалить файл":
                    MenuButtonsOrder.GetEditingTypePriority(11, currentUser);
                    if (MenuButtonsOrder.CheckEditingTypePriority(currentUser) == true)
                    {
                        EditMenu.UpdateEditing(isEditing: true, currentUser, editingType: message.Text.ToLower());
                        StorageEdit.GetFilesListToDelete(botClient, message, currentUser);
                        break;
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Ошибка доступа. {message.Chat.FirstName}, нажми кнопку текущего меню.");
                    break;

                case "удалить все файлы":
                    MenuButtonsOrder.GetEditingTypePriority(11, currentUser);
                    if (MenuButtonsOrder.CheckEditingTypePriority(currentUser) == true)
                    {
                        EditMenu.UpdateEditing(isEditing: true, currentUser, editingType: message.Text.ToLower());
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, подтверди удаление.", replyMarkup: ConfirmMenu.GetConfirmDeletingMenu());
                        break;
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Ошибка доступа. {message.Chat.FirstName}, нажми кнопку текущего меню.");
                    break;

                case "очистить данные всех курсов":
                    MenuButtonsOrder.GetEditingTypePriority(9, currentUser);
                    if (MenuButtonsOrder.CheckEditingTypePriority(currentUser) == true)
                    {
                        EditMenu.UpdateEditing(isEditing: true, currentUser, editingType: message.Text.ToLower());
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{message.Chat.FirstName}, введи пароль, чтобы подтвердить удаление.", replyMarkup: ConfirmMenu.GetConfirmDeletingButton());
                        break;
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Ошибка доступа. {message.Chat.FirstName}, нажми кнопку текущего меню.");
                    break;
            }
        }

        static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }  
}