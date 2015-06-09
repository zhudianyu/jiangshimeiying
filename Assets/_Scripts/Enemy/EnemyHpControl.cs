using UnityEngine;
using System.Collections;


public class EnemyHpControl : MonoBehaviour {

    public float enemyHp = 100;
	// Use this for initialization
    public Animator enemyAnimator;

    public GlobalSceneControl gCtrl;
	void Start () {
	
	}
	public void hited(int hurtPoint)
    {
        enemyHp -= hurtPoint;
        if(enemyHp<=0)
        {
            enemyAnimator.SetInteger("DeathType", Random.Range(1, 3));
            gCtrl.enemyCount -= 1;
            Destroy(this.gameObject.GetComponent<NavMeshAgent>());
            Destroy(this.gameObject.GetComponent<EnemyCtronl>());
            Destroy(this.gameObject.GetComponent<AttackPlayer>());
            Destroy(this.gameObject.GetComponent<CharacterController>());
           // Destroy(this.gameObject.GetComponent<NavMeshAgent>());
        
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
