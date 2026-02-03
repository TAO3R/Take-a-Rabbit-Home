using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PictureViewer : MonoBehaviour
{
    public Sprite[] pictures;
    public int currentIndex = 0;

    public float counter;
    private bool autoPlay;

    public GameObject button;
    public Color buttonNormalColor;
    public Color buttonPressedColor;
    
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        GetComponent<SpriteRenderer>().sprite = pictures[currentIndex];

        counter = 0.5f;
        autoPlay = true;

        
        buttonNormalColor = button.GetComponent<Button>().colors.normalColor;
        buttonPressedColor = button.GetComponent<Button>().colors.pressedColor;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (autoPlay)
            {
                autoPlay = false;
                button.GetComponent<Image>().color = buttonNormalColor;
            }
            currentIndex++;

            if (currentIndex >= pictures.Length)
            {
                if (SceneManager.GetActiveScene().name == "ChildLossScene")
                { SceneManager.LoadScene("Level01"); }
                else if (SceneManager.GetActiveScene().name == "CrocWakeUp")
                { SceneManager.LoadScene("Level07"); }

            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = pictures[currentIndex];
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentIndex >= 1)
        {
            autoPlay = false;
            button.GetComponent<Image>().color = buttonNormalColor;
            currentIndex--;
            GetComponent<SpriteRenderer>().sprite = pictures[currentIndex];
        }

        if (autoPlay)
        {
            if (counter <= 0)
            {
                currentIndex++;
                if (currentIndex >= pictures.Length)
                {
                    if (SceneManager.GetActiveScene().name == "ChildLossScene")
                    { SceneManager.LoadScene("Level01"); }
                    else if (SceneManager.GetActiveScene().name == "CrocWakeUp")
                    { SceneManager.LoadScene("Level07"); }

                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = pictures[currentIndex];
                }

                counter = 0.5f;
            }
            else
            {
                counter -= Time.deltaTime;
            }
        }
    }

    public void autoPlaySwitch()
    {
        autoPlay = !autoPlay;
        if (autoPlay) { button.GetComponent<Image>().color = buttonPressedColor; }
        else { button.GetComponent<Image>().color = buttonNormalColor; }
        counter = 1f;
    }
}
