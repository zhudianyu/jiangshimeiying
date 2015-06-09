﻿using UnityEngine;
using System.Collections;
//通过射线检测来判断是否攻击到玩家
public class AttackPlayer : MonoBehaviour {

    public Transform rayStart;
    public Transform rayEnd;
    private Vector3 dir;
    private float distance;
    private RaycastHit hitInfo;
    public Animator enemyAnimator;
    public PlayerHpCtronl hpCtrl;
    public int hurtValue = 1;
    private float timer = 0;
	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        //只有播放攻击动画时候才判断射线检测
        if(enemyAnimator.GetCurrentAnimatorStateInfo(0).IsTag(GlobalCtrol.ATTACK))
        {
            dir = rayEnd.position - rayStart.position;
            distance = Vector3.Distance(rayStart.position, rayEnd.position);
      
            if (Physics.Raycast(rayStart.position, dir, out hitInfo, distance))
            {
                if(hitInfo.collider.tag.Equals(GlobalCtrol.PLAYER))
                {
                    timer += Time.deltaTime;
                    if(timer>0.2f)
                    {
                        hpCtrl.hited(hurtValue);
                  
                        timer = 0;
                    }
                  


                }
            }
         
        }
     
      }
}