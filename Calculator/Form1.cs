using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        enum TrangThai { Start,FirstNumberEntry, SecondNumberEntry, ComputeResult, ComputeUnaryResult, ErrorState , OperatorEntry };

        TrangThai m_TrangThai = TrangThai.Start;
        double m_FirstNumber = 0;
        double m_SecondNumber = 0;
        string m_Operator = "";
        string m_stringHienThi = "";
        string m_Unary = "";
        public Form1()
        {
            InitializeComponent();
            btn0.Click += btnNumber_Click;
            btn1.Click += btnNumber_Click;
            btn2.Click += btnNumber_Click;
            btn3.Click += btnNumber_Click;
            btn4.Click += btnNumber_Click;
            btn5.Click += btnNumber_Click;
            btn6.Click += btnNumber_Click;
            btn7.Click += btnNumber_Click;
            btn8.Click += btnNumber_Click;
            btn9.Click += btnNumber_Click;
            btnCham.Click += btnNumber_Click;

            btnCong.Click += btnOperator_Click;
            btnTru.Click += btnOperator_Click;
            btnNhan.Click += btnOperator_Click;
            btnChia.Click += btnOperator_Click;

            btnNghichDao.Click += btnUnary_Click;
            btnPhanTram.Click += btnUnary_Click;
            btnBinhPhuong.Click += btnUnary_Click;
            btnCanHai.Click += btnUnary_Click;
            btnNegate.Click += btnUnary_Click;

            btnBang.Click += btnBang_Click;
            btnBackSpace.Click += btnBackSpace_Click;
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            string number = ((Button)sender).Text;
            switch (m_TrangThai)
            {
                case TrangThai.Start:
                    m_TrangThai = TrangThai.FirstNumberEntry;
                    m_stringHienThi += number;
                    HienThi();
                    break;
                case TrangThai.FirstNumberEntry:
                    m_TrangThai = TrangThai.FirstNumberEntry;
                    m_stringHienThi += number;
                    HienThi();
                    break;
                case TrangThai.SecondNumberEntry:
                    m_TrangThai = TrangThai.SecondNumberEntry;
                    m_stringHienThi += number;
                    HienThi();
                    break;
                case TrangThai.OperatorEntry:
                    m_TrangThai = TrangThai.SecondNumberEntry;
                    m_stringHienThi = number;
                    HienThi();
                    break;
                case TrangThai.ComputeResult:
                    break;
                case TrangThai.ComputeUnaryResult:
                    break;
                case TrangThai.ErrorState:
                    break;
                default:
                    break;
            }
        }

        private void HienThi()
        {
            txtHienThi.Text = m_stringHienThi;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            String operatorSymbol = ((Button)sender).Text;
            switch (m_TrangThai)
            {
                case TrangThai.Start:
                    txtHienThi.Text = "Error";
                    m_TrangThai = TrangThai.ErrorState;
                    break;
                case TrangThai.FirstNumberEntry:
                    m_TrangThai = TrangThai.OperatorEntry;
                    m_Operator = operatorSymbol;
                    m_FirstNumber = double.Parse(m_stringHienThi);
                    break;
                case TrangThai.SecondNumberEntry:
                    btnBang_Click(sender, e);
                    m_TrangThai = TrangThai.OperatorEntry;
                    m_Operator = operatorSymbol;
                    m_FirstNumber = double.Parse(m_stringHienThi);
                    break;
                case TrangThai.OperatorEntry:
                    m_Operator = operatorSymbol;
                    break;
                case TrangThai.ComputeResult:
                    m_FirstNumber = double.Parse(m_stringHienThi);
                    m_Operator = operatorSymbol;
                    m_TrangThai = TrangThai.OperatorEntry;
                    break;
                case TrangThai.ComputeUnaryResult:
                    break;
                case TrangThai.ErrorState:
                    break;
                default:
                    break;
            }
        }
        private void btnBang_Click(object sender, EventArgs e)
        {
            switch (m_TrangThai)
            {
                case TrangThai.Start:
                    break;
                case TrangThai.FirstNumberEntry:
                    m_TrangThai=TrangThai.ComputeResult;
                    m_FirstNumber = double.Parse(m_stringHienThi);
                    HienThi();
                    break;
                case TrangThai.SecondNumberEntry:
                    m_TrangThai = TrangThai.ComputeResult;
                    m_SecondNumber = double.Parse(m_stringHienThi);
                    switch (m_Operator)
                    {
                        case "+":
                            m_stringHienThi = (m_FirstNumber + m_SecondNumber).ToString();
                            break;
                        case "-":
                            m_stringHienThi = (m_FirstNumber - m_SecondNumber).ToString();
                            break;
                        case "*":
                            m_stringHienThi = (m_FirstNumber * m_SecondNumber).ToString();
                            break;
                        case "/":
                            if (m_SecondNumber == 0)
                                MessageBox.Show("Khong the chia cho 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else m_stringHienThi = (m_FirstNumber / m_SecondNumber).ToString();
                            break;
                    }

                    m_FirstNumber = double.Parse(m_stringHienThi); // Update for next operation
                    HienThi();
                    break;
                case TrangThai.OperatorEntry:
                    break;
                case TrangThai.ComputeResult:
                    break;
                case TrangThai.ComputeUnaryResult:
                    break;
                case TrangThai.ErrorState:
                    break;
                default:
                    break;
            }
        }
        private void btnUnary_Click(object sender, EventArgs e)
        {
            String unarySymbol = ((Button)sender).Text;
            switch (m_TrangThai)
            {
                case TrangThai.Start:
                    break;
                case TrangThai.FirstNumberEntry:
                    m_FirstNumber = double.Parse(m_stringHienThi);
                    switch (unarySymbol)
                    {
                        case "1/x":
                            if (m_FirstNumber == 0)
                                MessageBox.Show("Khong the chia cho 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else m_stringHienThi = (1 / m_FirstNumber).ToString();
                            break;
                        case "x^2":
                            m_stringHienThi = (m_FirstNumber * m_FirstNumber).ToString();
                            break;
                        case "Sqrt(x)":
                            m_stringHienThi = (Math.Sqrt(m_FirstNumber)).ToString();
                            break;
                        case "%":
                            m_stringHienThi = (m_FirstNumber / 100).ToString();
                            break;
                        case "+/-":
                            m_stringHienThi =(-m_FirstNumber).ToString();
                            break;
                    }
                    m_FirstNumber = double.Parse(m_stringHienThi); // Update for next operation
                    HienThi();
                    break;
                case TrangThai.SecondNumberEntry:
                    m_SecondNumber = double.Parse(m_stringHienThi);
                    switch (unarySymbol)
                    {
                        case "1/x":
                            if (m_SecondNumber == 0)
                                MessageBox.Show("Khong the chia cho 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else m_stringHienThi = (1 / m_SecondNumber).ToString();
                            break;
                        case "x^2":
                            m_stringHienThi = (m_SecondNumber * m_SecondNumber).ToString();
                            break;
                        case "Sqrt(x)":
                            m_stringHienThi = (Math.Sqrt(m_SecondNumber)).ToString();
                            break;
                        case "%":
                            m_stringHienThi = (m_SecondNumber / 100).ToString();
                            break;
                        case "+/-":
                            m_stringHienThi = (-m_FirstNumber).ToString();
                            break;
                    }
                    m_SecondNumber = double.Parse(m_stringHienThi); // Update for next operation
                    HienThi();
                    break;
                case TrangThai.OperatorEntry:
                    break;
                case TrangThai.ComputeResult:
                    m_FirstNumber = double.Parse(m_stringHienThi);
                    switch (unarySymbol)
                    {
                        case "1/x":
                            if (m_FirstNumber == 0)
                            {
                                m_TrangThai=TrangThai.ErrorState;
                                MessageBox.Show("Khong the chia cho 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else m_stringHienThi = (1 / m_FirstNumber).ToString();
                            break;
                        case "x^2":
                            m_stringHienThi = (m_FirstNumber * m_FirstNumber).ToString();
                            break;
                        case "Sqrt(x)":
                            m_stringHienThi = (Math.Sqrt(m_FirstNumber)).ToString();
                            break;
                        case "%":
                            m_stringHienThi = (m_FirstNumber / 100).ToString();
                            break;
                        case "+/-":
                            m_stringHienThi = (-m_FirstNumber).ToString();
                            break;
                    }
                    m_FirstNumber = double.Parse(m_stringHienThi); // Update for next operation
                    HienThi();
                    break;
                case TrangThai.ComputeUnaryResult:

                    break;
                case TrangThai.ErrorState:
                    break;
                default:
                    break;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_TrangThai = TrangThai.Start;
            m_stringHienThi = "";
            m_FirstNumber = 0;
            m_SecondNumber = 0;
            m_Operator = "";
            HienThi();
        }
        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            switch (m_TrangThai)
            {
                case TrangThai.FirstNumberEntry:
                    if (m_stringHienThi.Length > 0) // Kiểm tra nếu TextBox không rỗng
                    {
                        m_stringHienThi = m_stringHienThi.Substring(0, m_stringHienThi.Length - 1);
                        HienThi();
                    }
                    break;
                case TrangThai.SecondNumberEntry:
                    if (m_stringHienThi.Length > 0) // Kiểm tra nếu TextBox không rỗng
                    {
                        m_stringHienThi = m_stringHienThi.Substring(0, m_stringHienThi.Length - 1);
                        HienThi();
                    }
                    break;
                case TrangThai.ComputeResult:
                    btnClear_Click(sender, e);
                    break;
                case TrangThai.ComputeUnaryResult:
                    btnClear_Click(sender, e); 
                    break;
                default: break;
            }
        }

    }
}
