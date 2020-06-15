using Common;
using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace Server
{
    class Program
    {
        public static void Main(String[] args)
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository,new FileInfo("log4setting.xml"));
            var log = LogManager.GetLogger(repository.Name, "NETCorelog4net");

            log.Info("test");
            log.Error("test");
            //SockerManager.Instance.StartListening();
        }

    }
}
