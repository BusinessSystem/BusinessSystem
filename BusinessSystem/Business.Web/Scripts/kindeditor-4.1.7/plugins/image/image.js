/*******************************************************************************
* KindEditor - WYSIWYG HTML Editor for Internet
* Copyright (C) 2006-2011 kindsoft.net
*
* @author Roddy <luolonghao@gmail.com>
* @site http://www.kindsoft.net/
* @licence http://www.kindsoft.net/license.php
*******************************************************************************/

KindEditor.plugin('image', function (K)
{
    var self = this, name = 'image',
		allowImageUpload = K.undef(self.allowImageUpload, true),
		allowImageRemote = K.undef(self.allowImageRemote, true),
		allowPicspace = K.undef(self.allowPicspace, true),
		formatUploadUrl = K.undef(self.formatUploadUrl, true),
		allowFileManager = K.undef(self.allowFileManager, false),
    //		uploadJson = K.undef(self.uploadJson, self.basePath + 'php/upload_json.php'),
		uploadJson = K.undef(self.uploadJson, '../upload_json.ashx'),
		imageTabIndex = K.undef(self.imageTabIndex, 0),
		imgPath = self.pluginsPath + 'image/images/',
		extraParams = K.undef(self.extraFileUploadParams, {}),
		filePostName = K.undef(self.filePostName, 'imgFile'),
		fillDescAfterUploadImage = K.undef(self.fillDescAfterUploadImage, false),
		lang = self.lang(name + '.');

    self.plugin.imageDialog = function (options)
    {
        var imageUrl = options.imageUrl,
			imageWidth = K.undef(options.imageWidth, ''),
			imageHeight = K.undef(options.imageHeight, ''),
			imageTitle = K.undef(options.imageTitle, ''),
			imageAlign = K.undef(options.imageAlign, ''),
			showRemote = K.undef(options.showRemote, true),
			showLocal = K.undef(options.showLocal, true),
			showPicspace = K.undef(options.showPicspace, true),
			tabIndex = K.undef(options.tabIndex, 0),
			clickFn = options.clickFn;
        var target = 'kindeditor_upload_iframe_' + new Date().getTime();
        var target3 = 'kindeditor_picspace_iframe_' + new Date().getTime();
        var hiddenElements = [];
        for (var k in extraParams)
        {
            hiddenElements.push('<input type="hidden" name="' + k + '" value="' + extraParams[k] + '" />');
        }
        //---
        var html = [
			'<div style="padding:20px;">',
        //tabs
			'<div class="tabs"></div>',
        //remote image - start
			'<div class="tab1" style="display:none;">',
        //url
			'<div class="ke-dialog-row">',
			'<label for="remoteUrl" style="width:60px;">' + lang.remoteUrl + '</label>',
			'<input type="text" id="remoteUrl" class="ke-input-text" name="url" value="" style="width:200px;" /> &nbsp;',
			'<span class="ke-button-common ke-button-outer">',
			'<input type="button" class="ke-button-common ke-button" name="viewServer" value="' + lang.viewServer + '" />',
			'</span>',
			'</div>',
        //size
			'<div class="ke-dialog-row">',
			'<label for="remoteWidth" style="width:60px;">' + lang.size + '</label>',
			lang.width + ' <input type="text" id="remoteWidth" class="ke-input-text ke-input-number" name="width" value="" maxlength="4" /> ',
			lang.height + ' <input type="text" class="ke-input-text ke-input-number" name="height" value="" maxlength="4" /> ',
			'<img class="ke-refresh-btn" src="' + imgPath + 'refresh.png" width="16" height="16" alt="" style="cursor:pointer;" title="' + lang.resetSize + '" />',
			'</div>',
        //align
			'<div class="ke-dialog-row">',
			'<label style="width:60px;">' + lang.align + '</label>',
			'<input type="radio" name="align" class="ke-inline-block" value="" checked="checked" /> <img name="defaultImg" src="' + imgPath + 'align_top.gif" width="23" height="25" alt="" />',
			' <input type="radio" name="align" class="ke-inline-block" value="left" /> <img name="leftImg" src="' + imgPath + 'align_left.gif" width="23" height="25" alt="" />',
			' <input type="radio" name="align" class="ke-inline-block" value="right" /> <img name="rightImg" src="' + imgPath + 'align_right.gif" width="23" height="25" alt="" />',
			'</div>',
        //title
			'<div class="ke-dialog-row">',
			'<label for="remoteTitle" style="width:60px;">' + lang.imgTitle + '</label>',
			'<input type="text" id="remoteTitle" class="ke-input-text" name="title" value="" style="width:200px;" />',
			'</div>',
			'</div>',
        //remote image - end
        //local upload - start
			'<div class="tab2" style="display:none;">',
			'<iframe name="' + target + '" style="display:none;"></iframe>',
			'<form class="ke-upload-area ke-form" method="post" enctype="multipart/form-data" target="' + target + '" action="' + K.addParam(uploadJson , 'dir=image') + '">',
        //file
			'<div class="ke-dialog-row">',
			hiddenElements.join(''),
			'<label style="width:60px;">上传分类</label><select name="picCategory_up" class="picCategory"></select>',
            ' <input type="button" value="刷新图片分类" onclick="GetPicCategory()" class="ctm_btn"/><br/>',
            '<label style="width:60px;">' + lang.localUrl + '</label>',
			'<input type="text" name="localUrl" class="ke-input-text" tabindex="-1" style="width:200px;" readonly="true" /> &nbsp;',
			'<input type="button" class="ke-upload-button" value="' + lang.upload + '" />',
			'</div>',
			'</form>',
			'</div>',
        //local upload - end

        //picspace - start
			'<div class="tab3" style="display:none;">',
			'<iframe name="' + target3 + '" style="display:none;"></iframe>',
			'<form class="ke-upload-area ke-form" method="get">',
        //file
			'<div class="ke-dialog-row">',
            '图片分类: <select id="picCategory_get" class="picCategory"></select> <input type="button" value="查询" class="ctm_btn" onclick="GetPicData()"/>',
            ' <input type="button" value="刷新图片分类" class="ctm_btn" onclick="GetPicCategory()"/>',
            ' 图片名称: <input type="text" id="picName"  style="width:90px;height:20px;"/>',
            '<div  id="divPicSpaceList"></div>',
            '<div  id="pagerDiv"></div>',
			'</div>',
			'</form>',
			'</div>',
        //picspace - end
			'</div>'
		].join('');
        var dialogWidth = showLocal || allowFileManager ? 750 : 400,
			dialogHeight = showLocal && showRemote && showPicspace ? 430 : 250;
        var dialog = self.createDialog({
            name: name,
            width: dialogWidth,
            height: dialogHeight,
            title: self.lang(name),
            body: html,
            yesBtn: {
                name: self.lang('yes'),
                click: function (e)
                {
                    // Bugfix: http://code.google.com/p/kindeditor/issues/detail?id=319
                    if (dialog.isLoading)
                    {
                        return;
                    }
                    // insert local image
                    if (showLocal && showRemote && showPicspace && tabs && tabs.selectedIndex === 1 || (!showRemote && !showPicspace))
                    {
                        if (uploadbutton.fileBox.val() == '')
                        {
                            alert(self.lang('pleaseSelectFile'));
                            return;
                        }
                        dialog.showLoading(self.lang('uploadLoading'));
                        uploadbutton.submit();
                        localUrlBox.val('');
                        return;
                    }
                    // insert picspace image
                    if (showLocal && showRemote && showPicspace && tabs && tabs.selectedIndex === 2 || (!showRemote && !showLocal))
                    {
                        for (var i = 0; i < selPicList.length; i++)
                        {
                            clickFn.call(self, selPicList[i], '', '', '', '0', '');
                        }
                        selPicList = [];
                        return;
                    }

                    // insert remote image
                    var url = K.trim(urlBox.val()),
						width = widthBox.val(),
						height = heightBox.val(),
						title = titleBox.val(),
						align = '';
                    alignBox.each(function ()
                    {
                        if (this.checked)
                        {
                            align = this.value;
                            return false;
                        }
                    });
                    if (url == 'http://' || K.invalidUrl(url))
                    {
                        alert(self.lang('invalidUrl'));
                        urlBox[0].focus();
                        return;
                    }
                    if (!/^\d*$/.test(width))
                    {
                        alert(self.lang('invalidWidth'));
                        widthBox[0].focus();
                        return;
                    }
                    if (!/^\d*$/.test(height))
                    {
                        alert(self.lang('invalidHeight'));
                        heightBox[0].focus();
                        return;
                    }
                    clickFn.call(self, url, title, width, height, 0, align);
                }
            },
            beforeRemove: function ()
            {
                viewServerBtn.unbind();
                widthBox.unbind();
                heightBox.unbind();
                refreshBtn.unbind();
            }
        }),
		div = dialog.div;
        //TODO:
        GetPicCategory();
        var urlBox = K('[name="url"]', div),
			localUrlBox = K('[name="localUrl"]', div),
			viewServerBtn = K('[name="viewServer"]', div),
			widthBox = K('.tab1 [name="width"]', div),
			heightBox = K('.tab1 [name="height"]', div),
			refreshBtn = K('.ke-refresh-btn', div),
			titleBox = K('.tab1 [name="title"]', div),
			alignBox = K('.tab1 [name="align"]', div);

        var tabs;
        if (showRemote && showLocal && showPicspace)
        {
            tabs = K.tabs({
                src: K('.tabs', div),
                afterSelect: function (i) { }
            });
            tabs.add({
                title: lang.remoteImage,
                panel: K('.tab1', div)
            });
            tabs.add({
                title: lang.localImage,
                panel: K('.tab2', div)
            });
            tabs.add({
                title: lang.picspaceImage,
                panel: K('.tab3', div)
            });
            tabs.select(tabIndex);
        } else if (showRemote)
        {
            K('.tab1', div).show();
        } else if (showLocal)
        {
            K('.tab2', div).show();
        } else if (showPicspace)
        {
            K('.tab3', div).show();
        }

        var uploadbutton = K.uploadbutton({
            button: K('.ke-upload-button', div)[0],
            fieldName: filePostName,
            form: K('.ke-form', div),
            target: target,
            width: 60,
            afterUpload: function (data)
            {
                dialog.hideLoading();
                if (data.error === 0)
                {
                    var url = data.url;
                    if (formatUploadUrl)
                    {
                        url = K.formatUrl(url, 'absolute');
                    }
                    if (self.afterUpload)
                    {
                        self.afterUpload.call(self, url, data, name);
                    }
                    if (!fillDescAfterUploadImage)
                    {
                        clickFn.call(self, url, data.title, data.width, data.height, data.border, data.align);
                    } else
                    {
                        K(".ke-dialog-row #remoteUrl", div).val(url);
                        K(".ke-tabs-li", div)[0].click();
                        K(".ke-refresh-btn", div).click();
                    }
                } else
                {
                    alert(data.message);
                }
            },
            afterError: function (html)
            {
                dialog.hideLoading();
                self.errorDialog(html);
            }
        });
        uploadbutton.fileBox.change(function (e)
        {
            localUrlBox.val(uploadbutton.fileBox.val());
        });
        if (allowFileManager)
        {
            viewServerBtn.click(function (e)
            {
                self.loadPlugin('filemanager', function ()
                {
                    self.plugin.filemanagerDialog({
                        viewType: 'VIEW',
                        dirName: 'image',
                        clickFn: function (url, title)
                        {
                            if (self.dialogs.length > 1)
                            {
                                K('[name="url"]', div).val(url);
                                if (self.afterSelectFile)
                                {
                                    self.afterSelectFile.call(self, url);
                                }
                                self.hideDialog();
                            }
                        }
                    });
                });
            });
        } else
        {
            viewServerBtn.hide();
        }
        var originalWidth = 0, originalHeight = 0;
        function setSize(width, height)
        {
            widthBox.val(width);
            heightBox.val(height);
            originalWidth = width;
            originalHeight = height;
        }
        refreshBtn.click(function (e)
        {
            var tempImg = K('<img src="' + urlBox.val() + '" />', document).css({
                position: 'absolute',
                visibility: 'hidden',
                top: 0,
                left: '-1000px'
            });
            tempImg.bind('load', function ()
            {
                setSize(tempImg.width(), tempImg.height());
                tempImg.remove();
            });
            K(document.body).append(tempImg);
        });
        widthBox.change(function (e)
        {
            if (originalWidth > 0)
            {
                heightBox.val(Math.round(originalHeight / originalWidth * parseInt(this.value, 10)));
            }
        });
        heightBox.change(function (e)
        {
            if (originalHeight > 0)
            {
                widthBox.val(Math.round(originalWidth / originalHeight * parseInt(this.value, 10)));
            }
        });
        urlBox.val(options.imageUrl);
        setSize(options.imageWidth, options.imageHeight);
        titleBox.val(options.imageTitle);
        alignBox.each(function ()
        {
            if (this.value === options.imageAlign)
            {
                this.checked = true;
                return false;
            }
        });
        if (showRemote && tabIndex === 0)
        {
            urlBox[0].focus();
            urlBox[0].select();
        }
        return dialog;
    };
    self.plugin.image = {
        edit: function ()
        {
            var img = self.plugin.getSelectedImage();
            self.plugin.imageDialog({
                imageUrl: img ? img.attr('data-ke-src') : 'http://',
                imageWidth: img ? img.width() : '',
                imageHeight: img ? img.height() : '',
                imageTitle: img ? img.attr('title') : '',
                imageAlign: img ? img.attr('align') : '',
                showRemote: allowImageRemote,
                showLocal: allowImageUpload,
                showPicspace: allowPicspace,
                tabIndex: img ? 0 : imageTabIndex,
                clickFn: function (url, title, width, height, border, align)
                {
                    self.exec('insertimage', url, title, width, height, border, align);
                    // Bugfix: [Firefox] 上传图片后，总是出现正在加载的样式，需要延迟执行hideDialog
                    setTimeout(function ()
                    {
                        self.hideDialog().focus();
                    }, 0);
                }
            });
        },
        'delete': function ()
        {
            var target = self.plugin.getSelectedImage();
            if (target.parent().name == 'a')
            {
                target = target.parent();
            }
            target.remove();
            // [IE] 删除图片后立即点击图片按钮出错
            self.addBookmark();
        }
    };
    self.clickToolbar(name, self.plugin.image.edit);
});

