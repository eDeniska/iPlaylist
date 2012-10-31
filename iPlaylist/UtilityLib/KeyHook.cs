using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace UtilityLib
{
    public class KeyHook : IDisposable
    {
        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);

        [DllImport("user32", SetLastError = true)]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);
        
        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalAddAtom(string lpString);
        
        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalDeleteAtom(short nAtom);

        public const int Alt = 1;
        public const int Control = 2;
        public const int Shift = 4;
        public const int Win = 8;

        public const int WM_HOTKEY = 0x312;

        private IntPtr handle;

        public KeyHook(IntPtr p_handle)
        {
            this.handle = p_handle;
            this.KeyID = 0;
        }

        public short KeyID
        {
            get;
            private set;
        }

        /// <summary> register the key hook </summary>
        public void Register(Keys hotkey, int modifiers)
        {
            Unregister();

            try
            {
                // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN docs)
                String atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + this.GetType().FullName + hotkey.ToString();
                this.KeyID = GlobalAddAtom(atomName);
                
                if (this.KeyID == 0)
                {
                    throw new Exception("Unable to generate unique hotkey ID. Error: " + Marshal.GetLastWin32Error().ToString());
                }

                // register the hotkey, throw if any error
                if (!RegisterHotKey(this.handle, (int)this.KeyID, modifiers, (int)hotkey))
                {
                    throw new Exception("Unable to register hotkey. Error: " + Marshal.GetLastWin32Error().ToString());
                }

            }
            catch (Exception e)
            {
                Log.Write(e);
                Unregister();
            }
        }

        public void Unregister()
        {
            if (this.KeyID != 0)
            {
                try
                {
                    UnregisterHotKey(this.handle, KeyID);
                    // clean up the atom list
                    GlobalDeleteAtom(KeyID);
                    KeyID = 0;
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    KeyID = 0;
                }
            }
        }

        public void Dispose()
        {
            Unregister();
        }
    }
}
