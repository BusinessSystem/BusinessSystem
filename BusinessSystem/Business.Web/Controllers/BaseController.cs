using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;
using Business.Utils;
using Business.Utils.Info;
using Business.Web.PageModel;

namespace Business.Web.Controllers
{
    public class BaseController : AdminBaseController
    {
        //
        // GET: /Base/

        [HttpGet]
        public ActionResult IntentionList()
        {
           return View(BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id));
        }

        [HttpPost]
        public ActionResult IntentionAdd(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                Intention intention = IntentionFactory.Create(description, CurrentManager.Id, CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
                BaseService.SaveIntention(intention);
            }
            return RedirectToAction("IntentionList");
        }

        [HttpPost]
        public ActionResult IntentionEdit(long id, string description)
        {
            if (id != 0 && !string.IsNullOrEmpty(description))
            {
                Intention intention = BaseService.GetIntentionById(id);
                intention.Description = description;
                BaseService.SaveIntention(intention);
                return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.DataError), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IntentionDelete(long id)
        {
            BaseService.DeleteIntention(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok),JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UserDefinedList()
        {
            return View(BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id));
        }

        [HttpPost]
        public ActionResult UserDefinedAdd(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                UserDefined userDefined = UserDefinedFactory.Create(description, CurrentManager.Id, CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
                BaseService.SaveUserDefined(userDefined);
            }
            return RedirectToAction("UserDefinedList");
        }

        [HttpPost]
        public ActionResult UserDefinedEdit(long id, string description)
        {
            if (id != 0 && !string.IsNullOrEmpty(description))
            {
                UserDefined userDefined = BaseService.GetUserDefinedById(id);
                userDefined.Description = description;
                BaseService.SaveUserDefined(userDefined);
                return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.DataError), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UserDefinedDelete(long id)
        {
            BaseService.DeleteUserDefined(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DictionaryList()
        {
            PageDictionary pageModel = new PageDictionary();
            ValueTypeEnum valueType = ValueTypeEnum.Language;
            if (!string.IsNullOrEmpty(Request["valueType"]))
            {
                valueType = (ValueTypeEnum) short.Parse(Request["valueType"].ToString());
            }
            ViewBag.ValueType = valueType;
            pageModel.BaseDictionaries = BaseService.GetBaseDictionaries(valueType);
            pageModel.ValueTypes = EnumTools.GetEnumDescriptions<ValueTypeEnum>();
            return View(pageModel);
        }

        [HttpGet]
        public ActionResult DictionaryAdd()
        {
            Dictionary<ValueTypeEnum, string> dictionary = EnumTools.GetEnumDescriptions<ValueTypeEnum>();
            return View(dictionary);
        }

        [HttpPost]
        public ActionResult DictionaryAdd(string dataValue, string description, string datasort, ValueTypeEnum valueType)
        {
            int dataSort = 1;
            if (!int.TryParse(datasort, out dataSort))
            {
                dataSort = 1;
            }
            BaseDictionary baseDictionary = BaseDictionaryFactory.Create(valueType, dataSort, dataValue, description,
                CurrentManager.UserName, description);
            string responseCode = BaseService.SaveDictionary(baseDictionary);
            return Json(InfoTools.GetMsgInfo(responseCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DictionaryDelete(long id)
        {
            BaseService.DictionaryDelete(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DictionaryEdit(long id)
        {
            EditDictionary editDictionary = new EditDictionary();
            editDictionary.BaseDictionary = BaseService.GetDictionaryById(id);
            editDictionary.ValueTypes = EnumTools.GetEnumDescriptions<ValueTypeEnum>();
            return View(editDictionary);
        }

        [HttpPost]
        public ActionResult DictionaryEdit(long id, string dataValue, string description, string datasort,
            ValueTypeEnum valueType)
        {
            int dataSort = 1;
            if (!int.TryParse(datasort, out dataSort))
            {
                dataSort = 1;
            }
            BaseDictionary baseDictionary = BaseService.GetDictionaryById(id);
            if (baseDictionary != null)
            {
                baseDictionary.Value = dataValue;
                baseDictionary.Description = description;
                baseDictionary.DicId = dataSort;
                baseDictionary.ValueType = valueType;
                string responseCode = BaseService.SaveDictionary(baseDictionary);
                return Json(InfoTools.GetMsgInfo(responseCode));
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.NotFoundData), JsonRequestBehavior.AllowGet);
        }
    }
}
