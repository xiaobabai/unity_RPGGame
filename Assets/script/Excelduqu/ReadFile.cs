using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadFile : MonoBehaviour
{
    Dictionary<string, List<string>> jsonData = new Dictionary<string, List<string>>();

    List<string> dadada = new List<string>();
    string fileSite;
    void Start()
    {
        ExcelData data = new ExcelData();
        fileSite= data.fileSite();

        string[] dirs = Directory.GetFileSystemEntries(fileSite, "*.json");//读取当前文件夹下后缀为json文件
        //DirectoryInfo di = new DirectoryInfo(filePath);
        //FileInfo[] afi = di.GetFiles("*.dat");
        print(dirs.Length);
        if (dirs.Length > 0 )
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                LoadJson(dirs[i]);
            }
        }
    }
    /// <summary>
    /// 读取保存数据的方法
    /// </summary>
    public void LoadJson(string jsonPath)
    {
        if (!File.Exists(jsonPath))
        {
            Debug.LogError("不存在此文件");
            return;
        }
        StreamReader streamReader = new StreamReader(jsonPath);
        string json = streamReader.ReadToEnd();
        streamReader.Close();
        JsonData data=JsonMapper.ToObject(json);
        foreach (var item in data)
        {
            JsonData json1 = item as JsonData;
            foreach (var it in json1)
            {
                print(it);
                dadada.Add(it.ToString());
            }
        }
        //ExcelDataClass asadas = data as 
        Debug.Log("读取成功");
    }
    void Tmd()
    {
        print("cp");
    }
    private void OnGUI()
    {
        if (GUILayout.Button("LoadJson"))
            Tmd();


        GUILayout.Label("LoadJson");

    }
}

[Serializable]
public class ExcelDataClass
{
    public string ID;
    public string[] content;
}
[Serializable]
public class ContentData
{
    public string name;
    public string attack;
    public string def;
    public string hitRate;
    public string dodugRate;
}

