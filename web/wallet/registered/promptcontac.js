gx.evt.autoSkip=!1;gx.define("wallet.registered.promptcontac",!1,function(){var n,t;this.ServerClass="wallet.registered.promptcontac";this.PackageName="GeneXus.Programs";this.ServerFullClass="wallet.registered.promptcontac.aspx";this.setObjectType("web");this.hasEnterEvent=!1;this.skipOnEnter=!1;this.autoRefresh=!0;this.fullAjax=!0;this.supportAjaxEvents=!0;this.ajaxSecurityToken=!0;this.DSO="GeneXusUnanimoUnanimoWeb";this.SetStandaloneVars=function(){this.AV6allContacts=gx.fn.getControlValue("vALLCONTACTS");this.AV27wallet=gx.fn.getControlValue("vWALLET")};this.e141k2_client=function(){return this.executeServerEvent("'SELECT CONTACT'",!0,arguments[0],!1,!1)};this.e111k2_client=function(){return this.executeServerEvent("'CLOSE'",!1,null,!1,!1)};this.e151k2_client=function(){return this.executeServerEvent("ENTER",!0,arguments[0],!1,!1)};this.e161k2_client=function(){return this.executeServerEvent("CANCEL",!0,arguments[0],!1,!1)};this.GXValidFnc=[];n=this.GXValidFnc;this.GXCtrlIds=[2,3,4,5,7,8,9,10,11,12];this.GXLastCtrlId=12;this.ContactgridContainer=new gx.grid.grid(this,2,"WbpLvl2",6,"Contactgrid","Contactgrid","ContactgridContainer",this.CmpContext,this.IsMasterPage,"wallet.registered.promptcontac",[],!1,1,!1,!0,0,!1,!1,!1,"CollWallet\registeredContact_SDT",0,"px",0,"px","New row",!1,!1,!1,null,null,!1,"AV6allContacts",!1,[1,1,1,1],!1,0,!0,!1);t=this.ContactgridContainer;t.addSingleLineEdit("GXV2",7,"CTLUSERPRIVATENAME1","Contact  Name","","userPrivateName","char",0,"px",250,80,"start",null,[],"GXV2","GXV2",!0,0,!1,!1,"Attribute",0,"");t.addSingleLineEdit("GXV3",8,"CTLUSERNAME1","User Name","","userName","char",0,"px",250,80,"start",null,[],"GXV3","GXV3",!0,0,!1,!1,"Attribute",0,"");t.addSingleLineEdit("Selectcontact",9,"vSELECTCONTACT","","","selectContact","char",0,"px",20,20,"start","e141k2_client",[],"Selectcontact","selectContact",!0,0,!1,!1,"Attribute",0,"");this.ContactgridContainer.emptyText="";this.setGrid(t);n[2]={id:2,fld:"",grid:0};n[3]={id:3,fld:"MAINTABLE",grid:0};n[4]={id:4,fld:"",grid:0};n[5]={id:5,fld:"",grid:0};n[7]={id:7,lvl:2,type:"char",len:250,dec:0,sign:!1,ro:0,isacc:0,grid:6,gxgrid:this.ContactgridContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLUSERPRIVATENAME1",fmt:0,gxz:"ZV29GXV2",gxold:"OV29GXV2",gxvar:"GXV2",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",autoCorrect:"1",v2v:function(n){n!==undefined&&(gx.O.GXV2=n)},v2z:function(n){n!==undefined&&(gx.O.ZV29GXV2=n)},v2c:function(n){gx.fn.setGridControlValue("CTLUSERPRIVATENAME1",n||gx.fn.currentGridRowImpl(6),gx.O.GXV2,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV2=this.val(n))},val:function(n){return gx.fn.getGridControlValue("CTLUSERPRIVATENAME1",n||gx.fn.currentGridRowImpl(6))},nac:gx.falseFn};n[8]={id:8,lvl:2,type:"char",len:250,dec:0,sign:!1,ro:0,isacc:0,grid:6,gxgrid:this.ContactgridContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"CTLUSERNAME1",fmt:0,gxz:"ZV30GXV3",gxold:"OV30GXV3",gxvar:"GXV3",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",autoCorrect:"1",v2v:function(n){n!==undefined&&(gx.O.GXV3=n)},v2z:function(n){n!==undefined&&(gx.O.ZV30GXV3=n)},v2c:function(n){gx.fn.setGridControlValue("CTLUSERNAME1",n||gx.fn.currentGridRowImpl(6),gx.O.GXV3,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.GXV3=this.val(n))},val:function(n){return gx.fn.getGridControlValue("CTLUSERNAME1",n||gx.fn.currentGridRowImpl(6))},nac:gx.falseFn};n[9]={id:9,lvl:2,type:"char",len:20,dec:0,sign:!1,ro:0,isacc:0,grid:6,gxgrid:this.ContactgridContainer,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vSELECTCONTACT",fmt:0,gxz:"ZV25selectContact",gxold:"OV25selectContact",gxvar:"AV25selectContact",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",inputType:"text",autoCorrect:"1",v2v:function(n){n!==undefined&&(gx.O.AV25selectContact=n)},v2z:function(n){n!==undefined&&(gx.O.ZV25selectContact=n)},v2c:function(n){gx.fn.setGridControlValue("vSELECTCONTACT",n||gx.fn.currentGridRowImpl(6),gx.O.AV25selectContact,0)},c2v:function(n){this.val(n)!==undefined&&(gx.O.AV25selectContact=this.val(n))},val:function(n){return gx.fn.getGridControlValue("vSELECTCONTACT",n||gx.fn.currentGridRowImpl(6))},nac:gx.falseFn,evt:"e141k2_client"};n[10]={id:10,fld:"",grid:0};n[11]={id:11,fld:"",grid:0};n[12]={id:12,fld:"CLOSE",grid:0,evt:"e111k2_client"};this.ZV29GXV2="";this.OV29GXV2="";this.ZV30GXV3="";this.OV30GXV3="";this.ZV25selectContact="";this.OV25selectContact="";this.GXV2="";this.GXV3="";this.AV25selectContact="";this.AV6allContacts=[];this.AV27wallet={WalletName:"",WalletType:"",EncryptedSecret:"",ExtEncryptedSecret:"",NetworkType:"",WalletFileName:"",WalletBaseDirectory:"",WalletReadBalanceOnStart:!1};this.Events={e141k2_client:["'SELECT CONTACT'",!0],e111k2_client:["'CLOSE'",!0],e151k2_client:["ENTER",!0],e161k2_client:["CANCEL",!0]};this.EvtParms.REFRESH=[[{av:"CONTACTGRID_nFirstRecordOnPage"},{av:"CONTACTGRID_nEOF"},{av:"AV6allContacts",fld:"vALLCONTACTS",grid:6,hsh:!0},{av:"nGXsfl_6_idx",ctrl:"GRID",prop:"GridCurrRow",grid:6},{av:"nRC_GXsfl_6",ctrl:"CONTACTGRID",prop:"GridRC",grid:6},{av:"AV27wallet",fld:"vWALLET",hsh:!0}],[]];this.EvtParms["CONTACTGRID.LOAD"]=[[],[{av:"AV25selectContact",fld:"vSELECTCONTACT"}]];this.EvtParms["'SELECT CONTACT'"]=[[{av:"AV6allContacts",fld:"vALLCONTACTS",grid:6,hsh:!0},{av:"nGXsfl_6_idx",ctrl:"GRID",prop:"GridCurrRow",grid:6},{av:"CONTACTGRID_nFirstRecordOnPage"},{av:"nRC_GXsfl_6",ctrl:"CONTACTGRID",prop:"GridRC",grid:6},{av:"AV27wallet",fld:"vWALLET",hsh:!0}],[]];this.EvtParms["'CLOSE'"]=[[],[]];this.EvtParms.ENTER=[[],[]];this.setVCMap("AV6allContacts","vALLCONTACTS",0,"CollWallet\registeredContact_SDT",0,0);this.setVCMap("AV27wallet","vWALLET",0,"WalletWallet",0,0);t.addRefreshingVar({rfrVar:"AV6allContacts"});t.addRefreshingVar({rfrVar:"AV27wallet"});t.addRefreshingParm({rfrVar:"AV6allContacts"});t.addRefreshingParm({rfrVar:"AV27wallet"});this.addGridBCProperty("Allcontacts",["userPrivateName"],this.GXValidFnc[7],"AV6allContacts");this.addGridBCProperty("Allcontacts",["userName"],this.GXValidFnc[8],"AV6allContacts");this.Initialize()});gx.wi(function(){gx.createParentObj(this.wallet.registered.promptcontac)})