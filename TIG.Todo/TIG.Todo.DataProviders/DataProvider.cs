using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;

namespace TIG.Todo.DataProviders
{
    public class DataProvider
    {
        private static readonly object SAVE_SYNC = new object();

        public static bool SaveValue<T>(T value)
        {
            string key = typeof(T).Name;

            IsolatedStorageSettings.ApplicationSettings[key] = value;
            return SaveChanges();
        }

        public static T RetrieveValue<T>()
        {
            string key = typeof(T).Name;

            T value;
            IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out value);

            return value;
        }

        private static bool SaveChanges()
        {
            bool saveSuccessful = false;
            lock (SAVE_SYNC)
            {
                try
                {
                    // Documentation claims this is not thread safe: http://msdn.microsoft.com/en-us/library/system.io.isolatedstorage.isolatedstoragesettings(v=VS.95).aspx
                    IsolatedStorageSettings.ApplicationSettings.Save();
                    saveSuccessful = true;
                }
                catch (SerializationException e)
                {
                    // Developer error... only notify at debug time.
                    Debug.Assert(false);
                }
                catch (IsolatedStorageException e)
                {
                    // If we reach this one of two problems have occured:
                    // 1. We are out of memory: http://msdn.microsoft.com/en-us/library/system.io.isolatedstorage.isolatedstoragesettings.save(v=vs.95).aspx
                    //		however, this should not happen unless the the phone has absolutly no free memory because we have access to all available free memory
                    // 2. There was a thread access issue (claims to be possible by documentation: http://msdn.microsoft.com/en-us/library/system.io.isolatedstorage.isolatedstoragesettings(v=VS.95).aspx)

                    Debug.Assert(false); // just trying to get the developer's attention :)
                }
            }
            return saveSuccessful;
        }

    }
}
