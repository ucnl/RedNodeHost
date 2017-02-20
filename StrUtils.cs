using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace RedLib.Utils
{
    public static class StrUtils
    {
        #region Methods

        public static string TrimStrToStComma(string source, int maxLen, int lenToTrim)
        {
            if (lenToTrim > maxLen)
                throw new ArgumentException("lenToTrim must be less or equal to maxLen");

            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException("source");

            if (source.Length > maxLen)
            {
                int commaIdx = source.IndexOf(',');

                if (commaIdx > lenToTrim)
                    return source.Substring(0, lenToTrim);
                else if (commaIdx < 0)
                    return source.Substring(0, lenToTrim);
                else
                    return source.Substring(0, commaIdx);
            }
            else
                return source;
        }     

        public static string GetFileNameInOwnPath(string ownPath, string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(ownPath), fileName);
        }

        public static string GetExecutableFileNameWithNewExt(string executableFileName, string newExt)
        {
            return Path.ChangeExtension(executableFileName, newExt);
        }

        public static string CreateDirectoryInOwnPath(string ownPath, string directoryName)
        {
            return Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(ownPath), directoryName)).FullName;
        }

        public static string GetTimeFileName(DateTime timeFix)
        {
            return string.Format("{0:00}-{1:00}-{2:00}",
                timeFix.Hour,
                timeFix.Minute,
                timeFix.Second);
        }

        public static string AngleToString(double angle)
        {
            int degree = (int)Math.Floor(angle);
            int minutes = (int)Math.Floor((angle - degree) * 60.0);
            double seconds = (angle - degree) * 3600 - minutes * 60.0;

            return string.Format(CultureInfo.InvariantCulture, "{0}°{1}\'{2:F04}\"", degree, minutes, seconds);
        }        

        public static string Ex2Str(Exception ex)
        {
            return string.Format("{0} {1}", ex.Message, ex.StackTrace);
        }

        public static string Bytes2HexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
                sb.Append(bytes[i].ToString("X2"));

            return sb.ToString();
        }


        #endregion
    }
}
