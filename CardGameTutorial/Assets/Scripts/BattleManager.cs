using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GamePhase
{
    playerDraw, playerAction, enemyDraw, enemyAction, gameStart
}
/*public enum GameEvent
{
    phaseChange, monsterDestroy
}*/
public class BattleManager : MonoSingleton<BattleManager>
{
    public GameObject playerData; // 数据
    public GameObject enemyData;
    public GameObject playerHands; // 手牌
    public GameObject enemyHands;
    public GameObject[] playerBlocks; // 怪兽区
    public GameObject[] enemyBlocks;
    public List<Card> playerDeckList = new List<Card>(); // 卡组
    public List<Card> enemyDeckList = new List<Card>();

    public GameObject cardPrefab;

    public GameObject arrowPrefab;//召唤指示箭头
    public GameObject attackPrefab;//攻击指示箭头
    private GameObject arrow;


    // 生命值
    public int playerHealthPoint;
    public int enemyHealthPoint;

    public GameObject playerIcon;
    public GameObject enemyIcon;

    // 召唤次数
    public int maxPlayerSummonCount;
    public int playerSummonCount;
    public int maxEnemySummonCount;
    public int enemySummonCount;


    public GamePhase currentPhase = GamePhase.playerDraw;


    protected CardData CardDate;

    public Transform canvas;


    private GameObject waitingMonster;
    private int waitingID;
    public GameObject attackingMonster;
    private int attackingID;

    // private Dictionary<string, UnityEvent> eventDic = new Dictionary<string, UnityEvent>();
    public UnityEvent phaseChangeEvent;


