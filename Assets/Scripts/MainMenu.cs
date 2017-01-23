using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject quitButn;

	[SerializeField]
	private GameObject startButtn;

	public void QuitButton ()
	{
		Application.Quit ();
	}

	//public void PvAButton ()
	//{
	//	SceneManager.LoadScene (2);
	//}

	public void PvPButton ()
	{
		SceneManager.LoadScene (1);
	}

	//public void SoundToggle ( bool audioValue )
	//{
	//	if (audioValue == true)
	//	{
	//		AudioListener.pause = true;
	//		AudioListener.volume = 1;
	//	}
	//	else
	//	{
	//		AudioListener.pause = false;
	//		AudioListener.volume = 0;
	//	}
	//}
}