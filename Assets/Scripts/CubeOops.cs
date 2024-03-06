using System.Collections;
using System.Collections.Generic;
using Unity.FPS;
using UnityEngine;

public class CubeOops : MonoBehaviour
{
    bool notified = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -100 && !notified)
        {
            NotificationHUDManager notification = FindObjectOfType<NotificationHUDManager>();
            notification.CreateNotification("When feeling stuck, consider restarting and shooting cautiously.");
            notified = true;
        }
    }
}
