using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator crossfadeAnim;
    public Animator crocAnim;

    private void Awake()
    {
        // Deactivate pause menu
        GameObject.Find("Level Manager/Canvas/Pause Menu").SetActive(false);
        StartCoroutine(ResumeTime());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.Find("Level Manager/Canvas/Pause Menu").activeSelf)
            {
                Resume();
            }
            else
            {
                GameObject.Find("Level Manager/Canvas/Pause Menu").SetActive(true);
                Time.timeScale = 0;
            }
        }
    }   // ACtivate the pause menu

    #region Resuem & UI Functions

    public void Resume()    // Used by the button
    {
        GameObject.Find("Level Manager/Canvas/Pause Menu").SetActive(false);
        StartCoroutine(ResumeTime());
    }

    IEnumerator ResumeTime()
    {
        yield return new WaitForEndOfFrame();     // need to check whether waitforendofframe works as well
        Time.timeScale = 1;
    }

    public void BackToMain()    // Used by the button
    {
        SceneManager.LoadScene(0);
    }

    public void BackToLevelSelect()     // Used by the button
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadLevel()       // Used by the button
    {
        Endlevel();
        Invoke(nameof(ReloadScene), 1f);
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name != "Level10")
        {
            Endlevel();
            Invoke(nameof(LoadNextScene), 1f);
        }
        else if (SceneManager.GetActiveScene().name == "Level06")
        {
            Endlevel();
            Invoke(nameof(LoadNightCutscene), 1f);
        }
        else
        {
            Endlevel();
            Invoke(nameof(BackToMain), 1f);
        }
        
    }

    public void Endlevel() { crossfadeAnim.SetTrigger("LevelEnd"); }
    private void ReloadScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

    private void LoadNextScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }

    private void LoadNightCutscene() { SceneManager.LoadScene("CrocWakeUp");  }

    #endregion
}
