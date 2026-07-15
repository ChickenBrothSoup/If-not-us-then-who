using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterMovementScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 0.5f;
    public float chaseSpeed = 2f;
    public GameObject Noise;
    public GameObject Food;

    private MonsterDetectionScript detection;

    private void Awake()
    {
        detection = GetComponent<MonsterDetectionScript>();
    }

    private void Update()
    {
        if (detection == null) return;

        if (detection.CurrentState == MonsterDetectionScript.State.Advancing)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, Noise.transform.position, speed * Time.deltaTime);
        }
        else if (detection.CurrentState == MonsterDetectionScript.State.Chasing)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, Food.transform.position, chaseSpeed * Time.deltaTime);
        }
    }
}




   