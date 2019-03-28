using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFFICE_R.Models;

namespace OFFICE_R
{
    namespace Stores
    {
        class SecurityStore
        {
            public Employee currentUser=null;

            public bool Login(String username, String password)
            {
                password = Employee.GetHashString(password);
                List<Employee> employees = Employee.Find("username='" + username + "' AND password='" + password+"'");
                if (employees.Any())
                {
                    currentUser = employees.First();
                    return true;
                }
                return false;
            }
        }
    }
}
