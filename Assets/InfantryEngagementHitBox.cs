using UnityEngine;

public class InfantryEngagementHitBox : MonoBehaviour
{

    public InfantryCombatScript infantryCombatScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I collided with: " + other.name);
        infantryCombatScript.Fire();
    }
}
