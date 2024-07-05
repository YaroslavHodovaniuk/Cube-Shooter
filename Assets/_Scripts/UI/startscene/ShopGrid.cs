using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopGrid : MonoBehaviour
{
    [SerializeField] private GameObject cellPref;
    [SerializeField] private string Type;
    private List<ShopCell> shopCells;

    private List<Texture2D> textures;
    private List<Sprite> sprites;

    public void Init()
    {
        if (shopCells == null)
        {
            LoadSprites();
            shopCells = new List<ShopCell>();
            for (int i = 0; i < textures.Count; i++)
            {
                var shopCell = Instantiate(cellPref, this.transform).GetComponent<ShopCell>();
                if (shopCell == null)
                {
                    Debug.LogWarning($"cell: {i} isn`t created");
                    return;
                }
                shopCell.Init(sprites[i], i);
                shopCells.Add(shopCell);
            }
        }
    }

    private void LoadSprites()
    {
        textures = Resources.LoadAll<Texture2D>(Type).ToList();
        sprites = new List<Sprite>();

        // Перетворення кожної текстури у спрайт
        foreach (var texture in textures)
        {
            Sprite sprite = TextureToSpriteConverter(texture);
            sprites.Add(sprite);
        }
    }

    private Sprite TextureToSpriteConverter(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
