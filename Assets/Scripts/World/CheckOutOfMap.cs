using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckOutOfMap : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            SceneManager.LoadScene("WilliamTestScene");
        }
    }
}