using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace TIG.Todo.WindowsPhone8
{
    public class CompletedToForegroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCompleted;
            if (value != null && bool.TryParse(value.ToString(), out isCompleted))
            {
                return new SolidColorBrush( isCompleted ? Colors.Green : Colors.White);
            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}