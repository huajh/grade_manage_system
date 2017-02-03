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
    public partial class addTeacher : Form
    {
        public addTeacher()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = new DataGridViewRow();
            addTeacherSub frm = new addTeacherSub(dataGridView1);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void addTeacher_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();

            col1.Name = "教师编号hjh";
            col1.HeaderText = "教师编号hjh";
            col2.Name = "姓名hjh";
            col2.HeaderText = "姓名hjh";
            col3.Name = "性别hjh";
            col3.HeaderText = "性别hjh";
            col4.Name = "学院编号hjh";
            col4.HeaderText = "学院编号hjh";
            col5.Name = "出生年月hjh";
            col5.HeaderText = "出生年月hjh";
            col6.Name = "学历hjh";
            col6.HeaderText = "学历hjh";
            col7.Name = "职称hjh";
            col7.HeaderText = "职称hjh";
            col8.Name = "电子邮箱hjh";
            col8.HeaderText = "电子邮箱hjh";
            col9.Name = "联系电话hjh";
            col9.HeaderText = "联系电话hjh";

            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns.Add(col3);
            dataGridView1.Columns.Add(col4);
            dataGridView1.Columns.Add(col5);
            dataGridView1.Columns.Add(col6);
            dataGridView1.Columns.Add(col7);
            dataGridView1.Columns.Add(col8);
            dataGridView1.Columns.Add(col9);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选中要删除的行！");
                return;
            }
            if (dataGridView1.CurrentRow.Index < 0 || dataGridView1.CurrentRow.Index >= dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("请选择有效行进行删除！");
                return;
            }
            if (this.dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() == "")
            {
                MessageBox.Show("请选择一项进行删除！");
                return;
            }
            else
            {
                if (MessageBox.Show("确认删除这一行？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.CurrentRow.Index]);
                }
            }
        }

        private void 确定_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            if (dataGridView1.Rows.Count == 1)
            {
                Close();
                return;
            }
            string sql = "";
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                string sql2 = "insert into 教师hjh(教师编号hjh,姓名hjh,性别hjh,学院编号hjh,出生年月hjh,学历hjh,职称hjh,电子邮箱hjh,联系电话hjh) values( ";
                for (int j = 0; j < dataGridView1.Columns.Count - 1; j++) //dataGridView1.Columns.Count-1
                {
                    sql2 += "'" + dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() + "',";
                }
                sql2 += "'" + dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value.ToString().Trim() + "');";
                sql += sql2;

            }
            try
            {
                cc.ExecuteNonQuery(sql);
                MessageBox.Show("添加成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cc.closeConnect();
                Close();
            }

        }

        private void 取消_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
