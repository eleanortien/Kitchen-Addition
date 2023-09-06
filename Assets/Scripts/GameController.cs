using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
      //DontDestroyOnLoad(transform.root.gameObject);
    }
    public GameObject sushiTargetPrefab;
    public GameObject pizzaTargetPrefab;
    

    public int score;

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    private int currentWaveNumber;
    public int targetIncreasePerWave;
    private int targetMax;
    private int targetsSpawned = 0;
    private int currentTargetNumber = 0;

    public float minSpawnTime;
    public float maxSpawnTime;

    public float screenTopCoordinate = 3.49f;
    public float maxLeftPoint;
    public float maxRightPoint;
    public float targetSpeed = -3f;

    private bool hasLost;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;
    public TextMeshProUGUI finalScoreText;

    


    private float height;

    // Start is called before the first frame update
    void Start()
    {
     gameOverUI.SetActive(false);
        currentHealth = maxHealth;
        currentWaveNumber = 0;
        hasLost = false;
        height = sushiTargetPrefab.GetComponent<SpriteRenderer>().sprite.rect.height;
        scoreText.text = "0";
        WaveChange();
    }

    // Update is called once per frame
    void Update()
    {
        //Invincible Timer
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    public void SpawnTarget ()
    {
        //CancelInvoke(); 
        if (targetsSpawned < targetMax && hasLost == false)
        {
            currentTargetNumber += 1;
            targetsSpawned += 1;
            int targetType = Random.Range(1, 4);
            if (targetType == 1 || targetType == 2)
            {
                Instantiate(sushiTargetPrefab, new Vector3(Random.Range(maxLeftPoint, maxRightPoint),
                    screenTopCoordinate + 1, 0), Quaternion.Euler(0, 0, 0));
            }
            else if (targetType == 3)
            {
                Instantiate(pizzaTargetPrefab, new Vector3(Random.Range(maxLeftPoint, maxRightPoint),
                    screenTopCoordinate + 1, 0), Quaternion.Euler(0, 0, 0));
            }

            // Start a new timer for the next random spawn
            Invoke("SpawnTarget", Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    public void WaveChange()
    {
        if (hasLost == false)
        {
  
            currentWaveNumber += 1;
            targetMax = currentWaveNumber * targetIncreasePerWave;
            targetsSpawned = 0;
            Invoke("SpawnTarget", 2);
            if (maxSpawnTime > minSpawnTime)
            {
                maxSpawnTime -= 0.5f;
            }
            targetSpeed += -1.5f;
        }
    }

    public void DecreaseTargetNumber ()
    {
        currentTargetNumber -= 1;
        if (currentTargetNumber <= 0 && targetsSpawned == targetMax)
        {
            WaveChange();
        }
    }

    public void ScoreChange (int scoreChange)
    {
        score += scoreChange;
        scoreText.text = (score.ToString());
    }

    public void ChangeHealth (int healthChange)
    {
        if (healthChange < 0)
        {
            if (isInvincible)
            {
                return;
            }

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + healthChange, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver ()
    {
        Debug.Log("HealthOut");
        hasLost = true;
        ShooterController shooterState = FindObjectOfType<ShooterController>();
        shooterState.currentState = ShooterState.shoot;
        finalScoreText.text = "Score: " + score.ToString();
        gameOverUI.SetActive(true);

        PlayerPrefs.SetString("Highscore", score.ToString());
        HighscoreTable.instance.AddHighscoreEntry(score, PlayerPrefs.GetString("PlayerName"));
        
        
        score = 0;
        //End Sequence
    }
}
