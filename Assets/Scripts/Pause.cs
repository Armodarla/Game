using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PausePanel;
    public CharacterCodes player;
    public bool isPaused = false;
    private float originalTimeScale;
    public SceneCode SceneLoader;

    void Awake()
    {
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape")){
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if(isPaused){
            Cursor.lockState = CursorLockMode.None;
            originalTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            player.isControllable = false;
        }else{
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = originalTimeScale;
            PausePanel.SetActive(false);
            player.isControllable = true;
        } 
    }

    public void GoTo(int SceneID){
        Time.timeScale = 1f; 
        SceneLoader.AsyncChangeScene(SceneID);
    }
}
 