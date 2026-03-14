namespace EmployeeDirectoryManagerApplication
{
    partial class EmployeeDirectoryManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDeleteEmployee = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdateEmployee = new System.Windows.Forms.Button();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.btnExitForm = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.EmployeeID = new System.Windows.Forms.Label();
            this.textEmployeeID = new System.Windows.Forms.TextBox();
            this.textFullName = new System.Windows.Forms.TextBox();
            this.FullName = new System.Windows.Forms.Label();
            this.textDepartment = new System.Windows.Forms.TextBox();
            this.Department = new System.Windows.Forms.Label();
            this.textRole = new System.Windows.Forms.TextBox();
            this.Role = new System.Windows.Forms.Label();
            this.textSalary = new System.Windows.Forms.TextBox();
            this.Salary = new System.Windows.Forms.Label();
            this.dateTimePickerHireDate = new System.Windows.Forms.DateTimePicker();
            this.HireDate = new System.Windows.Forms.Label();
            this.EmployeesDataGridView = new System.Windows.Forms.DataGridView();
            this.MessageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDeleteEmployee
            // 
            this.btnDeleteEmployee.Location = new System.Drawing.Point(31, 372);
            this.btnDeleteEmployee.Name = "btnDeleteEmployee";
            this.btnDeleteEmployee.Size = new System.Drawing.Size(117, 49);
            this.btnDeleteEmployee.TabIndex = 0;
            this.btnDeleteEmployee.Text = "Delete";
            this.btnDeleteEmployee.UseVisualStyleBackColor = true;
            this.btnDeleteEmployee.Click += new System.EventHandler(this.btnDeleteEmployee_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1051, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 66);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save to CSV File";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdateEmployee
            // 
            this.btnUpdateEmployee.Location = new System.Drawing.Point(154, 317);
            this.btnUpdateEmployee.Name = "btnUpdateEmployee";
            this.btnUpdateEmployee.Size = new System.Drawing.Size(117, 49);
            this.btnUpdateEmployee.TabIndex = 2;
            this.btnUpdateEmployee.Text = "Update";
            this.btnUpdateEmployee.UseVisualStyleBackColor = true;
            this.btnUpdateEmployee.Click += new System.EventHandler(this.btnUpdateEmployee_Click);
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Location = new System.Drawing.Point(31, 317);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(117, 49);
            this.btnAddEmployee.TabIndex = 3;
            this.btnAddEmployee.Text = "Add";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // btnExitForm
            // 
            this.btnExitForm.Location = new System.Drawing.Point(1324, 372);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(117, 66);
            this.btnExitForm.TabIndex = 4;
            this.btnExitForm.Text = "Exit Form";
            this.btnExitForm.UseVisualStyleBackColor = true;
            this.btnExitForm.Click += new System.EventHandler(this.btnExitForm_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(1184, 372);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(117, 66);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load from CSV File";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // EmployeeID
            // 
            this.EmployeeID.AutoSize = true;
            this.EmployeeID.Location = new System.Drawing.Point(27, 41);
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.Size = new System.Drawing.Size(100, 20);
            this.EmployeeID.TabIndex = 6;
            this.EmployeeID.Text = "Employee ID";
            // 
            // textEmployeeID
            // 
            this.textEmployeeID.Location = new System.Drawing.Point(130, 41);
            this.textEmployeeID.Name = "textEmployeeID";
            this.textEmployeeID.Size = new System.Drawing.Size(100, 26);
            this.textEmployeeID.TabIndex = 7;
            // 
            // textFullName
            // 
            this.textFullName.Location = new System.Drawing.Point(130, 90);
            this.textFullName.Name = "textFullName";
            this.textFullName.Size = new System.Drawing.Size(235, 26);
            this.textFullName.TabIndex = 9;
            // 
            // FullName
            // 
            this.FullName.AutoSize = true;
            this.FullName.Location = new System.Drawing.Point(27, 96);
            this.FullName.Name = "FullName";
            this.FullName.Size = new System.Drawing.Size(80, 20);
            this.FullName.TabIndex = 8;
            this.FullName.Text = "Full Name";
            // 
            // textDepartment
            // 
            this.textDepartment.Location = new System.Drawing.Point(130, 145);
            this.textDepartment.Name = "textDepartment";
            this.textDepartment.Size = new System.Drawing.Size(100, 26);
            this.textDepartment.TabIndex = 11;
            // 
            // Department
            // 
            this.Department.AutoSize = true;
            this.Department.Location = new System.Drawing.Point(27, 151);
            this.Department.Name = "Department";
            this.Department.Size = new System.Drawing.Size(94, 20);
            this.Department.TabIndex = 10;
            this.Department.Text = "Department";
            // 
            // textRole
            // 
            this.textRole.Location = new System.Drawing.Point(130, 195);
            this.textRole.Name = "textRole";
            this.textRole.Size = new System.Drawing.Size(199, 26);
            this.textRole.TabIndex = 13;
            // 
            // Role
            // 
            this.Role.AutoSize = true;
            this.Role.Location = new System.Drawing.Point(27, 201);
            this.Role.Name = "Role";
            this.Role.Size = new System.Drawing.Size(42, 20);
            this.Role.TabIndex = 12;
            this.Role.Text = "Role";
            // 
            // textSalary
            // 
            this.textSalary.Location = new System.Drawing.Point(130, 252);
            this.textSalary.Name = "textSalary";
            this.textSalary.Size = new System.Drawing.Size(100, 26);
            this.textSalary.TabIndex = 15;
            // 
            // Salary
            // 
            this.Salary.AutoSize = true;
            this.Salary.Location = new System.Drawing.Point(27, 258);
            this.Salary.Name = "Salary";
            this.Salary.Size = new System.Drawing.Size(53, 20);
            this.Salary.TabIndex = 14;
            this.Salary.Text = "Salary";
            // 
            // dateTimePickerHireDate
            // 
            this.dateTimePickerHireDate.Location = new System.Drawing.Point(371, 35);
            this.dateTimePickerHireDate.Name = "dateTimePickerHireDate";
            this.dateTimePickerHireDate.Size = new System.Drawing.Size(293, 26);
            this.dateTimePickerHireDate.TabIndex = 16;
            // 
            // HireDate
            // 
            this.HireDate.AutoSize = true;
            this.HireDate.Location = new System.Drawing.Point(288, 41);
            this.HireDate.Name = "HireDate";
            this.HireDate.Size = new System.Drawing.Size(77, 20);
            this.HireDate.TabIndex = 17;
            this.HireDate.Text = "Hire Date";
            // 
            // EmployeesDataGridView
            // 
            this.EmployeesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmployeesDataGridView.Location = new System.Drawing.Point(443, 96);
            this.EmployeesDataGridView.Name = "EmployeesDataGridView";
            this.EmployeesDataGridView.RowHeadersWidth = 62;
            this.EmployeesDataGridView.RowTemplate.Height = 28;
            this.EmployeesDataGridView.Size = new System.Drawing.Size(1047, 270);
            this.EmployeesDataGridView.TabIndex = 18;
            this.EmployeesDataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.EmployeesDataGridView_RowHeaderMouseClick);
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(254, 403);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(183, 20);
            this.MessageLabel.TabIndex = 19;
            this.MessageLabel.Text = "Messages will show here";
            // 
            // EmployeeDirectoryManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1502, 450);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.EmployeesDataGridView);
            this.Controls.Add(this.HireDate);
            this.Controls.Add(this.dateTimePickerHireDate);
            this.Controls.Add(this.textSalary);
            this.Controls.Add(this.Salary);
            this.Controls.Add(this.textRole);
            this.Controls.Add(this.Role);
            this.Controls.Add(this.textDepartment);
            this.Controls.Add(this.Department);
            this.Controls.Add(this.textFullName);
            this.Controls.Add(this.FullName);
            this.Controls.Add(this.textEmployeeID);
            this.Controls.Add(this.EmployeeID);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnExitForm);
            this.Controls.Add(this.btnAddEmployee);
            this.Controls.Add(this.btnUpdateEmployee);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDeleteEmployee);
            this.Name = "EmployeeDirectoryManagerForm";
            this.Text = "EmployeeDirectoryManager";
            this.Load += new System.EventHandler(this.EmployeeDirectoryManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EmployeesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDeleteEmployee;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdateEmployee;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.Button btnExitForm;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label EmployeeID;
        private System.Windows.Forms.TextBox textEmployeeID;
        private System.Windows.Forms.TextBox textFullName;
        private System.Windows.Forms.Label FullName;
        private System.Windows.Forms.TextBox textDepartment;
        private System.Windows.Forms.Label Department;
        private System.Windows.Forms.TextBox textRole;
        private System.Windows.Forms.Label Role;
        private System.Windows.Forms.TextBox textSalary;
        private System.Windows.Forms.Label Salary;
        private System.Windows.Forms.DateTimePicker dateTimePickerHireDate;
        private System.Windows.Forms.Label HireDate;
        private System.Windows.Forms.DataGridView EmployeesDataGridView;
        private System.Windows.Forms.Label MessageLabel;
    }
}

