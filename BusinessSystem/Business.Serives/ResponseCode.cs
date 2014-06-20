using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Serives
{
    public struct ResponseCode
    {
        public const string Ok = "0";//"S0000";
        public const string Timeout = "F0001"; //超时

        public const string IsVip = "V0001";
        public const string NotVip = "V0000";

        public const string ModifySpecialVipError = "E0001";
        /// <summary>
        /// 外挂
        /// </summary>
        public const string Hacker = "F0002";

        /// <summary>
        /// 未知错误
        /// </summary>
        public const string UnknownError = "-1";

        /// <summary>
        /// 未找到数据
        /// </summary>
        public const string NotFoundData = "-2";

        /// <summary>
        /// 服务器错误 
        /// </summary>
        public const string ServerError = "-3";

        /// <summary>
        /// 参数错误 
        /// </summary>
        public const string QueryParamIsEmpty = "-4";

        /// <summary>
        /// 参数错误 
        /// </summary>
        public const string QueryParamError = "-5";

        /// <summary>
        /// 没有登录 
        /// </summary>
        public const string NoLogin = "-6";


        /// <summary>
        /// 反序列化数据出错
        /// </summary>
        public const string DataError = "-7";

        /// <summary>
        /// 没有权限
        /// </summary>
        public const string NoRight = "-8";

        /// <summary>
        /// 用户级别数据错误
        /// </summary>
        public const string CustomLevelError = "-9";

        /// <summary>
        /// 用户邮箱数据错误
        /// </summary>
        public const string CustomEmailError = "-10";

        /// <summary>
        /// 数量必须大于0
        /// </summary>
        public const string NumberThanZero = "-11";

        public const string ExistMobile = "-13";

        /// <summary>
        /// 范围：100-400
        /// </summary>
        public struct Product
        {
            ///// <summary>
            ///// 该用户已经登记过某商品
            ///// </summary>
            //public const string RegisteredProduct = "";

            /// <summary>
            /// 生产日期比结束日期大
            /// </summary>
            public const string ProductStartTimeCannotBiggerEndTime = "100";
            /// <summary>
            /// 进货日期比结束日期大
            /// </summary>
            public const string PurchasingStartTimeBiggerEndTime = "101";

            /// <summary>
            /// 商品咨讯不存在
            /// </summary>
            public const string ProductConsultNotFound = "200";

            /// <summary>
            /// 商品咨讯内容为空
            /// </summary>
            public const string ContentIsEmpty = "201";

            /// <summary>
            /// 商品咨讯回复内容为空
            /// </summary>
            public const string ReplyContentIsEmpty = "202";

            /// <summary>
            /// 商品评论，已经点过有用，一个评价只能点一次
            /// </summary>
            public const string HasUseful = "250";

            /// <summary>
            /// 商品评论未找到
            /// </summary>
            public const string NoProductComment = "251";

            /// <summary>
            /// （在内存中）没有找到商品
            /// </summary>
            public const string NotFoundProducts = "301";

            /// <summary>
            /// 商品上过架，不能删除
            /// </summary>
            public const string CannotDelete = "302";

            /// <summary>
            /// 在mysql没有找到没有上过架的商品（用于删除商品）
            /// </summary>
            public const string CannotFoundNotUpStoreProducts = "303";

            /// <summary>
            /// 商品非上架状态
            /// </summary>
            public const string ProductStatusNotNormal = "304";

        }

        /// <summary>
        /// 范围：401-450
        /// </summary>
        public struct Address
        {
            public const string AddressNotFound = "401";
        }

        /// <summary>
        /// 范围：451-500
        /// </summary>
        public struct AddressPayType
        {
            public const string AddressPayTypeNotFound = "451";
        }

        /// <summary>
        /// 范围：501-550
        /// </summary>
        public struct SysUser
        {
            /// <summary>
            /// 无效物管理员ID
            /// </summary>
            public const string InvalidAdminUserId = "501";
            /// <summary>
            /// 无效的管理员名称
            /// </summary>
            public const string InvalidAdminUserName = "502";
        }

        /// <summary>
        /// 范围：551-600
        /// </summary>
        public struct ThirdPartUser
        {
            /// <summary>
            /// 第三方用户未在茶涂注册过
            /// </summary>
            public const string NotRegister = "551";

            /// <summary>
            /// 无效的第三方ID
            /// </summary>
            public const string InvalidThirdPartyId = "552";

            /// <summary>
            /// 无效的第三方来源
            /// </summary>
            public const string InvalidThirdParty = "553";

            /// <summary>
            /// 无效的茶涂用户ID
            /// </summary>
            public const string InvalidCustomId = "554";

            /// <summary>
            /// 第三方用户已在茶涂注册过
            /// </summary>
            public const string HadRegister = "555";
        }

        /// <summary>
        /// 范围：601-650
        /// </summary>
        public struct ProductSku
        {
            /// <summary>
            /// 商品SKU不存在
            /// </summary>
            public const string ProductSkuNotFound = "601";
        }

        /// <summary>
        /// 范围：651-700
        /// </summary>
        public struct ThirdParty
        {
            /// <summary>
            /// 第三方平台服务出错
            /// </summary>
            public static string ThirdPartyServerError = "651";
            /// <summary>
            /// 第三方平台数据暂未同步
            /// </summary>
            public static string ThirdPartyIsNotSync = "652";
            /// <summary>
            /// 无效参数
            /// </summary>
            public const string InvalidParam = "653";
            /// <summary>
            /// 初始化出错
            /// </summary>
            public const string InitError = "654";
            /// <summary>
            /// 没有权限
            /// </summary>
            public const string NoPermission = "655";

            public const string StockNotEnough = "656";

           
        }

        /// <summary>
        /// 范围：701-750
        /// </summary>
        public struct FakeComment
        {
            public static string NotChoiceComment = "701";
        }

        /// <summary>
        /// 范围：6000-6099
        /// </summary>
        public struct FreeTrial
        {
            /// <summary>
            /// 用户名不能为空
            /// </summary>
            public const string NameNull = "6000";
            /// <summary>
            /// 手机号不能为空
            /// </summary>
            public const string MobileNull = "6001";
            /// <summary>
            /// 申请理由不能为空
            /// </summary>
            public const string ReasonNull = "6002";
            /// <summary>
            /// 备注不能为空
            /// </summary>
            public const string RemarkNull = "6003";
            /// <summary>
            /// 未选择要修改的申请
            /// </summary>
            public const string SelectedNull = "6004";
        }

        /// <summary>
        /// 范围：7000-7099
        /// </summary>
        public struct Search
        {
            /// <summary>
            /// 文件路径为空
            /// </summary>
            public const string NoFilePath = "7000";

            /// <summary>
            /// 没有内容
            /// </summary>
            public const string NoContent = "7001";
        }

        /// <summary>
        /// 范围：8000-8099
        /// </summary>
        public struct Coupon
        {
            /// <summary>
            /// 找不到优惠券
            /// </summary>
            public const string NotFound = "8000";

            /// <summary>
            /// 优惠券数量不够
            /// </summary>
            public const string NotEnough = "8001";

            /// <summary>
            /// 优惠券还没有到领取时间
            /// </summary>
            public const string NotStart = "8002";

            /// <summary>
            /// 优惠券领取已结束
            /// </summary>
            public const string HaveEnd = "8003";

            /// <summary>
            /// 已经领取过
            /// </summary>
            public const string Taken = "8004";

            /// <summary>
            /// 该优惠券活动已经生成过优惠券
            /// </summary>
            public const string AlreadyGenerated = "8005";

            /// <summary>
            /// VIP不能使用优惠券
            /// </summary>
            public const string VipCanNotUseCoupon = "8006";
            /// <summary>
            /// 活动尚未开始
            /// </summary>
            public const string CompaignNotStart = "8007";
            /// <summary>
            /// 活动已经结束
            /// </summary>
            public const string CompaignAlreadyEnd = "8008";
            /// <summary>
            /// 购物车未计算完成（系统错误）
            /// </summary>
            public const string CartNotReady = "8009";
            /// <summary>
            /// 订单价格不符合优惠券最低订单价格限制
            /// </summary>
            public const string OrderPriceUnfit = "8010";
            /// <summary>
            /// 该优惠券不适用于其他优惠活动的商品
            /// </summary>
            public const string NotAllowWithOtherCompaign = "8011";
            /// <summary>
            /// 订单中未包含适用该优惠券的商品
            /// </summary>
            public const string OrderItemNotInCouponScope = "8012";

            public const string IsUsed = "8013";
            /// <summary>
            /// 无效活动
            /// </summary>
            public const string NullityCompaign = "8014";

            public const string IsApproved = "8015";
            /// <summary>
            /// 优惠券未过期
            /// </summary>
            public const string CouponNotExpired = "8016";
            /// <summary>
            /// 用户数不能小于或等于0
            /// </summary>
            public const string CustomerNotEnough = "8017";

            /// <summary>
            /// 优惠券活动面额必须大于0
            /// </summary>
            public const string FaceValueError = "8018";

            public const string UnApproved = "8019";
            public const string IsDeleted = "8020";

            public const string IsValid = "8025";


            /// <summary>
            /// 已经作废
            /// </summary>
            public const string Obsoleted = "8026";
            /// <summary>
            /// 过期
            /// </summary>
            public const string TimeOver = "8027";
        }

        /// <summary>
        /// 范围：8100-8399
        /// </summary>
        public struct Order
        {
            /// <summary>
            /// 订单找不到
            /// </summary>
            public const string OrderNotFound = "8100";

            /// <summary>
            /// 支付成功后更新订单失败
            /// </summary>
            public const string PayUpdateFaild = "8101";

            /// <summary>
            /// 执行添加退款记录发生错误
            /// </summary>
            public const string AddRefundLogError = "8102";

            /// <summary>
            /// 执行申请订单退货发生错误
            /// </summary>
            public const string OrderReturnError = "8103";

            /// <summary>
            /// 确认收货时发生错误
            /// </summary>
            public const string ConfirmReceiptError = "8104";

            /// <summary>
            /// 订单发货发生错误
            /// </summary>
            public const string OrderDeliveryError = "8105";

            /// <summary>
            /// 取消订单发生错误
            /// </summary>
            public const string OrderCancelError = "8106";

            /// <summary>
            /// 订单审核时发生错误
            /// </summary>
            public const string OrderCheckedError = "8107";

            /// <summary>
            /// 提交订单时发生错误
            /// </summary>
            public const string OrderSubmitFailed = "8108";

            /// <summary>
            /// 下单参数有误
            /// </summary>
            public const string OrderParamError = "8109";

            /// <summary>
            /// 订单不能关闭
            /// </summary>
            public const string OrderCannotClose = "8110";

            /// <summary>
            /// 订单不属于当前登录用户
            /// </summary>
            public const string OrderNotBelong = "8111";

            /// <summary>
            /// 订单不能取消
            /// </summary>
            public const string OrderCannotCancel = "8112";

            /// <summary>
            /// 订单不能发货
            /// </summary>
            public const string OrderCannotDelivery = "8113";

            /// <summary>
            /// 订单不能确认收货（不能完成）
            /// </summary>
            public const string OrderCannotComplete = "8114";

            /// <summary>
            /// 不能审核订单
            /// </summary>
            public const string OrderCannotCheck = "8115";

            /// <summary>
            /// 订单没有发货，不能完成订单
            /// </summary>
            public const string OrderCannotCompleteByNoDelivy = "8116";

            /// <summary>
            /// 订单还没有支付，不能完成订单
            /// </summary>
            public const string OrderCannotCompleteByNoPay = "8117";

            /// <summary>
            /// 订单已经支付，不能关闭取消
            /// </summary>
            public const string OrderCannotColseByIsPay = "8118";

            /// <summary>
            /// 结算订单，存在下架商品
            /// </summary>
            public const string ProductOutSale = "8119";

            /// <summary>
            /// 结算订单，存在库存不足的商品
            /// </summary>
            public const string ProductQtyNotEnough = "8120";

            /// <summary>
            ///结算订单，存在商品购买数量超过限购数量
            /// </summary>
            public const string ProductLimitBuyNum = "8121";

            /// <summary>
            /// 结算订单，存在商品数据错误
            /// </summary>
            public const string ProductDataError = "8122";

            /// <summary>
            /// 结算订单，使用了无效的优惠券
            /// </summary>
            public const string CouponFail = "8123";

            /// <summary>
            /// 结算订单，使用了大于用户积分余额的积分
            /// </summary>
            public const string PointNotEnough = "8124";

            /// <summary>
            /// 结算订单，生成订单编号失败
            /// </summary>
            public const string GenerateOrderCodeFail = "8125";

            /// <summary>
            /// 结算订单是系统发生异常
            /// </summary>
            public const string SubmitOrderException = "8126";

            /// <summary>
            /// 购物车数据有变动，不能下单
            /// </summary>
            public const string ShoppingCartDataHasChange = "8127";

            /// <summary>
            /// 购物车里面没有商品
            /// </summary>
            public const string ShoppingCartDataHaveNoData = "8128";

            /// <summary>
            /// 优惠券不存在
            /// </summary>
            public const string CouponNotExist = "8129";
            /// <summary>
            /// 优惠券已使用过
            /// </summary>
            public const string CouponUsed = "8130";
            /// <summary>
            /// 优惠券使用日期非法
            /// </summary>
            public const string CouponInvalidDate = "8131";
            /// <summary>
            /// 订单已支付
            /// </summary>
            public const string Paid = "8132";
            /// <summary>
            /// 订单还没有确认收货，不能评论
            /// </summary>
            public const string OrderNotConfirmReceipt = "8133";

            /// <summary>
            /// 结算订单时，未知的优惠券来源
            /// </summary>
            public const string UnKnowCouponSource = "8134";

            /// <summary>
            /// 结算订单时，非本人使用
            /// </summary>
            public const string NotOwnerUse = "8135";

            /// <summary>
            /// 订单还没有审核
            /// </summary>
            public const string OrderNotStillChecked = "8136";

            /// <summary>
            /// 订单还没有支付，不能发货
            /// </summary>
            public const string CannotDeliverByNoPay = "8137";

            /// <summary>
            /// 订单已支付，不能取消
            /// </summary>
            public const string CannotCloseByPaid = "8138";

            /// <summary>
            /// 付款金额与订单金额不一致
            /// </summary>
            public const string PaidPriceError = "8139";

            //-------------------------------------------------------------------------------

            /// <summary>
            /// 第三方显示已支付，而本地却不是
            /// </summary>
            public const string RemotePaySuccess = "8200";

            /// <summary>
            /// 第三方显示支付失败
            /// </summary>
            public const string RemotePayFail = "8201";

            /// <summary>
            /// 第三方还没有生成支付订单
            /// </summary>
            public const string RemotePayOrderNotFound = "8202";

            /// <summary>
            /// 第三方显示等侍支付
            /// </summary>
            public const string RemoteWaitPay = "8203";

            /// <summary>
            /// 无效的物流编号
            /// </summary>
            public const string InvalidShipNo = "8204";

            /// <summary>
            /// 无效的物流名称
            /// </summary>
            public const string InvalidShipName = "8205";

            /// <summary>
            /// 没有找到用户的发票记录
            /// </summary>
            public const string NotFoundInvoice = "8206";
        }

        /// <summary>
        /// 防伪码：8401-8499
        /// </summary>
        public struct SecurityCode
        {
            /// <summary>
            /// 防伪码存在
            /// </summary>
            public const string Exist = "8401";
            /// <summary>
            /// 防伪码不存在
            /// </summary>
            public const string Inexistence = "8402";
            /// <summary>
            /// 批号不能为空
            /// </summary>
            public const string NullBatchNumber = "8403";
            /// <summary>
            /// 批号已被使用
            /// </summary>
            public const string BatchNumberUsed = "8404";
            /// <summary>
            /// 防伪码已被删除
            /// </summary>
            public const string SecurityCodeDeleted = "8405";
        }

        /// <summary>
        /// 范围：9100-9300
        /// </summary>
        public struct Manage
        {
            /// <summary>
            /// 类目不能为空
            /// </summary>
            public const string CategoryIsEmpty = "9101";
            /// <summary>
            /// 品牌不能为空
            /// </summary>
            public const string BrandIsEmpty = "9102";

            /// <summary>
            /// 商品名称长度异常
            /// </summary>
            public const string ProNameLengthEx = "9103";

            /// <summary>
            /// 请选择一个小图片作为主要预览图
            /// </summary>
            public const string NoSelectMainImage = "9202";
            /// <summary>
            /// 主要图片不能为空
            /// </summary>
            public const string MainImageIsEmpty = "9203";
        }

        /// <summary>
        /// 范围：10000-19999
        /// </summary>
        public struct ShoppingCart
        {
            /// <summary>
            /// -1：出错
            /// </summary>
            public const string Error = "10000";

            /// <summary>
            /// 0：失败
            /// </summary>
            public const string Fail = "10001";

            /// <summary>
            /// 1：成功
            /// </summary>
            public const string Success = "10002";

            /// <summary>
            /// 2：没有数据
            /// </summary>
            public const string NoData = "10003";

            /// <summary>
            /// 3：没有登录
            /// </summary>
            public const string NoLogin = "10004";

            /// <summary>
            /// 数据已存在
            /// </summary>
            public const string DataExist = "10005";

            /// <summary>
            /// 超出数量限制
            /// </summary>
            public const string LimitCount = "10006";

            /// <summary>
            /// 没有数据权限
            /// </summary>
            public const string NotDataAccess = "10007";

            /// <summary>
            /// 没有操作权限
            /// </summary>
            public const string NotOperationAccess = "10008";

            /// <summary>
            /// 库存不足
            /// </summary>
            public const string OutOfStock = "10009";

            /// <summary>
            /// 超过购物车容量
            /// </summary>
            public const string OverShoppingCartCapacity = "10010";

            /// <summary>
            /// 产品已下架(不出售)
            /// </summary>
            public const string ProductNotSale = "10011";

            /// <summary>
            /// SKU不存在
            /// </summary>
            public const string SkuNotExist = "10012";

            /// <summary>
            /// 购物车已合并
            /// </summary>
            public const string Merged = "10013";

            /// <summary>
            /// 购物车商品数据失效
            /// </summary>
            public const string InvalidProduct = "10014";

            /// <summary>
            /// 购物车商品不存在
            /// </summary>
            public const string ProductNotExist = "10015";

            /// <summary>
            /// 超出单件购买限制
            /// </summary>
            public const string OverBuyableMaxCount = "10016";
        }

        /// <summary>
        /// 范围：20000-2999
        /// </summary>
        public struct Item
        {
            /// <summary>
            /// 0：失败
            /// </summary>
            public const string Fail = "20001";

            /// <summary>
            /// 1：成功
            /// </summary>
            public const string Success = "20002";

            /// <summary>
            /// 2：没有数据
            /// </summary>
            public const string NoData = "20003";
        }

        /// <summary>
        /// 范围：30000-3999
        /// </summary>
        public struct Payment
        {
            /// <summary>
            /// 没有数据权限
            /// </summary>
            public const string NotDataAccess = "30000";

            /// <summary>
            /// 不允许在线支付
            /// </summary>
            public const string CannotPayOnLine = "30001";


            /// <summary>
            /// 没有通知参数
            /// </summary>
            public const string NoNotifyParamters = "30002";

            /// <summary>
            /// 请求支付宝出错
            /// </summary>
            public const string AlipayRequestFailed = "30003";

            /// <summary>
            /// 支付宝签名验证失败
            /// </summary>
            public const string AlipayVerifyFailed = "30004";

            /// <summary>
            /// 请求网银在线出错
            /// </summary>
            public const string ChinaBankRequestFailed = "30005";

            /// <summary>
            /// 网银在线签名验证出错
            /// </summary>
            public const string ChinaBankVerifyFailed = "30006";

            /// <summary>
            /// 请求财付通出错
            /// </summary>
            public const string TenpayRequestFailed = "30007";

            /// <summary>
            /// 财付通即时到账失败
            /// </summary>
            public const string TenpayRedirectFailed = "30008";

            /// <summary>
            /// 财付通查询验证签名失败或id验证失败
            /// </summary>
            public const string TenpayVerifyFailed = "30009";

            /// <summary>
            /// 通知ID查询签名验证失败
            /// </summary>
            public const string NotifyVerifyFailed = "30010";

            /// <summary>
            /// 财付通后台调用通信失败
            /// </summary>
            public const string NetCallFailed = "30011";

            /// <summary>
            /// 操作失败
            /// </summary>
            public const string Fail = "30012";

            /// <summary>
            /// AddPaymentFailed
            /// </summary>
            public const string AddPaymentFailed = "30013";

            /// <summary>
            /// UpdateOrderPayNoFailed
            /// </summary>
            public const string UpdateOrderPayNoFailed = "30014";

            /// <summary>
            /// 验证失败
            /// </summary>
            public const string ValidateFailure = "30015";

            /// <summary>
            /// 没有参数返回
            /// </summary>
            public const string NoParame = "30016";
            /// <summary>
            /// 不支持的支付方式
            /// </summary>
            public const string NoSupportPayType = "30017";

        }

        /// <summary>
        /// 以50000 开头
        /// </summary>
        public partial struct Member
        {
            /// <summary>
            /// 输入验证码错误
            /// </summary>
            public const string ValidCodeIsError = "50001";
            /// <summary>
            /// 注册成功
            /// </summary>
            public const string RegistSuccess = "50002";
            /// <summary>
            /// 登录成功
            /// </summary>
            public const string LoginSuccess = "50003";

            /// <summary>
            /// 注册写入失败
            /// </summary>
            public const string RegistServerError = "50004";

            /// <summary>
            /// 用户名或者密码错误
            /// </summary>
            public const string LoginNameOrPwdError = "50006";

            /// <summary>
            /// 用户名或者密码错误
            /// </summary>
            public const string EmailIsEmpty = "50007";

            /// <summary>
            /// 用户名或者密码错误
            /// </summary>
            public const string MobileIsEmpty = "50008";

            /// <summary>
            /// 用户名可以注册
            /// </summary>
            public const string LoginNameIsLocked = "50010";
            /// <summary>
            /// 用户名为邮箱
            /// </summary>
            public const string LoginNameIsEmail = "50011";
            /// <summary>
            /// 用户名为手机号
            /// </summary>
            public const string LoginNameIsMobile = "50012";
            /// <summary>
            /// 用户名包含中文
            /// </summary>
            public const string LoginNameIncludeChinese = "50013";
            /// <summary>
            /// 用户名不能超过50长度
            /// </summary>
            public const string LoginNameMoreLen = "50014";
            /// <summary>
            /// 用户名只能为手机号或者邮箱!
            /// </summary>
            public const string LoginNameErrorFormat = "50015";
            /// <summary>
            /// 用户名已经被注册
            /// </summary>
            public const string LoginNameUsedError = "50016";
            /// <summary>
            /// 用户名不存在
            /// </summary>
            public const string LoginNameNotExist = "50017";
            /// <summary>
            /// 用户名已存在
            /// </summary>
            public const string LoginNameIsExist = "50018";

            /// <summary>
            /// 用户昵称不能为空
            /// </summary>
            public const string NoNickName = "50019";

            /// <summary>
            /// 密码设置正确
            /// </summary>
            public const string PwdIsValid = "50020";
            /// <summary>
            /// 密码长度不能少于6位
            /// </summary>
            public const string PwdLessError = "50021";
            /// <summary>
            /// 密码长度不能超过50
            /// </summary>
            public const string PwdMaxLenError = "50022";
            /// <summary>
            /// 密码不能由纯数字构成
            /// </summary>
            public const string PwdIsAllNumber = "50023";
            /// <summary>
            /// 密码不能包含中文
            /// </summary>
            public const string PwdIncludeChinese = "50024";

            /// <summary>
            /// 密码不正确
            /// </summary>
            public const string PwdNotCorrect = "50025";

            /// <summary>
            /// 用户名包含敏感词
            /// </summary>
            public const string LoginNameSensitive = "50026";
            /// <summary>
            /// 密码不能由纯字母构成
            /// </summary>
            public const string PwdIsAllWord = "50027";

            /// <summary>
            /// 更新邮箱激活状态失败
            /// </summary>
            public const string ActiveMailError = "50301";

            /// <summary>
            /// 更新手机激活状态失败
            /// </summary>
            public const string ActiveMobileError = "50302";

            /// <summary>
            /// 已经激活手机
            /// </summary>
            public const string HadActiveMobile = "50303";

            /// <summary>
            /// 已经激活邮箱
            /// </summary>
            public const string HadActiveMail = "50304";

            /// <summary>
            /// 
            /// </summary>
            public const string MobileCodeError = "50305";

            /// <summary>
            /// 地址错误
            /// </summary>
            public const string AddressError = "50306";

            /// <summary>
            /// 
            /// </summary>
            public const string MobileCodeNotRight = "50307";


            /// <summary>
            /// 邮箱格式错误
            /// </summary>
            public const string EmailFormatError = "50400";

            /// <summary>
            /// 手机号格式错误
            /// </summary>
            public const string MobileFormatError = "50401";

            /// <summary>
            /// 手机号已被使用
            /// </summary>
            public const string MobileHadUsed = "50402";

            /// <summary>
            /// 邮箱已被使用
            /// </summary>
            public const string MailHadUsed = "50403";

            /// <summary>
            /// 发送邮件失败
            /// </summary>
            public const string SendEmailError = "50404";

            /// <summary>
            /// 短信发送失败
            /// </summary>
            public const string SendMessageError = "50405";

            /// <summary>
            /// 发送短信超出最大发送数量
            /// </summary>
            public const string SendMsgTooMuch = "50406";

            /// <summary>
            /// 发送频率太快
            /// </summary>
            public const string SendMsgTooFast = "50407";


            /// <summary>
            /// 非法请求
            /// </summary>
            public const string ReqeustNotExist = "50501";

            /// <summary>
            /// 用户不存在
            /// </summary>
            public const string UserNotFound = "50602";

            /// <summary>
            /// 用户没有权限
            /// </summary>
            public const string UserNoRight = "50603";

            /// <summary>
            /// 没有找到用户的默认设置数据，如收货人，地址及发票
            /// </summary>
            public const string NotFoundUserDefaultData = "50604";

            /// <summary>
            /// 激活邮件的token已失效
            /// </summary>
            public const string EmailVerificationError = "50605";

            /// <summary>
            /// 手机验证码已失效
            /// </summary>
            public const string MobileVerificationError = "50606";

            /// <summary>
            /// 第三方账户未在茶涂站注册过或绑定过本地帐户
            /// </summary>
            public const string NotBindAccount = "51000";
            
            /// <summary>
            /// VIP资格未激活
            /// </summary>
            public const string InactiveVip = "51002";
        }
       
    }
}
