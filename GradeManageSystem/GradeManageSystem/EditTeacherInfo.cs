using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GradeManageSystem
{
    public partial class EditTeacherInfo : Form
    {
        DataGridViewRow dgvr;
        public EditTeacherInfo(DataGridViewRow dgvr)
        {
            this.dgvr = dgvr;
            InitializeComponent();
        }

        private void EditTeacherInfo_Load(object sender, EventArgs e)
        {
            this.Text += "(" + dgvr.Cells[0].Value.ToString().Trim() + ")";
            label11.Text = dgvr.Cells[0].Value.ToString().Trim();
            textBox2.Text = dgvr.Cells[1].Value.ToString().Trim();
            textBox3.Text = dgvr.Cells[2].Value.ToString().Trim();
            comboBox1.Text = dgvr.Cells[3].Value.ToString().Trim();
            textBox5.Text = dgvr.Cells[4].Value.ToString().Trim();
            textBox6.Text = dgvr.Cells[5].Value.ToString().Trim();
            textBox7.Text = dgvr.Cells[6].Value.ToString().Trim();
            textBox8.Text = dgvr.Cells[7].Value.ToString().Trim();
            textBox9.Text = dgvr.Cells[8].Value.ToString().Trim();
            textBox10.Text = dgvr.Cells[9].Value.ToString().Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            string sql = "update 教师hjh set ";
            int flag = 0;
            if (comboBox1.Text != dgvr.Cells[3].Value.ToString().Trim())
            {
                
                string sql2 = "select * from 学院hjh where 学院名称hjh = '" + comboBox1.Text + "'";
                DataSet ds = cc.GetDataSet(sql2);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("未能到该学院名称的学院编号，学院信息有误！");
                    return;
                }
                string str = ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim();
                ds.Dispose();
                sql += "学院编号hjh = '" + str + "' ";
                flag = 1;
            }
            if (textBox2.Text != dgvr.Cells[1].Value.ToString().Trim())
            {
                if (flag == 1) sql += ",";
                sql += "姓名hjh = '" + textBox2.Text + "'";
                flag = 1;
            }
            if (textBox3.Text != dgvr.Cells[2].Value.ToString().Trim())
            {
                if (flag == 1) sql += ",";
                sql += "性别hjh = '" + textBox3.Text + "'";
                flag = 1;
            }
            if (textBox5.Text != dgvr.Cells[4].Value.ToString().Trim())
            {
                if (flag == 1) sql += ",";
                sql += "出生年月hjh = '" + textBox5.Text + "'";
                flag = 1;
            }
            if (textBox6.Text != dgvr.Cells[5].Value.ToString().Trim())
            {
                if (flag == 1) sql += ",";
                sql += "学历hjh = '" + textBox6.Text + "'";
                flag = 1;
            }
            if (textBox7.Text != dgvr.Cells[6].Value.ToString().Trim())
            {
                if (flag == 1) sql += ",";
                sql += "职称hjh = '" + textBox7.Text+"'";
                flag = 1;
            }
            if (textBox8.Text != dgvr.Cells[7].Value.ToString().Trim())
            {
                if (flag == 1) sql += ",";
                sql += "电子邮箱hjh = '" + textBox8.Text+"'";
                flag = 1;
            }
            if (textBox9.Text != dgvr.Cells[8].Value.ToString().Trim())
            {
                if (flag == 1) sql += ",";
                sql += "联系电话hjh = '" + textBox9.Text + "'";
                flag = 1;
            }
            if (textBox10.Text != dgvr.Cells[9].Value.ToString().Trim())
            {
                if (flag == 1) sql += ",";
                sql += "权限级别hjh = " + textBox10.Text;
                flag = 1;
            }
            sql += " where 教师编号hjh = '" + label11.Text + "'";
            try
            {
                if (flag == 1)
                {
                    cc.ExecuteNonQuery(sql);
                    MessageBox.Show("修改成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cc.closeConnect();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
