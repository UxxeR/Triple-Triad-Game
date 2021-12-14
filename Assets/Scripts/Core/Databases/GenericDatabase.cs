using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericDatabase<T> : MonoBehaviour where T : ITableElement
{
    public static GenericDatabase<T> Instance { get; set; }
    protected ITable<T> table;

    /// <summary>
    /// Get the element in a database by their id.
    /// </summary>
    /// <param name="elementId">Id of the element.</param>
    /// <returns>Return the element.</returns>
    public T FindElementById(string elementId)
    {
        return table.Elements.FirstOrDefault((element) => element.Id.Equals(elementId));
    }

    /// <summary>
    /// Get a random element un a database.
    /// </summary>
    /// <returns>Return the element.</returns>
    public T GetRandomElement()
    {
        return table.Elements.ElementAtOrDefault(Random.Range(0, table.Elements.Count));
    }
}
