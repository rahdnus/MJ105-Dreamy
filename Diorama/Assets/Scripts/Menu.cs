using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Menu : MonoBehaviour
{
    [SerializeField]GameObject curtains;
    public void UpdatePlayerCounter()
    {
        string path=Application.persistentDataPath+GameManager.Instance.saveName;
        FileStream stream=new FileStream(path,FileMode.OpenOrCreate,FileAccess.ReadWrite);
        PlayerData data=new PlayerData();
        BinaryFormatter formatter=new BinaryFormatter();
        if(stream.Length==0)
        {
            formatter.Serialize(stream,data);
        }
        // else
        // {
        //     data=formatter.Deserialize(stream) as PlayerData;
        //     data.increment();
        //     Debug.Log(data.playercounter);
        //     stream.SetLength(0);
        //     formatter.Serialize(stream,data);
        // }
        stream.Close();
    }
    Coroutine routine;
    public void LoadScene(int index)
    {
        if(routine==null)
            routine=StartCoroutine(IloadScene(index));
    }
    IEnumerator IloadScene(int index)
    {
        curtains.GetComponent<Animator>().Play("in");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(index);

    }
    public void Exit()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class PlayerData
{
    public int playercounter=0;
    public void increment()
    {
        if(playercounter<4)
        playercounter+=1;
    }
}
