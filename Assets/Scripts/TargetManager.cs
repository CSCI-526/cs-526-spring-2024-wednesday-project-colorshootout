using System.Collections;
using System.Collections.Generic;
using Unity.FPS;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    Dictionary<string, GameObject> floorMap = new Dictionary<string, GameObject>();
    PlayerCharacterController m_Controller;
    string NextFloor;
    bool isTargetCreated = false;
    [Tooltip("Target prefab")] public GameObject TargetPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("InteractiveFloor");
        foreach (GameObject cube in cubes)
        {
            floorMap[cube.name] = cube;    // name : 1, 2, 3, 4, 5
        }
        m_Controller = FindObjectOfType<PlayerCharacterController>();
        NextFloor = "2";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = m_Controller.transform.position;

        // TODO Determine whether playerPos is within the range of floorMap[NextFloor], judge x, y, z
        // If so, the current target is completed, delete the current target, and initialize the next target
        GameObject CurrTarget = GameObject.FindGameObjectWithTag("CurrTarget");
        if (CurrTarget) // Set the current target to complete
        {
            ObjectTutoring myScript = CurrTarget.GetComponent<ObjectTutoring>();
            if (myScript != null)
            {
                myScript.SetCompleted();
            }
        }
        Destroy(CurrTarget); // Delete the current target
        NextFloor = (int.Parse(NextFloor) + 1).ToString();  // floor = next
        // Initialize a new target prefab
        // When NextFloor = "7": Do nothing
        if (TargetPrefab && !isTargetCreated)
        {
            var targetInstance = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
            var myScript = targetInstance.GetComponent<ObjectTutoring>();
            if (myScript != null)
            {
                myScript.Title = "haha load from cs\n"; // ÉèÖÃÐÂµÄ
                myScript.Description = "111 cs\n";
            }
        }
        isTargetCreated = true; // maybe don't need this
        // When NextFloor = "2": Move, jump, accelerate with SHIFT, follow the arrow instructions
        // When NextFloor = "3": Shoot the same color cube, walk over
        // When NextFloor = "4": Pick up the weapon package, shoot different color cube, jump over
        // When NextFloor = "5": shift + jump, jump further
        // When NextFloor = "6": Touch the target, complete the task
    }
}