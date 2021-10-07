using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : BattleManager
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GameStart()
    {
        playerSummonCount = maxPlayerSummonCount;
        enemySummonCount = maxEnemySummonCount;
        CardDate = playerData.GetComponent<CardData>();

        currentPhase = GamePhase.gameStart;
        ReadDeck();
        //Debug.Log(currentPhase);
        DrawCard(0, 5);
        DrawCard(1, 5,true);
        currentPhase = GamePhase.playerDraw;
        //Debug.Log(currentPhase);
    }
    public override void OnClickTurnEnd()
    {
        if (currentPhase == GamePhase.playerAction)
        {
            base.OnClickTurnEnd();
        }
    }
}
