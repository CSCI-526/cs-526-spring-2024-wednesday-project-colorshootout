using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using Unity.FPS;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static System.Net.WebRequestMethods;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class Form : MonoBehaviour
{
    [SerializeField] private string URL = "https://docs.google.com/forms/u/3/d/e/1FAIpQLScApEjdcPN4NyrCOJkwygtOdE0__a9zQH8TtuVxL2dZ8m6C4g/formResponse";
    public enum EndType
    {
        Win,
        Restart,
        Timeout,
        Fall
    }
    private static long _sessionID = DateTime.Now.Ticks;
    public float[] _timeOnObject; // time on sth
    public float[] _ammoReceive;
    public EndType _typeEnd; 
    public float[] _endPos;
    public float _leftTime;   
    public float _ammoRemain;
    string _scene;
    // Start is called before the first frame update
    public Dictionary<string, int> cubeMap = new Dictionary<string, int>();


    string ArrayToString(float[] array)
    {
        if (array == null)
        {
            return "";
        }
        return string.Join(", ", array.Select(x => x.ToString()));
    }

    private void Awake()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube_Color");
        int index = 0;
        foreach (GameObject cube in cubes)
        {
            cubeMap[cube.name] = index;
            index++;
        }
        cubes = GameObject.FindGameObjectsWithTag("Cube_Gravity");
        foreach (GameObject cube in cubes)
        {
            cubeMap[cube.name] = index;
            index++;
        }
        _ammoReceive = new float[cubeMap.Count];
    }

    public void Send(EndType t)
    {
        PlayerCharacterController m_Controller = FindObjectOfType<PlayerCharacterController>();
        _leftTime = FindObjectOfType<ObjectiveReachPoint>().timeLimit;
        _ammoRemain = FindObjectOfType<WeaponController>().m_CurrentAmmo;
        _endPos = new float[3] { m_Controller.transform.position.x, m_Controller.transform.position.y, m_Controller.transform.position.z };
        _typeEnd = t;
        _scene = SceneManager.GetActiveScene().name;
        StartCoroutine(Post());
    }

    private IEnumerator Post()
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1701183535", _sessionID.ToString());
        form.AddField("entry.373754194", ArrayToString(_timeOnObject));
        form.AddField("entry.120729588", ArrayToString(_ammoReceive));
        form.AddField("entry.428501384", _typeEnd.ToString());
        form.AddField("entry.1289767374", _leftTime.ToString());
        form.AddField("entry.303806218", _ammoRemain.ToString());
        form.AddField("entry.652706724", ArrayToString(_endPos));
        form.AddField("entry.1939232977", _scene);

        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
