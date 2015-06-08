using UnityEngine;
using System.Collections;
//通过球形触发器判断是否追击玩家
public class FindPlayerCtronl : MonoBehaviour {

  public  EnemyCtronl enemCtrol;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider player)
    {
        if(player.tag.Equals(GlobalCtrol.PLAYER))
        {
            enemCtrol.isFindePlayer = true;
        }
        
    }
    void OnTriggerExit(Collider player)
    {
        if (player.tag.Equals(GlobalCtrol.PLAYER))
        {
            enemCtrol.isFindePlayer = false;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
