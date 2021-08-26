using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Text cardName;
    public Text cardAttack;
    public Text cardHealth;

    public Text infoText;

    public Image background;

    public Color monsterColor;
    public Color itemColor;
    public Color spellColor;
    // Start is called before the first frame update
    void Start()
    {
        // ColorUtility.TryParseHtmlString("#387445", out monsterColor);
        // ColorUtility.TryParseHtmlString("#556874", out itemColor);
        // ColorUtility.TryParseHtmlString("#79548E", out spellColor);
        if (card != null)
        {
            ShowCard();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowCard()
    {
        cardName.text = card.cardName;
        if (card is MonsterCard) // 如果是怪兽卡，就将攻击力显示为红色，生命值为绿色
        {
            var monstercard = card as MonsterCard;
            cardAttack.text = "<color=red>" + monstercard.attack.ToString() + "</color>";
            cardHealth.text = "<color=blue>" + monstercard.healthPoint.ToString() + "</color>";
            background.color = monsterColor;
            infoText.gameObject.SetActive(false);

        }
        // 如果是法术牌和道具牌，则不需要显示攻击力等数值
        else if (card is ItemCard)
        {
            var itemcard = card as ItemCard;
            cardAttack.gameObject.SetActive(false);
            cardHealth.gameObject.SetActive(false);
            background.color = itemColor;
            infoText.text = itemcard.effect;
        }
        else if (card is SpellCard)
        {
            var spellcard = card as SpellCard;
            cardAttack.gameObject.SetActive(false);
            cardHealth.gameObject.SetActive(false);
            background.color = spellColor;
            infoText.text = spellcard.effect;
        }

    }
}
