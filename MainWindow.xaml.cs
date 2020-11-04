using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculateStage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculating calculating = new Calculating();
        private DateTime date1;
        private DateTime date2;
        public DateTime Date1{get { return date1; }}
        public DateTime Date2{get { return date2; }}

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (date1 < date2)
            {
                calculating.Start(out string currentStage, out string totalStage);
                lblCurrentStage.Content = currentStage;
                lblTotalStage.Content = totalStage;
            }
            else
            {
                MessageBox.Show("Вторая дата должна быть больше первой!\nПовторите ввод!");
                tbDate1.Text = "";
                tbDate2.Text = "";
            }
        }

        private void tbDate1_LostFocus(object sender, RoutedEventArgs e)
        {
            calculating.GetDate(tbDate1.Text, out date1);
            if (date1 == DateTime.MinValue)
            {
                MessageBox.Show("Неверный формат даты поступления на работу!\nПовторите ввод!");
                tbDate1.Text = "";
            }
        }

        private void tbDate2_LostFocus(object sender, RoutedEventArgs e)
        {
            calculating.GetDate(tbDate2.Text, out date2);
            if (date2 == DateTime.MinValue)
            {
                MessageBox.Show("Неверный формат даты увольнения с работы!\nПовторите ввод!");
                tbDate2.Text = "";
            }
        }
    }
}
