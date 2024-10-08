using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void PhepTinh_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //sender.GetType();
            switch (button.Name)
            {
                case "btnCong":
                    txtKetQua.Text = (Convert.ToDouble(txtSoA.Text) + Convert.ToDouble(txtSoB.Text)).ToString();
                    //txtKetQua.Text = sender.GetType().ToString();
                    break;

                case "btnTru":
                    txtKetQua.Text = (Convert.ToDouble(txtSoA.Text) - Convert.ToDouble(txtSoB.Text)).ToString();
                    break;

                case "btnNhan":
                    txtKetQua.Text = (Convert.ToDouble(txtSoA.Text) * Convert.ToDouble(txtSoB.Text)).ToString();
                    break;

                case "btnChia":
                    if (Convert.ToDouble(txtSoB.Text) != 0)
                    {
                        txtKetQua.Text = (Convert.ToDouble(txtSoA.Text) / Convert.ToDouble(txtSoB.Text)).ToString();
                    }
                    else
                    {
                        txtKetQua.Text = "Không thể chia cho 0";
                    }
                    break;

                default:
                    txtKetQua.Text = "Phép tính không hợp lệ";
                    break;
            }
        }




    }
}

