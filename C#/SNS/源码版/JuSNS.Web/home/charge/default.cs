using System;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.home.charge
{
    public class @default : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "充值中心");
            string charge = Public.GetXMLValue("charge");
            if (charge.IndexOf(",") == -1)
            {
                context.Put("errors", "参数配置错误：ConfigError");
            }
            else
            {
                string[] chargeARR = charge.Split(',');
                context.Put("pointvalue", chargeARR[0]);
                context.Put("gpointvalue", chargeARR[1]);
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            ShowInfo(ref context);
            int moeny = GetInt("money", 0);
            context.Put("next", this.UserID);
            string orderNumber = Input.MD5(Guid.NewGuid().ToString(), true).ToUpper();
            string charge = Public.GetXMLValue("charge");
            string[] chargeARR = charge.Split(',');
            context.Put("moeny", "<span class=\"reshow\">" + moeny + "</span>RMB，支付成功可获得<span class=\"reshow\">" + moeny * Convert.ToInt32(chargeARR[0]) + "</span>个积分 <span class=\"reshow\">" + moeny * Convert.ToInt32(chargeARR[1]) + "</span>个" + UiConfig.gName + "");
            context.Put("ordernumber", orderNumber);
            //插入订单
            ChargeOrderInfo mdl = new ChargeOrderInfo();
            mdl.CreatTime = DateTime.Now;
            mdl.Gpoint = moeny * Convert.ToInt32(chargeARR[1]);
            mdl.Id = 0;
            mdl.IsSucces = false;
            mdl.Money = moeny;
            mdl.OrderNumber = orderNumber;
            mdl.Point = moeny * Convert.ToInt32(chargeARR[0]);
            mdl.PostIP = Public.GetClientIP();
            mdl.UserID = this.UserID;
            int n = JuSNS.Home.User.User.Instance.InsertChargeOrder(mdl);
            if (n > 0)
            {
                //这里输入ISP的订单信息
            }
            else
            {
                context.Put("errors", "充值发生错误。");
            }
        }
    }
}
