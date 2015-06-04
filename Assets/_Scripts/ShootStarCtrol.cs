using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
 1.实现瞄准星的旋转
2.实现瞄准星大小随时间变化
3.锁定敌人后变红，没有敌人保持原来的颜色
 */
public class ShootStarCtrol : MonoBehaviour {

    private RaycastHit hitinfo ;

    public Color hitedEnemyColor;
    public Color notHitedColor;
	// Use this for initialization
	void Start () {
        //实现瞄准星大小随时间变化
        iTween.ScaleTo(this.gameObject, iTween.Hash("scale", new Vector3(2, 2, 1), "time", 1.5f, "looptype", iTween.LoopType.pingPong));
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(new Vector3(0, 0, 200*Time.deltaTime));
        //射线 检测到物体就返回true
       if( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitinfo))
       {
           if(hitinfo.collider.tag.Equals("Enemy"))
           {
               this.gameObject.GetComponent<Image>().color = hitedEnemyColor;
           }
           else
           {
               this.gameObject.GetComponent<Image>().color = notHitedColor;
           }
       }
       else
       {
           this.gameObject.GetComponent<Image>().color = notHitedColor;

       }
	}
}
