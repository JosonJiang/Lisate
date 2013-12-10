using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Log;
using Lishate.Message;
using Lishate.Basedata;

namespace Lishate.Message.Parse
{
    public class MessageParseBL
    {
        public void Run()
        {
            while (true)
            {
                try
                {
                    IpMessage im = (IpMessage)MessageInstance.getInstance().RecvList.Get();
                    DeviceInfo di = null;
                    MobInfo mi = null;
                    if (im.Msg.Req == GobalDef.BASE_MSG_FT_REQ)
                    {
                        if (im.Msg.FromType == GobalDef.BASE_MSG_FT_HUB)
                        {
                            di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(im.Msg.FromDevID);
                            if (di != null)
                            {
                                di.IPPoint = im.EndPoint;
                            }
                        }
                        else if (im.Msg.FromType == GobalDef.BASE_MSG_FT_END)
                        {
                            di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(im.Msg.FromDevID);
                            if (di != null)
                            {
                                di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(di.ParentId);
                                if (di != null)
                                {
                                    di.IPPoint = im.EndPoint;
                                }
                            }
                        }
                        else if (im.Msg.FromType == GobalDef.BASE_MSG_FT_MOBILE)
                        {
                            mi = BaseInfoInstance.getInstance().MobInfo.GetMobInfo(im.Msg.FromDevID);
                            if (mi != null)
                            {
                                mi.EndPoint = im.EndPoint;
                            }
                        }

                        if (im.Msg.ToType == GobalDef.BASE_MSG_FT_SERVER)
                        {
                            MessageInstance.getInstance().ServerList.Add(im);
                        }
                        else if (im.Msg.ToType == GobalDef.BASE_MSG_FT_HUB)
                        {
                            di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(im.Msg.ToDevID);
                            if (di != null)
                            {
                                im.EndPoint = di.IPPoint;
                                MessageInstance.getInstance().SendList.Add(im);
                            }
                        }
                        else if (im.Msg.ToType == GobalDef.BASE_MSG_FT_END)
                        {
                            di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(im.Msg.ToDevID);
                            if (di != null)
                            {
                                di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(di.ParentId);
                                if (di != null)
                                {
                                    im.EndPoint = di.IPPoint;
                                    MessageInstance.getInstance().SendList.Add(im);
                                }
                            }
                        }
                        else if (im.Msg.ToType == GobalDef.BASE_MSG_FT_MOBILE)
                        {
                            mi = BaseInfoInstance.getInstance().MobInfo.GetMobInfo(im.Msg.ToDevID);
                            if (mi != null)
                            {
                                im.EndPoint = mi.EndPoint;
                                MessageInstance.getInstance().SendList.Add(im);
                            }
                        }
                    }
                    else if (im.Msg.Req == GobalDef.BASE_MSG_FT_RCV)
                    {
                        if (im.Msg.ToType == GobalDef.BASE_MSG_FT_END)
                        {
                            di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(im.Msg.FromDevID);
                            if (di != null)
                            {
                                di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(di.ParentId);
                                if (di != null)
                                {
                                    di.IPPoint = im.EndPoint;
                                }
                            }
                        }
                        else if (im.Msg.ToType == GobalDef.BASE_MSG_FT_HUB)
                        {
                            di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(im.Msg.FromDevID);
                            if (di != null)
                            {
                                di.IPPoint = im.EndPoint;
                            }
                        }
                        else if (im.Msg.ToType == GobalDef.BASE_MSG_FT_MOBILE)
                        {
                            mi = BaseInfoInstance.getInstance().MobInfo.GetMobInfo(im.Msg.FromDevID);
                            if (mi != null)
                            {
                                mi.EndPoint = im.EndPoint;
                            }
                        }
                        


                        if (im.Msg.FromType == GobalDef.BASE_MSG_FT_END)
                        {
                            di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(im.Msg.ToDevID);
                            if (di != null)
                            {
                                di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(di.ParentId);
                                if (di != null)
                                {
                                    im.EndPoint = di.IPPoint;
                                    MessageInstance.getInstance().SendList.Add(im);
                                }
                            }
                        }
                        else if (im.Msg.FromType == GobalDef.BASE_MSG_FT_HUB)
                        {
                            di = BaseInfoInstance.getInstance().DeviceInfo.GetDeviceInfo(im.Msg.ToDevID);
                            if (di != null)
                            {
                                im.EndPoint = di.IPPoint;
                                MessageInstance.getInstance().SendList.Add(im);
                            }
                        }
                        else if (im.Msg.FromType == GobalDef.BASE_MSG_FT_MOBILE)
                        {
                            mi = BaseInfoInstance.getInstance().MobInfo.GetMobInfo(im.Msg.ToDevID);
                            if (mi != null)
                            {
                                im.EndPoint = mi.EndPoint;
                                MessageInstance.getInstance().SendList.Add(im);
                            }
                        }
                        else if (im.Msg.FromType == GobalDef.BASE_MSG_FT_SERVER)
                        {
                            MessageInstance.getInstance().ServerList.Add(im);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Log.WriteErrorLog(e.Message);
                    Log.Log.WriteErrorLog(e.StackTrace);
                }
            }
        }
    }
}
