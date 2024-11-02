using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCode : MonoBehaviour
{
    public GameObject panel;

    public void AsyncChangeScene(int sceneID)
    {
        StartCoroutine(LoadSceneID(sceneID));
    }

    public void OpenClosePanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
            
    }


    private IEnumerator LoadSceneID(int sceneID)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
