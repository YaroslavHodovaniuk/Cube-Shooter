using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopGrid : MonoBehaviour
{
    [SerializeField] private GameObject cellPref;

    private List<ShopCell> shopCells;

    private List<GameObject> weapons;

    public void Init()
    {
        weapons = Resources.LoadAll<GameObject>("Weapon").ToList();
        Debug.Log(weapons.Count);
        shopCells = new List<ShopCell>();
        for (int i = 0; i < weapons.Count; i++)
        {
            var _ = Instantiate(cellPref, this.transform).GetComponent<ShopCell>();
            if (_ == null)
            {
                Debug.LogWarning($"cell: {i} isn`t created");
                return;
            }
            _.Init(weapons[i], i);
            shopCells.Add(_);
        }
    }
}
