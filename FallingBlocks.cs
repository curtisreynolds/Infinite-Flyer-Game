
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    private Vector3[] blockSize;

    public Material[] material;

    public float fallingSpeed = 5f;

    public int enemyStage = 0;

    public bool isBuilding;
    private Material currentMaterial;

    private Color color;
    

    void Awake()
    {
        currentMaterial = GetComponent<Renderer>().material;

    }

    // Start is called before the first frame update
    void Start()
    {

        if (isBuilding)
        {
            blockSize = new Vector3[10];
            blockSize[0] = new Vector3(4, 20f, 4);
            blockSize[1] = new Vector3(4, 20f, 4);
            blockSize[2] = new Vector3(4, 20f, 4);
            blockSize[3] = new Vector3(4, 20f, 4);
            blockSize[4] = new Vector3(4, 20f, 4);
            blockSize[5] = new Vector3(4, 20f, 4);
            blockSize[6] = new Vector3(4, 20f, 4);
            blockSize[7] = new Vector3(4, 20f, 4);
            blockSize[8] = new Vector3(4, 20f, 4);
            blockSize[9] = new Vector3(4, 20f, 4);
        }
        else if (!isBuilding)
        {
            blockSize = new Vector3[1];
            blockSize[0] = new Vector3(24, Random.Range(4, 6), 3);
        }

        if (enemyStage < blockSize.Length)
        {
            transform.localScale = blockSize[enemyStage];
        }
        else
        {
            transform.localScale = blockSize[blockSize.Length -1];
        }
        if(enemyStage < material.Length)
        {
            GetComponent<Renderer>().material = material[enemyStage];
        }
        else
        {
            GetComponent<Renderer>().material = material[Random.Range(0,material.Length)];
        }


        if(enemyStage < 25)
        {
            fallingSpeed = (10 + (enemyStage + 5));
        }else if(enemyStage >= 25 && enemyStage < 40)
        {
            fallingSpeed = (10f + (25 + 5));
        }
        else if(enemyStage >= 40 && enemyStage < 50)
        {
            fallingSpeed = (10f + (40 + 5));
        }else if(enemyStage >= 50)
        {
            fallingSpeed = (10f + (enemyStage + 5));
        }

 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float activeSpeed = 1 * Time.deltaTime * -fallingSpeed;
        transform.Translate(activeSpeed * Vector3.forward);
    }
}
