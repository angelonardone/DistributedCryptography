gx.evt.autoSkip=!1;gx.define("wallet.enterpassword",!1,function(){this.ServerClass="wallet.enterpassword";this.PackageName="GeneXus.Programs";this.ServerFullClass="wallet.enterpassword.aspx";this.setObjectType("web");this.hasEnterEvent=!0;this.skipOnEnter=!1;this.autoRefresh=!0;this.fullAjax=!0;this.supportAjaxEvents=!0;this.ajaxSecurityToken=!0;this.DSO="GeneXusUnanimoUnanimoWeb";this.SetStandaloneVars=function(){this.AV8wallet=gx.fn.getControlValue("vWALLET");this.AV6walletName=gx.fn.getControlValue("vWALLETNAME")};this.e120j2_client=function(){return this.executeServerEvent("ENTER",!0,null,!1,!1)};this.e140j2_client=function(){return this.executeServerEvent("CANCEL",!0,null,!1,!1)};this.GXValidFnc=[];var n=this.GXValidFnc;this.GXCtrlIds=[2,3,4,5,6,7,8,9,10,11];this.GXLastCtrlId=11;n[2]={id:2,fld:"",grid:0};n[3]={id:3,fld:"MAINTABLE",grid:0};n[4]={id:4,fld:"",grid:0};n[5]={id:5,fld:"",grid:0};n[6]={id:6,fld:"",grid:0};n[7]={id:7,fld:"",grid:0};n[8]={id:8,lvl:0,type:"char",len:100,dec:0,sign:!1,isPwd:!0,ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vPASSWORD",fmt:0,gxz:"ZV5password",gxold:"OV5password",gxvar:"AV5password",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV5password=n)},v2z:function(n){n!==undefined&&(gx.O.ZV5password=n)},v2c:function(){gx.fn.setControlValue("vPASSWORD",gx.O.AV5password,0)},c2v:function(){this.val()!==undefined&&(gx.O.AV5password=this.val())},val:function(){return gx.fn.getControlValue("vPASSWORD")},nac:gx.falseFn};n[9]={id:9,fld:"",grid:0};n[10]={id:10,fld:"",grid:0};n[11]={id:11,fld:"ENTER",grid:0,evt:"e120j2_client",std:"ENTER"};this.AV5password="";this.ZV5password="";this.OV5password="";this.AV5password="";this.AV6walletName="";this.AV8wallet={WalletName:"",WalletType:"",EncryptedSecret:"",ExtEncryptedSecret:"",NetworkType:"",WalletFileName:"",WalletBaseDirectory:"",WalletReadBalanceOnStart:!1};this.Events={e120j2_client:["ENTER",!0],e140j2_client:["CANCEL",!0]};this.EvtParms.REFRESH=[[{av:"AV8wallet",fld:"vWALLET",hsh:!0}],[]];this.EvtParms.ENTER=[[{av:"AV5password",fld:"vPASSWORD"},{av:"AV8wallet",fld:"vWALLET",hsh:!0}],[]];this.EnterCtrl=["ENTER"];this.setVCMap("AV8wallet","vWALLET",0,"WalletWallet",0,0);this.setVCMap("AV6walletName","vWALLETNAME",0,"char",200,0);this.Initialize()});gx.wi(function(){gx.createParentObj(this.wallet.enterpassword)})