using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using static System.Net.Mime.MediaTypeNames;

namespace напоминалкаа
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Reminder> reminders = new ObservableCollection<Reminder>();
        DispatcherTimer timer = new DispatcherTimer();


        public MainWindow()
        {

            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(55);
            timer.Tick += Timer_Tick;
            RemindersGrid.ItemsSource = reminders;
        }
        private void Timer_Tick(object? sander, EventArgs e)
        {
            int h = DateTime.Now.Hour;
            int m = DateTime.Now.Minute;
            foreach (Reminder r in reminders)
            {
                if (r.Hour == h && r.Minute == m && r.IsShown == false)
                {
                    MessageBox.Show($"{r.Hour}:{r.Minute} - {r.Text}",
                        "Напоминание",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    r.IsShown = true;
                    RemindersGrid.ItemsSource = null;
                    RemindersGrid.ItemsSource = reminders;
                }
            }
        }
        private void OpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстовые файлы (*.txt)|*.txt|Все ваши файлы (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                string path = dlg.FileName;
                string[] temp = File.ReadAllLines(path);

                foreach (string s in temp)
                {
                    string[] t = s.Split("%%");
                    reminders.Add(new Reminder
                    {
                        Hour = Convert.ToInt32(t[0]),
                        Minute = Convert.ToInt32(t[1]),
                        Text = t[2],
                        IsShown = Convert.ToBoolean(t[3])
                    
                    });
                }
            }
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Текстовые файлы(*.txt)|*.txt";
            dlg.FileName = "Отчет.txt";

            if (dlg.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(dlg.FileName))
                {
                    foreach (Reminder r in reminders)
                        writer.WriteLine(
                            $"{r.Hour}%%{r.Minute}%%{r.Text}%%{r.IsShown}");
                }

            }
        }
        private void AddClick(object sender, RoutedEventArgs e)
        {
            reminders.Add(new Reminder()
            {

                Hour = Convert.ToInt32(txtHour.Text),
                Minute = Convert.ToInt32(txtHour.Text),
                Text = txtText.Text,
                IsShown = false
            });
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (RemindersGrid.SelectedItem is Reminder r)
            {
                reminders.Remove(r);
            }
        }
    
    }
}