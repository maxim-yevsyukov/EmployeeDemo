using System.Windows;
using System.ComponentModel;

namespace EmployeeDemo.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void chkName_Checked(object sender, RoutedEventArgs e)
        {
            chkId.IsChecked = false;
            itemsControlMain.Items.SortDescriptions.Clear();
            itemsControlMain.Items.SortDescriptions.Add(new SortDescription("Employee_Name", ListSortDirection.Ascending));
        }

        private void chkId_Checked(object sender, RoutedEventArgs e)
        {
            chkName.IsChecked = false;
            itemsControlMain.Items.SortDescriptions.Clear();
            itemsControlMain.Items.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
        }
    }
}
