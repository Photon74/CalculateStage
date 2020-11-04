using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CalculateStage
{
    /// <summary>
    /// Логика подсчета рабочего стажа
    /// </summary>
    class Calculating
    {
        MainWindow mainWindow = new MainWindow();
        public void Start(out string currentStage, out string totalStage)
        {
            int totalYears = 0, totalMonths = 0, totalDays = 0;
            int years = 0, months = 0, days = 0;

            //GetDate(d1, out DateTime date1);
            //GetDate(d2, out DateTime date2);
            CalculateCurrentStage(mainWindow.Date1, mainWindow.Date2, ref years, ref months, ref days);
            currentStage = PrintStage("Ваш стаж на данной работе составляет: ", years, months, days);
            CalculateTotalStage(years, months, days, ref totalYears, ref totalMonths, ref totalDays);
            totalStage = PrintStage("Ваш общий стаж составляет: ", totalYears, totalMonths, totalDays);
        }

        public string Information(string v)
        {
            return $"{v}";
        }

        public void GetDate(string d, out DateTime date) //Получаем начальную и конечную даты
        {
            date = default;
            CultureInfo ruRU = new CultureInfo("ru-RU");
            string[] formats =
            {
                "dd/MM/yyyy", "dd-MM-yyyy", "dd.MM.yyyy", "dd,MM,yyyy",
                "d/MM/yyyy",  "d-MM-yyyy",  "d.MM.yyyy",  "d,MM,yyyy",
                "dd/M/yyyy",  "dd-M-yyyy",  "dd.M.yyyy",  "dd,M,yyyy",
                "d/M/yyyy",   "d-M-yyyy",   "d.M.yyyy",   "d,M,yyyy"
            };

            bool checkup = true;
            while (checkup)
            {
                if (DateTime.TryParseExact(d, formats, ruRU, DateTimeStyles.None, out date))
                {
                    checkup = false;
                }
                else
                {
                    Information("Неверный формат даты!");
                    break;
                }
            }
        }

        //Считаем общий стаж
        void CalculateTotalStage(int years, int months, int days, ref int totalYears, ref int totalMonths, ref int totalDays)
        {
            totalYears += years;

            if ((totalMonths + months) > 11)
            {
                totalMonths = totalMonths + months - 12;
                ++totalYears;
            }
            else totalMonths += months;

            if ((totalDays + days) > 29)
            {
                totalDays = totalDays + days - 30;
                ++totalMonths;
            }
            else totalDays += days;
        }

        //Считаем текущий стаж
        void CalculateCurrentStage(DateTime date1, DateTime date2, ref int years, ref int months, ref int days)
        {
            //int years = 0, months = 0, days = 0;
            if (date1 < date2)
            {
                DateTime tempDate = new DateTime((date2 - date1).Ticks);
                years = tempDate.Year - 1;
                months = tempDate.Month - 1;
                days = tempDate.Day - 1;
            }
            else
            {
                Information("Вторая дата должна быть больше первой!");
            }
        }

        //Выводим стаж на печать
        string PrintStage(string str, int years, int months, int days)
        {
            string[] arrayYearsWords = new string[] { "год", "лет", "года" };
            string[] arrayMonthsWords = new string[] { "месяц", "месяцев", "месяца" };
            string[] arrayDaysWords = new string[] { "день", "дней", "дня" };

            return $"{str} {years} {ManyOrOne(years, arrayYearsWords)} {months} {ManyOrOne(months, arrayMonthsWords)} {days} {ManyOrOne(days, arrayDaysWords)}";
        }

        //Выбираем правильное слово для лет, месяцев и дней
        string ManyOrOne(int a, string[] b)
        {
            int[] x = a.ToString().ToCharArray().Select(y => y - '0').ToArray();
            if (a < 5 || a > 20)
            {
                if (x[^1] == 1) return b[0];
                if (x[^1] > 1 && x[^1] <= 4) return b[2];
            }
            return b[1];
        }
    }

}