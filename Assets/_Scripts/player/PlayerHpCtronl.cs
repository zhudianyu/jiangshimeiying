using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHpCtronl : MonoBehaviour {

    public GameObject hpObject;
    private Image hpImage;
    public float hp = 20;
    public  Animation playerAnimation;
    public PlayerAnimtionControl playCtrol;
    public PlayerRotateX playRotateScr;
    public PlayerSpineControl playSpineScr;
    public Image panelImage;
	// Use this for initialization
	void Start () {
        hpImage = hpObject.GetComponent<Image>();
	}
	void StartGameOver()
    {
        GlobalCtrol.gameState = GameState.GameOver;
        playRotateScr.enabled = false;
        playSpineScr.enabled = false;
        playerAnimation.CrossFade("Death_" + playCtrol.weapon.ToString(),0.2f);
       
    }
    public void hited(int hurtValue)
    {
        hp -= hurtValue;
        ShakeCamera.shakeCamera();
    }
    void changeScene()
    {
        Application.LoadLevel("StartScene");
    }
	// Update is called once per frame
	void Update () {
        if (hp > 100)
        {
            hp = 100;
        }
        else if (hp <= 0)
        {
            hp = 0;
            StartGameOver();
            panelImage.color = Color.Lerp(panelImage.color, Color.red, Time.deltaTime * 0.3f);
            Invoke("changeScene", 3f);

        }
           
        if (hp > 60)
            hpImage.color = Color.green;
        else if (hp > 30 && hp < 60)
            hpImage.color = Color.yellow;
        else
            hpImage.color = Color.red;
        hpImage.transform.localScale = new Vector3(hp / 100, 1, 1);
        
	}
}
