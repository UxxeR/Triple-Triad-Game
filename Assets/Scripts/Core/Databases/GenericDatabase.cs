using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericDatabase<T> : MonoBehaviour where T : ITableElement
{
    public static GenericDatabase<T> Instance { get; set; }
    protected ITable<T> table;

    public T FindElementById(string elementId)
    {
        return table.Elements.FirstOrDefault((element) => element.Id.Equals(elementId));
    }

    public T GetRandomElement()
    {
        return table.Elements.ElementAtOrDefault(Random.Range(0, table.Elements.Count));
    }
}
