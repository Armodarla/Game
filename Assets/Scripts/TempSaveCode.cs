using UnityEngine;

public class TempSaveCode : MonoBehaviour
{
    public bool isNew = true;
    public GameObject continueBtn;
    void Start()
    {
        if (isNew)
        {
            continueBtn.SetActive(false);
        }
        else
        {
            continueBtn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
