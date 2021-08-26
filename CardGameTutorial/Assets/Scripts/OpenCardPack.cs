using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCardPack : MonoBehaviour
{
    public GameObject card;
    public GameObject dataManager;

    private float startPoint = -700.0f;
    private float step;
    private List<GameObject> cardPool = new List<GameObject>();
    private CardData cardData;
    private PlayerDataManager pdm;

    public Transform canvas;
    // Start is called before the first frame update
    void Start()
    {
        step = -startPoint / 2;
        pdm = dataManager.GetComponent<PlayerDataManager>();
        cardData = dataManager.GetComponent<CardData>(); // 获取卡牌数据
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickOpen()
    {
        ClearPool();
        if (pdm.totalCoins >= 300)
        {
            pdm.totalCoins -= 300;


            for (int i = 0; i < 5; i++)
            {
                // 生成卡牌实体
                GameObject newCard = GameObject.Instantiate(card, canvas);
                newCard.transform.localPosition = new Vector2(startPoint + step * i, 0.0f);

                // 添加到对象池
                cardPool.Add(newCard);
                // 赋予该实体以数据
                newCard.GetComponent<CardDisplay>().card = cardData.RandomCard();
            }
            StoreCardsData();
            pdm.updateText();
            pdm.SavePlayerData();
        }
    }
    public void ClearPool()
    {
        foreach (var card in cardPool)
        {
            Destroy(card);
        }
        cardPool.Clear();
    }

    public void StoreCardsData()
    {
        foreach (var item in cardPool)
        {
            int id = item.GetComponent<CardDisplay>().card.id;
            pdm.playerCards[id] += 1; //直接编辑数据是危险行为
        }
    }
}
