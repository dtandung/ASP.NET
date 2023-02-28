using _19T1021044.DomainModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace _19T1021044.Web
{
    public static class Converter
    {
        public static DateTime? DMYStringToDateTime(string s, string format = "dd/MM/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static UserAccount CookieToUserAccount(string cookie)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(cookie);
        }
    }
}