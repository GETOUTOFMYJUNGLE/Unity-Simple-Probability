using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour
{
    [Header("���Ʀ���")]
    [Tooltip("���ƷU�hŪ���U�C���U���")]
    public float Sample_size;//���ƺ�h�֦�

    public enum Type
    {
        �H���q�D�w�X�D�ؤ��D�ح��|���v,
        �c�l����y�íY���O���y�N���z�������y�����榸��,
    }
    [Header("�����D��")]
    public Type TheEase;

    [Header("�H���q�D�w�X�D�ؤ��D�ح��|���v")]
    [Tooltip("�D�w�`���D�q")]
    public int band;//�D�w���D�q
    int[] Test_paper;//�ը�
    [Tooltip("��@�ը������D�q")]
    public int Number_of_questions;//��@�ը������D�q
    int all_yes = 0;//�`�����Ʀ���

    [Header("�c�l����y�íY���O���y�N���z�������y�����榸��")]
    [Tooltip("�@���X���y")]
    public int Howmanyball;//�@���X���y
    [Tooltip("���X�����y")]
    public int HowmanyRed;//���X�����y
    int Run_time = 0;//�]�ĴX��
    void Start()
    {
        if (TheEase == Type.�c�l����y�íY���O���y�N���z�������y�����榸��)
            Expected_value_of_the_ball();
        else
            Repeat_probability();
    }

    void Repeat_probability()//�⭫�ƾ��v
    {
        for (int a = 1; a <= Sample_size; a++)
        {
            bool repeat = false;
            //�b��
            Test_paper = new int[Number_of_questions];
            //���D�w
            for (int x = 0; x <= Test_paper.Length - 1; x++)
            {
                Test_paper[x] = Random.Range(1, band + 1);
            }
            //�ݦ��S������
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
        //��̫ܳᵲ�G
        Debug.Log("���ƾ��v�G" + all_yes * 100 / Sample_size + " %");
    }
    void Expected_value_of_the_ball()//��y�y�����
    {
        for (int a = 1; a <= Sample_size; a++)
        {
            bool GetRed = false;
            int[] Box = new int[Howmanyball];//�@���X���y
            for (int x = 0; x <= Box.Length - 1; x++)
            {
                if (x + 1 <= HowmanyRed)
                    Box[x] = 2;
                else
                    Box[x] = 1;
            }
            while (!GetRed)//�S������y�N�~��
            {
                Run_time += 1;
                //�H�K��@���y
                int rand = Random.Range(0, Box.Length);
                while (Box[rand] == 0)//�u�����o���y
                {
                    rand = Random.Range(0, Box.Length);
                }
                if (Box[rand] == 1)//�ղy
                    Box[rand] = 0;//���_��
                else
                    GetRed = true;
            }
        }
        //��̫ܳᵲ�G
        Debug.Log("���榸�ơG" + Run_time / Sample_size);
    }
}
