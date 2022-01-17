using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class character : MonoBehaviour
{

    private float speed = 20f;
    private float a;
    private float b;
    private float coin = 0;
    bool walk;
    int deathCounter;

    Transform trans;
    Vector3 move;
    Animator anim;
    Rigidbody rigid;
    public Text text;
    
    public AudioClip audioAttack;
    public AudioClip audioItem;
    AudioSource audioSource;

    private GameObject heart1;
    private GameObject heart2;
    private GameObject heart3;

    private GameObject gameOver;
    

    




    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();

        heart1 = GameObject.Find("Hp1");
        heart2 = GameObject.Find("Hp2");
        heart3 = GameObject.Find("Hp3");
        gameOver = GameObject.Find("GameOver");
    }


    void Update()
    {
        a = Input.GetAxisRaw("Horizontal");
        b = Input.GetAxisRaw("Vertical");

        move = new Vector3(a, 0, b).normalized;
        Vector3 getVel = new Vector3(a, 0, b) * speed;
        rigid.velocity = getVel;
        transform.LookAt(transform.position + move);


        walk = Input.GetButton("Walk");
        anim.SetBool("isRun", move != Vector3.zero);
        anim.SetBool("isWalk", walk);

        if(coin == 5)
        {
            text.text = "¿Ã¡¶ ≈ª√‚«œººø‰!";
        }

    }

    void FixedUpdate()
    {
        rigid.angularVelocity = Vector3.zero;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Debug.Log("»πµÊ!");
            Destroy(col.gameObject);
            coin += 1;
            Debug.Log("ƒ⁄¿Œ : " + coin.ToString());
            PlaySound("ITEM");
            col.gameObject.SetActive(false);
            audioSource.loop = false;
           

            if (coin == 5)
            {
                Debug.Log("¿Ã¡¶ ≈ª√‚«œººø‰!");
            }
          

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster")
        {
            Debug.Log("¥Í¿Ω!");
            transform.position = new Vector3(0, 0, 0);
            deathCounter += 1;
            PlaySound("ATTACK");
            audioSource.loop = false;


        }

        if (deathCounter == 1)
        {
            Destroy(heart3);
        }

        if (deathCounter == 2)
        {
            Destroy(heart2);
        }

        if (deathCounter == 3)
        {
            Destroy(heart1);
            SceneManager.LoadScene("GameOver");
        }




        if (col.gameObject.tag == "Finish")
        {
            if (coin < 5)
            {
                Debug.Log("ƒ⁄¿Œ¿Ã ∫Œ¡∑«’¥œ¥Á");
            }
            else
            {
                Debug.Log("≈ª√‚!");
                SceneManager.LoadScene("ClearScene");
            }
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
        }
        audioSource.Play();
    }

    
}
