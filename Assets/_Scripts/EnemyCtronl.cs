using UnityEngine;
using System.Collections;


public class EnemyCtronl : MonoBehaviour {
    public NavMeshAgent meshAgent;

    public Transform[] wayArray;
    private int wayPointIndex = 0;

    private float timer = 0;
	// Use this for initialization
	void Start () {

        meshAgent.destination = wayArray[wayPointIndex].position;
	}
	
	// Update is called once per frame
	void Update () {
	    if(meshAgent.remainingDistance < 0.5f)
        {
            timer += Time.deltaTime;
            if(timer>3f)
            {
                timer = 0;
            }
            wayPointIndex++;
            if(wayPointIndex >= wayArray.Length)
            {
                wayPointIndex = 0;

            }
            meshAgent.destination = wayArray[wayPointIndex].position;
        }
	}
}
