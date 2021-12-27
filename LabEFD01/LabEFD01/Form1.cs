using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabEFD01.Model;

namespace LabEFD01
{
    public partial class Form1 : Form
    {
        ITIContext db;
        //method of combobox of topic fill
        public void comboTopic()
        {
            cb_topic.DataSource = db.Topics.ToList();
            cb_topic.DisplayMember = "top_name";
            cb_topic.ValueMember = "top_id";
        }
        public Form1()
        {
            InitializeComponent();
            db = new ITIContext();
          dgv_course.DataSource =  db.Courses.Select(n => new { id = n.Crs_Id, name = n.Crs_Name, duration = n.Crs_Duration, topic_id = n.Top_Id }).ToList();
            comboTopic();
        }

        private void txt_add_Click(object sender, EventArgs e)
        {
            try
            {
                Course cr = new Course()
                {
                    Crs_Id = int.Parse(txt_id.Text),
                    Crs_Name = txt_name.Text,
                    Crs_Duration = int.Parse(txt_duration.Text),
                    Top_Id = (int)cb_topic.SelectedValue
                };
                db = new ITIContext();
                db.Courses.Add(cr);
                db.SaveChanges();
                dgv_course.DataSource = db.Courses.Select(n => new { id = n.Crs_Id, name = n.Crs_Name, duration = n.Crs_Duration, topic_id = n.Top_Id }).ToList();
                txt_id.Text = txt_name.Text = txt_duration.Text = " ";
                comboTopic();  //combobox of topic fill
                lbl_result.Text = "Course Added";
            }
            catch
            {
                lbl_result.Text = "Invalid";
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dgv_course.SelectedRows[0].Cells[0].Value;
                Course cr = db.Courses.Where(n => n.Crs_Id == id).SingleOrDefault();
                db.Courses.Remove(cr);
                db.SaveChanges();
                dgv_course.DataSource = db.Courses.Select(n => new { id = n.Crs_Id, name = n.Crs_Name, duration = n.Crs_Duration, topic_id = n.Top_Id }).ToList();

                comboTopic(); //combobox of topic fill 
                btn_add.Visible = true;
                lbl_result.Text = "Course Deleted";
                lbl_result.ForeColor = Color.Blue;
            }
            catch
            {
                lbl_result.Text = "Invalid";
            }


        }

        private void dgv_course_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = (int)dgv_course.SelectedRows[0].Cells[0].Value;
            Course cr = db.Courses.Where(n => n.Crs_Id == id).SingleOrDefault();
            txt_id.Text = cr.Crs_Id.ToString();
            txt_name.Text = cr.Crs_Name;
            txt_duration.Text = cr.Crs_Duration.ToString();
            cb_topic.SelectedValue = cr.Top_Id;
            txt_id.Enabled = false;
            btn_add.Visible = false;

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {

                int id = int.Parse(txt_id.Text);
                Course cr = db.Courses.Where(n => n.Crs_Id == id).SingleOrDefault();
                cr.Crs_Name = txt_name.Text;
                cr.Crs_Duration = int.Parse(txt_duration.Text);
                cr.Top_Id = (int)cb_topic.SelectedValue;
                db.SaveChanges();
                dgv_course.DataSource = db.Courses.Select(n => new { id = n.Crs_Id, name = n.Crs_Name, duration = n.Crs_Duration, topic_id = n.Top_Id }).ToList();
                txt_id.Text = txt_name.Text = txt_duration.Text = " ";
                btn_add.Visible = true;
                txt_id.Enabled = true;
                btn_update.Visible = false;
                comboTopic();   //combobox of topic fill
                lbl_result.Text = "Course Updated";
                lbl_result.ForeColor = Color.Green;
            }
            catch
            {
                lbl_result.Text = "Invalid";
            }




        }
    }
}
