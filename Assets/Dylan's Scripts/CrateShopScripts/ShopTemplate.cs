using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
    public GameObject[] StatBars;
    public Button BuyButton;
    private int current = 0;    // stat counter
    public int UpgradeType;
    private PlayerInfo PI;
    private ShopSystem ShopSystem;
    public int CostToUpgrade;
    private void Start() {
        BuyButton.onClick.AddListener(Upgrade);
        PI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        ShopSystem = GameObject.Find("ShopSystem").GetComponent<ShopSystem>();
    }
    private void Upgrade() {
        int CurrentBalance = ShopSystem.balance;

        // Not enough money
        if (CurrentBalance < CostToUpgrade) {
            return;
        }
        // Max upgrades
        if (current == StatBars.Length) {
            return;
        }
        
        ShopSystem.balance -= CostToUpgrade;
        StatBars[current].GetComponent<Image>().color = new Color32(85, 238, 62, 255);
        current++;
        StatUpgrade();
    }

    // Upgrade player info stats
    private void StatUpgrade() {
        switch (UpgradeType) {
            // Max hearts
            case 0:
                PI.SetMaxHearts(1);
                break;
            // Strength
            case 1:
                PI.SetStrength(1);
                break;    
            // Speed
            case 2:
                PI.SetSpeed(0.5f);
                break;
        }
    }
}
