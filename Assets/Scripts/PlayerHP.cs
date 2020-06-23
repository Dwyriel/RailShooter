using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] Player player;
    Text playerHP;

    void Start()
    {
        playerHP = GetComponent<Text>();
        playerHP.text = player.HP.ToString();
    }

    public void ModifyHP()
    {
        player.HP -= 1;
        playerHP.text = player.HP.ToString();
    }
}
