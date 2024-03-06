using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllLevelCleared : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LevelManager.currLevel == LevelManager.levels.Length - 1)
        {
            Canvas obj = GameObject.Find("Canvas").GetComponent<Canvas>();
            TextMeshProUGUI textMesh = obj.GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = "Congratulations!\nAll levels cleared!";
            GameObject nextButton = obj.transform.Find("NextButton").gameObject;
            if (nextButton != null)
            {
                // »•µÙNextButton
                Destroy(nextButton);
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
