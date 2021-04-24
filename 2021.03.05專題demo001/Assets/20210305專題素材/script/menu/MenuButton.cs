using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] int thisIndex;
    public GameObject pick;
    [SerializeField] bool onoff;
    public GameObject loadscreen;
    public Slider slider;
    public Text progresstext;

    // Update is called once per frame
    void Update()
    {
       
        if (menuButtonController.index == thisIndex )
        {
            pick.SetActive(true);
            if (Input.GetAxis("Submit")==1)
            {
                if (thisIndex == 0)
                    loadlevel(1);
                if (thisIndex == 1)
                    Debug.Log("還沒做拉");
            }
            
        }
        else
        {
            pick.SetActive(false);
        }
        
    }
    public void OnMouseOver()
    {
        onoff = true;
        menuButtonController.index = thisIndex;
        
        
    }
    public void OnMouseExit()
    {
        onoff = false;
    }
    

    public void loadlevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadscreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progresstext.text = progress * 100f + "%";
            yield return null;
        }
    }
}
