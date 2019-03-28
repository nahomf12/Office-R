using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Npgsql;

namespace OFFICE_R
{
    namespace Models
    {
        class Model
        {
            public int id;
            public int ID
            {
                get { return this.id; }
                set { this.id = value; }
            }
        }

        class Employee : Model
        {
            public String fullName;
            public String username;
            public String password;
            public Double salary = 0;
            public String address;
            public String email;
            public String phone;
            public bool isAdmin = false;
            public bool isReleased = false;
            public static byte[] GetHash(string inputString)
            {
                HashAlgorithm algorithm = SHA256.Create();
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            }

            public static string GetHashString(string inputString)
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in GetHash(inputString))
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }

            public static void CreateTable()
            {
                Program.database.ExecuteUpdate("CREATE TABLE IF NOT EXISTS employee(id SERIAL PRIMARY KEY, " +
                    "fullName VARCHAR(255) NOT NULL, username VARCHAR(255) NOT NULL, password VARCHAR(255) NOT NULL, " +
                    "salary FLOAT DEFAULT 0, address VARCHAR(255), email VARCHAR(255), phone VARCHAR(255) NOT NULL, " +
                    "isAdmin BOOLEAN DEFAULT FALSE, isReleased BOOLEAN DEFAULT FALSE);");
            }

            public static Employee Create(Employee employee)
            {
                Program.database.ExecuteUpdate(String.Format("INSERT INTO employee(fullName, username, password, salary, address, email, phone) " +
                    "VALUES('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}');", employee.fullName, employee.username, GetHashString(employee.password), employee.salary, employee.address, employee.email, employee.phone));
                return employee;
            }


