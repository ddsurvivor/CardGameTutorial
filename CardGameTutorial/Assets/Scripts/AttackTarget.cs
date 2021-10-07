using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackTarget : MonoBehaviour, IPointerClickHandler
{
    public bool attackable;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (attackable && BattleManager.Instance.attackingMonster != null)
        {
            BattleManager.Instance.AttackCofirm(transform.gameObject);
        }
    }
}
