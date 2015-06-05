using UnityEngine;
using System.Collections;
//控制玩家的旋转 随着mouse x 旋转轴为Y轴
public class PlayerRotateX : MonoBehaviour {

    public float sensitivy = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   this.gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")*sensitivy,0));
	}
}
