﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Business.Services
{
    /// <summary>
    /// Objeto de tipo RUT utilizado para la devolución de la función de validación de este tipo de valor.
    /// </summary>
    public class rutObject
    {
        /// <summary>
        /// Código real del RUT
        /// </summary>
        public int Mantiza = 0;

        /// <summary>
        /// Dígito verificador del RUT
        /// </summary>
        public string Digito = String.Empty;

        /// <summary>
        /// Rut compuesto (ej:11.111.111-1)
        /// </summary>
        public string compuesto = String.Empty;

        /// <summary>
        /// Define si el rut validado es correcto o no
        /// </summary>
        public bool valido = false;

        /// <summary>
        /// Objeto de tipo RUT utilizado para la devolución de la función de validación de este tipo de valor.
        /// </summary>
        public rutObject()
        {

        }

        /// <summary>
        /// Objeto de tipo RUT utilizado para la devolución de la función de validación de este tipo de valor.
        /// </summary>
        /// <param name="rutArg">Código real del RUT</param>
        /// <param name="verificadorArg">Dígito verificador del RUT</param>
        /// <param name="compuestoArg">Rut compuesto (ej:11.111.111-1)</param>
        public rutObject(int rutArg, string verificadorArg, string compuestoArg)
        {
            this.Mantiza = rutArg;
            this.Digito = verificadorArg;
            this.compuesto = compuestoArg;
            this.valido = true;
        }
    }

    /// <summary>
    /// Una clase de extensión para la operación "Between"
    /// patron de nombre IsBetweenXX donde X = I -> Inclusive, X = E -> Exclusive
    /// </summary>
    public static class BetweenExtensions
    {
        /// <summary>
        /// Verifica que el valor entregado esta entre los rangos min (Inclusive) y max (Inclusive) <![CDATA[min <= value <= max]]> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">El valor a verificar</param>
        /// <param name="min">Valor minimo permitido</param>
        /// <param name="max">Valor maximo permitido</param>
        /// <returns>true si el value esta entre el min y el max else false</returns>
        public static bool IsBetweenII<T>(this T value, T min, T max) where T : IComparable
        {
            return (min.CompareTo(value) <= 0) && (value.CompareTo(max) <= 0);
        }

        /// <summary>
        /// Verifica que el valor entregado esta entre los rangos min (Exclusive) y max (Inclusive) <![CDATA[min <= value <= max]]> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">El valor a verificar</param>
        /// <param name="min">Valor minimo permitido</param>
        /// <param name="max">Valor maximo permitido</param>
        /// <returns>true si el value esta entre el min y el max else false</returns>
        public static bool IsBetweenEI<T>(this T value, T min, T max) where T : IComparable
        {
            return (min.CompareTo(value) < 0) && (value.CompareTo(max) <= 0);
        }

        /// <summary>
        /// Verifica que el valor entregado esta entre los rangos min (Inclusive) y max (Exclusive) <![CDATA[min <= value <= max]]> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">El valor a verificar</param>
        /// <param name="min">Valor minimo permitido</param>
        /// <param name="max">Valor maximo permitido</param>
        /// <returns>true si el value esta entre el min y el max else false</returns>
        public static bool IsBetweenIE<T>(this T value, T min, T max) where T : IComparable
        {
            return (min.CompareTo(value) <= 0) && (value.CompareTo(max) < 0);
        }

        /// <summary>
        /// Verifica que el valor entregado esta entre los rangos min (Exclusive) y max (Exclusive) <![CDATA[min <= value <= max]]> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">El valor a verificar</param>
        /// <param name="min">Valor minimo permitido</param>
        /// <param name="max">Valor maximo permitido</param>
        /// <returns>true si el value esta entre el min y el max else false</returns>
        public static bool IsBetweenEE<T>(this T value, T min, T max) where T : IComparable
        {
            return (min.CompareTo(value) < 0) && (value.CompareTo(max) < 0);
        }
    }

    /// <summary>
    /// Extensión de clases para casting explícito
    /// </summary>
    public static class ObjectExtension
    {

        /// <summary>
        /// Verifica si el objeto es un decimal con separador punto o coma
        /// </summary>
        /// <param name="str">string de email ej:123@123.cl</param>
        /// <returns>true o false si el string corresponde a email o no</returns>
        public static bool _isDecimalWithSeparador(this string str)
        {          
            return System.Text.RegularExpressions.Regex.IsMatch(str._toString(), @"/^-?[0-9]+([,\.][0-9]*)?$/");
        }

        /// <summary>
        /// Verifica si el objeto corresponde a correo o no.
        /// </summary>
        /// <param name="str">string de email ej:123@123.cl</param>
        /// <returns>true o false si el string corresponde a email o no</returns>
        public static bool _isEmail(this string str)
        {
            /* Dirección E-Mail válida.
             "^[a-zA-Z0-9+&*-]+(?:\\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,7}$"
             "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$"
             */
            return System.Text.RegularExpressions.Regex.IsMatch(str._toString(), "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
        }

        /// <summary>
        /// Transforma cualquier objeto a un rut formateado y lo valida.
        /// </summary>
        /// <param name="str">RUT en cualquier formato (ej: 111111111, 11.111.1111, 11111111-1, 11.111.111-1)</param>
        /// <returns>Retorna el rut totalmente formateado, si falla retorna String.Empty</returns>
        public static rutObject _toRut(this object str)
        {
            /*
             * Ejemplo de uso:
             * 
             * rutObject rut = "111111111"._toRut();
             * if(rut.valido){
             *		rut.compuesto	// 11.111.111-1
             *		rut.rut			// 11111111
             *		rut.verificador	// 1
             *		rut.valido		// True
             * }
             * 
             * rutObject rut = "123457890"._toRut();
             * if(rut.valido){
             *		rut.compuesto	// String.Empty
             *		rut.rut			// 0
             *		rut.verificador	// String.Empty
             *		rut.valido		// False
             * }
            */

            string data = str._toString();

            /* Hay rut? */
            if (string.IsNullOrEmpty(data))
                return new rutObject();

            /* Limpia los valores permitidos unicamente */
            data = System.Text.RegularExpressions.Regex.Replace(data.ToLower(), @"[^0-9k]", "")._toString();

            /* Era código basura? */
            if (string.IsNullOrEmpty(data))
                return new rutObject();

            /* Dígito verificador a comparar */
            string v = data.Substring(data.Length - 1)._toString().ToUpper();

            /* Rut a comparar */
            string r = data.Substring(0, data.Length - 1)._toInt()._toString();

            //por si el rut viene con 00 por delante transforma parte numerica en decimal - DCS 20160412
            data = r + v;
            
            if (!((data.Length == 7) || (data.Length == 8) || (data.Length == 9)))
                return new rutObject(); /* La cantidad de carácteres no es válida. */

            /* No se puede obtener el valor del RUT */
            if (r.Equals("0"))
                return new rutObject();

            int x = 2;
            int s = 0;

            for (int i = (r.Length - 1); i >= 0; i--)
            {
                if (x > 7)
                    x = 2;
                s += (r[i]._toInt() * x);
                x++;
            }

            int dv = 11 - (s % 11);
            string dvr = String.Empty;
            if (dv == 10)
                dvr = "K";
            else if (dv == 11)
                dvr = "0";
            else
                dvr = dv._toString();

            // Válido
            if (dvr.ToUpper().Equals(v.ToUpper()))
                return new rutObject(r._toInt(), dvr, r._toInt().ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR")) + "-" + dvr);

            return new rutObject();
        }

        /// <summary>
        /// Verifica si el texto es realmente alfanbumérico. Si el texto es nulo retornará false. (regla: /^[a-zA-Z0-9\-_]*$/)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool _isSafeString(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            Regex r = new Regex("@[^A-Za-z0-9áÁéÉíÍóÓúÚñÑü\\-_, \\.]+@i");

            return r.IsMatch(str);
        }

        public static bool _isDateTime(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            return str._toDateTime() != new DateTime();

            /*
            Regex r = new Regex("^(0?[1-9]|1[0-9]|2|2[0-9]|3[0-1])/(0?[1-9]|1[0-2])/(d{2}|d{4})$");

            return r.IsMatch(str);*/
        }

        public static bool _isAlfaString(this string str)
        {
            Regex r = new Regex("@[^A-Za-z0-9áÁéÉíÍóÓúÚñÑü-_ .]+@i");

            return r.IsMatch(str);
        }

        /// <summary>
        /// Transforma cualquier objeto a texto, si el objeto es nulo entonces retorna string empty
        /// </summary>
        public static string _toString(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return String.Empty;

            return ob.ToString().Trim();
        }

        /// <summary>
        /// Transforma cualquier objeto a texto, si el objeto es nulo entonces retorna string empty
        /// </summary>
        public static bool _toBool(this object ob)
        {
            try
            {
                bool ret = false;

                if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                    ret = false;
                else if (ob.ToString().Trim().ToUpper().Equals("TRUE") || ob.ToString().Equals("1"))
                    ret = true;
                else
                    ret = (bool)ob;

                return ret;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Transforma un objeto de tipo datetime y retorna solo el date en formato de string.
        /// </summary>
        /// <param name="dt">Objeto DateTime</param>
        /// <returns>Retorna solo el date en formato de string</returns>
        public static string _toDateString(this DateTime dt)
        {
            if (dt == null)
                return String.Empty;

            return dt._toString().Split(' ')[0];
        }

        /// <summary>
        /// Retorna string en formato Time 
        /// Ejemplo: 12:30
        /// </summary>
        public static string _toTimeString(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return string.Empty;

            try
            {
                CultureInfo culture = new CultureInfo("es-CL");

                culture.DateTimeFormat.DateSeparator = "/";
                culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

                Thread.CurrentThread.CurrentCulture = culture;

                DateTime outDate = new DateTime();

                if (DateTime.TryParse(ob.ToString(), out outDate))
                    return outDate.ToString("HH:mm");
                else
                    return new DateTime().ToString("HH:mm");
            }
            catch
            {
                return new DateTime().ToString("HH:mm");
            }
        }

        /// <summary>
        /// Verifica si el dato es string sin caracteres no validos - DCS 20160414 
        /// Acepta letras, numeros, guion
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static bool _isStringWithOut(this object ob)
        {
            Regex r = new Regex(@"^[a-zA-Z0-9\-_]*$");
            return r.IsMatch(ob.ToString());
        }
            

        /// <summary>
        /// Retorna string en formato Time 
        /// Ejemplo: 12:30:00
        /// </summary>
        public static string _toTimeStringSS(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return string.Empty;

            try
            {
                CultureInfo culture = new CultureInfo("es-CL");

                culture.DateTimeFormat.DateSeparator = "/";
                culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

                Thread.CurrentThread.CurrentCulture = culture;

                DateTime outDate = new DateTime();

                if (DateTime.TryParse(ob.ToString(), out outDate))
                    return outDate.ToString("HH:mm:ss");
                else
                    return new DateTime().ToString("HH:mm:ss");
            }
            catch
            {
                return new DateTime().ToString("HH:mm:ss");
            }
        }

        /// <summary>
        /// Transforma cualquier objeto a DateTime. Si falla devuelve New Datetime().
        /// </summary>
        public static DateTime _toDateTime(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return new DateTime();

            try
            {
                CultureInfo culture = new CultureInfo("es-CL");

                culture.DateTimeFormat.DateSeparator = "/";
                culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

                Thread.CurrentThread.CurrentCulture = culture;

                DateTime outDate = new DateTime();
                if (DateTime.TryParse(ob.ToString(), out outDate))
                    return outDate;
                else
                    return new DateTime();
            }
            catch
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// Transforma cualquier objeto a numérico de tipo Int32. Si falla devuelve 0.
        /// Redondea decimales para transformar en int
        /// Ejemplos: 123=123 abc123=123 1a2b3c=123 Control=0 NULL=0 Empty=0
        /// </summary>
        public static int _toInt(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return 0;

            try
            {
                if ((ob.ToString()).Contains(","))
                    return (int)Math.Round(ob.ToString()._toDecimal(), MidpointRounding.ToEven);

                return Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(ob.ToString(), @"@[^0-9]+@i", ""));
            }
            catch
            {
                return 0;
            }
        }




        /// <summary>
        /// Transforma cualquier objeto a numérico de tipo long. Si falla devuelve 0.
        /// Redondea decimales para transformar en int
        /// Ejemplos: 123=123 abc123=123 1a2b3c=123 Control=0 NULL=0 Empty=0
        /// </summary>
        public static long _toLong(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return 0;

            try
            {
                if ((ob.ToString()).Contains(","))
                {
                    return (long)Math.Round(ob.ToString()._toDecimal(), MidpointRounding.ToEven);
                }

                return Convert.ToInt64(System.Text.RegularExpressions.Regex.Replace(ob.ToString(), @"@[^0-9]+@i", ""));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Transforma cualquier objeto a numérico de tipo Decimal. Si falla devuelve 0.
        /// Ejemplos: 123=123 abc123=123 1a2b3c=123 Control=0 NULL=0 Empty=0
        /// </summary>
        public static decimal _toDecimal(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return 0;

            try
            {
                string value = System.Text.RegularExpressions.Regex.Replace(ob.ToString(), @"@[^0-9]+@i", "");

                value = value.Replace("$", string.Empty).Trim();

                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Transforma cualquier objeto a un valor flotante double. Si falla retorna 0.
        /// Por defecto .Net utiliza comas para separar los decimales.
        /// Ejemplos: 12.3=12.3 ab.c123=0.123 1a2b.3c=12.3 Control=0 NULL=0 Empty=0
        /// </summary>
        public static double _toFloatDouble(this object ob, bool ImportarDesdeCulturaComa = true)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return 0;

            try
            {
                if (ImportarDesdeCulturaComa)
                    return Convert.ToDouble(
                        System.Text.RegularExpressions.Regex.Replace(ob.ToString(), @"@[^Ee0-9\.\,]+@i", ""),
                        CultureInfo.GetCultureInfo("de-DE").NumberFormat
                    );
                else
                    return Convert.ToDouble(
                        System.Text.RegularExpressions.Regex.Replace(ob.ToString(), @"@[^Ee0-9\.\,]+@i", ""),
                        CultureInfo.InvariantCulture.NumberFormat
                    );
            }
            catch
            {
                return 0;
            }
        }

       

    

        public static string _toSplit(this object ob, int lenght)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return "";

            string value = ob.ToString();

            string ret = string.Join("\n\b", value.ToCharArray().Select((c, i) => new { Char = c, Index = i }).GroupBy(o => o.Index / lenght).Select(g => new String(g.Select(o => o.Char).ToArray())).ToList());

            return ret;
        }

       

        /// <summary>
        /// Transforma cualquier texto en su equivalencia como string en un hash SHA1 (una via de 40 bytes)
        /// </summary>
        public static string _toSHA1(this object ob)
        {
            if (ob == null) /* SHA1 nulos también tienen un hash válido */
                ob = "";

            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();

            stream = sha1.ComputeHash(encoding.GetBytes(ob.ToString()));

            for (int i = 0; i < stream.Length; i++)
                sb.AppendFormat("{0:x2}", stream[i]);

            return sb.ToString();
        }

        /// <summary>
        /// Transforma cualquier texto en su equivalencia como string en un hash MD5 (una via de 32 bytes)
        /// </summary>
        public static string _toMD5(this object ob)
        {
            if (ob == null) /* MD5 nulos también tienen un hash válido */
                ob = "";

            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();

            stream = md5.ComputeHash(encoding.GetBytes(ob.ToString()));

            for (int i = 0; i < stream.Length; i++)
                sb.AppendFormat("{0:x2}", stream[i]);

            return sb.ToString();
        }

        /// <summary>
        /// Retorna solamente los carácteres alfanuméricos del objeto convertido a string mas \-_ (RegEx=/@[^A-Za-z0-9\\-_]+@i/)
        /// </summary>
        public static string _toSafeString(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return "";

            return System.Text.RegularExpressions.Regex.Replace(ob.ToString(), "@[^A-Za-z0-9áÁéÉíÍóÓúÚñÑü\\-_, \\.]+@i", "");
        }

        public static string _toAlfaString(this object ob)
        {
            if ((ob == null) || (String.IsNullOrEmpty(ob.ToString())))
                return "";

            return System.Text.RegularExpressions.Regex.Replace(ob.ToString(), "@[^A-Za-z0-9áÁéÉíÍóÓúÚñÑü-_ .]+@i", "");
        }

        public static byte[] _toByteArray(this string str)
        {
            var encoding = new System.Text.UTF8Encoding();

            return encoding.GetBytes(str);
        }

        //public static string _toHypelinkLiteral(this string label, string url, bool codificarLabelHtml = true)
        //{
        //	string append = HttpContext.Current.Request.Url.Scheme
        //				+ "://" + HttpContext.Current.Request.Url.Authority
        //				+ HttpContext.Current.Request.ApplicationPath.TrimEnd('/')
        //				+ "/front_end/" + ConfigurationManager.AppSettings["cliente"] + "vistas/";

        //	return "<a href='" + append + url + "'>" + (codificarLabelHtml ? label._toHtmlEncode() : label) + "</a>";
        //}

        public static string _toFormatType(this object label, string formato)
        {
            string ret = label._toString();

            try
            {
                switch (formato.Trim().ToUpper())
                {
                    case "TIME":
                        ret = ret._toTimeString().ToString();
                        break;

                    case "DATETIME":
                        ret = ret._toDateTime().ToString();
                        break;

                    case "DATE":
                        if (!string.IsNullOrEmpty(ret))
                            ret = ret._toDateTime().ToString().Split(' ')[0];
                        else
                            ret = string.Empty;

                        if (ret.Equals("01-01-1900") || ret.Equals("01/01/1900") || ret.Equals("01-01-0001") || ret.Equals("01/01/0001"))
                            ret = string.Empty;
                        break;

                    case "INT":
                        ret = ret._toInt().ToString();
                        break;

                    case "DOUBLE":
                        ret = ret._toFloatDouble().ToString();
                        break;

                    case "SAFE":
                        ret = ret._toFloatDouble().ToString();
                        break;

                    case "PESOS":
                        if (ret != "0")
                            ret = string.Format("$ {0:N0}", (decimal)(ret._toFloatDouble()));
                        else
                            ret = "$ 0";
                        break;

                    case "DECIMAL2":
                        ret = string.Format("{0:0.00}", ret._toDecimal());
                        break;
                }
            }
            catch
            {
                //Do nothing
            }

            return ret;
        }

        public static string _toFormatTypeEnvio(this object label, string formato)
        {
            string ret = label._toString();

            try
            {
                switch (formato.Trim().ToUpper())
                {
                    case "STRING_DELCOMAS":
                        ret = ret.Replace(",", "");
                        break;

                    case "TIME":
                        ret = ret._toDateTime().ToString("HHmmss");
                        break;

                    case "DATETIME":
                        ret = ret._toDateTime().ToString();
                        break;

                    case "DATE":
                        if (!string.IsNullOrEmpty(ret))
                            ret = ret._toDateTime().ToString("yyyyMMdd").Split(' ')[0];
                        else
                            ret = "00000000";

                        ret = ret.Equals("19000101") ? "00000000" : ret;
                        break;

                    case "INT":
                        ret = ret._toInt().ToString();
                        break;

                    case "DOUBLE":
                        ret = ret._toFloatDouble().ToString();
                        break;

                    case "SAFE":
                        ret = ret._toFloatDouble().ToString();
                        break;

                    case "PESOS":
                        ret = string.Format("$ {0:N0}", (decimal)(ret._toFloatDouble()));
                        //ret = String.Format("{0:C}", (decimal)(ret._toFloatDouble()));
                        break;

                    case "DATEANDTIME":
                        if (!string.IsNullOrEmpty(ret))
                            ret = ret._toDateTime().ToString("yyyyMMddHHmmss").Split(' ')[0];
                        else
                            ret = "00000000000000";
                        break;

                    case "ENTERO":
                        ret = ret.Split(',')[0];
                        break;

                    case "AAAAMMDD":
                        ret = ret._toDateTime().ToString("yyyyMMdd");

                        ret = ret.Equals("19000101") ? "00000000" : ret;
                        break;
                }
            }
            catch
            {
                //do nothing
            }

            return ret;
        }

        /// <summary>
        /// Convierte Lista a DataTable
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TSource>(this IList<TSource> data)
        {
            DataTable dataTable = new DataTable(typeof(TSource).Name);
            PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (TSource item in data)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        /// <summary>
        /// Convierte DataTable a Lista
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<TSource> ToList<TSource>(this DataTable dataTable) where TSource : new()
        {
            var dataList = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                                 select new
                                 {
                                     Name = aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
                             aProp.PropertyType
                                 }).ToList();

            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new
                                     {
                                         Name = aHeader.ColumnName,
                                         Type = aHeader.DataType
                                     }).ToList();
            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var aTSource = new TSource();
                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value) ? null : dataRow[aField.Name]; //if field is nullable
                    propertyInfos.SetValue(aTSource, value, null);
                }
                dataList.Add(aTSource);
            }
            return dataList;
        }

        public static TKey FindKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                if (value.Equals(pair.Value)) return pair.Key;

            throw new Exception("the value is not found in the dictionary");
        }
    }//end class
}