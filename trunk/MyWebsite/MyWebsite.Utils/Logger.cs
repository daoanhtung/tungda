using System;
using System.Linq;
using System.Reflection;
using log4net;
namespace MyWebsite.Utils
{
    /// <summary>
    /// Create log4net
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// create function static void log
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void Log(string errorMessage, bool isSendMail)
        {
            try
            {
                //read config form web.config
                ILog log;
                if (isSendMail)
                {
                    log = GetLogger(Constants.LoggerNameSendEmail);
                }
                else
                {
                    log = GetLogger(Constants.LoggerNameWriteFile);
                }
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    //add error log and send mail 
                    log.Error(errorMessage);
                }
            }
            catch (Exception exception)
            {
                var log = GetLogger(Constants.LoggerNameWriteFile);
                //write log to file log.txt
                log.Error(exception.Message);
            }
            
        }
        public static ILog GetLogger(string loggerName)
        {
            log4net.Config.XmlConfigurator.Configure();
            return LogManager.GetLogger(loggerName);
        }
    }
}
