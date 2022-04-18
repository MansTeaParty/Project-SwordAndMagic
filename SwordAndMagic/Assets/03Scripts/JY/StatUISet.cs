using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUISet : MonoBehaviour
{
    public Text Status1;   
    public Text Status2;

    private void Update()
    {
        StatUITextSet();
    }

    void StatUITextSet()
    {
        Status1.text =
            "Level" + "\t" + "\n" +
            "Max Hp" + "\t" + PlayerStatus.instance.maxHP + "\n" +
            "Armor" + "\t" + PlayerStatus.instance.armorPoint + "\n" +
            "\n" +
            "AD Power" + "\t" + PlayerStatus.instance.attackDamage + "\n" +
            "AD Speed" + "\t" + PlayerStatus.instance.attackSpeed + "\n" +
            "Skill Power" + "\t" + "\n" +
            "Skill Speed" + "\t" + "\n";

        Status2.text =
            "Penetration" + "\t" + "\n" +
            "Duration" + "\t" + "\n" +
            "Projectile Speed" + "\t" + "\n" +
            "Range" + "\t" + "\n" +
            "\n" +
            "EXP Bonus" + "\t" + "\n" +
            "Gold Bonus" + "\t" + "\n" +
            "Move Speed" + "\t"+ PlayerStatus.instance.movementSpeed + "\n";
    }
}
