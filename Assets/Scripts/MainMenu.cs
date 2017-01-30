using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Parameters parameters;

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PvAButton()
    {
        parameters.GetComponent<Parameters>().AIPlayer = true;
        SceneManager.LoadScene(1);
    }

    public void PvPButton()
    {
        parameters.GetComponent<Parameters>().AIPlayer = false;
        SceneManager.LoadScene(1);
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