using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    public GameObject[] blueprints;
    private int blueprintsNumber;
    public GameObject placeableObject;
    public Camera fpsCam;
    int maxBuildDistance = 10;
    private KeyCode newObjectHotkey = KeyCode.B;
    public bool buildingMode = true;

    // Start is called before the first frame update
    void Start()
    {
        blueprints = GameObject.FindGameObjectsWithTag("Blueprint");
        for (int i = 0; i < blueprints.Length; i++)
        {
            blueprints[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(newObjectHotkey))
        {
            buildingMode = !buildingMode;

            if (buildingMode == true)
            {
                for (int i = 0; i < blueprints.Length; i++)
                {
                    blueprints[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < blueprints.Length; i++)
                {
                    blueprints[i].SetActive(false);
                }
            }  
        }
        if (buildingMode)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                RaycastHit hit;
                Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, maxBuildDistance);
                if (hit.transform.gameObject.tag == "Blueprint")
                {
                    Instantiate(placeableObject, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                    //hit.transform.gameObject.tag = "BlueprintReplaced";
                    Destroy(hit.transform.gameObject);
                    blueprints = GameObject.FindGameObjectsWithTag("Blueprint");
                }
            }
        }
    }
}
