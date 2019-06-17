using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

	// Update is called once per frame
	public virtual void Update ()
    {
        transform.LookAt(Camera.main.transform);
	}
}
