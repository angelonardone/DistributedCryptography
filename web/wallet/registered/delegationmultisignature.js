gx.evt.autoSkip=!1;gx.define("wallet.registered.delegationmultisignature",!0,function(n){var t,i;this.ServerClass="wallet.registered.delegationmultisignature";this.PackageName="GeneXus.Programs";this.ServerFullClass="wallet.registered.delegationmultisignature.aspx";this.setObjectType("web");this.setCmpContext(n);this.ReadonlyForm=!0;this.hasEnterEvent=!1;this.skipOnEnter=!1;this.autoRefresh=!0;this.fullAjax=!0;this.supportAjaxEvents=!0;this.ajaxSecurityToken=!0;this.DSO="GeneXusUnanimoUnanimoWeb";this.SetStandaloneVars=function(){this.AV13group_sdt=gx.fn.getControlValue("vGROUP_SDT");this.AV18groupContacts=gx.fn.getControlValue("vGROUPCONTACTS");this.AV32saveAndReturn=gx.fn.getControlValue("vSAVEANDRETURN");this.AV24PopupName=gx.fn.getControlValue("vPOPUPNAME");this.AV17groupContactAdd=gx.fn.getControlValue("vGROUPCONTACTADD");this.AV10error=gx.fn.getControlValue("vERROR");this.AV5externalUser=gx.fn.getControlValue("vEXTERNALUSER");this.AV22message_signature=gx.fn.getControlValue("vMESSAGE_SIGNATURE");this.AV26sdt_message=gx.fn.getControlValue("vSDT_MESSAGE");this.AV36extKeyInfoRoot=gx.fn.getControlValue("vEXTKEYINFOROOT");this.AV35wallet=gx.fn.getControlValue("vWALLET")};this.Validv_Gxv3=function(){var n=gx.fn.currentGridRowImpl(14);return this.validCliEvt("Validv_Gxv3",14,function(){try{var n=gx.util.balloon.getNew("CTLCONTACTID1");if(this.AnyError=0,!gx.util.regExp.isMatch(this.GXV3,"([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}|^\\s*$)"))try{n.setError("GXM_InvalidGUID");this.AnyError=gx.num.trunc(1,0)}catch(t){}}catch(t){}try{return n==null?!0:n.show()}catch(t){}return!0})};this.e111o1_client=function(){return this.clearMessages(),this.popupOpenUrl(gx.http.formatLink("wallet.registered.promptcontac.aspx",[]),[]),this.refreshOutputs([]),this.OnClientEventEnd(),gx.$.Deferred().resolve()};this.e121o2_client=function(){return this.executeServerEvent("'SAVE'",!1,null,!1,!1)};this.e131o2_client=function(){return this.executeServerEvent("'CANCEL EDIT'",!1,null,!1,!1)};this.e141o2_client=function(){return this.setEventParameters([["PopupName","vPOPUPNAME","AV24PopupName"]],arguments[2]),this.executeServerEvent("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",!0,null,!0,!0)};this.e191o2_client=function(){return this.executeServerEvent("'REMOVE CONTACT'",!0,arguments[0],!1,!1)};this.e151o2_client=function(){return this.executeServerEvent("'SEND INVITATION TO GROUP MEMBERS'",!1,null,!1,!1)};this.e161o2_client=function(){return this.executeServerEvent("'ACTIVATE GROUP'",!1,null,!1,!1)};this.e201o2_client=function(){return this.executeServerEvent("ENTER",!0,arguments[0],!1,!1)};this.e211o2_client=function(){return this.executeServerEvent("CANCEL",!0,arguments[0],!1,!1)};this.GXValidFnc=[];t=this.GXValidFnc;this.GXCtrlIds=[2,3,4,5,6,7,8,9,10,11,12,13,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33];this.GXLastCtrlId=33;this.GridcontactsContainer=new gx.grid.grid(this,2,"WbpLvl2",14,"Gridcontacts","Gridcontacts","GridcontactsContainer",this.CmpContext,this.IsMasterPage,"wallet.registered.delegationmultisignature",[],!1,1,!1,!0,0,!1,!1,!1,"CollWallet\registeredGroup_SDT.ContactItem",0,"px",0,"px","New row",!1,!1,!1,null,null,!1,"AV18groupContacts",!1,[1,1,1,1],!1,0,!0,!1);i=this.GridcontactsContainer;i.addSingleLineEdit("GXV3",15,"CTLCONTACTID1","contact Id","","contactId","guid",0,"px",36,36,"",null,[],"GXV3","GXV3",!1,0,!1,!0,"Attribute",0,"");i.addSingleLineEdit("GXV4",16,"CTLCONTACTPRIVATENAME","Contact  Name","","contactPrivateName","char",0,"px",250,80,"start",null,[],"GXV4","GXV4",!0,0,!1,!1,"Attribute",0,"");i.addSingleLineEdit("GXV5",17,"CTLCONTACTUSERNAME","Contact Name","","contactUserName","char",0,"px",250,80,"start",null,[],"GXV5","GXV5",!0,0,!1,!1,"Attribute",0,"");i.addSingleLineEdit("GXV6",18,"CTLCONTACTINVITATIONSENT","Invitation Sent","","contactInvitationSent","dtime",0,"px",17,17,"end",null,[],"GXV6","GXV6",!0,5,!1,!1,"Attribute",0,"");i.addSingleLineEdit("GXV7",19,"CTLCONTACTINVITACIONACCEPTED","Invitacion Accepted","","contactInvitacionAccepted","dtime",0,"px",17,17,"end",null,[],"GXV7","GXV7",!0,5,!1,!1,"Attribute",0,"");i.addSingleLineEdit("Removecontact",20,"vREMOVECONTACT","","","removeContact","char",0,"px",20,20,"start","e191o2_client",[],"Removecontact","removeContact",!0,0,!1,!1,"Attribute",0,"");this.GridcontactsContainer.emptyText="";this.setGrid(i);t[2]={id:2,fld:"",grid:0};t[3]={id:3,fld:"MAINTABLE",grid:0};t[4]={id:4,fld:"",grid:0};t[5]={id:5,fld:"",grid:0};t[6]={id:6,fld:"MSGTOOWNER",format:0,grid:0,ctrltype:"textblock"};t[7]={id:7,fld:"",grid:0};t[8]={id:8,fld:"",grid:0};t[9]={id:9,fld:"",grid:0};t[10]={id:10,fld:"",grid:0};t[11]={id:11,lvl:0,type:"int",len:4,dec:0,sign:!1,pic:"ZZZ9",ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLMINIMUMSHARES",fmt:0,gxz:"ZV37GXV1",gxold:"OV37GXV1",gxvar:"GXV1",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.GXV1=gx.num.intval(n))},v2z:function(n){n!==undefined&&(gx.O.ZV37GXV1=gx.num.intval(n))},v2c:function(){gx.fn.setControlValue("CTLMINIMUMSHARES",gx.O.GXV1,0)},c2v:function(){this.val()!==undefined&&(gx.O.GXV1=gx.num.intval(this.val()))},val:function(){return gx.fn.getIntegerValue("CTLMINIMUMSHARES",",")},nac:gx.falseFn};t[12]={id:12,fld:"",grid:0};t[13]={id:13,fld:"",grid:0};t[15]={id:15,lvl:2,type:"guid",len:4,dec:0,sign:!1,ro:0,isacc:0,grid:14,gxgrid:this.GridcontactsContainer,fnc:this.Validv_Gxv3,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLCONTACTID1",fmt:0,gxz:"ZV39GXV3",gxold:"OV39GXV3",gxvar:"GXV3",ucs:[],op:[],ip:[15],nacdep:[],ctrltype:"edit",inputType:"text",v2v:function(n){n!==undefined&&(gx.O.GXV3=n)},v2z:function(n){n!==undefined&&(gx.O.ZV39GXV3=n)},v2c:function(n){gx.fn.setGridControlValue("CTLCONTACTID1",n||gx.fn.currentGridRowImpl(14),gx.O.GXV3,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV3=this.val(n))},val:function(n){return gx.fn.getGridControlValue("CTLCONTACTID1",n||gx.fn.currentGridRowImpl(14))},nac:gx.falseFn};t[16]={id:16,lvl:2,type:"char",len:250,dec:0,sign:!1,ro:0,isacc:0,grid:14,gxgrid:this.GridcontactsContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLCONTACTPRIVATENAME",fmt:0,gxz:"ZV40GXV4",gxold:"OV40GXV4",gxvar:"GXV4",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",autoCorrect:"1",v2v:function(n){n!==undefined&&(gx.O.GXV4=n)},v2z:function(n){n!==undefined&&(gx.O.ZV40GXV4=n)},v2c:function(n){gx.fn.setGridControlValue("CTLCONTACTPRIVATENAME",n||gx.fn.currentGridRowImpl(14),gx.O.GXV4,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV4=this.val(n))},val:function(n){return gx.fn.getGridControlValue("CTLCONTACTPRIVATENAME",n||gx.fn.currentGridRowImpl(14))},nac:gx.falseFn};t[17]={id:17,lvl:2,type:"char",len:250,dec:0,sign:!1,ro:0,isacc:0,grid:14,gxgrid:this.GridcontactsContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLCONTACTUSERNAME",fmt:0,gxz:"ZV41GXV5",gxold:"OV41GXV5",gxvar:"GXV5",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",autoCorrect:"1",v2v:function(n){n!==undefined&&(gx.O.GXV5=n)},v2z:function(n){n!==undefined&&(gx.O.ZV41GXV5=n)},v2c:function(n){gx.fn.setGridControlValue("CTLCONTACTUSERNAME",n||gx.fn.currentGridRowImpl(14),gx.O.GXV5,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV5=this.val(n))},val:function(n){return gx.fn.getGridControlValue("CTLCONTACTUSERNAME",n||gx.fn.currentGridRowImpl(14))},nac:gx.falseFn};t[18]={id:18,lvl:2,type:"dtime",len:8,dec:5,sign:!1,ro:0,isacc:0,grid:14,gxgrid:this.GridcontactsContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLCONTACTINVITATIONSENT",fmt:0,gxz:"ZV42GXV6",gxold:"OV42GXV6",gxvar:"GXV6",dp:{f:0,st:!0,wn:!1,mf:!1,pic:"99/99/99 99:99",dec:5},ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",v2v:function(n){n!==undefined&&(gx.O.GXV6=gx.fn.toDatetimeValue(n,"Y4MD"))},v2z:function(n){n!==undefined&&(gx.O.ZV42GXV6=gx.fn.toDatetimeValue(n))},v2c:function(n){gx.fn.setGridControlValue("CTLCONTACTINVITATIONSENT",n||gx.fn.currentGridRowImpl(14),gx.O.GXV6,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV6=gx.fn.toDatetimeValue(this.val(n)))},val:function(n){return gx.fn.getGridDateTimeValue("CTLCONTACTINVITATIONSENT",n||gx.fn.currentGridRowImpl(14))},nac:gx.falseFn};t[19]={id:19,lvl:2,type:"dtime",len:8,dec:5,sign:!1,ro:0,isacc:0,grid:14,gxgrid:this.GridcontactsContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLCONTACTINVITACIONACCEPTED",fmt:0,gxz:"ZV43GXV7",gxold:"OV43GXV7",gxvar:"GXV7",dp:{f:0,st:!0,wn:!1,mf:!1,pic:"99/99/99 99:99",dec:5},ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",v2v:function(n){n!==undefined&&(gx.O.GXV7=gx.fn.toDatetimeValue(n,"Y4MD"))},v2z:function(n){n!==undefined&&(gx.O.ZV43GXV7=gx.fn.toDatetimeValue(n))},v2c:function(n){gx.fn.setGridControlValue("CTLCONTACTINVITACIONACCEPTED",n||gx.fn.currentGridRowImpl(14),gx.O.GXV7,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV7=gx.fn.toDatetimeValue(this.val(n)))},val:function(n){return gx.fn.getGridDateTimeValue("CTLCONTACTINVITACIONACCEPTED",n||gx.fn.currentGridRowImpl(14))},nac:gx.falseFn};t[20]={id:20,lvl:2,type:"char",len:20,dec:0,sign:!1,ro:0,isacc:0,grid:14,gxgrid:this.GridcontactsContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vREMOVECONTACT",fmt:0,gxz:"ZV25removeContact",gxold:"OV25removeContact",gxvar:"AV25removeContact",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",autoCorrect:"1",v2v:function(n){n!==undefined&&(gx.O.AV25removeContact=n)},v2z:function(n){n!==undefined&&(gx.O.ZV25removeContact=n)},v2c:function(n){gx.fn.setGridControlValue("vREMOVECONTACT",n||gx.fn.currentGridRowImpl(14),gx.O.AV25removeContact,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.AV25removeContact=this.val(n))},val:function(n){return gx.fn.getGridControlValue("vREMOVECONTACT",n||gx.fn.currentGridRowImpl(14))},nac:gx.falseFn,evt:"e191o2_client"};t[21]={id:21,fld:"",grid:0};t[22]={id:22,fld:"ADDACONTACT",grid:0,evt:"e111o1_client"};t[23]={id:23,fld:"",grid:0};t[24]={id:24,fld:"",grid:0};t[25]={id:25,fld:"SAVE",grid:0,evt:"e121o2_client"};t[26]={id:26,fld:"",grid:0};t[27]={id:27,fld:"CANCEL",grid:0,evt:"e131o2_client"};t[28]={id:28,fld:"",grid:0};t[29]={id:29,fld:"",grid:0};t[30]={id:30,fld:"SENDINVITATIOTOGROUPMEMBERS",grid:0,evt:"e151o2_client"};t[31]={id:31,fld:"",grid:0};t[32]={id:32,fld:"",grid:0};t[33]={id:33,fld:"ACTIVATEGROUP",grid:0,evt:"e161o2_client"};this.GXV1=0;this.ZV37GXV1=0;this.OV37GXV1=0;this.ZV39GXV3="00000000-0000-0000-0000-000000000000";this.OV39GXV3="00000000-0000-0000-0000-000000000000";this.ZV40GXV4="";this.OV40GXV4="";this.ZV41GXV5="";this.OV41GXV5="";this.ZV42GXV6=gx.date.nullDate();this.OV42GXV6=gx.date.nullDate();this.ZV43GXV7=gx.date.nullDate();this.OV43GXV7=gx.date.nullDate();this.ZV25removeContact="";this.OV25removeContact="";this.GXV1=0;this.GXV3="00000000-0000-0000-0000-000000000000";this.GXV4="";this.GXV5="";this.GXV6=gx.date.nullDate();this.GXV7=gx.date.nullDate();this.AV25removeContact="";this.AV18groupContacts=[];this.AV13group_sdt={groupId:"00000000-0000-0000-0000-000000000000",groupType:0,groupName:"",amIgroupOwner:!1,isActive:!1,minimumShares:0,encPassword:"",clearTextShare:"",numOfSharesReached:!1,extPubKeyMultiSigReceiving:"",extPubKeyMultiSigChange:"",Contact:[],otherGroup:{referenceGroupId:"00000000-0000-0000-0000-000000000000",invitationDeclined:!1,encPassword:"",referenceUsernName:"",signature:"",extPubKeyMultiSigReceiving:"",extPubKeyMultiSigChange:""}};this.AV32saveAndReturn=!1;this.AV24PopupName="";this.AV17groupContactAdd={contactId:"00000000-0000-0000-0000-000000000000",numShares:0,contactPrivateName:"",contactUserName:"",contactUserPubKey:"",contactencryptedKey:"",contactIV:"",contactEncryptedText:"",contactInvitationSent:gx.date.nullDate(),contactInvitacionAccepted:gx.date.nullDate(),contactInvitationDelined:!1,contactInviSent:!1,contactInvRec:!1,contactGroupId:"00000000-0000-0000-0000-000000000000",contactGroupEncPassword:"",clearTextShare:"",numOfSharesReached:!1,extPubKeyMultiSigReceiving:"",extPubKeyMultiSigChange:"",muSigSignatures:[]};this.AV10error="";this.AV5externalUser={ExternalToken:"",UserInfo:{guid:"",username:"",email:"",verified_email:!1,first_name:"",last_name:"",external_id:"",birthday:gx.date.nullDate(),gender:"",url_image:"",url_profile:"",phone:"",address:"",city:"",state:"",post_code:"",language:"",timezone:"",attributes:[],roles:[]},KeyInfo:{PrivateKey:"",PublicKey:"",TaprootPubKey:"",PublicKeyHash:"",ScriptPublicKey:"",Address:"",WIF:"",encryptedWIF:""},ChatKeyInfo:{PrivateKey:"",PublicKey:"",TaprootPubKey:"",PublicKeyHash:"",ScriptPublicKey:"",Address:"",WIF:"",encryptedWIF:""},GroupsKeyInfo:{PrivateKey:"",PublicKey:"",TaprootPubKey:"",PublicKeyHash:"",ScriptPublicKey:"",Address:"",WIF:"",encryptedWIF:""}};this.AV22message_signature={UserName:"",PubKey:"",NewPubKey:"",GroupPubKey:"",Signature:""};this.AV26sdt_message={Id:"00000000-0000-0000-0000-000000000000",MessageType:0,dateTimeUnix:0,Message:""};this.AV36extKeyInfoRoot={PrivateKey:"",PublicKey:"",ChainCode:"",Mnemonic:"",WIF:"",encryptedWIF:"",Extended:{PrivateKey:"",PrivateKeySegwitP2SH:"",PrivateKeySegwit:"",PrivateKeyTaproot:"",NuterPublicKey:"",NuterPublicKeySegwitP2SH:"",NuterPublicKeySegwit:"",NuterPublicKeyTaproot:"",Fingerprint:"",ParentFingerprint:"",Depth:0,Child:0,isHardended:!1,keyPath:""}};this.AV35wallet={WalletName:"",WalletType:"",EncryptedSecret:"",ExtEncryptedSecret:"",NetworkType:"",WalletFileName:"",WalletBaseDirectory:"",WalletReadBalanceOnStart:!1};this.Events={e121o2_client:["'SAVE'",!0],e131o2_client:["'CANCEL EDIT'",!0],e141o2_client:["GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",!0],e191o2_client:["'REMOVE CONTACT'",!0],e151o2_client:["'SEND INVITATION TO GROUP MEMBERS'",!0],e161o2_client:["'ACTIVATE GROUP'",!0],e201o2_client:["ENTER",!0],e211o2_client:["CANCEL",!0],e111o1_client:["'ADD A CONTACT'",!1]};this.EvtParms.REFRESH=[[{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"GRIDCONTACTS_nEOF"},{av:"AV25removeContact",fld:"vREMOVECONTACT"},{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14},{av:"sPrefix"},{av:"AV5externalUser",fld:"vEXTERNALUSER",hsh:!0},{av:"AV36extKeyInfoRoot",fld:"vEXTKEYINFOROOT",hsh:!0},{av:"AV35wallet",fld:"vWALLET",hsh:!0}],[]];this.EvtParms["'SAVE'"]=[[{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14},{av:"AV32saveAndReturn",fld:"vSAVEANDRETURN"}],[{av:"AV32saveAndReturn",fld:"vSAVEANDRETURN"},{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"AV10error",fld:"vERROR"}]];this.EvtParms["'CANCEL EDIT'"]=[[],[]];this.EvtParms["'ADD A CONTACT'"]=[[],[]];this.EvtParms["GRIDCONTACTS.LOAD"]=[[{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14}],[{ctrl:"CTLCONTACTPRIVATENAME",prop:"Visible"},{ctrl:"CTLCONTACTUSERNAME",prop:"Visible"},{av:"gx.fn.getCtrlProperty('vREMOVECONTACT','Visible')",ctrl:"vREMOVECONTACT",prop:"Visible"}]];this.EvtParms["GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED"]=[[{av:"AV24PopupName",fld:"vPOPUPNAME"},{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14},{av:"AV17groupContactAdd",fld:"vGROUPCONTACTADD"},{av:"GRIDCONTACTS_nEOF"},{av:"AV25removeContact",fld:"vREMOVECONTACT"},{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"sPrefix"},{av:"AV5externalUser",fld:"vEXTERNALUSER",hsh:!0},{av:"AV36extKeyInfoRoot",fld:"vEXTKEYINFOROOT",hsh:!0},{av:"AV35wallet",fld:"vWALLET",hsh:!0}],[{av:"AV25removeContact",fld:"vREMOVECONTACT"},{av:"AV17groupContactAdd",fld:"vGROUPCONTACTADD"},{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14}]];this.addExoEventHandler("extensions.web.popup.onpopupclosed",this.e141o2_client);this.EvtParms["'REMOVE CONTACT'"]=[[{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14},{av:"GRIDCONTACTS_nEOF"},{av:"AV25removeContact",fld:"vREMOVECONTACT"},{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"sPrefix"},{av:"AV5externalUser",fld:"vEXTERNALUSER",hsh:!0},{av:"AV36extKeyInfoRoot",fld:"vEXTKEYINFOROOT",hsh:!0},{av:"AV35wallet",fld:"vWALLET",hsh:!0}],[{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14}]];this.EvtParms["'SEND INVITATION TO GROUP MEMBERS'"]=[[{av:"AV10error",fld:"vERROR"},{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"AV5externalUser",fld:"vEXTERNALUSER",hsh:!0},{av:"AV22message_signature",fld:"vMESSAGE_SIGNATURE"},{av:"AV26sdt_message",fld:"vSDT_MESSAGE"},{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14},{av:"AV32saveAndReturn",fld:"vSAVEANDRETURN"}],[{av:"AV32saveAndReturn",fld:"vSAVEANDRETURN"},{av:"AV22message_signature",fld:"vMESSAGE_SIGNATURE"},{av:"AV10error",fld:"vERROR"},{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"AV26sdt_message",fld:"vSDT_MESSAGE"}]];this.EvtParms["'ACTIVATE GROUP'"]=[[{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"AV18groupContacts",fld:"vGROUPCONTACTS",grid:14},{av:"nGXsfl_14_idx",ctrl:"GRID",prop:"GridCurrRow",grid:14},{av:"GRIDCONTACTS_nFirstRecordOnPage"},{av:"nRC_GXsfl_14",ctrl:"GRIDCONTACTS",prop:"GridRC",grid:14},{av:"AV36extKeyInfoRoot",fld:"vEXTKEYINFOROOT",hsh:!0},{av:"AV35wallet",fld:"vWALLET",hsh:!0},{av:"AV5externalUser",fld:"vEXTERNALUSER",hsh:!0},{av:"AV10error",fld:"vERROR"},{av:"AV22message_signature",fld:"vMESSAGE_SIGNATURE"},{av:"AV26sdt_message",fld:"vSDT_MESSAGE"}],[{av:"AV10error",fld:"vERROR"},{av:"AV13group_sdt",fld:"vGROUP_SDT"},{av:"AV22message_signature",fld:"vMESSAGE_SIGNATURE"},{av:"AV26sdt_message",fld:"vSDT_MESSAGE"}]];this.EvtParms.ENTER=[[],[]];this.EvtParms.VALIDV_GXV3=[[{av:"GXV3",fld:"CTLCONTACTID1"}],[]];this.setVCMap("AV13group_sdt","vGROUP_SDT",0,"Wallet\registeredGroup_SDT",0,0);this.setVCMap("AV18groupContacts","vGROUPCONTACTS",0,"CollWallet\registeredGroup_SDT.ContactItem",0,0);this.setVCMap("AV32saveAndReturn","vSAVEANDRETURN",0,"boolean",4,0);this.setVCMap("AV24PopupName","vPOPUPNAME",0,"char",200,0);this.setVCMap("AV17groupContactAdd","vGROUPCONTACTADD",0,"Wallet\registeredGroup_SDT.ContactItem",0,0);this.setVCMap("AV10error","vERROR",0,"char",250,0);this.setVCMap("AV5externalUser","vEXTERNALUSER",0,"DistCryptExternalUser",0,0);this.setVCMap("AV22message_signature","vMESSAGE_SIGNATURE",0,"Wallet\registeredMessage_signature",0,0);this.setVCMap("AV26sdt_message","vSDT_MESSAGE",0,"NostrSDT_message",0,0);this.setVCMap("AV36extKeyInfoRoot","vEXTKEYINFOROOT",0,"NBitcoinExtKeyInfo",0,0);this.setVCMap("AV35wallet","vWALLET",0,"WalletWallet",0,0);this.setVCMap("AV18groupContacts","vGROUPCONTACTS",0,"CollWallet\registeredGroup_SDT.ContactItem",0,0);this.setVCMap("AV18groupContacts","vGROUPCONTACTS",0,"CollWallet\registeredGroup_SDT.ContactItem",0,0);i.addRefreshingVar({rfrVar:"AV25removeContact",rfrProp:"Value",gxAttId:"Removecontact"});i.addRefreshingVar({rfrVar:"AV13group_sdt"});i.addRefreshingVar({rfrVar:"AV18groupContacts"});i.addRefreshingVar({rfrVar:"AV5externalUser"});i.addRefreshingVar({rfrVar:"AV36extKeyInfoRoot"});i.addRefreshingVar({rfrVar:"AV35wallet"});i.addRefreshingParm({rfrVar:"AV25removeContact",rfrProp:"Value",gxAttId:"Removecontact"});i.addRefreshingParm({rfrVar:"AV13group_sdt"});i.addRefreshingParm({rfrVar:"AV18groupContacts"});i.addRefreshingParm({rfrVar:"AV5externalUser"});i.addRefreshingParm({rfrVar:"AV36extKeyInfoRoot"});i.addRefreshingParm({rfrVar:"AV35wallet"});this.addBCProperty("Group_sdt",["minimumShares"],this.GXValidFnc[11],"AV13group_sdt");this.addGridBCProperty("Groupcontacts",["contactId"],this.GXValidFnc[15],"AV18groupContacts");this.addGridBCProperty("Groupcontacts",["contactPrivateName"],this.GXValidFnc[16],"AV18groupContacts");this.addGridBCProperty("Groupcontacts",["contactUserName"],this.GXValidFnc[17],"AV18groupContacts");this.addGridBCProperty("Groupcontacts",["contactInvitationSent"],this.GXValidFnc[18],"AV18groupContacts");this.addGridBCProperty("Groupcontacts",["contactInvitacionAccepted"],this.GXValidFnc[19],"AV18groupContacts");this.Initialize()})