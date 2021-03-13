using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text winText;
    public Text loseText;
    private int lifeValue = 3;
    public Text Lives;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        Lives.text = "Lives: " + lifeValue.ToString();
        loseText.text="";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
        Application.Quit();
        }

    }
    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector3(44.12f , 0.0f, 0.0f);
                lifeValue = 3;
                Lives.text = "Lives: " + lifeValue.ToString();
            }

            if (scoreValue >=8)
            {
                musicSource.Stop();
		        musicSource.clip = musicClipTwo;
		        musicSource.Play();
                winText.text = "You win! Game created by Victoria";
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            lifeValue = lifeValue - 1;
            Lives.text = "Lives: " + lifeValue.ToString();
            Destroy(collision.collider.gameObject);
            if (lifeValue ==0)
            {
                loseText.text = "Game Over!";
                Destroy(this);
            }

        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
