using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Finish : MonoBehaviour
{
    Collider thisCollider;
    public TextMeshProUGUI panelText;
    public SceneCode SceneLoader;
    public int SceneID;
    void Awake()
    {
        thisCollider = GetComponent<Collider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            panelText.text = "Tutorial Finished!";
            StartCoroutine(Wait(3));
            SceneLoader.AsyncChangeScene(SceneID);
        }
    }
    IEnumerator Wait(int sec){
        yield return new WaitForSeconds(sec);
    }
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject.tag == "Player")
            thisCollider.isTrigger = false;
    }
}
