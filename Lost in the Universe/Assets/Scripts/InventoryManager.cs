using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Stack inventoryOne;

    [SerializeField] private int[] gemQuantity = {0, 0, 0} ;

    // Start is called before the first frame update
    void Start()
    {
        inventoryOne = new Stack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[] GetGemQuantity()
    {
        return gemQuantity;
    }

    public void CountGem(GameObject gem){
        GemController g = gem.GetComponent<GemController>();
        switch (g.GetTypeGem())
        {
            case GameManager.typesGem.Gem:
            gemQuantity[0]++;
            break;
            case GameManager.typesGem.SuperGem:
            gemQuantity[1]++;
            break;
            case GameManager.typesGem.HiperGem:
            gemQuantity[2]++;
            break;
            default:
            Debug.Log("No se puede contar");
            break;
        }
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
