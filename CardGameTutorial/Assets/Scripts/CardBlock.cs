using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBlock : MonoBehaviour, IPointerClickHandler
{
    public GameObject summonBlock;
    public GameObject attackBlock;// not require
    //public bool hasMonster;
    public GameObject monsterCard;

    //public BattleManager BattleManager;
    // Start is called before the first frame update
    void Start()
    {
        //BattleManager = GameObject.Find("AIBattleManager").GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetSummon()
    {
        summonBlock.SetActive(true);
    }
    public void SetAttack()
    {
        attackBlock.SetActive(true);
    }
    public void CloseAll()
    {
        summonBlock.SetActive(false);
        attackBlock.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (summonBlock.activeInHierarchy)
        {
            BattleManager.Instance.SummonCofirm(transform);
            //hasMonster = true;
        }
        //Debug.Log("click block");
    }
}
