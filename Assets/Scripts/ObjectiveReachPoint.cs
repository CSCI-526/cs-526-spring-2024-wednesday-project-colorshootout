using Unity.FPS;
using UnityEngine;

namespace Unity.FPS
{
    [RequireComponent(typeof(Collider))]
    public class ObjectiveReachPoint : Objective
    {
        void Awake()
        {
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject);
            if (IsCompleted)
                return;
            if (other.GetComponent<PlayerCharacterController>() == null)
                return;
            var player = other.GetComponent<PlayerCharacterController>();
            // test if the other collider contains a PlayerCharacterController, then complete
            if (player != null)
            {
                CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

            }
        }
    }
}