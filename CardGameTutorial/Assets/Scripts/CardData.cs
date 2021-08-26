using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public List<Card> CardList = new List<Card>(); // 存储卡牌数据的链表
    public TextAsset cardListData; // 卡牌数据txt文件
    // Start is called before the first frame update
    void Start()
    {
        // LordCardList();
        // 实例化卡牌并加入到卡表中，两种方式
        // MonsterCard RedDragon = new MonsterCard(0, "RedDragon", 60, 100, "fire");
        // CardList.Add(RedDragon);

        // CardList.Add(new MonsterCard(1, "LightKnight", 30, 60, "Light"));
        // CardList.Add(new MonsterCard(2, "ForestDeer", 20, 70, "Forest"));
        // CardList.Add(new MonsterCard(3, "DeepWater", 40, 80, "Water"));
        // CardList.Add(new MonsterCard(4, "TheaterBird", 70, 30, "Theater"));
        // CardList.Add(new SpellCard(5, "FireBall", 4, "Fire", "造成20点伤害"));
        // CardList.Add(new ItemCard(6, "HealthPotion", "Consumable", "恢复40点生命值"));
        // CardList.Add(new ItemCard(7, "FireSword", "Equipment", "增加10点攻击力"));

        // foreach (var item in CardList)
        // {
        //     print(item.cardName);
        //     print(item.GetType());
        //     var card1 = item as MonsterCard;
        //     if (card1 != null)
        //     {
        //         Debug.Log("Monster Card: " + card1.cardName + "\n <color=red>Attack point: " + card1.attack + "</color>");
        //     }

        // }

        // TestCopy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LordCardList()
    {
        string[] dataArray = cardListData.text.Split('\n');
        foreach (var row in dataArray)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "m")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int atk = int.Parse(rowArray[3]);
                int hp = int.Parse(rowArray[4]);
                string type = rowArray[5];
                CardList.Add(new MonsterCard(id, name, atk, hp, type));
            }
            else if (rowArray[0] == "s")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int rank = int.Parse(rowArray[3]);
                string type = rowArray[4];
                string effect = rowArray[5];
                CardList.Add(new SpellCard(id, name, rank, type, effect));
            }
            else if (rowArray[0] == "t")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                string type = rowArray[3];
                string effect = rowArray[4];
                CardList.Add(new ItemCard(id, name, type, effect));
            }
        }
    }

    void TestCopy()//复制数据，需要创建一个新的实例，从原有的数据中构建
    {
        List<Card> copylist = new List<Card>();
        copylist = CardList;
        Card card1 = copylist[1];
        Card card2 = new Card(card1.id, card1.cardName);
        Card card3 = CopyCard(1);

        card1.cardName = "DarkKnight";
        print("test copy");
        print(card3.cardName + "," + card1.GetType());
        print(card2.cardName + "," + card2.GetType());
        print(CardList[1].cardName + "," + CardList[1].GetType());
        // MonsterCard c = card1;
    }
    public Card RandomCard()
    {
        Card randCard = CardList[Random.Range(0, CardList.Count)];
        return randCard;
    }

    public Card CopyCard(int _id) // 从卡牌数据中复制一个实体，这个实体的改变不会影响原始数据
    {
        Card card = CardList[_id];
        Card copyCard = new Card(_id, card.cardName);
        if (card is MonsterCard)
        {
            var monstercard = card as MonsterCard;
            copyCard = new MonsterCard(_id, monstercard.cardName, monstercard.attack, monstercard.healthPointMax, monstercard.type);
        }
        else if (card is SpellCard)
        {
            var spellcard = card as SpellCard;
            copyCard = new SpellCard(_id, spellcard.cardName, spellcard.rank, spellcard.type, spellcard.effect);
        }
        else if (card is ItemCard)
        {
            var itemcard = card as ItemCard;
            copyCard = new ItemCard(_id, itemcard.cardName, itemcard.type, itemcard.effect);
        }
        return copyCard;
    }
}
