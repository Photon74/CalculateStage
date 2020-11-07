using System;
using System.Globalization;
using System.Linq;

namespace CalculateStage
{
    /// <summary>
    /// Логика подсчета рабочего стажа
    /// </summary>
    class Calculating
    {
        /// <summary>
        /// Получаем начальную и конечную даты
        /// </summary>
        /// <param name="d">Дата введенная пользователем</param>
        /// <param name="date">Перевод даты в формат DateTime</param>
        public void GetDate(string d, out DateTime date)
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

        /// <summary>
        /// Подсчет текущего стажа
        /// </summary>
        /// <param name="dateFrom">Дата поступления на работу</param>
        /// <param name="dateTo">Дата увольнения с работы</param>
        /// <param name="Years">Стаж (годы)</param>
        /// <param name="Months">Стаж (месяцы)</param>
        /// <param name="Days">Стаж (дни)</param>
        public void CalculateCurrentStage(DateTime dateFrom, DateTime dateTo, ref int Years, ref int Months, ref int Days)
        {
            DateTime tempDate = new DateTime((dateTo - dateFrom).Ticks);
            Years = tempDate.Year - 1;
            Months = tempDate.Month - 1;
            Days = tempDate.Day - 1;
        }

        /// <summary>
        /// Подсчет общего стажа
        /// </summary>
        /// <param name="years">Текущий стаж (годы)</param>
        /// <param name="months">Текущий стаж (месяцы)</param>
        /// <param name="days">Текущий стаж (дни)</param>
        /// <param name="totalYears">Общий стаж (годы)</param>
        /// <param name="totalMonths">Общий стаж (месяцы)</param>
        /// <param name="totalDays">Общий стаж (дни)</param>
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

        /// <summary>
        /// Формирование строки вывода подсчитанного стажа
        /// </summary>
        /// <param name="str">Текстовое сообщение (необязательно)</param>
        /// <param name="years">Стаж (годы)</param>
        /// <param name="months">Стаж (месяцы)</param>
        /// <param name="days">Стаж (дни)</param>
        /// <returns></returns>
        public string PrintStage(string str, int years, int months, int days)
        {
            string[] arrayYearsWords = new string[] { "год", "лет", "года" };
            string[] arrayMonthsWords = new string[] { "месяц", "месяцев", "месяца" };
            string[] arrayDaysWords = new string[] { "день", "дней", "дня" };

            return $"{str} {years} {ManyOrOne(years, arrayYearsWords)} {months} {ManyOrOne(months, arrayMonthsWords)} {days} {ManyOrOne(days, arrayDaysWords)}";
        }

        /// <summary>
        /// Выбираем правильное слово для лет, месяцев и дней
        /// </summary>
        /// <param name="a">Кол-во лет, месяцев или дней</param>
        /// <param name="b">Список слов для выбора</param>
        /// <returns></returns>
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