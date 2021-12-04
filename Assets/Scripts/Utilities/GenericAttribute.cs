using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericAttribute
{
    public static T GetAttribute<T>(Enum enumValue) where T : Attribute
    {
        var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
        return (T)member?.GetCustomAttributes(typeof(T), false).FirstOrDefault();
    }
}
