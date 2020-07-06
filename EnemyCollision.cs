using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.hasLost)
        {
            Destroy(other.gameObject);
            GameManager.Score += 1;
        }
        else
        {
            Destroy(other.gameObject);
        }
        
        
    }
}
