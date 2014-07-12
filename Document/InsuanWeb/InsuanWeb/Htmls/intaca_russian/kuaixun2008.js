var Groj = function() {
	this.initialize.apply(this, arguments);
}
// download by http://www.codefans.net
Groj.prototype = {
	initialize: function(url, options) {
    //	跨域请求
		this.url = url;
		this.setOptions(options);
		this.request();
	},
	
	setOptions: function(options) {
		this.options = {
			variable: '',
			step: 500,		//	循环问询间隔时间（毫秒为单位）
			timeout: 5000	//	超时时间（毫秒为单位）
		}
		for (var i in options) {
			this.options[i] = options[i];
		}
		this.varArr = this.options.variable.split(".");
	},
	
	request: function() {
		if (this.isVarValid()) {
			this.onResult();
			return;
		}
		
		var _this = this;
		
		var oHead = document.getElementsByTagName('head')[0];
		this.link = document.createElement('script');
		this.link.src = this.url;
		this.link.type = 'text/javascript';
		this.timer = window.setInterval(function() {_this.onCheck();}, this.options.step);
		oHead.appendChild(this.link);
		
		//	设置超时回调函数
		this.outTimer = window.setTimeout(function() {_this.onTimeout();}, this.options.timeout)
	},
	
	isVarValid: function() {
		var vari = this.getVar();
		return typeof(vari) != "undefined" && vari != null;
		//return (typeof(this.getVar()) != "undefined" );
	},
	
	getVar: function() {
		var vari = window;
		for (var i=0; i<this.varArr.length; i++) {
			vari = vari[this.varArr[i]];
			
			if (typeof(vari) == "undefined" || vari == null) break;
		}
		return vari;
	},
	clearVar:function(){
		var vari = window;
		for (var i=0; i<this.varArr.length; i++) {
			var vvv= vari[this.varArr[i]];
			if (typeof(vvv) == "undefined" || vvv == null) {
				break;
			}
			vari[this.varArr[i]]=null;
		}
	},
	onResult: function() {
		this.clearTimer();
		var currentVar=this.getVar();
		this.options.onSuccess(currentVar);
		
		//清除变量，移除该SCRIPT标签.
		this.clearVar();
		try{
		if(this.link){
			this.link.parentNode.removeChild(this.link);
		}
		}catch(e){};
	},
	
	onCheck: function() {
		if (this.isVarValid()) this.onResult();
		else return;
	},
	
	onTimeout: function() {
		this.clearTimer();
		//if (this.link) Element.remove(this.link);
		if(this.link){
			this.link.parentNode.removeChild(this.link);
		}
		else if (this.options.onFailure) this.options.onFailure();
	},
	
	clearTimer: function() {
		if (this.timer) {
			window.clearInterval(this.timer);
			this.timer = null;
		}
		window.clearTimeout(this.outTimer);
		this.outTimer = null;
	}
} 
////////////////////////////////////////////////////////////////////////
		function GR() {
			if (arguments.length < 1) return;
			for (var i=0; i<arguments.length; i++) {
				GRO(arguments[i]);
			}
		}
		function GRO(options) {
			if (typeof(options.interval) == "number" && options.interval > 0) {
				function run() {
					var url = typeof(options.url) == "function" ? options.url() : options.url;
					var variable = typeof(options.variable) == "function" ? options.variable() : options.variable;
					if (!!url && !!variable) {
						new Groj(url, {
						  variable: variable,
						  onSuccess: options.onSuccess,
						  onFailure: options.onFailure || function() {}
						});
					}
				}
				run();
				window[options.hold] = window.setInterval(run, options.interval);
			} else {
				var url = typeof(options.url) == "function" ? options.url() : options.url;
				var variable = typeof(options.variable) == "function" ? options.variable() : options.variable;
				if (!!url && !!variable) {
					new Groj(url, {
					  variable: variable,
					  onSuccess: options.onSuccess,
					  onFailure: options.onFailure || function() {}
					});
				}
			}
		}
////////////////////////////////////////////////////////////////////////		

