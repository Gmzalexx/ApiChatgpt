namespace ApiChatgpt
{
    public partial class MainPage : ContentPage
    {
        private readonly ChatGPTService _chatGPTService;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var message = MessageEntry.Text;

            if (!string.IsNullOrWhiteSpace(message))
            {
                ResponseLabel.Text = "Enviando...";
                var response = await _chatGPTService.SendMessageAsync(message);
                ResponseLabel.Text = response;
            }
            else
            {
                ResponseLabel.Text = "Por favor, escribe un mensaje.";
            }
        }
    }

}
