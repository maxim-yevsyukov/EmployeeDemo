using Caliburn.Micro;
using EmployeeDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Media.Imaging;

namespace EmployeeDemo.ViewModels
{
    public class MainViewModel : Screen
    {
        public BindableCollection<EmployeeModel> Employees { get; set; } = new BindableCollection<EmployeeModel>();
        private List<EmployeeModel> allEmployees;
        private Random _random;

        public MainViewModel()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                allEmployees = new List<EmployeeModel>(DataAccess.GetEmployees());
                PopulateIncrementallyWithRandomRecord();
            }).Start();
        }

        private void PopulateIncrementallyWithRandomRecord()
        {
            _random = new Random();
            while(allEmployees.Count > 0)   // in order for the main loop not to be infinite, I decided to stop when all entries are displayed
            {
                int idx = _random.Next(0, allEmployees.Count - 1);

                allEmployees[idx].Profile_Image = new BitmapImage(new Uri("../../person1.png", UriKind.Relative));
                allEmployees[idx].Profile_Image.Freeze();

                Employees.Add(allEmployees[idx]);
                allEmployees.RemoveAt(idx);

                Thread.Sleep(1000);
            }

        }

    }
}
