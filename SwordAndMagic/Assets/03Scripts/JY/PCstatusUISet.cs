using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCstatusUISet : MonoBehaviour
{
    public PlayerCtrl playerCtrl;
    public PlayerStatus playerStatus;
    
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


    //플레이어스탯+버프스탯으로 분리할지를 생각중

    void Update()
    {
        PlayerLevel.text = "Level: " + playerCtrl.playerLevel;
        ElementLevel.text = "속성레벨: ";

        PCstatus.text =
        "STR : " + " " + statSTR + "\n" +
        "- attackDamage : " + " " + playerStatus.getAttackDamage() + "\n" +
        "- knockBack : " + " " +playerStatus.knockBack + "\n" +
        "\n" +
        "DEX : " + " " + statDEX + "\n" +
        "- attackSpeed : " + " " + playerStatus.getAttackSpeed() + "\n" +
        "- Movement : " + " " + playerStatus.getMovementSpeed() + "\n" +
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
        "- maxHp : 25 : " + " " + playerStatus.getMaxHP() + "\n" +        
        "- armorPoint : " + " " + playerStatus.getArmorPoint() +"\n" +        
        "- dodgeCoolReduction : " + " " + "\n" +
        "\n" +
        "MP :" + " " + statMP + "\n" +
        "- projectileSpeed : " + " " + playerStatus.projectileSpeed +"\n" +            
        "- projectileScale : " + " " + playerStatus.projectileScale + "\n" +
        "- penetration : " + playerStatus.penetration + " ";

        SP.text = "SP: " + " " + playerStatus.playerSP;
    }
    public void STR()
    {
        //_playerstatus.attackDamage += (int)(_playerstatus.attackDamage * 0.05);
        playerStatus.addAttackDamage(1);
        statSTR += 1;
        playerStatus.knockBack += 0.5f;
        playerStatus.playerSP -= 1;
        Debug.Log("STR + 1");
    }
    public void DEX()
    {        
        statDEX += 1;
        playerStatus.addAttackSpeed(1.0f);
        playerStatus.addMovementSpeed(1.0f);
        playerStatus.playerSP -= 1;
        Debug.Log("DEX + 1");
    }
    public void INT()
    {
        statINT += 1;
        Debug.Log("지능 수치 1마다 투사체 속도 + 5%");
        Debug.Log("지능 수치 1마다 투사체 크기 + 5%");
        Debug.Log("지능 수치 2마다 투사체 관통 횟수 + 1");
        playerStatus.playerSP -= 1;
        Debug.Log("INT + 1");
    }
    public void LUK()
    {
        statLUK += 1;
        Debug.Log("행운 수치 1마다 치명타 확률 + 3%");
        Debug.Log("행운 수치 1마다 치명타 피해 + 5%");
        Debug.Log("행운 수치 2마다 드랍 확률 + 10%");
        playerStatus.playerSP -= 1;
        Debug.Log("LUK + 1");
    }
    public void HP()
    {
        statHP += 1;
        playerStatus.addMaxMP(5);
        playerStatus.addArmorPoint(1);     
        Debug.Log("체력 수치 2마다 구르기 쿨감 5%");
        playerStatus.playerSP -= 1;
        Debug.Log("HP + 1");
    }
    public void MP()
    {
        statMP += 1;
        playerStatus.projectileSpeed += 1.0f;
        playerStatus.projectileScale += 1.0f;
        playerStatus.penetration += 1;
        playerStatus.playerSP -= 1;
        Debug.Log("MP + 1");
    }
}
