using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Sprites;
using Debug = UnityEngine.Debug;


/// <summary>
/// https://velog.io/@eqeq109/%EA%B5%AC%EA%B8%80-%EC%8A%A4%ED%94%84%EB%A0%88%EB%93%9C-%EC%8B%9C%ED%8A%B8-API%EB%A5%BC-%EC%9D%B4%EC%9A%A9%ED%95%B4-%EC%9C%A0%EB%8B%88%ED%8B%B0-%EB%8D%B0%EC%9D%B4%ED%84%B0-%ED%85%8C%EC%9D%B4%EB%B8%94-%EA%B4%80%EB%A6%AC-%EB%A7%A4%EB%8B%88%EC%A0%80-%EB%A7%8C%EB%93%A4%EA%B8%B0-2-%EA%B5%AC%ED%98%84%ED%8E%B8
/// 이걸 보고 진행
/// </summary>

public static class TablePatch
{
    private const string ResourcesPath = "Tables";
    
    private const string ClientId = "866924294334-je66mpa04e5qakub033atq36dsraobq9.apps.googleusercontent.com";
    private const string ClientSecret = "87Cb_mLZ5Nbuzy95XlNgwSKn";
    private const string ClientName = "BaseProject";
    private const string ApplicationName = "BaseProject";
    
    private static readonly List<string> TableKeys = new List<string>
    {
        "1hw5fnPrGQEly67CXiuAGVpCbOB8mU2pNaoWZiw2WR98", // Data
    };
    
    [MenuItem("Util/Table/Patch")]
    public static void PatchTable()
    {
        CreateSheetSet();
    }

    private static void CreateSheetSet()
    {
        var pass = new ClientSecrets
        {
            ClientId = ClientId, 
            ClientSecret = ClientSecret
        };

        var scopes = new string[] {SheetsService.Scope.SpreadsheetsReadonly};
        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(pass, scopes, ClientName, CancellationToken.None)
            .Result;
        
        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName
        });

        foreach (var t in TableKeys)
        {
            var request = service.Spreadsheets.Get(t).Execute();

            foreach (var s in request.Sheets)
            {
                //service, tableKey, s.Properties.Title
                SendRequest(service, t,s.Properties.Title);
            }
        }
    }

    private static void SendRequest(SheetsService service,string tableKey, string sheetName)
    {
        bool success = true;
        try
        {
            var request = service.Spreadsheets.Values.Get(tableKey, sheetName);
            //  API 호출로 받아온 IList 데이터
            var jsonObject = request.Execute().Values;
            //  IList 데이터를 jsonConvert 하기위해 직렬화
            string jsonString = ParseSheetData(sheetName, jsonObject);

            SaveDataToFile(sheetName, jsonString);
        }
        catch (Exception e)
        {
            Debug.LogError($"Json 로드 실패 : {e.Message}");
            return;
        }
        
        Debug.Log($"Json Save Success : {sheetName}");
    }

    private static DataTable SpreadSheetToDataTable(string json)
    {
        DataTable data = JsonConvert.DeserializeObject<DataTable>(json);
        return data;
    }

    private static string ParseSheetData(string sheetName,IList<IList<object>> value)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append($"{{\"{sheetName}\":[");
        
        if (value.Count > 0)
        {
            List<string> headers = value[0].Select(x => x.ToString()).ToList();    //  헤더
            List<bool> canRows = new List<bool>();                                        //  값 사용 유무
            foreach (var t in headers)
            {
                //  공백이거나 앞에 #이붙으면 해당 셀 무시
                canRows.Add(string.IsNullOrEmpty(t)== false && t.StartsWith("#") == false);
            }
            
            //  헤더를 제외한 1번 행부터 진행
            for (int row = 1; row < value.Count; row++)
            {
                var columns = value[row];
                
                jsonBuilder.Append("{");
                for (int col = 0; col < columns.Count; col++)
                {
                    if(canRows[col] == false || columns[col].Equals(string.Empty))
                        continue;
                    
                    jsonBuilder.Append("\"" + headers[col] + "\"" + ":");
                    jsonBuilder.Append("\"" + columns[col] + "\"");
                    jsonBuilder.Append(",");
                }

                jsonBuilder.Append("}");
                jsonBuilder.Append(",");
            }
        }

        jsonBuilder.Append("]}");
        return jsonBuilder.ToString();
    }

    //  Json 문자열을 파일로 저장
    private static void SaveDataToFile(string sheetName, string newJson)
    {
        var rootPath = Path.Combine(Application.dataPath, "Resources", ResourcesPath);
        Directory.CreateDirectory(rootPath);
        
        //  json파일 저장
        string filePath = Path.Combine(rootPath, $"{sheetName}.json");
        File.WriteAllText(filePath,newJson);
    }
}
