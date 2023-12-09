using System.Collections;
using GP1.Gameplay;
using UnityEngine;

namespace GP1.Global
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public void Begin()
        {
            _userInput.Locked = false;
            _levelUI.ToggleStartUI(false);
            _levelUI.Timer.StartTimer();
            StartCoroutine(CoBegin());
        }

        public void End()
        {
            _userInput.Locked = true;
            _levelUI.Timer.StopTimer();
            _levelUI.ShowFinalTime();
            StartCoroutine(CoEnd());
        }
        
        private void Start()
        {
            MovementManager.Instance.UnitPassed += OnUnitPassed;
            MovementManager.Instance.GoalReached += End;
        }

        private IEnumerator CoBegin()
        {
            MovementManager.Instance.GlobalMultiplier = 15f;
            while (MovementManager.Instance.GlobalMultiplier > 1.01f)
            {
                MovementManager.Instance.GlobalMultiplier = Mathf.Lerp(MovementManager.Instance.GlobalMultiplier, 1f,
                    Time.deltaTime * 4f);
                yield return null;
            }

            MovementManager.Instance.GlobalMultiplier = 1f;
            _levelUI.TogglePlayerHUD(true);
        }
        
        private IEnumerator CoEnd()
        {
            while (MovementManager.Instance.GlobalMultiplier > 0.05f)
            {
                MovementManager.Instance.GlobalMultiplier = Mathf.Lerp(MovementManager.Instance.GlobalMultiplier, 0f,
                    Time.deltaTime * 4f);
                yield return null;
            }

            MovementManager.Instance.GlobalMultiplier = 0f;
            _levelUI.TogglePlayerHUD(false);
            _levelUI.ToggleOverUI(true);
        }

        private void OnUnitPassed(int totalDistance, float offset)
        {
            _levelUI.ShowDistance(totalDistance, MovementManager.Instance.DistanceGoal);
        }

        [SerializeField]
        private UserInput _userInput;
        [SerializeField]
        private LevelUI _levelUI;
    }
}
