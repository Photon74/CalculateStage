﻿using System;
using System.Windows;

namespace CalculateStage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calculating calculating = new Calculating();

        private DateTime dateFrom;
        private DateTime dateTo;
        public DateTime DateFrom { get { return dateFrom; } }
        public DateTime DateTo { get { return dateTo; } }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Message(int field)
        {
            switch (field)
            {
                case 1:
                    {
                        MessageBox.Show("Неверный формат даты поступления на работу!\nПовторите ввод!");
                        tbDateFrom.Text = "";
                    };
                    break;
                case 2:
                    {
                        MessageBox.Show("Неверный формат даты увольнения с работы!\nПовторите ввод!");
                        tbDateTo.Text = "";
                    };
                    break;
                case 3:
                    {
                        MessageBox.Show("Вторая дата должна быть больше первой!\nПовторите ввод!");
                        tbDateFrom.Text = "";
                        tbDateTo.Text = "";
                    }
                    break;
                default:
                    {
                        tbDateFrom.Text = "";
                        tbDateTo.Text = "";
                    }
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int years = 0, months = 0, days = 0;
            int totalYears = 0, totalMonths = 0, totalDays = 0;

            calculating.GetDate(tbDateFrom.Text, out dateFrom);
            if (dateFrom == DateTime.MinValue) Message(1);
            else
            {
                calculating.GetDate(tbDateTo.Text, out dateTo);
                if (dateTo == DateTime.MinValue) Message(2);
                else
                if (dateFrom > dateTo) Message(3);
                else
                {
                    calculating.CalculateCurrentStage(DateFrom, DateTo, ref years, ref months, ref days);
                    lblCurrentStage.Content = calculating.PrintStage("", years, months, days);
                    calculating.CalculateTotalStage(years, months, days, ref totalYears, ref totalMonths, ref totalDays);
                    lblTotalStage.Content = calculating.PrintStage("", totalYears, totalMonths, totalDays);
                    Message(4);
                }
            }
        }
    }
}