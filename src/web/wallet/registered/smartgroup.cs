using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs.wallet.registered {
   public class smartgroup : GXDataArea
   {
      public smartgroup( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public smartgroup( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId )
      {
         this.AV9groupId = aP0_groupId;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         cmbavCtlgrouptype = new GXCombobox();
         chkavCtlamigroupowner = new GXCheckbox();
         chkavCtlisactive = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "groupId");
            gxfirstwebparm_bkp = gxfirstwebparm;
            gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
            {
               setAjaxCallMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               dyncall( GetNextPar( )) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
            {
               setAjaxEventMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "groupId");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "groupId");
            }
            else
            {
               if ( ! IsValidAjaxCall( false) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = gxfirstwebparm_bkp;
            }
            if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
            {
               AV9groupId = StringUtil.StrToGuid( gxfirstwebparm);
               AssignAttri("", false, "AV9groupId", AV9groupId.ToString());
               GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV9groupId, context));
            }
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("general.ui.masterunanimosidebar", "GeneXus.Programs.general.ui.masterunanimosidebar", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         this.cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA1G2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START1G2( ) ;
         }
         return gxajaxcallmode ;
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
         context.WriteHtmlTextNl( "</title>") ;
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( StringUtil.Len( sDynURL) > 0 )
         {
            context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
         }
         define_styles( ) ;
         if ( nGXWrapped != 1 )
         {
            MasterPageObj.master_styles();
         }
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 2014200), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
         context.AddJavascriptSource("Tab/BasicTabRender.js", "", false, true);
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
         context.WriteHtmlText( "<body ") ;
         if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
         {
            context.WriteHtmlText( " dir=\"rtl\" ") ;
         }
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.smartgroup.aspx", new object[] {UrlEncode(AV9groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV9groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV9groupId, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Group_sdt", AV8group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Group_sdt", AV8group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV9groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV9groupId, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV8group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV8group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "TABS_Pagecount", StringUtil.LTrim( StringUtil.NToC( (decimal)(Tabs_Pagecount), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "TABS_Class", StringUtil.RTrim( Tabs_Class));
         GxWebStd.gx_hidden_field( context, "TABS_Historymanagement", StringUtil.BoolToStr( Tabs_Historymanagement));
      }

      public override void RenderHtmlCloseForm( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken((string)(sPrefix));
         SendComponentObjects();
         SendServerCommands();
         SendState();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         context.WriteHtmlTextNl( "</form>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
         if ( ! ( WebComp_Comp_grouptype == null ) )
         {
            WebComp_Comp_grouptype.componentjscripts();
         }
         if ( ! ( WebComp_Comp_walletbalance == null ) )
         {
            WebComp_Comp_walletbalance.componentjscripts();
         }
         if ( ! ( WebComp_Comp_signatures == null ) )
         {
            WebComp_Comp_signatures.componentjscripts();
         }
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE1G2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT1G2( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return false ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("wallet.registered.smartgroup.aspx", new object[] {UrlEncode(AV9groupId.ToString())}, new string[] {"groupId"})  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.SmartGroup" ;
      }

      public override string GetPgmdesc( )
      {
         return "Smart Group" ;
      }

      protected void WB1G0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( nGXWrapped == 1 )
            {
               RenderHtmlHeaders( ) ;
               RenderHtmlOpenForm( ) ;
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavCtlgrouptype_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavCtlgrouptype_Internalname, "Group Type", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavCtlgrouptype, cmbavCtlgrouptype_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV8group_sdt.gxTpr_Grouptype), 4, 0)), 1, cmbavCtlgrouptype_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", 1, cmbavCtlgrouptype.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", "", "", true, 0, "HLP_Wallet/registered/SmartGroup.htm");
            cmbavCtlgrouptype.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV8group_sdt.gxTpr_Grouptype), 4, 0));
            AssignProp("", false, cmbavCtlgrouptype_Internalname, "Values", (string)(cmbavCtlgrouptype.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlgroupname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlgroupname_Internalname, "Name", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            GxWebStd.gx_single_line_edit( context, edtavCtlgroupname_Internalname, StringUtil.RTrim( AV8group_sdt.gxTpr_Groupname), StringUtil.RTrim( context.localUtil.Format( AV8group_sdt.gxTpr_Groupname, "")), "", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlgroupname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlgroupname_Enabled, 0, "text", "", 80, "chr", 1, "row", 80, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/registered/SmartGroup.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavCtlamigroupowner_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavCtlamigroupowner_Internalname, "I am group Owner", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavCtlamigroupowner_Internalname, StringUtil.BoolToStr( AV8group_sdt.gxTpr_Amigroupowner), "", "I am group Owner", 1, chkavCtlamigroupowner.Enabled, "true", "", StyleString, ClassString, "", "", "");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavCtlisactive_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavCtlisactive_Internalname, "is Active", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavCtlisactive_Internalname, StringUtil.BoolToStr( AV8group_sdt.gxTpr_Isactive), "", "is Active", 1, chkavCtlisactive.Enabled, "true", "", StyleString, ClassString, "", "", "");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucTabs.SetProperty("PageCount", Tabs_Pagecount);
            ucTabs.SetProperty("Class", Tabs_Class);
            ucTabs.SetProperty("HistoryManagement", Tabs_Historymanagement);
            ucTabs.Render(context, "basictab", Tabs_Internalname, "TABSContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"TABSContainer"+"title1"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblGropuinfo_title_Internalname, "Group Info", "", "", lblGropuinfo_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/SmartGroup.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "GropuInfo") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"TABSContainer"+"panel1"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divTabpage1table_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "W0032"+"", StringUtil.RTrim( WebComp_Comp_grouptype_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpW0032"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldComp_grouptype), StringUtil.Lower( WebComp_Comp_grouptype_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0032"+"");
                  }
                  WebComp_Comp_grouptype.componentdraw();
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldComp_grouptype), StringUtil.Lower( WebComp_Comp_grouptype_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspEndCmp();
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"TABSContainer"+"title2"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblWalletbalance_title_Internalname, "Wallet Balance", "", "", lblWalletbalance_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/SmartGroup.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "WalletBalance") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"TABSContainer"+"panel2"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divTabpage2table_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "W0040"+"", StringUtil.RTrim( WebComp_Comp_walletbalance_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpW0040"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Comp_walletbalance_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldComp_walletbalance), StringUtil.Lower( WebComp_Comp_walletbalance_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0040"+"");
                  }
                  WebComp_Comp_walletbalance.componentdraw();
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldComp_walletbalance), StringUtil.Lower( WebComp_Comp_walletbalance_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspEndCmp();
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"TABSContainer"+"title3"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblSignatures_title_Internalname, "Signatures", "", "", lblSignatures_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/SmartGroup.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Signatures") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"TABSContainer"+"panel3"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divTabpage1table1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "W0048"+"", StringUtil.RTrim( WebComp_Comp_signatures_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpW0048"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Comp_signatures_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldComp_signatures), StringUtil.Lower( WebComp_Comp_signatures_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0048"+"");
                  }
                  WebComp_Comp_signatures.componentdraw();
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldComp_signatures), StringUtil.Lower( WebComp_Comp_signatures_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspEndCmp();
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START1G2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_8-180599", 0) ;
            }
         }
         Form.Meta.addItem("description", "Smart Group", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP1G0( ) ;
      }

      protected void WS1G2( )
      {
         START1G2( ) ;
         EVT1G2( ) ;
      }

      protected void EVT1G2( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               sEvt = cgiGet( "_EventName");
               EvtGridId = cgiGet( "_EventGridId");
               EvtRowId = cgiGet( "_EventRowId");
               if ( StringUtil.Len( sEvt) > 0 )
               {
                  sEvtType = StringUtil.Left( sEvt, 1);
                  sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                  if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
                  {
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Start */
                              E111G2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E121G2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ! wbErr )
                              {
                                 Rfr0gs = false;
                                 if ( ! Rfr0gs )
                                 {
                                 }
                                 dynload_actions( ) ;
                              }
                              /* No code required for Cancel button. It is implemented as the Reset button. */
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                        }
                     }
                     else if ( StringUtil.StrCmp(sEvtType, "W") == 0 )
                     {
                        sEvtType = StringUtil.Left( sEvt, 4);
                        sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-4));
                        nCmpId = (short)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                        if ( nCmpId == 32 )
                        {
                           OldComp_grouptype = cgiGet( "W0032");
                           if ( ( StringUtil.Len( OldComp_grouptype) == 0 ) || ( StringUtil.StrCmp(OldComp_grouptype, WebComp_Comp_grouptype_Component) != 0 ) )
                           {
                              WebComp_Comp_grouptype = getWebComponent(GetType(), "GeneXus.Programs", OldComp_grouptype, new Object[] {context} );
                              WebComp_Comp_grouptype.ComponentInit();
                              WebComp_Comp_grouptype.Name = "OldComp_grouptype";
                              WebComp_Comp_grouptype_Component = OldComp_grouptype;
                           }
                           if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
                           {
                              WebComp_Comp_grouptype.componentprocess("W0032", "", sEvt);
                           }
                           WebComp_Comp_grouptype_Component = OldComp_grouptype;
                        }
                        else if ( nCmpId == 40 )
                        {
                           OldComp_walletbalance = cgiGet( "W0040");
                           if ( ( StringUtil.Len( OldComp_walletbalance) == 0 ) || ( StringUtil.StrCmp(OldComp_walletbalance, WebComp_Comp_walletbalance_Component) != 0 ) )
                           {
                              WebComp_Comp_walletbalance = getWebComponent(GetType(), "GeneXus.Programs", OldComp_walletbalance, new Object[] {context} );
                              WebComp_Comp_walletbalance.ComponentInit();
                              WebComp_Comp_walletbalance.Name = "OldComp_walletbalance";
                              WebComp_Comp_walletbalance_Component = OldComp_walletbalance;
                           }
                           if ( StringUtil.Len( WebComp_Comp_walletbalance_Component) != 0 )
                           {
                              WebComp_Comp_walletbalance.componentprocess("W0040", "", sEvt);
                           }
                           WebComp_Comp_walletbalance_Component = OldComp_walletbalance;
                        }
                        else if ( nCmpId == 48 )
                        {
                           OldComp_signatures = cgiGet( "W0048");
                           if ( ( StringUtil.Len( OldComp_signatures) == 0 ) || ( StringUtil.StrCmp(OldComp_signatures, WebComp_Comp_signatures_Component) != 0 ) )
                           {
                              WebComp_Comp_signatures = getWebComponent(GetType(), "GeneXus.Programs", OldComp_signatures, new Object[] {context} );
                              WebComp_Comp_signatures.ComponentInit();
                              WebComp_Comp_signatures.Name = "OldComp_signatures";
                              WebComp_Comp_signatures_Component = OldComp_signatures;
                           }
                           if ( StringUtil.Len( WebComp_Comp_signatures_Component) != 0 )
                           {
                              WebComp_Comp_signatures.componentprocess("W0048", "", sEvt);
                           }
                           WebComp_Comp_signatures_Component = OldComp_signatures;
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE1G2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               if ( nGXWrapped == 1 )
               {
                  RenderHtmlCloseForm( ) ;
               }
            }
         }
      }

      protected void PA1G2( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            init_web_controls( ) ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = cmbavCtlgrouptype_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void send_integrity_hashes( )
      {
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
         if ( cmbavCtlgrouptype.ItemCount > 0 )
         {
            AV8group_sdt.gxTpr_Grouptype = (short)(Math.Round(NumberUtil.Val( cmbavCtlgrouptype.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV8group_sdt.gxTpr_Grouptype), 4, 0))), "."), 18, MidpointRounding.ToEven));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavCtlgrouptype.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV8group_sdt.gxTpr_Grouptype), 4, 0));
            AssignProp("", false, cmbavCtlgrouptype_Internalname, "Values", cmbavCtlgrouptype.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF1G2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         cmbavCtlgrouptype.Enabled = 0;
         AssignProp("", false, cmbavCtlgrouptype_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavCtlgrouptype.Enabled), 5, 0), true);
         edtavCtlgroupname_Enabled = 0;
         AssignProp("", false, edtavCtlgroupname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlgroupname_Enabled), 5, 0), true);
         chkavCtlamigroupowner.Enabled = 0;
         AssignProp("", false, chkavCtlamigroupowner_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavCtlamigroupowner.Enabled), 5, 0), true);
         chkavCtlisactive.Enabled = 0;
         AssignProp("", false, chkavCtlisactive_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavCtlisactive.Enabled), 5, 0), true);
      }

      protected void RF1G2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( 1 != 0 )
            {
               if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
               {
                  WebComp_Comp_grouptype.componentstart();
               }
            }
         }
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( 1 != 0 )
            {
               if ( StringUtil.Len( WebComp_Comp_walletbalance_Component) != 0 )
               {
                  WebComp_Comp_walletbalance.componentstart();
               }
            }
         }
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( 1 != 0 )
            {
               if ( StringUtil.Len( WebComp_Comp_signatures_Component) != 0 )
               {
                  WebComp_Comp_signatures.componentstart();
               }
            }
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E121G2 ();
            WB1G0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes1G2( )
      {
      }

      protected void before_start_formulas( )
      {
         cmbavCtlgrouptype.Enabled = 0;
         AssignProp("", false, cmbavCtlgrouptype_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavCtlgrouptype.Enabled), 5, 0), true);
         edtavCtlgroupname_Enabled = 0;
         AssignProp("", false, edtavCtlgroupname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlgroupname_Enabled), 5, 0), true);
         chkavCtlamigroupowner.Enabled = 0;
         AssignProp("", false, chkavCtlamigroupowner_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavCtlamigroupowner.Enabled), 5, 0), true);
         chkavCtlisactive.Enabled = 0;
         AssignProp("", false, chkavCtlisactive_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavCtlisactive.Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1G0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E111G2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vGROUP_SDT"), AV8group_sdt);
            ajax_req_read_hidden_sdt(cgiGet( "Group_sdt"), AV8group_sdt);
            /* Read saved values. */
            Tabs_Pagecount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "TABS_Pagecount"), ".", ","), 18, MidpointRounding.ToEven));
            Tabs_Class = cgiGet( "TABS_Class");
            Tabs_Historymanagement = StringUtil.StrToBool( cgiGet( "TABS_Historymanagement"));
            /* Read variables values. */
            cmbavCtlgrouptype.CurrentValue = cgiGet( cmbavCtlgrouptype_Internalname);
            AV8group_sdt.gxTpr_Grouptype = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavCtlgrouptype_Internalname), "."), 18, MidpointRounding.ToEven));
            AV8group_sdt.gxTpr_Groupname = cgiGet( edtavCtlgroupname_Internalname);
            AV8group_sdt.gxTpr_Amigroupowner = StringUtil.StrToBool( cgiGet( chkavCtlamigroupowner_Internalname));
            AV8group_sdt.gxTpr_Isactive = StringUtil.StrToBool( cgiGet( chkavCtlisactive_Internalname));
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E111G2 ();
         if (returnInSub) return;
      }

      protected void E111G2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV8group_sdt.FromJSonString(AV5websession.Get("Group_EDIT"), null);
         if ( ( AV8group_sdt.gxTpr_Grouptype == 10 ) && AV8group_sdt.gxTpr_Amigroupowner )
         {
            /* Object Property */
            if ( true )
            {
               bDynCreated_Comp_grouptype = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_grouptype_Component), StringUtil.Lower( "Wallet.registered.SmartGroupWalletBackup")) != 0 )
            {
               WebComp_Comp_grouptype = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.smartgroupwalletbackup", new Object[] {context} );
               WebComp_Comp_grouptype.ComponentInit();
               WebComp_Comp_grouptype.Name = "Wallet.registered.SmartGroupWalletBackup";
               WebComp_Comp_grouptype_Component = "Wallet.registered.SmartGroupWalletBackup";
            }
            if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
            {
               WebComp_Comp_grouptype.setjustcreated();
               WebComp_Comp_grouptype.componentprepare(new Object[] {(string)"W0032",(string)""});
               WebComp_Comp_grouptype.componentbind(new Object[] {});
            }
            this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)2});
            this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)3});
         }
         else if ( ( AV8group_sdt.gxTpr_Grouptype == 10 ) && ! AV8group_sdt.gxTpr_Amigroupowner )
         {
            /* Object Property */
            if ( true )
            {
               bDynCreated_Comp_grouptype = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_grouptype_Component), StringUtil.Lower( "Wallet.registered.SmartGroupWalletBackupNotOwner")) != 0 )
            {
               WebComp_Comp_grouptype = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.smartgroupwalletbackupnotowner", new Object[] {context} );
               WebComp_Comp_grouptype.ComponentInit();
               WebComp_Comp_grouptype.Name = "Wallet.registered.SmartGroupWalletBackupNotOwner";
               WebComp_Comp_grouptype_Component = "Wallet.registered.SmartGroupWalletBackupNotOwner";
            }
            if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
            {
               WebComp_Comp_grouptype.setjustcreated();
               WebComp_Comp_grouptype.componentprepare(new Object[] {(string)"W0032",(string)""});
               WebComp_Comp_grouptype.componentbind(new Object[] {});
            }
            this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)2});
            this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)3});
         }
         else if ( ( AV8group_sdt.gxTpr_Grouptype == 200 ) && AV8group_sdt.gxTpr_Amigroupowner )
         {
            /* Object Property */
            if ( true )
            {
               bDynCreated_Comp_grouptype = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_grouptype_Component), StringUtil.Lower( "Wallet.registered.DelegationMultiSignature")) != 0 )
            {
               WebComp_Comp_grouptype = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.delegationmultisignature", new Object[] {context} );
               WebComp_Comp_grouptype.ComponentInit();
               WebComp_Comp_grouptype.Name = "Wallet.registered.DelegationMultiSignature";
               WebComp_Comp_grouptype_Component = "Wallet.registered.DelegationMultiSignature";
            }
            if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
            {
               WebComp_Comp_grouptype.setjustcreated();
               WebComp_Comp_grouptype.componentprepare(new Object[] {(string)"W0032",(string)""});
               WebComp_Comp_grouptype.componentbind(new Object[] {});
            }
            if ( ! AV8group_sdt.gxTpr_Isactive )
            {
               this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)2});
               this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)3});
            }
            else
            {
               /* Object Property */
               if ( true )
               {
                  bDynCreated_Comp_walletbalance = true;
               }
               if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_walletbalance_Component), StringUtil.Lower( "Wallet.registered.DelegationMultiSigWallet")) != 0 )
               {
                  WebComp_Comp_walletbalance = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.delegationmultisigwallet", new Object[] {context} );
                  WebComp_Comp_walletbalance.ComponentInit();
                  WebComp_Comp_walletbalance.Name = "Wallet.registered.DelegationMultiSigWallet";
                  WebComp_Comp_walletbalance_Component = "Wallet.registered.DelegationMultiSigWallet";
               }
               if ( StringUtil.Len( WebComp_Comp_walletbalance_Component) != 0 )
               {
                  WebComp_Comp_walletbalance.setjustcreated();
                  WebComp_Comp_walletbalance.componentprepare(new Object[] {(string)"W0040",(string)"",(Guid)AV9groupId});
                  WebComp_Comp_walletbalance.componentbind(new Object[] {(string)""});
               }
               /* Object Property */
               if ( true )
               {
                  bDynCreated_Comp_signatures = true;
               }
               if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_signatures_Component), StringUtil.Lower( "Wallet.registered.DelegationMyltiSigAllSignatures")) != 0 )
               {
                  WebComp_Comp_signatures = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.delegationmyltisigallsignatures", new Object[] {context} );
                  WebComp_Comp_signatures.ComponentInit();
                  WebComp_Comp_signatures.Name = "Wallet.registered.DelegationMyltiSigAllSignatures";
                  WebComp_Comp_signatures_Component = "Wallet.registered.DelegationMyltiSigAllSignatures";
               }
               if ( StringUtil.Len( WebComp_Comp_signatures_Component) != 0 )
               {
                  WebComp_Comp_signatures.setjustcreated();
                  WebComp_Comp_signatures.componentprepare(new Object[] {(string)"W0048",(string)"",(Guid)AV9groupId});
                  WebComp_Comp_signatures.componentbind(new Object[] {(string)""});
               }
            }
         }
         else if ( ( AV8group_sdt.gxTpr_Grouptype == 200 ) && ! AV8group_sdt.gxTpr_Amigroupowner )
         {
            /* Object Property */
            if ( true )
            {
               bDynCreated_Comp_grouptype = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_grouptype_Component), StringUtil.Lower( "Wallet.registered.DelegationMultiSignatureNotOwner")) != 0 )
            {
               WebComp_Comp_grouptype = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.delegationmultisignaturenotowner", new Object[] {context} );
               WebComp_Comp_grouptype.ComponentInit();
               WebComp_Comp_grouptype.Name = "Wallet.registered.DelegationMultiSignatureNotOwner";
               WebComp_Comp_grouptype_Component = "Wallet.registered.DelegationMultiSignatureNotOwner";
            }
            if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
            {
               WebComp_Comp_grouptype.setjustcreated();
               WebComp_Comp_grouptype.componentprepare(new Object[] {(string)"W0032",(string)""});
               WebComp_Comp_grouptype.componentbind(new Object[] {});
            }
            if ( ! AV8group_sdt.gxTpr_Isactive )
            {
               this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)2});
               this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)3});
            }
            else
            {
               /* Object Property */
               if ( true )
               {
                  bDynCreated_Comp_walletbalance = true;
               }
               if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_walletbalance_Component), StringUtil.Lower( "Wallet.registered.DelegationMultiSigWallet")) != 0 )
               {
                  WebComp_Comp_walletbalance = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.delegationmultisigwallet", new Object[] {context} );
                  WebComp_Comp_walletbalance.ComponentInit();
                  WebComp_Comp_walletbalance.Name = "Wallet.registered.DelegationMultiSigWallet";
                  WebComp_Comp_walletbalance_Component = "Wallet.registered.DelegationMultiSigWallet";
               }
               if ( StringUtil.Len( WebComp_Comp_walletbalance_Component) != 0 )
               {
                  WebComp_Comp_walletbalance.setjustcreated();
                  WebComp_Comp_walletbalance.componentprepare(new Object[] {(string)"W0040",(string)"",(Guid)AV9groupId});
                  WebComp_Comp_walletbalance.componentbind(new Object[] {(string)""});
               }
               /* Object Property */
               if ( true )
               {
                  bDynCreated_Comp_signatures = true;
               }
               if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_signatures_Component), StringUtil.Lower( "Wallet.registered.DelegationMyltiSigAllSignatures")) != 0 )
               {
                  WebComp_Comp_signatures = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.delegationmyltisigallsignatures", new Object[] {context} );
                  WebComp_Comp_signatures.ComponentInit();
                  WebComp_Comp_signatures.Name = "Wallet.registered.DelegationMyltiSigAllSignatures";
                  WebComp_Comp_signatures_Component = "Wallet.registered.DelegationMyltiSigAllSignatures";
               }
               if ( StringUtil.Len( WebComp_Comp_signatures_Component) != 0 )
               {
                  WebComp_Comp_signatures.setjustcreated();
                  WebComp_Comp_signatures.componentprepare(new Object[] {(string)"W0048",(string)"",(Guid)AV9groupId});
                  WebComp_Comp_signatures.componentbind(new Object[] {(string)""});
               }
            }
         }
         else if ( ( AV8group_sdt.gxTpr_Grouptype == 300 ) && AV8group_sdt.gxTpr_Amigroupowner )
         {
            /* Object Property */
            if ( true )
            {
               bDynCreated_Comp_grouptype = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_grouptype_Component), StringUtil.Lower( "Wallet.registered.VoteMultiSignature")) != 0 )
            {
               WebComp_Comp_grouptype = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.votemultisignature", new Object[] {context} );
               WebComp_Comp_grouptype.ComponentInit();
               WebComp_Comp_grouptype.Name = "Wallet.registered.VoteMultiSignature";
               WebComp_Comp_grouptype_Component = "Wallet.registered.VoteMultiSignature";
            }
            if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
            {
               WebComp_Comp_grouptype.setjustcreated();
               WebComp_Comp_grouptype.componentprepare(new Object[] {(string)"W0032",(string)""});
               WebComp_Comp_grouptype.componentbind(new Object[] {});
            }
            this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)2});
            this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)3});
         }
         else if ( ( AV8group_sdt.gxTpr_Grouptype == 100 ) && AV8group_sdt.gxTpr_Amigroupowner )
         {
            /* Object Property */
            if ( true )
            {
               bDynCreated_Comp_grouptype = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_grouptype_Component), StringUtil.Lower( "Wallet.registered.MultiSignature")) != 0 )
            {
               WebComp_Comp_grouptype = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.multisignature", new Object[] {context} );
               WebComp_Comp_grouptype.ComponentInit();
               WebComp_Comp_grouptype.Name = "Wallet.registered.MultiSignature";
               WebComp_Comp_grouptype_Component = "Wallet.registered.MultiSignature";
            }
            if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
            {
               WebComp_Comp_grouptype.setjustcreated();
               WebComp_Comp_grouptype.componentprepare(new Object[] {(string)"W0032",(string)""});
               WebComp_Comp_grouptype.componentbind(new Object[] {});
            }
            this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)2});
            this.executeUsercontrolMethod("", false, "TABSContainer", "HideTab", "", new Object[] {(short)3});
         }
         else
         {
            GX_msglist.addItem("This type of group is not implemented yet");
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E121G2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV9groupId = (Guid)getParm(obj,0);
         AssignAttri("", false, "AV9groupId", AV9groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV9groupId, context));
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PA1G2( ) ;
         WS1G2( ) ;
         WE1G2( ) ;
         this.cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("Tab/BasicTab.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         if ( ! ( WebComp_Comp_grouptype == null ) )
         {
            if ( StringUtil.Len( WebComp_Comp_grouptype_Component) != 0 )
            {
               WebComp_Comp_grouptype.componentthemes();
            }
         }
         if ( ! ( WebComp_Comp_walletbalance == null ) )
         {
            if ( StringUtil.Len( WebComp_Comp_walletbalance_Component) != 0 )
            {
               WebComp_Comp_walletbalance.componentthemes();
            }
         }
         if ( ! ( WebComp_Comp_signatures == null ) )
         {
            if ( StringUtil.Len( WebComp_Comp_signatures_Component) != 0 )
            {
               WebComp_Comp_signatures.componentthemes();
            }
         }
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20248813113260", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("wallet/registered/smartgroup.js", "?20248813113260", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
         context.AddJavascriptSource("Tab/BasicTabRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         cmbavCtlgrouptype.Name = "CTLGROUPTYPE";
         cmbavCtlgrouptype.WebTags = "";
         cmbavCtlgrouptype.addItem("0", "Select Group Type", 0);
         cmbavCtlgrouptype.addItem("10", "Wallet Backup", 0);
         cmbavCtlgrouptype.addItem("200", "Delegation Multi Signature Wallet", 0);
         cmbavCtlgrouptype.addItem("100", "Multi Signature Wallet", 0);
         cmbavCtlgrouptype.addItem("600", "Concensus File Vault", 0);
         cmbavCtlgrouptype.addItem("700", "Concensus Timed File Vault", 0);
         cmbavCtlgrouptype.addItem("300", "Vote based wallet", 0);
         if ( cmbavCtlgrouptype.ItemCount > 0 )
         {
            AV8group_sdt.gxTpr_Grouptype = (short)(Math.Round(NumberUtil.Val( cmbavCtlgrouptype.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV8group_sdt.gxTpr_Grouptype), 4, 0))), "."), 18, MidpointRounding.ToEven));
         }
         chkavCtlamigroupowner.Name = "CTLAMIGROUPOWNER";
         chkavCtlamigroupowner.WebTags = "";
         chkavCtlamigroupowner.Caption = "";
         AssignProp("", false, chkavCtlamigroupowner_Internalname, "TitleCaption", chkavCtlamigroupowner.Caption, true);
         chkavCtlamigroupowner.CheckedValue = "false";
         chkavCtlisactive.Name = "CTLISACTIVE";
         chkavCtlisactive.WebTags = "";
         chkavCtlisactive.Caption = "";
         AssignProp("", false, chkavCtlisactive_Internalname, "TitleCaption", chkavCtlisactive.Caption, true);
         chkavCtlisactive.CheckedValue = "false";
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         cmbavCtlgrouptype_Internalname = "CTLGROUPTYPE";
         edtavCtlgroupname_Internalname = "CTLGROUPNAME";
         chkavCtlamigroupowner_Internalname = "CTLAMIGROUPOWNER";
         chkavCtlisactive_Internalname = "CTLISACTIVE";
         lblGropuinfo_title_Internalname = "GROPUINFO_TITLE";
         divTabpage1table_Internalname = "TABPAGE1TABLE";
         lblWalletbalance_title_Internalname = "WALLETBALANCE_TITLE";
         divTabpage2table_Internalname = "TABPAGE2TABLE";
         lblSignatures_title_Internalname = "SIGNATURES_TITLE";
         divTabpage1table1_Internalname = "TABPAGE1TABLE1";
         Tabs_Internalname = "TABS";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         chkavCtlisactive.Caption = "is Active";
         chkavCtlamigroupowner.Caption = "I am group Owner";
         chkavCtlisactive.Enabled = -1;
         chkavCtlamigroupowner.Enabled = -1;
         edtavCtlgroupname_Enabled = -1;
         cmbavCtlgrouptype.Enabled = -1;
         chkavCtlisactive.Enabled = 0;
         chkavCtlamigroupowner.Enabled = 0;
         edtavCtlgroupname_Jsonclick = "";
         edtavCtlgroupname_Enabled = 0;
         cmbavCtlgrouptype_Jsonclick = "";
         cmbavCtlgrouptype.Enabled = 0;
         Tabs_Historymanagement = Convert.ToBoolean( 0);
         Tabs_Class = "Tab";
         Tabs_Pagecount = 3;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Smart Group";
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GXV3","fld":"CTLAMIGROUPOWNER"},{"av":"GXV4","fld":"CTLISACTIVE"},{"av":"AV9groupId","fld":"vGROUPID","hsh":true}]}""");
         setEventMetadata("VALIDV_GXV1","""{"handler":"Validv_Gxv1","iparms":[]}""");
         return  ;
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         wcpOAV9groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV8group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         StyleString = "";
         ucTabs = new GXUserControl();
         lblGropuinfo_title_Jsonclick = "";
         WebComp_Comp_grouptype_Component = "";
         OldComp_grouptype = "";
         lblWalletbalance_title_Jsonclick = "";
         WebComp_Comp_walletbalance_Component = "";
         OldComp_walletbalance = "";
         lblSignatures_title_Jsonclick = "";
         WebComp_Comp_signatures_Component = "";
         OldComp_signatures = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV5websession = context.GetSession();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         WebComp_Comp_grouptype = new GeneXus.Http.GXNullWebComponent();
         WebComp_Comp_walletbalance = new GeneXus.Http.GXNullWebComponent();
         WebComp_Comp_signatures = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
         cmbavCtlgrouptype.Enabled = 0;
         edtavCtlgroupname_Enabled = 0;
         chkavCtlamigroupowner.Enabled = 0;
         chkavCtlisactive.Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int Tabs_Pagecount ;
      private int edtavCtlgroupname_Enabled ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string Tabs_Class ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string cmbavCtlgrouptype_Internalname ;
      private string cmbavCtlgrouptype_Jsonclick ;
      private string edtavCtlgroupname_Internalname ;
      private string edtavCtlgroupname_Jsonclick ;
      private string chkavCtlamigroupowner_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string chkavCtlisactive_Internalname ;
      private string Tabs_Internalname ;
      private string lblGropuinfo_title_Internalname ;
      private string lblGropuinfo_title_Jsonclick ;
      private string divTabpage1table_Internalname ;
      private string WebComp_Comp_grouptype_Component ;
      private string OldComp_grouptype ;
      private string lblWalletbalance_title_Internalname ;
      private string lblWalletbalance_title_Jsonclick ;
      private string divTabpage2table_Internalname ;
      private string WebComp_Comp_walletbalance_Component ;
      private string OldComp_walletbalance ;
      private string lblSignatures_title_Internalname ;
      private string lblSignatures_title_Jsonclick ;
      private string divTabpage1table1_Internalname ;
      private string WebComp_Comp_signatures_Component ;
      private string OldComp_signatures ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool Tabs_Historymanagement ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool bDynCreated_Comp_grouptype ;
      private bool bDynCreated_Comp_walletbalance ;
      private bool bDynCreated_Comp_signatures ;
      private Guid AV9groupId ;
      private Guid wcpOAV9groupId ;
      private IGxSession AV5websession ;
      private GXWebComponent WebComp_Comp_grouptype ;
      private GXWebComponent WebComp_Comp_walletbalance ;
      private GXWebComponent WebComp_Comp_signatures ;
      private GXUserControl ucTabs ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavCtlgrouptype ;
      private GXCheckbox chkavCtlamigroupowner ;
      private GXCheckbox chkavCtlisactive ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private GXWebForm Form ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV8group_sdt ;
   }

}
