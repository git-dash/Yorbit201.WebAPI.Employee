using Employee.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Repository.Services
{
    public class EmployeeServiceFake : IEmployeeService
    {
        private readonly List<EmployeeEntity> _shoppingCart;

        public EmployeeServiceFake()
        {
            _shoppingCart = new List<EmployeeEntity>()
            {
                new EmployeeEntity() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    FullName = "Orange Juice", Password ="a" , Username = "a" ,DateOfBirth=DateTime.Now,EmailID="a@gmail.com",Gender="Male",SecurityQuestion="a",SecurityAnswer="a"},
                new EmployeeEntity() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    FullName = "Diary Milk", Password ="b" , Username = "b",DateOfBirth=DateTime.Now,EmailID="b@gmail.com",Gender="Male",SecurityQuestion="b",SecurityAnswer="b"},
                new EmployeeEntity() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    FullName = "Frozen Pizza", Password ="c" , Username = "c",DateOfBirth=DateTime.Now,EmailID="c@gmail.com",Gender="Female",SecurityQuestion="c",SecurityAnswer="c"}
            };
        }

        public IEnumerable<EmployeeEntity> GetAllItems()
        {
            return _shoppingCart;
        }

        public EmployeeEntity Add(EmployeeEntity newItem)
        {
            newItem.Id = Guid.NewGuid();
            _shoppingCart.Add(newItem);
            return newItem;
        }

        public EmployeeEntity GetById(Guid id)
        {
            return _shoppingCart.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var existing = _shoppingCart.First(a => a.Id == id);
            _shoppingCart.Remove(existing);
        }

        public EmployeeEntity checkLogin(string username, string password)
        {
            return _shoppingCart.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
        }
    }
}
