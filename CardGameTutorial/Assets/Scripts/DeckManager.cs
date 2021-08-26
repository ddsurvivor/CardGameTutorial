using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject cardLabel;
    public GameObject panel;

    public GameObject playerData;

    private PlayerDataManager pdm;
    private CardData cardData;

    private List<GameObject> cardPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        pdm = playerData.GetComponent<PlayerDataManager>();
        cardData = playerData.GetComponent<CardData>();
        UpdateDeck();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateDeck()
    {
        ClearPool();
        for (int i = 0; i < pdm.playerDeck.Length; i++)
        {
            if (pdm.playerDeck[i] != 0)
            {
                GameObject newCard = GameObject.Instantiate(cardLabel, panel.transform);
                cardPool.Add(newCard);
                newCard.GetComponent<CardDisplay>().card = cardData.CardList[i];
                newCard.GetComponent<UICardCounter>().counter.text = pdm.playerDeck[i].ToString();
            }
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

    public void OnClickSave()
    {
        pdm.SavePlayerData();
    }
}
