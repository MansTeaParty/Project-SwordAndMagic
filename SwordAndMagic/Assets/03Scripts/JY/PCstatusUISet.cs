using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCstatusUISet : MonoBehaviour
{
    public PlayerCtrl _playerctrl;
    public PlayerStatus _playerstatus;
    
    public Text PlayerLevel;
    public Text ElementLevel;
    public Text PCstatus;
    public Text SP;

    private int statSTR = 0;
    private int statDEX = 0;
    private int statINT = 0;
    private int statLUK = 0;
    private int statHP = 0;
    private int statMP = 0;


    void Update()
    {
        PlayerLevel.text = "Level: " + _playerctrl.playerLevel;
        ElementLevel.text = "속성레벨: ";

        PCstatus.text =
        "STR : " + " " + statSTR + "\n" +
        "- attackDamage : " + " " + _playerstatus.attackDamage + "\n" +
        "- knockBack : " + " " +_playerstatus.knockBack + "\n" +
        "\n" +
        "DEX : " + " " + statDEX + "\n" +
        "- attackSpeed : " + " " + _playerstatus.attackSpeed + "\n" +
        "- Movement : " + " " + _playerstatus.movementSpeed + "\n" +
        "\n" +
        "INT : " + " " + statINT + "\n" +
        "- Duration : " + " " + "\n" +
        "- increaseMpValue : " + " " + "\n" +
        "- projectileCoolReduction : " + " " + "\n" +
        "\n" +
        "LUK : " + " " + statLUK + "\n" +
        "- criticalBonus : " + " " + "\n" +
        "- criticalDamage : " + " " + "\n" +
        "- dropBonus : " + " " + "\n" +
        "\n" +
        "HP : " + " " + statHP + "\n" +
        "- maxHp : 25 : " + " " + _playerstatus.maxHP + "\n" +        
        "- armorPoint : " + " " + _playerstatus.armorPoint +"\n" +        
        "- dodgeCoolReduction : " + " " + "\n" +
        "\n" +
        "MP :" + " " + statMP + "\n" +
        "- projectileSpeed : " + " " + _playerstatus.projectileSpeed +"\n" +            
        "- projectileScale : " + " " + _playerstatus.projectileScale + "\n" +
        "- penetration : " + _playerstatus.penetration + " ";

        SP.text = "SP: " + " " + _playerctrl.PlayerSP;
    }
    public void STR()
    {
        //_playerstatus.attackDamage += (int)(_playerstatus.attackDamage * 0.05);
        _playerstatus.attackDamage += 1;
        statSTR += 1;
        _playerstatus.knockBack += 0.5f;
        _playerctrl.PlayerSP -= 1;
        Debug.Log("STR + 1");
    }
    public void DEX()
    {        
        statDEX += 1;
        _playerstatus.attackSpeed += 1.0f;
        _playerstatus.movementSpeed += 1.0f;
        _playerctrl.PlayerSP -= 1;
        Debug.Log("DEX + 1");
    }
    public void INT()
    {
        statINT += 1;
        Debug.Log("지능 수치 1마다 투사체 속도 + 5%");
        Debug.Log("지능 수치 1마다 투사체 크기 + 5%");
        Debug.Log("지능 수치 2마다 투사체 관통 횟수 + 1");
        _playerctrl.PlayerSP -= 1;
        Debug.Log("INT + 1");
    }
    public void LUK()
    {
        statLUK += 1;
        Debug.Log("행운 수치 1마다 치명타 확률 + 3%");
        Debug.Log("행운 수치 1마다 치명타 피해 + 5%");
        Debug.Log("행운 수치 2마다 드랍 확률 + 10%");
        _playerctrl.PlayerSP -= 1;
        Debug.Log("LUK + 1");
    }
    public void HP()
    {
        statHP += 1;
        _playerstatus.maxHP += 5;
        _playerstatus.armorPoint += 1;     
        Debug.Log("체력 수치 2마다 구르기 쿨감 5%");
        _playerctrl.PlayerSP -= 1;
        Debug.Log("HP + 1");
    }
    public void MP()
    {
        statMP += 1;
        _playerstatus.projectileSpeed += 1.0f;
        _playerstatus.projectileScale += 1.0f;
        _playerstatus.penetration += 1;
        _playerctrl.PlayerSP -= 1;
        Debug.Log("MP + 1");
    }
}
