using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchCriminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CriminalDataBase criminalDataBase = new CriminalDataBase();
            criminalDataBase.Work();
        }
    }

    class CriminalDataBase
    {
        private const int criminalsCount = 100000;

        private List<Criminal> _criminals;
        private CriminalCreater _criminalCreater = new CriminalCreater();

        public CriminalDataBase()
        {
            _criminals = new List<Criminal>();

            for (int i = 0; i < criminalsCount; i++)
            {
                _criminals.Add(_criminalCreater.CreateCriminal());
            }
        }

        public void Work()
        {
            Console.WriteLine("Поиск преступника");

            Console.WriteLine("Введите рост:");
            int height = GetNumber();

            Console.WriteLine("Введите вес:");
            int weight = GetNumber();

            Console.WriteLine("Введите национальность:");
            string nationality = Console.ReadLine();

            ShowCriminalsByParameters(height, weight, nationality);
        }

        private void ShowCriminals(List<Criminal> criminals)
        {
            if (criminals.Count > 0)
            {
                foreach (var criminal in criminals)
                {
                    Console.WriteLine($"{criminal.FullName} - {criminal.GetStatus()}\n" +
                    $"Рост: {criminal.Height} Вес: {criminal.Weight} Национальность: {criminal.Nationality}\n");
                }
            }
            else
            {
                Console.WriteLine("Нет таких преступников");
            }
        }

        private void ShowCriminalsByParameters(int height, int weight, string nationality)
        {
            var filteredCriminals = _criminals
                .Where(criminal => criminal.IsConcluded == false)
                .Where(criminal => criminal.Nationality.ToLower() == nationality.ToLower())
                .Where(criminal => criminal.Weight == weight)
                .Where(criminal => criminal.Height == height)
                .ToList();

            ShowCriminals(filteredCriminals);
        }

        private int GetNumber()
        {
            int number;

            while (int.TryParse(Console.ReadLine(), out number) == false)
                Console.WriteLine("Ошибка ввода!");

            return number;
        }
    }

    class Criminal
    {

        public Criminal(string fullName, bool isConcluded, string nationality, int height, int weight)
        {
            FullName = fullName;
            IsConcluded = isConcluded;
            Nationality = nationality;
            Height = height;
            Weight = weight;
        }

        public string FullName { get; private set; }
        public bool IsConcluded { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public string Nationality { get; private set; }

        public string GetStatus()
        {
            if (IsConcluded)
            {
                return "Под стражей";
            }
            else
            {
                return "Свободен";
            }
        }
    }

    class CriminalCreater
    {
        private CriminalFullNamesGenerator _criminalFullNamesGenerator = new CriminalFullNamesGenerator();
        private NationalitiesGenerator _nationalitiesGenerator = new NationalitiesGenerator();

        public Criminal CreateCriminal()
        {
            const int MaximumHeight = 110;
            const int MinimumHeight = 100;
            const int MaximumWeight = 110;
            const int MinimumWeight = 100;

            string fullName = _criminalFullNamesGenerator.GenerateRandomFullName();
            bool isConcluded = Convert.ToBoolean(UserUtils.GenerateRandomNumber(2));
            string nationality = _nationalitiesGenerator.GenerateRandomFullName();
            int height = UserUtils.GenerateRandomNumber(MinimumHeight, MaximumHeight);
            int weight = UserUtils.GenerateRandomNumber(MinimumWeight, MaximumWeight);

            return new Criminal(fullName, isConcluded, nationality, height, weight);
        }
    }

    class NationalitiesGenerator
    {
        private List<string> _nationalities;

        public NationalitiesGenerator()
        {
            _nationalities = new List<string>();
            _nationalities.Add("Русский");
            _nationalities.Add("Украинец");
            _nationalities.Add("Американец");
            _nationalities.Add("Немец");
            _nationalities.Add("Бурят");
            _nationalities.Add("Чеченец");
            _nationalities.Add("Башкир");
        }

        public string GenerateRandomFullName()
        {
            return _nationalities[UserUtils.GenerateRandomNumber(_nationalities.Count)];
        }
    }

    class CriminalFullNamesGenerator
    {
        private List<string> _fullNames;

        public CriminalFullNamesGenerator()
        {
            _fullNames = new List<string>();
            _fullNames.Add("Пупкин Василий Викторович");
            _fullNames.Add("Сафронов Алексей Николаевич");
            _fullNames.Add("Табуретов Биба Васильевич");
            _fullNames.Add("Табуретов Боба Васильевич");
            _fullNames.Add("Котофеев Нурлан Барсикович");
            _fullNames.Add("Рыбаков Александр Юрьевич");
            _fullNames.Add("Владимиров Владимир Владимирович");
        }

        public string GenerateRandomFullName()
        {
            return _fullNames[UserUtils.GenerateRandomNumber(_fullNames.Count)];
        }
    }

    static class UserUtils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int maximum)
        {
            return _random.Next(0, maximum);
        }

        public static int GenerateRandomNumber(int minimum, int maximum)
        {
            return _random.Next(minimum, maximum);
        }
    }
}