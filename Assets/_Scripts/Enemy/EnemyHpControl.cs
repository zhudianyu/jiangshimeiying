using UnityEngine;
using System.Collections;


public class EnemyHpControl : MonoBehaviour {

    public float enemyHp = 100;
	// Use this for initialization
    private Animator enemyAnimator;

    private GlobalSceneControl gCtrl;

    private GameObject bloodObj;
	void Start () {
        enemyAnimator = gameObject.GetComponent<Animator>();
        GameObject gObj = GameObject.Find("GlobalSceneControl");
        gCtrl = gObj.GetComponent<GlobalSceneControl>();
        bloodObj = (GameObject)Resources.Load("Box/IGSoft_Resources/Projects/[EffectParticle]/Legacy_Ground/ground 14");
	}
	public void hited(int hurtPoint)
    {
        enemyHp -= hurtPoint;
        GameObject.Instantiate(bloodObj, transform.position, Quaternion.identity);
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
