using System.Collections;
using UnityEngine.SceneManagement;

namespace GP1.Global
{
    public class TransitionManager : MonoSingleton<TransitionManager>
    {
        public void OpenMenu() => StartCoroutine(TransitionTo("MainMenu"));
        public void OpenLevel() => StartCoroutine(TransitionTo("Game"));
        
        private IEnumerator TransitionTo(string sceneName)
        {
            // Animations go here
            SceneManager.LoadScene(sceneName);
            yield return null;
        }
    }
}
