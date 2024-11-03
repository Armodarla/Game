using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCode : MonoBehaviour
{
    public void AsyncChangeScene(int sceneID)
    {
        StartCoroutine(LoadSceneID(sceneID));
    }

    public void OpenClosePanel(GameObject panel)
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

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
