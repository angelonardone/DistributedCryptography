gx.evt.autoSkip=!1;gx.define("wallet.registered.sendcoinsmusig",!1,function(){this.ServerClass="wallet.registered.sendcoinsmusig";this.PackageName="GeneXus.Programs";this.ServerFullClass="wallet.registered.sendcoinsmusig.aspx";this.setObjectType("web");this.hasEnterEvent=!1;this.skipOnEnter=!1;this.autoRefresh=!0;this.fullAjax=!0;this.supportAjaxEvents=!0;this.ajaxSecurityToken=!0;this.DSO="GeneXusUnanimoUnanimoWeb";this.SetStandaloneVars=function(){this.AV10error=gx.fn.getControlValue("vERROR");this.AV28wallet=gx.fn.getControlValue("vWALLET");this.AV31group_sdt=gx.fn.getControlValue("vGROUP_SDT");this.AV24transactionFee=gx.fn.getDecimalValue("vTRANSACTIONFEE",",",".");this.AV25transactionsToSend=gx.fn.getControlValue("vTRANSACTIONSTOSEND");this.AV7changeTo=gx.fn.getControlValue("vCHANGETO");this.AV35oneMuSigSignatures=gx.fn.getControlValue("vONEMUSIGSIGNATURES");this.AV16PopupName=gx.fn.getControlValue("vPOPUPNAME")};this.e171w1_client=function(){return this.clearMessages(),this.AV17sendAllCoins?(this.AV18sendCoins=this.AV23totalBalance,gx.fn.setCtrlProperty("vSENDCOINS","Enabled",!1)):(this.AV18sendCoins=0,gx.fn.setCtrlProperty("vSENDCOINS","Enabled",!0)),this.refreshOutputs([{av:"AV18sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"gx.fn.getCtrlProperty('vSENDCOINS','Enabled')",ctrl:"vSENDCOINS",prop:"Enabled"}]),this.OnClientEventEnd(),gx.$.Deferred().resolve()};this.e121w2_client=function(){return this.executeServerEvent("'NEXT'",!1,null,!1,!1)};this.e131w2_client=function(){return this.executeServerEvent("'SEND COINS'",!1,null,!1,!1)};this.e141w2_client=function(){return this.executeServerEvent("'CANCEL'",!1,null,!1,!1)};this.e151w2_client=function(){return this.setEventParameters([["PopupName","vPOPUPNAME","AV16PopupName"]],arguments[2]),this.executeServerEvent("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",!0,null,!0,!0)};this.e181w2_client=function(){return this.executeServerEvent("ENTER",!0,null,!1,!1)};this.e191w2_client=function(){return this.executeServerEvent("CANCEL",!0,null,!1,!1)};this.GXValidFnc=[];var n=this.GXValidFnc;this.GXCtrlIds=[2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41];this.GXLastCtrlId=41;n[2]={id:2,fld:"",grid:0};n[3]={id:3,fld:"MAINTABLE",grid:0};n[4]={id:4,fld:"",grid:0};n[5]={id:5,fld:"",grid:0};n[6]={id:6,fld:"",grid:0};n[7]={id:7,fld:"",grid:0};n[8]={id:8,lvl:0,type:"decimal",len:16,dec:8,sign:!1,pic:"ZZZZZZ9.99999999",ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vTOTALBALANCE",fmt:0,gxz:"ZV23totalBalance",gxold:"OV23totalBalance",gxvar:"AV23totalBalance",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV23totalBalance=gx.fn.toDecimalValue(n,",","."))},v2z:function(n){n!==undefined&&(gx.O.ZV23totalBalance=gx.fn.toDecimalValue(n,",","."))},v2c:function(){gx.fn.setDecimalValue("vTOTALBALANCE",gx.O.AV23totalBalance,8,".");typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV23totalBalance=this.val())},val:function(){return gx.fn.getDecimalValue("vTOTALBALANCE",",",".")},nac:gx.falseFn};this.declareDomainHdlr(8,function(){});n[9]={id:9,fld:"",grid:0};n[10]={id:10,fld:"",grid:0};n[11]={id:11,fld:"",grid:0};n[12]={id:12,lvl:0,type:"boolean",len:4,dec:0,sign:!1,ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:"e171w1_client",evt_cvcing:null,rgrid:[],fld:"vSENDALLCOINS",fmt:0,gxz:"ZV17sendAllCoins",gxold:"OV17sendAllCoins",gxvar:"AV17sendAllCoins",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"checkbox",v2v:function(n){n!==undefined&&(gx.O.AV17sendAllCoins=gx.lang.booleanValue(n))},v2z:function(n){n!==undefined&&(gx.O.ZV17sendAllCoins=gx.lang.booleanValue(n))},v2c:function(){gx.fn.setCheckBoxValue("vSENDALLCOINS",gx.O.AV17sendAllCoins,!0)},c2v:function(){this.val()!==undefined&&(gx.O.AV17sendAllCoins=gx.lang.booleanValue(this.val()))},val:function(){return gx.fn.getControlValue("vSENDALLCOINS")},nac:gx.falseFn,values:["true","false"]};n[13]={id:13,fld:"",grid:0};n[14]={id:14,fld:"",grid:0};n[15]={id:15,fld:"",grid:0};n[16]={id:16,fld:"",grid:0};n[17]={id:17,lvl:0,type:"decimal",len:16,dec:8,sign:!1,pic:"ZZZZZZ9.99999999",ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vSENDCOINS",fmt:0,gxz:"ZV18sendCoins",gxold:"OV18sendCoins",gxvar:"AV18sendCoins",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV18sendCoins=gx.fn.toDecimalValue(n,",","."))},v2z:function(n){n!==undefined&&(gx.O.ZV18sendCoins=gx.fn.toDecimalValue(n,",","."))},v2c:function(){gx.fn.setDecimalValue("vSENDCOINS",gx.O.AV18sendCoins,8,".");typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV18sendCoins=this.val())},val:function(){return gx.fn.getDecimalValue("vSENDCOINS",",",".")},nac:gx.falseFn};this.declareDomainHdlr(17,function(){});n[18]={id:18,fld:"",grid:0};n[19]={id:19,fld:"",grid:0};n[20]={id:20,fld:"",grid:0};n[21]={id:21,fld:"",grid:0};n[22]={id:22,lvl:0,type:"char",len:250,dec:0,sign:!1,ro:0,multiline:!0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vSENDTO",fmt:0,gxz:"ZV19sendTo",gxold:"OV19sendTo",gxvar:"AV19sendTo",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV19sendTo=n)},v2z:function(n){n!==undefined&&(gx.O.ZV19sendTo=n)},v2c:function(){gx.fn.setControlValue("vSENDTO",gx.O.AV19sendTo,0);typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV19sendTo=this.val())},val:function(){return gx.fn.getControlValue("vSENDTO")},nac:gx.falseFn};this.declareDomainHdlr(22,function(){});n[23]={id:23,fld:"",grid:0};n[24]={id:24,fld:"",grid:0};n[25]={id:25,fld:"",grid:0};n[26]={id:26,fld:"",grid:0};n[27]={id:27,lvl:0,type:"svchar",len:250,dec:0,sign:!1,ro:0,multiline:!0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vDESCRIPTION",fmt:0,gxz:"ZV33description",gxold:"OV33description",gxvar:"AV33description",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV33description=n)},v2z:function(n){n!==undefined&&(gx.O.ZV33description=n)},v2c:function(){gx.fn.setControlValue("vDESCRIPTION",gx.O.AV33description,0)},c2v:function(){this.val()!==undefined&&(gx.O.AV33description=this.val())},val:function(){return gx.fn.getControlValue("vDESCRIPTION")},nac:gx.falseFn};n[28]={id:28,fld:"",grid:0};n[29]={id:29,fld:"",grid:0};n[30]={id:30,fld:"",grid:0};n[31]={id:31,fld:"",grid:0};n[32]={id:32,lvl:0,type:"decimal",len:16,dec:8,sign:!1,pic:"ZZZZZZ9.99999999",ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vUSERFEE",fmt:0,gxz:"ZV26userFee",gxold:"OV26userFee",gxvar:"AV26userFee",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"combo",v2v:function(n){n!==undefined&&(gx.O.AV26userFee=gx.fn.toDecimalValue(n,",","."))},v2z:function(n){n!==undefined&&(gx.O.ZV26userFee=gx.fn.toDecimalValue(n,",","."))},v2c:function(){gx.fn.setComboBoxValue("vUSERFEE",gx.O.AV26userFee);typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV26userFee=this.val())},val:function(){return gx.fn.getDecimalValue("vUSERFEE",",",".")},nac:gx.falseFn};this.declareDomainHdlr(32,function(){});n[33]={id:33,fld:"",grid:0};n[34]={id:34,fld:"",grid:0};n[35]={id:35,fld:"NEXT",grid:0,evt:"e121w2_client"};n[36]={id:36,fld:"",grid:0};n[37]={id:37,fld:"",grid:0};n[38]={id:38,fld:"SENDCOINS",grid:0,evt:"e131w2_client"};n[39]={id:39,fld:"",grid:0};n[40]={id:40,fld:"",grid:0};n[41]={id:41,fld:"CANCEL",grid:0,evt:"e141w2_client"};this.AV23totalBalance=0;this.ZV23totalBalance=0;this.OV23totalBalance=0;this.AV17sendAllCoins=!1;this.ZV17sendAllCoins=!1;this.OV17sendAllCoins=!1;this.AV18sendCoins=0;this.ZV18sendCoins=0;this.OV18sendCoins=0;this.AV19sendTo="";this.ZV19sendTo="";this.OV19sendTo="";this.AV33description="";this.ZV33description="";this.OV33description="";this.AV26userFee=0;this.ZV26userFee=0;this.OV26userFee=0;this.AV23totalBalance=0;this.AV17sendAllCoins=!1;this.AV18sendCoins=0;this.AV19sendTo="";this.AV33description="";this.AV26userFee=0;this.AV10error="";this.AV28wallet={WalletName:"",WalletType:"",EncryptedSecret:"",ExtEncryptedSecret:"",NetworkType:"",WalletFileName:"",WalletBaseDirectory:"",WalletReadBalanceOnStart:!1};this.AV31group_sdt={groupId:"00000000-0000-0000-0000-000000000000",groupType:0,groupName:"",amIgroupOwner:!1,isActive:!1,minimumShares:0,encPassword:"",clearTextShare:"",numOfSharesReached:!1,extPubKeyMultiSigReceiving:"",extPubKeyMultiSigChange:"",Contact:[],otherGroup:{referenceGroupId:"00000000-0000-0000-0000-000000000000",invitationDeclined:!1,encPassword:"",referenceUsernName:"",signature:"",extPubKeyMultiSigReceiving:"",extPubKeyMultiSigChange:""}};this.AV24transactionFee=0;this.AV25transactionsToSend=[];this.AV7changeTo="";this.AV35oneMuSigSignatures={id:"00000000-0000-0000-0000-000000000000",description:"",signedDateTime:gx.date.nullDate(),compleated:!1,sendAllCoins:!1,sendCoins:0,sendTo:"",changeTo:"",referenceGroupId:"00000000-0000-0000-0000-000000000000",senderUserName:"",senderSignature:"",transactions:[]};this.AV16PopupName="";this.Events={e121w2_client:["'NEXT'",!0],e131w2_client:["'SEND COINS'",!0],e141w2_client:["'CANCEL'",!0],e151w2_client:["GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",!0],e181w2_client:["ENTER",!0],e191w2_client:["CANCEL",!0],e171w1_client:["VSENDALLCOINS.CONTROLVALUECHANGED",!1]};this.EvtParms.REFRESH=[[{av:"AV17sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV28wallet",fld:"vWALLET",hsh:!0},{av:"AV31group_sdt",fld:"vGROUP_SDT",hsh:!0},{av:"AV35oneMuSigSignatures",fld:"vONEMUSIGSIGNATURES",hsh:!0},{av:"AV23totalBalance",fld:"vTOTALBALANCE",pic:"ZZZZZZ9.99999999",hsh:!0}],[]];this.EvtParms["'NEXT'"]=[[{av:"AV18sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"AV23totalBalance",fld:"vTOTALBALANCE",pic:"ZZZZZZ9.99999999",hsh:!0},{av:"AV17sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV10error",fld:"vERROR"},{av:"AV19sendTo",fld:"vSENDTO"},{av:"AV28wallet",fld:"vWALLET",hsh:!0},{av:"AV31group_sdt",fld:"vGROUP_SDT",hsh:!0},{av:"AV33description",fld:"vDESCRIPTION"}],[{av:"AV10error",fld:"vERROR"}]];this.EvtParms["'SEND COINS'"]=[[{ctrl:"vUSERFEE"},{av:"AV26userFee",fld:"vUSERFEE",pic:"ZZZZZZ9.99999999"},{av:"AV31group_sdt",fld:"vGROUP_SDT",hsh:!0},{av:"AV17sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV24transactionFee",fld:"vTRANSACTIONFEE",pic:"ZZZZZZ9.99999999"},{av:"AV28wallet",fld:"vWALLET",hsh:!0},{av:"AV25transactionsToSend",fld:"vTRANSACTIONSTOSEND"},{av:"AV18sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"AV19sendTo",fld:"vSENDTO"},{av:"AV7changeTo",fld:"vCHANGETO"},{av:"AV35oneMuSigSignatures",fld:"vONEMUSIGSIGNATURES",hsh:!0},{av:"AV10error",fld:"vERROR"}],[{av:"AV10error",fld:"vERROR"},{av:"AV25transactionsToSend",fld:"vTRANSACTIONSTOSEND"},{ctrl:"SENDCOINS",prop:"Visible"},{ctrl:"vUSERFEE"}]];this.EvtParms["'CANCEL'"]=[[],[]];this.EvtParms["VSENDALLCOINS.CONTROLVALUECHANGED"]=[[{av:"AV17sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV23totalBalance",fld:"vTOTALBALANCE",pic:"ZZZZZZ9.99999999",hsh:!0}],[{av:"AV18sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"gx.fn.getCtrlProperty('vSENDCOINS','Enabled')",ctrl:"vSENDCOINS",prop:"Enabled"}]];this.EvtParms["GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED"]=[[{av:"AV16PopupName",fld:"vPOPUPNAME"},{av:"AV35oneMuSigSignatures",fld:"vONEMUSIGSIGNATURES",hsh:!0},{av:"AV33description",fld:"vDESCRIPTION"},{av:"AV18sendCoins",fld:"vSENDCOINS",pic:"ZZZZZZ9.99999999"},{av:"AV7changeTo",fld:"vCHANGETO"},{av:"AV10error",fld:"vERROR"},{av:"AV31group_sdt",fld:"vGROUP_SDT",hsh:!0},{av:"AV17sendAllCoins",fld:"vSENDALLCOINS"},{av:"AV28wallet",fld:"vWALLET",hsh:!0},{av:"AV25transactionsToSend",fld:"vTRANSACTIONSTOSEND"},{av:"AV19sendTo",fld:"vSENDTO"},{ctrl:"vUSERFEE"},{av:"AV26userFee",fld:"vUSERFEE",pic:"ZZZZZZ9.99999999"}],[{av:"gx.fn.getCtrlProperty('vSENDCOINS','Enabled')",ctrl:"vSENDCOINS",prop:"Enabled"},{av:"gx.fn.getCtrlProperty('vSENDTO','Enabled')",ctrl:"vSENDTO",prop:"Enabled"},{av:"gx.fn.getCtrlProperty('vDESCRIPTION','Enabled')",ctrl:"vDESCRIPTION",prop:"Enabled"},{ctrl:"NEXT",prop:"Visible"},{ctrl:"SENDCOINS",prop:"Visible"},{av:"AV25transactionsToSend",fld:"vTRANSACTIONSTOSEND"},{av:"AV10error",fld:"vERROR"},{av:"AV7changeTo",fld:"vCHANGETO"},{av:"AV24transactionFee",fld:"vTRANSACTIONFEE",pic:"ZZZZZZ9.99999999"},{ctrl:"vUSERFEE"},{av:"AV26userFee",fld:"vUSERFEE",pic:"ZZZZZZ9.99999999"}]];this.addExoEventHandler("extensions.web.popup.onpopupclosed",this.e151w2_client);this.EvtParms.ENTER=[[],[]];this.setVCMap("AV10error","vERROR",0,"char",250,0);this.setVCMap("AV28wallet","vWALLET",0,"WalletWallet",0,0);this.setVCMap("AV31group_sdt","vGROUP_SDT",0,"Wallet\registeredGroup_SDT",0,0);this.setVCMap("AV24transactionFee","vTRANSACTIONFEE",0,"decimal",16,8);this.setVCMap("AV25transactionsToSend","vTRANSACTIONSTOSEND",0,"CollWalletSDTAddressHistory",0,0);this.setVCMap("AV7changeTo","vCHANGETO",0,"char",250,0);this.setVCMap("AV35oneMuSigSignatures","vONEMUSIGSIGNATURES",0,"Wallet\registeredMuSigSignatures",0,0);this.setVCMap("AV16PopupName","vPOPUPNAME",0,"char",100,0);this.Initialize()});gx.wi(function(){gx.createParentObj(this.wallet.registered.sendcoinsmusig)})