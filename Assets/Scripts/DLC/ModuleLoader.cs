using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ModuleLoader : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;
    [SerializeField]
    private TextMeshProUGUI loadingText;
    [SerializeField]
    private GameObject _prefab;
    private void Start()
    {
        const string url = "https://api.vr.chipp.wtf/module";
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
            //disable scene camera
            sceneCamera.enabled = false;
            sceneCamera.gameObject.SetActive(false);
            loadingText.gameObject.SetActive(false);
        }
    }

}
