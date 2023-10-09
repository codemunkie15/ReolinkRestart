namespace ReolinkRestart.Ui
{
    public class UiMessageHandler : IUiMessageHandler
    {
        public void ShowMessage(string title, string message)
        {
            MessageBox.Show(message, title);
        }
    }
}
