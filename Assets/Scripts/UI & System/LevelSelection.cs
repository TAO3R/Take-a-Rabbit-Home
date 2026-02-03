using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void Level01()
    {
        SceneManager.LoadScene("ChildLossScene");
    }

    public void Level02()
    {
        SceneManager.LoadScene("Level02");
    }

    public void Level03()
    {
        SceneManager.LoadScene("Level03");
    }

    public void Level04()
    {
        SceneManager.LoadScene("Level04");
    }

    public void Level05()
    {
        SceneManager.LoadScene("Level05");
    }

    public void Level06()
    {
        SceneManager.LoadScene("Level06");
    }

    public void Level07()
    {
        SceneManager.LoadScene("CrocWakeUp");
    }

    public void Level08()
    {
        SceneManager.LoadScene("Level08");
    }

    public void Level09()
    {
        SceneManager.LoadScene("Level09");
    }

    public void Level10()
    {
        SceneManager.LoadScene("Level10");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
