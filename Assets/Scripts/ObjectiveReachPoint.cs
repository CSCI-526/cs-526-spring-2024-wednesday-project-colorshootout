using System.Threading;
using Unity.FPS;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

namespace Unity.FPS
{
    [RequireComponent(typeof(Collider))]

    public class ObjectiveReachPoint : Objective
    {
        public float timeLimit = 120.0f; // 时间限制，单位为秒
        bool[] notificationSent = new bool[2] { false, false };
        bool startProcessingTimeout = false;
        void Awake()
        {
        }

        private void Update()
        {
            timeLimit -= Time.deltaTime;
            if (timeLimit > 0)
            {
                // 更新UI文本以显示剩余时间，格式化为分钟:秒
                string text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeLimit / 60), Mathf.FloorToInt(timeLimit % 60));
                UpdateObjective(string.Empty, text, string.Empty);
                if (timeLimit < 30.0f && !notificationSent[0])
                {
                    DisplayMessageEvent displayMessage = Events.DisplayMessageEvent;
                    displayMessage.Message = "30s Left !!!";
                    displayMessage.DelayBeforeDisplay = 0.0f;
                    EventManager.Broadcast(displayMessage);
                    notificationSent[0] = true;
                }

                if (timeLimit < 5.0f && !notificationSent[1])
                {
                    DisplayMessageEvent displayMessage = Events.DisplayMessageEvent;
                    displayMessage.Message = "Last 5s !!!";
                    displayMessage.DelayBeforeDisplay = 0.0f;
                    EventManager.Broadcast(displayMessage);
                    notificationSent[1] = true;
                }
            } 
            else if (!startProcessingTimeout)
            {
                PlayerCharacterController m_Controller = FindObjectOfType<PlayerCharacterController>();
                Form form = FindObjectOfType<Form>();
                if (form != null) form.Send(Form.EndType.Timeout);
                startProcessingTimeout = true;
                m_Controller.KillMySelf();
            }
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