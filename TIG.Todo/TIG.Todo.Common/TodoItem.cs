using System.Diagnostics;

namespace TIG.Todo.Common
{
    public class TodoItem : Bindable
    {
        private string _text;
        public string Text
        {
            [DebuggerStepThrough]
            get { return _text; }
            set
            {
                if (value == _text)
                    return;

                _text = value;
                OnPropertyChanged();
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            [DebuggerStepThrough]
            get { return _isCompleted; }
            set
            {
                if (value == _isCompleted)
                    return;

                _isCompleted = value;
                OnPropertyChanged();
            }
        }
    }
}
