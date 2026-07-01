using UnityEngine;

public class MonsterSpawnerScript : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform spawnPoint;
    public float spacing = 1f;

    private void Start()
    {
        if (BattleDataScript.Instance == null)
        {
            Debug.LogError("No BattleData found!");
            return;
        }

        int count = BattleDataScript.Instance.monsterSquadSize;
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = spawnPoint.position + new Vector3(0f, i * spacing, 0f);
            GameObject monster = Instantiate(monsterPrefab, pos, Quaternion.identity);

            MonsterSquadHealthScript health = monster.GetComponent<MonsterSquadHealthScript>();
            if (health != null)
            {
                health.monstersHealth = BattleDataScript.Instance.monsterHealth;
            }
        }
    }
}
