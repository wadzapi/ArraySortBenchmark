using System;
using System.IO;

namespace Benchmark
{
    /// <summary>
    /// Класс для записи результатов тестов в текстовый лог
    /// </summary>
    public class TestLog
    {
        private static StreamWriter Writer;//Для записи потока нового файла
        private FileInfo FILog;//Информация о файле лога
        private FileStream FSLog; //Файловый поток лога
        private string logPath; //Путь к логу
        /// <summary>
        /// Дополнять существующий с совпадающим именем / создавать новый с переименованием
        /// </summary>
        public bool Append = false;
        public const string logSeparator = " ** "; //Символы, разделяющие записи в логе
        private const string lineSeparator = "===============================================================";
        private const string extension = ".txt"; //Расширение лога
        private int uniqueAppendix = 0; //Для генерации уникального имени лога

        /// <summary>
        /// Текстовый лог
        /// </summary>
        /// <param name="LogPath">Полный путь, включая имя файла</param>
        public TestLog(string LogPath)
        {
            this.LogPath = LogPath;
        }
        public TestLog()
        {
        }

        #region Создание и открытие лога

        /// <summary>
        /// Проверка имени файла, создание FileInfo
        /// </summary>
        /// <param name="Name">строка формата "Путь\Имя файла"</param>
        private void uniqueFileName(string Name)
        {
            while (File.Exists(Name + (++uniqueAppendix).ToString() + extension))
            {
            }
            logPath = Name + uniqueAppendix.ToString() + ".txt";
            createFILog();
        }

        /// <summary>
        /// Метод создания FileInfo для файла лога
        /// </summary>
        /// <param name="LogPath">Полный путь, включая имя файла</param>
        private  void createFILog()
        {
            try
            {
                FILog = new FileInfo(logPath);
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                    throw new ArgumentNullException("Не зададан полный путь к файлу лога!");
                if (e is System.Security.SecurityException)
                    throw new System.Security.SecurityException("Нет прав доступа!");
                if (e is ArgumentException)
                    throw new ArgumentException("Недопустимые символы в пути к логу!");
                if (e is UnauthorizedAccessException)
                    throw new UnauthorizedAccessException("Доступ к файлу лога запрещен!");
                if (e is PathTooLongException)
                    throw new PathTooLongException("Перывышен max символов для пути!");
                if (e is NotSupportedException)
                    throw new Exception("Символ : используется только как разделитель после имени диска!");
            }
        }

        /// <summary>
        /// Метод для создания FileStream для текстового лога
        /// </summary>
        /// <param name="mode">Режим записи в лог</param>
        private  void newFSLog()//Метод для создания потока файла лога
        {
            //Попытка создать поток из файлу, указанного в logSource FileInfo
            try
            {
                FSLog = FILog.Open(FileMode.Append, FileAccess.Write);
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException)//Если указанный в path каталог не найден
                {
                    FILog.Directory.Create();//Создаем такой каталог
                }
                if (e is IOException)
                    throw new IOException("Файл открыт и используется другим процессом");
            }
        }
        #endregion


        #region Запись в лог
        /// <summary>
        /// Запись в лог значений
        /// </summary>
        /// <param name="obj1">Значение 1</param>
        /// <param name="obj2">Значение 2</param>
        public void WriteLine(Object obj1, Object obj2)
        {
            newFSLog();//Создание файлового потока
            Writer = new StreamWriter(FSLog);//Создаем SteamWriter
            Writer.WriteLine(logSeparator + obj1.ToString() + logSeparator + obj2.ToString()); //Записываем строку в файл
            Writer.Close(); //Закрываем потоки
            FSLog.Close();
        }

        /// <summary>
        /// Запись строки в лог
        /// </summary>
        /// <param name="LogString">Строка для записи</param>
        public void WriteLine(string LogString)
        {
            newFSLog();//Создание файлового потока
            Writer = new StreamWriter(FSLog);//Создаем SteamWriter
            Writer.WriteLine(LogString); //Записываем строку в файл
            Writer.Close(); //Закрываем потоки
            FSLog.Close();
        }


        /// <summary>
        /// Вставка разделителя строк
        /// </summary>
        public void Separate()
        {
            newFSLog();//Создание файлового потока
            Writer = new StreamWriter(FSLog);//Создаем SteamWriter
            Writer.WriteLine(lineSeparator); //Записываем строку в файл
            Writer.Close(); //Закрываем потоки
            FSLog.Close();
        }
        #endregion

        #region Свойства
        /// <summary>
        /// Задает и получает путь к файлу лога
        /// </summary>
        public string LogPath
        {
            get
            {
                return logPath;
            }
            set
            {
                if (Append)
                {
                    logPath = value+extension;
                    createFILog();
                }
                else
                {
                    uniqueFileName(value);
                }
            }
        }
            
        #endregion

    }
}
