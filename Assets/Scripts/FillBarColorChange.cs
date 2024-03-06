using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS
{
    public class FillBarColorChange : MonoBehaviour
    {
        [Header("Foreground")] [Tooltip("Image for the foreground")]
        public Image ForegroundImage;

        [Tooltip("Default foreground color")] public Color DefaultForegroundColor;

        [Tooltip("Flash foreground color when full")]
        public Color FlashForegroundColorFull;

        [Header("Background")] [Tooltip("Image for the background")]
        public Image BackgroundImage;

        [Tooltip("Flash background color when empty")]
        public Color DefaultBackgroundColor;

        [Tooltip("Sharpness for the color change")]
        public Color FlashBackgroundColorEmpty;

        [Header("Values")] [Tooltip("Value to consider full")]
        public float FullValue = 1f;

        [Tooltip("Value to consider empty")] public float EmptyValue = 0f;

        [Tooltip("Sharpness for the color change")]
        public float ColorChangeSharpness = 5f;

        float m_PreviousValue;
        bool notificationSent = false;
        public void Initialize(float fullValueRatio, float emptyValueRatio)
        {
            FullValue = fullValueRatio;
            EmptyValue = emptyValueRatio;
            PlayerCharacterController m_Controller = FindObjectOfType<PlayerCharacterController>();
            // convert m_Controller.color to hsv, and decrease the saturation, then convert back
            Color.RGBToHSV(m_Controller.color, out float H, out float S, out float V);
            S -= 0.6f; // decrease the saturation by 20%
            S = Mathf.Clamp01(S); // ensure S stays within the range [0, 1]
            DefaultForegroundColor = Color.HSVToRGB(H, S, V);

            m_PreviousValue = fullValueRatio;
        }

        public void UpdateVisual(float currentRatio)
        {
            if (currentRatio == FullValue && currentRatio != m_PreviousValue)
            {
                ForegroundImage.color = FlashForegroundColorFull;
            }
            else if (currentRatio < EmptyValue)
            {
                BackgroundImage.color = FlashBackgroundColorEmpty;
                if (!notificationSent)
                {
                    NotificationHUDManager notification = FindObjectOfType<NotificationHUDManager>();
                    notification.CreateNotification("Out of ammo? Hit R to restart!");
                    notificationSent = true;
                }
            }
            else
            {
                ForegroundImage.color = Color.Lerp(ForegroundImage.color, DefaultForegroundColor,
                    Time.deltaTime * ColorChangeSharpness);
                BackgroundImage.color = Color.Lerp(BackgroundImage.color, DefaultBackgroundColor,
                    Time.deltaTime * ColorChangeSharpness);
            }

            m_PreviousValue = currentRatio;
        }
    }
}