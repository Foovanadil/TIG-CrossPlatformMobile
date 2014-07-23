using System.Diagnostics;
using TIG.TODO.Common.SQLite;

namespace TIG.Todo.Common
{
    public class TodoItem : Bindable, IHaveID
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

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
				OnPropertyChanged("Text");
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
				OnPropertyChanged("IsCompleted");
            }
        }

		public override string ToString ()
		{
			return Text;
		}
    }
}
