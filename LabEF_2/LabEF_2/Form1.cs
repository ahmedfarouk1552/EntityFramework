using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabEF_2.Model;

namespace LabEF_2
{
    public partial class Form1 : Form
    {
        ITIContext db;

        public void fillGide()
        {
            dgv_student.DataSource = db.Students.Where(n => n.Dept_Id == (int)cb_dept.SelectedValue).Select(n => new { id = n.St_Id, name = n.St_Fname + " " + n.St_Lname, age = n.St_Age, address = n.St_Address, dept_id = n.Dept_Id }).ToList();
            dgv_instructor.DataSource = db.Instructors.Where(n => n.Dept_Id == (int)cb_dept.SelectedValue).Select(n => new { id = n.Ins_Id, name = n.Ins_Name, degree = n.Ins_Degree, salary = n.Salary, dept_id = n.Dept_Id }).ToList();
        }
        public Form1()
        {
            InitializeComponent();
             db= new ITIContext();
            cb_dept.DataSource = db.Departments.ToList();
            cb_dept.DisplayMember = "dept_name";
            cb_dept.ValueMember = "dept_id";

            fillGide();
            
        }

        private void cb_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGide();
        }
    }
}
