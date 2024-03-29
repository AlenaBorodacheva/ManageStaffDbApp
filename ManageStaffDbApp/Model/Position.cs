﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageStaffDbApp.Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MaxNumber { get; set; }
        public List<User> Users { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [NotMapped]
        public Department PositionDepartment
        {
            get
            {
                return DataWorker.GetDepartmentById(DepartmentId);
            }
        }

        [NotMapped]
        public List<User> PositionUsers
        {
            get
            {
                return DataWorker.GetAllUsersByPositionId(Id);
            }
        }
    }
}
