gx.evt.autoSkip=!1;gx.define("wallet.registered.createsmartgroup",!1,function(){this.ServerClass="wallet.registered.createsmartgroup";this.PackageName="GeneXus.Programs";this.ServerFullClass="wallet.registered.createsmartgroup.aspx";this.setObjectType("web");this.hasEnterEvent=!1;this.skipOnEnter=!1;this.autoRefresh=!0;this.fullAjax=!0;this.supportAjaxEvents=!0;this.ajaxSecurityToken=!0;this.DSO="GeneXusUnanimoUnanimoWeb";this.SetStandaloneVars=function(){this.AV7group_sdt=gx.fn.getControlValue("vGROUP_SDT");this.AV5all_groups_sdt=gx.fn.getControlValue("vALL_GROUPS_SDT")};this.Validv_Grouptype=function(){return this.validCliEvt("Validv_Grouptype",0,function(){try{var n=gx.util.balloon.getNew("vGROUPTYPE");if(this.AnyError=0,!(this.AV9groupType==0||this.AV9groupType==10||this.AV9groupType==200||this.AV9groupType==100||this.AV9groupType==600||this.AV9groupType==700||this.AV9groupType==300))try{n.setError("Field group Type is out of range");this.AnyError=gx.num.trunc(1,0)}catch(t){}}catch(t){}try{return n==null?!0:n.show()}catch(t){}return!0})};this.e121i2_client=function(){return this.executeServerEvent("'CREATE'",!1,null,!1,!1)};this.e131i2_client=function(){return this.executeServerEvent("'CANCEL EDIT'",!1,null,!1,!1)};this.e151i2_client=function(){return this.executeServerEvent("ENTER",!0,null,!1,!1)};this.e161i2_client=function(){return this.executeServerEvent("CANCEL",!0,null,!1,!1)};this.GXValidFnc=[];var n=this.GXValidFnc;this.GXCtrlIds=[2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18];this.GXLastCtrlId=18;n[2]={id:2,fld:"",grid:0};n[3]={id:3,fld:"MAINTABLE",grid:0};n[4]={id:4,fld:"",grid:0};n[5]={id:5,fld:"",grid:0};n[6]={id:6,fld:"",grid:0};n[7]={id:7,fld:"",grid:0};n[8]={id:8,lvl:0,type:"int",len:4,dec:0,sign:!1,pic:"ZZZ9",ro:0,grid:0,gxgrid:null,fnc:this.Validv_Grouptype,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vGROUPTYPE",fmt:0,gxz:"ZV9groupType",gxold:"OV9groupType",gxvar:"AV9groupType",ucs:[],op:[8],ip:[8],nacdep:[],ctrltype:"combo",v2v:function(n){n!==undefined&&(gx.O.AV9groupType=gx.num.intval(n))},v2z:function(n){n!==undefined&&(gx.O.ZV9groupType=gx.num.intval(n))},v2c:function(){gx.fn.setComboBoxValue("vGROUPTYPE",gx.O.AV9groupType);typeof this.dom_hdl=="function"&&this.dom_hdl.call(gx.O)},c2v:function(){this.val()!==undefined&&(gx.O.AV9groupType=gx.num.intval(this.val()))},val:function(){return gx.fn.getIntegerValue("vGROUPTYPE",",")},nac:gx.falseFn};this.declareDomainHdlr(8,function(){});n[9]={id:9,fld:"",grid:0};n[10]={id:10,fld:"",grid:0};n[11]={id:11,fld:"",grid:0};n[12]={id:12,fld:"",grid:0};n[13]={id:13,lvl:0,type:"char",len:80,dec:0,sign:!1,ro:0,grid:0,gxgrid:null,fnc:null,isvalid:null,evt_cvc:null,evt_cvcing:null,rgrid:[],fld:"vGROUPNAME",fmt:0,gxz:"ZV8groupName",gxold:"OV8groupName",gxvar:"AV8groupName",ucs:[],op:[],ip:[],nacdep:[],ctrltype:"edit",v2v:function(n){n!==undefined&&(gx.O.AV8groupName=n)},v2z:function(n){n!==undefined&&(gx.O.ZV8groupName=n)},v2c:function(){gx.fn.setControlValue("vGROUPNAME",gx.O.AV8groupName,0)},c2v:function(){this.val()!==undefined&&(gx.O.AV8groupName=this.val())},val:function(){return gx.fn.getControlValue("vGROUPNAME")},nac:gx.falseFn};n[14]={id:14,fld:"",grid:0};n[15]={id:15,fld:"",grid:0};n[16]={id:16,fld:"CREATE",grid:0,evt:"e121i2_client"};n[17]={id:17,fld:"",grid:0};n[18]={id:18,fld:"CANCEL",grid:0,evt:"e131i2_client"};this.AV9groupType=0;this.ZV9groupType=0;this.OV9groupType=0;this.AV8groupName="";this.ZV8groupName="";this.OV8groupName="";this.AV9groupType=0;this.AV8groupName="";this.AV7group_sdt={groupId:"00000000-0000-0000-0000-000000000000",groupType:0,groupName:"",amIgroupOwner:!1,isActive:!1,minimumShares:0,encPassword:"",clearTextShare:"",numOfSharesReached:!1,extPubKeyMultiSigReceiving:"",extPubKeyMultiSigChange:"",Contact:[],otherGroup:{referenceGroupId:"00000000-0000-0000-0000-000000000000",invitationDeclined:!1,encPassword:"",referenceUsernName:"",signature:"",extPubKeyMultiSigReceiving:"",extPubKeyMultiSigChange:""}};this.AV5all_groups_sdt=[];this.Events={e121i2_client:["'CREATE'",!0],e131i2_client:["'CANCEL EDIT'",!0],e151i2_client:["ENTER",!0],e161i2_client:["CANCEL",!0]};this.EvtParms.REFRESH=[[],[]];this.EvtParms["'CREATE'"]=[[{ctrl:"vGROUPTYPE"},{av:"AV9groupType",fld:"vGROUPTYPE",pic:"ZZZ9"},{av:"AV8groupName",fld:"vGROUPNAME"},{av:"AV7group_sdt",fld:"vGROUP_SDT"},{av:"AV5all_groups_sdt",fld:"vALL_GROUPS_SDT"}],[{av:"AV7group_sdt",fld:"vGROUP_SDT"},{av:"AV6error",fld:"vERROR"},{av:"AV5all_groups_sdt",fld:"vALL_GROUPS_SDT"}]];this.EvtParms["'CANCEL EDIT'"]=[[],[]];this.EvtParms.ENTER=[[],[]];this.EvtParms.VALIDV_GROUPTYPE=[[],[]];this.setVCMap("AV7group_sdt","vGROUP_SDT",0,"Wallet\registeredGroup_SDT",0,0);this.setVCMap("AV5all_groups_sdt","vALL_GROUPS_SDT",0,"CollWallet\registeredGroup_SDT",0,0);this.Initialize()});gx.wi(function(){gx.createParentObj(this.wallet.registered.createsmartgroup)})