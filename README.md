# AgeCalculator
Данная программа предназначена для расчета информации по вашей дате рождения.

![Начальный экран](https://github.com/Nedzhuk/AgeCalculator/assets/129393865/a7beb50c-1943-475e-ac6d-aa3f18f21257)
# Описание
Используя данное приложение вы можете узнать:
* Прожитое время 
* День недели рождения
* Количество лет, в которых день рождения был в день недели вашего рождения _(например, по понедельникам)_
* Количество прожитых **високосных** лет
* Тотемное существо по выбранному гороскопу _(восточный или древнеславянский)_

# Инструкция к установке
## Требования:
1. Работающий компьютер
2. Доступ к интернету
3. Программа Visual Studio
4. Человек
## Процесс установки:
1. Открыть репозиторий AgeCalculator
2. Нажать на зеленую кнопу `<>Code`
3. В выплывающем списке выбрать "Открыть с помощью Visual Studio" и запустить проект, либо скачать файл зип-архивом (далее будет инструкция по его установке)

### Если вы скачали проект зип-архивом:
+ Извлечь все из архива
+ Открыть разархивированную папку
+ Запустить файл `DateB.sln`
+ Запустить проект

**Образец установки с помощью Visual Studio:**

![OpenWithVS](https://github.com/Nedzhuk/AgeCalculator/assets/129393865/68b8595c-96f0-4371-a169-e63fcc1e9ba9)

**Образец установки с помощью Zip архвива:**

![OpenWithZIP](https://github.com/Nedzhuk/AgeCalculator/assets/129393865/11f9d0b6-8655-45a3-a374-46f5bd5b41c3)

# Инструкция по использованию приложения
**Пример использования AgeCalculator:**
![readme](https://github.com/Nedzhuk/AgeCalculator/assets/129393865/358c012b-5ec8-4ff5-939c-6b39809bbd39)

# Листинг программы
## Файл разметки
```
<Window x:Class="DateB.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:DateB"
        mc:Ignorable="d"
        Title="Calculator" Height="450" Width="800"
        FontSize="25" FontFamily="Avenir"
        Foreground="#333333">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"/>
            <ColumnDefinition Width="4*"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition  Height="*"/>
            <RowDefinition  Height="4*"/>
            <RowDefinition  Height="30"/>
        </Grid.RowDefinitions>
        <TextBlock Text="Калькулятор расчета возраста" Grid.Column="1" VerticalAlignment="Center" HorizontalAlignment="Center" Foreground="Black" FontWeight="DemiBold"/>
        <StackPanel  Grid.Row="1" Grid.Column="1">
            <StackPanel Name="DateBlock" Orientation="Horizontal" HorizontalAlignment="Center">
                <TextBlock Text="Выберите дату рождения" Margin="0,0,20,-10" Foreground="#1a1a1a"/>
                <DatePicker Name="DP"/>
            </StackPanel>
            <StackPanel x:Name="Calendar">
                <TextBlock Text="Выберите календарь для большей информации:" Margin="-40,10" HorizontalAlignment="Center" Foreground="#1a1a1a"/>
                <RadioButton Content="Восточный календарь" Name="CalVest"/>
                <RadioButton Content="Древнеславянский календарь"  Name="CalSlav"/>
            </StackPanel>
            <Button Content="Вычислить" Name="ButtonSolve" Click="Solve" Style="{StaticResource ButtonStyle}"/>
            <Button Content="Сбросить" Name="ButtonThrow" Click="Throw" Style="{StaticResource ButtonTrowStyle}" Visibility="Collapsed"/>
            <ScrollViewer Name="Scroll" HorizontalScrollBarVisibility="Visible" Visibility="Collapsed">
                <StackPanel x:Name="Result" Margin="0,40,0,0" Visibility="Collapsed">
                    <StackPanel>
                        <TextBlock Text="Прожитое время:" FontWeight="Bold"/>
                        <TextBlock Name="Life" Text=""/>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="День недели рождения: " FontWeight="Bold"/>
                        <TextBlock x:Name="DayWeek"/>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="Дни рождения в этот день недели: " FontWeight="Bold"/>
                        <TextBlock x:Name="DayWeekAll"/>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="Колличество прожитых високосных лет: " FontWeight="Bold"/>
                        <TextBlock x:Name="CountDayLeapYear"/>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="Високосные прожитые года:" FontWeight="Bold"/>
                        <TextBlock x:Name="LeapYear"/>
                    </StackPanel>
                    <StackPanel Name="EasternCalendar" Orientation="Horizontal">
                        <TextBlock Text="Животное по китайскому гороскопу:" FontWeight="Bold"/>
                        <TextBlock x:Name="YearAnimal"/>
                    </StackPanel>
                    <StackPanel Name="SlavicCalendar" Orientation="Horizontal">
                        <TextBlock Text="Божество по древнеславянскому гороскопу:" FontWeight="Bold"/>
                        <TextBlock x:Name="YearGood"/>
                    </StackPanel>
                </StackPanel>
            </ScrollViewer>
        </StackPanel>
    </Grid>
</Window>
```

## Файл функциональной части
```
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
```
## Файл стилей
```
<Application x:Class="DateB.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:DateB"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <Style x:Key="ButtonStyle" TargetType="Button">
            <Style.Setters>
                <Setter Property="Background" Value="#007AFF"/>
                <Setter Property="Foreground" Value="White"/>
                <Setter Property="Margin" Value="40,0"/>
                <Setter Property="Template">
                    <Setter.Value>
                        <ControlTemplate TargetType="Button">
                            <Border Background="{TemplateBinding Background}" CornerRadius="5" HorizontalAlignment="Center" Padding="70,5" Margin="0,10,0,0">
                                <ContentPresenter/>
                            </Border>
                        </ControlTemplate>
                    </Setter.Value>
                </Setter>
            </Style.Setters>
            <Style.Triggers>
                <Trigger Property="IsMouseOver" Value="True">
                    <Setter Property="Background" Value="#34AADC"/>
                </Trigger>
                <Trigger Property="IsPressed" Value="True">
                    <Setter Property="Background" Value="#5856D6"/>
                </Trigger>
            </Style.Triggers>
        </Style>

        <Style x:Key="ButtonTrowStyle" TargetType="Button">
            <Style.Setters>
                <Setter Property="Background" Value="Red"/>
                <Setter Property="Foreground" Value="White"/>
                <Setter Property="Margin" Value="40,0"/>
                <Setter Property="Template">
                    <Setter.Value>
                        <ControlTemplate TargetType="Button">
                            <Border Background="{TemplateBinding Background}" CornerRadius="5" HorizontalAlignment="Center" Padding="70,5" Margin="0,10,0,0">
                                <ContentPresenter/>
                            </Border>
                        </ControlTemplate>
                    </Setter.Value>
                </Setter>
            </Style.Setters>
            <Style.Triggers>
                <Trigger Property="IsMouseOver" Value="True">
                    <Setter Property="Background" Value="#CD5C5C"/>
                </Trigger>
                <Trigger Property="IsPressed" Value="True">
                    <Setter Property="Background" Value="DarkRed"/>
                </Trigger>
            </Style.Triggers>
        </Style>

        <Style TargetType="RadioButton">
            <Setter Property="FontSize" Value="25"/>
            <Setter Property="Foreground" Value="#333333"/>
        </Style>
    </Application.Resources>
</Application>
```

# Цветовая палитра
1. Цвет текста: `#333333`, `#ffffff`
2. Цвет фона: `#ffffff`
3. Кнопка результата: `#007AFF`, `#34AADC`, `#5856D6`
4. Кнопка сброса результата: `#FF0000`, `#CD5C5C`, `#8b0000`

## Разработчик
Больше проектов в профиле [Надежда](https://github.com/Nedzhuk "Профиль разработчика"):heartpulse:
