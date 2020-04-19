using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ModuleLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    private void Start()
    {
        const string url = "http://192.168.1.10:3001/module";
        // hardcoded module name
        StartCoroutine(PostModule(url, "roomwsp"));
    }

    IEnumerator PostModule(string url, string moduleName)
    {
        WWWForm form = new WWWForm();
        form.AddField("module", moduleName);

        var uwr = UnityWebRequest.Post(url, form);
        DownloadHandlerAssetBundle handler = new DownloadHandlerAssetBundle(uwr.url, 0u);
        uwr.downloadHandler = handler;
        Debug.Log("uno");

        yield return uwr.SendWebRequest();

        Debug.Log("duo");

        AssetBundle bundle = handler.assetBundle;

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log($"Error while loading module: {uwr.error}");
        } else
        {
            GameObject module = (GameObject)bundle.LoadAsset("room2");
            Instantiate(module);

            Transform spawnPoints = module.transform.Find("Spawn");

            Debug.Log(module.gameObject.name);

            // instantiate player over network
            Debug.Log(((PhotonNetwork.LocalPlayer.ActorNumber - 1) % 4) + 1);
            Debug.Log("reeeeeeeee" + spawnPoints.position);
            MasterManager.NetworkInstantiate(_prefab, spawnPoints.Find($"S{((PhotonNetwork.LocalPlayer.ActorNumber - 1) % 4) + 1}").position, Quaternion.identity);
            //disable main camera
            Camera.main.gameObject.SetActive(false);
        }
    }

}
