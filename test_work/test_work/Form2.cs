using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace test_work
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader("input.json"));
            reader.SupportMultipleContent = true;
            while (true)
            {
                if (!reader.Read())
                {
                    break;
                }

                JsonSerializer serializer = new JsonSerializer();
                Person person2 = serializer.Deserialize<Person>(reader);
                textBox1.Text += person2.Name + "   " + person2.Namber + "         " + person2.Pasport + "      " + person2.Login + "      " + person2.Password + "         " + Environment.NewLine;
            }
            reader.Close(); //Решение проблемы с вылетом приложения когда открыта форма с JSON-файлом
            textBox1.ReadOnly = true;
        }
    }
}
