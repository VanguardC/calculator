using System;
using System.IO;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private double _firstNumber;
        private double _secondNumber;
        private string _operation;
        private bool _isOperationPerformed;
        private bool _isResultDisplayed;

        public Form1()
        {
            InitializeComponent();
            _isOperationPerformed = false;
            _isResultDisplayed = false;
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (_isResultDisplayed)
            {
                textBox1.Clear();
                _isResultDisplayed = false;
            }

            Button button = sender as Button;
            if (button != null)
            {
                if (_isOperationPerformed)
                {
                    textBox1.Clear();
                    _isOperationPerformed = false;
                }

                if (textBox1.Text == "0")
                    textBox1.Clear();

                textBox1.Text += button.Text;
            }
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (double.TryParse(textBox1.Text, out _firstNumber))
                {
                    _operation = button.Text;
                    _isOperationPerformed = true;
                }
            }
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out _secondNumber))
            {
                double result = 0;
                try
                {
                    switch (_operation)
                    {
                        case "+":
                            result = _firstNumber + _secondNumber;
                            break;
                        case "-":
                            result = _firstNumber - _secondNumber;
                            break;
                        case "*":
                            result = _firstNumber * _secondNumber;
                            break;
                        case "/":
                            if (_secondNumber != 0)
                            {
                                result = _firstNumber / _secondNumber;
                            }
                            else
                            {
                                throw new DivideByZeroException("Ошибка: деление на ноль.");
                            }
                            break;
                        default:
                            MessageBox.Show("Ошибка: выберите операцию.");
                            return;
                    }

                    textBox1.Text = result.ToString();
                    _isResultDisplayed = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: введите корректное число.", "Ошибка");
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            _firstNumber = 0;
            _secondNumber = 0;
            _operation = string.Empty;
            _isOperationPerformed = false;
            _isResultDisplayed = false;
        }

        private void SaveResultButton_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText("result.txt", textBox1.Text);
                MessageBox.Show("Результат сохранен.", "Сохранение");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при сохранении");
            }
        }

        private void LoadResultButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("result.txt"))
                {
                    textBox1.Text = File.ReadAllText("result.txt");
                    MessageBox.Show("Результат загружен.", "Загрузка");
                }
                else
                {
                    MessageBox.Show("Файл с результатом не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при загрузке");
            }
        }

        private void ChangeColorSchemeButton_Click(object sender, EventArgs e)
        {
            this.BackColor = this.BackColor == System.Drawing.Color.LightGray ? System.Drawing.Color.LightBlue : System.Drawing.Color.LightGray;
        }

        private void ChangeFontSizeButton_Click(object sender, EventArgs e)
        {
            float currentSize = textBox1.Font.Size;
            textBox1.Font = new System.Drawing.Font(textBox1.Font.FontFamily, currentSize == 12 ? 16 : 12);
        }
    }
}
