using System;
using System.IO;

namespace hobd{

public class Logger
{
    public static bool DUMP = false;
    public static bool TRACE = false;
    public static bool INFO = false;
    public static bool WARN = false;
    public static bool ERROR = true;

    static StreamWriter fs = InitFS();
    
    static StreamWriter InitFS()
    {        
        try{
            return new StreamWriter(new FileStream( Path.Combine(HOBD.AppPath, "log.txt"), FileMode.Append));
        }catch(Exception){
            return null;
        }
    }
    
    public static void error(String comp, String msg)
    {
        if (ERROR) error(comp, msg, null);
    }

    public static void error(String comp, String msg, Exception e)
    {
        if (ERROR) log("ERROR", comp, msg, e);
    }

    public static void info(String comp, String msg)
    {
        if (INFO) log("INFO", comp, msg, null);
    }

    public static void trace(String comp, String msg)
    {
        if (TRACE) log("TRACE", comp, msg, null);
    }

    public static void trace(String msg)
    {
        if (TRACE) log("TRACE", "", msg, null);
    }
    
    public static void dump(String comp, String msg)
    {
        if (DUMP) log("DUMP", comp, msg, null);
    }


    static void log(string level, string comp, string msg, Exception e)
    {
        if (comp == null) comp = "";
        if (msg == null) msg = "";
        
        var nowms = DateTimeMs.NowMs;
        var ts = nowms.ToLongTimeString() + "." + nowms.Millisecond.ToString().PadLeft(3, '0');

        msg = "["+level+"] "+ ts + "["+comp+"]  " + msg;
        if (e != null)
        {
            msg +=  "\n" + e.GetType().ToString() + ": " + e.Message +"\n"+ e.StackTrace;
        }
        System.Console.WriteLine(msg);
        if (fs != null)
        {
            fs.Write(msg+"\n");
            fs.Flush();    
        }
    }

    
    public static void SetLevel(string logLevel)
    {
        if (logLevel == "DUMP") {
            DUMP = TRACE = INFO = WARN = ERROR = true;
            return;
        }
        if (logLevel == "TRACE") {
            DUMP = false;
            TRACE = INFO = WARN = ERROR = true;
            return;
        }
        if (logLevel == "INFO") {
            DUMP = false;
            TRACE = false;
            INFO = WARN = ERROR = true;
            return;
        }
        if (logLevel == "WARN") {
            DUMP = TRACE = INFO = false;
            WARN = ERROR = true;
            return;
        }
        if (logLevel == "ERROR") {
            DUMP = TRACE = INFO = WARN = false;
            ERROR = true;
            return;
        }
        SetLevel("WARN");
    }
}


}