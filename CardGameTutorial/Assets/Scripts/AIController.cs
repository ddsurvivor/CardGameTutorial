using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    /// <summary>
    /// 行动间隔时间
    /// </summary>
    public float actionTimeStart;
    private float actionTime;

    bool haveBlock;
    bool haveMonster;
    GameObject currentMonster;
    Transform currentBlock;


    // Start is called before the first frame update
    void Start()
    {
        actionTime = actionTimeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.Instance.currentPhase == GamePhase.enemyDraw)
        {
            BattleManager.Instance.DrawCard(1, 1, true);
        }
        if (BattleManager.Instance.currentPhase == GamePhase.enemyAction)
        {
            if (actionTime <= 0)
            {
                NextStep();
                Debug.Log("Next step");
                actionTime = actionTimeStart;
            }
            else
            {
                actionTime -= Time.deltaTime;
            }
        }
        
    }

    private void NextStep()
    {
        if (BattleManager.Instance.enemySummonCount >= 1)
        {
            Debug.Log("执行召唤判定");
            //执行召唤步骤
            foreach (var block in BattleManager.Instance.enemyBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard == null)
                {
                    haveBlock = true;
                    currentBlock = block.transform;
                    break;
                }
            }
            foreach (Transform child in BattleManager.Instance.enemyHands.transform)
            {
                if (child.GetComponent<CardDisplay>().card is MonsterCard)
                {
                    haveMonster = true;
                    currentMonster = child.gameObject;
                    break;
                }
            }
            if (haveBlock && haveMonster)
            {
                Debug.Log("开始召唤");
                BattleManager.Instance.Summon(currentMonster, 1, currentBlock);
            }
        }
        
    }
}
