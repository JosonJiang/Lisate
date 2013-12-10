using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lishate.Log
{
    public class Log
    {
        private static void WriteShowLog(LogParameter lp)
        {
            Console.WriteLine(lp.Msg);
        }

        private static void WriteShowLog(string message)
        {
            Console.WriteLine(message);
        }

        private static void WriteTxtLog(LogParameter lp)
        {
            File.AppendAllText(Config.LogPath, lp.Msg);
        }

        private static void WriteTxtLog(string message)
        {
            File.AppendAllText(Config.LogPath, message);
        }

        private static void WriteDatabaseLog(LogParameter lp)
        {

        }

        private static void WriteDatabaseLog(string message)
        {
        }

        public static void WriteDebugLog(String logTrace)
        {
            WriteLog(logTrace, Config.Debug_Level);
        }

        public static void WriteInfoLog(String logTrace)
        {
            WriteLog(logTrace, Config.Info_Level);
        }

        public static void WriteUserLog(String logTrace)
        {
            WriteLog(logTrace, Config.User_Level);
        }

        public static void WriteSystemLog(String logTrace)
        {
            WriteLog(logTrace, Config.System_Level);
        }

        public static void WriteErrorLog(String logTrace)
        {
            WriteLog(logTrace, Config.Error_Level);
        }


        public static void WriteLog(String logTrace, int Level)
        {
            try
            {
                if (Config.LogLevel <= Level)
                {
                    if((Config.LogType & Config.DataBase_Log) > 0)
                    {
                        WriteDatabaseLog(logTrace);
                    }
                    if ((Config.LogType & Config.Txt_Log) > 0)
                    {
                        WriteTxtLog(logTrace);
                    }
                    if ((Config.LogType & Config.Show_Log) > 0)
                    {
                        WriteShowLog(logTrace);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(logTrace);
            }
          
        }

        

        public static void WriteLog(LogParameter lp)
        {
            try
            {
                if (Config.LogLevel <= lp.Level)
                {
                    if ((Config.LogType & Config.DataBase_Log) > 0)
                    {
                        WriteDatabaseLog(lp);
                    }
                    if ((Config.LogType & Config.Txt_Log) > 0)
                    {
                        WriteTxtLog(lp);
                    }
                    if ((Config.LogType & Config.Show_Log) > 0)
                    {
                        WriteShowLog(lp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(lp.Msg);
            }
        }
    }
}
