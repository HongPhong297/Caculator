using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int mahoadon = 0;
        private int totalAmount = 0;
        private List<Button> bt = new List<Button>();
        private AppRapPhimEntities1 dbContext = new AppRapPhimEntities1();

        private void button20_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn != null)
            {
                if (btn.BackColor == Color.White) // Nếu màu trắng (có thể chọn)
                {
                    btn.BackColor = Color.Blue;
                    bt.Add(btn);// Đổi thành màu xanh (đã chọn)
                    TinhTien(bt);
                    txtThanhTien.Text = $"{totalAmount} VNĐ";
                }
                else if (btn.BackColor == Color.Blue) // Nếu màu xanh (đã chọn)
                {
                    btn.BackColor = Color.White; // Đổi lại thành màu trắng (bỏ chọn)
                    bt.Remove(btn);
                    TinhTien(bt);
                    txtThanhTien.Text = $"{totalAmount} VNĐ";
                }
                else if (btn.BackColor == Color.Yellow) // Nếu màu vàng (đã bán)
                {
                    MessageBox.Show("Vé này đã được bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Không tiếp tục tính tiền nếu ghế đã bán
                }             
            }

        }
        private void TinhTien(List<Button> list)
        {
            totalAmount = 0;
            foreach (Button bt in list)
            {
                if (int.Parse(bt.Text) <= 5) // Dãy 1
                {
                    totalAmount += 30000;
                }
                else if (int.Parse(bt.Text) <= 10) // Dãy 2
                {
                    totalAmount += 40000;
                }
                else if (int.Parse(bt.Text) <= 15) // Dãy 3
                {
                    totalAmount += 50000;
                }
                else if (int.Parse(bt.Text) <= 20) // Dãy 4
                {
                    totalAmount += 80000;
                }
                
            }

        }
        private void btnChon_Click(object sender, EventArgs e)
        {
            



            //txtThanhTien.Text = $"{totalAmount} VNĐ";





            //if (bt.Count > 0)
            //{
            //    string gender = rdoNu.Checked ? "Nữ" : "Nam";
            //    this.mahoadon += 1;

            //    dgvDanhSach.Rows.Add(mahoadon, txtHoTen.Text, DateTime.Today.ToShortDateString(), totalAmount.ToString());
            //    MessageBox.Show($"Chọn thành công mã hóa đơn: {mahoadon}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //    foreach (Button buttons in bt)
            //    {

            //        if (buttons.BackColor == Color.Blue) // Nếu nút đang màu xanh (đã chọn)
            //        {
            //            buttons.BackColor = Color.Yellow; // Đổi sang màu vàng (đã bán)
            //        }

            //    }
            //}
            //else
            //{
            //    MessageBox.Show($"Vui lòng chọn vé !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //
            // Lấy thông tin từ các control
            try
            {
                if (bt.Count > 0)
                {
                    string hoTen = txtHoTen.Text;
                    string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ"; // Lấy giới tính
                    string soDienThoai = txtSDT.Text;
                    string khuVuc = cmbKhuVuc.SelectedItem?.ToString(); // Lấy khu vực đã chọn
                    decimal tongTien = totalAmount;

                    // Tạo đối tượng NguoiMua
                    var nguoiMua = new NguoiMua
                    {
                        HoTen = hoTen,
                        GioiTinh = gioiTinh,
                        SoDienThoai = soDienThoai,
                        KhuVuc = khuVuc
                    };

                    // Lưu thông tin người mua vào cơ sở dữ liệu
                    dbContext.NguoiMuas.Add(nguoiMua);
                    dbContext.SaveChanges();
                    // Lấy mã người mua vừa thêm
                    int maNguoiMua = nguoiMua.MaNguoiMua;

                    // Lưu thông tin hóa đơn
                    var hoaDon = new HoaDon
                    {
                        MaNguoiMua = maNguoiMua,
                        NgayDat = DateTime.Now,
                        TongTien = tongTien
                    };

                    dbContext.HoaDons.Add(hoaDon);
                    dbContext.SaveChanges();

                    // Lấy mã hóa đơn vừa thêm
                    int maHoaDon = hoaDon.MaHoaDon;

                    // Lưu thông tin chi tiết hóa đơn cho từng ghế đã chọn
                    foreach (var seat in bt)
                    {
                        var chiTietHoaDon = new ChiTietHoaDon
                        {
                            MaHoaDon = maHoaDon,
                            MaVe = seat.Text // Sử dụng số ghế làm mã vé
                        };
                        dbContext.ChiTietHoaDons.Add(chiTietHoaDon);
                    }

                    dbContext.SaveChanges();

                    MessageBox.Show("Thông tin đã được lưu thành công!");
                    dgvDanhSach.Rows.Add(maHoaDon, hoTen, DateTime.Now.ToString("dd/MM/yyyy"), tongTien);
                    foreach (Button buttons in bt)
                    {

                        if (buttons.BackColor == Color.Blue) // Nếu nút đang màu xanh (đã chọn)
                        {
                            buttons.BackColor = Color.Yellow; // Đổi sang màu vàng (đã bán)
                        }

                    }
                    //
                    totalAmount = 0;

                    bt.Clear();
                    txtThanhTien.Text = $"0 VNĐ";
                }else
                    MessageBox.Show($"Vui lòng chọn vé !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Thất bại");
            }
            
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {

            foreach (Control control in this.Controls)
            {
                if (control is Button btn)
                {
                    if (btn.BackColor == Color.Blue) // Nếu nút đang màu xanh (đã chọn)
                    {
                        btn.BackColor = Color.White; // Đổi sang màu vàng (đã bán)
                    }
                }
            }
            txtThanhTien.Text = $"0 VNĐ";


            totalAmount = 0;
            bt.Clear();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận thoát chương trình
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát chương trình
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CaiDatThongTin(this);
        }

        private void CaiDatThongTin(Form1 form1)
        {
            txtThanhTien.Text = "0 VNĐ";

            


            cmbKhuVuc.Items.Add("TP HCM");
            cmbKhuVuc.Items.Add("Đồng Nai");
            cmbKhuVuc.Items.Add("Phú Yên");
            cmbKhuVuc.SelectedIndex = 2;
            //
            // Truy vấn để lấy dữ liệu cần thiết
            dgvDanhSach.Columns.Clear();

            // Thêm các cột vào DataGridView
            dgvDanhSach.Columns.Add("MaHd", "Mã Hóa Đơn");
            dgvDanhSach.Columns.Add("TenKH", "Tên Khách Hàng");
            dgvDanhSach.Columns.Add("NgayDat", "Ngày Đặt");
            dgvDanhSach.Columns.Add("TongTien", "Tổng Tiền");

            // Thiết lập AutoSizeColumnsMode để các cột lấp đầy dgv
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Điều chỉnh chiều rộng cột "Tên Khách Hàng" lớn hơn một chút
            dgvDanhSach.Columns["TenKH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDanhSach.Columns["TenKH"].Width = 120; // Tùy chỉnh chiều rộng

            // Truy vấn để lấy dữ liệu cần thiết
            var query = from hd in dbContext.HoaDons
                        join nm in dbContext.NguoiMuas on hd.MaNguoiMua equals nm.MaNguoiMua
                        select new
                        {
                            MaHoaDon = hd.MaHoaDon,
                            TenKhachHang = nm.HoTen,
                            NgayDat = hd.NgayDat,   
                            TongTien = hd.TongTien
                        };

            // Chuyển đổi dữ liệu sang dạng danh sách và thêm vào DataGridView
            foreach (var item in query.ToList())
            {
                dgvDanhSach.Rows.Add(item.MaHoaDon, item.TenKhachHang, item.NgayDat, item.TongTien);
            }

        }


    }
}
