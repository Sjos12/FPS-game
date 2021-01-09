using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class SpawnZombie : MonoBehaviour
    {
        //gameobject for spawning 
        public GameObject spawnObj;
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

        void SpawnObject()
        {
            float x = Random.Range(MinX, MaxX);
            float y = Random.Range(MinY, MaxY);
            float z = Random.Range(MinZ, MaxZ);

            GameObject zombie = Instantiate(spawnObj, new Vector3(x, y, z), Quaternion.identity);

            zombie.GetComponent<AIZombieController>().target = GameObject.Find("FPSController_Low_Poly").transform;

        }

        void Update()
        {
            if (Input.GetKeyDown("c"))
            {
                SpawnObject();
            }


        }

    }
}

