using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text ScoreText;
    public float Score;

    void Update()
    {
        if(ScoreText != null)
            ScoreText.text = "Score: " + Score.ToString();
    }

    /// <summary>
    /// Metodo richiamato nella distruzione della torretta per aumentare lo score
    /// </summary>
    /// <param name="ValueScore"></param>
    public void GainScore(float ValueScore)
    {
        Score += ValueScore;
    }
}
