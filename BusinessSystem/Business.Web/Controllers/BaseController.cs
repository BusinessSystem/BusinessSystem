﻿using System;
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
            return View(BaseService.GetIntentions());
        }

        [HttpPost]
        public ActionResult IntentionAdd(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                Intention intention = IntentionFactory.Create(description, 1234);
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
                return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.DataError));
        }

        [HttpGet]
        public ActionResult IntentionDelete(long id)
        {
            BaseService.DeleteIntention(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }


        [HttpGet]
        public ActionResult UserDefinedList()
        {
            return View(BaseService.GetUserDefineds());
        }

        [HttpPost]
        public ActionResult UserDefinedAdd(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                UserDefined userDefined = UserDefinedFactory.Create(description, 1234);
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
            return Json(InfoTools.GetMsgInfo(ResponseCode.DataError));
        }

        [HttpPost]
        public ActionResult UserDefinedDelete(long id)
        {
            BaseService.DeleteUserDefined(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
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
            return Json(InfoTools.GetMsgInfo(responseCode));
        }

        [HttpPost]
        public ActionResult DictionaryDelete(long id)
        {
            BaseService.DictionaryDelete(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }
    }
}
