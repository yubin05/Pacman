using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetLoaderController : MonoBehaviour
{
    private static GoogleSheetLoaderController instance;
    public static GoogleSheetLoaderController Instance
    {
        get => instance != null ? instance : FindObjectOfType<GoogleSheetLoaderController>();
    }

    public void RefreshSheet(string url, string filePath) { StartCoroutine(RefreshSheetProcess(url, filePath)); }
    private IEnumerator RefreshSheetProcess(string url, string filePath)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.ConnectionError)
        {
            var downloadText = webRequest.downloadHandler.text;
            // Debug.Log(downloadText); // test code
            File.WriteAllText(filePath, downloadText, encoding:Encoding.UTF8);
        }
        else
        {
            Debug.LogError("Connection Error");
            yield break;
        }

        UnityEditor.AssetDatabase.Refresh();
    }
}
