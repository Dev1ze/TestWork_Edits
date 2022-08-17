using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace test_work
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Form2 f2;
        Timer timer;

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form2"] != null) f2.Close();
            timer = new Timer();
            timer.Interval = 300000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            string pasport_string = maskedTextBox2.Text;
            StringBuilder sb = new StringBuilder(pasport_string);
            if(sb.Length == 9)
            {
                if (Convert.ToString(sb[1]) != "4" || Convert.ToString(sb[2]) != "3" || Convert.ToString(sb[2]) != "3" || Convert.ToString(sb[7]) != "3" || Convert.ToString(sb[8]) != "2")
                {
                    if (textBox1.Text == "" || maskedTextBox1.Text == "+7 (   )-   -  -" || maskedTextBox2.Text == "    -" || textBox2.Text == "" || textBox3.Text == "") MessageBox.Show("Ошибка! Есть пустые поля"); // Проверка на Null
                    else
                    {
                        var person = new Person
                        {
                            Name = textBox1.Text,
                            Namber = maskedTextBox1.Text,
                            Pasport = maskedTextBox2.Text,
                            Login = textBox2.Text,
                            Password = textBox3.Text
                        };
                        string jsonString = JsonConvert.SerializeObject(person);
                        File.AppendAllText("input.json", jsonString);

                        MessageBox.Show("Запись добавлена");
                        textBox1.Text = "";
                        maskedTextBox1.Text = "";
                        maskedTextBox2.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                    }
                }
                else MessageBox.Show("Ошибка! Невозможно иметь такие паспортные данные");
            }
            else MessageBox.Show("Ошибка! Паспортные данные введены не полностью");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f2 = new Form2();
            f2.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) //Запрет вводить цыфры
        {
            string Symbol = e.KeyChar.ToString();
            char number = e.KeyChar;

            if (!Regex.Match(Symbol, @"[а-яА-Я]|[a-zA-Z]").Success & number != 8 & number != 32) e.Handled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            DateTime curDate = DateTime.Now;
            string data;
            data = Convert.ToString(curDate).Replace(" ", "_");
            data = data.Replace(".", "_");
            data = data.Replace(":", "_");

            File.WriteAllText(data + "%input%" + ".json", File.ReadAllText("input.json"));
        }
    }
}
