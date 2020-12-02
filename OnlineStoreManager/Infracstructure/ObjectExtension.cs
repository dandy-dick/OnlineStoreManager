using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineStoreManager.Models;

namespace OnlineStoreManager.Infracstructure
{
    public static class ObjectExtension
    {
        /*
            Giống với Object.assign của Javascript
         */
        public static void ObjectAssign<TSource,TTarget>(this TTarget target, TSource src)
        {
            PropertyInfo[] properties = typeof(TTarget).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var key = property.Name;
                bool hasProperty = src.GetType().GetProperty(key) != null;
                if (hasProperty)
                {
                    var value = src.GetType()
                                    .GetProperty(key)
                                    .GetValue(src, null);
                    property.SetValue(target, value);
                }
            }
        }

        /*
         Chuyển đổi tiếng việt thành không dấu
         */
        public static string TiengVietKhongDau(this string str)
        {
            string[] VietNamChar = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
    }
}
