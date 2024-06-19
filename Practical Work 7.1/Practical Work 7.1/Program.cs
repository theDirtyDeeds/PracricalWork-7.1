using System.ComponentModel.Design;

namespace Practical_Work_7._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите действие: \n1:Запись.\n2:Чтение.\n3:Поиск по ID.\n4:Добавить нового сотрудника.\n5:Удалить сотрудника.\n6:Сортировка. ");
            
            int userChoise = Convert.ToInt32(Console.ReadLine());

            Repository repository = new Repository();

            switch (userChoise) 
            {
                case 1:
                    CreateWorker();
                    break;
                case 2:
                    repository.GetAllWorker();
                    break;
                case 3:
                    Console.WriteLine("Введите ID: ");

                    if(int.TryParse(Console.ReadLine(), out int id))
                    {
                        Worker worker = repository.GetWorkerbyId(id);

                        if (worker.Id != 0)
                        {
                            Console.WriteLine($"{worker.Id}, {worker.Name}");
                        }
                        else
                        {
                            Console.WriteLine("Сотрудник не найден");
                        }  
                    }
                    else 
                    {
                        Console.WriteLine("Некорректный ID");
                    }
                    break;
                case 4:
                    repository.AddWorker(new Worker());
                    break;
                case 5:
                    Console.WriteLine("Введите ID: ");

                    if(int.TryParse(Console.ReadLine(), out int idToDelete))
                    {
                        repository.RemoveWorker(idToDelete);

                        Console.WriteLine("Сотрудник удален");
                    }
                    else
                    {
                        Console.WriteLine("Сотрудник не найден");
                    }
                    break;
                case 6:
                    Console.WriteLine("Введите дату начала (дд.мм.гггг)");

                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dateFrom))
                    {
                        Console.WriteLine("Введите дату окончания(дд.мм.гггг)");

                        if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTo))
                        {
                            repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);

                            foreach (Worker worker in repository.GetAllWorker())
                            {
                                Console.WriteLine($"{worker.Id},{worker.Name}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некорректная дата окончания");
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Некорректная дата начала");
                    }
                    break;
            }

            
        }
        #region Создание файла и добавлние 1-го сотрудника
        /// <summary>
        /// Создание 1-го сотрудника
        /// </summary>
        static string CreateWorker()
        {
            Worker worker = new Worker();

            Console.WriteLine("Введите данные сотрудника: ");

            Console.WriteLine("Введите ФИО: ");

            worker.Name = Console.ReadLine();

            Console.WriteLine("Возраст: ");

            worker.Age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Рост: ");

            worker.Height = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Дата рождения: ");

            worker.Birthday = Console.ReadLine();

            Console.WriteLine("Место рождения: ");

            worker.Birthplace = Console.ReadLine();

            worker.Id = 1;

            if (File.Exists(@"E:\Notepad.txt"))
            {
                worker.Id = File.ReadAllLines(@"E:\Notepad.txt").Length + 1;
            }

            string employerData = $"{worker.Id}, {DateTime.Now:g}, {worker.Name}, {worker.Age}, {worker.Height}, {worker.Birthday}, {worker.Birthplace}";

            string[] employer = employerData.Split(" ");

            foreach (string e in employer)
            {
                Console.WriteLine(e);
            }
            string path = @"E:\Notepad.txt";

            FileInfo fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(employerData);

                    Console.WriteLine("Сотрудник добавлен");
                }
            }
            return employerData;  
        }
        #endregion
    }

}
