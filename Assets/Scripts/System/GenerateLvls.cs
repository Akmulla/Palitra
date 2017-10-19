using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

using System.Linq;


[System.Serializable]
public struct LvlParams
{
    public float defaultDist;
    public Vector2 dist;
    public Vector2 speed;
    public Vector2 chngClrDist;
    public Vector2 chngClrTime;
    public Vector2 blockSpeed;
    public Vector2 blockCount;
    public Vector2 tapAmount1;
    public Vector2 tapSlow1;
    public Vector2 tapAmount2;
    public Vector2 tapSlow2;
    public Vector2 tapAmount3;
    public Vector2 tapSlow3;
    public Vector2 comboSlow3;
    public Vector2 comboSlow4;
    public Vector2 comboSlow5;
}

public class GenerateLvls : MonoBehaviour
{
    public int cycles=1;
    public float beginCalcDist;
    public float availTime = 30.0f;
    public string path = "Assets/LvlData/NewLvls";
    public LvlParams topParams;
    public LvlParams botParams;


    public LvlParams maxTopParams;
    public LvlParams maxBotParams;

    void Start()
    {

        //int lvl_count = (int)((start_params.dist.x - end_params.dist.x) / 0.1f);
        //int lvl_count = (int)((start_params.default_dist - end_params.default_dist) / 0.1f);
        //int lvl_count = (int)((start_params.default_dist - end_params.default_dist) / 0.1f);

        
        int cyclesPassed=0;
        bool down = true;
        float curStep = beginCalcDist;
        float t = Mathf.InverseLerp(topParams.defaultDist, botParams.defaultDist,
            beginCalcDist);


        //int lvl_count = (int)((top_params.default_dist - bot_params.default_dist) / 0.1f)+
        //    (int)((begin_calc_dist - bot_params.default_dist) / 0.1f)+
        //    (int)((top_params.default_dist - bot_params.default_dist) / 0.1f)*2*(cycles-1);

        int lvlCount = 1000;

        List <string> res = CalcCombinations(lvlCount);
        //print(lvl_count);
        //print(lvl_count);
        int lvlNumb = 0;
        LvlType lvlType = LvlType.SpeedIncr;

        float difficulty = 0;
        LvlParams newBot = botParams;
        LvlParams newTop = topParams;
        //while ((cycles_passed < cycles)&& (lvl_numb <lvl_count))
        while (lvlNumb < lvlCount)
        {
            
            LvlData lvl = ScriptableObject.CreateInstance<LvlData>();
            
            if (lvlNumb%10==0)
            {
                difficulty += 0.05f;

                newBot = CalcLvlParams(botParams, maxBotParams, difficulty);
                newTop = CalcLvlParams(topParams, maxTopParams, difficulty);
                if (difficulty >= 1.0f)
                    difficulty = 1.0f;
            }
            


            lvl.lvlType = lvlType;
            //float t = (float)lvl_count / (float)lvl_count;

            //CalcLineParams(lvl, t,bot_params,top_params);
            CalcLineParams(lvl, t, newBot, newTop);
            SetLineCount(lvl, res[lvlNumb],lvlNumb);
            CalcSteps(lvl, t, lvlType);

            AssetDatabase.CreateAsset(lvl, path + "/Lvl_" + lvlNumb + ".asset");

            lvlType++;
            if (lvlType == LvlType.Count)
                lvlType = 0;

            AssetDatabase.SaveAssets();


            if (down)
            {
                curStep -= 0.1f;
            }
            else
            {
                curStep += 0.1f;
            }

            if (curStep <= newBot.defaultDist+0.05f)
            {
                down = false;
            }

            if (curStep >= newTop.defaultDist-0.05f)
            {
                down = true;
                cyclesPassed++;
            }

            t = Mathf.InverseLerp(newTop.defaultDist, newBot.defaultDist,
             curStep);
            lvlNumb++;
        }

        
        
    }

