﻿using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Runtime.InteropServices;

public class GameStartUI : MonoBehaviour {

    public Button beginBtn;
    public GameUI game;
    ResLoader resLoader = new ResLoader();
    void Start()
    {
        beginBtn.onClick.AddListener(() => { game.gameObject.SetActive(true); gameObject.SetActive(false); });
        TextAsset str = resLoader.LoadSync<TextAsset>("resources://data");
        GetJson json = JsonUtility.FromJson<GetJson>(str.text);
        SetData(json.y, json.m, json.d, json.w);
        Log.E(json.w);
    }
    [DllImport("__Internal")]
    private static extern void SetData(int y,int m,int d,string w);
    
}
public class GetJson
{
    public int y,m,d;
    public string w;
}


// Unity调用iOS原生函数非常简单，只需要两步即可完成：
// 
// 
// 
// 第一步：在Unity中声明函数并调用：
// 
// 声明：
// 
// 		#if UNITY_IPHONE  
// 		 [DllImport("__Internal")]  
// 		 private static extern void 函数名(参数类型* 参数名）;  
// 		#endif  
// eg：
// 
// 		#if UNITY_IPHONE  
// 		 [DllImport("__Internal")]  
// 		  private static extern void SavePhoto(char * picPath）;  
// 		#endif  
// 
// 
// 
// 
// 
// Unity调用：
// 
//          IEnumerator unitySaveLocalPic(char * picPath){
// 
//               SavePhoto(picPath);
// 
//          }
// 
// 
// 
// 第二步：在iOS中实现函数：
// 
// 
// 声明：
//         extern “C” void 函数名（参数类型* 参数名）
// 
// iOS原生实现图片保存到相册eg：
// 
// extern "C" void SavePhoto(char *picPath){
// 
//         NSString *pathStr = [NSString stringWithUTF8String:picPath];
// 
//         UIImage* image = [UIImage imageWithContentsOfFile:pathStr];
// 
//         UIImageWriteToSavedPhotosAlbum(image, mySelf , @selector(image:didFinishSavingWithError:contextInfo:), NULL);
// 
//         }
// 
// 
// 
// -(void)image:(UIImage*)image didFinishSavingWithError:(NSError*)error contextInfo:(void*)contextInfo
// 
//       {
//         NSString* result;
//         if(error)
//         {
//            result = @"图片保存到相册失败!";
//         }
//         else
//         {
//            result = @"图片保存到相册成功!";
//         }
// 
//         NSLog(@"result---->%@", result);
// 
//       }
