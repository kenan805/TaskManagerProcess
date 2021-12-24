using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TaskManagerProcess.Command;
using TaskManagerProcess.Views;

namespace TaskManagerProcess.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Process> Processes { get; set; }
        public ObservableCollection<string> BlackList { get; set; } = new ObservableCollection<string>();
        private DispatcherTimer _timer;

        public RelayCommand AddProcessToBlaclistCommand { get; set; }
        public RelayCommand CreateProcessCommand { get; set; }
        public RelayCommand EndProcessCommand { get; set; }

        private MainWindow _mainWindow;
        public MainViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            AddProcessToBlaclistCommand = new RelayCommand(
                act => BtnAddProcessToBlackList_Click(),
                pre => true
                );

            CreateProcessCommand = new RelayCommand(
                act => BtnCreateProcess_Click(),
                pre => true
                );

            EndProcessCommand = new RelayCommand(
                act => BtnEndProcess_Click(),
                pre => true
                );

            _mainWindow.lv_Tasks.SelectionChanged += Lv_Tasks_SelectionChanged;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Processes = new ObservableCollection<Process>(Process.GetProcesses());
            _mainWindow.lv_Tasks.ItemsSource = Processes;
        }

        private void BtnAddProcessToBlackList_Click()
        {
            if (!string.IsNullOrWhiteSpace(_mainWindow.txbProcess.Text))
            {

                if (!BlackList.Any(x => x == _mainWindow.txbProcess.Text))
                {
                    BlackList.Add(_mainWindow.txbProcess.Text);
                    _mainWindow.txbProcess.Text = "";
                }
            }
            else
                MessageBox.Show("Error!");

        }
        private void BtnCreateProcess_Click()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_mainWindow.txbProcess.Text))
                {
                    Process process = new Process();
                    process.StartInfo.FileName = _mainWindow.txbProcess.Text;
                    Processes.Add(process);
                    process.Start();
                    if (BlackList.Any(x => x == _mainWindow.txbProcess.Text))
                    {
                        Thread.Sleep(3000);
                        Processes.Remove(process);
                        process.Kill();
                    }
                }
                else
                    MessageBox.Show("Error!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEndProcess_Click()
        {
            try
            {
                var process = _mainWindow.lv_Tasks.SelectedItem as Process;
                Processes.Remove(process);
                process.Kill();
                _timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Lv_Tasks_SelectionChanged(object sender, SelectionChangedEventArgs e) => _timer.Stop();
    }
}