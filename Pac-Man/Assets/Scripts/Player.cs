using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Different Player States
public enum PlayerState
{
    move,
    stagger,
    idle
}

public class Player : MonoBehaviour
{
    private PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator anim;
    public FloatValue life;
    public SignalSender lifeSignal;
    public Ghosts[] ghost;
    public FloatValue points;
    public FloatValue ghostPoints;
    public SignalSender signal;
    //public SignalSender gameOver;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startDelayCo());
        // Updates the framerate of the application
        Application.targetFrameRate = 60;

        currentState = PlayerState.move;
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        anim.SetFloat("moveX", 1);
        anim.SetFloat("moveY", 0);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (currentState == PlayerState.move || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    // Method to move player
    void MovePlayer()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    // Method to update animation when moving
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MovePlayer();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            if (other.CompareTag("Enemy") && ghost[i].canBeEaten == false && currentState != PlayerState.stagger)
            {
                currentState = PlayerState.stagger;
                deathEffect();

                if (life.runtimeValue > 0)
                {

                    life.runtimeValue -= 1;
                    lifeSignal.Raise();
                }
            }

            if (other.CompareTag("Enemy") && ghost[i].canBeEaten == true)
            {
                ghostPoints.runtimeValue += 500;
                signal.Raise();
                if (other.gameObject.name == "Blinky")
                {
                    other.GetComponent<Ghosts>().currentState = GhostState.retreat;
                    break;
                }
                else if (other.gameObject.name == "Speedy")
                {
                    other.GetComponent<Ghosts>().currentState = GhostState.retreat;
                    break;
                }
                else if (other.gameObject.name == "Inky")
                {
                    other.GetComponent<Ghosts>().currentState = GhostState.retreat;
                    break;
                }
                else if (other.gameObject.name == "Clyde")
                {
                    other.GetComponent<Ghosts>().currentState = GhostState.retreat;
                    break;
                }
            }

            if (other.CompareTag("BigDot"))
            {
                ghost[i].currentState = GhostState.stagger;
            }

        }


  


    }

    private void deathEffect()
    {
        StartCoroutine(deathCo());
    }

    private IEnumerator deathCo()
    {
        anim.SetBool("death", true);
        yield return null;
        anim.SetBool("death", false);
        yield return new WaitForSeconds(4.5f);

        if (life.runtimeValue > 0)
        {
            SceneManager.LoadScene("Game");

        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private IEnumerator startDelayCo()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3f;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return null;
        }
        Time.timeScale = 1;
    }
}
