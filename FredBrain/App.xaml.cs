using System.Windows;
using System.Windows.Controls;

namespace FredBrain
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(TextBoxSelectAll));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewMouseLeftButtonDownEvent, new RoutedEventHandler(TextBoxSelectAll));

            base.OnStartup(e);
        }

        private void TextBoxSelectAll(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.SelectAll();
                e.Handled = true;
                textBox.Focus();
            }
        } 
    }
}
