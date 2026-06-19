using System;
using System.Globalization;

namespace EShop.Application.Utils
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar persianCalender = new PersianCalendar();
            return persianCalender.GetYear(value)+ "/"+
                persianCalender.GetMonth(value).ToString("00") +"/"+
                persianCalender.GetDayOfMonth(value).ToString("00");
        }
        public static string ToShamsi(this DateTime? value)
        {
            PersianCalendar persianCalender = new PersianCalendar();
            return persianCalender.GetYear((DateTime)value) + "/" +
                persianCalender.GetMonth((DateTime)value).ToString("00") + "/" +
                persianCalender.GetDayOfMonth((DateTime)value).ToString("00");
        }

    }
}