var KuaiXun2008 = function() {
	this.initialize.apply(this, arguments);
}
KuaiXun2008.prototype = {
	initialize: function() {
		//this.dataJsPath = "http://testserver.sohu.com/olympicKuaixun/data.js";
		//this.imagesPath = "http://testserver.sohu.com/olympicKuaixun/images";
		this.dataJsPath = "data.js";
		this.imagesPath = "images";
	  	this.popDivs ='<style>';
		this.popDivs += '#olympic2008pop *{color:#333;font-size:12px;font-family:"宋体";text-align:center;}';
		this.popDivs += '#olympic2008pop > div{margin-right:auto;margin-left:auto;text-align:center;}'; 
		this.popDivs += '#olympic2008pop div,#olympic2008pop form,#olympic2008pop ul,#olympic2008pop ol,#olympic2008pop li,#olympic2008pop span,#olympic2008pop p{margin:0;padding:0;border:0;}';
		this.popDivs += '#olympic2008pop img,#olympic2008pop a img{margin:0;padding:0;border:0;}';
		this.popDivs += '#olympic2008pop a{color:#333;text-decoration:none; }';
		this.popDivs += '#olympic2008pop a:hover{color:#D30000;text-decoration:underline;}';
		this.popDivs += '#olympic2008pop .kuaixunmain{width:258px;height:216px;FILTER:progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled="true",sizingMethod="scale",src="'+this.imagesPath+'/pop_bg.png");background:#fff;FILTER: Alpha(opacity=80);}';
		this.popDivs += '#olympic2008pop .kuaixunmain .kxtitle{display:inline;float:left;width:155px;height:15px;margin:34px 0 0 72px;text-align: left; font-size:14px;}';
		this.popDivs += '#olympic2008pop .kuaixunmain .kxclose{ float:right;width:15px;height:15px;margin:34px 10px 0 0;display:inline;position:relative}';
		this.popDivs += '#olympic2008pop .kuaixunmain .kxsohu2008{clear:both;width:239px;height:22px;margin:0 auto;text-align:left;position:relative}';
		this.popDivs += '#olympic2008pop .kuaixunads240x40{ margin:5px 0 0;text-align:center;position:relative}';
		this.popDivs += '#olympic2008pop .kuaixunmain #kuaixunContent{float:left;width: 230px;height:72px; margin:15px 0 0 15px;display:inline;line-height: 22px;text-align:left;position:relative;word-wrap:break-word}';
		this.popDivs += '</style>';
		this.popDivs += '<div id="olympic2008pop" style="Z-INDEX: 99999;VISIBILITY: hidden;POSITION: absolute;left:0px;top:0px;">';
		this.popDivs += '<div class="kuaixunmain">';
		this.popDivs += '<div class="kxtitle">2012 Exhibition</div>';
		//this.popDivs += '<div class="kxclose"><a style="cursor:hand;" onclick="window.olympic2008pop.style.display=\'none\';"><img src="'+this.imagesPath+'/close.png" width="15" height="15" alt="" /></a></div>';
		this.popDivs += '<div class="kxclose"><a style="cursor:hand;" onclick="kuaixun2008.closeDiv();"><img src="'+this.imagesPath+'/close.png" width="15" height="15" alt="" /></a></div>';
		this.popDivs += '<div id="kuaixunContent"></div>';
		this.popDivs += '</div>';
		this.popDivs += '</div>';
		this._initGlobal();
	},
	
	_initGlobal:function() {
		this.docUrl = document.location.href;
		this.cookieDomain = "";
		this.CheckCookieDomain();
		
		this.kuaixun2008type = this.GetCookie("kuaixun2008type");
		this.kuaixun2008id = parseInt(this.GetCookie("kuaixun2008id"));
		
		this.timeDifference = 1000 * 60 * 10; //时间间隔10分钟
		this.kuaixun2008requestTime = this.GetCookie("kuaixun2008requestTime");
		
		this.isSHTML = document.compatMode=="CSS1Compat" ? true:false;
		
		this.divTop=0;
		this.divLeft=0;
		this.divWidth=0;
		this.divHeight = 0;
		this.docHeight = 0;
		this.docWidth = 0;
		this.objTimer = 0;
		this.timecounter = 0;
		this.showtimeout = 30; //显示关闭时间，单位为秒
		this.olympic2008pop = null;
		document.write(this.popDivs);
		
		this.olympic2008pop = document.getElementById("olympic2008pop");
		if (window.addEventListener) {
		    window.addEventListener("resize", this.resizeDiv, false);
		} else {
		    window.attachEvent("onresize", this.resizeDiv);
		}
		//window.onerror = window.onerror || function(e){}
	},
	start:function() {
		var this_ = this;
		var getKuaixun = {
			url:function(){
				var date = new Date();
				return this_.dataJsPath+"?random="+date.getTime();
			},
			variable: "kuaixun",
			interval: -1,
			hold: "kuaixun_hold",
			onSuccess : function(data){
				if(data.id != null && data.id != "" && typeof(data.id)!= "undefined"){
					if( data.type == this_.kuaixun2008type && parseInt(data.id) == this_.kuaixun2008id ){
						
					}else{
						this_.kuaixun2008type = data.type;
						this_.kuaixun2008id = parseInt(data.id);
						this_.SetCookie("kuaixun2008type",data.type);
						this_.SetCookie("kuaixun2008id",data.id);
						if(data.type == "system"){
							this_.SetCookie("olympic2008kuaixunId",data.id);					
						}
						var dataContent = data.content.replace(/\r\n/g,"<br>");
						if(data.url != ""){
							if(data.url.indexOf("http://")<0){
								data.url = "http://"+data.url; 
							}
							dataContent = this_.writeStr(dataContent);
							//dataContent += "&nbsp;&nbsp;<a href=\""+data.url+"\" target=\"_blank\">详细</a>&gt;&gt";
							dataContent += "</a>&nbsp;&nbsp;<a href=\""+data.url+"\" target=\"_blank\">详细</a>&gt;&gt";
							dataContent = "<a href=\""+data.url+"\" target=\"_blank\">" + dataContent;
						}
						this_.initDiv(dataContent);
					}
				}
			}
		};
		
		//if( (new Date().getTime()-this.kuaixun2008requestTime) > this.timeDifference){
				this.SetCookie("kuaixun2008requestTime",new Date().getTime());
		    	GR(getKuaixun);
		//}
	
	},
	getStrLen:function(str){
		var len = 0;
		var cnstrCount = 0; 
		for(var i = 0 ; i <str.length ; i++){
		  if(str.charCodeAt(i)>255)
		   cnstrCount = cnstrCount + 1 ;
		}
		len = str.length + cnstrCount;
		return len;
	},
	substring:function(str,start,end){
		var retStr = "";
		var count = 0;
		for(var i=0;i<str.length;i++){
			if(str.charCodeAt(i)>255){
				count += 2;
			}else{
				count += 1;
			}
			if(count<=end){
				retStr += str.charAt(i);	
			}else{
				break;
			}
		}
		return retStr;
	},
	writeStr:function(str){
		var newStr = "";
		if(this.getStrLen(str)>100){
			newStr = this.substring(str,0,100)+"...";
		}else{
			newStr = str;
		}
		return newStr;
	},
	getCookieVal:function (offset){
	    var endstr = document.cookie.indexOf (";", offset);
	    if (endstr == -1)
	      endstr = document.cookie.length;
	    return unescape(document.cookie.substring(offset, endstr));
	},
	
	GetCookie:function (name){
	    var arg = name + "=";
	    var alen = arg.length;
	    var clen = document.cookie.length;
	    var i = 0;
	    while (i < clen) {
	      var j = i + alen;
	      if (document.cookie.substring(i, j) == arg)
	        return this.getCookieVal (j);
	      i = document.cookie.indexOf(" ", i) + 1;
	      if (i == 0) break;
	    }
	    return null;
	},
	SetCookie:function (name, value){
	    var argv = this.SetCookie.arguments;
	    var argc = this.SetCookie.arguments.length;
	    //var expires = (argc > 2) ? argv[2] : null;
	    var expiresTime = new Date("2008","8","10");
	    var expires = (argc > 2) ? argv[2] : expiresTime;
	    var path = (argc > 3) ? argv[3] : "/";
	    var domain = (argc > 4) ? argv[4] : this.cookieDomain;
	    var secure = (argc > 5) ? argv[5] : false;
	    document.cookie = name + "=" + escape (value) +((expires == null) ? "" : ("; expires=" + expires.toGMTString())) +((path == null) ? "" : ("; path=" + path)) +((domain == null) ? "" : ("; domain=" + domain)) +((secure == true) ? "; secure" : "");
	},
	CheckCookieDomain:function(){
		var sohuMatrix = [".sohu.com",".sogou.com",".chinaren.com",".focus.cn",".17173.com",".goodfeel.com.cn",".go2map.com"];
		for(var i=0;i<sohuMatrix.length;i++){
			if(this.docUrl.indexOf(sohuMatrix[i])>=0){
				this.cookieDomain = sohuMatrix[i];
				break;
			}
		}
	},
	/********************** view Div **************************/
	//initDiv --> moveDiv --> resizeDiv --> closeDiv
	
	initDiv:function (content){
			try{
				this.divTop = parseInt(this.olympic2008pop.style.top,10);//div上部的像素点
				this.divLeft = parseInt(this.olympic2008pop.style.left,10);//div左边的像素点
				this.divHeight = parseInt(this.olympic2008pop.offsetHeight,10);//div自身高度
				this.divWidth = parseInt(this.olympic2008pop.offsetWidth,10);//div自身宽度
				//+++在web标准声明下用document.documentElement; 如果是未声明用document.body+++用document.compatMode判断+++
				if(this.isSHTML){
					this.docHeight = document.documentElement.clientHeight;//页面高度 
					this.docWidth = document.documentElement.clientWidth;//页面宽度
					this.olympic2008pop.style.top = parseInt(document.documentElement.scrollTop,10) + this.docHeight + 10 + "px";//变换div上部的位置 
					this.olympic2008pop.style.left = this.docWidth - this.divWidth + parseInt(document.documentElement.scrollLeft,10) + "px";//变换div左边的位置
				}else{
					this.docHeight = document.body.clientHeight;//页面高度
					this.docWidth = document.body.clientWidth;//页面宽度
					this.olympic2008pop.style.top = parseInt(document.body.scrollTop,10) + this.docHeight + 10;//变换div上部的位置 
					this.olympic2008pop.style.left = this.docWidth - this.divWidth + parseInt(document.body.scrollLeft,10);//变换div左边的位置
				}
				document.getElementById("kuaixunContent").innerHTML = content;//填充内容
				this.olympic2008pop.style.visibility="visible";
				this.objTimer = window.setInterval("kuaixun2008.moveDiv()",1);
			}catch(e){
			}
	},
	
	moveDiv:function(){
		try{
			var tmpVar1;
			if(this.isSHTML){
				tmpVar1 = parseInt(document.documentElement.scrollTop,10);
			}else{
				tmpVar1 = parseInt(document.body.scrollTop,10);
			}
			
			if(parseInt(this.olympic2008pop.style.top,10) <= (this.docHeight - this.divHeight + tmpVar1)){
				if(this.objTimer){
					window.clearInterval(this.objTimer);
				}
				this.timecounter=0;
				this.resizeDiv();
				this.objTimer = window.setInterval("kuaixun2008.resizeDiv()",1000);//1秒调一次，实现showtimeout*1000ms的效果
			}else{
				this.divTop = parseInt(this.olympic2008pop.style.top,10);
				this.olympic2008pop.style.top = this.divTop - 5 +"px";
			}
		}catch(e){
		}
	},
	
	resizeDiv:function(){
		this.timecounter+=1;
		if((this.timecounter) >= this.showtimeout){//超过时间限制就隐藏Div
			if(this.objTimer) window.clearInterval(this.objTimer)
			this.objTimer = window.setInterval("kuaixun2008.closeDiv()",1);
		}
		try{
			this.divHeight = parseInt(this.olympic2008pop.offsetHeight,10);
			this.divWidth = parseInt(this.olympic2008pop.offsetWidth,10);
			if(this.isSHTML){
				this.docWidth = document.documentElement.clientWidth;
				this.docHeight = document.documentElement.clientHeight;
				this.olympic2008pop.style.top = this.docHeight - this.divHeight + parseInt(document.documentElement.scrollTop,10)+"px";
				this.olympic2008pop.style.left = this.docWidth - this.divWidth + parseInt(document.documentElement.scrollLeft,10)+"px";
			}else{
				this.docWidth = document.body.clientWidth;
				this.docHeight = document.body.clientHeight;
				this.olympic2008pop.style.top = this.docHeight - this.divHeight + parseInt(document.body.scrollTop,10);
				this.olympic2008pop.style.left = this.docWidth - this.divWidth + parseInt(document.body.scrollLeft,10);
			}
			
		}catch(e){
		}
	},
	
	closeDiv:function(){
		this.olympic2008pop.style.visibility="hidden";
		if(this.objTimer) window.clearInterval(this.objTimer);
	}
}

var kuaixun2008 = new KuaiXun2008();
kuaixun2008.start();