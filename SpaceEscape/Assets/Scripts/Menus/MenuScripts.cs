using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScripts : MonoBehaviour
{


	public void ReturnToMainMenuButtonPressed()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}

	public void PlayButtonPressed()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainLevel");
	}
	public void ExitButtonPressed()
	{
		Application.Quit();
	}


}
