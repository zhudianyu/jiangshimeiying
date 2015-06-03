
using UnityEngine;
using System.Collections;
public enum PlayerWeapon
{
    Pistol,//手枪
    Rifle,//步枪
    Launcher,//机关枪
    Heavy,//大炮
}
public class PlayerAnimtionControl : MonoBehaviour {

    public Animation playerAnimation;
    //按下键盘上下键时，返回的键值（-1--1）
    private float h;
    //按下键盘左右键时，返回的键值（-1--1）
    private int weaponIndex = 0;
    private float v;

    public GameObject[] weaponArray;
	// Use this for initialization
    public PlayerWeapon weapon = PlayerWeapon.Pistol;

    public float playerSpeed = 5.0f;

    public Rigidbody playerRigidBody;
	void Start ()
    {
         
	
	}
	private void PlayerMoveAnimation()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (v > 0.1f)//向前走
        {
            playerAnimation.CrossFade("Run_"+weapon.ToString(), 0.2f);//有缓冲
            // playerAnimation.Play("Run_Pistol");//直接转化状态 无缓冲

        }
        else if (v < -0.1f)//向后走
        {

            playerAnimation.CrossFade("Run_Backward_" + weapon.ToString(), 0.2f);
        }
        else if (h > 0.1f)
        {
            playerAnimation.CrossFade("Run_Right_" + weapon.ToString(), 0.2f);
        }
        else if (h < -0.1f)
        {
            playerAnimation.CrossFade("Run_Left_" + weapon.ToString(), 0.2f);
        }
        else
        {
            playerAnimation.CrossFade("Idle_" + weapon.ToString(), 0.2f);
        }
        //因为有时物体移动速度过快，会导致玩家直接穿越物体，（要实现碰撞，最好不用此方法）
       // this.gameObject.transform.Translate((new Vector3(h, 0, v)) * playerSpeed * Time.deltaTime, Space.Self);
        //相对于世界坐标系下的移动，不会出现穿越物体的情况
       // playerRigidBody.AddForce((new Vector3(h, 0, v)) * playerSpeed);
        //相对于局部坐标系下的移动，不会出现穿越物体的情况
        playerRigidBody.AddRelativeForce((new Vector3(h, 0, v)) * playerSpeed);
    }
    private void changeWeapon()
    {
         if(Input.GetKeyDown(KeyCode.Tab))
        {
            ++weaponIndex;
            if (weaponIndex == 4)
            {
                weaponIndex = 0;
            }
            if (weaponIndex == 0)
            {
                weapon = PlayerWeapon.Pistol;
            }
            else if (weaponIndex == 1)
            {
                weapon = PlayerWeapon.Rifle;
            }
            else if (weaponIndex == 2)
            {
                weapon = PlayerWeapon.Launcher;
            }
            else if (weaponIndex == 3)
            {
                weapon = PlayerWeapon.Heavy;
            }
            for(int i = 0;i<4;i++)
            {
                if(i == weaponIndex)
                {
                    weaponArray[i].SetActive(true);
                }
                else
                {
                    weaponArray[i].SetActive(false);
                }
                

            }
         }
    }
	// Update is called once per frame
	void Update ()
    {
        if (!(playerAnimation.IsPlaying("Fire_" + weapon.ToString())||Input.GetKeyDown(KeyCode.W)))
        {
            PlayerMoveAnimation();
            changeWeapon();
        }

        if(Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.W))
        {
            playerAnimation.CrossFade("Fire_" + weapon.ToString());
        }
       
	}
}
