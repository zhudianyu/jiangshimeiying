using UnityEngine;
using System.Collections;

public class CaissonCtronl : MonoBehaviour {

   public  PlayerAnimtionControl playControl;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        this.gameObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            playControl.reloadBullet();
            Destroy(this.gameObject);
        }
    }
}
