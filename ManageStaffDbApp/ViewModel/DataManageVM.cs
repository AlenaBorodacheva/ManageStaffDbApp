using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ManageStaffDbApp.Model;
using ManageStaffDbApp.View;

namespace ManageStaffDbApp.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {
        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value; 
                NotifyPropertyChanged("AllDepartments");
            }
        }

        private List<Position> allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get { return allPositions; }
            private set
            {
                allPositions = value;
                NotifyPropertyChanged("allPositions");
            }
        }

        private List<User> allUsers = DataWorker.GetAllUsers();
        public List<User> AllUsers
        {
            get { return allUsers; }
            private set
            {
                allUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }

        public static string DepartmentName { get; set; }

        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public static Department PositionDepartment { get; set; }

        public static string UserName { get; set; }
        public static string UserSurname { get; set; }
        public static string UserPhone { get; set; }
        public static Position UserPosition { get; set; }

        public TabItem SelectedTabItem { get; set; }
        public static User SelectedUser { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }


        #region COMMANDS TO ADD

        private RelayCommand addNewDepartment;

        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuewToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommand addNewPosition;

        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    bool flag = true;
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                        flag = false;
                    }
                    if (PositionSalary == 0)
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                        flag = false;
                    }
                    if (PositionMaxNumber == 0)
                    {
                        SetRedBlockControll(wnd, "MaxNumberBlock");
                        flag = false;
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Укажите отдел");
                        flag = false;
                    }
                    if(flag)
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber,
                            PositionDepartment);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuewToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommand addNewUser;

        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    bool flag = true;
                    if (UserName == null || UserName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                        flag = false;
                    }
                    if (UserSurname == null || UserSurname.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "SurnameBlock");
                        flag = false;
                    }
                    //if (UserPhone == null || UserPhone.Replace(" ", "").Length == 0)
                    //{
                    //    SetRedBlockControll(wnd, "PhoneBlock");
                    //    flag = false;
                    //}
                    if (UserPosition == null)
                    {
                        MessageBox.Show("Укажите позицию");
                        flag = false;
                    }
                    if (flag)
                    {
                        resultStr = DataWorker.CreateUser(UserName, UserSurname, UserPhone, UserPosition);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuewToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        #endregion

        #region COMMAND TO DELETE

        private RelayCommand deleteItem;

        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    if (SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                    {
                        resultStr = DataWorker.DeleteUser(SelectedUser);
                        UpdateAllDataView();
                    }
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }

                    SetNullValuewToProperties();
                    ShowMessageToUser(resultStr);
                });
            }
        }

        #endregion

        #region EDITCOMMANDS

        private RelayCommand editUser;

        public RelayCommand EditUser
        {
            get
            {
                return editUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string noPositionStr = "Не выбрана новая должность";
                    if (SelectedUser != null)
                    {
                        if (UserPosition != null)
                        {
                            resultStr = DataWorker.EditUser(SelectedUser, UserName, UserSurname, UserPhone, UserPosition);
                            UpdateAllDataView();
                            SetNullValuewToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else
                        {
                            ShowMessageToUser(noPositionStr);
                        }
                    }
                    else
                    {
                        ShowMessageToUser(resultStr);
                    }
                });
            }
        }

        private RelayCommand editPosition;

        public RelayCommand EditPosition
        {
            get
            {
                return editPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана позиция";
                    string noDepartmentStr = "Не выбран новый отдел";
                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionMaxNumber, PositionSalary, PositionDepartment);
                            UpdateAllDataView();
                            SetNullValuewToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else
                        {
                            ShowMessageToUser(noDepartmentStr);
                        }
                    }
                    else
                    {
                        ShowMessageToUser(resultStr);
                    }
                });
            }
        }

        private RelayCommand editDepartment;

        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";
                    if (SelectedDepartment != null)
                    {
                        resultStr = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);
                        UpdateAllDataView();
                        SetNullValuewToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else
                    {
                        ShowMessageToUser(resultStr);
                    }
                });
            }
        }

        #endregion

        #region COMMANDS TO OPEN WINDOW

        private RelayCommand openAddNewDepartmentWnd;

        public RelayCommand OpenAddNewDepartmentWnd
        {
            get
            {
                return openAddNewDepartmentWnd ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentWindowMethod();
                });
            }
        }

        private RelayCommand openAddNewPositionWnd;

        public RelayCommand OpenAddNewPositionWnd
        {
            get
            {
                return openAddNewPositionWnd ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                });
            }
        }

        private RelayCommand openAddNewUserWnd;

        public RelayCommand OpenAddNewUserWnd
        {
            get
            {
                return openAddNewUserWnd ?? new RelayCommand(obj =>
                {
                    OpenAddUserWindowMethod();
                });
            }
        }

        private RelayCommand openEditItemWnd;

        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    if (SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                    {
                        OpenEditUserWindowMethod(SelectedUser);
                    }
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindowMethod(SelectedPosition);
                    }
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMethod(SelectedDepartment);
                    }
                });
            }
        }

        #endregion

        #region METHODS TO OPEN WONDOW

        private void OpenAddDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpen(newDepartmentWindow);
        }

        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpen(newPositionWindow);
        }

        private void OpenAddUserWindowMethod()
        {
            AddNewUserWindow newUserWindow = new AddNewUserWindow();
            SetCenterPositionAndOpen(newUserWindow);
        }

        private void OpenEditDepartmentWindowMethod(Department department)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }

        private void OpenEditPositionWindowMethod(Position position)
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow(position);
            SetCenterPositionAndOpen(editPositionWindow);
        }

        private void OpenEditUserWindowMethod(User user)
        {
            EditUserWindow editUserWindow = new EditUserWindow(user);
            SetCenterPositionAndOpen(editUserWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }
        #endregion

        #region UPDATE VIEWS

        private void SetNullValuewToProperties()
        {
            DepartmentName = null;

            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;

            UserName = null;
            UserSurname = null;
            UserPhone = null;
            UserPosition= null;
        }

        private void UpdateAllDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllPositionsView();
            UpdateAllUsersView();
        }

        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }

        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }

        private void UpdateAllUsersView()
        {
            AllUsers = DataWorker.GetAllUsers();
            MainWindow.AllUsersView.ItemsSource = null;
            MainWindow.AllUsersView.Items.Clear();
            MainWindow.AllUsersView.ItemsSource = AllUsers;
            MainWindow.AllUsersView.Items.Refresh();
        }

        #endregion

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