    // Start is called before the first frame update
    void Start()
    {      
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerDrawCard()
    {
        if (currentPhase == GamePhase.playerDraw)
        {
            DrawCard(0, 1);           
        }
    }
    public void OnEnemyDrawCard()
    {
        if (currentPhase == GamePhase.enemyDraw)
        {
            DrawCard(1, 1);  
        }
    }
    public void DrawCard(int _player, int _number, bool _back = false, bool _state=true)
    {
        if (_player == 0)
        {
            for (int i = 0; i < _number; i++)
            {
                GameObject newCard = GameObject.Instantiate(cardPrefab, playerHands.transform);
                newCard.GetComponent<CardDisplay>().card = playerDeckList[0];
                playerDeckList.RemoveAt(0);
                newCard.GetComponent<BattleCard>().cardState = CardState.inPlayerHand;
                // 显示卡背
                if (_back)
                {
                    newCard.GetComponent<CardDisplay>().back = true;
                }
                if (_state)
                {
                    currentPhase = GamePhase.playerAction;
                    phaseChangeEvent.Invoke();
                }
            }

        }
        else if (_player == 1)
        {
            for (int i = 0; i < _number; i++)
            {
                GameObject newCard = GameObject.Instantiate(cardPrefab, enemyHands.transform);
                newCard.GetComponent<CardDisplay>().card = enemyDeckList[0];
                enemyDeckList.RemoveAt(0);
                newCard.GetComponent<BattleCard>().cardState = CardState.inEnemyHand;
                // 显示卡背
                if (_back)
                {
                    newCard.GetComponent<CardDisplay>().back = true;
                }
                if (_state)
                {
                    currentPhase = GamePhase.enemyAction;
                    phaseChangeEvent.Invoke();
                }
            }
        }     
    }

    public virtual void OnClickTurnEnd()
    {
        TurnEnd();
    }
    public void TurnEnd()
    {
        
        if (arrow != null)
        {
            Destroy(arrow);
        }
        if (currentPhase == GamePhase.playerAction)
        {
            currentPhase = GamePhase.enemyDraw;
            enemySummonCount = maxEnemySummonCount;

            //playerIcon.GetComponent<AttackTarget>().attackable = true;
            //enemyIcon.GetComponent<AttackTarget>().attackable = false;
            foreach (var block in playerBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard != null)
                {
                    block.GetComponent<CardBlock>().monsterCard.GetComponent<AttackTarget>().attackable = true;
                }
            }
            foreach (var block in enemyBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard != null)
                {
                    block.GetComponent<CardBlock>().monsterCard.GetComponent<AttackTarget>().attackable = false;
                    block.GetComponent<CardBlock>().monsterCard.GetComponent<BattleCard>().hasAttacked = false;
                }
            }
        }
        else if (currentPhase == GamePhase.enemyAction)
        {
            currentPhase = GamePhase.playerDraw;
            playerSummonCount = maxPlayerSummonCount;

            //playerIcon.GetComponent<AttackTarget>().attackable = false;
            //enemyIcon.GetComponent<AttackTarget>().attackable = true;
            foreach (var block in playerBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard != null)
                {
                    block.GetComponent<CardBlock>().monsterCard.GetComponent<AttackTarget>().attackable = false;
                    block.GetComponent<CardBlock>().monsterCard.GetComponent<BattleCard>().hasAttacked = false;
                }
            }
            foreach (var block in enemyBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard != null)
                {
                    block.GetComponent<CardBlock>().monsterCard.GetComponent<AttackTarget>().attackable = true;
                }
            }
        }
        phaseChangeEvent.Invoke();
    }



    public void SummonRequst(Vector2 _startPoint, int _player, GameObject _monster) // 召唤请求，点击手牌时触发
    {
        if (arrow != null)
        {
            Destroy(arrow);
        }
        if (_player == 0 && playerSummonCount >= 1)
        {
            arrow = GameObject.Instantiate(arrowPrefab, canvas);
            arrow.GetComponent<ArrowFollow>().SetStartPoint(_startPoint);
            foreach (var block in playerBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard == null)
                {
                    block.GetComponent<CardBlock>().SetSummon();
                }
            }
            waitingMonster = _monster;
            waitingID = _player;
        }
        else if (_player == 1 && enemySummonCount >= 1)
        {
            arrow = GameObject.Instantiate(arrowPrefab, canvas);
            arrow.GetComponent<ArrowFollow>().SetStartPoint(_startPoint);
            foreach (var block in enemyBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard == null)
                {
                    block.GetComponent<CardBlock>().SetSummon();
                }
            }
            waitingMonster = _monster;
            waitingID = _player;
        }
    }
    public void SummonCofirm(Transform _block) // 召唤确认，点击格子时触发
    {
        Summon(waitingMonster, waitingID, _block);
        waitingMonster = null;
    }

    /// <summary>
    /// 召唤怪兽
    /// </summary>
    /// <param name="_monster">要召唤的怪兽卡物体</param>
    /// <param name="_id">召唤者编号</param>
    /// <param name="_block">要召唤到的格子节点</param>
    public void Summon(GameObject _monster, int _id, Transform _block)
    {
        _monster.transform.SetParent(_block);
        _monster.GetComponent<CardDisplay>().ShowCard();
        _block.GetComponent<CardBlock>().monsterCard = _monster;
        //_block.GetComponent<CardBlock>().hasMonster = true;
        _monster.transform.localPosition = Vector3.zero;
        if (_id == 0)
        {
            _monster.GetComponent<BattleCard>().cardState = CardState.inPlayerBlock;
            playerSummonCount--;
            foreach (var block in playerBlocks)
            {
                block.GetComponent<CardBlock>().CloseAll();
            }
        }
        else if (_id == 1)
        {
            _monster.GetComponent<BattleCard>().cardState = CardState.inEnemyBlock;
            enemySummonCount--;
            foreach (var block in enemyBlocks)
            {
                block.GetComponent<CardBlock>().CloseAll();
            }
        }

        if (arrow != null)
        {
            Destroy(arrow);
        }
    }


    public void AttackRequst(Vector2 _startPoint, int _player, GameObject _monster)
    {
        if (arrow == null)
        {
            arrow = GameObject.Instantiate(attackPrefab, canvas);
        }

        arrow.GetComponent<ArrowFollow>().SetStartPoint(_startPoint);

        // 直接攻击条件
        bool strightAttack = true;
        if (_player == 0)
        {
            foreach (var block in enemyBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard != null)
                {
                    block.GetComponent<CardBlock>().SetAttack();
                    strightAttack = false;
                }
            }
            if (strightAttack)
            {
                // 可以直接攻击对手玩家
            }
        }
        if (_player == 1)
        {
            foreach (var block in playerBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard != null)
                {
                    block.GetComponent<CardBlock>().SetAttack();
                    strightAttack = false;
                }
            }
            if (strightAttack)
            {
                // 可以直接攻击对手玩家
            }
        }

        attackingMonster = _monster;
        attackingID = _player;

    }
    public void AttackCofirm(GameObject _target)
    {
        Attack(attackingMonster, attackingID, _target);
        attackingMonster = null;
    }
    public void Attack(GameObject _monster, int _id, GameObject _target)
    {
        //结算伤害
        //处理销毁
        //恢复攻击状态，已攻击状态
        if (arrow != null)
        {
            Destroy(arrow);
        }
        _monster.GetComponent<BattleCard>().hasAttacked = true;
        Debug.Log("攻击成立");

        // 
        var attackMonster = _monster.GetComponent<CardDisplay>().card as MonsterCard;
        var targetMonster = _target.GetComponent<CardDisplay>().card as MonsterCard;
        //Debug.Log(targetMonster.healthPoint);
        targetMonster.GetDamage(attackMonster.attack);
        if (targetMonster.healthPoint > 0)
        {
            _target.GetComponent<CardDisplay>().ShowCard();
        }
        else
        {
            Destroy(_target);
        }


        foreach (var block in playerBlocks)
        {
            block.GetComponent<CardBlock>().CloseAll();
        }
        foreach (var block in enemyBlocks)
        {
            block.GetComponent<CardBlock>().CloseAll();
        }
    }

    public virtual void GameStart() // 游戏开始，读取卡组，抽五张手牌
    {
        playerSummonCount = maxPlayerSummonCount;
        enemySummonCount = maxEnemySummonCount;
        CardDate = playerData.GetComponent<CardData>();

        currentPhase = GamePhase.gameStart;
        ReadDeck();
        //Debug.Log(currentPhase);
        DrawCard(0, 5);
        DrawCard(1, 5);
        currentPhase = GamePhase.playerDraw;
        //Debug.Log(currentPhase);
    }

    public void ReadDeck() // 从数据中读取卡组
    {
        PlayerDataManager pdm = playerData.GetComponent<PlayerDataManager>();
        for (int i = 0; i < pdm.playerDeck.Length; i++)
        {
            if (pdm.playerDeck[i] != 0)
            {
                int counter = pdm.playerDeck[i];
                for (int j = 0; j < counter; j++)
                {
                    playerDeckList.Add(CardDate.CopyCard(i));
                }
            }
        }
        // 读取敌人的卡组
        PlayerDataManager edm = enemyData.GetComponent<PlayerDataManager>();
        for (int i = 0; i < edm.playerDeck.Length; i++)
        {
            if (edm.playerDeck[i] != 0)
            {
                int counter = edm.playerDeck[i];
                for (int j = 0; j < counter; j++)
                {
                    enemyDeckList.Add(CardDate.CopyCard(i));
                }
            }
        }
        ShuffletDeck(0);
        ShuffletDeck(1);
        foreach (var item in playerDeckList)
        {
            //Debug.Log(item.cardName);
        }
    }

    public void ShuffletDeck(int _player) // 将卡组洗牌，输入玩家编号，0代表player，1代表Enemy
    {
        // 洗牌算法的基本思路是遍历整个卡组，对于每一张牌，都和随机的一张牌调换位置。
        switch (_player)
        {
            case 0:
                for (int i = 0; i < playerDeckList.Count; i++)
                {
                    int rad = Random.Range(0, playerDeckList.Count);
                    Card temp = playerDeckList[i];
                    playerDeckList[i] = playerDeckList[rad];
                    playerDeckList[rad] = temp;
                }
                break;
            case 1:
                for (int i = 0; i < enemyDeckList.Count; i++)
                {
                    int rad = Random.Range(0, enemyDeckList.Count);
                    Card temp = enemyDeckList[i];
                    enemyDeckList[i] = enemyDeckList[rad];
                    enemyDeckList[rad] = temp;
                }
                break;
        }
    }
}
