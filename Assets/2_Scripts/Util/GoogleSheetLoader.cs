using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(fileName="", menuName="Create Google Sheet Loader", order=(int)CreateAssetMenuSequence.GoogleSheetLoader)]
public class GoogleSheetLoader : ScriptableObject
{
    public string docid;
    public string gid;
    public string fileName;

    private const string googleSheetPath = "https://docs.google.com/spreadsheets/d";
    public string openUrl
    {
        get
        {
            return $"{googleSheetPath}/{docid}/edit#gid={gid}";
        }
    }
    public string downloadUrl
    {
        get
        {
            return $"{googleSheetPath}/{docid}/export?format=csv&gid={gid}";
        }
    }
    public string filePath
    {
        get
        {
            // Application.dataPath: [UnityProjectPath]/Assets
            return $"{Application.dataPath}/Resources/TableList/{fileName}.csv";
        }
    }

    public void OpenSheet()
    {
        Application.OpenURL(openUrl);
    }
    public void RefreshSheet()
    {
        GoogleSheetLoaderController.Instance.RefreshSheet(downloadUrl, filePath);
    }
}
#endif