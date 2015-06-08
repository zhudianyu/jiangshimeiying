using UnityEngine;
using System.Collections;


public class EnemyCtronl : MonoBehaviour {
    public NavMeshAgent meshAgent;

    public Transform[] wayArray;
    private int wayPointIndex = 0;

    private float timer = 0;

    public Animator enemyAnimator;
    public Transform playerTrans;
    public bool isFindePlayer = false;
	// Use this for initialization
	void Start () {

        meshAgent.destination = wayArray[wayPointIndex].position;
	}
	
    private void Patrol()
    {
        enemyAnimator.SetBool("IsAttacked", false);
        if (meshAgent.remainingDistance < 0.5f)
        {
            enemyAnimator.SetBool("IsWalk", false);
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                timer = 0;
            }
            wayPointIndex++;
            if (wayPointIndex >= wayArray.Length)
            {
                wayPointIndex = 0;

            }
            meshAgent.destination = wayArray[wayPointIndex].position;
        }
        else
        {
            enemyAnimator.SetBool("IsWalk", true);
            enemyAnimator.SetBool("IsRun", false);
            meshAgent.destination = wayArray[wayPointIndex].position;
        }
    }
    //攻击玩家
    private void RaidPlayer()
    {
        meshAgent.destination = playerTrans.position;
        enemyAnimator.SetBool("IsRun", true);
        enemyAnimator.SetBool("IsWalk", false);
        meshAgent.speed = 2f;
        //敌人靠近玩家后，攻击，首先要停下来
        if (meshAgent.remainingDistance < 1f)
        {
            enemyAnimator.SetBool("IsRun", false);
            enemyAnimator.SetBool("IsAttacked", true);
            meshAgent.Stop();
        }
        else
        {
            enemyAnimator.SetBool("IsAttacked", false);
            enemyAnimator.SetBool("IsRun", true);
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if(isFindePlayer)
        {
            RaidPlayer();
           
        }
        else
        {
            Patrol();

        }

	}
}
