using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Database_Fulloption
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        MySqlConnection connect = new MySqlConnection("host=localhost;user=root;password=123456789;database=computer_shop");//เชื่อมฐานข้อมูล
        MySqlCommand command;//ตัวแปล command สร้างไว้เพื่อส่งข้อมูลที่จ้องการนำเข้าไปในฐานข้อมูล
       


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGirdUpdate();//ฟั่งชั่นดูข่อมูลในฐานข้อมูล
        }
        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {
            metroPanel1.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;//setting panel

        }

        //เงี่ยนไขในการเชื่อต่อข้อมูล เมื่อฐานข้อมูลปิดให้เปิดและเมื่อเปิดอยู่ให้ปิด
        public void openConnection() { 
            if (connect.State == ConnectionState.Closed) 
            { 
                connect.Open();
                metroLabel7.ForeColor = Color.Green;
                metroLabel7.Text = "Connect";
            }
        }
        public void closeConnection() {

            if (connect.State == ConnectionState.Open) {

                connect.Close();
                metroLabel7.ForeColor = Color.Red;
                metroLabel7.Text = "DisConnect";
                

            }
        }

        //ฟั่งชั่น updateข้อมูลในฐานข้อมูลให้เเสดงใน dataGridview (updateข้อมูล)
        public void dataGirdUpdate()
        {

            openConnection();
            string viewData = "SELECT * FROM computer_shop.customer";
            MySqlDataAdapter adapter = new MySqlDataAdapter(viewData, connect);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            closeConnection();


        }
        //ฟั่งชั่น Check ข้อมูลที่ได้รับจาก Form นำข้อมูลส่งเข้าไปในฐานข้อมูล
        public void executeQuery(String query) {

            try
            {
                openConnection();
                command = new MySqlCommand(query, connect);
                if (command.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("ดำเนินการเสร็จสิ้น");
                }
                else {

                    MessageBox.Show("ดำเนินการไม่สำเร็จ");
                }
                

            }
            catch (Exception ex) {

                MessageBox.Show("ERROR\n เนื่องจากมีข้อมูลนี้บนฐานข้อมูลแล้ว\n" + ex.Message);
            }
            finally {
                closeConnection();
            }
        
        
        }

        /*public void executeDataGridView(String dataGridview) {

            try
            {
                openConnection();

                command = new MySqlCommand(dataGridview, connect);
                if (command.ExecuteNonQuery() != 0)
                {

                    MessageBox.Show("แสดงรายการทั้งหมดแล้ว");
                }
                else
                {

                    MessageBox.Show("ดำเนินการไม่สำเร็จ");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR\n" + ex.Message);
            }
            finally
            {
                closeConnection();
            }

        }*/

        



        //ปุ่ม insert/update/delete
        private void metroButton1_Click(object sender, EventArgs e)
        {
            string insert = "INSERT INTO computer_shop.customer(ID,Name,Surname,Address,Phone)VALUES ('" + metroTextBox1.Text + "','" + metroTextBox2.Text + "','" + metroTextBox3.Text + "','" + richTextBox1.Text + "','" + metroTextBox4.Text + "')";
            executeQuery(insert);
            dataGirdUpdate();
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            string update = "UPDATE computer_shop.customer SET Name='" + metroTextBox2.Text + "',Surname='" + metroTextBox3.Text + "',Address='" + richTextBox1.Text + "',Phone='" + metroTextBox4.Text + "' WHERE ID='" + metroTextBox1.Text+ "'";
            executeQuery(update);
            dataGirdUpdate();
        }
        private void metroButton3_Click(object sender, EventArgs e)
        {
            string delete = "DELETE FROM computer_shop.customer WHERE ID='" + metroTextBox1.Text + "'";
            executeQuery(delete);
            dataGirdUpdate();
        }


        //ปุ่ม view ข้อมูลให้แสดงใน dataGridview
        private void metroButton4_Click(object sender, EventArgs e)
        {
            dataGirdUpdate();
        }

        //ฟังชั่นเเมื่อกดดูข้อมูลใน dataGridview ให้มันสามารถนำข้อมูลกลับไปแสดงใน Form ที่ใส่ข้อมูลได้
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            metroTextBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            metroTextBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            metroTextBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            metroTextBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void metroLabel7_Click(object sender, EventArgs e)
        {

        }
    }
}