            public static Employee FindById(int id)
            {
                NpgsqlCommand command = Program.database.PrepareStatement("SELECT * FROM employee WHERE id=@id;");
                command.Parameters.Add("id", id);
                command.Prepare();
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Employee employee = new Employee();
                    employee.ID = reader.GetInt32(0);
                    employee.fullName = reader.GetString(1);
                    employee.username = reader.GetString(2);
                    employee.password = reader.GetString(3);
                    employee.salary = reader.GetDouble(4);
                    employee.address = reader.GetString(5);
                    employee.email = reader.GetString(6);
                    employee.phone = reader.GetString(7);
                    employee.isAdmin = reader.GetBoolean(8);
                    employee.isReleased = reader.GetBoolean(9);
                    return employee;
                }
                else
                {
                    throw new Exception("No Employee Found");
                }
            }
            public static List<Employee> FindAll()
            {
                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM employee;");
                List<Employee> employees = new List<Employee>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.ID = reader.GetInt32(0);
                        employee.fullName = reader.GetString(1);
                        employee.username = reader.GetString(2);
                        employee.password = reader.GetString(3);
                        employee.salary = reader.GetDouble(4);
                        employee.address = reader.GetString(5);
                        employee.email = reader.GetString(6);
                        employee.phone = reader.GetString(7);
                        employee.isAdmin = reader.GetBoolean(8);
                        employee.isReleased = reader.GetBoolean(9);
                        employees.Add(employee);
                    }
                    return employees;
                }
                else
                {
                    return employees;
                }
            }
            public static List<Employee> Find(String query)
            {
                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM employee WHERE " + query + ";");
                List<Employee> employees = new List<Employee>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.ID = reader.GetInt32(0);
                        employee.fullName = reader.GetString(1);
                        employee.username = reader.GetString(2);
                        employee.password = reader.GetString(3);
                        employee.salary = reader.GetDouble(4);
                        employee.address = reader.GetString(5);
                        employee.email = reader.GetString(6);
                        employee.phone = reader.GetString(7);
                        employee.isAdmin = reader.GetBoolean(8);
                        employee.isReleased = reader.GetBoolean(9);
                        employees.Add(employee);
                    }
                    return employees;
                }
                else
                {
                    return employees;
                }
            }
        }

        class Comment : Model
        {
            public int employeeId;
            public Employee employee;
            public int assignerId;
            public Employee assigner;
            public string description;

            public static void CreateTable()
            {
                Program.database.ExecuteUpdate("CREATE TABLE IF NOT EXISTS comment(id SERIAL PRIMARY KEY, " +
                    "employeeId INTEGER REFERENCES employee(id) NOT NULL, assignerId INTEGER REFERENCES employee(id) NOT NULL, description varchar(255) NOT NULL);");
            }

            public static Comment Create(Comment task)
            {
                Program.database.ExecuteUpdate("INSERT INTO comment( employeeId, assignerId, description) " +
                    "VALUES("+task.employeeId + ", " + task.assignerId + ", '" + task.description + "');");
                return task;
            }

            public static List<Comment> FindAll()
            {
                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM comment;");
                List<Comment> tasks = new List<Comment>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Comment task = new Comment();
                        task.ID = reader.GetInt32(0);
                        task.employeeId = reader.GetInt32(1);
                        if (task.employeeId != 0)
                        {
                            task.employee = Employee.FindById(task.employeeId);
                        }
                        task.assignerId = reader.GetInt32(2);
                        if (task.employeeId != 0)
                        {
                            task.assigner = Employee.FindById(task.assignerId);
                        }
                        task.description = reader.GetString(3);
                        tasks.Add(task);
                    }
                    return tasks;
                }
                else
                {
                    return tasks;
                }
            }

            public static List<Comment> Find(String query)
            {
                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM comment WHERE " + query + ";");
                List<Comment> tasks = new List<Comment>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Comment task = new Comment();
                        task.ID = reader.GetInt32(0);
                        task.employeeId = reader.GetInt32(1);
                        if (task.employeeId != 0)
                        {
                            task.employee = Employee.FindById(task.employeeId);
                        }
                        task.assignerId = reader.GetInt32(2);
                        if (task.employeeId != 0)
                        {
                            task.assigner = Employee.FindById(task.assignerId);
                        }
                        task.description = reader.GetString(3);
                        tasks.Add(task);
                    }
                    return tasks;
                }
                else
                {
                    return tasks;
                }
            }
        }
        class Task : Model
        {
            public String title;
            public int employeeId;
            public Employee employee;
            public int assignerId;
            public Employee assigner;
            public string description;
            public bool isComplete = false;
            public bool isChecked = false;

            public static void CreateTable()
            {
                Program.database.ExecuteUpdate("CREATE TABLE IF NOT EXISTS task(id SERIAL PRIMARY KEY, " +
                    " title VARCHAR(255) NOT NULL, employeeId INTEGER REFERENCES employee(id) NOT NULL, assignerId INTEGER REFERENCES employee(id) NOT NULL, description varchar(255) NOT NULL," +
                    "isComplete BOOLEAN DEFAULT FALSE, isChecked BOOLEAN DEFAULT FALSE);");
            }

            public static Task Create(Task task)
            {
                Program.database.ExecuteUpdate("INSERT INTO task(title, employeeId, assignerId, description) " +
                    "VALUES('" + task.title + "', " + task.employeeId + ", " + task.assignerId + ", '" + task.description + "');");
                return task;
            }

            public static List<Task> FindAll()
            {
                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM task;");
                List<Task> tasks = new List<Task>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Task task = new Task();
                        task.ID = reader.GetInt32(0);
                        task.title = reader.GetString(1);
                        task.employeeId = reader.GetInt32(2);
                        if (task.employeeId != 0)
                        {
                            task.employee = Employee.FindById(task.employeeId);
                        }
                        task.assignerId = reader.GetInt32(3);
                        if (task.employeeId != 0)
                        {
                            task.assigner = Employee.FindById(task.assignerId);
                        }
                        task.description = reader.GetString(4);
                        task.isComplete = reader.GetBoolean(5);
                        task.isChecked = reader.GetBoolean(6);
                        tasks.Add(task);
                    }
                    return tasks;
                }
                else
                {
                    return tasks;
                }
            }

            public static List<Task> Find(String query)
            {
                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM task WHERE " + query + ";");
                List<Task> tasks = new List<Task>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Task task = new Task();
                        task.ID = reader.GetInt32(0);
                        task.title = reader.GetString(1);
                        task.employeeId = reader.GetInt32(2);
                        if (task.employeeId != 0)
                        {
                            task.employee = Employee.FindById(task.employeeId);
                        }
                        task.assignerId = reader.GetInt32(3);
                        if (task.employeeId != 0)
                        {
                            task.assigner = Employee.FindById(task.assignerId);
                        }
                        task.description = reader.GetString(4);
                        task.isComplete = reader.GetBoolean(5);
                        task.isChecked = reader.GetBoolean(6);
                        tasks.Add(task);
                    }
                    return tasks;
                }
                else
                {
                    return tasks;
                }
            }
        }
        class Announcement : Model
        {
            public String description;
            public static void CreateTable()
            {
                Program.database.ExecuteUpdate("CREATE TABLE IF NOT EXISTS announcement(id SERIAL PRIMARY KEY, description VARCHAR(255) NOT NULL)");
            }

            public static Announcement Create(Announcement announcement)
            {
                Program.database.ExecuteUpdate(String.Format("INSERT INTO announcement(description) VALUES('{0}');", announcement.description));
                return announcement;
            }

            public static void Remove(int id)
            {
                Program.database.ExecuteUpdate(String.Format("DELETE FROM announcement WHERE id={0};", id));
            }

            public static List<Announcement> FindAll()
            {

                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM announcement;");
                List<Announcement> tasks = new List<Announcement>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Announcement task = new Announcement();
                        task.ID = reader.GetInt32(0);
                        task.description = reader.GetString(1);
                        tasks.Add(task);
                    }
                    return tasks;
                }
                else
                {
                    return tasks;
                }

            }
            public static List<Announcement> Find(String query)
            {

                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM announcement WHERE " + query + ";");
                List<Announcement> tasks = new List<Announcement>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Announcement task = new Announcement();
                        task.ID = reader.GetInt32(0);
                        task.description = reader.GetString(1);
                        tasks.Add(task);
                    }
                    return tasks;
                }
                else
                {
                    return tasks;
                }

            }
        }
        class Attendance : Model
        {
            public int employeeId;
            public Employee employee;
            public DateTime dateTime;
            public static void CreateTable()
            {
                Program.database.ExecuteUpdate("CREATE TABLE IF NOT EXISTS attendance(id SERIAL PRIMARY KEY, employeeId INTEGER REFERENCES employee(id) NOT NULL, datetime timestamp NOT NULL)");
            }
            public static Attendance Create(Attendance attendance)
            {
                Program.database.ExecuteUpdate(String.Format("INSERT INTO attendance(employeeId, dateTime) VALUES({0}, '{1}'::timestamp);", attendance.employeeId, attendance.dateTime.ToString()));
                return attendance;
            }
            public static List<Attendance> FindAll()
            {

                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM attendance;");
                List<Attendance> tasks = new List<Attendance>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Attendance task = new Attendance();
                        task.ID = reader.GetInt32(0);
                        task.employeeId = reader.GetInt32(1);
                        if (task.employeeId!=0)
                        {
                            task.employee=Employee.FindById(task.employeeId);
                        }
                        task.dateTime = reader.GetDateTime(2);
                        tasks.Add(task);
                    }
                    return tasks;
                }
                else
                {
                    return tasks;
                }

            }

            public static List<Attendance> Find(String query)
            {
                NpgsqlDataReader reader = Program.database.ExecuteQuery("SELECT * FROM attendance WHERE "+query+";");
                List<Attendance> tasks = new List<Attendance>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Attendance task = new Attendance();
                        task.ID = reader.GetInt32(0);
                        task.employeeId = reader.GetInt32(1);
                        if (task.employeeId != 0)
                        {
                            task.employee = Employee.FindById(task.employeeId);
                        }
                        task.dateTime = reader.GetDateTime(2);
                        tasks.Add(task);
                    }
                    return tasks;
                }
                else
                {
                    return tasks;
                }

            }
        }
    }
}
