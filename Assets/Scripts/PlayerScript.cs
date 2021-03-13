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

    public Text livesText;

    private int Lives = 3;

    public AudioClip BackgroundMusic;

    public AudioClip WinMusic;

    public AudioSource musicSource; 

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        livesText.text = Lives.ToString();
        musicSource.clip = BackgroundMusic;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
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
                transform.position = new Vector3(74.0f, 1.0f, 0.0f);
                Lives = 3;
                livesText.text = Lives.ToString();
            }
        }
        if (scoreValue >= 8)
        {
            winText.text = "You win! Game created by James Acosta.";
            musicSource.Stop();
            musicSource.clip = WinMusic;
            musicSource.Play();
            musicSource.loop = true;
        }
        if (collision.collider.tag == "Enemy")
        {
            Lives -= 1;
            livesText.text = Lives.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (Lives == 0)
        {
            winText.text = "You Lose! Game created by James Acosta.";
            Destroy(this.gameObject);
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