using Microsoft.Phone.Controls;
using System;
using System.Collections.Specialized;
using System.Linq;
using TIG.Todo.Common;
using TIG.Todo.DataProviders;
using TIG.Todo.WindowsPhone8.Scheduling;

namespace TIG.Todo.WindowsPhone8
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            ViewModel = new TodoListViewModel(DataProvider.RetrieveValue<TodoItem[]>());
            DataContext = ViewModel;
            ViewModel.TodoItems.CollectionChanged += TodoItems_CollectionChanged;
        }

        public TodoListViewModel ViewModel { get; set; }

        private void TodoItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TodoItem[] incompleteTodoItems =
              ViewModel.TodoItems.Where(item => !item.IsCompleted).ToArray();

            bool saveSuccessful = DataProvider.SaveValue<TodoItem[]>(ViewModel.TodoItems.ToArray());
            LiveTileHelper.UpdateLiveTile(false, incompleteTodoItems);
        }

        private void ActivateLiveTile(object sender, EventArgs e)
        {
            TodoItem[] incompleteTodoItems =
                ViewModel.TodoItems.Where(item => !item.IsCompleted).ToArray();

            LiveTileHelper.UpdateLiveTile(true, incompleteTodoItems);
            TaskHelper.StartTileUpdaterTask();
        }
    }
}