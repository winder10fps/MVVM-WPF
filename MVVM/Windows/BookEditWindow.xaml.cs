using MVVM;
using System.Windows;

namespace MVVM
{
    public partial class BookEditWindow : Window
    {
        public BookEditWindow(Book book)
        {
            InitializeComponent();
            DataContext = book;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace((DataContext as Book)?.Title))
            {
                MessageBox.Show("Название не может быть пустым.");
                return;
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}