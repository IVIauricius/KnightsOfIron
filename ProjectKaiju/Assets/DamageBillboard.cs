using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBillboard : BillBoard {

    public float life;
    float timer = 0f;
    public bool started = false;

    public override void Update()
    {
        // base.Update();
        GetComponentInParent<Transform>().LookAt(Camera.main.transform);
        if (started)
        {
            timer += Time.deltaTime;
            transform.position += transform.up * Time.deltaTime;
            if(timer >= life)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
