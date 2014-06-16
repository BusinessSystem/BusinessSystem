using System;
using System.Drawing;

namespace Business.Utils.ValidateCode
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ValidateCode
    {
        public abstract Bitmap OutputImage(out string mathResult);

        public abstract string GenerateExpression(out string Result);

        public abstract Bitmap CreateGraph(string expression);
       
    }

    /// <summary> 
    /// 数学算式的验证码 
    /// </summary> 
    public  class MathVerifyCode:ValidateCode
    {
        
        /// <summary> 
        /// 输出验证码表达式到浏览器 
        /// </summary> 
        /// <param name="context">httpcontext</param> 
        /// <param name="sessionKey">保存运算值的SESSION的KEY</param> 
        public override Bitmap OutputImage(out string result)
        {

            string expression = GenerateExpression(out result);
            Bitmap bmp = CreateGraph(expression);
            //bmp = TwistImage(bmp, true, 3, 5);
            return bmp;
        }

        public override string GenerateExpression(out string Result)
        {
            //StringBuilder builder = new StringBuilder();
            int  mathResult = 0;
            Random rnd = new Random();
            ////生成10以内的整数，用来运算 
            int operator1 = rnd.Next(1, 10);
            int operator2 = rnd.Next(1, 10);
            string expression;
            //随机组合运算顺序，只做 + 和 * 运算 
            switch (rnd.Next(0, 4))
            {
                case 0:
                    mathResult = 0;
                    int temp1 =0,temp2=0;
                    if (operator1 > operator2)
                    {
                        temp1 = operator1;
                        temp2 = operator2;
                    }
                    else
                    {

                        temp1 = operator2;
                        temp2 = operator1;
                    }
                    expression = string.Format("{0}减  {1} =?", temp1, temp2);
                    mathResult = temp1 - temp2;
                    break;
                case 1:
                    mathResult = operator1 * operator2;
                    expression = string.Format("{0}乘  {1} =?", operator1, operator2);
                    break;
                //case 2: //去掉除法
                //       mathResult = operator1 / operator2 ;
                //       expression = string.Format("{0} ÷ {1} = ?", operator1, operator2);
                //    break;
                default:
                    mathResult = operator2 + operator1;
                    expression = string.Format("{0}加  {1} =?", operator1, operator2);
                    break;
            }
            Result = mathResult.ToString();
            return expression;
        }

        public override Bitmap CreateGraph(string expression)
        {
            Random rnd = new Random();
            Bitmap bmp = new Bitmap(100, 38);

            using (Graphics graph = Graphics.FromImage(bmp))
            {
                //图片背景色
              //  graph.Clear(Color.FromArgb(232, 238, 247)); ////背景色，可自行设置 
                #region 注释
                //画图片背景线
                //for (int i = 0; i < 10; i++)
                //{
                //    int x1 = rnd.Next(bmp.Width);
                //    int x2 = rnd.Next(bmp.Width);
                //    int y1 = rnd.Next(bmp.Height);
                //    int y2 = rnd.Next(bmp.Height);

                //    graph.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                //}
                ////画图片的前景噪音点
                //for (int i = 0; i < 50; i++)
                //{
                //    int x = rnd.Next(bmp.Width);
                //    int y = rnd.Next(bmp.Height);

                //    bmp.SetPixel(x, y, Color.FromArgb(rnd.Next()));
                //} 
                #endregion
                ////输出表达式 
                for (int i = 0; i < expression.Length; i++)
                {
                    graph.DrawString(expression.Substring(i, 1),
                    new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
                    //new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(128), rnd.Next(255))),
                    new SolidBrush(Color.White),
                    5 + i * 10,
                    rnd.Next(1, 5));
                }
                ////画边框，不需要可以注释掉 
                //graph.DrawRectangle(new Pen(Color.Firebrick), 0, 0, 100 - 1, 25 - 1);
            }
            return TwistImage(bmp,true,8,3);
        }

        ///<summary>
        ///正弦曲线Wave扭曲图片
        ///</summary>
        ///<param name="srcBmp">图片路径</param>
        ///<param name="twist">如果扭曲则选择为True</param>
        ///<param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        ///<param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        ///<returns></returns>
        private  Bitmap TwistImage(Bitmap srcBmp, bool twist, double dMultValue, double dPhase)
        {
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            //graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);

            graph.Dispose();
            double dBaseAxisLen = twist ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = twist ? (Math.PI * (double)j) / dBaseAxisLen : (Math.PI * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = twist ? i + (int)(dy * dMultValue) : i;
                    nOldY = twist ? j : j + (int)(dy * dMultValue);
                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }


    }
}
