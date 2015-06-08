using UnityEngine;
using System.Collections;

public class HpBoxCtronl : MonoBehaviour {

    public PlayerHpCtronl hpControl;
    private float currentHp;
    private bool isAllowAddHp = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
        if(isAllowAddHp)
        {
            hpControl.hp = Mathf.Lerp(hpControl.hp, currentHp + 30, 1f * Time.deltaTime);

        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            currentHp = hpControl.hp;
            isAllowAddHp = true;
            Destroy(gameObject.GetComponent<MeshRenderer>());
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(this.gameObject, 3);
        }
    }
}
