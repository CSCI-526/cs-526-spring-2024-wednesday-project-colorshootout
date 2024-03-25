using System.Collections;
using System.Collections.Generic;
using Unity.FPS;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    Dictionary<string, GameObject> floorMap = new Dictionary<string, GameObject>();
    PlayerCharacterController m_Controller;
    string floor;
    bool isTargetCreated = false;
    [Tooltip("Target prefab")] public GameObject TargetPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("InteractiveFloor");
        foreach (GameObject cube in cubes)
        {
            floorMap[cube.name] = cube;    // name : 1, 2, 3, 4, 5, 6
        }
        m_Controller = FindObjectOfType<PlayerCharacterController>();
        floor = "1";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = m_Controller.transform.position;

        // �ж�playerPos�Ƿ���floorMap[��һ��floor]�ķ�Χ�ڣ� x y z �ж�
        // ����ڣ������õ�ǰtarget��ɣ���floor = "1"��"2"��"3"��"4"��"5"��"6" ����һ�������ҳ�ʼ���µ�target
        if (TargetPrefab && !isTargetCreated)
        {
            // destroy�ɵ�targetInstance
            // destroy ---->   GameObject.FindGameObjectWithTag("CurrTarget");
            var targetInstance = Instantiate(TargetPrefab, transform.position, Quaternion.identity);
            var myScript = targetInstance.GetComponent<ObjectTutoring>();
            if (myScript != null)
            {
                myScript.Title = "haha load from cs\n"; // �����µ�
                // ....
            }
        }
        isTargetCreated = true;
    }
}