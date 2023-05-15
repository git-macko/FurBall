using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public float waveTime = 180.0F;
    private float nextWave = 0.0F;
    private float spawnCooldown;
    private float nextSpawn = 0.0F;
    public bool activeWave = false;

    public GameObject[] thingsToSpawn;
    private int objectNum;
    // Start is called before the first frame update
    void Start()
    {
        spawnCooldown = Random.Range(6, 9f);
    }

    
    // Update is called once per frame
    void Update()
    {
        
        // Don't start wave if...
        if (ConditionsBeforeSpawn()) {
            return;
        } 

        if (Time.time > nextWave)
        {
            nextWave = Time.time + waveTime;
            if (activeWave == true)
            {
                activeWave = false;
            }
            else
            {
                activeWave = true;
            }
        }

        if (activeWave)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnCooldown;
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy()
    {
        InstructScript.active = false;
        objectNum = Random.Range(0, thingsToSpawn.Length);

        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 
                Random.Range(-size.y / 2, size.y / 2), 0);
        Instantiate(thingsToSpawn[objectNum], pos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    private bool ConditionsBeforeSpawn() {
        return GameObject.FindGameObjectWithTag("Shop") || GameObject.FindGameObjectWithTag("WeaponCrate") || 
        (!activeWave && (GameObject.FindGameObjectWithTag("Enemy")));
    }

    public void UpdateEnemies(bool strength) {
        foreach (GameObject enemy in thingsToSpawn) {
            enemy.GetComponent<EnemyCombat>().health += 10;
            if (strength) {
                enemy.GetComponent<EnemyCombat>().damage += 1;
            }
        }
    }
}
