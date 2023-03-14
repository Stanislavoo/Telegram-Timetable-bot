# Telegram-Timetable-bot
Description: 
The program is a telegram student assistant bot(developed for students of the management academy). This bot supports functions of viewing/downloading/editing the
schedule, university course information, as well as file attachments. The bot implements interfaces for methodist, student-headman and common student, where the 
headman and methodist have the prerogatives of "administrators" of different levels. The system of authorisation to the account of the headmen and methodist is 
implemented, as well as changing and restoring the login data “methodist” and “headmen”.

Launch: 
To run the program, you need to insert your bot's telegram token obtained from Bot Father in the "TOKEN" field in the program.cs file, as well as the 
connection string in your database in the "CONNECTION STRING" field in the ApplicationContext.cs file. Next, you need to load all the .xlsx files except  
"HeadmansAccountsDatabase.xlsx" and "MethodistsAccountsDatabase.xlsx" into the database. The files "HeadmansAccountsDatabase.xlsx" and "MethodistsAccountsDatabase.xlsx" 
do not need to load, they contain the standard logins, passwords, as well as keys to restore the login data from the "methodist" and "headman" in unhesitating form  
(program supports the function of changing logins and passwords, as well as their recovery through the interface bot), which should be provided to the appropriate 
users. 
After performing all of the above steps, the bot is ready to run.


Описание:
Программа представляет собой телеграм бот-помощник студента(разрабатывался для студентов академии управления).
Данный бот поддерживает функции просмотра/загрузки/редактирования расписания, информации об университетских предметах,
а также файловых вложений. В боте реализованы интерфейсы методиста, студента-старосты и простого студента, где староста и методист имеют
привилегии "администраторов" разных уровней. Реализована система авторизации в аккаунт "старосты" и "методиста", изменение и восстановление данных
входа "методистов" и "старост".

Запуск:
Чтобы запустить программу нужно вставить токен своего бота телеграм, полученный у Bot Father в поле "TOKEN" в файле program.cs, а также строку
подключения в своей базе данных в поле "CONNECTION STRING" в файле ApplicationContext.cs. Далее необходимо загрузить все .xlsx файлы кроме 
"HeadmansAccountsDatabase.xlsx" и "MethodistsAccountsDatabase.xlsx" в базу данных. Файлы "HeadmansAccountsDatabase.xlsx" и "MethodistsAccountsDatabase.xlsx"
загружать не нужно, они содержат стандартные логины, пароли, а так же ключи восстановления данных входа от "методистов" и "старост" в незахешированном виде 
(программа поддерживает функцию изменения логинов и паролей, а также их восстановление через интерфейс бота), которые необходимо предоставить соответствующим
пользователям.
После выполнения всех вышеперечисленных действий бот готов к запуску.
