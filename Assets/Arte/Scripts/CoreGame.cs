using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float diff = 3f;
    public float hueInc = 0.05f;
    public float upModifier = 0.05f;
    int height = 0;

    public int tileNumber = 0;

    public Transform root;
    public GameObject droppingTilePrefab;

    public Transform spawnerX, spawnerZ, limitX, limitZ;

    private float currentHue = 0;

    private float startDif;

    StackerCube currentTile;

    Vector3 startPos;

    void Start()
    {
        startDif = diff;
        startPos = transform.position;
        currentTile = CreateTile((int)Random.Range(0, 2));
    }

    public void Reset(){
        foreach(Transform t in root){
            Destroy(t.gameObject);
        }

        tileNumber = 0;
        currentTile = CreateTile((int)Random.Range(0, 2));
        currentHue = 0;
        diff = startDif;

        transform.position = startPos;
        
        height = 0;
    }

    StackerCube CreateTile(int dir){
        GameObject inst = Instantiate(droppingTilePrefab, new Vector3(1000,1000,1000), Quaternion.identity) as GameObject;
        inst.transform.parent = root;
        StackerCube cube = inst.GetComponent<StackerCube>();

        Vector3 from, to;

        if(dir == 0){
            // Use X
            from = spawnerX.position;
            to = limitX.position;
        } else {
            // Use Z
            from = spawnerZ.position;
            to = limitZ.position;
        }

        Debug.Log(cube);
        cube.StartMoving(from, to, diff, Color.HSVToRGB(currentHue % 1, 1, 1));
        Debug.Log("What");

        currentHue += hueInc;
        diff -= 0.025f;

        return cube;
    }

    void TryDropTile() {
        tileNumber++;
        currentTile.Drop();
        currentTile = null;

        transform.position += Vector3.up * upModifier;
        currentTile = CreateTile(tileNumber % 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touches.Length > 0){
            Touch t = Input.touches[0];
            if(t.phase == TouchPhase.Ended){
                TryDropTile();
            }
        }
    }
}
