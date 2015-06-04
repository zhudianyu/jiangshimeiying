using UnityEngine;
using System.Collections;
//控制人物的抬头低头动作，主要控制脊椎 绕z轴旋转
public class PlayerSpineControl : MonoBehaviour {
    public float sensitivyZ = 10f;

    private float spineRotion = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //发现无法控制 
        //因为脊椎本身有动画  就是在update里面做的 所以把rotate动作给覆盖掉  
//        this.gameObject.transform.Rotate(new Vector3(0, 0, Input.GetAxis("Mouse Y") * sensitivyZ));
	}
    void LateUpdate()
    {
        //动画对脊椎的控制是在update里面的，所以我们要在LateUpdate里面控制，把动画对脊椎的控制给覆盖掉，实现我们自己的rotation
       // this.gameObject.transform.Rotate(new Vector3(0, 0, Input.GetAxis("Mouse Y") * sensitivyZ));
        //上面这种方法  虽然有控制 但是当鼠标停止时 Input.GetAxis("Mouse Y")  此时返回值为0  所以还是无法实现
        spineRotion += Input.GetAxis("Mouse Y") * sensitivyZ;
        if(spineRotion>60)
        {
            spineRotion = 60;
        }
        if(spineRotion<-60)
        {
            spineRotion = -60;
        }
        this.gameObject.transform.localEulerAngles = new Vector3(0, 0, spineRotion);
    }
}
