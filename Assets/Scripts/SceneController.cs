using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public void Load(string scene)
	{
		Debug.Log ("Load scene: " + scene);
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
	}

	public void Quit()
	{
		Load("MenuScene");
	}

	public void Close()
	{
		Application.Quit ();
	}
}