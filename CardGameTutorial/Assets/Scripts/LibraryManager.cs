using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject panel;

    public GameObject playerData;

    private PlayerDataManager pdm;
    private CardData cardData;
    // Start is called before the first frame update
    void Start()
    {
        pdm = playerData.GetComponent<PlayerDataManager>();
        cardData = playerData.GetComponent<CardData>();
        UpdateLibrary();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateLibrary()
    {
        for (int i = 0; i < pdm.playerCards.Length; i++)
        {
            if (pdm.playerCards[i] != 0)
            {
                GameObject newCard = GameObject.Instantiate(cardPrefab, panel.transform);
                // 赋予该实体以数据
                newCard.GetComponent<CardDisplay>().card = cardData.CardList[i];
                newCard.GetComponent<UICardCounter>().counter.text = pdm.playerCards[i].ToString();
            }
        }

    }
}
