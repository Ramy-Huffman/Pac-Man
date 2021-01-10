using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public Image[] lives;
    //public Sprite life;
    public FloatValue lifeTotal;
    public TextMeshProUGUI textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        InitLife();
    }

    private void Update()
    {
        
    }

    public void InitLife()
    {
        for (int i = 0; i < lifeTotal.runtimeValue; i++)
        {
            lives[i].gameObject.SetActive(true);

        }
    }

    public void updateLife()
    {
        InitLife();

        if (lifeTotal.runtimeValue == 2)
        {
            lives[2].gameObject.SetActive(false);
        }
        else if(lifeTotal.runtimeValue == 1)
        {
            for (int i = 1; i <= 2; i++)
            {
                lives[i].gameObject.SetActive(false);

            }
        }
        else
        {
            for (int i = 0; i <= 2; i++)
            {
                lives[i].gameObject.SetActive(false);
                GameOver();
            }
        }

    }

    public void GameOver()
    {
        if (lifeTotal.runtimeValue == 0)
        {
            textDisplay.gameObject.SetActive(true);
        }
    }
}
