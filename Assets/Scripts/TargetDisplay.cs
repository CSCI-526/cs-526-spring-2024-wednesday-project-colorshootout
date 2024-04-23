using System.Collections;
using System.Collections.Generic;
using Unity.FPS;
using UnityEngine;

public class TargetDisplay : MonoBehaviour
{
    [Tooltip("Target prefab")] public GameObject TargetPrefab;
    private GameObject currentTarget;
    private string lastFloor;
    // Start is called before the first frame update
    void Start()
    {
        //currentTarget = GameObject.FindGameObjectWithTag("CurrTarget");
        //myScript = currentTarget.GetComponent<ObjectTutoring>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTarget = GameObject.FindGameObjectWithTag("CurrTarget");
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject == player && this.name != lastFloor)
        {
            // ��ҽ���floor������
            // Debug.Log("Player is on the floor " + this.name);
            // ɾ����һ��floor������currentTarget
            if (currentTarget != null)
            {
                var myScript = currentTarget.GetComponent<ObjectTutoring>();
                if (myScript != null)
                {
                    myScript.SetCompleted();
                }
                Destroy(currentTarget);
                currentTarget = null;
            }
            // ��ʾ�������µ�Ŀ��
            ShowCurrentTarget(this.name);
            lastFloor = this.name;
        }

        void ShowCurrentTarget(string floor)
        {
            switch (floor)
            {
                case "0.1":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Go to next platform.";
                            myScript.Description = "Try WASD / SPACE, and move MOUSE to look around.";
                        }
                    }
                    break;
                case "0.2":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Go to next platform.";
                            myScript.Description = "Hold W and SHIFT to speed up.";
                        }
                    }
                    break;
                case "0.3":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Hold W, SHIFT and Space to perform a long jump.";
                            myScript.Description = "Hold the key simultaneously until you land.";
                        }

                    }
                    break;
                case "0.4":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Reach the orange target point to finish.";
                            myScript.Description = "";
                        }

                    }
                    break;
                case "1.1":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot left or right side of the green cube.";
                            myScript.Description = "Make it wider. Then walking through.";
                        }
                    }
                    break;
                case "1.2":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot different sides of green cube.";
                            myScript.Description = "Form a bridge for jumping or walking.";
                        }
                    }
                    break;
                case "2.0":
                    // Debug.Log("Current target: Pick up the weapon package, shoot different color cube, jump over");
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot one side of non-green cubes";
                            myScript.Description = "Make it smaller.";
                        }

                    }
                    break;
                case "2.1":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot green cubes and non-green cubes.";
                            myScript.Description = "Make possible to go ahead!";
                        }

                    }
                    break;
                
                default:
                    // Debug.Log("No target for this floor");
                    break;
            }
        }
    }

}
