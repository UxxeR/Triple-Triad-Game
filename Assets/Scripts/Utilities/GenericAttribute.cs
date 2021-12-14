using System;
using System.Linq;

public class GenericAttribute
{
    /// <summary>
    /// Gets a custom attribute from an enumerator.
    /// </summary>
    /// <param name="enumValue">The enumerator value.</param>
    /// <typeparam name="T">The custom attribute class.</typeparam>
    /// <returns>Returns the custom attribute.</returns>
    public static T GetAttribute<T>(Enum enumValue) where T : Attribute
    {
        var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
        return (T)member?.GetCustomAttributes(typeof(T), false).FirstOrDefault();
    }
}
