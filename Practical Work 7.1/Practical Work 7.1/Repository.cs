using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Practical_Work_7._1
{
    struct Repository
    {
        #region Чтение файла
        public Worker[] GetAllWorker()
        {
            string path = @"E:\Notepad.txt";

            FileInfo fileInfo = new FileInfo(path);

            List<Worker> workers = new List<Worker>();

            if (fileInfo.Exists)
            {
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    Worker worker = new Worker();

                    workers.Add(worker);

                    Console.WriteLine(line);
                }

            }
            return workers.ToArray();
        }
        #endregion
        #region Поиск по ID
        public Worker GetWorkerbyId(int id)
        {
            string path = @"E:\Notepad.txt";

            FileInfo fileInfo = new FileInfo(path);

            Worker worker = new Worker();

            if (!fileInfo.Exists)
            {
                Console.WriteLine("Файл не найден");
            }

            using (StreamReader reader = new StreamReader(path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {

                    string[] strings = line.Split(',');

                    int workerId;
                    if (int.TryParse(strings[0], out workerId) && workerId == id)
                    {
                        worker.Id = id;
                        worker.Name = strings[2];
                        break;
                    }

                }
            }
            return worker;
        }
        #endregion
        #region Добавление нового сотрудника
        public void AddWorker(Worker worker)
        {
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

            if (fileInfo.Exists)
            {
                File.AppendAllText(@"E:\Notepad.txt", employerData + Environment.NewLine);

                Console.WriteLine("Сотрудник добавлен");
            }
        }
        #endregion
        public void RemoveWorker(int id)
        {
            string path = @"E:\Notepad.txt";

            if (File.Exists(path))
            {
                List<Worker> workers = new List<Worker>();

                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    Worker worker = new Worker
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[2],


                    };
                    workers.Add(worker);
                }
                workers.RemoveAll(worker => worker.Id == id);

                using (StreamWriter sw = new StreamWriter(path)) 
                {
                    foreach (Worker worker in workers)
                    {
                        sw.WriteLine($"{worker.Id}, {worker.Name}");
                    }
                }
            }
        }
    }
    //public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo) 
    //{

    //}
} 

