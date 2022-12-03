using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Saver : MonoBehaviour
{
    private const string CoinDataFileName = "/Coin.dat";
    private const string ItemDataFileName = "/Item.dat";

    public void Save(List<CoinData> coinDatas)
    {
        string filePath = Application.persistentDataPath + CoinDataFileName;

        using (FileStream fileStream = File.Create(filePath))
        {
            new BinaryFormatter().Serialize(fileStream, coinDatas);
        }
    }

    public void Save(ItemData itemData)
    {
        string filePath = Application.persistentDataPath + ItemDataFileName;

        using (FileStream fileStream = File.Create(filePath))
        {
            new BinaryFormatter().Serialize(fileStream, itemData);
        }
    }

    public List<CoinData> LoadCoinDatas()
    {
        List<CoinData> coinDatas;
        string filePath = Application.persistentDataPath + CoinDataFileName;

        if (File.Exists(filePath))
        {
            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(fileStream);
                coinDatas = (List<CoinData>)loadedData;
            }

            return coinDatas;
        }

        return null;
    }

    public ItemData LoadItemData()
    {
        ItemData itemData;
        string filePath = Application.persistentDataPath + ItemDataFileName;

        if (File.Exists(filePath))
        {
            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(fileStream);
                itemData = (ItemData)loadedData;
            }

            return itemData;
        }

        return null;
    }
}
