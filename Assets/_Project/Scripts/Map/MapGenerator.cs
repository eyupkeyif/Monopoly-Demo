using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteData
{
    public ItemType type;
    public Sprite sprite;
}
public class MapGenerator : Singleton<MapGenerator>
{
    [SerializeField] Tile tilePrefab;
    Tile[] tiles;
    [SerializeField] float positionOffset;
    [SerializeField] List<SpriteData> spriteDatas;

    

    [ContextMenu("Generate Random Map")]
    public void GenerateRandom()
    {
        int mapSize = 12 * UnityEngine.Random.Range(1,5);
        tiles = new Tile[mapSize];

        GameObject tileParent = new GameObject("Tile Parent");

        for (int i = 0; i < mapSize; i++)
        {
            Tile tile = Instantiate(tilePrefab,new Vector3(0,0,i*positionOffset),Quaternion.identity);
            tile.gameObject.transform.SetParent(tileParent.transform);
            
            Array randomType = Enum.GetValues(typeof(ItemType));
            tile.item.itemType = (ItemType)randomType.GetValue(UnityEngine.Random.Range(0,randomType.Length));
            
            if (tile.item.itemType!=ItemType.None)
            {
                tile.item.itemAmount = UnityEngine.Random.Range(1,21);
                tile.item.itemImage.sprite = GetSprite(tile.item.itemType);
                tile.item.itemImage.gameObject.SetActive(true);
                tile.item.itemText.text = tile.item.itemAmount.ToString() + "X " + tile.item.itemType;
            }
            else
            {
                tile.item.itemImage.gameObject.SetActive(false);
                tile.item.itemText.text = "";
            }
            
            tiles[i]=tile;
        
            
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            if (i>0)
            {
                tiles[i].previousTile = tiles[i-1];
            }
            
            
            if (i<(mapSize-1))
            {
                tiles[i].nextTile = tiles[i+1];
            }
            else if(i==(mapSize-1))
            {
                tiles[i].nextTile = tiles[0];
            }
        }
    }

    private Sprite GetSprite(ItemType type)
    {
        Sprite tempSprite = null;
        foreach (var item in spriteDatas)
        {
            if (item.type==type)
            {
                tempSprite = item.sprite;
            }
        }

        return tempSprite;
    }

    public Tile[] GetTileArray()
    {
        return tiles;
    }
}
