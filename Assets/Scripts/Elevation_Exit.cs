using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision) // collision is the last object that entered the trigger, in this case the player
    {
        if (collision.gameObject.tag != "Player") return; // if the object that entered the trigger is not the player, do nothing
        foreach (Collider2D mountain in mountainColliders)
        {
            mountain.enabled = true;
        }
        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 20; // set the player's sorting order to 20 so they appear in front of the mountains

        foreach (Collider2D boundary in boundaryColliders)
        {
            boundary.enabled = false;
        }
        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5; // set the player's sorting order to 20 so they appear in front of the mountains
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
