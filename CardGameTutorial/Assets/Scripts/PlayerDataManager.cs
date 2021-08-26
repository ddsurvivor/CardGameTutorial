using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/* 用于保存玩家的数据
可以从 csv 文件中读取玩家的资料，并且自动保存
可以保存玩家的金币数量，仓库中卡牌的数量，以及卡组。
【商店场景】玩家在商店中购买卡包的时候会先查看数据里的金币数是否充足。
【编辑卡组】卡组的数据格式与仓库类似
【对战场景】。。。
*/
public class PlayerDataManager : MonoBehaviour
{
    public TextAsset playerData;

    public Text coinsText;
    public Text cardsText;

    public int totalCoins;

    private CardData cardData;
    public int[] playerCards;
    public int[] playerDeck;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        cardData = GetComponent<CardData>();
        cardData.LordCardList();
        LordPlayerData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LordPlayerData()
    {
        playerCards = new int[cardData.CardList.Count];
        playerDeck = new int[cardData.CardList.Count];
        string[] dataArray = playerData.text.Split('\n');
        foreach (var row in dataArray)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "coins")
            {
                totalCoins = int.Parse(rowArray[1]);
            }
            else if (rowArray[0] == "card")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);
                playerCards[id] = num;
            }
            else if (rowArray[0] == "deck")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);
                playerDeck[id] = num;
            }
        }
        updateText();
    }
    public void updateText()
    {
        coinsText.text = "Total Coins:" + totalCoins.ToString();
        cardsText.text = "Cards Number:" + Sum(playerCards).ToString();
    }

    public int Sum(int[] _cards)
    {
        int sum = 0;
        foreach (int item in _cards)
        {
            sum += item;
        }
        return sum;
    }

    public void SavePlayerData()
    {
        List<string> datas = new List<string>();
        string path = Application.dataPath + "/Datas/playerdata.csv";
        datas.Add("coins," + totalCoins.ToString());
        for (int i = 0; i < playerCards.Length; i++)
        {
            if (playerCards[i] != 0)
            {
                datas.Add("card," + i.ToString() + "," + playerCards[i].ToString());
            }
        }
        for (int i = 0; i < playerDeck.Length; i++)
        {
            if (playerDeck[i] != 0)
            {
                datas.Add("deck," + i.ToString() + "," + playerDeck[i].ToString());
            }
        }

        File.WriteAllLines(path, datas);
    }

}
