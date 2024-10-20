using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject WorldDialolgBox;
    public GameObject UIDialogBox;
    public TMP_Text talkText;
    public GameObject greenUI;
    public GameObject bulletUI;
    public GameObject talkUI;
    float timerDisplay;
    public TMP_Text talk;
    public TMP_Text talkpanel;

    // Start is called before the first frame update
    void Start()
    {
        UIDialogBox.SetActive(false);
        WorldDialolgBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;

            if (timerDisplay < 0)
            {
                WorldDialolgBox.SetActive(false);
                UIDialogBox.SetActive(false);
                UIONOff(true);
            }
        }

    
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
#if (!UNITY_ANDIORD)

        WorldDialolgBox.SetActive(true);
#else
        UIDialogBox.SetActive(true);
        UIONOff(false);
#endif

    }
        //dialogBox.SetActive(false);

    public void UIONOff(bool value)
    {
        greenUI.SetActive(value);
        bulletUI.SetActive(value);
        talkUI.SetActive(value);
    }

    public void ChageDisplayDialog()
    {
        talk.text = $"wow! good!";
        talkpanel.text = $"wow! good!";

    }

}

