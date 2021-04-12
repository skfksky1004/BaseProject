using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class TableUtility
{
    public static DataTable GetDataTable(string fileName, string tableName)
    {
        var obj = Resources.Load(fileName);
        var value = (obj as TextAsset).ToString();
        DataTable data = JsonConvert.DeserializeObject<DataTable>(value);
        data.TableName = tableName;

        return data;
    }

    public static DataTable GetDataTable(FileInfo info)
    {
        string fileName = Path.GetFileNameWithoutExtension(info.Name);
        string path = string.Concat("JsonData/", fileName);
        string value = string.Empty;
        try
        {
            value = (Resources.Load(path) as TextAsset).ToString();
        }
        catch (Exception e)
        {
            
        }

        DataTable data = JsonConvert.DeserializeObject<DataTable>(value);
        data.TableName = fileName;

        return data;
    }




    public static void SetObjectFile<T>(string key, T data)
    {
    }
}
