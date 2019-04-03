using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public GameObject ball;
    public static bool isPaused = true;
    private Vector3 ballSpeed;

    public int player1Score = 0;
    public int player2Score = 0;

    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text scoreText;

    private bool scoreTextFading = false;
    private float initialSpeed = 200.0f;
    private float maxSpeed = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        player1ScoreText.text = "Player 1: " + player1Score.ToString();
        player2ScoreText.text = "Player 2: " + player2Score.ToString();
        scoreText.text = "Scored!";
        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 0.0f);
    }

    void Update()
    {
        ball.transform.position = new Vector3(ball.transform.position.x, 6.0f, ball.transform.position.z);
    }

    int getRandomDirection()
    {
        return (Random.Range(-1.0f, 1.0f) > 0.0f) ? 1 : -1;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                ballSpeed = ball.GetComponent<Rigidbody>().velocity;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            } else
            {
                if (ballSpeed != Vector3.zero)
                {
                    ball.GetComponent<Rigidbody>().velocity = ballSpeed;
                } else
                {
                    ball.GetComponent<Rigidbody>().velocity = new Vector3(getRandomDirection() * (Random.Range(3.0f, 16.0f)) * 10.0f,
                        0.0f, getRandomDirection() * Random.Range(5.0f, 10.0f)).normalized * initialSpeed;
                }
            }
        }

        if (scoreTextFading && scoreText.color.a > 0.0f)
        {
            var alphaValue = scoreText.color.a - 0.05f > 0.0f ? scoreText.color.a - 0.01f : 0.0f;
            if (alphaValue == 0.0f)
            {
                scoreTextFading = false;
            }

            scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, alphaValue);
        }
    }

    void resetGame()
    {
        ball.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
        ball.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        ballSpeed = Vector3.zero;
        ball.GetComponent<Rigidbody>().velocity = ballSpeed;
        isPaused = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "RailLeft")
        {
            player2Score += 1;
            player2ScoreText.text = "Player 2: " + player2Score.ToString();
            animateScoreText();
            resetGame();
        }

        if (col.gameObject.name == "RailRight")
        {
            player1Score += 1;
            player1ScoreText.text = "Player 1: " + player1Score.ToString();
            animateScoreText();
            resetGame();
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (ball.GetComponent<Rigidbody>().velocity.magnitude < maxSpeed &&
            (col.gameObject.name == "Player1" || col.gameObject.name == "Player2"))
        {
            ball.GetComponent<Rigidbody>().velocity = ball.GetComponent<Rigidbody>().velocity * 1.05f;
        }
    }

    void animateScoreText()
    {
        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 1.0f);
        scoreTextFading = true;
    }
}
