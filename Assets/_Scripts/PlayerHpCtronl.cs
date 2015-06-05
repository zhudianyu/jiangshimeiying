using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerHpCtronl : MonoBehaviour {

    public GameObject hpObject;
    private Image hpImage;
    public float hp = 100f;
	// Use this for initialization
	void Start () {
        hpImage = hpObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hp > 100)
            hp = 100;
        else if (hp < 0)
            hp = 0;
        if (hp > 60)
            hpImage.color = Color.green;
        else if (hp > 30 && hp < 60)
            hpImage.color = Color.yellow;
        else
            hpImage.color = Color.red;
        hpImage.transform.localScale = new Vector3(hp / 100, 1, 1);
        
	}
}
