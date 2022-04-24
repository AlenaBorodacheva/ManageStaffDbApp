using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ManageStaffDbApp.Model;
using ManageStaffDbApp.ViewModel;

namespace ManageStaffDbApp.View
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow(User userToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedUser = userToEdit;
            DataManageVM.UserName = userToEdit.Name;
            DataManageVM.UserSurname = userToEdit.Surname;
            DataManageVM.UserPhone = userToEdit.Phone;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
