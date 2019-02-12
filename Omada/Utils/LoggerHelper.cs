using NUnit.Framework;
using Serilog;
using System.Configuration;

namespace Omada.Utils
{
    public static class LoggerHelper
    {
        public static void SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.RollingFile(ConfigurationManager.AppSettings["LogsLocation"] + $"log_{TestContext.CurrentContext.Test.ClassName}.txt")
                .CreateLogger();
        }
    }
}
