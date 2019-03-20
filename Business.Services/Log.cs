using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Business.Services
{
    public class LogEvent
    {
        public enum Tipo
        {
            Ninguno = 0,
            Evento = 1,
            Archivo = 2,
            Ambos = 3
        }

        /// <summary>
        /// Registra un evento en el log.
        /// Si el tipoLog es archivo, la ruta debe estar especificada en el web.config bajo la key "logPath".
        /// </summary>
        /// <param name="aplicacion">Nombre de la aplicación que utiliza la clase</param>
        /// <param name="mensaje">Mensage a registrar </param>
        /// <param name="tipo">Especifica elm tipo de mensaje</param>
        /// <param name="tipoLog">Especifica donde almacenará el mensaje</param>
        public void Registrar(string aplicacion, string mensaje, EventLogEntryType tipo, Tipo tipoLog)
        {
            try
            {
                this.Save(tipoLog, aplicacion, mensaje, tipo);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Registra un evento en el log y lo almacena utilizando el tipo registrado en el web.config en la key "logRegistrar".
        /// Si el espeficiado es archivo, la ruta debe estar especificada en el web.config bajo la key "logPath".
        /// </summary>
        /// <param name="aplicacion">Nombre de la aplicación que utiliza la clase, utilizada como nombre del archivo</param>
        /// <param name="mensaje">Mensage a registrar </param>
        /// <param name="tipo">Especifica elm tipo de mensaje</param>
        public void Registrar(string aplicacion, string mensaje, EventLogEntryType tipo)
        {
            try
            {
                var tipoLog = (Tipo)(int.Parse(ConfigurationManager.AppSettings["logRegistrar"]));

                this.Save(tipoLog, aplicacion, mensaje, tipo);
            }
            catch
            {
                throw;
            }
        }

        public void Registrar(string aplicacion, Exception excepcion, EventLogEntryType tipo)
        {
            try
            {
                var tipoLog = (Tipo)(int.Parse(ConfigurationManager.AppSettings["logRegistrar"]));

                string errror = excepcion.InnerException != null ? excepcion.InnerException.ToString() : excepcion.Message.ToString();
                string mensaje = $"{errror}{Environment.NewLine}{excepcion.StackTrace}";

                this.Save(tipoLog, aplicacion, mensaje, tipo);
            }
            catch
            {
                throw;
            }
        }

        #region Privadas

        private void Save(Tipo tipolog, string aplicacion, string mensaje, EventLogEntryType tipo)
        {
            if (string.IsNullOrEmpty(aplicacion))
                return;

            if (tipolog == Tipo.Evento || tipolog == Tipo.Ambos)
                new Event().Registar(aplicacion, mensaje, tipo);

            if (tipolog == Tipo.Archivo || tipolog == Tipo.Ambos)
                new Log().Registar(aplicacion, mensaje, tipo);
        }

        #endregion
    }

    #region Privadas

    /// <summary>
    /// Registra un evento en la consola de eventos
    /// </summary>
    class Event : LogEventBase
    {
        public override void Registar(string aplicacion, string mensaje, EventLogEntryType tipo)
        {
            try
            {
                EventLog.WriteEntry(aplicacion, mensaje, tipo);
            }
            catch
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Registra un evento en un archivo de texto
    /// </summary>
    class Log : LogEventBase
    {
        private string _path = ConfigurationManager.AppSettings["logPath"];

        public override void Registar(string aplicacion, string mensaje, EventLogEntryType tipo)
        {
            try
            {
                if (string.IsNullOrEmpty(this._path))
                    throw new ArgumentException();

                string nombre = string.Format("Log{0}_{1}.txt", aplicacion, DateTime.Today.ToString("yyyyMMdd"));
                string fullPath = null;

                FileInfo info = new FileInfo(this._path);

                if ((info.Attributes.Equals(FileAttributes.Directory)))
                    fullPath = info.FullName;
                else
                    fullPath = info.ToString();

                fullPath = string.Format("{0}\\{1}", fullPath, nombre);

                StringBuilder linea = new StringBuilder();

                linea.AppendLine(string.Format("[{0:00}:{1:00}:{2:00}:{3:0000}] {4} | {5}",
                                                       DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond,
                                                       tipo.ToString(), mensaje));
                bool error = true;
                int cont = 0;

                while (error)
                {
                    try
                    {
                        File.AppendAllText(fullPath, linea.ToString());

                        error = false;
                    }
                    catch
                    {
                        cont += 1;

                        if (cont > 3)
                            error = false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }

    abstract class LogEventBase : IDisposable
    {
        private bool _disposed = false;

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //nothing
                }

                this._disposed = true;
            }
        }

        ~LogEventBase()
        {
            this.Dispose(false);
        }

        abstract public void Registar(string aplicacion, string mensaje, EventLogEntryType tipo);
    }

    #endregion
}
