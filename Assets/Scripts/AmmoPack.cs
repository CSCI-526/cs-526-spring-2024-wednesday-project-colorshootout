using System.Collections;
using System.Collections.Generic;
using Unity.FPS;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCharacterController>() == null)
            return;
        var player = other.GetComponent<PlayerCharacterController>();
        // test if the other collider contains a PlayerCharacterController, then complete
        if (player != null)
        {
            WeaponController w = FindObjectOfType<WeaponController>();
            w.m_CurrentAmmo = Mathf.Min(w.m_CurrentAmmo + (int)(w.MaxAmmo * 0.3f), w.MaxAmmo);
            NotificationHUDManager notification = FindObjectOfType<NotificationHUDManager>();
            notification.CreateNotification("Ammo Added!");

            Destroy(gameObject);
        }
    }
}
