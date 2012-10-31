using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace UtilityLib
{
    /// <summary>
    /// Logger class
    /// </summary>
    public static class Log
    {
        private const String FolderName = @"\iPlaylist";
        private const String FileName = @"\iPlaylist.log";
        private static String fileName;
        private static object fileLock = new object();
        private static StreamWriter logWriter = null;
        
        static Log()
        {
            try
            {
                String logFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + FolderName;
                if (!(Directory.Exists(logFolder)))
                {
                    Directory.CreateDirectory(logFolder);
                }

                fileName = logFolder + FileName;
            }
            catch (Exception e)
            {
                // !!! replace it with something standard

                MessageBox.Show(String.Format("Error creating application folder!" + Environment.NewLine + "{0}", e.Message), 
                    "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw e;
            }
        }

        /// <summary>
        /// Write entry to log
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Write(String message)
        {
            lock (fileLock)
            {
                openLog();
                writeLog(message);
                closeLog();
            }
        }

        /// <summary>
        /// Write entry to log
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="obj">Object to describe</param>
        public static void Write(String message, object obj)
        {
            lock (fileLock)
            {
                openLog();
                writeLog(message);
                ObjectDumper.Write(obj, 0, logWriter);
                closeLog();
            }
        }

        /// <summary>
        /// Write entry to log
        /// </summary>
        /// <param name="e">Exception to describe</param>
        public static void Write(Exception e)
        {
            lock (fileLock)
            {
                openLog();

                StackTrace sTrace = new StackTrace(true);
                writeLog(sTrace.GetFrame(1).GetMethod().DeclaringType.Name + "." + sTrace.GetFrame(1).GetMethod().Name + "() got " +
                    describeException(e));
                closeLog();
            }
        }

        /// <summary>
        /// Write entry to log
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="e">Exception to describe</param>
        public static void Write(String message, Exception e)
        {
            lock (fileLock)
            {
                openLog();
                writeLog(message +  " " + describeException(e));
                closeLog();
            }

        }

        /// <summary>
        /// Calling method name
        /// </summary>
        /// <returns>Method name</returns>
        public static String selfName()
        {
            StackTrace sTrace = new StackTrace(true);
            return sTrace.GetFrame(1).GetMethod().DeclaringType.Name + "." +
                sTrace.GetFrame(1).GetMethod().Name + "()";
        }

        /// <summary>
        /// Returns described exception
        /// </summary>
        /// <param name="e">Exception</param>
        /// <returns>Information about exception</returns>
        private static String describeException(Exception e)
        {
            return e.GetType().Name + ": " + e.Message + " (thrown by " +
                e.TargetSite.ReflectedType.FullName + "." + e.TargetSite.Name + ")" +
                Environment.NewLine + e.StackTrace;
        }

        /// <summary>
        /// Open log file
        /// </summary>
        private static void openLog()
        {
            logWriter = File.AppendText(fileName);
        }

        /// <summary>
        /// Write entry to log file
        /// </summary>
        /// <param name="message">Message to log</param>
        private static void writeLog(String message)
        {
            logWriter.WriteLine("[{0:s}] {1}", DateTime.Now, message);
        }

        /// <summary>
        /// Close log file
        /// </summary>
        private static void closeLog()
        {
            logWriter.Close();
            logWriter = null;
        }
    }
}
