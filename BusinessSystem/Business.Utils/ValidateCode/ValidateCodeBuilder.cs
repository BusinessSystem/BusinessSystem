using System.Drawing;

namespace Business.Utils.ValidateCode
{
    public interface IValidateCodeFactory
    {
        Bitmap CreateValidateCode(out string result);
    }

    public class MathOperateCodeFactory : IValidateCodeFactory
    {
        public Bitmap CreateValidateCode(out string result)
        {
             return new MathVerifyCode().OutputImage(out result);
        }
    }
}
