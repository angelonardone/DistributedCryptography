gx.evt.autoSkip=!1;gx.define("wallet.registered.chat",!1,function(){var t,n;this.ServerClass="wallet.registered.chat";this.PackageName="GeneXus.Programs";this.ServerFullClass="wallet.registered.chat.aspx";this.setObjectType("web");this.hasEnterEvent=!1;this.skipOnEnter=!1;this.autoRefresh=!0;this.fullAjax=!0;this.supportAjaxEvents=!0;this.ajaxSecurityToken=!0;this.DSO="Wallet\registeredChatbotDesignSystem";this.SetStandaloneVars=function(){this.AV12contact=gx.fn.getControlValue("vCONTACT");this.AV16externalUser=gx.fn.getControlValue("vEXTERNALUSER");this.AV14sdt_message=gx.fn.getControlValue("vSDT_MESSAGE");this.AV18json_enc=gx.fn.getControlValue("vJSON_ENC");this.AV21wallet=gx.fn.getControlValue("vWALLET");this.AV10receive_msgs=gx.fn.getControlValue("vRECEIVE_MSGS")};this.e11182_client=function(){return this.executeServerEvent("GLOBALEVENTS.RECEIVECHAT",!0,null,!0,!0)};this.e12182_client=function(){return this.executeServerEvent("'SEND'",!0,null,!1,!1)};this.e15182_client=function(){return this.executeServerEvent("ENTER",!0,arguments[0],!1,!1)};this.e16182_client=function(){return this.executeServerEvent("CANCEL",!0,arguments[0],!1,!1)};this.GXValidFnc=[];t=this.GXValidFnc;this.GXCtrlIds=[2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20,21,22,25,26,27,28,29,32,33,35];this.GXLastCtrlId=35;this.GridchatContainer=new gx.grid.grid(this,2,"WbpLvl2",14,"Gridchat","Gridchat","GridchatContainer",this.CmpContext,this.IsMasterPage,"wallet.registered.chat",[],!0,1,!1,!0,0,!1,!1,!1,"CollNostrSDT_receive_msg",0,"px",0,"px","New row",!1,!1,!1,null,null,!1,"AV10receive_msgs",!0,[1,1,1,1],!1,0,!1,!1);n=this.GridchatContainer;n.startDiv(15,"Gridmessagestable","0px","0px");n.startDiv(16,"","0px","0px");n.startDiv(17,"","0px","0px");n.startDiv(18,"","0px","0px");n.addLabel();n.addSingleLineEdit("GXV3",19,"CTLDATETIME","","","dateTime","dtime",17,"chr",17,17,"end",null,[],"GXV3","GXV3",!0,5,!1,!1,"gxchatgrid-message__date",0,"");n.endDiv();n.endDiv();n.endDiv();n.startDiv(20,"","0px","0px");n.startDiv(21,"","0px","0px");n.startTable("Tablemessage",22,"0px");n.startRow("","","","","","");n.startCell("","","","","","","","","","");n.startDiv(25,"","0px","0px");n.addLabel();n.addMultipleLineEdit("GXV4",26,"CTLMSESSAGEENC","","msessageEnc","vchar",80,"chr",10,"row","2097152",2097152,"start",null,!0,!1,0,"");n.endDiv();n.endCell();n.endRow();n.endTable();n.endDiv();n.endDiv();n.endDiv();this.GridchatContainer.emptyText="";this.setGrid(n);t[2]={id:2,fld:"",grid:0};t[3]={id:3,fld:"MAINTABLE",grid:0};t[4]={id:4,fld:"",grid:0};t[5]={id:5,fld:"",grid:0};t[6]={id:6,fld:"CHATTABLE",grid:0};t[7]={id:7,fld:"",grid:0};t[8]={id:8,fld:"",grid:0};t[9]={id:9,fld:"",grid:0};t[10]={id:10,fld:"",grid:0};t[11]={id:11,lvl:0,type:"char",len:250,dec:0,sign:!1,ro:0,multiline:!0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLUSERPRIVATENAME",fmt:0,gxz:"ZV22GXV1",gxold:"OV22GXV1",gxvar:"GXV1",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.GXV1=n)},v2z:function(n){n!==undefined&&(gx.O.ZV22GXV1=n)},v2c:function(){gx.fn.setControlValue("CTLUSERPRIVATENAME",gx.O.GXV1,0)},c2v:function(){this.val()!==undefined&&(gx.O.GXV1=this.val())},val:function(){return gx.fn.getControlValue("CTLUSERPRIVATENAME")},nac:gx.falseFn};t[12]={id:12,fld:"",grid:0};t[13]={id:13,fld:"",grid:0};t[15]={id:15,fld:"GRIDMESSAGESTABLE",grid:14};t[16]={id:16,fld:"",grid:14};t[17]={id:17,fld:"",grid:14};t[18]={id:18,fld:"",grid:14};t[19]={id:19,lvl:2,type:"dtime",len:8,dec:5,sign:!1,ro:1,isacc:0,grid:14,gxgrid:this.GridchatContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLDATETIME",fmt:0,gxz:"ZV24GXV3",gxold:"OV24GXV3",gxvar:"GXV3",dp:{f:0,st:!0,wn:!1,mf:!1,pic:"99/99/99 99:99",dec:5},ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",v2v:function(n){n!==undefined&&(gx.O.GXV3=gx.fn.toDatetimeValue(n,"Y4MD"))},v2z:function(n){n!==undefined&&(gx.O.ZV24GXV3=gx.fn.toDatetimeValue(n))},v2c:function(n){gx.fn.setGridControlValue("CTLDATETIME",n||gx.fn.currentGridRowImpl(14),gx.O.GXV3,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV3=gx.fn.toDatetimeValue(this.val(n)))},val:function(n){return gx.fn.getGridDateTimeValue("CTLDATETIME",n||gx.fn.currentGridRowImpl(14))},nac:gx.falseFn};t[20]={id:20,fld:"",grid:14};t[21]={id:21,fld:"",grid:14};t[22]={id:22,fld:"TABLEMESSAGE",grid:14};t[25]={id:25,fld:"",grid:14};t[26]={id:26,lvl:2,type:"vchar",len:2097152,dec:0,sign:!1,ro:1,isacc:0,multiline:!0,grid:14,gxgrid:this.GridchatContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLMSESSAGEENC",fmt:0,gxz:"ZV25GXV4",gxold:"OV25GXV4",gxvar:"GXV4",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",autoCorrect:"1",v2v:function(n){n!==undefined&&(gx.O.GXV4=n)},v2z:function(n){n!==undefined&&(gx.O.ZV25GXV4=n)},v2c:function(n){gx.fn.setGridControlValue("CTLMSESSAGEENC",n||gx.fn.currentGridRowImpl(14),gx.O.GXV4,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV4=this.val(n))},val:function(n){return gx.fn.getGridControlValue("CTLMSESSAGEENC",n||gx.fn.currentGridRowImpl(14))},nac:gx.falseFn};t[27]={id:27,fld:"",grid:0};t[28]={id:28,fld:"",grid:0};t[29]={id:29,fld:"TABLE1",grid:0};t[32]={id:32,fld:"",grid:0};t[33]={id:33,lvl:0,type:"vchar",len:2097152,dec:0,sign:!1,ro:0,multiline:!0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vSENDCLEARTEXT",fmt:0,gxz:"ZV11sendClearText",gxold:"OV11sendClearText",gxvar:"AV11sendClearText",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV11sendClearText=n)},v2z:function(n){n!==undefined&&(gx.O.ZV11sendClearText=n)},v2c:function(){gx.fn.setControlValue("vSENDCLEARTEXT",gx.O.AV11sendClearText,0)},c2v:function(){this.val()!==undefined&&(gx.O.AV11sendClearText=this.val())},val:function(){return gx.fn.getControlValue("vSENDCLEARTEXT")},nac:gx.falseFn};t[35]={id:35,fld:"CHATSENDIMAGE",grid:0,evt:"e12182_client"};this.GXV1="";this.ZV22GXV1="";this.OV22GXV1="";this.ZV24GXV3=gx.date.nullDate();this.OV24GXV3=gx.date.nullDate();this.ZV25GXV4="";this.OV25GXV4="";this.AV11sendClearText="";this.ZV11sendClearText="";this.OV11sendClearText="";this.GXV1="";this.AV11sendClearText="";this.GXV3=gx.date.nullDate();this.GXV4="";this.AV10receive_msgs=[];this.AV12contact={contactrId:"00000000-0000-0000-0000-000000000000",userName:"",userPubKey:"",messagePubKey:"",groupPubKey:"",userPrivateName:"",invitationSent:gx.date.nullDate(),invitacionAccepted:gx.date.nullDate(),invitationDeclined:!1,sent:!1,received:!1,edited:!1};this.AV16externalUser={ExternalToken:"",UserInfo:{guid:"",username:"",email:"",verified_email:!1,first_name:"",last_name:"",external_id:"",birthday:gx.date.nullDate(),gender:"",url_image:"",url_profile:"",phone:"",address:"",city:"",state:"",post_code:"",language:"",timezone:"",attributes:[],roles:[]},KeyInfo:{PrivateKey:"",PublicKey:"",TaprootPubKey:"",PublicKeyHash:"",ScriptPublicKey:"",Address:"",WIF:"",encryptedWIF:""},ChatKeyInfo:{PrivateKey:"",PublicKey:"",TaprootPubKey:"",PublicKeyHash:"",ScriptPublicKey:"",Address:"",WIF:"",encryptedWIF:""},GroupsKeyInfo:{PrivateKey:"",PublicKey:"",TaprootPubKey:"",PublicKeyHash:"",ScriptPublicKey:"",Address:"",WIF:"",encryptedWIF:""}};this.AV14sdt_message={Id:"00000000-0000-0000-0000-000000000000",MessageType:0,dateTimeUnix:0,Message:""};this.AV18json_enc={encryptedKey:"",IV:"",encryptedText:""};this.AV21wallet={WalletName:"",WalletType:"",EncryptedSecret:"",ExtEncryptedSecret:"",NetworkType:"",WalletFileName:"",WalletBaseDirectory:"",WalletReadBalanceOnStart:!1};this.Events={e11182_client:["GLOBALEVENTS.RECEIVECHAT",!0],e12182_client:["'SEND'",!0],e15182_client:["ENTER",!0],e16182_client:["CANCEL",!0]};this.EvtParms.REFRESH=[[{av:"GRIDCHAT_nFirstRecordOnPage"},{av:"GRIDCHAT_nEOF"},{av:"AV10receive_msgs",fld:"vRECEIVE_MSGS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"nRC_GXsfl_14",ctrl:"GRIDCHAT",prop:"GridRC",grid:14},{av:"AV16externalUser",fld:"vEXTERNALUSER",hsh:!0},{av:"AV21wallet",fld:"vWALLET",hsh:!0},{av:"GXV1",fld:"CTLUSERPRIVATENAME"}],[]];this.EvtParms["GLOBALEVENTS.RECEIVECHAT"]=[[{av:"AV12contact",fld:"vCONTACT"},{av:"AV10receive_msgs",fld:"vRECEIVE_MSGS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCHAT_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCHAT",prop:"GridRC",grid:14},{av:"GRIDCHAT_nEOF"},{av:"AV16externalUser",fld:"vEXTERNALUSER",hsh:!0},{av:"AV21wallet",fld:"vWALLET",hsh:!0},{av:"GXV1",fld:"CTLUSERPRIVATENAME"}],[{av:"AV10receive_msgs",fld:"vRECEIVE_MSGS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCHAT_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCHAT",prop:"GridRC",grid:14}]];this.addExoEventHandler("ReceiveChat",this.e11182_client);this.EvtParms["'SEND'"]=[[{av:"AV11sendClearText",fld:"vSENDCLEARTEXT"},{av:"AV16externalUser",fld:"vEXTERNALUSER",hsh:!0},{av:"AV12contact",fld:"vCONTACT"},{av:"AV14sdt_message",fld:"vSDT_MESSAGE"},{av:"AV18json_enc",fld:"vJSON_ENC"},{av:"AV21wallet",fld:"vWALLET",hsh:!0},{av:"AV10receive_msgs",fld:"vRECEIVE_MSGS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCHAT_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCHAT",prop:"GridRC",grid:14},{av:"GRIDCHAT_nEOF"},{av:"GXV1",fld:"CTLUSERPRIVATENAME"}],[{av:"AV14sdt_message",fld:"vSDT_MESSAGE"},{av:"AV18json_enc",fld:"vJSON_ENC"},{av:"AV11sendClearText",fld:"vSENDCLEARTEXT"},{av:"AV10receive_msgs",fld:"vRECEIVE_MSGS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCHAT_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCHAT",prop:"GridRC",grid:14}]];this.EvtParms["GRIDCHAT.LOAD"]=[[{av:"AV10receive_msgs",fld:"vRECEIVE_MSGS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCHAT_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCHAT",prop:"GridRC",grid:14},{av:"AV16externalUser",fld:"vEXTERNALUSER",hsh:!0}],[{av:"gx.fn.getCtrlProperty('GRIDMESSAGESTABLE','Class')",ctrl:"GRIDMESSAGESTABLE",prop:"Class"},{av:"gx.fn.getCtrlProperty('TABLEMESSAGE','Class')",ctrl:"TABLEMESSAGE",prop:"Class"}]];this.EvtParms.ENTER=[[],[]];this.setVCMap("AV12contact","vCONTACT",0,"Wallet\registeredContact_SDT",0,0);this.setVCMap("AV16externalUser","vEXTERNALUSER",0,"DistCryptExternalUser",0,0);this.setVCMap("AV14sdt_message","vSDT_MESSAGE",0,"NostrSDT_message",0,0);this.setVCMap("AV18json_enc","vJSON_ENC",0,"WalletSDT_Json_Enc",0,0);this.setVCMap("AV21wallet","vWALLET",0,"WalletWallet",0,0);this.setVCMap("AV10receive_msgs","vRECEIVE_MSGS",0,"CollNostrSDT_receive_msg",0,0);this.setVCMap("AV10receive_msgs","vRECEIVE_MSGS",0,"CollNostrSDT_receive_msg",0,0);this.setVCMap("AV16externalUser","vEXTERNALUSER",0,"DistCryptExternalUser",0,0);this.setVCMap("AV10receive_msgs","vRECEIVE_MSGS",0,"CollNostrSDT_receive_msg",0,0);this.setVCMap("AV16externalUser","vEXTERNALUSER",0,"DistCryptExternalUser",0,0);n.addRefreshingVar({rfrVar:"AV10receive_msgs"});n.addRefreshingVar({rfrVar:"AV16externalUser"});n.addRefreshingVar({rfrVar:"AV21wallet"});n.addRefreshingVar(this.GXValidFnc[11]);n.addRefreshingParm({rfrVar:"AV10receive_msgs"});n.addRefreshingParm({rfrVar:"AV16externalUser"});n.addRefreshingParm({rfrVar:"AV21wallet"});n.addRefreshingParm(this.GXValidFnc[11]);this.addBCProperty("Contact",["userPrivateName"],this.GXValidFnc[11],"AV12contact");this.addGridBCProperty("Receive_msgs",["dateTime"],this.GXValidFnc[19],"AV10receive_msgs");this.addGridBCProperty("Receive_msgs",["msessageEnc"],this.GXValidFnc[26],"AV10receive_msgs");this.Initialize()});gx.wi(function(){gx.createParentObj(this.wallet.registered.chat)})