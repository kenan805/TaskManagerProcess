using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using TaskManagerProcess.ViewModels;

namespace TaskManagerProcess.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; set; }
        //public ObservableCollection<Process> Processes { get; set; }
        //public ObservableCollection<string> BlackList { get; set; } = new ObservableCollection<string>();
        //private DispatcherTimer _timer;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                MainViewModel = new MainViewModel(this);
                DataContext = MainViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception: ", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        //private void BtnCreateProcess_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(txbProcess.Text))
        //        {
        //            Process process = new Process();
        //            process.StartInfo.FileName = txbProcess.Text;
        //            Processes.Add(process);
        //            process.Start();
        //            if (BlackList.Any(x => x == txbProcess.Text))
        //            {
        //                Thread.Sleep(3000);
        //                Processes.Remove(process);
        //                process.Kill();
        //            }
        //        }
        //        else
        //            MessageBox.Show("Error!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void BtnEndProcess_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        var process = lv_Tasks.SelectedItem as Process;
        //        Processes.Remove(process);
        //        process.Kill();
        //        _timer.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //void Timer_Tick(object sender, EventArgs e)
        //{
        //    Processes = new ObservableCollection<Process>(Process.GetProcesses());
        //    lv_Tasks.ItemsSource = Processes;
        //}

        //private void BtnAddProcessToBlackList_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(txbProcess.Text))
        //    {

        //        if (!BlackList.Any(x => x == txbProcess.Text))
        //        {
        //            BlackList.Add(txbProcess.Text);
        //            txbProcess.Text = "";
        //        }
        //    }
        //    else
        //        MessageBox.Show("Error!");

        //}

        //private void Lv_Tasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    _timer.Stop();
        //}
    }
}
