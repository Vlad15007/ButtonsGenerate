using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
        private void MyButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] == button)
                {
                    int row = i / (tableLayoutPanel1.ColumnCount - 1);
                    int column = i % (tableLayoutPanel1.ColumnCount - 1);
                    string mess = string.Format("{0} {1}", row, column);
                    MessageBox.Show(mess);
                }
            }
        }

        public void Generate()
        {
            int x = 5;
            int y = 10;

            Padding padding = new Padding(0);
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Button myButton = new Button() { Text = i + "/" + j, Dock = DockStyle.Fill, BackColor = Color.Transparent, Margin = padding, FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderSize = 0 } };
                    //Button myButton = new Button() { Text = "*", Dock = DockStyle.Fill, BackColor = Color.Transparent, Margin = padding, FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderSize = 0 } };
                    myButton.Click += MyButton_Click;
                    this.Invoke((MethodInvoker)delegate {
                        tableLayoutPanel1.Controls.Add(myButton, j, i);
                    });

                    if (tableLayoutPanel1.ColumnCount <= x)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            tableLayoutPanel1.ColumnCount++;
                            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                            tableLayoutPanel1.ColumnStyles[j].Width = 100 / x;
                        });
                        
                    }
                }
                this.Invoke((MethodInvoker)delegate {
                    tableLayoutPanel1.RowCount++;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent));
                    tableLayoutPanel1.RowStyles[i].Height = 100 / y;
                });
            }
        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            Thread generate = new Thread(Generate);
            generate.IsBackground = true;
            generate.Start();
        }
    }
}
