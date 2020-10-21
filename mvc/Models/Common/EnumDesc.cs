using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace mvc.Models.Common
{
    public static class EnumDesc
    {
        public static string GetDescription(this Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }
        public static List<IdName> GetDictEnum<T>() where T : Enum
        {
            var lstEnum = (T[])Enum.GetValues(typeof(T));
            List<IdName> idNames = new List<IdName>();

            foreach(var en in lstEnum){
                var name = en.GetDescription();
                var key =  Convert.ToInt32(en);
                idNames.Add(new IdName{Id = key, Name = name});
            }
            return idNames;
        }
    }

    public class IdName{
        public int Id {get;set;}
        public string Name {get;set;}
        
    }
}