using UnityEngine;

public class BattleDataScript : MonoBehaviour
{
    public static BattleDataScript Instance { get; private set; }

    public int infantrySquadSize;
    public int monsterSquadSize;
    public float monsterHealth;
    public int iconToDestroy = -1;
    public int fightingIconIndex = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
