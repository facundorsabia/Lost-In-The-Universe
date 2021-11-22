using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Stack inventoryOne;

    // Start is called before the first frame update
    void Start()
    {
        inventoryOne = new Stack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInventoryOne(GameObject item)
    {
        inventoryOne.Push(item);
    }

    public GameObject GetInventoryOne()
    {
        return inventoryOne.Pop() as GameObject;
    }
}
