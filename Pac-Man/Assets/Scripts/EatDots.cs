using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EatDots : MonoBehaviour
{
    public FloatValue points;
    public SignalSender signal;

    public BoolValue dots;
    public TextMeshProUGUI textDisplay;
    public Ghosts[] ghost;

    //public SignalSender dotSignal;

    // Start is called before the first frame update
    void Start()
    {
        //eaten = dots.RuntimeValue;

        /*        if (eaten)
                {
                    removeDot();
                }*/

        //InitDots();
        if (dots.RuntimeValue == true)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //GameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.CompareTag("BigDot"))
            {
                points.runtimeValue += 200;
            }
            points.runtimeValue += 50;
            signal.Raise();


            removeDot();
            
        }
    }

    public void removeDot()
    {
        //int i = System.Array.IndexOf(dots, gameObject);
        //dots[i].RuntimeValue = true;*/
        dots.RuntimeValue = true;
        //eaten = true;
        //dotSignal.Raise();
        Destroy(gameObject);

        GameOver();
    }

    public void GameOver()
    {
        if (points.runtimeValue > 12400)
        {
            textDisplay.gameObject.SetActive(true);
            for (int i = 0; i < ghost.Length; i++)
            {
                ghost[i].gameObject.SetActive(false);
            }
        }
    }
}
