using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

#if UNITY_EDITOR
[CustomEditor(typeof(RequestHandler))]
public class HttpRequestManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RequestHandler manager = (RequestHandler)target;

        GUILayout.Space(10);

        if (GUILayout.Button("GET"))
        {
            manager.CallCoroutine("GetRequest");
        }
    }
}

[ExecuteInEditMode]
#endif
public class RequestHandler : MonoBehaviour
{
    [SerializeField] private string url = "";

    IEnumerator GetRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Successfully received a response.
                string response = webRequest.downloadHandler.text;

                Pokemon _pokemon = JsonUtility.FromJson<Pokemon>(response);
                _pokemon.GetData(response);
                _pokemon.DisplayBaseInfo();
                _pokemon.DisplayTypes();
                _pokemon.DisplayStats();
            }
        }
    }

    public void CallCoroutine(string method)
    {
        StartCoroutine(method);
    }
}
