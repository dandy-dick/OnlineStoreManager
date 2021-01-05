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
    public class ExcludeAttribute: Attribute
    {
        public string Name { get; set; }
        public ExcludeAttribute(string Name)
        {
            this.Name = Name;
        }
    }

    public static class ObjectExtension
    {
        /*
            Giống với Object.assign của Javascript
         */
        public static void TrySetProperty<TType>(string key, object val)
        {

        }

        public static object GetPropertyValue<TSource>(this TSource src, string key)
            where TSource : class, new()
        {
            var type = src.GetType();
            bool hasProperty = type.GetProperty(key) != null;
            if (hasProperty)
                return type.GetProperty(key).GetValue(src, null);
            return null;
        }

        public static void ObjectAssign<TSource, TTarget>(this TTarget target, TSource src) 
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

        public static void AssignProperties<TSource, TTarget>(this TTarget target, TSource src)
            where TSource: class,new()
            where TTarget: class,new()
        {
            PropertyInfo[] properties = typeof(TTarget).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var key = property.Name;
                bool hasProperty = src.GetType().GetProperty(key) != null;
                if (hasProperty)
                {
                    // check for exclude
                    var excluded = (string)src.PropAttrValue
                                                <TSource, ExcludeAttribute>(key, "Name");
                    if (excluded == "AssignProperties")  // bo qua 
                        continue;

                    var value = src.GetType()
                            .GetProperty(key)
                            .GetValue(src, null);
                    property.SetValue(target, value);
                }
            }
        }

/*
    Lấy giá trị key từ attribute của một property của object
    property hoặc attr không tồn tại thì throw
 */
public static object PropAttrValue<TSrc,TAttr>(this TSrc src, string propertyName, string attrName)
            where TAttr : class
            where TSrc : class
        {
            var srcType = typeof(TSrc);
            var attrType = typeof(TAttr);

            var prop = srcType.GetProperty(propertyName);
            if (prop != null)
            {
                var attr = prop.GetCustomAttribute(attrType);
                if (attr != null)
                {
                    try
                    {
                        var result = attrType.GetProperty(attrName).GetValue(attr, null);
                        return result;

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            
            return null;
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
            if (str != null)
            {
                for (int i = 1; i < VietNamChar.Length; i++)
                {
                    for (int j = 0; j < VietNamChar[i].Length; j++)
                        str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }

            return str;
        }
    }
}
