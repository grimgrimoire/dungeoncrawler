﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLoader
{

    public static CharacterModel LoadMonsterData(string id, int level, string alternateName)
    {
        if (id == null)
            return null;
        if (id.Equals(""))
            return null;
        CharacterModel monster = LoadMonsterData(id, level);
        monster.name = "Lv. " + level + " " + alternateName;
        return monster;
    }

    public static CharacterModel LoadMonsterData(string id, int level)
    {
        if (id == null || id.Length == 0)
            return null;
        CharacterModel monster = XmlLoader.LoadFromXmlResource<CharacterModel>("Xml/Monster/" + id);
        monster.level = level;
        monster.name = "Lv. " + level + " " + monster.name;
        MonsterLevelUp(monster);
        return monster;
    }

    static void MonsterLevelUp(CharacterModel monster)
    {
        //monster.attribute.agi += Random.Range(0, MaxIncrease(monster.level, monster.attribute.agi));
        //monster.attribute.str += Random.Range(0, MaxIncrease(monster.level, monster.attribute.str));
        //monster.attribute.wisdom += Random.Range(0, MaxIncrease(monster.level, monster.attribute.wisdom));
        //monster.attribute.cons += Random.Range(0, MaxIncrease(monster.level, monster.attribute.cons));
        //monster.attribute.endurance += Random.Range(0, MaxIncrease(monster.level, monster.attribute.endurance));
        //monster.attribute.intel += Random.Range(0, MaxIncrease(monster.level, monster.attribute.intel));

        //monster.attribute.agi += MaxIncrease(monster.level, monster.attribute.agi);
        //monster.attribute.str += MaxIncrease(monster.level, monster.attribute.str);
        //monster.attribute.wisdom += MaxIncrease(monster.level, monster.attribute.wisdom);
        //monster.attribute.cons += MaxIncrease(monster.level, monster.attribute.cons);
        //monster.attribute.endurance += MaxIncrease(monster.level, monster.attribute.endurance);
        //monster.attribute.intel += MaxIncrease(monster.level, monster.attribute.intel);

        int points = (monster.level - 1) * 3;
        monster.exp += points;
        while (points > 0)
        {
            switch (Random.Range(0, 6))
            {
                case 0:
                    monster.attribute.agi++;
                    break;
                case 1:
                    monster.attribute.str++;
                    break;
                case 2:
                    monster.attribute.cons++;
                    break;
                case 3:
                    monster.attribute.endurance++;
                    break;
                case 4:
                    monster.attribute.wisdom++;
                    break;
                case 5:
                    monster.attribute.intel++;
                    break;
            }
            points--;
        }
    }

    static int MaxIncrease(int level, int val)
    {
        int max = 0;
        max = Mathf.RoundToInt(level * (val / 10f));
        return max;
    }

    public static void SaveMonster(CharacterModel model)
    {
        XmlSaver.SaveXmlToFile<CharacterModel>("/" + model.name + ".xml", model);
    }
}
