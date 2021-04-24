using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class savemanager : MonoBehaviour
{
    
    public item thisItem;
    public inventory myinventory;

    public save sav = new save();
    public GameObject playercat;
    int status01;
    
    // Start is called before the first frame update
    public void Savegame()
    {
        Debug.Log(Application.persistentDataPath);

        if (!Directory.Exists(Application.persistentDataPath + "/game_saveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_saveData");
        }

        BinaryFormatter formatter = new BinaryFormatter();//二進位轉化

        FileStream file = File.Create(Application.persistentDataPath + "/game_saveData/inventory.txt");

        var json = JsonUtility.ToJson(myinventory);

        formatter.Serialize(file, json);

        file.Close();
        BinaryFormatter formatter2 = new BinaryFormatter();//二进制转化

        FileStream file2 = File.Create(Application.persistentDataPath + "/game_SaveData/inventory2.txt");//  1   创建存储文件

        var json2 = JsonUtility.ToJson(thisItem);//   2  能覆写回来

        formatter2.Serialize(file2, json2);//(1,2)
        file2.Close();
    }

    public void loadgame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if(File.Exists(Application.persistentDataPath + "/game_saveData/inventory.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_saveData /inventory.txt", FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), myinventory);

            file.Close();
        }
        BinaryFormatter bf2 = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_SaveData/inventory2.txt"))
        {
            FileStream file2 = File.Open(Application.persistentDataPath + "/game_SaveData/inventory2.txt", FileMode.Open);//打开文件

            JsonUtility.FromJsonOverwrite((string)bf2.Deserialize(file2), thisItem);//反序列化

            file2.Close();

        }
    }
    public void save02()
    {
        int status01 = PLAYER.status;
        Debug.Log(status01 + "應為");
        sav.status = status01;
        sav.playerx = playercat.transform.position.x;
        sav.playery = playercat.transform.position.y;
        sav.playerz = playercat.transform.position.z;


        string path = Application.dataPath + "/savegamebinary";
        string fileName = "charact.sav";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path + "/" + fileName);

        try{
            bf.Serialize(file,sav);
        }catch(SerializationException e){
            Debug.Log("沒創到拉");
        }

        file.Close();
    }
    public void load02()
    {
        string path = Application.dataPath + "/savegamebinary";
        string fileName = "charact.sav";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path + "/" + fileName, FileMode.Open);
        Vector3 pos = transform.position;
        pos.x = sav.playerx;
        pos.y = sav.playery;
        pos.z = sav.playerz;
        sav = (save)bf.Deserialize(file);
        status01 = sav.status;
        playercat.transform.position = pos;
        PLAYER.status = status01;
        

        file.Close();
    }
}
