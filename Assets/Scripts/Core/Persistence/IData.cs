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
