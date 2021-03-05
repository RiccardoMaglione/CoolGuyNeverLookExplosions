using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 160f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Speed * Time.deltaTime * v);      //Muove il player sull'asse z
        transform.Translate(Vector3.right * Speed * Time.deltaTime * h);        //Muove il player sull'asse x
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")                               //Se l'altro oggetto ha il tag Bullet
        {
            SceneManager.LoadScene("GameOver");                                 //Cambio scena in gameover
        }
    }
}