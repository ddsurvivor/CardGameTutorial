// 卡牌类 基础类
public class Card
{
    //卡牌编号
    public int id;
    public string cardName;
    public Card(int _id, string _cardName)
    {
        this.id = _id;
        this.cardName = _cardName;
    }

}

// 怪兽卡类
public class MonsterCard : Card
{

    // 怪兽攻击力
    public int attack;
    /// 生命值
    public int healthPoint;
    // 生命值上限
    public int healthPointMax;
    // 怪兽等级
    public int rank;
    // 怪兽属性
    public string type;
    // 怪兽效果
    public string effect;

    public MonsterCard(int _id, string _cardName, int _attack, int _healthPoint, string _type) : base(_id, _cardName)
    {
        this.attack = _attack;
        this.healthPointMax = _healthPoint;
        this.healthPoint = _healthPoint;
        this.type = _type;
    }

    public void GetDamage(int _damagePoint)
    {
        if (healthPoint > _damagePoint)
        {
            healthPoint -= _damagePoint;
        }
        else
        {
            healthPoint = 0;
            damageDestroy();
        }
    }

    public void damageDestroy()
    {
        // do some thing...
    }
}

// 法术卡类 继承自卡牌类
public class SpellCard : Card
{
    public int rank;
    public string type;
    public string effect;

    public SpellCard(int _id, string _cardName, int _rank, string _type, string _effect) : base(_id, _cardName)
    {
        this.rank = _rank;
        this.type = _type;
        this.effect = _effect;
    }
}
public class ItemCard : Card
{
    public string effect;
    public string type;
    public ItemCard(int _id, string _cardName, string _type, string _effect) : base(_id, _cardName)
    {
        this.type = _type;
        this.effect = _effect;
    }
}