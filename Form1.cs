using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Toan_Lop_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] chuoi = tbDeBai.Text.Split('.');
            chuoi[0] = chuoi[0].Trim();
            chuoi[1] = chuoi[1].Trim();
            chuoi[2] = chuoi[2].Trim();
            chuoi[2] = chuoi[2].Substring(0, chuoi[2].Length - 1); //cắt bỏ dấu ?

            if(chuoi[2].Contains("cả") || chuoi[2].Contains("hết thảy") || chuoi[2].Contains("hai") || chuoi[2].Contains("và") || chuoi[1].Contains("/"))
            {
                //2 phép tính, sẽ lấy số từ câu 1 trước vì cả 4 phép tính ở câu 1 đều có một số.
                string s = "";
                int num2 = 1;
                int num1 = 1;
                int num0 = 1;
                //lấy số từ câu 1
                string[] nums0 = Regex.Split(chuoi[0], @"\D+");
                foreach (string value in nums0)
                {
                    if (!string.IsNullOrEmpty(value))
                        num0 = int.Parse(value);
                }

                /// phép cộng, trừ, nhân, chia đơn giản (không có dấu /)
                if (!chuoi[1].Contains("/"))
                {
                    //Lấy số từ câu 2
                    string[] nums1 = Regex.Split(chuoi[1], @"\D+");
                    foreach (string value in nums1)
                    {
                        if (!string.IsNullOrEmpty(value))
                            num1 = int.Parse(value);
                    }

                    //--------------------------------tính câu 2------------------------------------//
                    
                    //trường hợp đặc biệt cho phép nhân và phép chia thì lấy từ đầu câu đến trước các từ ít/nhiều/nặng/nhẹ
                    if (!chuoi[1].Contains("lần") && !chuoi[1].Contains("gấp"))
                    {
                        s = "Số " + chuoi[1].Substring(chuoi[1].IndexOf(num1.ToString()) + 2).Trim();
                        s = s + " ";
                    }
                    
                    if (chuoi[1].Contains("ít"))
                    {
                        s = s + chuoi[1].Substring(0, chuoi[1].IndexOf("ít"));
                    }
                    else if (chuoi[1].Contains("nhiều"))
                    {
                        s = s + chuoi[1].Substring(0, chuoi[1].IndexOf("nhiều"));
                    }
                    else if (chuoi[1].Contains("nặng"))
                    {
                        s = s + chuoi[1].Substring(0, chuoi[1].IndexOf("nặng"));
                    }
                    else if(chuoi[1].Contains("nhẹ"))
                    {
                        s = s + chuoi[1].Substring(0, chuoi[1].IndexOf("nhẹ"));
                    }

                    s = s + "là:";
                    s = s + Environment.NewLine;
                    s += "\t";
                    
                    if ((chuoi[1].Contains("nhiều") || chuoi[1].Contains("nặng")) && chuoi[1].Contains("lần"))//nhân
                    {
                        s = s + num0.ToString();
                        s = s + " x ";
                        s = s + num1.ToString();
                        s = s + " = ";
                        num1 = num0 * num1;
                    }
                    else if ((chuoi[1].Contains("ít") || chuoi[1].Contains("nhẹ")) && chuoi[1].Contains("lần"))//chia
                    {
                        s = s + num0.ToString();
                        s = s + " / ";
                        s = s + num1.ToString();
                        s = s + " = ";
                        num1 = num0 / num1;
                    }
                    else if (chuoi[1].Contains("nhiều") || chuoi[1].Contains("nặng"))//cộng
                    {
                        s = s + num0.ToString();
                        s = s + " + ";
                        s = s + num1.ToString();
                        s = s + " = ";
                        num1 = num0 + num1;
                    }
                    else //trừ
                    {
                        s = s + num0.ToString();
                        s = s + " - ";
                        s = s + num1.ToString();
                        s = s + " = ";
                        num1 = num0 - num1;
                    }
                    
                    s = s + num1.ToString();
                    s = s + " (dv).";
                    s = s + Environment.NewLine;
                    
                }
                else//phép toán có dấu /, quy ước toán lớp 3 thì số trước dấu / chỉ là 1 nên HIỆN TẠI không cần xử lý số này, chỉ cần lấy được số sau dấu /
                {
                    //lấy số từ câu 2
                    string[] nums1 = chuoi[1].Split('/');
                    //num1 = int.Parse(((chuoi[1].Substring(chuoi[1].LastIndexOf(" ") + 1)).Trim()));
                    string[] tem = nums1[1].Split(' ');
                    num2 = int.Parse(tem[0].Trim());

                    //Tính câu 2
                    s = s + "Số ";
                    s = s + chuoi[2].Substring(chuoi[2].IndexOf("nhiêu") + 6);
                    s = s + " ";
                    s = s + chuoi[1].Substring(0, chuoi[1].IndexOf(num1.ToString()));
                    s = s + "là:";
                    s = s + Environment.NewLine;
                    s += "\t";

                    //Lưu ý: quy ước toán lớp 3 thì x/y luôn có x nhỏ hơn y và x luôn bằng 1 nên làm toán chia, sau này có điều kiện sẽ cải tiến.
                    s = s + num0.ToString();
                    s = s + " / ";
                    s = s + num2.ToString();
                    s = s + " = ";
                    //vì num1 = 1 không dùng tới trong phép toán có dấu / nên sử dụng làm kết quả trung gian
                    num1 = num0 / num2;
                    s = s + num1.ToString();
                    s = s + " (dv).";
                    s = s + Environment.NewLine;
                }
                //-----------------------------tính kết quả cuối cùng------------------------------//
                s = s + "Số ";
                s = s + chuoi[2].Substring(chuoi[2].IndexOf("nhiêu") + 6);
                s = s + chuoi[2].Substring(3, chuoi[2].IndexOf("bao") - 3);
                s = s + "là:";
                s = s + Environment.NewLine;
                s += "\t";

                s = s + num0.ToString();
                s = s + " dau ";
                s = s + num1.ToString();
                s = s + " = result (dv).";
                s = s + Environment.NewLine;
                s = s + "Đáp số: result (dv).";

                int ketqua = 0;
                string dau = "";
                if (chuoi[2].Contains("còn lại"))
                {
                    ketqua = num0 - num1;
                    dau = "-";
                }
                else
                {
                    ketqua = num0 + num1;
                    dau = "+";
                }
                string donvi = (chuoi[2].Substring(chuoi[2].LastIndexOf("nhiêu") + 6)).Split(' ')[0];
                if (donvi == "ki-lô-gam")
                    donvi = "kg";
                else if (donvi == "mét")
                    donvi = "m";

                s = s.Replace("dv", donvi);
                s = s.Replace("result", ketqua.ToString());
                s = s.Replace("dau", dau);
                tbLoiGiai.Text = s;
            }
            else //1 phép tính
            {
                //Lấy số từ câu 1
                int num0 = 1;
                string[] nums0 = Regex.Split(chuoi[0], @"\D+");
                foreach (string value in nums0)
                {
                    if (!string.IsNullOrEmpty(value))
                        num0 = int.Parse(value);
                }

                //Lấy số từ câu 2
                int num1 = 1;
                string[] nums1 = Regex.Split(chuoi[1], @"\D+");
                foreach (string value in nums1)
                {
                    if (!string.IsNullOrEmpty(value))
                        num1 = int.Parse(value);
                }
                string s = "";
               
                s = "Số " + chuoi[2].Substring(chuoi[2].IndexOf("nhiêu") + 6);
                s = s + chuoi[2].Substring(3, chuoi[2].IndexOf("bao") - 3);
                s = s + "là:";
                s = s + Environment.NewLine;
                s += "\t";

                //tạo concept chung cho lời giải, đặt các phép tính bằng dấu "+", kết quả bằng "result" tượng trưng, sau đó thay lại.
                s = s + num0.ToString();
                s = s + " + ";
                s = s + num1.ToString();
                s = s + " = result (dv).";
                s = s + Environment.NewLine;
                s = s + "Đáp số: result (dv).";


                int ketqua = 0;
                if ((chuoi[1].Contains("thêm")|| chuoi[1].Contains("cho")) && !chuoi[1].Contains("chia"))
                {
                    //cộng
                    ketqua = num0 + num1;
                }
                else if (chuoi[1].Contains("đã") || chuoi[1].Contains("hết") || chuoi[1].Contains("còn") || chuoi[1].Contains("ít") || chuoi[1].Contains("bớt"))
                {
                    //trừ
                    s = s.Replace("+", "-");
                    ketqua = num0 - num1;
                }
                else if (!chuoi[1].Contains("chia") && (chuoi[1].Contains("mỗi") || chuoi[1].Contains("gấp") || chuoi[1].Contains("lần") || chuoi[1].Contains("Mỗi")))
                {
                    //nhân
                    s = s.Replace("+", "x");
                    ketqua = num0 * num1;
                }
                else //if (chuoi[1].Contains("chia") || chuoi[2].Contains("gấp") || chuoi[1].Contains("lần") || chuoi[1].Contains("mấy"))
                {
                    //chia
                    s = s.Replace("+", "/");
                    ketqua = num0 / num1;
                }

                string donvi = (chuoi[2].Substring(chuoi[2].LastIndexOf("nhiêu") + 6)).Split(' ')[0];
                if (donvi == "ki-lô-gam")
                    donvi = "kg";
                else if (donvi == "mét")
                    donvi = "m";

                s = s.Replace("dv", donvi);
                tbLoiGiai.Text = s.Replace("result", ketqua.ToString());

            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            tbLoiGiai.Text = "";
            tbDeBai.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát khỏi chương trình không?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                Application.Exit();
            
        }

        private void tbLoiGiai_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
