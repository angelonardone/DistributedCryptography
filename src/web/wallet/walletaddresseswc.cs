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
namespace GeneXus.Programs.wallet {
   public class walletaddresseswc : GXWebComponent
   {
      public walletaddresseswc( )
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

      public walletaddresseswc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId )
      {
         this.AV20groupId = aP0_groupId;
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
         cmbavChainfilter = new GXCombobox();
         cmbavStatusfilter = new GXCombobox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
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
                  AV20groupId = StringUtil.StrToGuid( GetPar( "groupId"));
                  AssignAttri(sPrefix, false, "AV20groupId", AV20groupId.ToString());
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(Guid)AV20groupId});
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Addressesgrid") == 0 )
               {
                  gxnrAddressesgrid_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Addressesgrid") == 0 )
               {
                  gxgrAddressesgrid_refresh_invoke( ) ;
                  return  ;
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

      protected void gxnrAddressesgrid_newrow_invoke( )
      {
         nRC_GXsfl_30 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_30"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_30_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_30_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_30_idx = GetPar( "sGXsfl_30_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrAddressesgrid_newrow( ) ;
         /* End function gxnrAddressesgrid_newrow_invoke */
      }

      protected void gxgrAddressesgrid_refresh_invoke( )
      {
         subAddressesgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subAddressesgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         AV20groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         cmbavChainfilter.FromJSonString( GetNextPar( ));
         AV12chainFilter = (short)(Math.Round(NumberUtil.Val( GetPar( "chainFilter"), "."), 18, MidpointRounding.ToEven));
         cmbavStatusfilter.FromJSonString( GetNextPar( ));
         AV17statusFilter = GetPar( "statusFilter");
         ajax_req_read_hidden_sdt(GetNextPar( ), AV6addresses);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrAddressesgrid_refresh_invoke */
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
            PA3L2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavShownaddress_Enabled = 0;
               AssignProp(sPrefix, false, edtavShownaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavShownaddress_Enabled), 5, 0), true);
               edtavCtladdressindex_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtladdressindex_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtladdressindex_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               edtavGridstatus_Enabled = 0;
               AssignProp(sPrefix, false, edtavGridstatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGridstatus_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               edtavGridaddress_Enabled = 0;
               AssignProp(sPrefix, false, edtavGridaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGridaddress_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               edtavCtlpath_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlpath_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlpath_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               edtavCtllabel_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtllabel_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtllabel_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               edtavCtlbalance_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlbalance_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               edtavCtlreceived_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlreceived_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlreceived_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               edtavCtltransactions_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtltransactions_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtltransactions_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               edtavCtlstatus_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlstatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlstatus_Enabled), 5, 0), !bGXsfl_30_Refreshing);
               WS3L2( ) ;
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
            context.SendWebValue( "Addresses of the wallet") ;
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.walletaddresseswc", new object[] {UrlEncode(AV20groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Addresses", AV6addresses);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Addresses", AV6addresses);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_30", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_30), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV20groupId", wcpOAV20groupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV20groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vADDRESSES", AV6addresses);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vADDRESSES", AV6addresses);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSESGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSESGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSESGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSESGRID_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm3L2( )
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
         return "Wallet.WalletAddressesWC" ;
      }

      public override string GetPgmdesc( )
      {
         return "Addresses of the wallet" ;
      }

      protected void WB3L0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.walletaddresseswc");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavChainfilter_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavChainfilter_Internalname, "Chain", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavChainfilter, cmbavChainfilter_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV12chainFilter), 4, 0)), 1, cmbavChainfilter_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "int", "", 1, cmbavChainfilter.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", "", true, 0, "HLP_Wallet/WalletAddressesWC.htm");
            cmbavChainfilter.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV12chainFilter), 4, 0));
            AssignProp(sPrefix, false, cmbavChainfilter_Internalname, "Values", (string)(cmbavChainfilter.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavStatusfilter_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavStatusfilter_Internalname, "Show", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavStatusfilter, cmbavStatusfilter_Internalname, StringUtil.RTrim( AV17statusFilter), 1, cmbavStatusfilter_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavStatusfilter.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,12);\"", "", true, 0, "HLP_Wallet/WalletAddressesWC.htm");
            cmbavStatusfilter.CurrentValue = StringUtil.RTrim( AV17statusFilter);
            AssignProp(sPrefix, false, cmbavStatusfilter_Internalname, "Values", (string)(cmbavStatusfilter.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttReload_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(30), 2, 0)+","+"null"+");", "Reload", bttReload_Jsonclick, 5, "Reload", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'RELOAD\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/WalletAddressesWC.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbsummary_Internalname, lblTbsummary_Caption, "", "", lblTbsummary_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/WalletAddressesWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbaddressesinfo_Internalname, "> NEXT = the address the wallet offers now. Reserved = held by a pending payment request. Click an address to see its QR. The list follows the last balance check (Rescan Balance asks for every address again).", "", "", lblTbaddressesinfo_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/WalletAddressesWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavShownaddress_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavShownaddress_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavShownaddress_Internalname, "Address", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavShownaddress_Internalname, StringUtil.RTrim( AV9shownAddress), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,25);\"", 0, edtavShownaddress_Visible, edtavShownaddress_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/WalletAddressesWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbaddressqr_Internalname, lblTbaddressqr_Caption, "", "", lblTbaddressqr_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_Wallet/WalletAddressesWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            AddressesgridContainer.SetWrapped(nGXWrapped);
            StartGridControl30( ) ;
         }
         if ( wbEnd == 30 )
         {
            wbEnd = 0;
            nRC_GXsfl_30 = (int)(nGXsfl_30_idx-1);
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AddressesgridContainer.AddObjectProperty("ADDRESSESGRID_nEOF", ADDRESSESGRID_nEOF);
               AddressesgridContainer.AddObjectProperty("ADDRESSESGRID_nFirstRecordOnPage", ADDRESSESGRID_nFirstRecordOnPage);
               AV21GXV1 = nGXsfl_30_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"AddressesgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Addressesgrid", AddressesgridContainer, subAddressesgrid_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"AddressesgridContainerData", AddressesgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"AddressesgridContainerData"+"V", AddressesgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"AddressesgridContainerData"+"V"+"\" value='"+AddressesgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 30 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( AddressesgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AddressesgridContainer.AddObjectProperty("ADDRESSESGRID_nEOF", ADDRESSESGRID_nEOF);
                  AddressesgridContainer.AddObjectProperty("ADDRESSESGRID_nFirstRecordOnPage", ADDRESSESGRID_nFirstRecordOnPage);
                  AV21GXV1 = nGXsfl_30_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"AddressesgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Addressesgrid", AddressesgridContainer, subAddressesgrid_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"AddressesgridContainerData", AddressesgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"AddressesgridContainerData"+"V", AddressesgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"AddressesgridContainerData"+"V"+"\" value='"+AddressesgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3L2( )
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
            Form.Meta.addItem("description", "Addresses of the wallet", 0) ;
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
               STRUP3L0( ) ;
            }
         }
      }

      protected void WS3L2( )
      {
         START3L2( ) ;
         EVT3L2( ) ;
      }

      protected void EVT3L2( )
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
                                 STRUP3L0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "VCHAINFILTER.CONTROLVALUECHANGED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E113L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VSTATUSFILTER.CONTROLVALUECHANGED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E123L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'RELOAD'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Reload' */
                                    E133L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtladdressindex_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ADDRESSESGRIDPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3L0( ) ;
                              }
                              sEvt = cgiGet( sPrefix+"ADDRESSESGRIDPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subaddressesgrid_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subaddressesgrid_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subaddressesgrid_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subaddressesgrid_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "ADDRESSESGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'SHOW ADDRESS'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'SHOW ADDRESS'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3L0( ) ;
                              }
                              nGXsfl_30_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
                              SubsflControlProps_302( ) ;
                              AV21GXV1 = (int)(nGXsfl_30_idx+ADDRESSESGRID_nFirstRecordOnPage);
                              if ( ( AV6addresses.Count >= AV21GXV1 ) && ( AV21GXV1 > 0 ) )
                              {
                                 AV6addresses.CurrentItem = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1));
                                 AV13gridStatus = cgiGet( edtavGridstatus_Internalname);
                                 AssignAttri(sPrefix, false, edtavGridstatus_Internalname, AV13gridStatus);
                                 AV8gridAddress = cgiGet( edtavGridaddress_Internalname);
                                 AssignAttri(sPrefix, false, edtavGridaddress_Internalname, AV8gridAddress);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtladdressindex_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E143L2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtladdressindex_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E153L2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ADDRESSESGRID.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtladdressindex_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Addressesgrid.Load */
                                          E163L2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'SHOW ADDRESS'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtladdressindex_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Show Address' */
                                          E173L2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
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
                                       STRUP3L0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtladdressindex_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE3L2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3L2( ) ;
            }
         }
      }

      protected void PA3L2( )
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
               GX_FocusControl = cmbavChainfilter_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrAddressesgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_302( ) ;
         while ( nGXsfl_30_idx <= nRC_GXsfl_30 )
         {
            sendrow_302( ) ;
            nGXsfl_30_idx = ((subAddressesgrid_Islastpage==1)&&(nGXsfl_30_idx+1>subAddressesgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_30_idx+1);
            sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
            SubsflControlProps_302( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( AddressesgridContainer)) ;
         /* End function gxnrAddressesgrid_newrow */
      }

      protected void gxgrAddressesgrid_refresh( int subAddressesgrid_Rows ,
                                                Guid AV20groupId ,
                                                short AV12chainFilter ,
                                                string AV17statusFilter ,
                                                GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> AV6addresses ,
                                                string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         ADDRESSESGRID_nCurrentRecord = 0;
         RF3L2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrAddressesgrid_refresh */
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
         if ( cmbavChainfilter.ItemCount > 0 )
         {
            AV12chainFilter = (short)(Math.Round(NumberUtil.Val( cmbavChainfilter.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV12chainFilter), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV12chainFilter", StringUtil.LTrimStr( (decimal)(AV12chainFilter), 4, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavChainfilter.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV12chainFilter), 4, 0));
            AssignProp(sPrefix, false, cmbavChainfilter_Internalname, "Values", cmbavChainfilter.ToJavascriptSource(), true);
         }
         if ( cmbavStatusfilter.ItemCount > 0 )
         {
            AV17statusFilter = cmbavStatusfilter.getValidValue(AV17statusFilter);
            AssignAttri(sPrefix, false, "AV17statusFilter", AV17statusFilter);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavStatusfilter.CurrentValue = StringUtil.RTrim( AV17statusFilter);
            AssignProp(sPrefix, false, cmbavStatusfilter_Internalname, "Values", cmbavStatusfilter.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3L2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavShownaddress_Enabled = 0;
         AssignProp(sPrefix, false, edtavShownaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavShownaddress_Enabled), 5, 0), true);
         edtavCtladdressindex_Enabled = 0;
         edtavGridstatus_Enabled = 0;
         edtavGridaddress_Enabled = 0;
         edtavCtlpath_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavCtlreceived_Enabled = 0;
         edtavCtltransactions_Enabled = 0;
         edtavCtlstatus_Enabled = 0;
      }

      protected void RF3L2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            AddressesgridContainer.ClearRows();
         }
         wbStart = 30;
         /* Execute user event: Refresh */
         E153L2 ();
         nGXsfl_30_idx = 1;
         sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
         SubsflControlProps_302( ) ;
         bGXsfl_30_Refreshing = true;
         AddressesgridContainer.AddObjectProperty("GridName", "Addressesgrid");
         AddressesgridContainer.AddObjectProperty("CmpContext", sPrefix);
         AddressesgridContainer.AddObjectProperty("InMasterPage", "false");
         AddressesgridContainer.AddObjectProperty("Class", "Grid");
         AddressesgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         AddressesgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         AddressesgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Backcolorstyle), 1, 0, ".", "")));
         AddressesgridContainer.PageSize = subAddressesgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_302( ) ;
            /* Execute user event: Addressesgrid.Load */
            E163L2 ();
            if ( ( subAddressesgrid_Islastpage == 0 ) && ( ADDRESSESGRID_nCurrentRecord > 0 ) && ( ADDRESSESGRID_nGridOutOfScope == 0 ) && ( nGXsfl_30_idx == 1 ) )
            {
               ADDRESSESGRID_nCurrentRecord = 0;
               ADDRESSESGRID_nGridOutOfScope = 1;
               subaddressesgrid_firstpage( ) ;
               /* Execute user event: Addressesgrid.Load */
               E163L2 ();
            }
            wbEnd = 30;
            WB3L0( ) ;
         }
         bGXsfl_30_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3L2( )
      {
      }

      protected int subAddressesgrid_fnc_Pagecount( )
      {
         ADDRESSESGRID_nRecordCount = subAddressesgrid_fnc_Recordcount( );
         if ( ((long)((ADDRESSESGRID_nRecordCount) % (subAddressesgrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(ADDRESSESGRID_nRecordCount/ (decimal)(subAddressesgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(ADDRESSESGRID_nRecordCount/ (decimal)(subAddressesgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subAddressesgrid_fnc_Recordcount( )
      {
         return AV6addresses.Count ;
      }

      protected int subAddressesgrid_fnc_Recordsperpage( )
      {
         return (int)(100*1) ;
      }

      protected int subAddressesgrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(ADDRESSESGRID_nFirstRecordOnPage/ (decimal)(subAddressesgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subaddressesgrid_firstpage( )
      {
         ADDRESSESGRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSESGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSESGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subaddressesgrid_nextpage( )
      {
         ADDRESSESGRID_nRecordCount = subAddressesgrid_fnc_Recordcount( );
         if ( ( ADDRESSESGRID_nRecordCount >= subAddressesgrid_fnc_Recordsperpage( ) ) && ( ADDRESSESGRID_nEOF == 0 ) )
         {
            ADDRESSESGRID_nFirstRecordOnPage = (long)(ADDRESSESGRID_nFirstRecordOnPage+subAddressesgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSESGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSESGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         AddressesgridContainer.AddObjectProperty("ADDRESSESGRID_nFirstRecordOnPage", ADDRESSESGRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((ADDRESSESGRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subaddressesgrid_previouspage( )
      {
         if ( ADDRESSESGRID_nFirstRecordOnPage >= subAddressesgrid_fnc_Recordsperpage( ) )
         {
            ADDRESSESGRID_nFirstRecordOnPage = (long)(ADDRESSESGRID_nFirstRecordOnPage-subAddressesgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSESGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSESGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subaddressesgrid_lastpage( )
      {
         ADDRESSESGRID_nRecordCount = subAddressesgrid_fnc_Recordcount( );
         if ( ADDRESSESGRID_nRecordCount > subAddressesgrid_fnc_Recordsperpage( ) )
         {
            if ( ((long)((ADDRESSESGRID_nRecordCount) % (subAddressesgrid_fnc_Recordsperpage( )))) == 0 )
            {
               ADDRESSESGRID_nFirstRecordOnPage = (long)(ADDRESSESGRID_nRecordCount-subAddressesgrid_fnc_Recordsperpage( ));
            }
            else
            {
               ADDRESSESGRID_nFirstRecordOnPage = (long)(ADDRESSESGRID_nRecordCount-((long)((ADDRESSESGRID_nRecordCount) % (subAddressesgrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            ADDRESSESGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSESGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSESGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subaddressesgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            ADDRESSESGRID_nFirstRecordOnPage = (long)(subAddressesgrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            ADDRESSESGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSESGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSESGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavShownaddress_Enabled = 0;
         AssignProp(sPrefix, false, edtavShownaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavShownaddress_Enabled), 5, 0), true);
         edtavCtladdressindex_Enabled = 0;
         edtavGridstatus_Enabled = 0;
         edtavGridaddress_Enabled = 0;
         edtavCtlpath_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavCtlreceived_Enabled = 0;
         edtavCtltransactions_Enabled = 0;
         edtavCtlstatus_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3L0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E143L2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Addresses"), AV6addresses);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vADDRESSES"), AV6addresses);
            /* Read saved values. */
            nRC_GXsfl_30 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_30"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV20groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV20groupId"));
            ADDRESSESGRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"ADDRESSESGRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            ADDRESSESGRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"ADDRESSESGRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_30 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_30"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_30_fel_idx = 0;
            while ( nGXsfl_30_fel_idx < nRC_GXsfl_30 )
            {
               nGXsfl_30_fel_idx = ((subAddressesgrid_Islastpage==1)&&(nGXsfl_30_fel_idx+1>subAddressesgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_30_fel_idx+1);
               sGXsfl_30_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_302( ) ;
               AV21GXV1 = (int)(nGXsfl_30_fel_idx+ADDRESSESGRID_nFirstRecordOnPage);
               if ( ( AV6addresses.Count >= AV21GXV1 ) && ( AV21GXV1 > 0 ) )
               {
                  AV6addresses.CurrentItem = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1));
                  AV13gridStatus = cgiGet( edtavGridstatus_Internalname);
                  AV8gridAddress = cgiGet( edtavGridaddress_Internalname);
               }
            }
            if ( nGXsfl_30_fel_idx == 0 )
            {
               nGXsfl_30_idx = 1;
               sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
               SubsflControlProps_302( ) ;
            }
            nGXsfl_30_fel_idx = 1;
            /* Read variables values. */
            cmbavChainfilter.Name = cmbavChainfilter_Internalname;
            cmbavChainfilter.CurrentValue = cgiGet( cmbavChainfilter_Internalname);
            AV12chainFilter = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavChainfilter_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV12chainFilter", StringUtil.LTrimStr( (decimal)(AV12chainFilter), 4, 0));
            cmbavStatusfilter.Name = cmbavStatusfilter_Internalname;
            cmbavStatusfilter.CurrentValue = cgiGet( cmbavStatusfilter_Internalname);
            AV17statusFilter = cgiGet( cmbavStatusfilter_Internalname);
            AssignAttri(sPrefix, false, "AV17statusFilter", AV17statusFilter);
            AV9shownAddress = cgiGet( edtavShownaddress_Internalname);
            AssignAttri(sPrefix, false, "AV9shownAddress", AV9shownAddress);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E143L2 ();
         if (returnInSub) return;
      }

      protected void E143L2( )
      {
         /* Start Routine */
         returnInSub = false;
         lblTbaddressqr_Caption = "";
         AssignProp(sPrefix, false, lblTbaddressqr_Internalname, "Caption", lblTbaddressqr_Caption, true);
         edtavShownaddress_Visible = 0;
         AssignProp(sPrefix, false, edtavShownaddress_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavShownaddress_Visible), 5, 0), true);
         cmbavChainfilter.addItem(StringUtil.Trim( StringUtil.Str( NumberUtil.Val( "0", "."), 4, 0)), "Receiving", 0);
         cmbavChainfilter.addItem(StringUtil.Trim( StringUtil.Str( NumberUtil.Val( "1", "."), 4, 0)), "Change", 0);
         AV12chainFilter = (short)(Math.Round(NumberUtil.Val( "0", "."), 18, MidpointRounding.ToEven));
         AssignAttri(sPrefix, false, "AV12chainFilter", StringUtil.LTrimStr( (decimal)(AV12chainFilter), 4, 0));
         cmbavStatusfilter.addItem("Unused", "Unused (next ones)", 0);
         cmbavStatusfilter.addItem("Used", "Used", 0);
         cmbavStatusfilter.addItem("All", "All", 0);
         AV17statusFilter = "Unused";
         AssignAttri(sPrefix, false, "AV17statusFilter", AV17statusFilter);
      }

      protected void E153L2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         if ( (Guid.Empty==AV20groupId) )
         {
            new GeneXus.Programs.wallet.getwalletaddresses(context ).execute( out  AV11allAddresses, out  AV7error) ;
         }
         else
         {
            new GeneXus.Programs.wallet.getgroupaddresses(context ).execute(  AV20groupId, out  AV11allAddresses, out  AV7error) ;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The addresses can not be shown",(string)AV7error}, true);
         }
         AV6addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography");
         gx_BV30 = true;
         AV19usedCount = 0;
         AV18unusedCount = 0;
         AV16reservedCount = 0;
         AV14nextText = "";
         AV29GXV9 = 1;
         while ( AV29GXV9 <= AV11allAddresses.Count )
         {
            AV15oneAddress = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV11allAddresses.Item(AV29GXV9));
            if ( AV15oneAddress.gxTpr_Chainindex == AV12chainFilter )
            {
               if ( StringUtil.StrCmp(AV15oneAddress.gxTpr_Status, "Used") == 0 )
               {
                  AV19usedCount = (long)(AV19usedCount+1);
               }
               else if ( StringUtil.StrCmp(AV15oneAddress.gxTpr_Status, "Reserved") == 0 )
               {
                  AV18unusedCount = (long)(AV18unusedCount+1);
                  AV16reservedCount = (long)(AV16reservedCount+1);
               }
               else
               {
                  AV18unusedCount = (long)(AV18unusedCount+1);
               }
               if ( StringUtil.StrCmp(AV15oneAddress.gxTpr_Status, "Next") == 0 )
               {
                  AV14nextText = StringUtil.Trim( AV15oneAddress.gxTpr_Address) + " (index " + StringUtil.Trim( StringUtil.Str( (decimal)(AV15oneAddress.gxTpr_Addressindex), 10, 0)) + ")";
               }
               if ( ( StringUtil.StrCmp(AV17statusFilter, "All") == 0 ) || ( ( StringUtil.StrCmp(AV17statusFilter, "Used") == 0 ) && ( StringUtil.StrCmp(AV15oneAddress.gxTpr_Status, "Used") == 0 ) ) || ( ( StringUtil.StrCmp(AV17statusFilter, "Unused") == 0 ) && ( StringUtil.StrCmp(AV15oneAddress.gxTpr_Status, "Used") != 0 ) ) )
               {
                  AV6addresses.Add(AV15oneAddress, 0);
                  gx_BV30 = true;
               }
            }
            AV29GXV9 = (int)(AV29GXV9+1);
         }
         lblTbsummary_Caption = StringUtil.Trim( StringUtil.Str( (decimal)(AV19usedCount), 10, 0))+" used, "+StringUtil.Trim( StringUtil.Str( (decimal)(AV18unusedCount), 10, 0))+" unused";
         AssignProp(sPrefix, false, lblTbsummary_Internalname, "Caption", lblTbsummary_Caption, true);
         if ( AV16reservedCount > 0 )
         {
            lblTbsummary_Caption = lblTbsummary_Caption+" ("+StringUtil.Trim( StringUtil.Str( (decimal)(AV16reservedCount), 10, 0))+" reserved by payment requests)";
            AssignProp(sPrefix, false, lblTbsummary_Internalname, "Caption", lblTbsummary_Caption, true);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14nextText)) )
         {
            lblTbsummary_Caption = lblTbsummary_Caption+". Next address: "+StringUtil.Trim( AV14nextText);
            AssignProp(sPrefix, false, lblTbsummary_Internalname, "Caption", lblTbsummary_Caption, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV6addresses", AV6addresses);
      }

      protected void E113L2( )
      {
         /* Chainfilter_Controlvaluechanged Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV30 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV6addresses", AV6addresses);
            nGXsfl_30_bak_idx = nGXsfl_30_idx;
            gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
            nGXsfl_30_idx = nGXsfl_30_bak_idx;
            sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
            SubsflControlProps_302( ) ;
         }
      }

      protected void E123L2( )
      {
         /* Statusfilter_Controlvaluechanged Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV30 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV6addresses", AV6addresses);
            nGXsfl_30_bak_idx = nGXsfl_30_idx;
            gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
            nGXsfl_30_idx = nGXsfl_30_bak_idx;
            sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
            SubsflControlProps_302( ) ;
         }
      }

      private void E163L2( )
      {
         /* Addressesgrid_Load Routine */
         returnInSub = false;
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV6addresses.Count )
         {
            AV6addresses.CurrentItem = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1));
            AV8gridAddress = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtWalletAddressItem)(AV6addresses.CurrentItem)).gxTpr_Address);
            AssignAttri(sPrefix, false, edtavGridaddress_Internalname, AV8gridAddress);
            edtavGridaddress_Tooltiptext = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtWalletAddressItem)(AV6addresses.CurrentItem)).gxTpr_Address);
            if ( StringUtil.Len( AV8gridAddress) > 24 )
            {
               AV8gridAddress = StringUtil.Substring( AV8gridAddress, 1, 14) + "..." + StringUtil.Substring( AV8gridAddress, StringUtil.Len( AV8gridAddress)-7, 8);
               AssignAttri(sPrefix, false, edtavGridaddress_Internalname, AV8gridAddress);
            }
            if ( StringUtil.StrCmp(((GeneXus.Programs.wallet.SdtWalletAddressItem)(AV6addresses.CurrentItem)).gxTpr_Status, "Next") == 0 )
            {
               AV13gridStatus = "> NEXT";
               AssignAttri(sPrefix, false, edtavGridstatus_Internalname, AV13gridStatus);
            }
            else
            {
               AV13gridStatus = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtWalletAddressItem)(AV6addresses.CurrentItem)).gxTpr_Status);
               AssignAttri(sPrefix, false, edtavGridstatus_Internalname, AV13gridStatus);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 30;
            }
            if ( ( subAddressesgrid_Islastpage == 1 ) || ( 100 == 0 ) || ( ( ADDRESSESGRID_nCurrentRecord >= ADDRESSESGRID_nFirstRecordOnPage ) && ( ADDRESSESGRID_nCurrentRecord < ADDRESSESGRID_nFirstRecordOnPage + subAddressesgrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_302( ) ;
            }
            ADDRESSESGRID_nEOF = (short)(((subAddressesgrid_Rows==0)||(ADDRESSESGRID_nCurrentRecord<ADDRESSESGRID_nFirstRecordOnPage+subAddressesgrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSESGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSESGRID_nEOF), 1, 0, ".", "")));
            ADDRESSESGRID_nCurrentRecord = (long)(ADDRESSESGRID_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_30_Refreshing )
            {
               DoAjaxLoad(30, AddressesgridRow);
            }
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E173L2( )
      {
         AV21GXV1 = (int)(nGXsfl_30_idx+ADDRESSESGRID_nFirstRecordOnPage);
         if ( ( AV21GXV1 > 0 ) && ( AV6addresses.Count >= AV21GXV1 ) )
         {
            AV6addresses.CurrentItem = ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1));
         }
         /* 'Show Address' Routine */
         returnInSub = false;
         AV9shownAddress = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtWalletAddressItem)(AV6addresses.CurrentItem)).gxTpr_Address);
         AssignAttri(sPrefix, false, "AV9shownAddress", AV9shownAddress);
         GXt_char1 = AV10qrText;
         new GeneXus.Programs.wallet.buildaddressqrtext(context ).execute(  AV9shownAddress, out  GXt_char1) ;
         AV10qrText = GXt_char1;
         GXt_char1 = AV5addresImageText;
         new GeneXus.Programs.qrcoder.generateqrcodestring(context ).execute(  AV10qrText, out  GXt_char1) ;
         AV5addresImageText = GXt_char1;
         lblTbaddressqr_Caption = "<img src=\"data:image/png;base64, "+AV5addresImageText+"   \" style=\"width: 200px\" />";
         AssignProp(sPrefix, false, lblTbaddressqr_Internalname, "Caption", lblTbaddressqr_Caption, true);
         edtavShownaddress_Visible = 1;
         AssignProp(sPrefix, false, edtavShownaddress_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavShownaddress_Visible), 5, 0), true);
         /*  Sending Event outputs  */
      }

      protected void E133L2( )
      {
         /* 'Reload' Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV30 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV6addresses", AV6addresses);
            nGXsfl_30_bak_idx = nGXsfl_30_idx;
            gxgrAddressesgrid_refresh( subAddressesgrid_Rows, AV20groupId, AV12chainFilter, AV17statusFilter, AV6addresses, sPrefix) ;
            nGXsfl_30_idx = nGXsfl_30_bak_idx;
            sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
            SubsflControlProps_302( ) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV20groupId = (Guid)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV20groupId", AV20groupId.ToString());
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
         PA3L2( ) ;
         WS3L2( ) ;
         WE3L2( ) ;
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
         sCtrlAV20groupId = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA3L2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\walletaddresseswc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA3L2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV20groupId = (Guid)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV20groupId", AV20groupId.ToString());
         }
         wcpOAV20groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV20groupId"));
         if ( ! GetJustCreated( ) && ( ( AV20groupId != wcpOAV20groupId ) ) )
         {
            setjustcreated();
         }
         wcpOAV20groupId = AV20groupId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV20groupId = cgiGet( sPrefix+"AV20groupId_CTRL");
         if ( StringUtil.Len( sCtrlAV20groupId) > 0 )
         {
            AV20groupId = StringUtil.StrToGuid( cgiGet( sCtrlAV20groupId));
            AssignAttri(sPrefix, false, "AV20groupId", AV20groupId.ToString());
         }
         else
         {
            AV20groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"AV20groupId_PARM"));
         }
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
         PA3L2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS3L2( ) ;
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
         WS3L2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV20groupId_PARM", AV20groupId.ToString());
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV20groupId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV20groupId_CTRL", StringUtil.RTrim( sCtrlAV20groupId));
         }
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
         WE3L2( ) ;
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
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2026103013253425", true, true, false);
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
         context.AddJavascriptSource("wallet/walletaddresseswc.js", "?2026103013253425", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_302( )
      {
         edtavCtladdressindex_Internalname = sPrefix+"CTLADDRESSINDEX_"+sGXsfl_30_idx;
         edtavGridstatus_Internalname = sPrefix+"vGRIDSTATUS_"+sGXsfl_30_idx;
         edtavGridaddress_Internalname = sPrefix+"vGRIDADDRESS_"+sGXsfl_30_idx;
         edtavCtlpath_Internalname = sPrefix+"CTLPATH_"+sGXsfl_30_idx;
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL_"+sGXsfl_30_idx;
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE_"+sGXsfl_30_idx;
         edtavCtlreceived_Internalname = sPrefix+"CTLRECEIVED_"+sGXsfl_30_idx;
         edtavCtltransactions_Internalname = sPrefix+"CTLTRANSACTIONS_"+sGXsfl_30_idx;
         edtavCtlstatus_Internalname = sPrefix+"CTLSTATUS_"+sGXsfl_30_idx;
      }

      protected void SubsflControlProps_fel_302( )
      {
         edtavCtladdressindex_Internalname = sPrefix+"CTLADDRESSINDEX_"+sGXsfl_30_fel_idx;
         edtavGridstatus_Internalname = sPrefix+"vGRIDSTATUS_"+sGXsfl_30_fel_idx;
         edtavGridaddress_Internalname = sPrefix+"vGRIDADDRESS_"+sGXsfl_30_fel_idx;
         edtavCtlpath_Internalname = sPrefix+"CTLPATH_"+sGXsfl_30_fel_idx;
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL_"+sGXsfl_30_fel_idx;
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE_"+sGXsfl_30_fel_idx;
         edtavCtlreceived_Internalname = sPrefix+"CTLRECEIVED_"+sGXsfl_30_fel_idx;
         edtavCtltransactions_Internalname = sPrefix+"CTLTRANSACTIONS_"+sGXsfl_30_fel_idx;
         edtavCtlstatus_Internalname = sPrefix+"CTLSTATUS_"+sGXsfl_30_fel_idx;
      }

      protected void sendrow_302( )
      {
         sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
         SubsflControlProps_302( ) ;
         WB3L0( ) ;
         if ( ( 100 * 1 == 0 ) || ( nGXsfl_30_idx <= subAddressesgrid_fnc_Recordsperpage( ) * 1 ) )
         {
            AddressesgridRow = GXWebRow.GetNew(context,AddressesgridContainer);
            if ( subAddressesgrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subAddressesgrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subAddressesgrid_Class, "") != 0 )
               {
                  subAddressesgrid_Linesclass = subAddressesgrid_Class+"Odd";
               }
            }
            else if ( subAddressesgrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subAddressesgrid_Backstyle = 0;
               subAddressesgrid_Backcolor = subAddressesgrid_Allbackcolor;
               if ( StringUtil.StrCmp(subAddressesgrid_Class, "") != 0 )
               {
                  subAddressesgrid_Linesclass = subAddressesgrid_Class+"Uniform";
               }
            }
            else if ( subAddressesgrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subAddressesgrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subAddressesgrid_Class, "") != 0 )
               {
                  subAddressesgrid_Linesclass = subAddressesgrid_Class+"Odd";
               }
               subAddressesgrid_Backcolor = (int)(0x0);
            }
            else if ( subAddressesgrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subAddressesgrid_Backstyle = 1;
               if ( ((int)((nGXsfl_30_idx) % (2))) == 0 )
               {
                  subAddressesgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subAddressesgrid_Class, "") != 0 )
                  {
                     subAddressesgrid_Linesclass = subAddressesgrid_Class+"Even";
                  }
               }
               else
               {
                  subAddressesgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subAddressesgrid_Class, "") != 0 )
                  {
                     subAddressesgrid_Linesclass = subAddressesgrid_Class+"Odd";
                  }
               }
            }
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_30_idx+"\">") ;
            }
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',30)\"";
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtladdressindex_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Addressindex), 10, 0, ".", "")),StringUtil.LTrim( ((edtavCtladdressindex_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Addressindex), "ZZZZZZZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Addressindex), "ZZZZZZZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,31);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtladdressindex_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtladdressindex_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)10,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',30)\"";
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGridstatus_Internalname,StringUtil.RTrim( AV13gridStatus),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavGridstatus_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavGridstatus_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',30)\"";
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGridaddress_Internalname,StringUtil.RTrim( AV8gridAddress),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,33);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SHOW ADDRESS\\'."+sGXsfl_30_idx+"'",(string)"",(string)"",(string)edtavGridaddress_Tooltiptext,(string)"",(string)edtavGridaddress_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavGridaddress_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 34,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',30)\"";
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlpath_Internalname,((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Path,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,34);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlpath_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlpath_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',30)\"";
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtllabel_Internalname,((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Label,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,35);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtllabel_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtllabel_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',30)\"";
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlbalance_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Balance, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlbalance_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Balance, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Balance, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,36);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlbalance_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlbalance_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',30)\"";
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlreceived_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Received, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlreceived_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Received, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Received, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,37);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlreceived_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlreceived_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'" + sPrefix + "',false,'" + sGXsfl_30_idx + "',30)\"";
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtltransactions_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Transactions), 10, 0, ".", "")),StringUtil.LTrim( ((edtavCtltransactions_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Transactions), "ZZZZZZZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Transactions), "ZZZZZZZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,38);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtltransactions_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtltransactions_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)10,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( AddressesgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            AddressesgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlstatus_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWalletAddressItem)AV6addresses.Item(AV21GXV1)).gxTpr_Status),(string)"",""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlstatus_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlstatus_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)30,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3L2( ) ;
            AddressesgridContainer.AddRow(AddressesgridRow);
            nGXsfl_30_idx = ((subAddressesgrid_Islastpage==1)&&(nGXsfl_30_idx+1>subAddressesgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_30_idx+1);
            sGXsfl_30_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_30_idx), 4, 0), 4, "0");
            SubsflControlProps_302( ) ;
         }
         /* End function sendrow_302 */
      }

      protected void init_web_controls( )
      {
         cmbavChainfilter.Name = "vCHAINFILTER";
         cmbavChainfilter.WebTags = "";
         if ( cmbavChainfilter.ItemCount > 0 )
         {
         }
         cmbavStatusfilter.Name = "vSTATUSFILTER";
         cmbavStatusfilter.WebTags = "";
         if ( cmbavStatusfilter.ItemCount > 0 )
         {
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl30( )
      {
         if ( AddressesgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"AddressesgridContainer"+"DivS\" data-gxgridid=\"30\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subAddressesgrid_Internalname, subAddressesgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subAddressesgrid_Backcolorstyle == 0 )
            {
               subAddressesgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subAddressesgrid_Class) > 0 )
               {
                  subAddressesgrid_Linesclass = subAddressesgrid_Class+"Title";
               }
            }
            else
            {
               subAddressesgrid_Titlebackstyle = 1;
               if ( subAddressesgrid_Backcolorstyle == 1 )
               {
                  subAddressesgrid_Titlebackcolor = subAddressesgrid_Allbackcolor;
                  if ( StringUtil.Len( subAddressesgrid_Class) > 0 )
                  {
                     subAddressesgrid_Linesclass = subAddressesgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subAddressesgrid_Class) > 0 )
                  {
                     subAddressesgrid_Linesclass = subAddressesgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Index") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Status") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Address") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Path") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Label") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Balance") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Received") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Transactions") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "status") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            AddressesgridContainer.AddObjectProperty("GridName", "Addressesgrid");
         }
         else
         {
            AddressesgridContainer.AddObjectProperty("GridName", "Addressesgrid");
            AddressesgridContainer.AddObjectProperty("Header", subAddressesgrid_Header);
            AddressesgridContainer.AddObjectProperty("Class", "Grid");
            AddressesgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Backcolorstyle), 1, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("CmpContext", sPrefix);
            AddressesgridContainer.AddObjectProperty("InMasterPage", "false");
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtladdressindex_Enabled), 5, 0, ".", "")));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV13gridStatus)));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavGridstatus_Enabled), 5, 0, ".", "")));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV8gridAddress)));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavGridaddress_Enabled), 5, 0, ".", "")));
            AddressesgridColumn.AddObjectProperty("Tooltiptext", StringUtil.RTrim( edtavGridaddress_Tooltiptext));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlpath_Enabled), 5, 0, ".", "")));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtllabel_Enabled), 5, 0, ".", "")));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlbalance_Enabled), 5, 0, ".", "")));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlreceived_Enabled), 5, 0, ".", "")));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtltransactions_Enabled), 5, 0, ".", "")));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressesgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlstatus_Enabled), 5, 0, ".", "")));
            AddressesgridContainer.AddColumnProperties(AddressesgridColumn);
            AddressesgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Selectedindex), 4, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Allowselection), 1, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Selectioncolor), 9, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Allowhovering), 1, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Hoveringcolor), 9, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Allowcollapsing), 1, 0, ".", "")));
            AddressesgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressesgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         cmbavChainfilter_Internalname = sPrefix+"vCHAINFILTER";
         cmbavStatusfilter_Internalname = sPrefix+"vSTATUSFILTER";
         bttReload_Internalname = sPrefix+"RELOAD";
         lblTbsummary_Internalname = sPrefix+"TBSUMMARY";
         lblTbaddressesinfo_Internalname = sPrefix+"TBADDRESSESINFO";
         edtavShownaddress_Internalname = sPrefix+"vSHOWNADDRESS";
         lblTbaddressqr_Internalname = sPrefix+"TBADDRESSQR";
         edtavCtladdressindex_Internalname = sPrefix+"CTLADDRESSINDEX";
         edtavGridstatus_Internalname = sPrefix+"vGRIDSTATUS";
         edtavGridaddress_Internalname = sPrefix+"vGRIDADDRESS";
         edtavCtlpath_Internalname = sPrefix+"CTLPATH";
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL";
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE";
         edtavCtlreceived_Internalname = sPrefix+"CTLRECEIVED";
         edtavCtltransactions_Internalname = sPrefix+"CTLTRANSACTIONS";
         edtavCtlstatus_Internalname = sPrefix+"CTLSTATUS";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subAddressesgrid_Internalname = sPrefix+"ADDRESSESGRID";
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
         subAddressesgrid_Allowcollapsing = 0;
         subAddressesgrid_Allowselection = 0;
         subAddressesgrid_Header = "";
         edtavCtlstatus_Jsonclick = "";
         edtavCtlstatus_Enabled = 0;
         edtavCtltransactions_Jsonclick = "";
         edtavCtltransactions_Enabled = 0;
         edtavCtlreceived_Jsonclick = "";
         edtavCtlreceived_Enabled = 0;
         edtavCtlbalance_Jsonclick = "";
         edtavCtlbalance_Enabled = 0;
         edtavCtllabel_Jsonclick = "";
         edtavCtllabel_Enabled = 0;
         edtavCtlpath_Jsonclick = "";
         edtavCtlpath_Enabled = 0;
         edtavGridaddress_Jsonclick = "";
         edtavGridaddress_Tooltiptext = "";
         edtavGridaddress_Enabled = 1;
         edtavGridstatus_Jsonclick = "";
         edtavGridstatus_Enabled = 1;
         edtavCtladdressindex_Jsonclick = "";
         edtavCtladdressindex_Enabled = 0;
         subAddressesgrid_Class = "Grid";
         subAddressesgrid_Backcolorstyle = 0;
         lblTbaddressqr_Caption = "TBAddressQR";
         edtavShownaddress_Enabled = 1;
         edtavShownaddress_Visible = 1;
         lblTbsummary_Caption = "Summary";
         cmbavStatusfilter_Jsonclick = "";
         cmbavStatusfilter.Enabled = 1;
         cmbavChainfilter_Jsonclick = "";
         cmbavChainfilter.Enabled = 1;
         edtavCtlstatus_Enabled = -1;
         edtavCtltransactions_Enabled = -1;
         edtavCtlreceived_Enabled = -1;
         edtavCtlbalance_Enabled = -1;
         edtavCtllabel_Enabled = -1;
         edtavCtlpath_Enabled = -1;
         edtavCtladdressindex_Enabled = -1;
         subAddressesgrid_Rows = 100;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"ADDRESSESGRID_nEOF","type":"int"},{"av":"subAddressesgrid_Rows","ctrl":"ADDRESSESGRID","prop":"Rows"},{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV20groupId","fld":"vGROUPID","type":"guid"},{"av":"cmbavChainfilter"},{"av":"AV12chainFilter","fld":"vCHAINFILTER","pic":"ZZZ9","type":"int"},{"av":"cmbavStatusfilter"},{"av":"AV17statusFilter","fld":"vSTATUSFILTER","type":"char"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("VCHAINFILTER.CONTROLVALUECHANGED","""{"handler":"E113L2","iparms":[{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"ADDRESSESGRID_nEOF","type":"int"},{"av":"subAddressesgrid_Rows","ctrl":"ADDRESSESGRID","prop":"Rows"},{"av":"AV20groupId","fld":"vGROUPID","type":"guid"},{"av":"cmbavChainfilter"},{"av":"AV12chainFilter","fld":"vCHAINFILTER","pic":"ZZZ9","type":"int"},{"av":"cmbavStatusfilter"},{"av":"AV17statusFilter","fld":"vSTATUSFILTER","type":"char"},{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("VCHAINFILTER.CONTROLVALUECHANGED",""","oparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("VSTATUSFILTER.CONTROLVALUECHANGED","""{"handler":"E123L2","iparms":[{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"ADDRESSESGRID_nEOF","type":"int"},{"av":"subAddressesgrid_Rows","ctrl":"ADDRESSESGRID","prop":"Rows"},{"av":"AV20groupId","fld":"vGROUPID","type":"guid"},{"av":"cmbavChainfilter"},{"av":"AV12chainFilter","fld":"vCHAINFILTER","pic":"ZZZ9","type":"int"},{"av":"cmbavStatusfilter"},{"av":"AV17statusFilter","fld":"vSTATUSFILTER","type":"char"},{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("VSTATUSFILTER.CONTROLVALUECHANGED",""","oparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSESGRID.LOAD","""{"handler":"E163L2","iparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"}]""");
         setEventMetadata("ADDRESSESGRID.LOAD",""","oparms":[{"av":"AV8gridAddress","fld":"vGRIDADDRESS","type":"char"},{"av":"edtavGridaddress_Tooltiptext","ctrl":"vGRIDADDRESS","prop":"Tooltiptext"},{"av":"AV13gridStatus","fld":"vGRIDSTATUS","type":"char"}]}""");
         setEventMetadata("'SHOW ADDRESS'","""{"handler":"E173L2","iparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"}]""");
         setEventMetadata("'SHOW ADDRESS'",""","oparms":[{"av":"AV9shownAddress","fld":"vSHOWNADDRESS","type":"char"},{"av":"lblTbaddressqr_Caption","ctrl":"TBADDRESSQR","prop":"Caption"},{"av":"edtavShownaddress_Visible","ctrl":"vSHOWNADDRESS","prop":"Visible"}]}""");
         setEventMetadata("'RELOAD'","""{"handler":"E133L2","iparms":[{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"ADDRESSESGRID_nEOF","type":"int"},{"av":"subAddressesgrid_Rows","ctrl":"ADDRESSESGRID","prop":"Rows"},{"av":"AV20groupId","fld":"vGROUPID","type":"guid"},{"av":"cmbavChainfilter"},{"av":"AV12chainFilter","fld":"vCHAINFILTER","pic":"ZZZ9","type":"int"},{"av":"cmbavStatusfilter"},{"av":"AV17statusFilter","fld":"vSTATUSFILTER","type":"char"},{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'RELOAD'",""","oparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSESGRID_FIRSTPAGE","""{"handler":"subaddressesgrid_firstpage","iparms":[{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"ADDRESSESGRID_nEOF","type":"int"},{"av":"subAddressesgrid_Rows","ctrl":"ADDRESSESGRID","prop":"Rows"},{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV20groupId","fld":"vGROUPID","type":"guid"},{"av":"cmbavChainfilter"},{"av":"AV12chainFilter","fld":"vCHAINFILTER","pic":"ZZZ9","type":"int"},{"av":"cmbavStatusfilter"},{"av":"AV17statusFilter","fld":"vSTATUSFILTER","type":"char"}]""");
         setEventMetadata("ADDRESSESGRID_FIRSTPAGE",""","oparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSESGRID_PREVPAGE","""{"handler":"subaddressesgrid_previouspage","iparms":[{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"ADDRESSESGRID_nEOF","type":"int"},{"av":"subAddressesgrid_Rows","ctrl":"ADDRESSESGRID","prop":"Rows"},{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV20groupId","fld":"vGROUPID","type":"guid"},{"av":"cmbavChainfilter"},{"av":"AV12chainFilter","fld":"vCHAINFILTER","pic":"ZZZ9","type":"int"},{"av":"cmbavStatusfilter"},{"av":"AV17statusFilter","fld":"vSTATUSFILTER","type":"char"}]""");
         setEventMetadata("ADDRESSESGRID_PREVPAGE",""","oparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSESGRID_NEXTPAGE","""{"handler":"subaddressesgrid_nextpage","iparms":[{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"ADDRESSESGRID_nEOF","type":"int"},{"av":"subAddressesgrid_Rows","ctrl":"ADDRESSESGRID","prop":"Rows"},{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV20groupId","fld":"vGROUPID","type":"guid"},{"av":"cmbavChainfilter"},{"av":"AV12chainFilter","fld":"vCHAINFILTER","pic":"ZZZ9","type":"int"},{"av":"cmbavStatusfilter"},{"av":"AV17statusFilter","fld":"vSTATUSFILTER","type":"char"}]""");
         setEventMetadata("ADDRESSESGRID_NEXTPAGE",""","oparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSESGRID_LASTPAGE","""{"handler":"subaddressesgrid_lastpage","iparms":[{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"ADDRESSESGRID_nEOF","type":"int"},{"av":"subAddressesgrid_Rows","ctrl":"ADDRESSESGRID","prop":"Rows"},{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV20groupId","fld":"vGROUPID","type":"guid"},{"av":"cmbavChainfilter"},{"av":"AV12chainFilter","fld":"vCHAINFILTER","pic":"ZZZ9","type":"int"},{"av":"cmbavStatusfilter"},{"av":"AV17statusFilter","fld":"vSTATUSFILTER","type":"char"}]""");
         setEventMetadata("ADDRESSESGRID_LASTPAGE",""","oparms":[{"av":"AV6addresses","fld":"vADDRESSES","grid":30,"type":""},{"av":"nGXsfl_30_idx","ctrl":"GRID","prop":"GridCurrRow","grid":30},{"av":"ADDRESSESGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_30","ctrl":"ADDRESSESGRID","prop":"GridRC","grid":30,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv8","iparms":[]}""");
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
         wcpOAV20groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV17statusFilter = "";
         AV6addresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GX_FocusControl = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttReload_Jsonclick = "";
         lblTbsummary_Jsonclick = "";
         lblTbaddressesinfo_Jsonclick = "";
         AV9shownAddress = "";
         lblTbaddressqr_Jsonclick = "";
         AddressesgridContainer = new GXWebGrid( context);
         sStyleString = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV13gridStatus = "";
         AV8gridAddress = "";
         AV11allAddresses = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem>( context, "WalletAddressItem", "distributedcryptography");
         AV7error = "";
         AV14nextText = "";
         AV15oneAddress = new GeneXus.Programs.wallet.SdtWalletAddressItem(context);
         AddressesgridRow = new GXWebRow();
         AV10qrText = "";
         AV5addresImageText = "";
         GXt_char1 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV20groupId = "";
         subAddressesgrid_Linesclass = "";
         ROClassString = "";
         AddressesgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavShownaddress_Enabled = 0;
         edtavCtladdressindex_Enabled = 0;
         edtavGridstatus_Enabled = 0;
         edtavGridaddress_Enabled = 0;
         edtavCtlpath_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavCtlreceived_Enabled = 0;
         edtavCtltransactions_Enabled = 0;
         edtavCtlstatus_Enabled = 0;
      }

      private short ADDRESSESGRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short AV12chainFilter ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subAddressesgrid_Backcolorstyle ;
      private short nGXWrapped ;
      private short subAddressesgrid_Backstyle ;
      private short subAddressesgrid_Titlebackstyle ;
      private short subAddressesgrid_Allowselection ;
      private short subAddressesgrid_Allowhovering ;
      private short subAddressesgrid_Allowcollapsing ;
      private short subAddressesgrid_Collapsed ;
      private int nRC_GXsfl_30 ;
      private int subAddressesgrid_Rows ;
      private int nGXsfl_30_idx=1 ;
      private int edtavShownaddress_Enabled ;
      private int edtavCtladdressindex_Enabled ;
      private int edtavGridstatus_Enabled ;
      private int edtavGridaddress_Enabled ;
      private int edtavCtlpath_Enabled ;
      private int edtavCtllabel_Enabled ;
      private int edtavCtlbalance_Enabled ;
      private int edtavCtlreceived_Enabled ;
      private int edtavCtltransactions_Enabled ;
      private int edtavCtlstatus_Enabled ;
      private int edtavShownaddress_Visible ;
      private int AV21GXV1 ;
      private int subAddressesgrid_Islastpage ;
      private int ADDRESSESGRID_nGridOutOfScope ;
      private int nGXsfl_30_fel_idx=1 ;
      private int AV29GXV9 ;
      private int nGXsfl_30_bak_idx=1 ;
      private int idxLst ;
      private int subAddressesgrid_Backcolor ;
      private int subAddressesgrid_Allbackcolor ;
      private int subAddressesgrid_Titlebackcolor ;
      private int subAddressesgrid_Selectedindex ;
      private int subAddressesgrid_Selectioncolor ;
      private int subAddressesgrid_Hoveringcolor ;
      private long ADDRESSESGRID_nFirstRecordOnPage ;
      private long ADDRESSESGRID_nCurrentRecord ;
      private long ADDRESSESGRID_nRecordCount ;
      private long AV19usedCount ;
      private long AV18unusedCount ;
      private long AV16reservedCount ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_30_idx="0001" ;
      private string AV17statusFilter ;
      private string edtavShownaddress_Internalname ;
      private string edtavCtladdressindex_Internalname ;
      private string edtavGridstatus_Internalname ;
      private string edtavGridaddress_Internalname ;
      private string edtavCtlpath_Internalname ;
      private string edtavCtllabel_Internalname ;
      private string edtavCtlbalance_Internalname ;
      private string edtavCtlreceived_Internalname ;
      private string edtavCtltransactions_Internalname ;
      private string edtavCtlstatus_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string cmbavChainfilter_Internalname ;
      private string TempTags ;
      private string cmbavChainfilter_Jsonclick ;
      private string cmbavStatusfilter_Internalname ;
      private string cmbavStatusfilter_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string bttReload_Internalname ;
      private string bttReload_Jsonclick ;
      private string lblTbsummary_Internalname ;
      private string lblTbsummary_Caption ;
      private string lblTbsummary_Jsonclick ;
      private string lblTbaddressesinfo_Internalname ;
      private string lblTbaddressesinfo_Jsonclick ;
      private string AV9shownAddress ;
      private string lblTbaddressqr_Internalname ;
      private string lblTbaddressqr_Caption ;
      private string lblTbaddressqr_Jsonclick ;
      private string sStyleString ;
      private string subAddressesgrid_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV13gridStatus ;
      private string AV8gridAddress ;
      private string sGXsfl_30_fel_idx="0001" ;
      private string AV7error ;
      private string edtavGridaddress_Tooltiptext ;
      private string GXt_char1 ;
      private string sCtrlAV20groupId ;
      private string subAddressesgrid_Class ;
      private string subAddressesgrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtladdressindex_Jsonclick ;
      private string edtavGridstatus_Jsonclick ;
      private string edtavGridaddress_Jsonclick ;
      private string edtavCtlpath_Jsonclick ;
      private string edtavCtllabel_Jsonclick ;
      private string edtavCtlbalance_Jsonclick ;
      private string edtavCtlreceived_Jsonclick ;
      private string edtavCtltransactions_Jsonclick ;
      private string edtavCtlstatus_Jsonclick ;
      private string subAddressesgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_30_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool gx_BV30 ;
      private string AV5addresImageText ;
      private string AV14nextText ;
      private string AV10qrText ;
      private Guid AV20groupId ;
      private Guid wcpOAV20groupId ;
      private GXWebGrid AddressesgridContainer ;
      private GXWebRow AddressesgridRow ;
      private GXWebColumn AddressesgridColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavChainfilter ;
      private GXCombobox cmbavStatusfilter ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> AV6addresses ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletAddressItem> AV11allAddresses ;
      private GeneXus.Programs.wallet.SdtWalletAddressItem AV15oneAddress ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
