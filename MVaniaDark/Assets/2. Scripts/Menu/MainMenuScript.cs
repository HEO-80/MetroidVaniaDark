using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public Animator settingAnim;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        
       

        if (scene.name == "menuIntro")
        {
            
            AudioManager.instance.backGroundMusic.Stop();
            
            AudioManager.instance.PlayAudio(AudioManager.instance.mainMenu);
        }
        Time.timeScale= 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Game Closed");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowSettings()
    {
        settingAnim.SetBool("ShowSettings", true);
    }

    public void HideSettings()
    {
        settingAnim.SetBool("ShowSettings", false);
    }
}
