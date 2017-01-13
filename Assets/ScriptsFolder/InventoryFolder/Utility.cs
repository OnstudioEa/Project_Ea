using UnityEngine;
using System.Collections;
using System.Reflection;

public class StringValue : System.Attribute
{
    private string _value;

    public StringValue(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }
}

public class Utility : MonoBehaviour {

    public static string GetEnumString<EnumType>(EnumType enumValue)
    {
        System.Type type = enumValue.GetType();
        FieldInfo fi = type.GetField(enumValue.ToString());
        StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
        if (attrs.Length > 0)
        {
            return attrs[0].Value;
        }
        return string.Empty;
    }
    
    public static EnumType FindEnumString<EnumType>(string value, EnumType defaultValue)
    {
        foreach (EnumType item in System.Enum.GetValues(typeof(EnumType)))
        {
            if (GetEnumString<EnumType>(item) == value)
            {
                return item;
            }
        }

        return defaultValue;
    }
}
