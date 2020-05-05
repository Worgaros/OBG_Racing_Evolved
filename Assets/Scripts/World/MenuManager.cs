using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
    }
    
    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
