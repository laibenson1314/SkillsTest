using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines("C:/SMS/1060308.SM");
            foreach(var line in lines)
            {
                string op = line.Split(',')[2];
                int b = int.Parse(line.Split(',')[0]);
                int a = int.Parse(line.Split(',')[1]);
                int y = int.Parse(line.Split(',')[3]);
                int x = int.Parse(line.Split(',')[4]);

                int up = 0, down = a * x;

                switch (op)
                {
                    case "+": up = (b * x) + (a * y); break;
                    case "-": up = (b * x) - (a * y); break;
                    case "*": up = b * y; break;
                    case "/": up = b * x; down = a * y; break;
                }

                int gcd = GCD(up, down);
                dataGridView1.Rows.Add(
                    $"{b}/{a}",
                    op,
                    $"{x}/{y}",
                    up == down ? "1" :
                        down == 0 || up == 0 ? "0" :
                        $"{up / gcd}/{down / gcd}"
                ); 
            }
        }
        private int GCD(int a, int b) { return b == 0 ? a : GCD(b, a % b); }
    }
}
