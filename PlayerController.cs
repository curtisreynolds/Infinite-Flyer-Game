using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject gameOver;

    public Animator animator;

    public float playerSpeed = 100f;

    public bool canMoveVertical = true;
    public bool canMoveHorizontal = true;
    public bool gameStarted = false;
    public bool invertedVertical;

    public float upperBounds;
    public float bottomBounds;
    public float leftBounds;
    public float rightBounds;

    
    private bool hasDied = false;

    private void Awake()
    {
        ParticleSystem[] allChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem child in allChildren)
        {
            child.Stop();
        }
        if (PlayerPrefs.GetInt("InvertControls") !=1)
        {
            invertedVertical = false;
        }
        else
        {
            invertedVertical = true;
        }
        
    }
    void Update()
    {
        AnimationController();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        ///controls horizontal movment
        ///checks current position against maximum bounds
        if (canMoveHorizontal && !GameManager.hasLost && gameStarted)
        {
            if(transform.position.x > leftBounds && transform.position.x < rightBounds)
            {
                float horizontalVelocity = Input.GetAxis("Horizontal") * Time.deltaTime * -playerSpeed;
                transform.Translate(Vector3.left * horizontalVelocity, Space.World);
            }
            else if(transform.position.x <= leftBounds && Input.GetAxis("Horizontal") >= 0)
            {
                float horizontalVelocity = Input.GetAxis("Horizontal") * Time.deltaTime * -playerSpeed;
                transform.Translate(Vector3.left * horizontalVelocity, Space.World);
            }
            else if (transform.position.x >= rightBounds && Input.GetAxis("Horizontal") <= 0)
            {
                float horizontalVelocity = Input.GetAxis("Horizontal") * Time.deltaTime * -playerSpeed;
                transform.Translate(Vector3.left * horizontalVelocity, Space.World);

            }
            
        }



        //controls vertical movment
        //checks current position against maximum bounds
        
        if (canMoveVertical && !GameManager.hasLost && gameStarted && !invertedVertical)
        {
            if (transform.position.y > bottomBounds && transform.position.y < upperBounds)
            {
                float verticalvelocity = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
                transform.Translate(Vector3.up * verticalvelocity, Space.World);

            }
            else if(transform.position.y <= bottomBounds && Input.GetAxis("Vertical") >= 0)
            {
                float verticalvelocity = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
                transform.Translate(Vector3.up * verticalvelocity, Space.World);

            }
            else if (transform.position.y >= upperBounds && Input.GetAxis("Vertical") <= 0)
            {
                float verticalvelocity = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
                transform.Translate(Vector3.up * verticalvelocity, Space.World);
            }

            //controls vertical movment with inverted controls
            //checks current position against maximum bounds
        }
        else if(canMoveVertical && !GameManager.hasLost && gameStarted && invertedVertical)
        {
            if (transform.position.y > bottomBounds && transform.position.y < upperBounds)
            {
                float verticalvelocity = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
                transform.Translate(Vector3.up * -verticalvelocity, Space.World);

            }
            else if (transform.position.y <= bottomBounds && Input.GetAxis("Vertical") <= 0)
            {
                float verticalvelocity = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
                transform.Translate(Vector3.up * -verticalvelocity, Space.World);

            }
            else if (transform.position.y >= upperBounds && Input.GetAxis("Vertical") >= 0)
            {
                float verticalvelocity = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
                transform.Translate(Vector3.up * -verticalvelocity, Space.World);
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && !hasDied)
        {
            ParticleSystem[] allChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem child in allChildren)
            {
                child.Play();
            }
            gameManager.GetComponent<GameManager>().HasLost();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            hasDied = true;
            Light [] allchildren = gameObject.GetComponentsInChildren<Light>();
            foreach(Light Lights in allchildren)
            {
                Lights.enabled = false;
            }
        }
        
    }
    //sets the animation triggers based on the player input and settings
    private void AnimationController()
    {
        if (gameStarted) {
            animator.SetBool("gameStarted", true);
        }
        if (hasDied)
        {
            animator.SetBool("isDead", true);
        }
        else
        {
            animator.SetBool("isDead", false);
        }
        if (!gameStarted)
        {
            animator.SetBool("isInverted", invertedVertical);
        }
        if (gameStarted)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetBool("HoldingRight", true);
            }
            else
            {
                animator.SetBool("HoldingRight", false);

            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetBool("HoldingLeft", true);
            }
            else
            {
                animator.SetBool("HoldingLeft", false);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("HoldingUp", true);
            }
            else
            {
                animator.SetBool("HoldingUp", false);

            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("HoldingDown", true);
            }
            else
            {
                animator.SetBool("HoldingDown", false);
            }
        }
        
    }

    public void ReloadPlayer()
    {
        transform.position = new Vector3(0f, 5f, 0f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        hasDied = false;
        Light[] allchildren = gameObject.GetComponentsInChildren<Light>();
        foreach (Light Lights in allchildren)
        {
            Lights.enabled = true;
        }
    }
}

