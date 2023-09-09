using System.Windows;

namespace DateB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Solve(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = DP.SelectedDate.Value;
                if (date >= DateTime.Now.Date)
                {
                    MessageBox.Show("Выбрана неверная дата", "Ошибка");
                    Result.Visibility = Visibility.Collapsed;
                }
                else
                {
                    int day = 0;
                    int month = 0, year = 0;
                    for (int i = 0; i <= DateTime.Now.Year - date.Year; i++)
                    {
                        if (date.Year + i != DateTime.Now.Year)
                        {
                            if (date.Month < DateTime.Now.Month || date.Month == DateTime.Now.Month && date.Day <= DateTime.Now.Day)
                            {
                                year++;
                            }
                            else if (date.Year + i < DateTime.Now.Year - 1) year++;
                        }
                    }
                    day = (DateTime.Now - date).Days;
                    month = 12 * year;
                    if (month == 0)
                    {
                        if (date.Day <= DateTime.Now.Day) month += DateTime.Now.Month + (12 - date.Month);
                        else if (date.Day > DateTime.Now.Day) month += DateTime.Now.Month + (12 - date.Month) - 1;
                    }
                    else if (date.Month < DateTime.Now.Month) month += DateTime.Now.Month - date.Month;
                    else if (date.Month > DateTime.Now.Month) month += (12 - date.Month) + DateTime.Now.Month;

                    Years.Text = year.ToString();
                    Month.Text = month.ToString();
                    Days.Text = day.ToString();
                    Result.Visibility = Visibility.Visible;

                    DayWeek.Text = date.DayOfWeek.ToString();
                    int dayOfWeek = 0;
                    int countDayLeapYear = 0;
                    string leapYear = "";
                    for(int i = date.Year; i <= date.Year + year; i++)
                    {
                        DateTime tmpDate = new DateTime(i, date.Month, date.Day);
                        if (tmpDate.DayOfWeek == date.DayOfWeek) dayOfWeek++;
                        if(i % 4 == 0)
                        {
                            if (i % 100 != 0)
                            {
                                countDayLeapYear++;
                                leapYear += $" {i}";
                            }
                            else if (i % 400 == 0)
                            {
                                countDayLeapYear++;
                                leapYear += $" {i}";
                            }
                        }
                    }
                    DayWeekAll.Text = dayOfWeek.ToString();
                    CountDayLeapYear.Text = countDayLeapYear.ToString();
                    LeapYear.Text = leapYear.ToString();
                }
            }
            catch
            {
                Result.Visibility = Visibility.Collapsed;
                MessageBox.Show("Выбрана неверная дата", "Ошибка");
            }
            
        }
    }
}