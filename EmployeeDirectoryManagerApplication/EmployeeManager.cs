using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EmployeeDirectoryManagerApplication
{
    public sealed class EmployeeManager
    {
        public BindingList<Employee> Employees { get; } = new BindingList<Employee>();

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
                    $"An employee with this name: '{e.FullName}' already exists. ");
                }

            }
            //if all checks have passed now it adds the employee to the list
            Employees.Add(e);

        }

        //method to update an employee's data
        public void UpdateEmployeeData(string employeeID)
        {
            // Find the employee to update
            var ID = employeeID;

            if (!employeeID.Equals(ID, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("No employee with specified ID exists.", employeeID);
            }




        }

        public bool DeleteEmployee(string employeeID)
        {
            // Find the exact employee by their ID (case-insensitive comparison)
            var toRemove = Employees.FirstOrDefault(b =>
                b.EmployeeID.Equals(employeeID, StringComparison.OrdinalIgnoreCase));

            if (toRemove == null)
            {
                return false;// not found
            }
            else
            {
                Employees.Remove(toRemove);
                return true;// successfully deleted the employee
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
            sw.WriteLine("Id,FullName,Department,Role,Salary,HireDate");
            foreach (var e in Employees)
                sw.WriteLine(e.ToCsv());
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
                catch (Exception ex)
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

