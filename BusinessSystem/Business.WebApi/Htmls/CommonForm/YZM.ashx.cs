using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Business.WebApi.Htmls
{
    /// <summary>
    /// YZM 的摘要说明
    /// </summary>
    public class YZM : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //设置返回类型
            context.Response.ContentType = "image/jpeg";

            //创建一幅图片
            using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(80, 37))
            {
                //创建画布
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
                {
                    //定义画的字符
                    Random rand = new Random();
                    int code = rand.Next();
                    string codeStr = code.ToString().Substring(0, 4);
                    //此处要这样把值保存到session中，还要记得要继承IRequiresSessionState接口
                    HttpContext.Current.Session["code"] = codeStr;

                    Point pt = new Point(0, 0);
                    //将字符画到画布上
                    g.DrawString(codeStr, new System.Drawing.Font("楷书", 20), System.Drawing.Brushes.Red, pt);

                    Pen p = new Pen(Brushes.Red, 1);
                    Rectangle rec = new Rectangle(0, 0, 30, 20);
                    g.DrawPie(p, rec, 360, 360);

                    //将图片以指定格式保存到输出流中
                    bitmap.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}