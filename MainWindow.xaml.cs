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
                if (CalVest.IsChecked == false && CalSlav.IsChecked == false)
                {
                    MessageBox.Show("Выберите тип календаря", "Ошибка");
                    Result.Visibility = Visibility.Collapsed;
                }
                else if (date > DateTime.Now.Date || date == null)
                {
                    MessageBox.Show("Выбрана неверная дата", "Ошибка");
                    Result.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Calendar.Visibility = Visibility.Collapsed;
                    ButtonThrow.Visibility = Visibility.Visible;
                    ButtonSolve.Visibility = Visibility.Collapsed;
                    DateBlock.Visibility = Visibility.Collapsed;
                    Scroll.Visibility = Visibility.Visible;

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

                    if (year == 0)
                    {
                        if (DateTime.Now.Day < date.Day) month = DateTime.Now.Month - date.Month - 1;
                        else month = DateTime.Now.Month - date.Month;

                        if (month == 0 && date.Day != DateTime.Now.Day) day = (DateTime.Now - date).Days;
                        else if (date.Day == DateTime.Now.Day) day = 0;
                        else
                        {
                            DateTime tmp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day);
                            if (DateTime.Now.Day > date.Day) day = DateTime.Now.Day - tmp.Day;
                            else day = DateTime.Now.Day + (DateTime.DaysInMonth(DateTime.Now.Year, (DateTime.Now.Month - 1)) - tmp.Day) - 1;
                        }
                    }
                    else
                    {
                        if((int)DateTime.Now.Month > (int)date.Month)
                        {
                            if (DateTime.Now.Day < date.Day) month = DateTime.Now.Month - date.Month - 1;
                            else month = DateTime.Now.Month - date.Month;
                        }
                        else
                        {
                            if (DateTime.Now.Day < date.Day) month = DateTime.Now.Month + (12 - date.Month) - 1;
                            else month = DateTime.Now.Month + (12 - date.Month);
                        }
                        
                        if (DateTime.Now.Day < date.Day) day = DateTime.Now.Day + (DateTime.DaysInMonth(DateTime.Now.Year, (DateTime.Now.Month - 1)) - date.Day) - 1;
                        else if (DateTime.Now.Day > date.Day) day = DateTime.Now.Day - date.Day;
                    }

                    Life.Text = $"{year} лет, {month} месяцев, {day} дней.";
                    Result.Visibility = Visibility.Visible;

                    DayWeek.Text = date.DayOfWeek.ToString();
                    int dayOfWeek = 0;
                    int countDayLeapYear = 0;
                    string leapYear = "";
                    for (int i = date.Year; i <= date.Year + year; i++)
                    {
                        DateTime tmpDate = new DateTime(i, date.Month, date.Day);
                        if (tmpDate.DayOfWeek == date.DayOfWeek) dayOfWeek++;
                        if (i % 4 == 0)
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

                    //Китайский календарь 
                    if (CalVest.IsChecked == true)
                    {
                        SlavicCalendar.Visibility = Visibility.Collapsed;
                        EasternCalendar.Visibility = Visibility.Visible;
                        switch (date.Year % 12)
                        {
                            case 0:
                                YearAnimal.Text = " обезьяна";
                                break;
                            case 1:
                                YearAnimal.Text = " петух";
                                break;
                            case 2:
                                YearAnimal.Text = " собака";
                                break;
                            case 3:
                                YearAnimal.Text = " свинья (кабан";
                                break;
                            case 4:
                                YearAnimal.Text = " крыса (мышь)";
                                break;
                            case 5:
                                YearAnimal.Text = " бык";
                                break;
                            case 6:
                                YearAnimal.Text = " тигр";
                                break;
                            case 7:
                                YearAnimal.Text = " кролик (кот)";
                                break;
                            case 8:
                                YearAnimal.Text = " дракон";
                                break;
                            case 9:
                                YearAnimal.Text = " змея";
                                break;
                            case 10:
                                YearAnimal.Text = " лошадь";
                                break;
                            case 11:
                                YearAnimal.Text = " коза (овца)";
                                break;

                        }
                    }
                    //Древневосточный календарь
                    else if (CalSlav.IsChecked == true) 
                    {
                        EasternCalendar.Visibility = Visibility.Collapsed;
                        SlavicCalendar.Visibility = Visibility.Visible;

                        if ((int)date.Month == 12 && (int)date.Day >= 24 || (int)date.Month == 1 && (int)date.Day <= 30) YearGood.Text = " Мороз";
                        else if ((int)date.Month == 1 && (int)date.Day >= 31 || (int)date.Month == 2 && (int)date.Day <= 28) YearGood.Text = " Велес";
                        else if ((int)date.Month == 3) YearGood.Text = " Макошь";
                        else if ((int)date.Month == 4) YearGood.Text = " Жива";
                        else if ((int)date.Month == 5 && ((int)date.Day >= 1 || (int)date.Day <= 14)) YearGood.Text = " Ярило";
                        else if ((int)date.Month == 5 && (int)date.Day >= 15 || (int)date.Month == 6 && (int)date.Day <= 2) YearGood.Text = " Леля";
                        else if ((int)date.Month == 6 && ((int)date.Day >= 3 || (int)date.Day <= 12)) YearGood.Text = " Кострома";
                        else if ((int)date.Month == 6 && (int)date.Day >= 13 || (int)date.Month == 7 && (int)date.Day <= 6) YearGood.Text = " Додола";
                        else if ((int)date.Month == 7 && ((int)date.Day >= 7 || (int)date.Day <= 31) && (int)date.Day != 24) YearGood.Text = " Лада";
                        else if ((int)date.Month == 7 && (int)date.Day == 24) YearGood.Text = " Иван Купала";
                        else if ((int)date.Month == 8 && ((int)date.Day >= 1 || (int)date.Day <= 28)) YearGood.Text = " Перун";
                        else if ((int)date.Month == 8 && (int)date.Day >= 29 || (int)date.Month == 9 && (int)date.Day <= 13) YearGood.Text = " Сева";
                        else if ((int)date.Month == 9 && ((int)date.Day >= 14 || (int)date.Day <= 28)) YearGood.Text = " Рожаница";
                        else if ((int)date.Month == 9 && (int)date.Day >= 28 || (int)date.Month == 10 && (int)date.Day <= 15) YearGood.Text = " Сварожичи";
                        else if ((int)date.Month == 10 && (int)date.Day >= 16 || (int)date.Month == 11 && (int)date.Day <= 8) YearGood.Text = " Морена";
                        else if ((int)date.Month == 11 && ((int)date.Day >= 9 || (int)date.Day <= 28)) YearGood.Text = " Зима";
                        else if ((int)date.Month == 11 && (int)date.Day >= 29 || (int)date.Month == 12 && (int)date.Day <= 23) YearGood.Text = " Карачун";

                    }
                }
            }
            catch
            {
                Result.Visibility = Visibility.Collapsed;
                MessageBox.Show("Выбрана неверная дата", "Ошибка");
            }

        }

        private void Throw(object sender, RoutedEventArgs e)
        {
            Result.Visibility = Visibility.Collapsed;
            Calendar.Visibility = Visibility.Visible;
            DP.SelectedDate = null;
            ButtonSolve.Visibility = Visibility.Visible;
            ButtonThrow.Visibility = Visibility.Collapsed;
            DateBlock.Visibility = Visibility.Visible;
            Scroll.Visibility = Visibility.Collapsed;
        }
    }
}