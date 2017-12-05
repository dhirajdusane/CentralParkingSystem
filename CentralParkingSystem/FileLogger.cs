using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    public class FileLogger : IDisposable
    {
        public static Dictionary<string, FileLogger> Instance { get; private set; }

        static FileLogger()
        {
            Instance = new Dictionary<string, FileLogger>();
        }

        public FileLogger(string keyName)
        {
            this.keyName = keyName;
            messageQueue = new BlockingCollection<string>();
            LogMessagesAsync();
        }

        private BlockingCollection<string> messageQueue;
        private string keyName;

        public void LogMessage(string message)
        {
            messageQueue.Add(message + "\n");
        }

        private async void LogMessagesAsync()
        {
            await Task.Run(() =>
            {
                string fileName = keyName + "_Log.txt";
                foreach (var item in messageQueue.GetConsumingEnumerable())
                {
                    File.AppendAllText(fileName, item);
                }
            });
        }

        public void Dispose()
        {
            messageQueue.CompleteAdding();
        }
    }
}
