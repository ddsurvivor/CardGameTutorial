using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnStateDisplayer : MonoBehaviour
{
    public Text turnText;
    // Start is called before the first frame update
    void Start()
    {
        BattleManager.Instance.phaseChangeEvent.AddListener(UpdateTurnText);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateTurnText();  
    }

    void UpdateTurnText()
    {
        turnText.text = BattleManager.Instance.currentPhase.ToString();
    }
}
