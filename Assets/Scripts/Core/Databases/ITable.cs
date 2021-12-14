using System.Collections.Generic;

public interface ITable<T>
{
    public string Id { get; }
    public List<T> Elements { get; set; }
}