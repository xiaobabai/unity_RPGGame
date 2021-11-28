
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using LitJson;

public class ExcelData
{
    [Header("从第几行开始")]
    public int row = 4;
    [Header("从第几列开始")]
    public int column = 2;
    /// <summary>标签名字行数</summary>
    public int name = 1;

    List<Dictionary<string, string>> dictionaries = new List<Dictionary<string, string>>();  

    /// <summary> 读取excel文件位置 </summary>
    string fs= Directory.GetCurrentDirectory() + @"\Excel\Excel.xlsx";
    /// <summary> 保存目录 </summary>
    string jsonDir = Application.dataPath + @"\Archive";
    /// <summary>生成json加后缀可生成想要文件 </summary>
    string jsonPath= Application.dataPath + @"\Archive";


    //[UnityEditor.MenuItem("Tool/转Excel表为Json", false, 1)]
    static void excelInport()
    {
        ExcelData data = new ExcelData();
        data.EppColos();
    }
    /// <summary>
    /// 读取excel表
    /// </summary>
    /// <param name="file"></param>

    public void EppColos()
    {
        /// epplus插件是读取单元格里所有的数据，包括空字符;
        /// worksheet.Dimension.End.Row，从上到下读取有数据的行；
        /// worksheet.Dimension.End.Column ，从左到右.列数据


        //StreamReader sr = new StreamReader(fs);//获取当前unity的主文件目录'
        // Application.dataPath 获取当前unity的Assets目录

        FileStream file = new FileStream(fs, FileMode.Open, FileAccess.Read);//开启获取指定位置数据流

        //string tfr = Path.GetExtension(fs).ToLower();//读取文件后缀   
        //StreamReader sr = new StreamReader(file);//获取同目录下的131.xlsx文件
        using (ExcelPackage excelPackage = new ExcelPackage(file))//using的作用是读完{}里程序，释放资源，目前作用可以说是关闭excel表
        {
            if (excelPackage != null)
            {
                int tableNum = excelPackage.Workbook.Worksheets.Count;//记录这excel文件里有多少表

                for (int index = 1; index < tableNum + 1; index++)
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[index];//epp库开始索引是1，读取第一张表

                    int rowNum = worksheet.Dimension.End.Row;//获取当前不为空的单元格，行数
                    int columnNum = worksheet.Dimension.End.Column;//获取当前不为空的单元格，列数

                    //object a = worksheet.Cells[3, 2].Value;//获取表表索引位的数据
                    //string bt = excelPackage.Workbook.Worksheets[1].Name;//读取表名;        
                    for (int h = row; h < rowNum + 1; h++)
                    {
                        Dictionary<string, string> keys = new Dictionary<string, string>();
                        for (int l = column; l < columnNum + 1; l++)
                        {
                            string dataName = worksheet.Cells[name, l].Value.ToString();
                            string columnData = worksheet.Cells[h, l].Value.ToString();
                            string[] vs=columnData.Split('+');//分割
                            if (vs.Length > 1)
                            {
                                foreach(string da in vs)
                                {
                                    keys.Add(dataName, da);
                                }
                            }else
                                keys.Add(dataName, columnData);
                        }
                        dictionaries.Add(keys);
                    }

                    string json = JsonMapper.ToJson(dictionaries);//把数据转json格式;
                    string jsonPath = this.jsonPath + @"\" + worksheet.Name + ".json";//后缀指定为json文件
                    SaveJson(json, jsonPath);
                    dictionaries.Clear();

                    //writer.WriteArrayStart();//开始写入=[
                    //writer.WriteObjectStart();//开始写入={
                    //writer.WritePropertyName("name");
                    //writer.Write("大哥");               
                    //writer.WriteObjectEnd();
                    //writer.WriteArrayEnd();
                }
            }
        }
        file.Close();//关闭数据流
    }
    /// <summary>
    /// 保存数据转json格式
    /// </summary>
    /// <param name="excel"></param>
    public void SaveJson(string json, string jsonPath)
    {
        /// 1、在指定的位置创建目录
        /// Directory.CreateDirectory(string path);
        /// 3、判断在指定位置是否存在该目录
        /// Directory.Exists(string path);
        if (!Directory.Exists(jsonDir))
        {
            Directory.CreateDirectory(jsonDir);
        }

        if (!File.Exists(jsonPath))
        {
            //如果第一次创建并且写入文件 要Dispose
            File.Create(jsonPath).Dispose();
        }
        StreamWriter sw = new StreamWriter(jsonPath);
        sw.Write(json);
        sw.Close();
#if UNITY_EDITOR //打开本地沙盒路径
        Debug.Log("创建文件成功，路径：--->" + jsonDir+"保存json文件成功,路径:--->"+jsonPath);
        //System.Diagnostics.Process.Start(jsonDir);
#endif
    }
    public string fileSite()
    {
        return  this.jsonPath;
    }
    //public static void GenerateCode()
    //   {
    //       FileInfo info;
    //       FileStream stream;
    //       IExcelDataReader excelReader;
    //       DataSet result;
    //       string[] files = Directory.GetFiles(Application.dataPath + "/EasyUI/ExcelFiles", "*.xlsx", SearchOption.TopDirectoryOnly);

    //       string staticDataClassCode = "";

    //       try
    //       {
    //           int priority1 = 1;
    //           string code;
    //           foreach (string path in files)
    //           {
    //               info = new FileInfo(path);
    //               stream = info.Open(FileMode.Open, FileAccess.Read);
    //               excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
    //               result = excelReader.AsDataSet();
    //               int rowCount = result.Tables[0].Rows.Count;
    //               int colCount = result.Tables[0].Columns.Count;
    //               string className = result.Tables[1].Rows[2][0].ToString();

    //               staticDataClassCode += "     [ProtoMember(" + priority1++ + ")]\n";
    //               staticDataClassCode += "     public List<" + className + "> " + className + "List = new List<" + className + ">();\n";

    //               code = "";
    //               code += "using System.Collections;\n";
    //               code += "using System.Collections.Generic;\n";
    //               code += "using ProtoBuf;\n";
    //               code += "[ProtoContract]\n";
    //               code += "public class " + className + "\n";
    //               code += "{\n";
    //               int priority2 = 1;
    //               for (int col = 0; col < colCount; col++)
    //               {
    //                   code += "    [ProtoMember(" + priority2++ + ")]\n";
    //                   code += "    public " + result.Tables[1].Rows[1][col].ToString() + " " + result.Tables[1].Rows[0][col].ToString() + ";\n";
    //               }
    //               code += "    public " + className + "()\n";
    //               code += "    {}\n";
    //               code += "}\n";
    //               WriteClass(Application.dataPath + "/Script/Datas/" + className + ".cs", className, code);

    //               excelReader.Close();
    //               stream.Close();
    //           }
    //           code = "";
    //           code += "using System.Collections;\n";
    //           code += "using System.Collections.Generic;\n";
    //           code += "using ProtoBuf;\n";
    //           code += "[ProtoContract]\n";
    //           code += "public class StaticData\n";
    //           code += "{\n";
    //           code += staticDataClassCode;
    //           code += "    public StaticData(){}\n";
    //           code += "}\n";
    //           WriteClass(Application.dataPath + "/Script/Datas/StaticData.cs", "StaticData", code);
    //       }
    //       catch (IndexOutOfRangeException exp)
    //       {
    //           Debug.LogError(exp.StackTrace);
    //       }
    //       AssetDatabase.Refresh();
    //       AssetDatabase.SaveAssets();
    //   }

}