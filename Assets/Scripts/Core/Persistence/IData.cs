using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IData<T>
{
    /// <summary>
    /// Get the current data.
    /// </summary>
    /// <returns>Returns the current data.</returns>
    public T GetData();
    
    /// <summary>
    /// Set the current data.
    /// </summary>
    public void SetData();
}
