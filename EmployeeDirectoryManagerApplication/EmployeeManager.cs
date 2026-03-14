using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EmployeeDirectoryManagerApplication
{
    public sealed class EmployeeManager
    {
        public BindingList<Employee> Employees { get; } = new BindingList<Employee>();

        //adds some initial data into the form upon loading
        public void AddTestData()
        {
            try
            {
                Employees.Add(new Employee(
                    id: "T5600",
                    fullname: "Truda Developer",
                    department: "IT",
                    role: "Senior Developer",
                    salary: 56000.0,
                    hiredate: new DateTime(2023, 6, 15)
                    ));
                Employees.Add(new Employee(
                    id: "M4123",
                    fullname: "Mark Assurance",
                    department: "QA",
                    role: "Tester",
                    salary: 52000.0,
                    hiredate: new DateTime(2024, 1, 10)
                ));
            }
            catch (Exception e)
            {
                MessageBox.Show($"Test data failed: {e.Message} ");
            }
        
        }
        public void AddEmployee(Employee e)
        {
            //validate data first
            if (e == null)
            { throw new ArgumentNullException(nameof(e)); }

            else if (string.IsNullOrWhiteSpace(e.EmployeeID)) //
            { throw new ArgumentException("Employee ID is required.", nameof(e.EmployeeID)); }

            else if (string.IsNullOrWhiteSpace(e.FullName)) //checks for an empty textbox
            { throw new ArgumentException("Full name is required.", nameof(e.FullName)); }

            else if (string.IsNullOrWhiteSpace(e.Department)) //checks for an empty textbox
            { throw new ArgumentException("Department is required.", nameof(e.Department)); }

            else if (string.IsNullOrWhiteSpace(e.Role)) //checks for an empty textbox
            { throw new ArgumentException("Role is required.", nameof(e.Role)); }

            else if (string.IsNullOrWhiteSpace(e.Salary.ToString()))//checks for an empty textbox
                { throw new ArgumentException("Salary is required.", nameof(e.Salary)); }

            else if (e.HireDate > DateTime.Today)
            {
                throw new ArgumentOutOfRangeException("Hire Date cannot be in the future", nameof(e.HireDate));
            }

            // non-zero salary check
            else if (e.Salary <= 0)
            {
                throw new ArgumentException("Salary must be a positive number.", nameof(e.Salary));
            }
            //non numeric values check
            else if (double.IsNaN(e.Salary))
            {
                throw new ArgumentException("Salary must be a number.", nameof(e.Salary));
            }

            foreach (var existing in Employees) //loops through the entire employees to make sure there is not duplicate either by the ID or the name
            {
                //checks for employee id clash
                if (existing.EmployeeID.Equals(e.EmployeeID, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        $"An employee with ID '{e.EmployeeID} already exists.");
                }
                //checks for name clash
                else if (existing.FullName.Equals(e.FullName, StringComparison.OrdinalIgnoreCase))
                {
                   throw new InvalidOperationException($"An employee with this name: '{e.FullName}' already exists.");
                }

            }


            //if all checks have passed now it adds the employee to the list
            Employees.Add(e);

        }

        //method to update an employee's data
        public void UpdateEmployeeData(Employee updated)
        {
            if (updated == null)
            {
                throw new ArgumentNullException(nameof(updated));
            }
            if (string.IsNullOrWhiteSpace(updated.EmployeeID))
            {
                throw new ArgumentException("Employee ID is required for update.", nameof(updated.EmployeeID));
            }

            // Find the employee to update
            var existing = Employees.FirstOrDefault(e=> string.Equals(e.EmployeeID, updated.EmployeeID, StringComparison.OrdinalIgnoreCase));

            if (existing == null)
            {
                throw new ArgumentException($"No employee found with ID '{updated.EmployeeID}'.");
            }

            // if everything checks out we update the data
            existing.FullName = updated.FullName?.Trim() ?? existing.FullName;
            existing.Department = updated.Department?.Trim() ?? existing.Department;
            existing.Role = updated.Role?.Trim() ?? existing.Role;
            existing.Salary = updated.Salary;
            existing.HireDate = updated.HireDate.Date;   // keep date-only

            if (existing.Salary <= 0)
            {
                throw new ArgumentException("Salary must be greater than zero.", nameof(updated.Salary));
            }
            Employees.ResetBindings();
        }

        public bool DeleteEmployee(string employeeID)
        {
            if (employeeID == null)
            {
                throw new ArgumentNullException(nameof(employeeID));
            }
            if (string.IsNullOrWhiteSpace(employeeID))
            {
                throw new ArgumentException("Employee ID is required for deletion.", nameof(employeeID));
            }

            // Find the employee to delete
            var toDelete = Employees.FirstOrDefault(e => string.Equals(e.EmployeeID, employeeID, StringComparison.OrdinalIgnoreCase));

            if (toDelete == null)
            {
                throw new ArgumentException($"No employee found with ID '{employeeID}'.");
            }
            else
            {
                // if everything checks out we delete the employee
                Employees.Remove(toDelete);
                Employees.ResetBindings();
                return true;
            }
        }


        //!!! Helper Methods for the CSV. Do not modify anything below this
        //line
        // -------- Persistence (CSV) --------
        public void SaveToCsv(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))
            ?? ".");
            StreamWriter sw = new StreamWriter(path, false, new
            UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            sw.WriteLine("EmployeeID,FullName,Department,Role,Salary,HireDate");
            foreach (var e in Employees)
            {
                sw.WriteLine(e.ToCsv());
            }
                
        }
        public void LoadFromCsv(string path)
        {
            var sr = new StreamReader(path, Encoding.UTF8);
            Employees.Clear();
            string header = sr.ReadLine(); // skip header
            if (header is null) throw new InvalidDataException("File is empty.");
            int line = 1, loaded = 0, skipped = 0;
            while (!sr.EndOfStream)
            {
                string row = sr.ReadLine();
                line++;
                if (string.IsNullOrWhiteSpace(row)) continue;
                try
                {
                    var e = Employee.FromCsv(row);
                    // Enforce unique ID on load as well
                    if (Employees.Any(x => string.Equals(x.EmployeeID, e.EmployeeID, StringComparison.OrdinalIgnoreCase)))
                        throw new InvalidOperationException($"Duplicate ID '{e.EmployeeID}'in file.");
                    Employees.Add(e);
                    loaded++;
                }
                catch (Exception)
                {
                    skipped++;
                    // For classroom apps, a console note is fine; in prod you'd
                    //log this.Console.WriteLine($"Skipped line {line}: {ex.Message}");
                }
            }
            Console.WriteLine($"Load complete. Loaded: {loaded}, Skipped:{skipped}");
        }
    }
}

