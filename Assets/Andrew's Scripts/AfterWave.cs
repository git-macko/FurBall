using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AfterWave : MonoBehaviour
{
    public GameObject[] ObjectsToSpawn;
    private SpawnManager SM;
    private bool Spawned = true;
    public Button StartWaveButton;
    public GameObject StartWaveButtonUI;
    private int WaveNumber = 0;
    private GameObject Spawner;
    // Start is called before the first frame update
    void Start()
    {
        SM = GetComponent<SpawnManager>();
        StartWaveButton.onClick.AddListener(StartWave);
        StartWaveButtonUI.SetActive(true);    // The start wave button
        Spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn only if...
        if (!SM.activeWave && !Spawned && !(GameObject.FindGameObjectWithTag("Enemy"))) {
            Spawned = true;
            Instantiate(ObjectsToSpawn[0], new Vector2(26, -19), Quaternion.identity);
            Instantiate(ObjectsToSpawn[1], new Vector2(38, -19), Quaternion.identity);
            StartWaveButtonUI.SetActive(true); 
        }

        if (SM.activeWave) {
            Spawned = false;
        }
    }

    private void StartWave() {
        Destroy(GameObject.FindGameObjectWithTag("WeaponCrate"));  // Destroying crate is a condition for next wave
        Destroy(GameObject.FindGameObjectWithTag("Shop"));  // Destroying shop is a condition for next wave
        StartWaveButtonUI.SetActive(false);
        WaveNumber++;
        // No upgraded enemies in first wave
        if (WaveNumber > 1) {
            SpawnManager[] SMs = Spawner.GetComponents<SpawnManager>();

            // Count to only target enemy 1 and 2, prefabs save the progress !!
            int count = 0;
            foreach (SpawnManager SM in SMs)
            {
                if (count > 1) {
                    continue;
                }
                SM.UpdateEnemies(WaveNumber % 3 == 0);  // Every third wave, upgrade the strength of enemies
                count++;
            }
        }
    }
}
