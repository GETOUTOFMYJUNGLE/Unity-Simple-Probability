using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour
{
    [Header("重複次數")]
    [Tooltip("次數愈多讀取愈慢但愈精準")]
    public float Sample_size;//重複算多少次

    public enum Type
    {
        隨機從題庫出題目之題目重疊機率,
        箱子抽紅球並若不是紅球就捏爆之抽到紅球的期望次數,
    }
    [Header("哪個題目")]
    public Type TheEase;

    [Header("隨機從題庫出題目之題目重疊機率")]
    [Tooltip("題庫總問題量")]
    public int band;//題庫問題量
    int[] Test_paper;//試卷
    [Tooltip("單一試卷的問題量")]
    public int Number_of_questions;//單一試卷的問題量
    int all_yes = 0;//總有重複次數

    [Header("箱子抽紅球並若不是紅球就捏爆之抽到紅球的期望次數")]
    [Tooltip("共有幾顆球")]
    public int Howmanyball;//共有幾顆球
    [Tooltip("有幾顆紅球")]
    public int HowmanyRed;//有幾顆紅球
    int Run_time = 0;//跑第幾次
    void Start()
    {
        if (TheEase == Type.箱子抽紅球並若不是紅球就捏爆之抽到紅球的期望次數)
            Expected_value_of_the_ball();
        else
            Repeat_probability();
    }

    void Repeat_probability()//算重複機率
    {
        for (int a = 1; a <= Sample_size; a++)
        {
            bool repeat = false;
            //淨空
            Test_paper = new int[Number_of_questions];
            //給題庫
            for (int x = 0; x <= Test_paper.Length - 1; x++)
            {
                Test_paper[x] = Random.Range(1, band + 1);
            }
            //看有沒有重複
            for (int c = 0; c <= Test_paper.Length - 1; c++)
            {
                for (int d = 0; d <= Test_paper.Length - 1; d++)
                {
                    if (c != d && Test_paper[c] == Test_paper[d])
                    {
                        repeat = true;
                    }
                }
            }
            if (repeat)
                all_yes += 1;
        }
        //顯示最後結果
        Debug.Log("重複機率：" + all_yes * 100 / Sample_size + " %");
    }
    void Expected_value_of_the_ball()//算球球期望值
    {
        for (int a = 1; a <= Sample_size; a++)
        {
            bool GetRed = false;
            int[] Box = new int[Howmanyball];//共有幾顆球
            for (int x = 0; x <= Box.Length - 1; x++)
            {
                if (x + 1 <= HowmanyRed)
                    Box[x] = 2;
                else
                    Box[x] = 1;
            }
            while (!GetRed)//沒拿到紅球就繼續
            {
                Run_time += 1;
                //隨便抓一顆球
                int rand = Random.Range(0, Box.Length);
                while (Box[rand] == 0)//真的有這顆球
                {
                    rand = Random.Range(0, Box.Length);
                }
                if (Box[rand] == 1)//白球
                    Box[rand] = 0;//拿起來
                else
                    GetRed = true;
            }
        }
        //顯示最後結果
        Debug.Log("期望次數：" + Run_time / Sample_size);
    }
}
