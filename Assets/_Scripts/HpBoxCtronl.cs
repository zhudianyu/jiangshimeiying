using UnityEngine;
using System.Collections;

public class HpBoxCtronl : MonoBehaviour {

    public PlayerHpCtronl hpControl;
    private float currentHp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
        hpControl.hp = Mathf.Lerp(hpControl.hp, currentHp + 30, 1f*Time.deltaTime);
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            currentHp = hpControl.hp;
            Destroy(gameObject.GetComponent<MeshRenderer>());
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(this.gameObject, 3);
        }
    }
}
