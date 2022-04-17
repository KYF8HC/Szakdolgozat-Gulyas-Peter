using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    public List<Collectable> collectables;
    private void Start()
    {
        collectables = new List<Collectable>();
    }
    void FixedUpdate()
    {
        FindAllCollectable(transform);
    }
    private void FindAllCollectable(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag("Collectable"))
            {
                Collectable temp = child.GetComponent<Collectable>();
                
                if (temp != null)
                {
                    collectables.Add(temp);
                }
                else FindAllCollectable(child); 
            }
            else FindAllCollectable(child);
        }
    }
}
