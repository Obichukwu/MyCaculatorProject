using System;
using System.Windows.Forms;

namespace MyCalc
{
    public partial class Form1 : Form {
        private double _firstOp, _secondOp;
        private char _op='0';
        private bool _shouldClear;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnNum_Click(object sender, EventArgs e) {
            var displayedText = txtDisplay.Text;
            if (_shouldClear) {
                displayedText = "0";
                _shouldClear = false;
            }
            if (displayedText == "0")
                displayedText = "";

            if (sender is Button clickedButton) {
                displayedText += clickedButton.Text;
            }

            txtDisplay.Text = displayedText;
        }

        private void btnClear_Click(object sender, EventArgs e) {
            txtDisplay.Text = "0";
        }

        private void btnAddDecimal_Click(object sender, EventArgs e)
        {
            var displayedText = txtDisplay.Text;
            if (displayedText == "0")
                displayedText = "0.";
            else if (!displayedText.Contains(".")){
                displayedText += ".";
            }
            txtDisplay.Text = displayedText;
        }

        private void btnAddSign_Click(object sender, EventArgs e)
        {
            var displayedText = txtDisplay.Text;
            if (displayedText != "0")
            {
                if (displayedText.StartsWith("-"))
                {
                    displayedText = displayedText.TrimStart('-');
                }
                else {
                    displayedText = "-" + displayedText;
                }
                txtDisplay.Text = displayedText;
            }
        }

        private void btnOperationPercent_Click(object sender, EventArgs e) {
            var displayedText =txtDisplay.Text;

            if (displayedText != "0") {
                if (double.TryParse(displayedText, out var input)) {
                    input /= 100;
                    displayedText = input.ToString("##.####");
                }

                txtDisplay.Text = displayedText;
            }
        }

        private void btnOperationEquals_Click(object sender, EventArgs e)
        {
            if (_op =='0')
                return;

            var displayedText = txtDisplay.Text;

            if (double.TryParse(displayedText, out var input))
            {
                _secondOp = input;
            }

            var result= 0d;
            switch (_op) {
                case '+':
                    result = _firstOp + _secondOp;
                    break;
                case '-':
                    result = _firstOp - _secondOp;
                    break;
                case '*':
                    result = _firstOp * _secondOp;
                    break;
                case '/':
                    result = _firstOp / _secondOp;
                    break;
            }

            _op = '0';
            displayedText = result.ToString("##.####");

            txtDisplay.Text = displayedText;
        }

        private void OperationHandler(char opToPerform, object sender, EventArgs e) {
            if (_op != '0')
            {
                btnOperationEquals_Click(sender, e);
            }

            var displayedText = txtDisplay.Text;

            if (double.TryParse(displayedText, out var input))
            {
                _firstOp = input;
                _op = opToPerform;
                _shouldClear = true;
                //displayedText = "0";
            }

            txtDisplay.Text = displayedText;
        }
        private void btnOperationProduct_Click(object sender, EventArgs e) {
            OperationHandler('*', sender, e);
        }

        private void btnOperationDivision_Click(object sender, EventArgs e)
        {
            OperationHandler('/', sender, e);
        }

        private void btnOperationSubtract_Click(object sender, EventArgs e)
        {
            OperationHandler('-', sender, e);
        }

        private void btnOperationAddition_Click(object sender, EventArgs e)
        {
            OperationHandler('+', sender, e);
        }
    }
}
