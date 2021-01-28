using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnZombie : MonoBehaviour
    {
        //gameobject for spawning 
        public GameObject spawnObj;
        public TextMeshProUGUI waveDisplay;
        public TextMeshProUGUI zombieCounter;
        //public Wave[] waves;
        public int zombieAmount;
        public int zombieSpawnAmount = 3;
        public int wave = 0;
        public int waveIncreaser = 3;

        //set custom range for random position
        public float MinX = 0;
        public float MaxX = 10;
        public float MinY = 0;
        public float MaxY = 10;

        //for 3d you have z position
        public float MinZ = 0;
        public float MaxZ = 10;

        //turn off or on 3D placement
        public bool is3D = false;

        public void Update()
        {
            zombieCounter.text = zombieAmount.ToString();
            IEnumerator AllZombiesDead()
            {

                yield return new WaitForSeconds(5);
                
        }
        if (zombieAmount == 0)
            {
                //StartCoroutine(AllZombiesDead());
                SpawnWave(waveIncreaser);
                wave += 1;
                waveDisplay.text = wave.ToString();
        }
        }
        void SpawnWave(int waveMultiplier)
        {
            for (int i = 0; i < zombieSpawnAmount; i++)
            {
                SpawnObject();
            }
            zombieSpawnAmount = waveMultiplier + zombieSpawnAmount;
        }
        void SpawnObject()
        {
            float x = Random.Range(MinX, MaxX);
            float y = Random.Range(MinY, MaxY);
            float z = Random.Range(MinZ, MaxZ);

            GameObject zombie = Instantiate(spawnObj, new Vector3(x, y, z), Quaternion.identity);

            zombie.GetComponent<AIZombieController>().target = GameObject.FindGameObjectWithTag("Player").transform;

            zombieAmount += 1; 
        }

        

    }


