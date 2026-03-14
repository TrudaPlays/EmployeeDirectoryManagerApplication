using System;
using System.Globalization;
using System.Linq;

namespace EmployeeDirectoryManagerApplication
{
    public sealed class Employee
    {
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }

        // Constructor with validation
        public Employee(string id, string fullname, string department, string role, double salary, DateTime hiredate)
        {
            // Validate inputs for all input fields
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Employee ID is required.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(fullname))
            {
                throw new ArgumentException("Full name is required.", nameof(fullname));
            }

            if (string.IsNullOrWhiteSpace(department))
            {
                throw new ArgumentException("Department is required.", nameof(department));
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Role is required.", nameof(role));
            }

            if (double.IsNaN(salary))
            {
                throw new ArgumentException("Salary must be a number.", nameof(salary));
            }

            if (hiredate > DateTime.Today)
            {
                throw new ArgumentException("Hire date cannot be in the future.", nameof(hiredate));
            }

            // All validations passed → assign values
            EmployeeID = id.Trim();
            FullName = fullname.Trim();
            Department = department.Trim();
            Role = role.Trim();
            Salary = salary;
            HireDate = hiredate.Date;
        }


        //ToString method for printing details
        public override string ToString()
        {
            return $"{EmployeeID} | {FullName} | {Department} | {Role} | {Salary:C0} | {HireDate:yyyy-MM-dd}";
        }

        //!!! No need to modify anything below this line.
        // -------- CSV helpers (comma-quote safe, ISO dates) --------
        public string ToCsv()
        {
            return string.Join(",",
            CsvEscape(EmployeeID),
            CsvEscape(FullName),
            CsvEscape(Department),
            CsvEscape(Role),
            Salary.ToString(CultureInfo.InvariantCulture),
            HireDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            );
        }
        public static Employee FromCsv(string csvLine)
        {
            var cols = CsvParse(csvLine);
            if (cols.Count != 6) throw new FormatException("Invalid employee record(expected 6 columns).");
            string id = cols[0];
            string fullName = cols[1];
            string department = cols[2];
            string role = cols[3];

            if (!double.TryParse(cols[4], NumberStyles.Float,
            CultureInfo.InvariantCulture, out var salary))
                throw new FormatException("Invalid salary value.");
            if (!DateTime.TryParseExact(cols[5], "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out var hire))
                throw new FormatException("Invalid hire date.");
            return new Employee(id, fullName, department, role, salary, hire);
        }
        private static string CsvEscape(string input)
        {
            bool needs = input.Contains(',') || input.Contains('"') ||
            input.Contains('\n') || input.Contains('\r');
            if (!needs) return input;
            return "\"" + input.Replace("\"", "\"\"") + "\"";
        }
        private static System.Collections.Generic.List<string> CsvParse(string
        line)
        {
            var result = new System.Collections.Generic.List<string>();
            var sb = new System.Text.StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        { sb.Append('"'); i++; }
                        else { inQuotes = false; }
                    }
                    else sb.Append(c);
                }
                else
                {
                    if (c == ',') { result.Add(sb.ToString()); sb.Clear(); }
                    else if (c == '"') { inQuotes = true; }
                    else sb.Append(c);
                }
            }
            result.Add(sb.ToString());
            return result;
        }
    }

}