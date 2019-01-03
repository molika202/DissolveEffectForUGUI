using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DissolveRandom
{
    public static string JUSHU="当前局数:";
    public static string FENSHU = "当前分数:";
    public static string CISHU = "剩余次数:";
    static DissolveRandom instance=null;

    public static DissolveRandom INSTANCE
    {
        get
        {
            if (null == instance) instance = new DissolveRandom();
            return instance;
        }
    }
    public int GetTypeRanDom(int min,int max)
    {
        System.Random rdom = new System.Random(GetRandomSeed());
        int value = rdom.Next(min,max);
        return value;
    }
    private int GetRandomSeed()
    {
        byte[] bytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bytes);
        return BitConverter.ToInt32(bytes, 0);
    }
}

