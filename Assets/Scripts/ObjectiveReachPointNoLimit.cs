using System.Threading;
using Unity.FPS;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

namespace Unity.FPS
{
    [RequireComponent(typeof(Collider))]

    public class ObjectiveReachPointNoLimit : Objective
    {
        void Awake()
        {
        }

        private void Update()
        {
            
        }

        void OnTriggerEnter(Collider other)
        {
            if (IsCompleted)
                return;
            if (other.GetComponent<PlayerCharacterController>() == null)
                return;
            var player = other.GetComponent<PlayerCharacterController>();
            // test if the other collider contains a PlayerCharacterController, then complete
            if (player != null)
            {
                Form form = FindObjectOfType<Form>();
                if (form != null)
                    form.Send(Form.EndType.Win);

                CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);
            }
        }
    }
}