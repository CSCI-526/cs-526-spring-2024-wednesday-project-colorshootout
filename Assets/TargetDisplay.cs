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
            // 玩家进入floor的区域
            // Debug.Log("Player is on the floor " + this.name);
            // 删除上一个floor产生的currentTarget
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
            // 显示并创建新的目标
            ShowCurrentTarget(this.name);
            lastFloor = this.name;
        }

        void ShowCurrentTarget(string floor)
        {
            switch (floor)
            {
                case "1":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Explore, move, sprint, and jump. Follow the ground arrow.";
                            myScript.Description = "Use W A S D for movement, SPACE for jumping, SHIFT for running, and your mouse to look around.";
                        }
                    }
                    break;
                case "2":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot left or right face of green cube to make it wider. Then walking through.";
                            myScript.Description = "Use your mouse to aim (Right Click) and fire (Left Click).";
                        }
                    }
                    break;
                case "3":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Approach and collect the AmmoPack. Follow the arrow to next platform";
                            myScript.Description = "The green transparent sphere!";
                        }
                    }
                    break;
                case "4":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot the front of green cube to form a bridge for walking or jumping.";
                            myScript.Description = "Experiment by shooting different faces of the cube.";
                        }

                    }
                    break;
                case "5":
                    // Debug.Log("Current target: Pick up the weapon package, shoot different color cube, jump over");
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot at the faces of non-green cubes.";
                            myScript.Description = "Let's see what happen.";
                        }

                    }
                    break;
                case "5.1":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Shoot some thing to make it possible to go ahead!";
                            myScript.Description = "Think about the hit interaction of green cube and non-green cube.";
                        }

                    }
                    break;
                case "6":
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Master the art of long jumps.";
                            myScript.Description = "Hold W, SHIFT, and Space simultaneously to perform a long jump. Hold the key until you are landing.";
                        }

                    }
                    break;
                case "7":
                    // Debug.Log("Current target: Touch the target, complete the task");
                    if (currentTarget == null)
                    {
                        currentTarget = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
                        var myScript = currentTarget.GetComponent<ObjectTutoring>();
                        if (myScript != null)
                        {
                            myScript.Title = "Reach and touch the orange target point to complete the tutorial.";
                            myScript.Description = "Best of luck and enjoy the game!";
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
