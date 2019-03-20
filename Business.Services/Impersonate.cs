using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace Business.Services
{
    public class Impersonate : IDisposable
    {
        #region Declaraciones

        private bool _disposed = false;

        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        private WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        public bool Impersonalizar
        {
            get
            {
                try
                {
                    return bool.Parse(ConfigurationManager.AppSettings["impersonalizar"]);
                }
                catch
                {
                    return false;
                }
            }
        }

        public string Usuario
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["usuario"].ToString();
                }
                catch
                {
                    throw new Exception("Debe especificar Usuario en config.");
                }
            }
        }

        public string Contraseña
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["clave"].ToString();
                }
                catch
                {
                    throw new Exception("Debe especificar Contraseña en config.");
                }
            }
        }

        public string Dominio
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["dominio"].ToString();
                }
                catch
                {
                    throw new Exception("Debe especificar Dominio en config.");
                }
            }
        }

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
                    if (impersonationContext != null)
                        this.impersonationContext.Undo();
                }

                this._disposed = true;
            }
        }

        ~Impersonate()
        {
            this.Dispose(false);
        }

        #endregion

        public Impersonate()
        {
            if (this.Impersonalizar)
            {
                try
                {
                    WindowsIdentity tempWindowsIdentity;
                    IntPtr token = IntPtr.Zero;
                    IntPtr tokenDuplicate = IntPtr.Zero;

                    if (RevertToSelf())
                    {
                        if (LogonUserA(this.Usuario, this.Dominio, this.Contraseña,
                                        LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                        {
                            if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                            {
                                tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                                impersonationContext = tempWindowsIdentity.Impersonate();

                                if (impersonationContext != null)
                                {
                                    CloseHandle(token);
                                    CloseHandle(tokenDuplicate);
                                }
                            }

                            return;
                        }
                        //else
                        //    throw new Exception("Imposible impersonalizar usuario, credenciales no válidas");
                    }

                    if (token != IntPtr.Zero)
                        CloseHandle(token);

                    if (tokenDuplicate != IntPtr.Zero)
                        CloseHandle(tokenDuplicate);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
