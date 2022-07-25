using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSerialization
{
    private static string directoryPath = $"{Application.persistentDataPath}\\Saves";

    /// <summary>
    /// Save the current data in a new file or override and existing file.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="data">The data that will be saved.</param>
    public static void Save(string fileName, object data)
    {
        try
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = $"{directoryPath}\\{fileName}.DAT";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            object serializedData = data;
            binaryFormatter.Serialize(fileStream, serializedData);
            fileStream.Close();
        }
        catch (Exception exception)
        {
            Debug.LogError($"{fileName}: {exception}");
            throw;
        }
    }

    /// <summary>
    /// Load an existing file data.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>The data of the file.</returns>
    public static object Load(string fileName)
    {
        try
        {
            object result;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string filePath = $"{directoryPath}\\{fileName}.DAT";

            if (File.Exists(filePath))
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                object data = binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                result = data;
            }
            else
            {
                Debug.Log("El archivo no existe.");
                result = new object();
            }

            return result;
        }
        catch (Exception exception)
        {
            Debug.LogError($"{fileName}: {exception}");
            throw;
        }
    }
}
