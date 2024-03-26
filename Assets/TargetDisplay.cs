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
            Debug.Log("Player is on the floor " + this.name);
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
                case "1":
                    Debug.Log("Current target: Move, jump, accelerate with SHIFT, follow the arrow instructions");
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Learn how to move, run, and jump."; // �����µ�
                            myScript.Description = "Use the W/A/S/D keys to move, SPACE to jump, and SHIFT to run.";
                        }
                    }
                    break;
                case "2":
                    Debug.Log("Current target: Shoot the same color cube, walk over");
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot the same color cube, then walk over."; // �����µ�
                            myScript.Description = "Use the W/A/S/D keys to move, SPACE to jump, and SHIFT to run.";
                        }

                    }

                    break;
                case "3":
                    Debug.Log("Current target: Pick up the weapon package, shoot different color cube, jump over");
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Current target: Pick up the weapon package, shoot different color cube, jump over."; // �����µ�
                            myScript.Description = "Use the W/A/S/D keys to move, SPACE to jump, and SHIFT to run.";
                        }

                    }
                    break;
                case "4":
                    Debug.Log("Current target: shift + jump, jump further");
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Current target: shift + jump, jump further."; // �����µ�
                            myScript.Description = "Use the W/A/S/D keys to move, SPACE to jump, and SHIFT to run.";
                        }

                    }
                    break;
                case "5":
                    Debug.Log("Current target: Touch the target, complete the task");
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Current target: Touch the target, complete the task."; // �����µ�
                            myScript.Description = "Use the W/A/S/D keys to move, SPACE to jump, and SHIFT to run.";
                        }

                    }
                    break;
                default:
                    Debug.Log("No target for this floor");
                    break;
            }
        }
    }

}
