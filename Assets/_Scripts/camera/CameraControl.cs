using UnityEngine;
using System.Collections;
//和脊椎的算法一样
public class CameraControl : MonoBehaviour {

    public float sensitivyZ = 1f;

    private float spineRotion = 0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (spineRotion > 40)
        {
            spineRotion = 40;
        }
        if (spineRotion < -40)
        {
            spineRotion = -40;
        }
        this.gameObject.transform.localEulerAngles = new Vector3(-spineRotion,0,0 );
    }
}
