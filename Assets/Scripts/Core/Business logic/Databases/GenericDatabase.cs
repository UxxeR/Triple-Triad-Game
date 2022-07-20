using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericDatabase<T> : MonoBehaviour where T : ITableElement
{
    public static GenericDatabase<T> Instance { get; set; }
    [field: SerializeField] public ITable<T> Table { get; set; }

    /// <summary>
    /// Get an element from database by their id.
    /// </summary>
    /// <param name="elementId">Id of the element.</param>
    /// <returns>Return the element.</returns>
    public T FindElementById(string elementId)
    {
        return Table.Elements.FirstOrDefault((element) => element.Id.Equals(elementId));
    }

    /// <summary>
    /// Get a random element from database.
    /// </summary>
    /// <returns>Return the element.</returns>
    public T GetRandomElement()
    {
        return Table.Elements.ElementAtOrDefault(Random.Range(0, Table.Elements.Count));
    }

    /// <summary>
    /// Get every element from database.
    /// </summary>
    /// <returns>Return the element.</returns>
    public List<T> GetAllElement()
    {
        return Table.Elements;
    }
}
