using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GlobalSceneControl : MonoBehaviour {

    public GameObject winPanel;
    public int enemyCount;
    // Use this for initialization
	void Start () {
        enemyCount = GameObject.FindGameObjectsWithTag(GlobalCtrol.ENEMY).Length;
        InvokeRepeating("showWin", 3f, 2f);
        //重复调用showwin 3s后开始 2s调用一次

	}
	void showWin()
    {
        if(enemyCount<=0)
        {
            winPanel.SetActive(true);
            Invoke("changeScene", 3f);
        }
    }
    void changeScene()
    {
        Application.LoadLevel("StartScene");
    }
	// Update is called once per frame
	void Update () {
	
	}
}
