using Microsoft.Phone.Controls;
using System;
using System.Collections.Specialized;
using System.Linq;
using TIG.Todo.Common;
using TIG.Todo.DataProviders;
using TIG.Todo.Geofencing;
using TIG.Todo.WindowsPhone8.Scheduling;

namespace TIG.Todo.WindowsPhone8
{
    public partial class MainPage : PhoneApplicationPage
    {
        //TODO: 5.0 - Update MainPage.xaml.cs

        //TODO: 5.1  Create ViewModel auto property of type TodoListViewModel
        #region Solution 5.1
        //public TodoListViewModel ViewModel { get; set; } 
        #endregion

        public MainPage()
        {
            InitializeComponent();

            //TODO: 5.2 - Instantiate ViewModel passing DataProvider.RetrieveValue<TodoItem[]>() into the ctor of the ViewModel ctor
            // Set DataContext equal to ViewModel
            // Subscribe to ViewModel.TodoItems.CollectionChanged
           
            #region Solution 5.2
            //ViewModel = new TodoListViewModel(DataProvider.RetrieveValue<TodoItem[]>()); 
            //DataContext = ViewModel;
            //ViewModel.TodoItems.CollectionChanged += TodoItems_CollectionChanged;
            #endregion


        }

        private void TodoItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //TODO: 5.3 - Create an array of incompeteTodoItems by querying ViewModel.TodoItems where item.IsCompleted == false
            //  Call DataProvider.SaveValue<TodoItem[]> passing in ViewModel.TodoItems.ToArray()
            //  Call LiveTileHelper.UpdateLiveTile passing false and the array of incompleteTodoItems

            #region Solution 5.3
            //TodoItem[] incompleteTodoItems =
            //  ViewModel.TodoItems.Where(item => !item.IsCompleted).ToArray();

            //bool saveSuccessful = DataProvider.SaveValue<TodoItem[]>(ViewModel.TodoItems.ToArray());
            //LiveTileHelper.UpdateLiveTile(false, incompleteTodoItems); 
            #endregion
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: 7.0 - Create an array of incompeteTodoItems by querying ViewModel.TodoItems where item.IsCompleted == false (hint see 5.3)
            //  Call LiveTileHelper.UpdateLiveTile passing false and the array of incompleteTodoItems (hint see 5.3)
            //  Call TaskHelper.StartTileUpdaterTask
           
            #region Solution 7.0
            //TodoItem[] incompleteTodoItems =
            //    ViewModel.TodoItems.Where(item => !item.IsCompleted).ToArray();

            //LiveTileHelper.UpdateLiveTile(true, incompleteTodoItems); 
            //TaskHelper.StartTileUpdaterTask();
            #endregion
        }

        private void ActivateGeofence(object sender, EventArgs e)
        {
            GeofencingHelper.TryCreateGeofence();
        }

    }
}