    void SetLineCount(LvlData lvl, string lineInfo,int lvlNumb)
    {
        lvl.lineProp.count = 0;
        lvl.switchProp.count = 0;
        lvl.blockProp.count = 0;
        lvl.multipleProp1Part.count = 0;
        lvl.multipleProp2Parts.count = 0;
        lvl.multipleProp3Parts.count = 0;
        lvl.comboProp3Parts.count = 0;
        lvl.comboProp4Parts.count = 0;
        lvl.comboProp5Parts.count = 0;
        lvl.totalLineCount = 0;
        float lvlTime = 0.0f;
        //float dist = Edges.topEdge - Edges.botEdge;
        float dist = lvl.dist;

        if (dist < 0.01f)
        {
            dist = 0.01f;
            print("too small dist");
        }

        while (lvlTime<availTime)
        {
            for (int i = 0; i < lineInfo.Length; i++)
            {
                switch (lineInfo[i])
                {
                    case '1':
                        lvl.lineProp.count +=1;
                        lvlTime += dist / lvl.speed;
                        break;

                    case '2':
                        lvl.switchProp.count += 1;
                        lvlTime += dist / lvl.speed;
                        break;

                    case '3':
                        lvl.blockProp.count += 1;
                        lvlTime += dist / lvl.speed;
                        break;

                    case '4':
                        int typeMult = 1;
                        if ((lvlNumb>=19)&& (lvlNumb <= 38))
                        {
                            typeMult = Random.Range(1, 3);
                        }
                        
                        if (lvlNumb >= 39)
                        {
                            typeMult = Random.Range(1, 4);
                        }

                        switch (typeMult)
                        {
                            case 1:
                                lvl.multipleProp1Part.count += 1;
                                lvlTime += dist / lvl.multipleProp1Part.slowing;
                                break;
                            case 2:
                                lvl.multipleProp2Parts.count += 1;
                                lvlTime += dist / lvl.multipleProp2Parts.slowing;
                                break;
                            case 3:
                                lvl.multipleProp3Parts.count += 1;
                                lvlTime += dist / lvl.multipleProp3Parts.slowing;
                                break;
                        }
                        //lvl.multiple_prop_1_part.count += 1;
                        //lvl_time += dist / lvl.multiple_prop_1_part.slowing;
                        break;

                    case '5':
                        int typeComb = 3;
                        if ((lvlNumb >= 39) && (lvlNumb <= 58))
                        {
                            typeComb = Random.Range(3, 5);
                        }

                        if (lvlNumb >= 59)
                        {
                            typeComb = Random.Range(3, 6);
                        }
                        switch (typeComb)
                        {
                            case 3:
                                lvl.comboProp3Parts.count += 1;
                                lvlTime += dist / lvl.comboProp3Parts.slowing;
                                break;
                            case 4:
                                lvl.comboProp4Parts.count += 1;
                                lvlTime += dist / lvl.comboProp4Parts.slowing;
                                break;
                            case 5:
                                lvl.comboProp5Parts.count += 1;
                                lvlTime += dist / lvl.comboProp5Parts.slowing;
                                break;
                        }
                        //lvl.combo_prop_3_parts.count += 1;
                        //lvl_time += dist / lvl.combo_prop_3_parts.slowing;
                        break;

                    default:
                        print("error");
                        break;
                }
                lvl.totalLineCount++;
            }
        }
       
    }

    List<string> CalcCombinations(int lvlCount)
    {
        List<string> result = new List<string>();
        List<string> baseComb = GetCombination(new List<int> { 1, 2, 3, 4, 5 });


        //туториальные уровни
        result.Add("1");
        result.Add("12");
        result.Add("13");
        result.Add("14");
        result.Add("15");



        for (int i=3;i<=4;i++)
        {
            foreach (string comb in baseComb)
            {
                if ((comb.Length==i)&&(comb.Contains("1")))
                {
                    result.Add(comb);
                }
            }
        }
        
        result.Add("12345");
        result.Add("12345");
        result.Add("12345");
        result.Add("12345");
        result.Add("12345");
        ////////////////////////////

        //print(result.Count);
        //List<string> comb_pool = new List<string>(base_comb);
        while (result.Count< lvlCount)
        {
            for (int i = 2; i <= 5; i++)
            {
                List<string> combPool = GenTempPool(baseComb, i);

                for (int j = 0; j < 5; j++)
                {
                    if (combPool.Count != 0)
                    {
                        int k = Random.Range(0, combPool.Count);
                        result.Add(combPool[k]);
                        combPool.RemoveAt(k);
                    }
                    else
                    {
                        combPool = GenTempPool(baseComb, i);
                    }
                }
                //if (comb_pool.Any(s => s.Length == i))
                //{

                //}
            }
        }
        
        
        
        //comb_pool[0] = "0";
        //base_comb[0] = "1";

        //print(comb_pool[0]);
        //print(base_comb[0]);
        return result;
    }

