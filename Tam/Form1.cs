using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Sql;

namespace Tam
{
    public partial class f1 : Form
    {
        public f1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loaddata();
        }
        private void loaddata()
        {
            dataGridView1.Rows.Clear();
            try
            {
                
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(@"E:\WFC#\Tam\Tam\bin\Debug\Sinhvien.xml");
                DataTable dt = new DataTable();
                dt = dataSet.Tables["Sinhvien"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = dr["xID"].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dr["xName"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dr["xAge"].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = dr["xSchool"].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = dr["xPhone"].ToString();
                    i++;
                }
            }
            catch (Exception)
            {

            }
            dataGridView1.Refresh();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument testXML = XDocument.Load(@"E:\WFC#\Tam\Tam\bin\Debug\Sinhvien.xml");
                XElement newSinhvien = new XElement("sinhvien", new XAttribute("xID", textBox5.Text),
                new XElement("xName", textBox1.Text),
                new XElement("xAge", textBox2.Text),
                new XElement("xSchool", textBox3.Text),
                new XElement("xPhone", textBox4.Text));

                var lastStudent = testXML.Descendants("sinhvien").Last();

                testXML.Element("Sinhvien").Add(newSinhvien);
                testXML.Save(@"E:\WFC#\Tam\Tam\bin\Debug\Sinhvien.xml");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            suadata();
        }
        private void suadata()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"E:\WFC#\Tam\Tam\bin\Debug\Sinhvien.xml");
            XPathNavigator nav = doc.CreateNavigator();
            XPathNavigator sinhvien = nav.SelectSingleNode("Sinhvien/sinhvien[@xID='" + textBox5.Text + "']");
            sinhvien.SelectSingleNode("xName").SetValue(textBox1.Text);
            sinhvien.SelectSingleNode("xAge").SetValue(textBox2.Text);
            sinhvien.SelectSingleNode("xSchool").SetValue(textBox3.Text);
            sinhvien.SelectSingleNode("xPhone").SetValue(textBox4.Text);
            doc.Save(@"E:\WFC#\Tam\Tam\bin\Debug\Sinhvien.xml");
            loaddata();

        }
        private void xoadata()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"E:\WFC#\Tam\Tam\bin\Debug\Sinhvien.xml");
            XPathNavigator nav = doc.CreateNavigator();
            XPathNavigator sinhvien = nav.SelectSingleNode("Sinhvien/sinhvien[@xID='" + textBox5.Text + "']");
            sinhvien.DeleteSelf();
            doc.Save(@"E:\WFC#\Tam\Tam\bin\Debug\Sinhvien.xml");
            loaddata();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            xoadata();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Text = dataGridView1.SelectedRows[0].Cells["xID"].Value.ToString();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells["xName"].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells["xAge"].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells["xSchool"].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells["xPhone"].Value.ToString();
        }

        //SqlConnection connect = new SqlConnection(@"Data Source = ADMIN\SQLEXPRESS; Initial Catalog = SinhVien; Persist Security Info=True;User ID = sa");

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = ConnectData.DataConnection())
            {
                int rows = dataGridView1.Rows.Count;
                for(int i = 0; i< rows; ++i)        
                {
                    SqlCommand dCmd = new SqlCommand("Delete ThongTin",cnn);
                    string oString = @"Insert into ThongTin Values(@xID,@xName,@xAge,@xSchool,@xPhone)";
                    SqlCommand oCmd = new SqlCommand(oString, cnn);
                    oCmd.Parameters.AddWithValue("@xName", dataGridView1.Rows[i].Cells["xName"].Value.ToString());
                    oCmd.Parameters.AddWithValue("@xID", dataGridView1.Rows[i].Cells["xID"].Value.ToString());
                    oCmd.Parameters.AddWithValue("@xAge", dataGridView1.Rows[i].Cells["xAge"].Value.ToString());
                    oCmd.Parameters.AddWithValue("@xSchool", dataGridView1.Rows[i].Cells["xSchool"].Value.ToString());
                    oCmd.Parameters.AddWithValue("@xPhone", dataGridView1.Rows[i].Cells["xPhone"].Value.ToString());
                    cnn.Open(); 
                    if (i == 0) dCmd.ExecuteNonQuery();
                    oCmd.ExecuteNonQuery();
                    cnn.Close();
                }
                MessageBox.Show("Updated");
            }
        }
    }
}
