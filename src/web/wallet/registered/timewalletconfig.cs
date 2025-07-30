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
using System.Text.Json.Serialization;
namespace GeneXus.Programs.wallet.registered {
   public class timewalletconfig : GXWebComponent
   {
      public timewalletconfig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
      }

      public timewalletconfig( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            if ( nGotPars == 0 )
            {
               entryPointCalled = false;
               gxfirstwebparm = GetNextPar( );
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
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
                  gxfirstwebparm = GetNextPar( );
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetNextPar( );
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
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA2L2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               Gx_date = DateTimeUtil.Today( context);
               WS2L2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
               }
            }
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
         cleanup();
      }

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( "Time Wallet Config") ;
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
         }
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1759500), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
         context.AddJavascriptSource("Tab/BasicTabRender.js", "", false, true);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
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
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.timewalletconfig") +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vTODAY", context.localUtil.DToC( Gx_date, 0, "/"));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vTODAY", GetSecureSignedToken( sPrefix, Gx_date, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV25wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV25wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV25wallet, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vWASACTIVE", AV27wasActive);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWASACTIVE", GetSecureSignedToken( sPrefix, AV27wasActive, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUP_SDT", AV14group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUP_SDT", AV14group_sdt);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vTODAY", context.localUtil.DToC( Gx_date, 0, "/"));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vTODAY", GetSecureSignedToken( sPrefix, Gx_date, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vONETIMECONSTRAIN", AV17oneTimeConstrain);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vONETIMECONSTRAIN", AV17oneTimeConstrain);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV18PopupName));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSTOREDTRANSACTIONS", AV23storedTransactions);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSTOREDTRANSACTIONS", AV23storedTransactions);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV25wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV25wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV25wallet, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vWASACTIVE", AV27wasActive);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWASACTIVE", GetSecureSignedToken( sPrefix, AV27wasActive, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vCOMPONENTNAME", StringUtil.RTrim( AV6componentName));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABS_Pagecount", StringUtil.LTrim( StringUtil.NToC( (decimal)(Tabs_Pagecount), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABS_Class", StringUtil.RTrim( Tabs_Class));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABS_Historymanagement", StringUtil.BoolToStr( Tabs_Historymanagement));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABS_Activepagecontrolname", StringUtil.RTrim( Tabs_Activepagecontrolname));
      }

      protected void RenderHtmlCloseForm2L2( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
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
            if ( ! ( WebComp_Tabcomponent == null ) )
            {
               WebComp_Tabcomponent.componentjscripts();
            }
            context.WriteHtmlTextNl( "</body>") ;
            context.WriteHtmlTextNl( "</html>") ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.TimeWalletConfig" ;
      }

      public override string GetPgmdesc( )
      {
         return "Time Wallet Config" ;
      }

      protected void WB2L0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.timewalletconfig");
               context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
               context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
               context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
               context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
               context.AddJavascriptSource("Tab/BasicTabRender.js", "", false, true);
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTable1_Internalname, 1, 100, "%", 0, "px", "Table", "start", "top", " "+"data-gx-smarttable"+" ", "grid-template-columns:50fr 50fr;grid-template-rows:auto;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-smarttable-cell"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group", "start", "top", ""+" data-gx-for=\""+edtavRestoredate_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRestoredate_Internalname, "Select the date when the backup became available to be restored.", "gx-form-item AttributeLabel", 1, true, "width: 65%;");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 35, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'" + sPrefix + "',false,'',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavRestoredate_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavRestoredate_Internalname, context.localUtil.Format(AV19restoreDate, "99/99/99"), context.localUtil.Format( AV19restoreDate, "99/99/99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',0,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',0,12,'eng',false,0);"+";gx.evt.onblur(this,10);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavRestoredate_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavRestoredate_Enabled, 1, "text", "", 8, "chr", 1, "row", 8, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/registered/TimeWalletConfig.htm");
            GxWebStd.gx_bitmap( context, edtavRestoredate_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((1==0)||(edtavRestoredate_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "none", "HLP_Wallet/registered/TimeWalletConfig.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-smarttable-cell"+" ", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttChangerestoredate_Internalname, "", "Change Restore Date", bttChangerestoredate_Jsonclick, 7, "Change Restore Date", "", StyleString, ClassString, bttChangerestoredate_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e112l1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/TimeWalletConfig.htm");
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
            ucTabs.Render(context, "basictab", Tabs_Internalname, sPrefix+"TABSContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"TABSContainer"+"title1"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblResotregroup_title_Internalname, "Resotre Group", "", "", lblResotregroup_title_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/TimeWalletConfig.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "ResotreGroup") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"TABSContainer"+"panel1"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divTabpage1table_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"TABSContainer"+"title2"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblBountygroup_title_Internalname, "Bounty Group", "", "", lblBountygroup_title_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/TimeWalletConfig.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "BountyGroup") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"TABSContainer"+"panel2"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divTabpage2table_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, sPrefix+"W0028"+"", StringUtil.RTrim( WebComp_Tabcomponent_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+sPrefix+"gxHTMLWrpW0028"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Tabcomponent_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldTabcomponent), StringUtil.Lower( WebComp_Tabcomponent_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0028"+"");
                  }
                  WebComp_Tabcomponent.componentdraw();
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldTabcomponent), StringUtil.Lower( WebComp_Tabcomponent_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspEndCmp();
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 50, "px", "col-xs-12", "start", "Middle", "", "", "div");
            context.WriteHtmlText( "<hr/>") ;
            GxWebStd.gx_div_end( context, "start", "Middle", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSave_Internalname, "", "Save", bttSave_Jsonclick, 5, "Save", "", StyleString, ClassString, bttSave_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SAVE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/TimeWalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttClose_Internalname, "", "Close", bttClose_Jsonclick, 5, "Close", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/TimeWalletConfig.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttActivategroups_Internalname, "", "Activate Groups", bttActivategroups_Jsonclick, 7, "Activate Groups", "", StyleString, ClassString, bttActivategroups_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e122l1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/TimeWalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START2L2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_13-186702", 0) ;
               }
            }
            Form.Meta.addItem("description", "Time Wallet Config", 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUP2L0( ) ;
            }
         }
      }

      protected void WS2L2( )
      {
         START2L2( ) ;
         EVT2L2( ) ;
      }

      protected void EVT2L2( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
                  if ( StringUtil.Len( sEvt) > 0 )
                  {
                     sEvtType = StringUtil.Left( sEvt, 1);
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E132L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Save' */
                                    E142L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CLOSE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Close' */
                                    E152L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E162L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Load */
                                    E172L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       if ( ! Rfr0gs )
                                       {
                                       }
                                       dynload_actions( ) ;
                                    }
                                 }
                              }
                              /* No code required for Cancel button. It is implemented as the Reset button. */
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavRestoredate_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
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
                        if ( nCmpId == 28 )
                        {
                           OldTabcomponent = cgiGet( sPrefix+"W0028");
                           if ( ( StringUtil.Len( OldTabcomponent) == 0 ) || ( StringUtil.StrCmp(OldTabcomponent, WebComp_Tabcomponent_Component) != 0 ) )
                           {
                              WebComp_Tabcomponent = getWebComponent(GetType(), "GeneXus.Programs", OldTabcomponent, new Object[] {context} );
                              WebComp_Tabcomponent.ComponentInit();
                              WebComp_Tabcomponent.Name = "OldTabcomponent";
                              WebComp_Tabcomponent_Component = OldTabcomponent;
                           }
                           if ( StringUtil.Len( WebComp_Tabcomponent_Component) != 0 )
                           {
                              WebComp_Tabcomponent.componentprocess(sPrefix+"W0028", "", sEvt);
                           }
                           WebComp_Tabcomponent_Component = OldTabcomponent;
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE2L2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm2L2( ) ;
            }
         }
      }

      protected void PA2L2( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavRestoredate_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
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
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF2L2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         Gx_date = DateTimeUtil.Today( context);
      }

      protected void RF2L2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( 1 != 0 )
            {
               if ( StringUtil.Len( WebComp_Tabcomponent_Component) != 0 )
               {
                  WebComp_Tabcomponent.componentstart();
               }
            }
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E172L2 ();
            WB2L0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes2L2( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vTODAY", context.localUtil.DToC( Gx_date, 0, "/"));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vTODAY", GetSecureSignedToken( sPrefix, Gx_date, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV25wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV25wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV25wallet, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vWASACTIVE", AV27wasActive);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWASACTIVE", GetSecureSignedToken( sPrefix, AV27wasActive, context));
      }

      protected void before_start_formulas( )
      {
         Gx_date = DateTimeUtil.Today( context);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2L0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E132L2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vGROUP_SDT"), AV14group_sdt);
            /* Read saved values. */
            AV6componentName = cgiGet( sPrefix+"vCOMPONENTNAME");
            Tabs_Pagecount = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"TABS_Pagecount"), ".", ","), 18, MidpointRounding.ToEven));
            Tabs_Class = cgiGet( sPrefix+"TABS_Class");
            Tabs_Historymanagement = StringUtil.StrToBool( cgiGet( sPrefix+"TABS_Historymanagement"));
            /* Read variables values. */
            if ( context.localUtil.VCDate( cgiGet( edtavRestoredate_Internalname), 1) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_faildate", new   object[]  {"restore Date"}), 1, "vRESTOREDATE");
               GX_FocusControl = edtavRestoredate_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV19restoreDate = DateTime.MinValue;
               AssignAttri(sPrefix, false, "AV19restoreDate", context.localUtil.Format(AV19restoreDate, "99/99/99"));
            }
            else
            {
               AV19restoreDate = context.localUtil.CToD( cgiGet( edtavRestoredate_Internalname), 1);
               AssignAttri(sPrefix, false, "AV19restoreDate", context.localUtil.Format(AV19restoreDate, "99/99/99"));
            }
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
         E132L2 ();
         if (returnInSub) return;
      }

      protected void E132L2( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV25wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV25wallet = GXt_SdtWallet1;
         bttActivategroups_Visible = 0;
         AssignProp(sPrefix, false, bttActivategroups_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttActivategroups_Visible), 5, 0), true);
         AV14group_sdt.FromJSonString(AV26websession.Get("Group_EDIT"), null);
         GXt_SdtGroup_SDT_TimeConstrainItem2 = AV17oneTimeConstrain;
         new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV14group_sdt.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem2) ;
         AV17oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem2;
         AV19restoreDate = AV17oneTimeConstrain.gxTpr_Date;
         AssignAttri(sPrefix, false, "AV19restoreDate", context.localUtil.Format(AV19restoreDate, "99/99/99"));
         if ( ( AV14group_sdt.gxTpr_Grouptype == 20 ) && AV14group_sdt.gxTpr_Amigroupowner && (0==AV14group_sdt.gxTpr_Subgrouptype) )
         {
            AV7data_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
            AV7data_group_sdt.gxTpr_Groupname = StringUtil.Trim( AV14group_sdt.gxTpr_Groupname);
            AV7data_group_sdt.gxTpr_Amigroupowner = true;
            AV7data_group_sdt.gxTpr_Grouptype = 20;
            AV7data_group_sdt.gxTpr_Subgrouptype = 30;
            GXt_char3 = AV11error;
            new GeneXus.Programs.wallet.registered.creategroup(context ).execute(  AV7data_group_sdt, out  AV15grpupId, out  GXt_char3) ;
            AV11error = GXt_char3;
            GXt_char3 = AV8encryptionKey;
            new GeneXus.Programs.wallet.getlastjasonencritionkey(context ).execute( out  GXt_char3) ;
            AV8encryptionKey = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV7data_group_sdt.gxTpr_Groupid = AV15grpupId;
               AV7data_group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid = AV15grpupId;
               AV7data_group_sdt.gxTpr_Othergroup.gxTpr_Encpassword = AV8encryptionKey;
               GXt_char3 = AV11error;
               GXt_guid4 = AV7data_group_sdt.gxTpr_Groupid;
               new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV7data_group_sdt,  AV7data_group_sdt.gxTpr_Encpassword, out  GXt_guid4, out  GXt_char3) ;
               AV7data_group_sdt.gxTpr_Groupid = GXt_guid4;
               AV11error = GXt_char3;
               GXt_char3 = AV11error;
               new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV7data_group_sdt, out  GXt_char3) ;
               AV11error += GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV5bount_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
                  AV5bount_group_sdt.gxTpr_Groupname = StringUtil.Trim( AV14group_sdt.gxTpr_Groupname);
                  AV5bount_group_sdt.gxTpr_Amigroupowner = true;
                  AV5bount_group_sdt.gxTpr_Grouptype = 20;
                  AV5bount_group_sdt.gxTpr_Subgrouptype = 20;
                  GXt_char3 = AV11error;
                  new GeneXus.Programs.wallet.registered.creategroup(context ).execute(  AV5bount_group_sdt, out  AV15grpupId, out  GXt_char3) ;
                  AV11error = GXt_char3;
                  GXt_char3 = AV8encryptionKey;
                  new GeneXus.Programs.wallet.getlastjasonencritionkey(context ).execute( out  GXt_char3) ;
                  AV8encryptionKey = GXt_char3;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     AV5bount_group_sdt.gxTpr_Groupid = AV15grpupId;
                     AV5bount_group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid = AV15grpupId;
                     AV5bount_group_sdt.gxTpr_Othergroup.gxTpr_Encpassword = AV8encryptionKey;
                     GXt_char3 = AV11error;
                     GXt_guid4 = AV5bount_group_sdt.gxTpr_Groupid;
                     new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV5bount_group_sdt,  AV5bount_group_sdt.gxTpr_Encpassword, out  GXt_guid4, out  GXt_char3) ;
                     AV5bount_group_sdt.gxTpr_Groupid = GXt_guid4;
                     AV11error = GXt_char3;
                     GXt_char3 = AV11error;
                     new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV5bount_group_sdt, out  GXt_char3) ;
                     AV11error += GXt_char3;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        AV14group_sdt.gxTpr_Subgrouptype = 10;
                        AV14group_sdt.gxTpr_Bountygroupid = AV5bount_group_sdt.gxTpr_Groupid;
                        AV14group_sdt.gxTpr_Datagroupid = AV7data_group_sdt.gxTpr_Groupid;
                        AV14group_sdt.gxTpr_Othergroup.gxTpr_Encpassword = "";
                        GXt_char3 = AV11error;
                        new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV14group_sdt,  AV14group_sdt.gxTpr_Encpassword, out  AV15grpupId, out  GXt_char3) ;
                        AV11error = GXt_char3;
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                        {
                           GXt_char3 = AV11error;
                           new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV14group_sdt, out  GXt_char3) ;
                           AV11error = GXt_char3;
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                           {
                              AV26websession.Set("Group_EDIT", AV14group_sdt.ToJSonString(false, true));
                              CallWebObject(formatLink("wallet.registered.smartgroup", new object[] {UrlEncode(AV14group_sdt.gxTpr_Groupid.ToString())}, new string[] {"groupId"}) );
                              context.wjLocDisableFrm = 1;
                           }
                           else
                           {
                              GX_msglist.addItem(AV11error);
                           }
                        }
                        else
                        {
                           GX_msglist.addItem(AV11error);
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(AV11error);
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(AV11error);
                  }
               }
               else
               {
                  GX_msglist.addItem("We couldn't create Bounty Group: "+AV11error);
               }
            }
            else
            {
               GX_msglist.addItem("We couldn't create Data Group: "+AV11error);
            }
         }
         /* Object Property */
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            bDynCreated_Tabcomponent = true;
         }
         if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Tabcomponent_Component), StringUtil.Lower( "Wallet.registered.TimeWalletBackup")) != 0 )
         {
            WebComp_Tabcomponent = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.timewalletbackup", new Object[] {context} );
            WebComp_Tabcomponent.ComponentInit();
            WebComp_Tabcomponent.Name = "Wallet.registered.TimeWalletBackup";
            WebComp_Tabcomponent_Component = "Wallet.registered.TimeWalletBackup";
         }
         if ( StringUtil.Len( WebComp_Tabcomponent_Component) != 0 )
         {
            WebComp_Tabcomponent.setjustcreated();
            WebComp_Tabcomponent.componentprepare(new Object[] {(string)sPrefix+"W0028",(string)"",AV14group_sdt.gxTpr_Datagroupid});
            WebComp_Tabcomponent.componentbind(new Object[] {(string)""});
         }
         if ( new GeneXus.Programs.wallet.registered.isgroupreadytoactivate(context).executeUdp(  AV14group_sdt.gxTpr_Datagroupid) && new GeneXus.Programs.wallet.registered.isgroupreadytoactivate(context).executeUdp(  AV14group_sdt.gxTpr_Bountygroupid) )
         {
            bttActivategroups_Visible = 1;
            AssignProp(sPrefix, false, bttActivategroups_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttActivategroups_Visible), 5, 0), true);
         }
         if ( AV14group_sdt.gxTpr_Isactive )
         {
            edtavRestoredate_Enabled = 0;
            AssignProp(sPrefix, false, edtavRestoredate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRestoredate_Enabled), 5, 0), true);
            bttSave_Visible = 0;
            AssignProp(sPrefix, false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
            bttChangerestoredate_Visible = 1;
            AssignProp(sPrefix, false, bttChangerestoredate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttChangerestoredate_Visible), 5, 0), true);
            AV27wasActive = true;
            AssignAttri(sPrefix, false, "AV27wasActive", AV27wasActive);
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWASACTIVE", GetSecureSignedToken( sPrefix, AV27wasActive, context));
         }
         else
         {
            bttChangerestoredate_Visible = 0;
            AssignProp(sPrefix, false, bttChangerestoredate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttChangerestoredate_Visible), 5, 0), true);
            AV27wasActive = false;
            AssignAttri(sPrefix, false, "AV27wasActive", AV27wasActive);
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWASACTIVE", GetSecureSignedToken( sPrefix, AV27wasActive, context));
         }
      }

      protected void E142L2( )
      {
         /* 'Save' Routine */
         returnInSub = false;
         if ( (DateTime.MinValue==AV19restoreDate) )
         {
            GX_msglist.addItem("Please select the date when the backup became available to be restored.");
         }
         else
         {
            if ( DateTimeUtil.ResetTime ( AV19restoreDate ) <= DateTimeUtil.ResetTime ( Gx_date ) )
            {
               GX_msglist.addItem("The date must be in the future");
            }
            else
            {
               /* Execute user subroutine: 'SAVE GROUP' */
               S112 ();
               if (returnInSub) return;
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "SaveTimeWallet", new Object[] {}, true);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17oneTimeConstrain", AV17oneTimeConstrain);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14group_sdt", AV14group_sdt);
      }

      protected void E152L2( )
      {
         /* 'Close' Routine */
         returnInSub = false;
         AV26websession.Set("Group_EDIT_DATA", "");
         AV26websession.Set("Group_EDIT_BOUNTY", "");
         AV26websession.Set("Group_EDIT", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      protected void S112( )
      {
         /* 'SAVE GROUP' Routine */
         returnInSub = false;
         if ( ! AV14group_sdt.gxTpr_Isactive )
         {
            AV17oneTimeConstrain.gxTpr_Sequence = 0;
            AV17oneTimeConstrain.gxTpr_Address = "";
            AV17oneTimeConstrain.gxTpr_Date = AV19restoreDate;
            AV14group_sdt.gxTpr_Timeconstrain.Clear();
            AV14group_sdt.gxTpr_Timeconstrain.Add(AV17oneTimeConstrain, 0);
         }
         GXt_char3 = AV11error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV14group_sdt,  StringUtil.Trim( AV14group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV15grpupId, out  GXt_char3) ;
         AV11error = GXt_char3;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GXt_char3 = AV11error;
            new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV14group_sdt, out  GXt_char3) ;
            AV11error = GXt_char3;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               GX_msglist.addItem(AV11error);
            }
         }
         else
         {
            GX_msglist.addItem(AV11error);
         }
      }

      protected void E162L2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV12expectedPopupName = "Wallet.registered.ApproveRestoreDate";
         AV24strFound = (short)(StringUtil.StringSearch( AV18PopupName, StringUtil.Trim( StringUtil.Lower( AV12expectedPopupName)), 1));
         if ( AV24strFound > 0 )
         {
            GXt_SdtExtKeyInfo5 = AV13extKeyInfo;
            new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo5) ;
            AV13extKeyInfo = GXt_SdtExtKeyInfo5;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13extKeyInfo.gxTpr_Privatekey)) )
            {
               if ( AV14group_sdt.gxTpr_Isactive )
               {
                  AV14group_sdt.FromJSonString(AV26websession.Get("Group_EDIT"), null);
               }
               GXt_char3 = AV11error;
               new GeneXus.Programs.shamirss.createshares(context ).execute(  AV13extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot,  2,  2, out  AV22shares, ref  GXt_char3) ;
               AV11error = GXt_char3;
               AV9EncryptionResult = AV10EncryptionService.encrypt(((string)AV22shares.Item(2)), "");
               if ( AV9EncryptionResult.gxTpr_Success )
               {
                  GXt_char3 = AV11error;
                  new GeneXus.Programs.wallet.registered.sendactivationtimewalletbackup(context ).execute(  AV14group_sdt.gxTpr_Datagroupid,  ((string)AV22shares.Item(1)),  AV9EncryptionResult.gxTpr_Ciphertext, out  GXt_char3) ;
                  AV11error = GXt_char3;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     GXt_char3 = AV11error;
                     new GeneXus.Programs.wallet.registered.sendactivationtimewalletbounty(context ).execute(  AV14group_sdt.gxTpr_Bountygroupid,  AV9EncryptionResult.gxTpr_Generatedkey, out  GXt_char3) ;
                     AV11error = GXt_char3;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        GXt_SdtGroup_SDT6 = AV5bount_group_sdt;
                        new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14group_sdt.gxTpr_Bountygroupid, out  GXt_SdtGroup_SDT6) ;
                        AV5bount_group_sdt = GXt_SdtGroup_SDT6;
                        if ( ! AV14group_sdt.gxTpr_Isactive )
                        {
                           AV17oneTimeConstrain.gxTpr_Address = "";
                           AV17oneTimeConstrain.gxTpr_Sequence = 0;
                           AV17oneTimeConstrain.gxTpr_Date = AV19restoreDate;
                           AV14group_sdt.gxTpr_Timeconstrain.Clear();
                           AV14group_sdt.gxTpr_Timeconstrain.Add(AV17oneTimeConstrain, 0);
                        }
                        GXt_SdtGroup_SDT_TimeConstrainItem2 = AV17oneTimeConstrain;
                        new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV14group_sdt.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem2) ;
                        AV17oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem2;
                        AV16numAddressToGenerate = (short)(AV17oneTimeConstrain.gxTpr_Sequence+1);
                        AV5bount_group_sdt.gxTpr_Timeconstrain = (GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem>)(AV14group_sdt.gxTpr_Timeconstrain.Clone());
                        AV23storedTransactions.gxTpr_Transaction.Clear();
                        GXt_char3 = AV11error;
                        new GeneXus.Programs.wallet.registered.deriveaddressstimebounty(context ).execute(  AV23storedTransactions,  AV5bount_group_sdt,  AV25wallet.gxTpr_Networktype,  AV16numAddressToGenerate,  (short)(Math.Round(NumberUtil.Val( "4", "."), 18, MidpointRounding.ToEven)),  AV9EncryptionResult.gxTpr_Generatedkey, out  AV20sdt_addressess, out  GXt_char3) ;
                        AV11error = GXt_char3;
                        AV29GXV1 = 1;
                        while ( AV29GXV1 <= AV20sdt_addressess.Count )
                        {
                           AV21sdt_oneAddress = ((GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem)AV20sdt_addressess.Item(AV29GXV1));
                           if ( AV21sdt_oneAddress.gxTpr_Creationsequence == AV17oneTimeConstrain.gxTpr_Sequence )
                           {
                              AV17oneTimeConstrain.gxTpr_Address = StringUtil.Trim( AV21sdt_oneAddress.gxTpr_Address);
                              AV17oneTimeConstrain.gxTpr_Encryptedsecret = AV5bount_group_sdt.gxTpr_Encryptedtextshare;
                              AV17oneTimeConstrain.gxTpr_Encryptedkey = AV5bount_group_sdt.gxTpr_Encpassword;
                              if (true) break;
                           }
                           AV29GXV1 = (int)(AV29GXV1+1);
                        }
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV17oneTimeConstrain.gxTpr_Address)) )
                        {
                           AV14group_sdt.gxTpr_Timeconstrain.Add(AV17oneTimeConstrain, 0);
                           AV14group_sdt.gxTpr_Isactive = true;
                           GXt_char3 = AV11error;
                           new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV14group_sdt,  AV14group_sdt.gxTpr_Encpassword, out  AV15grpupId, out  GXt_char3) ;
                           AV11error = GXt_char3;
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                           {
                              GXt_char3 = AV11error;
                              new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV14group_sdt, out  GXt_char3) ;
                              AV11error = GXt_char3;
                              if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                              {
                                 AV26websession.Set("Group_EDIT", AV14group_sdt.ToJSonString(false, true));
                                 new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                                 GXt_SdtGroup_SDT6 = AV5bount_group_sdt;
                                 new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14group_sdt.gxTpr_Bountygroupid, out  GXt_SdtGroup_SDT6) ;
                                 AV5bount_group_sdt = GXt_SdtGroup_SDT6;
                                 AV5bount_group_sdt.gxTpr_Timeconstrain.Add(AV17oneTimeConstrain, 0);
                                 GXt_SdtGroup_SDT6 = AV7data_group_sdt;
                                 new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV14group_sdt.gxTpr_Datagroupid, out  GXt_SdtGroup_SDT6) ;
                                 AV7data_group_sdt = GXt_SdtGroup_SDT6;
                                 AV7data_group_sdt.gxTpr_Timeconstrain.Add(AV17oneTimeConstrain, 0);
                                 GXt_char3 = AV11error;
                                 GXt_guid4 = AV5bount_group_sdt.gxTpr_Groupid;
                                 new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV5bount_group_sdt,  AV5bount_group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  GXt_guid4, out  GXt_char3) ;
                                 AV5bount_group_sdt.gxTpr_Groupid = GXt_guid4;
                                 AV11error = GXt_char3;
                                 GXt_char3 = AV11error;
                                 new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV5bount_group_sdt, out  GXt_char3) ;
                                 AV11error += GXt_char3;
                                 GXt_char3 = AV11error;
                                 GXt_guid4 = AV7data_group_sdt.gxTpr_Groupid;
                                 new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV7data_group_sdt,  AV7data_group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  GXt_guid4, out  GXt_char3) ;
                                 AV7data_group_sdt.gxTpr_Groupid = GXt_guid4;
                                 AV11error += GXt_char3;
                                 GXt_char3 = AV11error;
                                 new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV7data_group_sdt, out  GXt_char3) ;
                                 AV11error += GXt_char3;
                                 if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                                 {
                                    if ( AV27wasActive )
                                    {
                                       this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Groups Update",(string)"All groups have been updated"}, true);
                                    }
                                    else
                                    {
                                       this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Groups Activation",(string)"All notifications sent"}, true);
                                    }
                                    CallWebObject(formatLink("wallet.registered.smartgroup", new object[] {UrlEncode(AV14group_sdt.gxTpr_Groupid.ToString())}, new string[] {"groupId"}) );
                                    context.wjLocDisableFrm = 1;
                                 }
                                 else
                                 {
                                    GX_msglist.addItem(AV11error);
                                 }
                              }
                              else
                              {
                                 GX_msglist.addItem(AV11error);
                              }
                           }
                           else
                           {
                              GX_msglist.addItem(AV11error);
                           }
                           new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                        }
                        else
                        {
                           GX_msglist.addItem(AV11error);
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(AV11error);
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(AV11error);
                  }
               }
               else
               {
                  AV11error = "There was approblem encrypting the second share";
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14group_sdt", AV14group_sdt);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17oneTimeConstrain", AV17oneTimeConstrain);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV23storedTransactions", AV23storedTransactions);
      }

      protected void nextLoad( )
      {
      }

      protected void E172L2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
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
         PA2L2( ) ;
         WS2L2( ) ;
         WE2L2( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA2L2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\timewalletconfig", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA2L2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
         }
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PA2L2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS2L2( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WS2L2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WE2L2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
         if ( ! ( WebComp_Tabcomponent == null ) )
         {
            WebComp_Tabcomponent.componentjscripts();
         }
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("Tab/BasicTab.css", "");
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         if ( ! ( WebComp_Tabcomponent == null ) )
         {
            if ( StringUtil.Len( WebComp_Tabcomponent_Component) != 0 )
            {
               WebComp_Tabcomponent.componentthemes();
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202573011554741", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("wallet/registered/timewalletconfig.js", "?202573011554742", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
         context.AddJavascriptSource("Tab/BasicTabRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavRestoredate_Internalname = sPrefix+"vRESTOREDATE";
         bttChangerestoredate_Internalname = sPrefix+"CHANGERESTOREDATE";
         divTable1_Internalname = sPrefix+"TABLE1";
         lblResotregroup_title_Internalname = sPrefix+"RESOTREGROUP_TITLE";
         divTabpage1table_Internalname = sPrefix+"TABPAGE1TABLE";
         lblBountygroup_title_Internalname = sPrefix+"BOUNTYGROUP_TITLE";
         divTabpage2table_Internalname = sPrefix+"TABPAGE2TABLE";
         Tabs_Internalname = sPrefix+"TABS";
         bttSave_Internalname = sPrefix+"SAVE";
         bttClose_Internalname = sPrefix+"CLOSE";
         bttActivategroups_Internalname = sPrefix+"ACTIVATEGROUPS";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         bttActivategroups_Visible = 1;
         bttSave_Visible = 1;
         bttChangerestoredate_Visible = 1;
         edtavRestoredate_Jsonclick = "";
         edtavRestoredate_Enabled = 1;
         Tabs_Historymanagement = Convert.ToBoolean( 0);
         Tabs_Class = "Tab";
         Tabs_Pagecount = 2;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"Gx_date","fld":"vTODAY","hsh":true,"type":"date"},{"av":"AV25wallet","fld":"vWALLET","hsh":true,"type":""},{"av":"AV27wasActive","fld":"vWASACTIVE","hsh":true,"type":"boolean"}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E142L2","iparms":[{"av":"AV19restoreDate","fld":"vRESTOREDATE","type":"date"},{"av":"Gx_date","fld":"vTODAY","hsh":true,"type":"date"},{"av":"AV14group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV17oneTimeConstrain","fld":"vONETIMECONSTRAIN","type":""}]""");
         setEventMetadata("'SAVE'",""","oparms":[{"av":"AV17oneTimeConstrain","fld":"vONETIMECONSTRAIN","type":""},{"av":"AV14group_sdt","fld":"vGROUP_SDT","type":""}]}""");
         setEventMetadata("'CLOSE'","""{"handler":"E152L2","iparms":[]}""");
         setEventMetadata("'ACTIVATE GROUPS'","""{"handler":"E122L1","iparms":[]}""");
         setEventMetadata("'CHANGE RESTORE DATE'","""{"handler":"E112L1","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E162L2","iparms":[{"av":"AV18PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV14group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV17oneTimeConstrain","fld":"vONETIMECONSTRAIN","type":""},{"av":"AV19restoreDate","fld":"vRESTOREDATE","type":"date"},{"av":"AV23storedTransactions","fld":"vSTOREDTRANSACTIONS","type":""},{"av":"AV25wallet","fld":"vWALLET","hsh":true,"type":""},{"av":"AV27wasActive","fld":"vWASACTIVE","hsh":true,"type":"boolean"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV14group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV17oneTimeConstrain","fld":"vONETIMECONSTRAIN","type":""},{"av":"AV23storedTransactions","fld":"vSTOREDTRANSACTIONS","type":""}]}""");
         setEventMetadata("VALIDV_RESTOREDATE","""{"handler":"Validv_Restoredate","iparms":[]}""");
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
         Tabs_Activepagecontrolname = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         Gx_date = DateTime.MinValue;
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         AV25wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXKey = "";
         AV14group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV17oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV18PopupName = "";
         AV23storedTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV6componentName = "";
         GX_FocusControl = "";
         TempTags = "";
         AV19restoreDate = DateTime.MinValue;
         ClassString = "";
         StyleString = "";
         bttChangerestoredate_Jsonclick = "";
         ucTabs = new GXUserControl();
         lblResotregroup_title_Jsonclick = "";
         lblBountygroup_title_Jsonclick = "";
         WebComp_Tabcomponent_Component = "";
         OldTabcomponent = "";
         bttSave_Jsonclick = "";
         bttClose_Jsonclick = "";
         bttActivategroups_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV26websession = context.GetSession();
         AV7data_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11error = "";
         AV15grpupId = Guid.Empty;
         AV8encryptionKey = "";
         AV5bount_group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12expectedPopupName = "";
         AV13extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo5 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV22shares = new GxSimpleCollection<string>();
         AV9EncryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult(context);
         AV10EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         GXt_SdtGroup_SDT_TimeConstrainItem2 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV20sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_AddressessItem", "distributedcryptography");
         AV21sdt_oneAddress = new GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem(context);
         GXt_SdtGroup_SDT6 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_guid4 = Guid.Empty;
         GXt_char3 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         WebComp_Tabcomponent = new GeneXus.Http.GXNullWebComponent();
         Gx_date = DateTimeUtil.Today( context);
         /* GeneXus formulas. */
         Gx_date = DateTimeUtil.Today( context);
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short AV24strFound ;
      private short AV16numAddressToGenerate ;
      private short nGXWrapped ;
      private int Tabs_Pagecount ;
      private int edtavRestoredate_Enabled ;
      private int bttChangerestoredate_Visible ;
      private int bttSave_Visible ;
      private int bttActivategroups_Visible ;
      private int AV29GXV1 ;
      private int idxLst ;
      private string Tabs_Activepagecontrolname ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV18PopupName ;
      private string AV6componentName ;
      private string Tabs_Class ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string divTable1_Internalname ;
      private string edtavRestoredate_Internalname ;
      private string TempTags ;
      private string edtavRestoredate_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string bttChangerestoredate_Internalname ;
      private string bttChangerestoredate_Jsonclick ;
      private string Tabs_Internalname ;
      private string lblResotregroup_title_Internalname ;
      private string lblResotregroup_title_Jsonclick ;
      private string divTabpage1table_Internalname ;
      private string lblBountygroup_title_Internalname ;
      private string lblBountygroup_title_Jsonclick ;
      private string divTabpage2table_Internalname ;
      private string WebComp_Tabcomponent_Component ;
      private string OldTabcomponent ;
      private string bttSave_Internalname ;
      private string bttSave_Jsonclick ;
      private string bttClose_Internalname ;
      private string bttClose_Jsonclick ;
      private string bttActivategroups_Internalname ;
      private string bttActivategroups_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV11error ;
      private string AV8encryptionKey ;
      private string AV12expectedPopupName ;
      private string GXt_char3 ;
      private DateTime Gx_date ;
      private DateTime AV19restoreDate ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV27wasActive ;
      private bool Tabs_Historymanagement ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool bDynCreated_Tabcomponent ;
      private Guid AV15grpupId ;
      private Guid GXt_guid4 ;
      private IGxSession AV26websession ;
      private GXWebComponent WebComp_Tabcomponent ;
      private GXUserControl ucTabs ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.SdtWallet AV25wallet ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV14group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV17oneTimeConstrain ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV23storedTransactions ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV7data_group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV5bount_group_sdt ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV13extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo5 ;
      private GxSimpleCollection<string> AV22shares ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionResult AV9EncryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV10EncryptionService ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem GXt_SdtGroup_SDT_TimeConstrainItem2 ;
      private GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> AV20sdt_addressess ;
      private GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem AV21sdt_oneAddress ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT6 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
