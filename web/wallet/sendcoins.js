gx.evt.autoSkip=!1;gx.define("wallet.sendcoins",!1,function(){this.ServerClass="wallet.sendcoins";this.PackageName="GeneXus.Programs";this.ServerFullClass="wallet.sendcoins.aspx";this.setObjectType("web");this.hasEnterEvent=!1;this.skipOnEnter=!1;this.autoRefresh=!0;this.fullAjax=!0;this.supportAjaxEvents=!0;this.ajaxSecurityToken=!0;this.DSO="GeneXusUnanimoUnanimoWeb";this.SetStandaloneVars=function(){this.AV7error=gx.fn.getControlValue("vERROR");this.AV23wallet=gx.fn.getControlValue("vWALLET");this.AV20transactionsToSend=gx.fn.getControlValue("vTRANSACTIONSTOSEND");this.AV13changeTo=gx.fn.getControlValue("vCHANGETO");this.AV25PopupName=gx.fn.getControlValue("vPOPUPNAME")};this.e17081_client=function(){return this.clearMessages(),this.AV24sendAllCoins?(this.AV14sendCoins=this.AV18totalBalance,gx.fn.setCtrlProperty("vSENDCOINS","Enabled",!1)):(this.AV14sendCoins=0,gx.fn.setCtrlProperty("vSENDCOINS","Enabled",!0)),this.refreshOutputs([{av:"AV14sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"gx.fn.getCtrlProperty('vSENDCOINS','Enabled')",ctrl:"vSENDCOINS",prop:"Enabled"}]),this.OnClientEventEnd(),gx.$.Deferred().resolve()};this.e12082_client=function(){return this.executeServerEvent("'NEXT'",!1,null,!1,!1)};this.e13082_client=function(){return this.executeServerEvent("'SEND COINS'",!1,null,!1,!1)};this.e14082_client=function(){return this.executeServerEvent("'CANCEL'",!1,null,!1,!1)};this.e15082_client=function(){return this.setEventParameters([["PopupName","vPOPUPNAME","AV25PopupName"]],arguments[2]),this.executeServerEvent("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",!0,null,!0,!0)};this.e18082_client=function(){return this.executeServerEvent("ENTER",!0,null,!1,!1)};this.e19082_client=function(){return this.executeServerEvent("CANCEL",!0,null,!1,!1)};this.GXValidFnc=[];var n=this.GXValidFnc;this.GXCtrlIds=[2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41];this.GXLastCtrlId=41;n[2]={id:2,fld:"",grid:0};n[3]={id:3,fld:"MAINTABLE",grid:0};n[4]={id:4,fld:"",grid:0};n[5]={id:5,fld:"",grid:0};n[6]={id:6,fld:"",grid:0};n[7]={id:7,fld:"",grid:0};n[8]={id:8,lvl:0,type:"decimal",len:16,dec:8,sign:!1,pic:"ZZZZZZ9.99999999",ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vTOTALBALANCE",fmt:0,gxz:"ZV18totalBalance",gxold:"OV18totalBalance",gxvar:"AV18totalBalance",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV18totalBalance=gx.fn.toDecimalValue(n,",","."))},v2z:function(n){n!==undefined&&(gx.O.ZV18totalBalance=gx.fn.toDecimalValue(n,",","."))},v2c:function(){gx.fn.setDecimalValue("vTOTALBALANCE",gx.O.AV18totalBalance,8,".");typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV18totalBalance=this.val())},val:function(){return gx.fn.getDecimalValue("vTOTALBALANCE",",",".")},nac:gx.falseFn};this.declareDomainHdlr(8,function(){});n[9]={id:9,fld:"",grid:0};n[10]={id:10,fld:"",grid:0};n[11]={id:11,fld:"",grid:0};n[12]={id:12,lvl:0,type:"boolean",len:4,dec:0,sign:!1,ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:"e17081_client",evt_cvcing:null,rgrid:[],fld:"vSENDALLCOINS",fmt:0,gxz:"ZV24sendAllCoins",gxold:"OV24sendAllCoins",gxvar:"AV24sendAllCoins",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"checkbox",v2v:function(n){n!==undefined&&(gx.O.AV24sendAllCoins=gx.lang.booleanValue(n))},v2z:function(n){n!==undefined&&(gx.O.ZV24sendAllCoins=gx.lang.booleanValue(n))},v2c:function(){gx.fn.setCheckBoxValue("vSENDALLCOINS",gx.O.AV24sendAllCoins,!0)},c2v:function(){this.val()!==undefined&&(gx.O.AV24sendAllCoins=gx.lang.booleanValue(this.val()))},val:function(){return gx.fn.getControlValue("vSENDALLCOINS")},nac:gx.falseFn,values:["true","false"]};n[13]={id:13,fld:"",grid:0};n[14]={id:14,fld:"",grid:0};n[15]={id:15,fld:"",grid:0};n[16]={id:16,fld:"",grid:0};n[17]={id:17,lvl:0,type:"decimal",len:16,dec:8,sign:!1,pic:"ZZZZZZ9.99999999",ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vSENDCOINS",fmt:0,gxz:"ZV14sendCoins",gxold:"OV14sendCoins",gxvar:"AV14sendCoins",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV14sendCoins=gx.fn.toDecimalValue(n,",","."))},v2z:function(n){n!==undefined&&(gx.O.ZV14sendCoins=gx.fn.toDecimalValue(n,",","."))},v2c:function(){gx.fn.setDecimalValue("vSENDCOINS",gx.O.AV14sendCoins,8,".");typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV14sendCoins=this.val())},val:function(){return gx.fn.getDecimalValue("vSENDCOINS",",",".")},nac:gx.falseFn};this.declareDomainHdlr(17,function(){});n[18]={id:18,fld:"",grid:0};n[19]={id:19,fld:"",grid:0};n[20]={id:20,fld:"",grid:0};n[21]={id:21,fld:"",grid:0};n[22]={id:22,lvl:0,type:"char",len:250,dec:0,sign:!1,ro:0,multiline:!0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vSENDTO",fmt:0,gxz:"ZV15sendTo",gxold:"OV15sendTo",gxvar:"AV15sendTo",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV15sendTo=n)},v2z:function(n){n!==undefined&&(gx.O.ZV15sendTo=n)},v2c:function(){gx.fn.setControlValue("vSENDTO",gx.O.AV15sendTo,0);typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV15sendTo=this.val())},val:function(){return gx.fn.getControlValue("vSENDTO")},nac:gx.falseFn};this.declareDomainHdlr(22,function(){});n[23]={id:23,fld:"",grid:0};n[24]={id:24,fld:"",grid:0};n[25]={id:25,fld:"",grid:0};n[26]={id:26,fld:"",grid:0};n[27]={id:27,lvl:0,type:"svchar",len:250,dec:0,sign:!1,ro:0,multiline:!0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vDESCRIPTION",fmt:0,gxz:"ZV31description",gxold:"OV31description",gxvar:"AV31description",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV31description=n)},v2z:function(n){n!==undefined&&(gx.O.ZV31description=n)},v2c:function(){gx.fn.setControlValue("vDESCRIPTION",gx.O.AV31description,0)},c2v:function(){this.val()!==undefined&&(gx.O.AV31description=this.val())},val:function(){return gx.fn.getControlValue("vDESCRIPTION")},nac:gx.falseFn};n[28]={id:28,fld:"",grid:0};n[29]={id:29,fld:"",grid:0};n[30]={id:30,fld:"",grid:0};n[31]={id:31,fld:"",grid:0};n[32]={id:32,lvl:0,type:"decimal",len:16,dec:8,sign:!1,pic:"ZZZZZZ9.99999999",ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vUSERFEE",fmt:0,gxz:"ZV21userFee",gxold:"OV21userFee",gxvar:"AV21userFee",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"combo",v2v:function(n){n!==undefined&&(gx.O.AV21userFee=gx.fn.toDecimalValue(n,",","."))},v2z:function(n){n!==undefined&&(gx.O.ZV21userFee=gx.fn.toDecimalValue(n,",","."))},v2c:function(){gx.fn.setComboBoxValue("vUSERFEE",gx.O.AV21userFee);typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV21userFee=this.val())},val:function(){return gx.fn.getDecimalValue("vUSERFEE",",",".")},nac:gx.falseFn};this.declareDomainHdlr(32,function(){});n[33]={id:33,fld:"",grid:0};n[34]={id:34,fld:"",grid:0};n[35]={id:35,fld:"NEXT",grid:0,evt:"e12082_client"};n[36]={id:36,fld:"",grid:0};n[37]={id:37,fld:"",grid:0};n[38]={id:38,fld:"SENDCOINS",grid:0,evt:"e13082_client"};n[39]={id:39,fld:"",grid:0};n[40]={id:40,fld:"",grid:0};n[41]={id:41,fld:"CANCEL",grid:0,evt:"e14082_client"};this.AV18totalBalance=0;this.ZV18totalBalance=0;this.OV18totalBalance=0;this.AV24sendAllCoins=!1;this.ZV24sendAllCoins=!1;this.OV24sendAllCoins=!1;this.AV14sendCoins=0;this.ZV14sendCoins=0;this.OV14sendCoins=0;this.AV15sendTo="";this.ZV15sendTo="";this.OV15sendTo="";this.AV31description="";this.ZV31description="";this.OV31description="";this.AV21userFee=0;this.ZV21userFee=0;this.OV21userFee=0;this.AV18totalBalance=0;this.AV24sendAllCoins=!1;this.AV14sendCoins=0;this.AV15sendTo="";this.AV31description="";this.AV21userFee=0;this.AV7error="";this.AV23wallet={WalletName:"",WalletType:"",EncryptedSecret:"",ExtEncryptedSecret:"",NetworkType:"",WalletFileName:"",WalletBaseDirectory:"",WalletReadBalanceOnStart:!1};this.AV20transactionsToSend=[];this.AV13changeTo="";this.AV25PopupName="";this.Events={e12082_client:["'NEXT'",!0],e13082_client:["'SEND COINS'",!0],e14082_client:["'CANCEL'",!0],e15082_client:["GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",!0],e18082_client:["ENTER",!0],e19082_client:["CANCEL",!0],e17081_client:["VSENDALLCOINS.CONTROLVALUECHANGED",!1]};this.EvtParms.REFRESH=[[{av:"AV24sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV23wallet",fld:"vWALLET",hsh:!0},{av:"AV18totalBalance",fld:"vTOTALBALANCE",pic:"ZZZZZZ9.99999999",hsh:!0}],[]];this.EvtParms["'NEXT'"]=[[{av:"AV14sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"AV18totalBalance",fld:"vTOTALBALANCE",pic:"ZZZZZZ9.99999999",hsh:!0},{av:"AV24sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV7error",fld:"vERROR"},{av:"AV15sendTo",fld:"vSENDTO"},{av:"AV23wallet",fld:"vWALLET",hsh:!0}],[{av:"AV7error",fld:"vERROR"}]];this.EvtParms["'SEND COINS'"]=[[{ctrl:"vUSERFEE"},{av:"AV21userFee",fld:"vUSERFEE",pic:"ZZZZZZ9.99999999"},{av:"AV24sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV23wallet",fld:"vWALLET",hsh:!0},{av:"AV20transactionsToSend",fld:"vTRANSACTIONSTOSEND"},{av:"AV14sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"AV15sendTo",fld:"vSENDTO"},{av:"AV13changeTo",fld:"vCHANGETO"},{av:"AV7error",fld:"vERROR"}],[{av:"AV7error",fld:"vERROR"},{ctrl:"SENDCOINS",prop:"Visible"},{ctrl:"vUSERFEE"},{av:"AV20transactionsToSend",fld:"vTRANSACTIONSTOSEND"}]];this.EvtParms["'CANCEL'"]=[[],[]];this.EvtParms["VSENDALLCOINS.CONTROLVALUECHANGED"]=[[{av:"AV24sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV18totalBalance",fld:"vTOTALBALANCE",pic:"ZZZZZZ9.99999999",hsh:!0}],[{av:"AV14sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"gx.fn.getCtrlProperty('vSENDCOINS','Enabled')",ctrl:"vSENDCOINS",prop:"Enabled"}]];this.EvtParms["GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED"]=[[{av:"AV25PopupName",fld:"vPOPUPNAME"},{av:"AV31description",fld:"vDESCRIPTION"},{av:"AV14sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"AV24sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV23wallet",fld:"vWALLET",hsh:!0},{av:"AV15sendTo",fld:"vSENDTO"},{ctrl:"vUSERFEE"},{av:"AV21userFee",fld:"vUSERFEE",pic:"ZZZZZZ9.99999999"}],[{av:"gx.fn.getCtrlProperty('vSENDCOINS','Enabled')",ctrl:"vSENDCOINS",prop:"Enabled"},{av:"gx.fn.getCtrlProperty('vSENDTO','Enabled')",ctrl:"vSENDTO",prop:"Enabled"},{av:"gx.fn.getCtrlProperty('vDESCRIPTION','Enabled')",ctrl:"vDESCRIPTION",prop:"Enabled"},{ctrl:"NEXT",prop:"Visible"},{ctrl:"SENDCOINS",prop:"Visible"},{av:"AV20transactionsToSend",fld:"vTRANSACTIONSTOSEND"},{av:"AV7error",fld:"vERROR"},{av:"AV13changeTo",fld:"vCHANGETO"},{ctrl:"vUSERFEE"},{av:"AV21userFee",fld:"vUSERFEE",pic:"ZZZZZZ9.99999999"}]];this.addExoEventHandler("extensions.web.popup.onpopupclosed",this.e15082_client);this.EvtParms.ENTER=[[],[]];this.setVCMap("AV7error","vERROR",0,"char",250,0);this.setVCMap("AV23wallet","vWALLET",0,"WalletWallet",0,0);this.setVCMap("AV20transactionsToSend","vTRANSACTIONSTOSEND",0,"CollWalletSDTAddressHistory",0,0);this.setVCMap("AV13changeTo","vCHANGETO",0,"char",250,0);this.setVCMap("AV25PopupName","vPOPUPNAME",0,"char",100,0);this.Initialize()});gx.wi(function(){gx.createParentObj(this.wallet.sendcoins)})