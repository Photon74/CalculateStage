using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CalculateStage;

namespace CalculateStage
{
    /// <summary>
    /// Логика подсчета рабочего стажа
    /// </summary>
    class Calculating
    {
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
            DateTime.TryParseExact(d, formats, ruRU, DateTimeStyles.None, out date);
        }

        //Считаем общий стаж
        public void CalculateTotalStage(int years, int months, int days, ref int totalYears, ref int totalMonths, ref int totalDays)
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
        public void CalculateCurrentStage(DateTime date1, DateTime date2, ref int Years, ref int Months, ref int Days)
        {
            DateTime tempDate = new DateTime((date2 - date1).Ticks);
            Years = tempDate.Year - 1;
            Months = tempDate.Month - 1;
            Days = tempDate.Day - 1;
        }

        //Выводим стаж на печать
        public string PrintStage(string str, int years, int months, int days)
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