using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Utils
{
    public static class PersianDate
    {
        public static string ToShamsiDate(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");
        }
        public static DateTime ToShamsiDateTime(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();

            return new DateTime(pc.GetYear(value), pc.GetMonth(value), pc.GetDayOfMonth(value), 0, 0, 0);
        }
        public static DateTime ToMiladi(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, new PersianCalendar());
        }     
        public static DateTime GetDateNow()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, new PersianCalendar());
        }
        public static string GetHourAndMinutesFormat(this DateTime time)
        {
            return time.ToString("HH:mm");
        }
        public static string ToStringShamsiDate(this DateTime dt)
        {
            System.Globalization.PersianCalendar PC = new PersianCalendar();
            int intYear = PC.GetYear(dt);
            int intMonth = PC.GetMonth(dt);
            int intDayOfMonth = PC.GetDayOfMonth(dt);
            DayOfWeek intDayOfWeek = PC.GetDayOfWeek(dt);
            string srtMonthName = "";
            string srtDayName = "";
        }




    }
}
