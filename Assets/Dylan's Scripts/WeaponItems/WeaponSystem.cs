using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// System that controls which items are active in the player and also houses all the avaliable items using a weapon number as index
public class WeaponSystem : MonoBehaviour
{
    private GameObject player;
    private Inventory inventory;
    public GameObject[] weapons;
    public GameObject[] WeaponsHeld;
    private int slotSelection = 0;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        WeaponsHeld = new GameObject[4];
    }
    private void FixedUpdate() {
        slotSelection = inventory.slot;

        for (int i = 0; i < WeaponsHeld.Length; i++) {
            if (slotSelection == i && WeaponsHeld[i]) {
                WeaponsHeld[i].SetActive(true);
                BunnyMovement.holdWeapon = true;
            }
            else {
                if (WeaponsHeld[i])
                {
                    WeaponsHeld[i].SetActive(false);
                    BunnyMovement.holdWeapon = false;
                }
                    
            }
        }
    }
}
