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
            PA2S2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               WS2S2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true, false);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true, false);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true, false);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true, false);
         context.AddJavascriptSource("Tab/BasicTabRender.js", "", false, true, false);
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
         GxWebStd.gx_hidden_field( context, sPrefix+"vDATAGROUPID", AV111dataGroupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDATAGROUPID", GetSecureSignedToken( sPrefix, AV111dataGroupId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vBOUNTYGROUPID", AV108bountyGroupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vBOUNTYGROUPID", GetSecureSignedToken( sPrefix, AV108bountyGroupId, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUPVIEW", AV112groupView);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUPVIEW", AV112groupView);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vGROUPVIEW", GetSecureSignedToken( sPrefix, AV112groupView, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, sPrefix+"vDATAGROUPID", AV111dataGroupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDATAGROUPID", GetSecureSignedToken( sPrefix, AV111dataGroupId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vBOUNTYGROUPID", AV108bountyGroupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vBOUNTYGROUPID", GetSecureSignedToken( sPrefix, AV108bountyGroupId, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUPVIEW", AV112groupView);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUPVIEW", AV112groupView);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vGROUPVIEW", GetSecureSignedToken( sPrefix, AV112groupView, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV22PopupName));
         GxWebStd.gx_hidden_field( context, sPrefix+"vCOMPONENTNAME", StringUtil.RTrim( AV15componentName));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABS_Pagecount", StringUtil.LTrim( StringUtil.NToC( (decimal)(Tabs_Pagecount), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABS_Class", StringUtil.RTrim( Tabs_Class));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABS_Historymanagement", StringUtil.BoolToStr( Tabs_Historymanagement));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABS_Activepagecontrolname", StringUtil.RTrim( Tabs_Activepagecontrolname));
      }

      protected void RenderHtmlCloseForm2S2( )
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

      protected void WB2S0( )
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
               context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true, false);
               context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true, false);
               context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true, false);
               context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true, false);
               context.AddJavascriptSource("Tab/BasicTabRender.js", "", false, true, false);
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
            GxWebStd.gx_single_line_edit( context, edtavRestoredate_Internalname, context.localUtil.Format(AV16restoreDate, "99/99/99"), context.localUtil.Format( AV16restoreDate, "99/99/99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',0,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',0,12,'eng',false,0);"+";gx.evt.onblur(this,10);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavRestoredate_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavRestoredate_Enabled, 1, "text", "", 8, "chr", 1, "row", 8, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/registered/TimeWalletConfig.htm");
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
            GxWebStd.gx_button_ctrl( context, bttChangerestoredate_Internalname, "", "Change Restore Date", bttChangerestoredate_Jsonclick, 7, "Change Restore Date", "", StyleString, ClassString, bttChangerestoredate_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e112s1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/TimeWalletConfig.htm");
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
            GxWebStd.gx_button_ctrl( context, bttActivategroups_Internalname, "", "Activate Groups", bttActivategroups_Jsonclick, 7, "Activate Groups", "", StyleString, ClassString, bttActivategroups_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e122s1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/TimeWalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START2S2( )
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
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
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
               STRUP2S0( ) ;
            }
         }
      }

      protected void WS2S2( )
      {
         START2S2( ) ;
         EVT2S2( ) ;
      }

      protected void EVT2S2( )
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
                                 STRUP2S0( ) ;
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
                                 STRUP2S0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E132S2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2S0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Save' */
                                    E142S2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CLOSE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2S0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Close' */
                                    E152S2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2S0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E162S2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2S0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Load */
                                    E172S2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2S0( ) ;
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
                                 STRUP2S0( ) ;
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

      protected void WE2S2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm2S2( ) ;
            }
         }
      }

      protected void PA2S2( )
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
         RF2S2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF2S2( )
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
            E172S2 ();
            WB2S0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes2S2( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vDATAGROUPID", AV111dataGroupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDATAGROUPID", GetSecureSignedToken( sPrefix, AV111dataGroupId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vBOUNTYGROUPID", AV108bountyGroupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vBOUNTYGROUPID", GetSecureSignedToken( sPrefix, AV108bountyGroupId, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUPVIEW", AV112groupView);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUPVIEW", AV112groupView);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vGROUPVIEW", GetSecureSignedToken( sPrefix, AV112groupView, context));
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2S0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E132S2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            AV108bountyGroupId = StringUtil.StrToGuid( cgiGet( sPrefix+"vBOUNTYGROUPID"));
            AssignAttri(sPrefix, false, "AV108bountyGroupId", AV108bountyGroupId.ToString());
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vBOUNTYGROUPID", GetSecureSignedToken( sPrefix, AV108bountyGroupId, context));
            AV111dataGroupId = StringUtil.StrToGuid( cgiGet( sPrefix+"vDATAGROUPID"));
            AssignAttri(sPrefix, false, "AV111dataGroupId", AV111dataGroupId.ToString());
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDATAGROUPID", GetSecureSignedToken( sPrefix, AV111dataGroupId, context));
            AV15componentName = cgiGet( sPrefix+"vCOMPONENTNAME");
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
               AV16restoreDate = DateTime.MinValue;
               AssignAttri(sPrefix, false, "AV16restoreDate", context.localUtil.Format(AV16restoreDate, "99/99/99"));
            }
            else
            {
               AV16restoreDate = context.localUtil.CToD( cgiGet( edtavRestoredate_Internalname), 1);
               AssignAttri(sPrefix, false, "AV16restoreDate", context.localUtil.Format(AV16restoreDate, "99/99/99"));
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
         E132S2 ();
         if (returnInSub) return;
      }

      protected void E132S2( )
      {
         /* Start Routine */
         returnInSub = false;
         bttActivategroups_Visible = 0;
         AssignProp(sPrefix, false, bttActivategroups_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttActivategroups_Visible), 5, 0), true);
         GXt_SdtGroupListItem1 = AV112groupView;
         new GeneXus.Programs.wallet.registered.getgroupeditview(context ).execute( out  GXt_SdtGroupListItem1) ;
         AV112groupView = GXt_SdtGroupListItem1;
         GXt_char2 = AV9error;
         new GeneXus.Programs.wallet.registered.preparetimevault(context ).execute(  AV112groupView.gxTpr_Groupid, out  AV110created, out  AV111dataGroupId, out  AV108bountyGroupId, out  AV106wasActive, out  AV16restoreDate, out  AV109canActivate, out  GXt_char2) ;
         AssignAttri(sPrefix, false, "AV111dataGroupId", AV111dataGroupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDATAGROUPID", GetSecureSignedToken( sPrefix, AV111dataGroupId, context));
         AssignAttri(sPrefix, false, "AV108bountyGroupId", AV108bountyGroupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vBOUNTYGROUPID", GetSecureSignedToken( sPrefix, AV108bountyGroupId, context));
         AssignAttri(sPrefix, false, "AV16restoreDate", context.localUtil.Format(AV16restoreDate, "99/99/99"));
         AV9error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GX_msglist.addItem(AV9error);
         }
         else
         {
            if ( AV110created )
            {
               new GeneXus.Programs.wallet.registered.setgroupedit(context ).execute(  AV112groupView.gxTpr_Groupid,  AV112groupView.gxTpr_Referencegroupid) ;
               CallWebObject(formatLink("wallet.registered.smartgroup", new object[] {UrlEncode(AV112groupView.gxTpr_Groupid.ToString())}, new string[] {"groupId"}) );
               context.wjLocDisableFrm = 1;
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
            WebComp_Tabcomponent.componentprepare(new Object[] {(string)sPrefix+"W0028",(string)"",(Guid)AV111dataGroupId});
            WebComp_Tabcomponent.componentbind(new Object[] {(string)""});
         }
         bttActivategroups_Visible = (AV109canActivate ? 1 : 0);
         AssignProp(sPrefix, false, bttActivategroups_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttActivategroups_Visible), 5, 0), true);
         if ( AV106wasActive )
         {
            edtavRestoredate_Enabled = 0;
            AssignProp(sPrefix, false, edtavRestoredate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRestoredate_Enabled), 5, 0), true);
            bttSave_Visible = 0;
            AssignProp(sPrefix, false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
            bttChangerestoredate_Visible = 1;
            AssignProp(sPrefix, false, bttChangerestoredate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttChangerestoredate_Visible), 5, 0), true);
         }
         else
         {
            bttChangerestoredate_Visible = 0;
            AssignProp(sPrefix, false, bttChangerestoredate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttChangerestoredate_Visible), 5, 0), true);
         }
      }

      protected void E142S2( )
      {
         /* 'Save' Routine */
         returnInSub = false;
         GXt_char2 = AV9error;
         new GeneXus.Programs.wallet.registered.savetimevaultdate(context ).execute(  AV112groupView.gxTpr_Groupid,  AV16restoreDate, out  GXt_char2) ;
         AV9error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "SaveTimeWallet", new Object[] {}, true);
         }
         else
         {
            GX_msglist.addItem(AV9error);
         }
      }

      protected void E152S2( )
      {
         /* 'Close' Routine */
         returnInSub = false;
         AV10websession.Set("Group_EDIT_DATA", "");
         AV10websession.Set("Group_EDIT_BOUNTY", "");
         AV10websession.Set("Group_EDIT", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      protected void E162S2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV31expectedPopupName = "Wallet.registered.ApproveRestoreDate";
         AV54strFound = (short)(StringUtil.StringSearch( AV22PopupName, StringUtil.Trim( StringUtil.Lower( AV31expectedPopupName)), 1));
         if ( AV54strFound > 0 )
         {
            AV107activationResult = AV10websession.Get("TEV_ACTIVATION_RESULT");
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV107activationResult)) )
            {
               AV10websession.Set("TEV_ACTIVATION_RESULT", "");
               new GeneXus.Programs.wallet.registered.setgroupedit(context ).execute(  AV112groupView.gxTpr_Groupid,  AV112groupView.gxTpr_Referencegroupid) ;
               if ( StringUtil.StrCmp(AV107activationResult, "updated") == 0 )
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Groups Update",(string)"All groups have been updated"}, true);
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Groups Activation",(string)"All notifications sent"}, true);
               }
               CallWebObject(formatLink("wallet.registered.smartgroup", new object[] {UrlEncode(AV112groupView.gxTpr_Groupid.ToString())}, new string[] {"groupId"}) );
               context.wjLocDisableFrm = 1;
            }
         }
         /*  Sending Event outputs  */
      }

      protected void nextLoad( )
      {
      }

      protected void E172S2( )
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
         PA2S2( ) ;
         WS2S2( ) ;
         WE2S2( ) ;
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
         PA2S2( ) ;
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
         PA2S2( ) ;
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
         PA2S2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS2S2( ) ;
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
         WS2S2( ) ;
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
         WE2S2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610714151829", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/timewalletconfig.js", "?202610714151829", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true, false);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true, false);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true, false);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true, false);
         context.AddJavascriptSource("Tab/BasicTabRender.js", "", false, true, false);
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV111dataGroupId","fld":"vDATAGROUPID","hsh":true,"type":"guid"},{"av":"AV108bountyGroupId","fld":"vBOUNTYGROUPID","hsh":true,"type":"guid"},{"av":"AV112groupView","fld":"vGROUPVIEW","hsh":true,"type":""}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E142S2","iparms":[{"av":"AV112groupView","fld":"vGROUPVIEW","hsh":true,"type":""},{"av":"AV16restoreDate","fld":"vRESTOREDATE","type":"date"}]}""");
         setEventMetadata("'CLOSE'","""{"handler":"E152S2","iparms":[]}""");
         setEventMetadata("'ACTIVATE GROUPS'","""{"handler":"E122S1","iparms":[]}""");
         setEventMetadata("'CHANGE RESTORE DATE'","""{"handler":"E112S1","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E162S2","iparms":[{"av":"AV22PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV112groupView","fld":"vGROUPVIEW","hsh":true,"type":""}]}""");
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
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         AV111dataGroupId = Guid.Empty;
         AV108bountyGroupId = Guid.Empty;
         AV112groupView = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         GXKey = "";
         AV22PopupName = "";
         AV15componentName = "";
         GX_FocusControl = "";
         TempTags = "";
         AV16restoreDate = DateTime.MinValue;
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
         GXt_SdtGroupListItem1 = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         AV9error = "";
         GXt_char2 = "";
         AV10websession = context.GetSession();
         AV31expectedPopupName = "";
         AV107activationResult = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         WebComp_Tabcomponent = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
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
      private short AV54strFound ;
      private short nGXWrapped ;
      private int Tabs_Pagecount ;
      private int edtavRestoredate_Enabled ;
      private int bttChangerestoredate_Visible ;
      private int bttSave_Visible ;
      private int bttActivategroups_Visible ;
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
      private string AV22PopupName ;
      private string AV15componentName ;
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
      private string AV9error ;
      private string GXt_char2 ;
      private string AV31expectedPopupName ;
      private string AV107activationResult ;
      private DateTime AV16restoreDate ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool Tabs_Historymanagement ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV110created ;
      private bool AV106wasActive ;
      private bool AV109canActivate ;
      private bool bDynCreated_Tabcomponent ;
      private Guid AV111dataGroupId ;
      private Guid AV108bountyGroupId ;
      private GXWebComponent WebComp_Tabcomponent ;
      private GXUserControl ucTabs ;
      private GXWebForm Form ;
      private IGxSession AV10websession ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem AV112groupView ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem GXt_SdtGroupListItem1 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
