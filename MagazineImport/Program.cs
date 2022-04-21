﻿using System;
using Serilog;
using Serilog.Core;

namespace MagazineImport
{
    public class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += AppUnhandledException;

            using (var logger = BuildLogger())
            {
                logger.Information("Magazines import started ...");

                var job = new MagazineImportJob();

                try
                {
                    job.Run();
                }
                catch (Exception ex)
                {
                    LogUnhandledException(ex);
                }

                logger.Information("All magazine imports completed.");
            }
        }

        private static void AppUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (Log.Logger != null && e.ExceptionObject is Exception exception)
            {
                LogUnhandledException(exception);

                // It's not necessary to flush if the application isn't terminating.
                if (e.IsTerminating)
                {
                    Log.CloseAndFlush();
                }
            }
        }

        private static Logger BuildLogger()
        {
            var logger = (Logger) new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("AppName", "MagazineImportJob")
                .CreateLogger();

            Log.Logger = logger;

            return logger;
        }

        private static void LogUnhandledException(Exception e)
        {
            Log.Logger?.Error(e, "Uncaught exception importing magazines!");
        }
    }
}
