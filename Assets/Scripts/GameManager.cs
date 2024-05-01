using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] enemyPrefab;
    public GameObject[] SpawnPoints;
    public int playerScore = 0;
    public int playerHealth = 3;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private GameObject restartButton, exitButton;
    [SerializeField] private GameObject player , RightControlls;
    public float spawnTime;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        score.text = "Score: " + playerScore;
        health.text = "Health: " + playerHealth;

        if(playerScore > 10 && playerScore < 20)
        {
            spawnTime = 1.5f;
        }
        else if(playerScore > 20 && playerScore < 50)
        {
            spawnTime = 1f;
        }
        else if(playerScore > 50 && playerScore < 80)
        {
            spawnTime = 0.5f;
        }
        else if (playerScore > 80)
        {
            spawnTime = 0.1f;
        }

    }
    public void StartGame()
    {
        spawnTime = 2f;
        playerHealth = 3;
        playerScore = 0;
        StartCoroutine(SpawnAfterTime());
    }

    Vector3 GetRandomPosition()
    {
        int randomPosition = Random.Range(0, SpawnPoints.Length);
        return SpawnPoints[randomPosition].transform.position;
    }

    IEnumerator SpawnAfterTime()
    {
        if (playerHealth > 0)
        {
            yield return new WaitForSeconds(spawnTime);
            Vector3 randomPosition = GetRandomPosition();
            int randomEnemy = Random.Range(0, enemyPrefab.Length);
            GameObject obj = Instantiate(enemyPrefab[randomEnemy], new Vector3(randomPosition.x, 0, randomPosition.z), Quaternion.identity);
            obj.transform.localScale = obj.transform.localScale * 3;
            Destroy(obj , 2);

            Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.AddForce(directionToPlayer * 4, ForceMode.Impulse);
            rb.AddForce(Vector3.up * 6, ForceMode.Impulse);
            StartCoroutine(SpawnAfterTime());
        }
        else
        {
            restartButton.SetActive(true);
            exitButton.SetActive(true);
            RightControlls.GetComponent<XRInteractorLineVisual>().enabled = true;
        }
    }

    public void restart()
    {
        playerHealth = 3;
        StartCoroutine(SpawnAfterTime());
    }

    public void Exit()
    {
        Application.Quit();
    }


}
