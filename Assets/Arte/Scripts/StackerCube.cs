using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackerCube : MonoBehaviour
{
    public Vector3 from, to;
    float time = 5;

    public Shader sha;

    bool moving = false;

    bool going = false;

    public bool dropping = false;

    Rigidbody rb;

    MeshRenderer rend;
    public void StartMoving(Vector3 from, Vector3 to, float time, Color col){
        rend = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        this.from = from;
        this.to = to;
        this.time = time;

        Debug.Log("ASSIGNED!!!");

        rend.sharedMaterial.color = col;

        rend.enabled = true;
        moving = true;

        Debug.Log("AAA");
    }

    public void Drop(){
        Debug.Log("DROPPIN!");

        dropping = true;

        Debug.Log(dropping);

        rb.useGravity = true;
    }

    // Update is called once per frame
    float f = 0;

    void Update()
    {
        if(!dropping){
            if(!going){
                if(f < 1){
                    f += Time.deltaTime / time;
                } else {
                    going = !going;
                }
            } else {
                if(f > 0){
                    f -= Time.deltaTime / time;
                } else {
                    going = !going;
                }
            }

            Debug.Log(from + " " + to);
            Debug.Log(f);

            Vector3 desiredPos = Vector3.Lerp(from, to, f);
            Debug.Log(desiredPos);
            transform.position = desiredPos;
        }
    }
}
