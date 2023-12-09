using UnityEngine;
using TMPro;

namespace GP1.UI
{
    public class Timer : MonoBehaviour
    {
        [Header("Component")]
        public TMP_Text timerText;

        [Header("Timer Settings")]
        public float currentTime;

        public static string FormatTime(float time)
        {
            return $"{(int) time / 60:D2}:{(int) time % 60:D2}";
        }

        public void StartTimer()
        {
            timerActive = true;
        }
        
        public void StopTimer()
        {
            timerActive = false;
        }
        
        // Level related global logic
        public void Update()
        { 
            if (timerActive)
            {
                UpdateTimerUI();
            }
        }
        
        public void UpdateTimerUI()
        {
            currentTime += Time.deltaTime;

            timerText.text = FormatTime(currentTime);
        }
        
        private bool timerActive;
    }
}