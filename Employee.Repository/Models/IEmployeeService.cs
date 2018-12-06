using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Repository.Models
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeEntity> GetAllItems();
        EmployeeEntity Add(EmployeeEntity newItem);
        EmployeeEntity GetById(Guid id);
        EmployeeEntity checkLogin(string username,string password);

        void Remove(Guid id);
    }
}
