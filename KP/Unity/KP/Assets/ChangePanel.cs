using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanel : MonoBehaviour
{
    void Start()
    {
       if (gameObject.name != "Structure")
        {
            if(gameObject.name != "HideStuctureButton")
            {
                Close(); // Закрыть всплывающее окно при запуске программы, если это не объект Structure
            }
        }
    }
    public void Open()
    {
        gameObject.SetActive(true); //   АКТИВИРОВАТЬ ОБЪЕКТ, ЧТОБЫ ОТКРЫТЬ ОКНО.  

    }

    public void Close()
    {
        gameObject.SetActive(false); // ДЕАКТИВИРОВАТЬ ОБЪЕКТ, ЧТОБЫ ЗАКРЫТЬ ОКНО.  
    }
}
