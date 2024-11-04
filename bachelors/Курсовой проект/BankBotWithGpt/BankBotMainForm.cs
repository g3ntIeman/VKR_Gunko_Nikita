using BankBotWithGpt.Logic;
using Microsoft.Extensions.Configuration;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;

namespace BankBotWithGpt
{
    public partial class BankBotMainForm : Form
    {
        private string prevReq = "";
        private ChatModel chatModel;
        
        public BankBotMainForm()
        {
            InitializeComponent();
            chatModel = new ChatModel();
        }
        /// <summary>
        /// Событие нажатия на кнопку "Очистить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxReq.Text = String.Empty;
            textBoxAnswer.Text = String.Empty;
        }
        /// <summary>
        /// Событие нажатия на кнопку "Задать вопрос"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonGet_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxReq.Text))
            {
                MessageBox.Show("Необходимо заполнить поле 'Вопрос'", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                if (textBoxReq.Text != prevReq)
                {
                    prevReq = textBoxReq.Text;
                    try
                    {
                        var answer = await ChatModel.AnswerQuestion(textBoxReq.Text);
                        if (!string.IsNullOrEmpty(answer))
                        {
                            textBoxAnswer.Text = "Вопрос: " + textBoxReq.Text + Environment.NewLine + Environment.NewLine + "Ответ: " + answer;
                            textBoxReq.Text = String.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void textBoxReq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonGet_Click(sender, e);
            }
        }
    }
}