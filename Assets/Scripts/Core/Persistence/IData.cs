using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IData<T>
{
    public T GetData();
    public void SetData();
}
