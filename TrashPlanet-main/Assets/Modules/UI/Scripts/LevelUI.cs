using UnityEngine;
using GP1.Global;
using GP1.UI;
using TMPro;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
     public Leaderboard Leaderboard => _leaderboard;
     public Timer Timer => _timer;
     
     public void BackToMenu() => TransitionManager.Instance.OpenMenu();
     public void Restart() => TransitionManager.Instance.OpenLevel();
     public void Begin() => LevelManager.Instance.Begin();
     public void Quit() => Application.Quit();

     public void TogglePlayerHUD(bool enabled) => _playerHUD.SetActive(enabled);
     public void ToggleStartUI(bool enabled) => _startUI.SetActive(enabled);
     public void ToggleOverUI(bool enabled) => _overUI.SetActive(enabled);

     public void SubmitLeaderboard()
     {
          _leaderboard.AddEntry(_playerName, _timer.currentTime);
          Restart();
     }

     public void ShowDistance(int units, int maxUnits)
     {
          _distanceText.text = $"{units:D4} / {maxUnits:D4}m";
     }
     
     public void ShowFinalTime()
     {
          _finalTimeText.text = $"Time: {Timer.FormatTime(_timer.currentTime)}";
     }

     public void OnNameUpdated(string name)
     {
          _playerName = name.Trim(' ');
          _submitTimeButton.interactable = _playerName != "";
     }

     private string _playerName;

     [SerializeField]
     private GameObject _playerHUD;
     [SerializeField]
     private GameObject _overUI;
     [SerializeField]
     private GameObject _startUI;
     [SerializeField]
     private TMP_Text _distanceText;
     [SerializeField]
     private TMP_Text _finalTimeText;
     [SerializeField]
     private Button _submitTimeButton;
     [SerializeField]
     private Leaderboard _leaderboard;
     [SerializeField]
     private Timer _timer;
}