function GetPicCategory() {
    $.ajax({
        type: "POST",
        url: "/Product/GetPictureCategoryList",
        dataType: 'json',
        data: { cmd: "getCategory" },
        success: function (data) {
            var option = "";
            for (var i = 0; i < data.length; i++) {
                option += "<option value='" + data[i].Key + "'>" + data[i].Value + "</option>";
            }

            $(".picCategory").html(option);
        },
        error: function () {
            alert("获取图片类目出错");
        }
    });
}
function GetPicData() {
    $("#pagerDiv").JqueryPager({
        postUrl: "/Product/GetPicturesByCategoryName",
        pageSize: 12,
        divOfTableHtmlAppendToID: "divPicSpaceList",
        redirectToWhenGetDataID: "divPicSpaceList",
        postDataJson: { cmd: "getPicture", cid: $("#picCategory_get").val(), cname: $("#picName").val() },
        success: function successGetData(data) {
            //这句是需要的
            this.recordCount = parseInt(data.count.toString());

            var html = "<ul id='piclist'>";
            for (var i = 0; i < data.pic.length; i++) {
                html += "<li><img id='img_" + data.pic[i]["Id"] + "' src='" + data.pic[i]["Url"] + "' title='" + data.pic[i]["PictureName"] + "' onerror='this.src=\"http://www.chatop.cn/images/no-pic.jpg\";return false;' onclick='clickPic(this)' /></li>";
            }
            html += "</ul>";
//            
//            alert(data.pic[i]["FullUrl"]);
            return html;
        },
        error: function errorGetData() {
            alert("获取图片数据出错");
        }
    });
}

function clickPic(url) {
    var r = $.inArray(url.src, selPicList);

    if (r == -1) {
        $("#"+url.id).css("border", "3px solid red");
        selPicList.push(url.src);
    }
    else {
        $("#" + url.id).css("border", "");
        selPicList.pop();
    }
}

var selPicList = [];