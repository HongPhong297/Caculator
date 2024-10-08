using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_02
{
    public partial class Form1 : Form
    {
        private int maleCount = 0;
        private int femaleCount = 0;
        public Form1()
        {
            InitializeComponent();
            cboChuyenNganh.SelectedIndex = 0;
            dgvSinhVien.AllowUserToAddRows = false;
        }
        private int GetSelectedRow(string studentID)
        {
            for (int i = 0; i < dgvSinhVien.Rows.Count; i++)
            {
                // Kiểm tra nếu ô không phải là null và có giá trị
                if (/*dgvSinhVien.Rows[i].Cells[0].Value != null &&*/
                    dgvSinhVien.Rows[i].Cells[0].Value.ToString() == studentID)
                {
                    return i;
                }
            }
            return -1;
        }


        private void InsertUpdate(int selectedRow)
        {
            dgvSinhVien.Rows[selectedRow].Cells[0].Value = txtMaSV.Text;
            dgvSinhVien.Rows[selectedRow].Cells[1].Value = txtHoTen.Text;
            dgvSinhVien.Rows[selectedRow].Cells[2].Value = rdoNu.Checked ? "Nữ" :
            "Nam";
            dgvSinhVien.Rows[selectedRow].Cells[3].Value =
            float.Parse(txtDiemTb.Text).ToString();
            dgvSinhVien.Rows[selectedRow].Cells[4].Value = cboChuyenNganh.Text;
        }
        private void btbThemSua_Click(object sender, EventArgs e)
        {
            

            try
            {

                if (txtHoTen.Text == "" || txtMaSV.Text == "" || txtDiemTb.Text == "" )
                {
                    throw new Exception("Vui lòng nhập đầy đủ thông tin sinh viên!" + dgvSinhVien.Rows.Count.ToString());
                }
                //MessageBox.Show(dgvSinhVien.Rows.Count.ToString());
               
                if (dgvSinhVien.Columns.Count == 0)
                {
                    dgvSinhVien.Columns.Add("MaSV", "Mã SV");
                    dgvSinhVien.Columns.Add("HoTen", "Họ Tên");
                    dgvSinhVien.Columns.Add("GioiTinh", "Giới Tính");
                    dgvSinhVien.Columns.Add("DiemTb", "Điểm TB");
                    dgvSinhVien.Columns.Add("ChuyenNganh", "Chuyên Ngành");
                    //dgvSinhVien.Rows.Clear();
                }
                //MessageBox.Show(dgvSinhVien.Rows.Count.ToString());
                //dgvSinhVien.Rows.Clear();
                //MessageBox.Show(dgvSinhVien.Rows.Count.ToString());
                int selectedRow = GetSelectedRow(txtMaSV.Text);
                if (selectedRow == -1)
                {
                    dgvSinhVien.AllowUserToAddRows = false;
                    //selectedRow = dgvSinhVien.Rows.Add();
                    //InsertUpdate(selectedRow);
                    // Thêm sinh viên mới
                    string gender = rdoNu.Checked ? "Nữ" : "Nam";
                    dgvSinhVien.Rows.Add(txtMaSV.Text, txtHoTen.Text, gender, txtDiemTb.Text, cboChuyenNganh.Text);

                    // Cập nhật biến đếm
                    if (gender == "Nam")
                    {
                        maleCount++;
                    }
                    else if (gender == "Nữ")
                    {
                        femaleCount++;
                    }
                    MessageBox.Show("Thêm thành công" + selectedRow.ToString());
                }
                else
                {
                    string currentGender = dgvSinhVien.Rows[selectedRow].Cells[2].Value.ToString();
                    MessageBox.Show("Thêm thành công" + selectedRow.ToString());
                    // Cập nhật thông tin trong hàng đã tồn tại
                    dgvSinhVien.Rows[selectedRow].Cells[1].Value = txtHoTen.Text; // Cập nhật Họ Tên
                    string updatedGender = rdoNu.Checked ? "Nữ" : "Nam"; // Cập nhật Giới Tính
                    dgvSinhVien.Rows[selectedRow].Cells[2].Value = updatedGender;
                    dgvSinhVien.Rows[selectedRow].Cells[3].Value = txtDiemTb.Text; // Cập nhật Điểm TB
                    dgvSinhVien.Rows[selectedRow].Cells[4].Value = cboChuyenNganh.Text; // Cập nhật Chuyên Ngành

                    // Cập nhật lại số lượng nam và nữ nếu giới tính đã thay đổi
                    if (updatedGender == "Nam")
                    {
                        //maleCount++;
                        //// Giả sử giới tính cũ là nữ
                        //femaleCount--;


                        if (currentGender == "Nữ")
                        {
                            maleCount++;
                            femaleCount--;
                        }
                    }
                    else 
                    {
                        //femaleCount++;
                        //// Giả sử giới tính cũ là nam
                        //maleCount--;
                        if (currentGender == "Nam")
                        {
                            femaleCount++;
                            maleCount--;
                        }
                    }


                    MessageBox.Show("Cập nhật thành công. Tổng số hàng: " + dgvSinhVien.Rows.Count.ToString());
                }

                lblTongNam.Text = maleCount.ToString();  // Đặt thuộc tính Text của Label
                lblTongNu.Text = femaleCount.ToString();  // Đặt thuộc tính Text của Label



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }

        private void btbXoa_Click(object sender, EventArgs e)
        {
            // Lấy mã sinh viên từ TextBox
            string studentID = txtMaSV.Text.Trim();

            // Kiểm tra xem TextBox có trống không
            if (string.IsNullOrEmpty(studentID))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên để xóa.");
                return;
            }

            // Biến để theo dõi xem đã tìm thấy hàng cần xóa chưa
            //bool found = false;
            string genderToRemove = ""; // Biến để lưu giới tính của sinh viên cần xóa

            int selectedRow = GetSelectedRow(txtMaSV.Text);
            // Lặp qua các hàng trong DataGridView để tìm hàng cần xóa
            //for (int i = 0; i < dgvSinhVien.Rows.Count; i++)
            //{
            //    // So sánh mã sinh viên với giá trị trong cột 0 (Giả sử cột 0 chứa mã sinh viên)
            //    if (dgvSinhVien.Rows[i].Cells[0].Value.ToString() == studentID)
            //    {
            //        // Lưu giới tính của sinh viên trước khi xóa
            //        genderToRemove = dgvSinhVien.Rows[i].Cells[2].Value.ToString(); // Giả sử cột 2 chứa giới tính

            //        // Nếu tìm thấy hàng cần xóa
            //        dgvSinhVien.Rows.RemoveAt(i); // Xóa hàng
            //        found = true; // Đánh dấu là đã tìm thấy
            //        MessageBox.Show("Xóa thành công mã sinh viên: " + studentID);
            //        break; // Thoát khỏi vòng lặp
            //    }
            //}
            if (selectedRow != -1)
            {
                genderToRemove = dgvSinhVien.Rows[selectedRow].Cells[2].Value.ToString();
                dgvSinhVien.Rows.RemoveAt(selectedRow); // Xóa hàng
                //found = true; // Đánh dấu là đã tìm thấy
                MessageBox.Show("Xóa thành công mã sinh viên: " + studentID);
                // Thoát khỏi vòng lặp

                // Cập nhật biến đếm số nam và nữ
                if (genderToRemove == "Nam")
                {
                    maleCount--; // Giảm số nam
                }
                else if (genderToRemove == "Nữ")
                {
                    femaleCount--; // Giảm số nữ
                }

                // Cập nhật hiển thị tổng số nam và nữ
                lblTongNam.Text = maleCount.ToString();
                lblTongNu.Text = femaleCount.ToString();
            }else
            {
                MessageBox.Show("Không tìm thấy mã sinh viên: " + studentID);
            }
            // Nếu không tìm thấy mã sinh viên
            //if (!found)
            //{
            //    MessageBox.Show("Không tìm thấy mã sinh viên: " + studentID);
            //}
            //else
            //{
            //    // Cập nhật biến đếm số nam và nữ
            //    if (genderToRemove == "Nam")
            //    {
            //        maleCount--; // Giảm số nam
            //    }
            //    else if (genderToRemove == "Nữ")
            //    {
            //        femaleCount--; // Giảm số nữ
            //    }

            //    // Cập nhật hiển thị tổng số nam và nữ
            //    lblTongNam.Text = maleCount.ToString();
            //    lblTongNu.Text = femaleCount.ToString();
            //}
        }

    }
}
