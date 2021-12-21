using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSerialization
{
    private static string directoryPath = $"{Application.persistentDataPath}\\Saves";

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
