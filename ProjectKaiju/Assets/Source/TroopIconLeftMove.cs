using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopIconLeftMove : MonoBehaviour
{

    public Transform front, back;
    public bool movingF, movingB;
    public float travelTime = 1f;
    public bool isForward = true;
    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    float timer = 0f;
	void Update ()
    {
        if(movingF)
        {

           

            timer += Time.deltaTime / travelTime;
            transform.position = Vector3.Lerp(back.position, front.position, timer);
            if (timer >= 1f)
            {
                timer = 0f;
                movingF = false;
                isForward = true;
            }
        }
         if (movingB)
        {
            

            timer += Time.deltaTime / travelTime;
            transform.position = Vector3.Lerp(front.position, back.position, timer);
            if (timer >= 1f)
            {
                timer = 0f;
                movingB = false;
                isForward = false;
            }

        }
        //print(transform.position);
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    print("Hit Up!");
        //    transform.position = new Vector3(240, 570, 0);

        //}
        //else if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    print("Hit Down!");
        //    transform.position = new Vector3(240, 510, 0);

        //}
	}
}
