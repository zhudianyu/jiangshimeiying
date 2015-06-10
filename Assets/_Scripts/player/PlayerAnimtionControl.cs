
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum PlayerWeapon
{
    Pistol,//手枪
    Rifle,//步枪
    Launcher,//机关枪
    Heavy,//大炮
}
/*
 玩家射击时不允许移动和跳跃，跳跃时，不允许射击和移动动画的播放
 */
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

    private RaycastHit hitinfo;//用射线检测是否跳起

    private bool isAllowCreateEffect = false;//是否允许创建粒子特效

    public GameObject pistolShootObject;//手枪射击粒子对象
    public Transform shootEffectParent;//手枪粒子对象父节点
    public GameObject hitedEffectObject;//手枪射中物体的粒子对象

    public GameObject launchShootObject;//机关枪射击粒子对象
    public Transform launchShootEffectParent;//机关枪粒子对象父节点
    public GameObject launchHitedEffectObject;//机关枪射中物体的粒子对象

    public GameObject rifeShootObject;//步枪射击粒子对象
    public Transform rifeShootEffectParent;//步枪粒子对象父节点
    public GameObject rifeHitedEffectObject;//步枪射中物体的粒子对象

    public GameObject heavyShootObject;//步枪射击粒子对象
    public Transform heavyShootEffectParent;//步枪粒子对象父节点
    public GameObject heavyHitedEffectObject;//步枪射中物体的粒子对象

    private RaycastHit effectHitInfo;//射击粒子特效射线
    public float upSpeeed = 100;
    private float rifeSque = 5f;//步枪射击频率
    private float heavySque = 5f;//大炮射击频率
    private float timer = 0f;
    private bool isAllowShooting = false;//是否连续射击

    public Sprite[] spriteArray;
    
    private int[] bulletNumArray = {20,40,5,30};

    private EnemyHpControl enemyHpCtrl;
    private EnemyCtronl enemyCtrl;
    private int[] weaponHurtArray = { 3, 2, 10, 3 };
    public Image bulletImage;
    public Text bulletNumText;
	void Start ()
    {
        enemyHpCtrl = GameObject.FindGameObjectWithTag(GlobalCtrol.ENEMY).GetComponent<EnemyHpControl>();
        enemyCtrl = GameObject.FindGameObjectWithTag(GlobalCtrol.ENEMY).GetComponent<EnemyCtronl>();

        setBulletImage(weaponIndex);
	
	}
    public void reloadBullet()
    {
        bulletNumArray[0] = 20;
        bulletNumArray[1] = 40;
        bulletNumArray[2] = 5;
        bulletNumArray[3] = 30;
        setBulletNum(weaponIndex);
    }
	private void PlayerMoveAnimation()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (!playerAnimation.IsPlaying("Fall_" + weapon.ToString()))
        {
            if (v > 0.1f)//向前走
            {
                playerAnimation.CrossFade("Run_" + weapon.ToString(), 0.2f);//有缓冲
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
        }
        //因为有时物体移动速度过快，会导致玩家直接穿越物体，（要实现碰撞，最好不用此方法）
       // this.gameObject.transform.Translate((new Vector3(h, 0, v)) * playerSpeed * Time.deltaTime, Space.Self);
        //相对于世界坐标系下的移动，不会出现穿越物体的情况
       // playerRigidBody.AddForce((new Vector3(h, 0, v)) * playerSpeed);
        //相对于局部坐标系下的移动，不会出现穿越物体的情况
        playerRigidBody.AddRelativeForce((new Vector3(h, 0, v)) * playerSpeed);
    }

    void setBulletImage(int index)
    {
        if(index<spriteArray.Length)
        {
            bulletImage.sprite = spriteArray[index];
            int value = (int)bulletNumArray[index];
            bulletNumText.text = value.ToString() ;
        }
       
    }
    void setBulletNum(int index)
    {
        if (!isHaveBullet(index))
            return;
        if (index < spriteArray.Length)
        {
            bulletNumArray[index]--;
            int value = (int)bulletNumArray[index];
            bulletNumText.text = value.ToString();
        }
    }
    bool isHaveBullet(int index)
    {
        if (index < spriteArray.Length)
        {
           
            int value = (int)bulletNumArray[index];
            if (value > 0)
                return true;
            else
                return false;
        }
        return false;
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
            setBulletImage(weaponIndex);
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
    void playerShoot()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W))
        {
            playerAnimation.CrossFade("Fire_" + weapon.ToString());
            isAllowCreateEffect = true;
            isAllowShooting = true;
        }
 
        else if (Input.GetMouseButtonUp(0))
        {
            
            isAllowShooting = false;
        }
        if(isAllowShooting)
        {
            timer += Time.deltaTime;
            if(weapon == PlayerWeapon.Rifle)
            {
               float fsq = 1 / rifeSque;
             
               if (timer > fsq)
                {
                    print("allow play isAllowCreateEffect ");
                    timer = 0;
                    isAllowCreateEffect = true;
               
                }
            }
            else if(weapon == PlayerWeapon.Heavy)
            {
                float fsq = 1 / heavySque;
                if (timer > fsq)
                {
                    timer = 0;
                    isAllowCreateEffect = true;
                   
                }
            }
           
        }
        else
        {
            playerAnimation.Stop("Fire_" + weapon.ToString());
           // isAllowCreateEffect = false;
        }
    }
    void playJump()
    {
        //当y轴上的速度大于0.1，认为在播放跳跃动画，反之停止
        //不能用0判断，因为y轴会有一点点速度
        //if(Mathf.Abs(playerRigidBody.velocity.y)>=0.1f)
     if(Physics.Raycast(this.gameObject.transform.position,-this.gameObject.transform.up,out hitinfo))
     {
         float dis = Vector3.Distance(this.gameObject.transform.position,hitinfo.point);
      //   Debug.Log(dis);
         if(dis>1)
         {
             playerAnimation.CrossFade("Fall_" + weapon.ToString(), 0.2f);
         }
         else
         {
             playerAnimation.Stop("Fall_" + weapon.ToString());
         }
     }
       
        //不能连续起跳
        if (!playerAnimation.IsPlaying("Fall_" + weapon.ToString()))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidBody.AddRelativeForce(Vector3.up * upSpeeed);

            }
        }
       
    }

    void setEnemyHp()
    {
        int hurtPoint = weaponHurtArray[weaponIndex];
        enemyHpCtrl.hited(hurtPoint);
        enemyCtrl.isFindePlayer = true;
    }
    void CreateEffect()
    {
        if(weapon == PlayerWeapon.Pistol)
        {

            setBulletNum(weaponIndex);
            if (!isHaveBullet(weaponIndex))
                return;
            GameObject.Instantiate(pistolShootObject, shootEffectParent.position, Quaternion.identity);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out effectHitInfo))
            {
                GameObject effObj = (GameObject)GameObject.Instantiate(hitedEffectObject, effectHitInfo.point, Quaternion.identity);
                setEnemyHp();
                Destroy(effObj, 0.5f);
            }
        }
        else if(weapon == PlayerWeapon.Launcher)
        {
            setBulletNum(weaponIndex);
            if (!isHaveBullet(weaponIndex))
                return;
            GameObject.Instantiate(launchShootObject, launchShootEffectParent.position, Quaternion.identity);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out effectHitInfo))
            {
                GameObject effObj = (GameObject)GameObject.Instantiate(launchHitedEffectObject, effectHitInfo.point, Quaternion.identity);
                setEnemyHp();
               
            }
        }
        else if(weapon == PlayerWeapon.Rifle)
        {
            setBulletNum(weaponIndex);
            if (!isHaveBullet(weaponIndex))
                return;
            GameObject.Instantiate(rifeShootObject, rifeShootEffectParent.position, Quaternion.identity);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out effectHitInfo))
            {
                GameObject effObj = (GameObject)GameObject.Instantiate(rifeHitedEffectObject, effectHitInfo.point, Quaternion.identity);
                setEnemyHp();
                Destroy(effObj, 0.5f);
            }
        }
        else if(weapon == PlayerWeapon.Heavy)
        {
            setBulletNum(weaponIndex);
            if (!isHaveBullet(weaponIndex))
                return;
            GameObject.Instantiate(heavyShootObject, heavyShootEffectParent.position, Quaternion.identity);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out effectHitInfo))
            {
                GameObject effObj = (GameObject)GameObject.Instantiate(heavyHitedEffectObject, effectHitInfo.point, Quaternion.identity);
                setEnemyHp();
                Destroy(effObj, 0.5f);
            }
        }
       
    }
	// Update is called once per frame
	void Update ()
    {
        if (playerAnimation.IsPlaying("Death_" + weapon.ToString()))
            return;
        //没有播放攻击动画时，可以进行移动和跳跃还有换枪
        if (!(playerAnimation.IsPlaying("Fire_" + weapon.ToString())||Input.GetKeyDown(KeyCode.W)))
        {
            PlayerMoveAnimation();
            changeWeapon();
            playJump();
            //播放完射击动画开始播放粒子
            if(isAllowCreateEffect)
            {
                isAllowCreateEffect = false;
                print("play effect");
                CreateEffect();
            }
          
        }
     if(isAllowCreateEffect)
     {
         isAllowCreateEffect = false;
         CreateEffect();
     }
        if (!playerAnimation.IsPlaying("Fall_" + weapon.ToString()))
        {
            playerShoot();
        }
       
	}
}
