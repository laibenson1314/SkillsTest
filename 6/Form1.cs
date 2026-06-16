using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string letters = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
        private void Form1_Load(object sender, EventArgs e)
        {
            // 讀取檔案
            string[] lines = File.ReadAllLines("C:/SMS/1060306.SM");

            foreach(string line in lines)
            {
                // 拿檔案的詭異東西
                string id = line.Split(',')[0];
                string sex = line.Split(',')[2];
                string error = "";

                // 判斷格式
                if (id.Length != 10 || 
                    !char.IsUpper(id[0]) ||
                    !id.Skip(1).All(char.IsDigit))
                    error = "FORMAT ERROR";

                // 判斷性別
                if (((id[1] == '1' ? "M" : "F") != sex) && string.IsNullOrEmpty(error))
                    error = "SEX CODE ERROR";

                // 最麻煩的判斷 (有問題再問我)
                if (string.IsNullOrEmpty(error))
                {
                    int num = letters.IndexOf(id[0]) + 10;
                    int y = (num / 10) + (num % 10 * 9);
                    for (int i = 1; i <= 9; i++) y += int.Parse($"{id[i]}") * (9 - i > 1 ? 9 - i : 1);
                    if (y % 10 != 0) error = "CHECK SUM ERROR";
                }

                // 輸出
                dataGridView1.Rows.Add(
                    id,
                    line.Split(',')[1],
                    sex,
                    error
                );
            }
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }
    }
}
