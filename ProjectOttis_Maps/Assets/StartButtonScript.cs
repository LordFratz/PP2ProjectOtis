using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartButtonScript : MonoBehaviour {

    [SerializeField]
    string SceneName;
	public void StartGame()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
