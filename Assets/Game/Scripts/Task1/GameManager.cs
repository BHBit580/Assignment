using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    
    public static ClientDataContainer clientDataContainer;
    [SerializeField] private GameObject filterOptions;
    [SerializeField] private GameObject fixedasset;
    [SerializeField] private GameObject loading;
    private void Start()
    {
        filterOptions.SetActive(false);
        loading.SetActive(true);
        fixedasset.SetActive(false);
        StartCoroutine(GetRequest("https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log(string.Format("Something Went wrong {0}" , webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    clientDataContainer = JsonConvert.DeserializeObject<ClientDataContainer>(webRequest.downloadHandler.text);
                    filterOptions.SetActive(true);
                    fixedasset.SetActive(true);
                    loading.SetActive(false);
                    break;
            }
        }
    }
}