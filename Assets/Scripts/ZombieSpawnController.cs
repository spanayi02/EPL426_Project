using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;


public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.9f; // Delay between spawning each zombie in a wave;

    public int currentWave = 0;
    public float waveCooldown = 10.0f; // Time in seconds between waves;

    public bool inCooldown;
    public float cooldownCounter = 0; // We only use this for testing and the UI;

    public List<Zombie> currentZombiesAlive;
    public GameObject zombiePrefab;
    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCounterUI;
    public TextMeshProUGUI currentWaveUI;

    private void Start()
{
    currentZombiesPerWave = initialZombiesPerWave;
        GlobalReferences.Instance.waveNumber = 0;
        StartNextWave();
}

private void StartNextWave()
{
    currentZombiesAlive.Clear();

    currentWave++;
        GlobalReferences.Instance.waveNumber = currentWave;
        currentWaveUI.text = "Wave" + currentWave.ToString();

    StartCoroutine(SpawnWave());
}

private IEnumerator SpawnWave()
{
    for (int i=0;i<currentZombiesPerWave;i++)
    {
        Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        Vector3 spawnPosition =transform.position + spawnOffset;

        var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

        Zombie enemyScript = zombie.GetComponent<Zombie>();

        //track zombie
        currentZombiesAlive.Add(enemyScript);

        yield return new WaitForSeconds(spawnDelay);

    }
}

    private void Update() { 

// Get all dead zombies
List<Zombie> zombiesToRemove = new List<Zombie>();
foreach (Zombie zombie in currentZombiesAlive)
{
    if (zombie.isDead)
    {
        zombiesToRemove.Add(zombie);
    }
}

// Actually remove all dead zombies
foreach (Zombie zombie in zombiesToRemove)
{
    currentZombiesAlive.Remove(zombie);
}

zombiesToRemove.Clear();

// Start Cooldown if all zombies are dead
if (currentZombiesAlive.Count == 0 && inCooldown == false)
{
    // Start cooldown for next wave
    StartCoroutine(WaveCooldown());
}

// Run the cooldown counter
if (inCooldown)
{
    cooldownCounter -= Time.deltaTime;
}
else
{
    // Reset the counter
    cooldownCounter = waveCooldown;
}
cooldownCounterUI.text=cooldownCounter.ToString("F0");
}

private IEnumerator WaveCooldown()
{
    inCooldown = true;
        waveOverUI.gameObject.SetActive(true);

    yield return new WaitForSeconds(waveCooldown);
    inCooldown = false;
        waveOverUI.gameObject.SetActive(false);
    currentZombiesPerWave += 5;
    StartNextWave();
}

}


