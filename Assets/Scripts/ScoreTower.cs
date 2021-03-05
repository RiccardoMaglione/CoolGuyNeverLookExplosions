using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTower : MonoBehaviour
{
    public float ScoreValueTower;
    public ScoreSystem ScoreSys;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")       //Se l'altro oggetto ha il tag Bullet
        {
            ScoreSys.GainScore(ScoreValueTower);        //Richiamo il metodo per guadagnare lo score
            Destroy(collision.gameObject);              
            Destroy(this.gameObject);
        }
    }
}