    List<string> GenTempPool(List<string> baseComb, int length)
    {
        List<string> combPool = new List<string>();
        for (int j = 0; j < baseComb.Count; j++)
        {
            if (baseComb[j].Length == length)
            {
                combPool.Add(baseComb[j]);
            }
        }

        return combPool;
    }

    List<string> GetCombination(List<int> list)
    {
        List<string> result = new List<string>();
        double count = Mathf.Pow(2, list.Count);
        for (int i = 1; i <= count - 1; i++)
        {
            string newStr = "";
            result.Add(newStr);
            string str = System.Convert.ToString(i, 2).PadLeft(list.Count, '0');
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == '1')
                {
                   // print(list[j]);
                    result[i-1] += list[j];
                }
            }
            //print("next");
           // Console.WriteLine();
        }

        return result;
    }

    void CalcSteps(LvlData lvl,float t,LvlType lvlType)
    {
        lvl.stepDist = 0;
        lvl.stepSpeed = 0;
        float minSpeed;
        float maxSpeed;
        float minDist;
        float maxDist;


        switch (lvlType)
        {
            case LvlType.SpeedIncr:
                minSpeed = lvl.speed;
                maxSpeed = Mathf.Lerp(topParams.speed.y, botParams.speed.y, t);
                lvl.stepSpeed = Mathf.Abs(maxSpeed - minSpeed) / lvl.totalLineCount;
                break;

            case LvlType.DistDecr:
                minDist = Mathf.Lerp(topParams.dist.y, botParams.dist.y, t);
                maxDist = lvl.dist;
                lvl.stepDist =  - Mathf.Abs(minDist - maxDist) / lvl.totalLineCount;
                break;

            case LvlType.SpeedIncrDistIncr:
                minSpeed = lvl.speed;
                maxSpeed = Mathf.Lerp(topParams.speed.y, botParams.speed.y, t);
                lvl.stepSpeed = Mathf.Abs(maxSpeed - minSpeed) / lvl.totalLineCount;

                minDist = Mathf.Lerp(topParams.dist.y, botParams.dist.y, t);
                maxDist = lvl.dist;
                lvl.stepDist = Mathf.Abs(minDist - maxDist) / lvl.totalLineCount;
                break;

            case LvlType.SpeedDecrDistDecr:
                minSpeed = lvl.speed;
                maxSpeed = Mathf.Lerp(topParams.speed.y, botParams.speed.y, t);
                lvl.stepSpeed = -Mathf.Abs(maxSpeed - minSpeed) / lvl.totalLineCount;

                minDist = Mathf.Lerp(topParams.dist.y, botParams.dist.y, t);
                maxDist = lvl.dist;
                lvl.stepDist = -Mathf.Abs(minDist - maxDist) / lvl.totalLineCount;
                break;

            case LvlType.SpeedIncrDistIncrHalf:
                minSpeed = lvl.speed;
                maxSpeed = Mathf.Lerp(topParams.speed.y, botParams.speed.y, t);
                lvl.stepSpeed = Mathf.Abs(maxSpeed - minSpeed) / (lvl.totalLineCount/2);

                minDist = Mathf.Lerp(topParams.dist.y, botParams.dist.y, t);
                maxDist = lvl.dist;
                lvl.stepDist = Mathf.Abs(minDist - maxDist) / (lvl.totalLineCount/2);
                break;

            case LvlType.SpeedDecrDistDecrHalf:
                minSpeed = lvl.speed;
                maxSpeed = Mathf.Lerp(topParams.speed.y, botParams.speed.y, t);
                lvl.stepSpeed = -Mathf.Abs(maxSpeed - minSpeed) / (lvl.totalLineCount / 2);

                minDist = Mathf.Lerp(topParams.dist.y, botParams.dist.y, t);
                maxDist = lvl.dist;
                lvl.stepDist = -Mathf.Abs(minDist - maxDist) / (lvl.totalLineCount / 2);
                break;

        }
        
    }

    void CalcLineParams(LvlData lvl,float t,LvlParams botParams,LvlParams topParams)
    {
        lvl.lineProp = new LineProp();
        switch (lvl.lvlType)
        {
            case LvlType.SpeedIncr:
                lvl.speed = Mathf.Lerp(topParams.speed.x, botParams.speed.x, t);
                //lvl.dist = Mathf.Lerp(start_params.dist.x, end_params.dist.x, t);
                lvl.dist = Mathf.Lerp(topParams.defaultDist, 
                    botParams.defaultDist, t);
                break;

            case LvlType.DistDecr:
                lvl.speed = Mathf.Lerp(topParams.speed.x, botParams.speed.x, t);
                lvl.dist = Mathf.Lerp(topParams.dist.y, botParams.dist.y, t);
                break;

            case LvlType.SpeedIncrDistIncr:
                lvl.speed = Mathf.Lerp(topParams.speed.x, botParams.speed.x, t);
                lvl.dist = Mathf.Lerp(topParams.dist.x, botParams.dist.x, t);
                break;

            case LvlType.SpeedDecrDistDecr:
                lvl.speed = Mathf.Lerp(topParams.speed.y, botParams.speed.y, t);
                lvl.dist = Mathf.Lerp(topParams.dist.y, botParams.dist.y, t);
                break;

            case LvlType.SpeedIncrDistIncrHalf:
                lvl.speed = Mathf.Lerp(topParams.speed.x, botParams.speed.x, t);
                lvl.dist = Mathf.Lerp(topParams.dist.x, botParams.dist.x, t);
                break;

            case LvlType.SpeedDecrDistDecrHalf:
                lvl.speed = Mathf.Lerp(topParams.speed.y, botParams.speed.y, t);
                lvl.dist = Mathf.Lerp(topParams.dist.y, botParams.dist.y, t);
                break;

        }
        //lvl.dist= Mathf.Lerp(start_params.dist.x, end_params.dist.x, t);

        

        //lvl.speed = Mathf.Lerp(start_params.speed.x, end_params.speed.x, t);
        

        lvl.switchProp = new SwitchProp();
        lvl.switchProp.dist = Mathf.Lerp(topParams.chngClrDist.x, botParams.chngClrDist.x, t);
        lvl.switchProp.timeToChange = Mathf.Lerp(topParams.chngClrTime.x, botParams.chngClrTime.x, t);

        lvl.blockProp = new BlockProp();
        lvl.blockProp.speed = Mathf.Lerp(topParams.blockSpeed.x, botParams.blockSpeed.x, t);
        lvl.blockProp.blockCount = (int)Mathf.Lerp(topParams.blockCount.x, botParams.blockCount.x, t);

        lvl.multipleProp1Part = new MultipleProp();
        lvl.multipleProp1Part.minTaps = (int)Mathf.Lerp(topParams.tapAmount1.x, botParams.tapAmount1.x, t);
        lvl.multipleProp1Part.slowing = Mathf.Lerp(topParams.tapSlow1.x, botParams.tapSlow1.x, t);

        lvl.multipleProp2Parts = new MultipleProp();
        lvl.multipleProp2Parts.minTaps = (int)Mathf.Lerp(topParams.tapAmount2.x, botParams.tapAmount2.x, t);
        lvl.multipleProp2Parts.slowing = Mathf.Lerp(topParams.tapSlow2.x, botParams.tapSlow2.x, t);

        lvl.multipleProp3Parts = new MultipleProp();
        lvl.multipleProp3Parts.minTaps = (int)Mathf.Lerp(topParams.tapAmount3.x, botParams.tapAmount3.x, t);
        lvl.multipleProp3Parts.slowing = Mathf.Lerp(topParams.tapSlow3.x, botParams.tapSlow3.x, t);

        lvl.comboProp3Parts = new ComboProp();
        lvl.comboProp3Parts.slowing= Mathf.Lerp(topParams.comboSlow3.x, botParams.comboSlow3.x, t);

        lvl.comboProp4Parts = new ComboProp();
        lvl.comboProp4Parts.slowing = Mathf.Lerp(topParams.comboSlow4.x, botParams.comboSlow4.x, t);

        lvl.comboProp5Parts = new ComboProp();
        lvl.comboProp5Parts.slowing = Mathf.Lerp(topParams.comboSlow5.x, botParams.comboSlow5.x, t);
    }


    LvlParams CalcLvlParams(LvlParams def,LvlParams max,float t)
    {
        LvlParams result = new LvlParams();

        result.defaultDist = Mathf.Lerp(def.defaultDist, max.defaultDist, t);

        result.dist.x = Mathf.Lerp(def.dist.x, max.dist.x, t);
        result.dist.y = Mathf.Lerp(def.dist.y, max.dist.y, t);

        result.speed.x = Mathf.Lerp(def.speed.x, max.speed.x, t);
        result.speed.y = Mathf.Lerp(def.speed.y, max.speed.y, t);

        result.chngClrDist.x = Mathf.Lerp(def.chngClrDist.x, max.chngClrDist.x, t);
        result.chngClrDist.y = Mathf.Lerp(def.chngClrDist.y, max.chngClrDist.y, t);

        result.chngClrTime.x = Mathf.Lerp(def.chngClrTime.x, max.chngClrTime.x, t);
        result.chngClrTime.y = Mathf.Lerp(def.chngClrTime.y, max.chngClrTime.y, t);

        result.blockSpeed.x = Mathf.Lerp(def.blockSpeed.x, max.blockSpeed.x, t);
        result.blockSpeed.y = Mathf.Lerp(def.blockSpeed.y, max.blockSpeed.y, t);

        result.blockCount.x = Mathf.Lerp(def.blockCount.x, max.blockCount.x, t);
        result.blockCount.y = Mathf.Lerp(def.blockCount.y, max.blockCount.y, t);

        result.tapAmount1.x = Mathf.Lerp(def.tapAmount1.x, max.tapAmount1.x, t);
        result.tapAmount1.y = Mathf.Lerp(def.tapAmount1.y, max.tapAmount1.y, t);

        result.tapSlow1.x = Mathf.Lerp(def.tapSlow1.x, max.tapSlow1.x, t);
        result.tapSlow1.y = Mathf.Lerp(def.tapSlow1.y, max.tapSlow1.y, t);

        result.tapAmount2.x = Mathf.Lerp(def.tapAmount2.x, max.tapAmount2.x, t);
        result.tapAmount2.y = Mathf.Lerp(def.tapAmount2.y, max.tapAmount2.y, t);

        result.tapSlow2.x = Mathf.Lerp(def.tapSlow2.x, max.tapSlow2.x, t);
        result.tapSlow2.y = Mathf.Lerp(def.tapSlow2.y, max.tapSlow2.y, t);

        result.tapAmount3.x = Mathf.Lerp(def.tapAmount2.x, max.tapAmount2.x, t);
        result.tapAmount3.y = Mathf.Lerp(def.tapAmount2.y, max.tapAmount2.y, t);

        result.tapSlow3.x = Mathf.Lerp(def.tapSlow3.x, max.tapSlow3.x, t);
        result.tapSlow3.y = Mathf.Lerp(def.tapSlow3.y, max.tapSlow3.y, t);

        result.comboSlow3.x = Mathf.Lerp(def.comboSlow3.x, max.comboSlow3.x, t);
        result.comboSlow3.y = Mathf.Lerp(def.comboSlow3.y, max.comboSlow3.y, t);

        result.comboSlow4.x = Mathf.Lerp(def.comboSlow4.x, max.comboSlow4.x, t);
        result.comboSlow4.y = Mathf.Lerp(def.comboSlow4.y, max.comboSlow4.y, t);

        result.comboSlow5.x = Mathf.Lerp(def.comboSlow5.x, max.comboSlow5.x, t);
        result.comboSlow5.y = Mathf.Lerp(def.comboSlow5.y, max.comboSlow5.y, t);

        return result;
    }
}

#endif
