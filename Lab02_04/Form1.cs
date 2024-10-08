using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupListView();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnThemCapNhat_Click(object sender, EventArgs e)
        {
            // Kiểm tra các ô thông tin đầu vào
            if (txtSoTaiKhoan.Text == "" || txtTenKhachHang.Text == "" || txtDiaChi.Text == "" || txtSoTienTrongTK.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Tìm hàng có cùng số tài khoản
            int selectedRow = GetSelectedRow(txtSoTaiKhoan.Text);

            if (selectedRow == -1)
            {
                // THÊM mới khách hàng
                ListViewItem newItem = new ListViewItem((lvThongTin.Items.Count + 1).ToString()); // STT sẽ là số thứ tự tiếp theo
                newItem.SubItems.Add(txtSoTaiKhoan.Text);
                newItem.SubItems.Add(txtTenKhachHang.Text);
                newItem.SubItems.Add(txtDiaChi.Text);
                newItem.SubItems.Add(txtSoTienTrongTK.Text);

                lvThongTin.Items.Add(newItem); // Thêm hàng vào ListView
                MessageBox.Show("Thêm thông tin thành công!");
            }
            else
            {
                // CẬP NHẬT thông tin khách hàng
                ListViewItem existingItem = lvThongTin.Items[selectedRow];
                existingItem.SubItems[1].Text = txtSoTaiKhoan.Text;  // Cập nhật số tài khoản
                existingItem.SubItems[2].Text = txtTenKhachHang.Text; // Cập nhật tên khách hàng
                existingItem.SubItems[3].Text = txtDiaChi.Text;       // Cập nhật địa chỉ
                existingItem.SubItems[4].Text = txtSoTienTrongTK.Text; // Cập nhật số tiền trong tài khoản

                MessageBox.Show("Cập nhật thông tin thành công!");
            }

            // Sau khi thêm/cập nhật xong, làm trống các ô nhập liệu
            ClearTextBoxes();
        }

        // Hàm tìm hàng theo số tài khoản
        private int GetSelectedRow(string soTaiKhoan)
        {
            for (int i = 0; i < lvThongTin.Items.Count; i++)
            {
                if (lvThongTin.Items[i].SubItems[1].Text == soTaiKhoan)
                {
                    return i;
                }
            }
            return -1; // Không tìm thấy
        }

        // Hàm xóa nội dung các TextBox sau khi thêm/cập nhật
        private void ClearTextBoxes()
        {
            txtSoTaiKhoan.Clear();
            txtTenKhachHang.Clear();
            txtDiaChi.Clear();
            txtSoTienTrongTK.Clear();
        }


        private void SetupListView()
        {
            // Xóa tất cả các cột hiện có (nếu có)
            lvThongTin.Columns.Clear();

            // Thêm các cột mới với tên tương ứng
            lvThongTin.Columns.Add("STT", 50);
            lvThongTin.Columns.Add("Số tài khoản", 100);
            lvThongTin.Columns.Add("Tên khách hàng", 150);
            lvThongTin.Columns.Add("Địa chỉ", 200);
            lvThongTin.Columns.Add("Số tiền trong tài khoản", 150);

            // Cài đặt các thuộc tính của ListView
            lvThongTin.View = View.Details;
            lvThongTin.FullRowSelect = true;
            lvThongTin.GridLines = true;
            //lvThongTin.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        }

    }
}
