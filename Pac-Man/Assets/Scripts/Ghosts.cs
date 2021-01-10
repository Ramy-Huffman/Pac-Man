using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostState
{
    move,
    stagger,
    retreat
}

public class Ghosts : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator anim;
    public GhostState currentState;
    public float speed;
    //public Transform target;
    public Transform[] decisionPoints;
    private int currentPt = 0;
    public bool canBeEaten = false;
    private bool escape = false;

    // Start is called before the first frame update
    void Start()
    {
        // Updates the framerate of the application
        Application.targetFrameRate = 60;

        currentState = GhostState.move;
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        anim.SetFloat("moveX", 1);
        anim.SetFloat("moveY", 0);
    }

    // Update is called once per frame
    void Update()
    {
        TargetPlayer();
        Staggering();
        Retreat();
        movingBackward();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void SetAnimFloat (Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void changeAnimation (Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if(direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if(direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    private void movingForward()
    {
        if (!escape)
        {
            if (transform.position != decisionPoints[currentPt].position)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, decisionPoints[currentPt].position, speed * Time.deltaTime);
                changeAnimation(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
            else
            {
                currentPt = (currentPt + 1) % decisionPoints.Length;
            }
        }
    }

    private void movingBackward()
    {
        if (escape)
        {
            if (transform.position != decisionPoints[currentPt].position)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, decisionPoints[currentPt].position, speed * Time.deltaTime);
                changeAnimation(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
            else if (currentPt == 0)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, decisionPoints[currentPt].position, speed * Time.deltaTime);
                changeAnimation(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
            else
            {
                currentPt = (currentPt - 1) % decisionPoints.Length;
            }
        }

    }

    private void TargetPlayer()
    {
        if (currentState == GhostState.move)
        {
            movingForward();
        }
    }


    private void Staggering()
    {
        if(currentState == GhostState.stagger)
        {
            canBeEaten = true;
            movingForward();
            StartCoroutine(StaggeringCo());
            currentState = GhostState.move;
        }
    }

    private IEnumerator StaggeringCo()
    {
        speed = 2;
        anim.SetBool("scared", true);
        yield return new WaitForSeconds(4f);
        anim.SetBool("scared", false);
        canBeEaten = false;
        speed = 10;
        
    }

    private void Retreat()
    {
        if(currentState == GhostState.retreat)
        {
            escape = true;
            StartCoroutine(RetreatCo());
            currentState = GhostState.move;
        }
    }

    private IEnumerator RetreatCo()
    {
        speed = 100;
        anim.SetBool("retreat", true);
        yield return new WaitForSeconds(4f);
        anim.SetBool("retreat", false);
        speed = 10;
        escape = false;
        currentPt = 0;
    }


}
