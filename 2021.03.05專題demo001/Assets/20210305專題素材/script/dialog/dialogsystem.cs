using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogsystem : MonoBehaviour
{
    
    [Header("ui組件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textfile;
    public int index;
    public float textSpeed;
    public TextAsset text2;
    public TextAsset text3;

    [Header("頭像")]
    public Sprite face01, face02;


    bool textFininshed;
    bool canceltyping;
    List<string> textlist = new List<string>();

    public GameObject missionpanel;
    public int missionNum=0;
    // Start is called before the first frame update
    void Awake()
    {
        getTextFormFile(textfile);
        missionpanel.SetActive(false);
    }
    private void OnEnable()
    {
        // textLabel.text = textlist[index];
        //index++;
        textFininshed = true;
        StartCoroutine(SetTextUi());

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mission.missioncomplete && missionNum == 0)
        {
            getTextFormFile(text3);
            //int i = textlist.Count;
            //index = i-1;
            missionpanel.SetActive(false);
            missionNum = 1;
            
        }
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R) && index == textlist.Count) //對話到最後一行
        {
            if(!mission.missioncomplete)
            {
                getTextFormFile(text2);
                missionpanel.SetActive(true);
            }
            if (mission.missioncomplete && missionNum == 0)
            {
                getTextFormFile(text3);
                missionpanel.SetActive(false);
                missionNum = 1;

            }
            gameObject.SetActive(false);
            index = 0;

            
            return;
        }

        //if (Input.GetKeyDown(KeyCode.R) && textFininshed)
        //{
        //    //textLabel.text = textlist[index];
        //    //index++;
        //    StartCoroutine(SetTextUi());
        //}
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (textFininshed && !canceltyping)
            {
                    StartCoroutine(SetTextUi());
            }
            else if (!textFininshed && !canceltyping)
            {
                    canceltyping = true;
            }
        }
    }
    IEnumerator SetTextUi()
    {
        textFininshed = false;
        textLabel.text = "";

        switch (textlist[index])
        {
            case "A":
                faceImage.sprite = face01;
                index++;
                break;
            case "B":
                faceImage.sprite = face02;
                index++;
                break;
        }

        //for(int i = 0; i < textlist[index].Length; i++)
        //{
        //    textLabel.text += textlist[index][i];

        //    yield return new WaitForSeconds(textSpeed);
        //}
        int letter = 0;
        while (!canceltyping && letter < textlist[index].Length - 1)
        {
            textLabel.text += textlist[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textlist[index];
        canceltyping = false;
        textFininshed = true;
        index++;
    }
    void  getTextFormFile(TextAsset file)
    {
        textlist.Clear();
        index = 0;

        var linedata = file.text.Split('\n');

        foreach(var line in linedata)
        {
            textlist.Add(line);
        }
    }
}
