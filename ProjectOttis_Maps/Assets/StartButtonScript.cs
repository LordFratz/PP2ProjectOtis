using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartButtonScript : MonoBehaviour {

    [SerializeField]
    string SceneName;
	public void StartGame()
    {
        SceneManager.LoadScene(SceneName);
        if (SceneName == "LevelStage_2.0")
        {
            SoundManager.instance.BackgroundMusic.Play();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
