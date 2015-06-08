using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	// Use this for initialization
    public GameObject loadObject;
    public GameObject canvas;
    public  void StartGame()
    {
        loadObject.SendMessage("LoadNextLevel");
        //因为和ugui冲突 所以把canvas禁用掉
        canvas.SetActive(false);
     //   Application.LoadLevel("Demo2");
    }

}
