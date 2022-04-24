using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageStaffDbApp.Data;

namespace ManageStaffDbApp.Model;

public static class DataWorker
{
    public static List<Department> GetAllDepartments()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Departments.ToList();
        }
    }

    public static List<Position> GetAllPositions()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Positions.ToList();
        }
    }

    public static List<User> GetAllUsers()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Users.ToList();
        }
    }

    public static string CreateDepartment(string name)
    {
        string result = "Уже существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            bool checkIsExist = db.Departments.Any(d => d.Name == name);
            if (!checkIsExist)
            {
                Department newDepartment = new Department() {Name = name};
                db.Departments.Add(newDepartment);
                db.SaveChanges();
                result = "Сделано!";
            }

            return result;
        }
    }

    public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
    {
        string result = "Уже существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            bool checkIsExist = db.Positions.Any(p => p.Name == name && p.Salary == salary);
            if (!checkIsExist)
            {
                Position newPosition = new Position()
                {
                    Name = name, 
                    Salary = salary, 
                    MaxNumber = maxNumber, 
                    DepartmentId = department.Id
                };
                db.Positions.Add(newPosition);
                db.SaveChanges();
                result = "Сделано!";
            }

            return result;
        }
    }

    public static string CreateUser(string name, string surname, string phone, Position position)
    {
        string result = "Уже существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            bool checkIsExist = db.Users.Any(u => u.Name == name && u.Surname == surname && u.Position == position);
            if (!checkIsExist)
            {
                User newUser = new User()
                {
                    Name = name,
                    Surname = surname,
                    Phone = phone,
                    PositionId = position.Id
                };
                db.Users.Add(newUser);
                db.SaveChanges();
                result = "Сделано!";
            }

            return result;
        }
    }

    public static string DeleteDepartment(Department department)
    {
        string result = "Такого отдела не существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            Department departmentFromDb = db.Departments.FirstOrDefault(d => d.Id == department.Id);
            if (departmentFromDb != null)
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = $"Сделано! Отдел {department.Name} удален";
            }
        }

        return result;
    }

    public static string DeletePosition(Position position)
    {
        string result = "Такой позиции не существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            Position positionFromDb = db.Positions.FirstOrDefault(p => p.Id == position.Id);
            if (positionFromDb != null)
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = $"Сделано! позиция {position.Name} удалена";
            }
        }

        return result;
    }

    public static string DeleteUser(User user)
    {
        string result = "Такого сотрудника не существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            User userFromDb = db.Users.FirstOrDefault(d => d.Id == user.Id);
            if (userFromDb != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = $"Сделано! сотрудник {user.Name} удален";
            }
        }

        return result;
    }

    public static string EditDepartment(Department oldDepartment, string newName)
    {
        string result = "Такого отдела не существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            Department department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
            if (department != null)
            {
                department.Name = newName;
                db.SaveChanges();
                result = $"Сделано! Отдел {department.Name} изменен";
            }
        }

        return result;
    }

    public static string EditPosition(Position oldPosition, string newName, int newMaxNumber, decimal newSalary, Department newDepartment)
    {
        string result = "Такой позиции не существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            Position position = db.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
            if (position != null)
            {
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxNumber = newMaxNumber;
                position.DepartmentId = newDepartment.Id;
                db.SaveChanges();
                result = $"Сделано! позиция {position.Name} изменена";
            }
        }

        return result;
    }

    public static string EditUser(User oldUser, string newName, string newSurname, string newPhone, Position newPosition)
    {
        string result = "Такого сотрудника не существует";

        using (ApplicationContext db = new ApplicationContext())
        {
            User user = db.Users.FirstOrDefault(d => d.Id == oldUser.Id);
            if (user != null)
            {
                user.Name = newName;
                user.Surname = newSurname;
                user.Phone = newPhone;
                user.PositionId = newPosition.Id;
                db.SaveChanges();
                result = $"Сделано! сотрудник {user.Name} изменен";
            }
        }

        return result;
    }
}