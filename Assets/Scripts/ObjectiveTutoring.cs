using System.Threading;
using Unity.FPS;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

namespace Unity.FPS
{
    public class ObjectTutoring: Objective
    {
        public bool Completed = false;
        bool Finished = false;
        void Awake()
        {
            
        }

        private void Update()
        {
        }

        public void SetCompleted()
        {
            CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);
        }

    }
}