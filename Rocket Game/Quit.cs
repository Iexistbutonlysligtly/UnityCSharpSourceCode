using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Quit : MonoBehaviour, IPointerDownHandler
{
    private int Next;

    void Start()
    {

        Next = SceneManager.GetActiveScene().buildIndex - 1;



    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 1;
        //Application.Quit();

        SceneManager.LoadScene(Next);
        print("Qutting");
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    void OnMouseOver()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(Next);
            // Application.Quit();
            // UnityEditor.EditorApplication.isPlaying = false;

        }

    }

  public void Quitting()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Next);
        //Application.Quit();
        //  UnityEditor.EditorApplication.isPlaying = false;
    }
}
