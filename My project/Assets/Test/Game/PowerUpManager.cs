using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public Transform spawnArea;
    public Vector2 powerUpAreaMin;
    public Vector2 powerUpAreaMax;

    public float spawnInterval;
    public int deSpawnInterval;
    public int maxPowerUpAmount;
    public List<GameObject> powerUpTemplateList;

    private List<GameObject> powerUpList;
    private float timer;
    private float lastExecute;

    private void Start() {
        powerUpList = new List<GameObject>();
        timer = 0;
        lastExecute = 0;
    }

    private void Update() {
        timer+= Time.deltaTime;

        if (timer - lastExecute >= spawnInterval) {
            GenerateRandomPowerUp();
            lastExecute = timer;
        }

        if (timer > deSpawnInterval) {
            if (powerUpList.Count > 0) {
                RemovePowerUp(powerUpList[0]);
            }
            timer -= deSpawnInterval;
            lastExecute = 0;
        }
    }

    public void GenerateRandomPowerUp() {
        GenerateRandomPowerUp(new Vector2(Random.Range(powerUpAreaMin.x, powerUpAreaMax.x), Random.Range(powerUpAreaMin.y, powerUpAreaMax.y)));
    }

    public void GenerateRandomPowerUp(Vector2 position) {
        if (powerUpList.Count >= maxPowerUpAmount)
        {
            return;
        }

        if (position.x < powerUpAreaMin.x ||
            position.x > powerUpAreaMax.x ||
            position.y < powerUpAreaMin.y ||
            position.y > powerUpAreaMax.y)
        {
            return;
        }
        
        int randomIndex = Random.Range(0, powerUpTemplateList.Count);

        GameObject powerUp = Instantiate(powerUpTemplateList[randomIndex], new Vector3(position.x, position.y, powerUpTemplateList[randomIndex].transform.position.z), Quaternion.identity, spawnArea);
        powerUp.SetActive(true);

        powerUpList.Add(powerUp);
    }

    public void RemovePowerUp(GameObject powerUp) {
        powerUpList.Remove(powerUp);
        Destroy(powerUp);
    }

    public void RemoveAllPowerUp() {
        while (powerUpList.Count > 0) {
            RemovePowerUp(powerUpList[0]);
        }
    }
}
