TIG-CrossPlatformMobile
=======================

This repo is for the San Diego Tech Immersion Group's cross platform mobile track.

Instructions for Windows Phone Lab1. (DON'T FORGET TO SWITCH TO THE LAB1 BRANCH)

STEP 0:
//TODO: 0.0 - Implement INotifyPropertyChanged - http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx
//TODO: 0.1 - Use the CallerMemberName attribute - http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callermembernameattribute(v=vs.110).aspx

STEP 1:
//TODO: 1 - Reading about ICommand and DelegateCommand<T>
//  http://msdn.microsoft.com/en-us/library/system.windows.input.icommand(v=vs.110).aspx
//  http://stackoverflow.com/questions/11960488/any-winrt-icommand-commandbinding-implementaiton-samples-out-there

STEP 2:
//TODO: 2.0 - Inherit from Bindable in order get support for INotifyPropertyChanged
//TODO: 2.1 - Create a string property named Text that calls OnPropertyChanged so when it is set it will notify it has changed
//TODO: 2.2 - Create a bool property named IsCompleted that notifies when it has changed (HINT: same process as TODO: 2.1)

STEP 3:
//TODO: 3.0 - Inherit from Bindable in order get support for INotifyPropertyChanged
//TODO: 3.1 - Create an ObservableCollection (OC) with generic type TodoItem property named TodoItems that notifies when it has changed (HINT: same process as TODO: 2.1)
//  Reading about ObservableCollection (OC) http://msdn.microsoft.com/en-us/library/ms668604(v=vs.110).aspx
//TODO: 3.2 - Create a TodoItem property named NewTodoItem that notifies when it has changed
//TODO: 3.3 - Create a DelegateCommand with generic type object readonly property named AddCommand
//  The getter of the AddCommand property should always return the same instance of field _addCommand
//  (HINT: _addCommand = _addCommand ?? new DelegateCommand<object>(AddExecutedHandler, AddCanExecuteHandler);)
//TODO: 3.4 - Create the private method bool AddCanExecuteHandler(object obj)
//  The method should always return true
//TODO: 3.5 - Create the private method void AddExecutedHandler(object obj)
//  The method should first add NewTodoItem to the OC TodoItems then set NewTodoItem with a new instance of TodoItem
//TODO: 3.6 - Create a DelegateCommand with generic type TodoItem readonly property named DeleteCommand
//  The getter of the DeleteCommand property should always return the same instance of field _deleteCommand
//  (HINT: TODO: 3.3)
//TODO: 3.7 - Create the private method bool DeleteCanExecuteHandler(TodoItem todoItem)
//  The method should always return true
//TODO: 3.8 - Create the private method void DeleteExecutedHandler(TodoItem todoItem)
//  The method should remove the todoItem paramerter from the OC TodoItems

STEP 4:
//TODO: 4 - Reading about IValueConverter http://msdn.microsoft.com/en-us/library/system.windows.data.ivalueconverter(v=vs.110).aspx

STEP 5: 
<!--TODO: 5.0 - Inside phone:PhoneApplicationPage.Resources
    Create a resource for the CompletedToForegroundColor converter with x:Key="CompletedToForegroundColor" -->
<!--TODO: 5.1 - Inside phone:PhoneApplicationPage.DataContext
    Specify TodoListViewModel
    -->
<!--TODO: 5.2 - Bind TextBox.Text to NewTodoItem.Text with Mode=TwoWay-->
<!--TODO: 5.3 - Bind Button.Command to AddCommand-->
<!--TODO: 5.4 - Bind ItemsControl.ItemsSource to TodoItems-->
<!--Reading about ItemsControl http://msdn.microsoft.com/en-us/library/system.windows.controls.itemscontrol(v=vs.110).aspx-->
<!--Reading about ItemsControl.ItemTemplate http://msdn.microsoft.com/en-us/library/system.windows.controls.itemscontrol.itemtemplate(v=vs.110).aspx
    and DataTemplate http://msdn.microsoft.com/en-us/library/system.windows.datatemplate(v=vs.110).aspx
    and more about DataTemplating http://msdn.microsoft.com/en-us/library/ms742521(v=vs.110).aspx-->
<!--TODO: 5.5 - Bind CheckBox.IsChecked to IsCompleted with Mode=TwoWay-->
<!--TODO: 5.6 - Bind TextBlock.Text to Text
    Bind TextBlock.Foreground to IsCompleted with Converter 
        set to a StaticResource of the CompletedToForegroundColor converter created in TODO: 5.0-->
<!--TODO: 5.7 - Bind Button.Command to DataContext.DeleteCommand with ElementName set to ContentPanel
    Bind Button.CommandParameter to current context (HINT: {Binding} with out a path binds to current context)-->
<!--TODO: 5.8 - If you get stuck here are some HINTS-->
