using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    public Vector2 softLimit, hardLimit;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // apply hard limit
        Vector3 pos = Vector3.forward*-10;
        
        //print(Mathf.Clamp(transform.position.x, -hardLimit.x + target.position.x, hardLimit.x + target.position.x));
        pos.x = Mathf.Clamp(transform.position.x, -hardLimit.x + target.position.x, hardLimit.x + target.position.x);
        pos.y = Mathf.Clamp(transform.position.y, -hardLimit.y + target.position.y, hardLimit.y + target.position.y);

        transform.position = pos;

        // apply soft limit movement
        Vector2 dif = target.position - pos;
        transform.Translate(dif * Time.deltaTime * speed);
    }
}
