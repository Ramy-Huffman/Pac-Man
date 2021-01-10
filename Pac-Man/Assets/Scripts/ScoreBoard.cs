using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public FloatValue points;
    public FloatValue ghostPoints;
    public TextMeshProUGUI pointsDisplay;

    public void Start()
    {
        updateScore();
    }

    public void updateScore()
    {
        pointsDisplay.text = "" + (points.runtimeValue + ghostPoints.runtimeValue);     
    }
}
