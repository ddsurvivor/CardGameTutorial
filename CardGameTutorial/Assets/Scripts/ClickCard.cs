using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCard : MonoBehaviour
{
    public DeckManager deckManger;
    private PlayerDataManager pdm;
    // Start is called before the first frame update
    void Start()
    {
        deckManger = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        pdm = GameObject.Find("PlayerData").GetComponent<PlayerDataManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnBuildClick()
    {
        int id = GetComponent<CardDisplay>().card.id;
        pdm.playerDeck[id] += 1;
        deckManger.UpdateDeck();
    }
    public void OnRemoveClick()
    {
        int id = GetComponent<CardDisplay>().card.id;
        pdm.playerDeck[id] -= 1;
        deckManger.UpdateDeck();
    }
    public void OnBattleClick()
    {

    }
}
