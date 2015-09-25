using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class AppFlood {
	public static int BANNER_LARGE = 10;
    public static int BANNER_MIDDLE = 11;
    public static int BANNER_SMALL = 12;
	
	public static int BANNER_TOP = 10;
	public static int BANNER_BOTTOM = 12;

    public static int PANEL_LANDSCAPE = 20;
    public static int PANEL_PORTRAIT= 21;
    
    public static int AD_NONE 		= 0;
    public static int AD_BANNER 		= 1;
    public static int AD_PANEL 		= 2;
    public static int AD_FULLSCREEN 	= 4;
    public static int AD_LIST 		= 8;
    public static int AD_DATA 		= 16;

	public static int AD_INTERSTITIAL = 128;
    public static int AD_ALL = AD_BANNER|AD_PANEL|AD_FULLSCREEN|AD_LIST|AD_DATA|AD_INTERSTITIAL;
    
    public static int PANEL_TOP = 0;
    public static int PANEL_BOTTOM = 1; 
	
    public static int LIST_GAME = 0;
    public static int LIST_APP = 1;
    public static int LIST_ALL = 2;
    public static int LIST_TAB_GAME = 3;
    public static int LIST_TAB_APP = 4;

    public static int NOTIFICATION_STYLE_TEXT = 1;
    public static int NOTIFICATION_STYLE_BANNER = 2;

	private static AndroidJavaClass appflood = new AndroidJavaClass("com.appflood.AppFlood");
	
	
	/**
     * initialize appflood
     * 
     * @param appID appKey
     * @param secretKey secretKey
     * @param adType AD type to show
     * **/
	public static void Initialize(String appId, String secretKey, int adType)
	{
		AndroidJNI.AttachCurrentThread();
		if(appflood == null)
			appflood = new AndroidJavaClass("com.appflood.AppFlood");
		appflood.CallStatic("initializeForUnity", appId, secretKey, adType);
	}
	
	/**
     * @return the type of AD
     */
	public static int GetAdType()
	{
		if(appflood != null)
		{
			return appflood.CallStatic<int>("getAdType");
		}
		return -1;
	}
	
    /**
     * show FullScreen AD
     * 
     */
	public static void ShowFullScreen()
	{
		if(appflood != null)
			appflood.CallStatic("showFullScreenForUnity");
	}
	
	
		/**
 * show Interstitial ads
 * 
 * 
 */
 public static void ShowInterstitial()
 {
 	if(appflood != null)
 	{
 		appflood.CallStatic("showInterstitialForUnity");
 	}	
 }
	

	
	/**
     * Check whether connected to server
     * 
     * @return if connected,return true,otherwise false
     */
	public static bool IsConnected()
	{
		if(appflood != null)
		{
			return appflood.CallStatic<bool>("isConnected");
		}
		return false;
	}
	
	/**
     * show panel advertisement
     * 
     * @param showType the type of Panel
     */
	public static void ShowPanel(int showType)
	{
		if(appflood != null)
			appflood.CallStatic("showPanelForUnity", showType);
	}
	
	/**
     * destroy the ads
     */
	public static void Destroy()
	{
		if(appflood != null) {
			appflood.CallStatic("destroy");
			appflood = null;
		}
	}
	
	/**
	 * @param obj         tell system where to find the callback function
     * @param callback    the callback function name
     * @return error return -1, otherwise,the ppy point of user
     */
	public static void QueryAFPoint(String obj, String callback)
	{
		if(appflood != null)
			appflood.CallStatic("queryAFPointForUnity", obj, callback);
	}
	
	/**
     * @param point the points to consume
	 * @param obj         tell system where to find the callback function
     * @param callback    the callback function name
     * @return 0 success -1 user didn't exist -2 the point formatter error; -3
     *         not enough points; -4 request too frequently -5 connection error
     */
	public static void ConsumeAFPoint(int point, String obj, String callback)
	{
		if(appflood != null)
			appflood.CallStatic("consumeAFPointForUnity", point, obj, callback);
	}
	
	/**
     * set the click AD interface
     * 
     * @param clickObj         tell system where to find the callback function if user click the ad.
     * @param clickCallback    the callback function name if user click the ad.
	 * @param closeObj         tell system where to find the callback function if user close the ad.
	 * @param closeCallback    the callback function name if user close the ad.
     */
	public static void SetEventDelegate(String clickObj, String clickCallback, String closeObj, String closeCallback,String finishObj,String finishCallback)
	{
		if(appflood != null)
			appflood.CallStatic("setEventDelegateForUnity", clickObj, clickCallback, closeObj, closeCallback,finishObj,finishCallback);
	}
	
	/**
     * to download res of FullAD to show AD as soon as possible
     * 
     * @param adType
     * @param listType if preload list ad, set this type
	 * @param obj         tell system where to find the callback function
     * @param callback    the callback function name
     */
	public static void Preload(int adType, int listType,String obj, String callback)
	{
		if(appflood != null)
			appflood.CallStatic("preloadForUnity", adType,listType,obj, callback);
	}
	/**
	 * display ad banner
	 * 
	 * @param bannerType  bannerType. 
	 * @param layoutType  banner shown on top or bottom
	 **/
	public static void ShowAFBannerView(int bannerType, int layoutType)
	{
		if(appflood != null) 
			appflood.CallStatic("showAFBannerViewForUnity", bannerType, layoutType);
	}
	
	/**
	 *  version 1.41
	 **/
	 
	/**
	 * remove ad banner
	 **/
	public static void RemoveAFBannerView()
	{
		if (appflood != null)
			appflood.CallStatic("removeAFBannerViewForUnity");
	}
	
	/**
	 * Show a listView ad.
	 * 
	 * @param showType you can config to show which type of ads, the param can be LIST_GAME,LIST_APP, LIST_ALL, LIST_TAB_GAME, LIST_TAB_APP
	 **/
	public static void ShowList(int showType)
	{
		if (appflood != null)
			appflood.CallStatic("showListForUnity", showType);
	}
	/**
	 * USed with getADData function. If you draw your own ad by using getADData function, when user click the ad, you can use this function to send callback request.
	 * when user click the ad, you can use this function to send callback request and open the download page.
	 *
	 * @param backUrl The callback url which you get from getADData function
	 * @param clickUrl The download url which you get from getADData function
	 **/
	public static void handleAFClick(String backUrl, String clickUrl)
	{
		if (appflood != null)
			appflood.CallStatic("handleAFClickForUnity", backUrl, clickUrl);
	}
	
	/**
	 * get ads message. If you want to create ad views by youself, you can use this function to get ads message.
	 * 
	 * @param obj         tell system where to find the callback function
     * @param callback    the callback function name
	 **/
	public static void getADData(String obj, String callback)
	{
		if (appflood != null)
			appflood.CallStatic("getADDataForUnity", obj, callback);
	}
}