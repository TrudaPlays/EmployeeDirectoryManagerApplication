using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDirectoryManagerApplication
{
    public partial class EmployeeDirectoryManagerForm : Form
    {
        private EmployeeManager manager = new EmployeeManager();
        private BindingSource employeeBindingSource = new BindingSource();

        public EmployeeDirectoryManagerForm()
        {
            InitializeComponent();

            employeeBindingSource.DataSource = manager.Employees;
            EmployeesDataGridView.DataSource = employeeBindingSource;

            // Optional: format salary column
            EmployeesDataGridView.Columns["Salary"].DefaultCellStyle.Format = "C2";  // currency with 2 decimals

            EmployeesDataGridView.AutoGenerateColumns = true;
            EmployeesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EmployeesDataGridView.AllowUserToAddRows = false;

        }

        private void EmployeeDirectoryManagerForm_Load(object sender, EventArgs e)
        {
            manager.AddTestData();

            EmployeesDataGridView.Refresh();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {   //grabs the data from the text only input fields
                string id = textEmployeeID.Text.Trim();
                string fullName = textFullName.Text.Trim();
                string department = textDepartment.Text.Trim();
                string role = textRole.Text.Trim();

                //tries to parse the salary to make sure it's not a wacky value
                if (!double.TryParse(textSalary.Text.Trim(), out double salary) || salary <= 0)
                {
                    MessageLabel.Text = "Please enter a valid positive salary number.";
                    textSalary.Focus();
                    return;
                }

                DateTime hireDate = dateTimePickerHireDate.Value.Date;

                var newEmployee = new Employee(
                    id: id,
                    fullname: fullName,
                    department: department,
                    role: role,
                    salary: salary,
                    hiredate: hireDate

                    );

                manager.AddEmployee(newEmployee);

                MessageLabel.Text = ToString();

                ClearInputFields();

                if (MessageLabel != null)
                    { MessageLabel.Text = $"Added: {fullName} ({id}) at {DateTime.Now:HH:mm:ss}"; }

            }
            catch (ArgumentException ex)
            {
                //validation errors from Employee Constructor or AddEmployee
                MessageLabel.Text = $"{ex.Message}, Input Error";
            }
            catch (InvalidOperationException ex)
            {
                // Duplicate ID
                MessageLabel.Text = $"{ex.Message}, Duplicate Employee";

            }
            catch (Exception ex)
            {
                // Unexpected errors
                MessageLabel.Text = $"An error occurred: {ex.Message}";

            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                /*// Make sure something is selected
                if (EmployeesDataGridView.SelectedRows.Count == 0)
                {
                    MessageLabel.Text = "Please select an employee to update.";
                    MessageLabel.ForeColor = Color.OrangeRed;
                    return;
                }*/

                // Get current values from inputs
                string id = textEmployeeID.Text.Trim();
                string fullName = textFullName.Text.Trim();
                string department = textDepartment.Text.Trim();
                string role = textRole.Text.Trim();

                // Validate salary
                if (!double.TryParse(textSalary.Text.Trim(), out double salary) || salary <= 0)
                {
                    MessageLabel.Text = "Please enter a valid positive salary number.";
                    MessageLabel.ForeColor = Color.OrangeRed;
                    textSalary.Focus();
                    return;
                }

                DateTime hireDate = dateTimePickerHireDate.Value.Date;

                // Basic required fields check
                if (string.IsNullOrWhiteSpace(id) ||
                    string.IsNullOrWhiteSpace(fullName) ||
                    string.IsNullOrWhiteSpace(department) ||
                    string.IsNullOrWhiteSpace(role))
                {
                    MessageLabel.Text = "All fields are required.";
                    MessageLabel.ForeColor = Color.OrangeRed;
                    return;
                }

                var updatedEmployee = new Employee(id, fullName, department, role, salary, hireDate);

                var existing = manager.Employees.FirstOrDefault(emp => string.Equals(emp.EmployeeID, id, StringComparison.OrdinalIgnoreCase));

                if (existing == null)
                {
                    //ID not found

                    MessageLabel.Text = $"No employee found with ID '{id}'. Use the Add button to create new employees.";
                    MessageLabel.ForeColor = Color.OrangeRed;
                    textEmployeeID.Focus();
                    return;
                }

                manager.UpdateEmployeeData(updatedEmployee);

                // Success
                MessageLabel.Text = $"Employee '{fullName}' (ID: {id}) updated successfully.";
                MessageLabel.ForeColor = Color.DarkGreen;

                EmployeesDataGridView.Refresh();

            }
            catch (ArgumentException ex)
            {
                MessageLabel.Text = ex.Message;
                MessageLabel.ForeColor= Color.Red;
            }
            catch (InvalidOperationException ex)
            {
                MessageLabel.Text = ex.Message;
                MessageLabel.ForeColor = Color.OrangeRed;
            }
        }

        //helper so i don't have to rewrite code
        private void ClearInputFields()
        {
            textEmployeeID.Clear();
            textFullName.Clear();
            textDepartment.Clear();
            textRole.Clear();
            textSalary.Clear();
            dateTimePickerHireDate.Value = DateTime.Today;
        }

        //this method is called when a rowheader is clicked with the mouse
        private void EmployeesDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (EmployeesDataGridView.SelectedRows.Count == 0)
            {
                ClearInputFields();
                MessageLabel.Text = "";
                return;
            }

            var selectedRow = EmployeesDataGridView.SelectedRows[0];
            var employee = selectedRow.DataBoundItem as Employee;

            if (employee != null)
            {
                textEmployeeID.Text = employee.EmployeeID;
                textFullName.Text = employee.FullName;
                textDepartment.Text = employee.Department;
                textRole.Text = employee.Role;
                textSalary.Text = employee.Salary.ToString("F2");   // 2 decimal places
                dateTimePickerHireDate.Value = employee.HireDate;

                MessageLabel.Text = $"Editing employee: {employee.FullName} (ID: {employee.EmployeeID})";
                
            }
        }

        //deletes an employee either by ID alone or if the user clicks on a row
        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                /*// Make sure something is selected
                if (EmployeesDataGridView.SelectedRows.Count == 0)
                {
                    MessageLabel.Text = "Please select an employee to update.";
                    MessageLabel.ForeColor = Color.OrangeRed;
                    return;
                }*/

                // Get current values from inputs
                string id = textEmployeeID.Text.Trim();
                string fullname = textFullName.Text.Trim();

                // Basic required fields check
                if (string.IsNullOrWhiteSpace(id))
                {
                    MessageLabel.Text = "ID is required for deletion";
                    MessageLabel.ForeColor = Color.OrangeRed;
                    return;
                }

                manager.DeleteEmployee(id);

                // Success
                MessageLabel.Text = $"Employee '{fullname}' (ID: {id}) updated successfully.";
                MessageLabel.ForeColor = Color.DarkGreen;

                EmployeesDataGridView.Refresh();

            }
            catch (ArgumentException ex)
            {
                MessageLabel.Text = ex.Message;
                MessageLabel.ForeColor = Color.Red;
            }
            catch (InvalidOperationException ex)
            {
                MessageLabel.Text = ex.Message;
                MessageLabel.ForeColor = Color.OrangeRed;
            }
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeesDataGridView.EndEdit();
                employeeBindingSource.EndEdit();

                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string fileName = $"employees_{DateTime.Now:yyyyMMdd}.csv";
                string filePath = Path.Combine(desktop, fileName);

                manager.SaveToCsv(filePath);
                
                MessageLabel.Text = $"Saved {manager.Employees.Count} employees to Desktop:\n{filePath}";
                MessageLabel.ForeColor = System.Drawing.Color.DarkGreen;

                /*
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Title = "Saved Employees CSV";
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.DefaultExt = "csv";
                    sfd.AddExtension = true;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        manager.SaveToCsv(sfd.FileName);

                        MessageLabel.Text = $"Saved {manager.Employees.Count} employees to:\n{sfd.FileName}";
                        MessageLabel.ForeColor = System.Drawing.Color.DarkGreen;
                    }
                }*/
            }
            catch (Exception ex)
            {
                MessageLabel.Text = $"Save failed: {ex.Message}";
                MessageLabel.ForeColor= Color.Red;
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string fileName = $"employees_{DateTime.Now:yyyyMMdd}.csv";
                string filePath = Path.Combine(desktop, fileName);


                if (!File.Exists(filePath))
                {
                    MessageLabel.Text = $"No file found on Desktop:\n{filePath}";
                    MessageLabel.ForeColor = System.Drawing.Color.OrangeRed;
                    return;
                }

                manager.LoadFromCsv(filePath);

                // Re-bind (safe) and refresh UI
                employeeBindingSource.DataSource = manager.Employees;
                EmployeesDataGridView.DataSource = employeeBindingSource;
                EmployeesDataGridView.Refresh();

                MessageLabel.Text = $"Loaded {manager.Employees.Count} employees from Desktop:\n{filePath}";
                MessageLabel.ForeColor = System.Drawing.Color.DarkGreen;

            }
            catch (InvalidDataException ex)
            {
                MessageLabel.Text = "File is empty";
            }
            catch (InvalidOperationException ex)
            {
                MessageLabel.Text = $"Duplicate ID '{EmployeeID}'in file.";
            }
            catch (FileNotFoundException)
            {
                MessageLabel.Text = "File not found. Verify it is still on the Desktop and has not been moved";
            }

        }
    }
}
