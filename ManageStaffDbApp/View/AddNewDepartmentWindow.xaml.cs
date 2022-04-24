using System.Windows;
using ManageStaffDbApp.ViewModel;

namespace ManageStaffDbApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewDepartmentWindow.xaml
    /// </summary>
    public partial class AddNewDepartmentWindow : Window
    {
        public AddNewDepartmentWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
