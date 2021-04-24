using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startgaame : MonoBehaviour
{
    
    public  void startgame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void endgame()
    {

    }
}
