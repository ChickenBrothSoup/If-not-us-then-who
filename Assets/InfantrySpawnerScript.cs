using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfantrySpawnerScript : MonoBehaviour
{
    public GameObject infantryPrefab;
    public Transform spawnPoint;
    public float spacing = 1f;


    private void Start()
    {

        


        if (BattleDataScript.Instance == null)
        {
            Debug.LogError("No BattleData found!");
            return;
        }

        int count = BattleDataScript.Instance.infantrySquadSize;
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = spawnPoint.position + new Vector3(0f, i * spacing, 0f);
            Instantiate(infantryPrefab, pos, Quaternion.identity);
        }
    }
}
