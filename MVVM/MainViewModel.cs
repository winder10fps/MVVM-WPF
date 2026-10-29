using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVM
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Book _selectedBook;

        public ObservableCollection<Book> Books { get; set; }
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            Books = new ObservableCollection<Book>
            {
                new Book { Title = "Война и мир", Author = "Л. Толстой", Year = 1869 },
                new Book { Title = "1984", Author = "Дж. Оруэлл", Year = 1949 }
            };

            AddCommand = new RelayCommand(OnAdd);
            EditCommand = new RelayCommand(OnEdit, CanEditOrDelete);
            DeleteCommand = new RelayCommand(OnDelete, CanEditOrDelete);
        }

        private void OnAdd(object obj)
        {
            var newBook = new Book();
            var editWindow = new BookEditWindow(newBook);

            if (editWindow.ShowDialog() == true)
            {
                Books.Add(newBook);
            }
        }

        private void OnEdit(object obj)
        {
            if (SelectedBook == null) return;

            var bookCopy = new Book
            {
                Title = SelectedBook.Title,
                Author = SelectedBook.Author,
                Year = SelectedBook.Year
            };

            var editWindow = new BookEditWindow(bookCopy);

            if (editWindow.ShowDialog() == true)
            {
                SelectedBook.Title = bookCopy.Title;
                SelectedBook.Author = bookCopy.Author;
                SelectedBook.Year = bookCopy.Year;
            }
        }

        private void OnDelete(object obj)
        {
            if (SelectedBook != null)
            {
                Books.Remove(SelectedBook);
            }
        }

        private bool CanEditOrDelete(object obj)
        {
            return SelectedBook != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}