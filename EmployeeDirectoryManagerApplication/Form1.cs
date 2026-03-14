using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDirectoryManagerApplication
{
    public partial class EmployeeDirectoryManagerForm : Form
    {
        private readonly EmployeeManager manager = new EmployeeManager();
        private BindingSource employeeBindingSource = new BindingSource();

        public EmployeeDirectoryManagerForm()
        {
            InitializeComponent();

            EmployeesDataGridView.DataSource = manager.Employees;

            EmployeesDataGridView.Columns.Clear();
            EmployeesDataGridView.Columns.Add("EmployeeID", "Employee ID");
            EmployeesDataGridView.Columns.Add("FullName", "Full Name");
            EmployeesDataGridView.Columns.Add("Department", "Department");
            EmployeesDataGridView.Columns.Add("Role", "Role");
            EmployeesDataGridView.Columns.Add("Salary", "Salary");

            // Optional: format salary column
            EmployeesDataGridView.Columns["Salary"].DefaultCellStyle.Format = "C2";  // currency with 2 decimals

            EmployeesDataGridView.AutoGenerateColumns = false;
            EmployeesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EmployeesDataGridView.AllowUserToAddRows = false;

        }

    }
}
