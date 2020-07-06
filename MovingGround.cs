using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    // Start is called before the first frame update

    public float MoveY;
    public float MoveTextureSpeedY;
   

    public GameObject gameManager;

    private MeshRenderer rend;

    private int currentscore;
    private float blockSpeed;
    private int enemyStage;
    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
       
    }
    private void Update()
    {
        currentscore = gameManager.GetComponent<GameManager>().OpenScore;
        enemyStage = currentscore / 10;

        if (enemyStage < 25)
        {
            blockSpeed = (10 + (enemyStage + 5));
        }
        else if (enemyStage >= 25 && enemyStage < 40)
        {
            blockSpeed = (10f + (25 + 5));
        }
        else if (enemyStage >= 40 && enemyStage < 50)
        {
            blockSpeed = (10f + (40 + 5));
        }
        else if (enemyStage >= 50)
        {
            blockSpeed = (10f + (enemyStage + 5));
        }

        MoveY = (((blockSpeed/5)*-1) * Time.deltaTime) + MoveY;
        if(MoveY < -1){
            MoveY = 0;
        }
        rend.material.SetTextureOffset("_BaseMap", new Vector2(0f, MoveY));
    }
}
