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
   public class paymentrequestswc : GXWebComponent
   {
      public paymentrequestswc( )
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

      public paymentrequestswc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId )
      {
         this.AV16groupId = aP0_groupId;
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
         cmbavExpiresinhours = new GXCombobox();
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
                  AV16groupId = StringUtil.StrToGuid( GetPar( "groupId"));
                  AssignAttri(sPrefix, false, "AV16groupId", AV16groupId.ToString());
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(Guid)AV16groupId});
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Requestsgrid") == 0 )
               {
                  gxnrRequestsgrid_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Requestsgrid") == 0 )
               {
                  gxgrRequestsgrid_refresh_invoke( ) ;
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

      protected void gxnrRequestsgrid_newrow_invoke( )
      {
         nRC_GXsfl_34 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_34"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_34_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_34_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_34_idx = GetPar( "sGXsfl_34_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrRequestsgrid_newrow( ) ;
         /* End function gxnrRequestsgrid_newrow_invoke */
      }

      protected void gxgrRequestsgrid_refresh_invoke( )
      {
         subRequestsgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subRequestsgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         AV16groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV13requests);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrRequestsgrid_refresh_invoke */
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
            PA3M2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavRequesturi_Enabled = 0;
               AssignProp(sPrefix, false, edtavRequesturi_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRequesturi_Enabled), 5, 0), true);
               edtavCtlcreatedat_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcreatedat_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcreatedat_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               edtavCtllabel_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtllabel_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtllabel_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               edtavCtlamount_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlamount_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlamount_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               edtavCtlstatus_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlstatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlstatus_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               edtavCtlreceivedamount_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlreceivedamount_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlreceivedamount_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               edtavCtlexpiresat_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlexpiresat_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlexpiresat_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               edtavGridaddress_Enabled = 0;
               AssignProp(sPrefix, false, edtavGridaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGridaddress_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               edtavCanceltext_Enabled = 0;
               AssignProp(sPrefix, false, edtavCanceltext_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCanceltext_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               edtavCtlrequestid_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlrequestid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlrequestid_Enabled), 5, 0), !bGXsfl_34_Refreshing);
               WS3M2( ) ;
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
            context.SendWebValue( "Payment requests") ;
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.paymentrequestswc", new object[] {UrlEncode(AV16groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Requests", AV13requests);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Requests", AV13requests);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_34", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_34), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV16groupId", wcpOAV16groupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV16groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vREQUESTS", AV13requests);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vREQUESTS", AV13requests);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vSHOWNURI", AV15shownUri);
         GxWebStd.gx_hidden_field( context, sPrefix+"REQUESTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(REQUESTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"REQUESTSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(REQUESTSGRID_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm3M2( )
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
         return "Wallet.PaymentRequestsWC" ;
      }

      public override string GetPgmdesc( )
      {
         return "Payment requests" ;
      }

      protected void WB3M0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.paymentrequestswc");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbrequestsinfo_Internalname, "Reserve a receiving address for a charge. While the request is Pending the wallet does not offer that address to anybody else. Amount 0 = any amount. The status follows the last balance check (Rescan Balance asks for every address again).", "", "", lblTbrequestsinfo_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/PaymentRequestsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRequestlabel_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRequestlabel_Internalname, "Label", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavRequestlabel_Internalname, AV12requestLabel, StringUtil.RTrim( context.localUtil.Format( AV12requestLabel, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,11);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavRequestlabel_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavRequestlabel_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/PaymentRequestsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRequestamount_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRequestamount_Internalname, "Amount (BTC)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavRequestamount_Internalname, StringUtil.LTrim( StringUtil.NToC( AV11requestAmount, 16, 8, ".", "")), StringUtil.LTrim( ((edtavRequestamount_Enabled!=0) ? context.localUtil.Format( AV11requestAmount, "ZZZZZZ9.99999999") : context.localUtil.Format( AV11requestAmount, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,15);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavRequestamount_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavRequestamount_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/PaymentRequestsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavExpiresinhours_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavExpiresinhours_Internalname, "Expires", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavExpiresinhours, cmbavExpiresinhours_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV8expiresInHours), 5, 0)), 1, cmbavExpiresinhours_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "int", "", 1, cmbavExpiresinhours.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"", "", true, 0, "HLP_Wallet/PaymentRequestsWC.htm");
            cmbavExpiresinhours.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV8expiresInHours), 5, 0));
            AssignProp(sPrefix, false, cmbavExpiresinhours_Internalname, "Values", (string)(cmbavExpiresinhours.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreaterequest_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(34), 2, 0)+","+"null"+");", "Create Request", bttCreaterequest_Jsonclick, 5, "Create Request", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CREATE REQUEST\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/PaymentRequestsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttReload_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(34), 2, 0)+","+"null"+");", "Reload", bttReload_Jsonclick, 5, "Reload", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'RELOAD\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/PaymentRequestsWC.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavRequesturi_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRequesturi_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRequesturi_Internalname, "Payment URI (BIP21)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavRequesturi_Internalname, AV14requestUri, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"", 0, edtavRequesturi_Visible, edtavRequesturi_Enabled, 0, 80, "chr", 5, "row", 0, StyleString, ClassString, "", "", "400", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/PaymentRequestsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbrequestqr_Internalname, lblTbrequestqr_Caption, "", "", lblTbrequestqr_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_Wallet/PaymentRequestsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            RequestsgridContainer.SetWrapped(nGXWrapped);
            StartGridControl34( ) ;
         }
         if ( wbEnd == 34 )
         {
            wbEnd = 0;
            nRC_GXsfl_34 = (int)(nGXsfl_34_idx-1);
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               RequestsgridContainer.AddObjectProperty("REQUESTSGRID_nEOF", REQUESTSGRID_nEOF);
               RequestsgridContainer.AddObjectProperty("REQUESTSGRID_nFirstRecordOnPage", REQUESTSGRID_nFirstRecordOnPage);
               AV17GXV1 = nGXsfl_34_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"RequestsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Requestsgrid", RequestsgridContainer, subRequestsgrid_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"RequestsgridContainerData", RequestsgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"RequestsgridContainerData"+"V", RequestsgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"RequestsgridContainerData"+"V"+"\" value='"+RequestsgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 34 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( RequestsgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  RequestsgridContainer.AddObjectProperty("REQUESTSGRID_nEOF", REQUESTSGRID_nEOF);
                  RequestsgridContainer.AddObjectProperty("REQUESTSGRID_nFirstRecordOnPage", REQUESTSGRID_nFirstRecordOnPage);
                  AV17GXV1 = nGXsfl_34_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"RequestsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Requestsgrid", RequestsgridContainer, subRequestsgrid_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"RequestsgridContainerData", RequestsgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"RequestsgridContainerData"+"V", RequestsgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"RequestsgridContainerData"+"V"+"\" value='"+RequestsgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3M2( )
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
            Form.Meta.addItem("description", "Payment requests", 0) ;
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
               STRUP3M0( ) ;
            }
         }
      }

      protected void WS3M2( )
      {
         START3M2( ) ;
         EVT3M2( ) ;
      }

      protected void EVT3M2( )
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
                                 STRUP3M0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "'CREATE REQUEST'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3M0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Create Request' */
                                    E113M2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'RELOAD'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3M0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Reload' */
                                    E123M2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3M0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlcreatedat_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "REQUESTSGRIDPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3M0( ) ;
                              }
                              sEvt = cgiGet( sPrefix+"REQUESTSGRIDPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subrequestsgrid_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subrequestsgrid_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subrequestsgrid_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subrequestsgrid_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "REQUESTSGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'SHOW REQUEST'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "'CANCEL REQUEST'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'SHOW REQUEST'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "'CANCEL REQUEST'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3M0( ) ;
                              }
                              nGXsfl_34_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
                              SubsflControlProps_342( ) ;
                              AV17GXV1 = (int)(nGXsfl_34_idx+REQUESTSGRID_nFirstRecordOnPage);
                              if ( ( AV13requests.Count >= AV17GXV1 ) && ( AV17GXV1 > 0 ) )
                              {
                                 AV13requests.CurrentItem = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1));
                                 AV9gridAddress = cgiGet( edtavGridaddress_Internalname);
                                 AssignAttri(sPrefix, false, edtavGridaddress_Internalname, AV9gridAddress);
                                 AV6cancelText = cgiGet( edtavCanceltext_Internalname);
                                 AssignAttri(sPrefix, false, edtavCanceltext_Internalname, AV6cancelText);
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
                                          GX_FocusControl = edtavCtlcreatedat_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E133M2 ();
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
                                          GX_FocusControl = edtavCtlcreatedat_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E143M2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REQUESTSGRID.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlcreatedat_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Requestsgrid.Load */
                                          E153M2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'SHOW REQUEST'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlcreatedat_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Show Request' */
                                          E163M2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'CANCEL REQUEST'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlcreatedat_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Cancel Request' */
                                          E173M2 ();
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
                                       STRUP3M0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlcreatedat_Internalname;
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

      protected void WE3M2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3M2( ) ;
            }
         }
      }

      protected void PA3M2( )
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
               GX_FocusControl = edtavRequestlabel_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrRequestsgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_342( ) ;
         while ( nGXsfl_34_idx <= nRC_GXsfl_34 )
         {
            sendrow_342( ) ;
            nGXsfl_34_idx = ((subRequestsgrid_Islastpage==1)&&(nGXsfl_34_idx+1>subRequestsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_34_idx+1);
            sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
            SubsflControlProps_342( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( RequestsgridContainer)) ;
         /* End function gxnrRequestsgrid_newrow */
      }

      protected void gxgrRequestsgrid_refresh( int subRequestsgrid_Rows ,
                                               Guid AV16groupId ,
                                               GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV13requests ,
                                               string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         REQUESTSGRID_nCurrentRecord = 0;
         RF3M2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrRequestsgrid_refresh */
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
         if ( cmbavExpiresinhours.ItemCount > 0 )
         {
            AV8expiresInHours = (int)(Math.Round(NumberUtil.Val( cmbavExpiresinhours.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV8expiresInHours), 5, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV8expiresInHours", StringUtil.LTrimStr( (decimal)(AV8expiresInHours), 5, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavExpiresinhours.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV8expiresInHours), 5, 0));
            AssignProp(sPrefix, false, cmbavExpiresinhours_Internalname, "Values", cmbavExpiresinhours.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3M2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavRequesturi_Enabled = 0;
         AssignProp(sPrefix, false, edtavRequesturi_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRequesturi_Enabled), 5, 0), true);
         edtavCtlcreatedat_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavCtlamount_Enabled = 0;
         edtavCtlstatus_Enabled = 0;
         edtavCtlreceivedamount_Enabled = 0;
         edtavCtlexpiresat_Enabled = 0;
         edtavGridaddress_Enabled = 0;
         edtavCanceltext_Enabled = 0;
         edtavCtlrequestid_Enabled = 0;
      }

      protected void RF3M2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            RequestsgridContainer.ClearRows();
         }
         wbStart = 34;
         /* Execute user event: Refresh */
         E143M2 ();
         nGXsfl_34_idx = 1;
         sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
         SubsflControlProps_342( ) ;
         bGXsfl_34_Refreshing = true;
         RequestsgridContainer.AddObjectProperty("GridName", "Requestsgrid");
         RequestsgridContainer.AddObjectProperty("CmpContext", sPrefix);
         RequestsgridContainer.AddObjectProperty("InMasterPage", "false");
         RequestsgridContainer.AddObjectProperty("Class", "Grid");
         RequestsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         RequestsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         RequestsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Backcolorstyle), 1, 0, ".", "")));
         RequestsgridContainer.PageSize = subRequestsgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_342( ) ;
            /* Execute user event: Requestsgrid.Load */
            E153M2 ();
            if ( ( subRequestsgrid_Islastpage == 0 ) && ( REQUESTSGRID_nCurrentRecord > 0 ) && ( REQUESTSGRID_nGridOutOfScope == 0 ) && ( nGXsfl_34_idx == 1 ) )
            {
               REQUESTSGRID_nCurrentRecord = 0;
               REQUESTSGRID_nGridOutOfScope = 1;
               subrequestsgrid_firstpage( ) ;
               /* Execute user event: Requestsgrid.Load */
               E153M2 ();
            }
            wbEnd = 34;
            WB3M0( ) ;
         }
         bGXsfl_34_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3M2( )
      {
      }

      protected int subRequestsgrid_fnc_Pagecount( )
      {
         REQUESTSGRID_nRecordCount = subRequestsgrid_fnc_Recordcount( );
         if ( ((long)((REQUESTSGRID_nRecordCount) % (subRequestsgrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(REQUESTSGRID_nRecordCount/ (decimal)(subRequestsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(REQUESTSGRID_nRecordCount/ (decimal)(subRequestsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subRequestsgrid_fnc_Recordcount( )
      {
         return AV13requests.Count ;
      }

      protected int subRequestsgrid_fnc_Recordsperpage( )
      {
         return (int)(50*1) ;
      }

      protected int subRequestsgrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(REQUESTSGRID_nFirstRecordOnPage/ (decimal)(subRequestsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subrequestsgrid_firstpage( )
      {
         REQUESTSGRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, sPrefix+"REQUESTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(REQUESTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subrequestsgrid_nextpage( )
      {
         REQUESTSGRID_nRecordCount = subRequestsgrid_fnc_Recordcount( );
         if ( ( REQUESTSGRID_nRecordCount >= subRequestsgrid_fnc_Recordsperpage( ) ) && ( REQUESTSGRID_nEOF == 0 ) )
         {
            REQUESTSGRID_nFirstRecordOnPage = (long)(REQUESTSGRID_nFirstRecordOnPage+subRequestsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"REQUESTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(REQUESTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         RequestsgridContainer.AddObjectProperty("REQUESTSGRID_nFirstRecordOnPage", REQUESTSGRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((REQUESTSGRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subrequestsgrid_previouspage( )
      {
         if ( REQUESTSGRID_nFirstRecordOnPage >= subRequestsgrid_fnc_Recordsperpage( ) )
         {
            REQUESTSGRID_nFirstRecordOnPage = (long)(REQUESTSGRID_nFirstRecordOnPage-subRequestsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"REQUESTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(REQUESTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subrequestsgrid_lastpage( )
      {
         REQUESTSGRID_nRecordCount = subRequestsgrid_fnc_Recordcount( );
         if ( REQUESTSGRID_nRecordCount > subRequestsgrid_fnc_Recordsperpage( ) )
         {
            if ( ((long)((REQUESTSGRID_nRecordCount) % (subRequestsgrid_fnc_Recordsperpage( )))) == 0 )
            {
               REQUESTSGRID_nFirstRecordOnPage = (long)(REQUESTSGRID_nRecordCount-subRequestsgrid_fnc_Recordsperpage( ));
            }
            else
            {
               REQUESTSGRID_nFirstRecordOnPage = (long)(REQUESTSGRID_nRecordCount-((long)((REQUESTSGRID_nRecordCount) % (subRequestsgrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            REQUESTSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"REQUESTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(REQUESTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subrequestsgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            REQUESTSGRID_nFirstRecordOnPage = (long)(subRequestsgrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            REQUESTSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"REQUESTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(REQUESTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavRequesturi_Enabled = 0;
         AssignProp(sPrefix, false, edtavRequesturi_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRequesturi_Enabled), 5, 0), true);
         edtavCtlcreatedat_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavCtlamount_Enabled = 0;
         edtavCtlstatus_Enabled = 0;
         edtavCtlreceivedamount_Enabled = 0;
         edtavCtlexpiresat_Enabled = 0;
         edtavGridaddress_Enabled = 0;
         edtavCanceltext_Enabled = 0;
         edtavCtlrequestid_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3M0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E133M2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Requests"), AV13requests);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vREQUESTS"), AV13requests);
            /* Read saved values. */
            nRC_GXsfl_34 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_34"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV16groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV16groupId"));
            REQUESTSGRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"REQUESTSGRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            REQUESTSGRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"REQUESTSGRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_34 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_34"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_34_fel_idx = 0;
            while ( nGXsfl_34_fel_idx < nRC_GXsfl_34 )
            {
               nGXsfl_34_fel_idx = ((subRequestsgrid_Islastpage==1)&&(nGXsfl_34_fel_idx+1>subRequestsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_34_fel_idx+1);
               sGXsfl_34_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_342( ) ;
               AV17GXV1 = (int)(nGXsfl_34_fel_idx+REQUESTSGRID_nFirstRecordOnPage);
               if ( ( AV13requests.Count >= AV17GXV1 ) && ( AV17GXV1 > 0 ) )
               {
                  AV13requests.CurrentItem = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1));
                  AV9gridAddress = cgiGet( edtavGridaddress_Internalname);
                  AV6cancelText = cgiGet( edtavCanceltext_Internalname);
               }
            }
            if ( nGXsfl_34_fel_idx == 0 )
            {
               nGXsfl_34_idx = 1;
               sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
               SubsflControlProps_342( ) ;
            }
            nGXsfl_34_fel_idx = 1;
            /* Read variables values. */
            AV12requestLabel = cgiGet( edtavRequestlabel_Internalname);
            AssignAttri(sPrefix, false, "AV12requestLabel", AV12requestLabel);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavRequestamount_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavRequestamount_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vREQUESTAMOUNT");
               GX_FocusControl = edtavRequestamount_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV11requestAmount = 0;
               AssignAttri(sPrefix, false, "AV11requestAmount", StringUtil.LTrimStr( AV11requestAmount, 16, 8));
            }
            else
            {
               AV11requestAmount = context.localUtil.CToN( cgiGet( edtavRequestamount_Internalname), ".", ",");
               AssignAttri(sPrefix, false, "AV11requestAmount", StringUtil.LTrimStr( AV11requestAmount, 16, 8));
            }
            cmbavExpiresinhours.Name = cmbavExpiresinhours_Internalname;
            cmbavExpiresinhours.CurrentValue = cgiGet( cmbavExpiresinhours_Internalname);
            AV8expiresInHours = (int)(Math.Round(NumberUtil.Val( cgiGet( cmbavExpiresinhours_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV8expiresInHours", StringUtil.LTrimStr( (decimal)(AV8expiresInHours), 5, 0));
            AV14requestUri = cgiGet( edtavRequesturi_Internalname);
            AssignAttri(sPrefix, false, "AV14requestUri", AV14requestUri);
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
         E133M2 ();
         if (returnInSub) return;
      }

      protected void E133M2( )
      {
         /* Start Routine */
         returnInSub = false;
         cmbavExpiresinhours.addItem("0", "Never", 0);
         cmbavExpiresinhours.addItem("1", "In 1 hour", 0);
         cmbavExpiresinhours.addItem("24", "In 24 hours", 0);
         cmbavExpiresinhours.addItem("168", "In 7 days", 0);
         cmbavExpiresinhours.addItem("720", "In 30 days", 0);
         AV8expiresInHours = 0;
         AssignAttri(sPrefix, false, "AV8expiresInHours", StringUtil.LTrimStr( (decimal)(AV8expiresInHours), 5, 0));
         lblTbrequestqr_Caption = "";
         AssignProp(sPrefix, false, lblTbrequestqr_Internalname, "Caption", lblTbrequestqr_Caption, true);
         edtavRequesturi_Visible = 0;
         AssignProp(sPrefix, false, edtavRequesturi_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavRequesturi_Visible), 5, 0), true);
      }

      protected void E143M2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.getpaymentrequests(context ).execute(  AV16groupId, out  AV13requests, out  AV7error) ;
         gx_BV34 = true;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The payment requests can not be read",(string)AV7error}, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13requests", AV13requests);
      }

      private void E153M2( )
      {
         /* Requestsgrid_Load Routine */
         returnInSub = false;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV13requests.Count )
         {
            AV13requests.CurrentItem = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1));
            AV9gridAddress = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPaymentRequest)(AV13requests.CurrentItem)).gxTpr_Address);
            AssignAttri(sPrefix, false, edtavGridaddress_Internalname, AV9gridAddress);
            edtavGridaddress_Tooltiptext = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPaymentRequest)(AV13requests.CurrentItem)).gxTpr_Address);
            if ( StringUtil.Len( AV9gridAddress) > 24 )
            {
               AV9gridAddress = StringUtil.Substring( AV9gridAddress, 1, 14) + "..." + StringUtil.Substring( AV9gridAddress, StringUtil.Len( AV9gridAddress)-7, 8);
               AssignAttri(sPrefix, false, edtavGridaddress_Internalname, AV9gridAddress);
            }
            if ( StringUtil.StrCmp(((GeneXus.Programs.wallet.SdtPaymentRequest)(AV13requests.CurrentItem)).gxTpr_Status, "Pending") == 0 )
            {
               AV6cancelText = "Cancel";
               AssignAttri(sPrefix, false, edtavCanceltext_Internalname, AV6cancelText);
            }
            else
            {
               AV6cancelText = "";
               AssignAttri(sPrefix, false, edtavCanceltext_Internalname, AV6cancelText);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 34;
            }
            if ( ( subRequestsgrid_Islastpage == 1 ) || ( 50 == 0 ) || ( ( REQUESTSGRID_nCurrentRecord >= REQUESTSGRID_nFirstRecordOnPage ) && ( REQUESTSGRID_nCurrentRecord < REQUESTSGRID_nFirstRecordOnPage + subRequestsgrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_342( ) ;
            }
            REQUESTSGRID_nEOF = (short)(((subRequestsgrid_Rows==0)||(REQUESTSGRID_nCurrentRecord<REQUESTSGRID_nFirstRecordOnPage+subRequestsgrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"REQUESTSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(REQUESTSGRID_nEOF), 1, 0, ".", "")));
            REQUESTSGRID_nCurrentRecord = (long)(REQUESTSGRID_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_34_Refreshing )
            {
               DoAjaxLoad(34, RequestsgridRow);
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E113M2( )
      {
         /* 'Create Request' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV12requestLabel))) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Label needed",(string)"Type a label (who pays or what for) for the payment request"}, true);
         }
         else
         {
            new GeneXus.Programs.wallet.createpaymentrequest(context ).execute(  AV16groupId,  AV12requestLabel,  AV11requestAmount,  AV8expiresInHours, out  AV10request, out  AV7error) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
            {
               AV15shownUri = StringUtil.Trim( AV10request.gxTpr_Uri);
               AssignAttri(sPrefix, false, "AV15shownUri", AV15shownUri);
               /* Execute user subroutine: 'SHOW REQUEST' */
               S112 ();
               if (returnInSub) return;
               AV12requestLabel = "";
               AssignAttri(sPrefix, false, "AV12requestLabel", AV12requestLabel);
               AV11requestAmount = 0;
               AssignAttri(sPrefix, false, "AV11requestAmount", StringUtil.LTrimStr( AV11requestAmount, 16, 8));
               AV8expiresInHours = 0;
               AssignAttri(sPrefix, false, "AV8expiresInHours", StringUtil.LTrimStr( (decimal)(AV8expiresInHours), 5, 0));
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Payment request created","The address "+StringUtil.Trim( AV10request.gxTpr_Address)+" is reserved for this request"}, true);
               context.DoAjaxRefreshCmp(sPrefix);
            }
            else
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The payment request was not created",(string)AV7error}, true);
            }
         }
         /*  Sending Event outputs  */
         cmbavExpiresinhours.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV8expiresInHours), 5, 0));
         AssignProp(sPrefix, false, cmbavExpiresinhours_Internalname, "Values", cmbavExpiresinhours.ToJavascriptSource(), true);
         if ( gx_BV34 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13requests", AV13requests);
            nGXsfl_34_bak_idx = nGXsfl_34_idx;
            gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
            nGXsfl_34_idx = nGXsfl_34_bak_idx;
            sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
            SubsflControlProps_342( ) ;
         }
      }

      protected void E163M2( )
      {
         AV17GXV1 = (int)(nGXsfl_34_idx+REQUESTSGRID_nFirstRecordOnPage);
         if ( ( AV17GXV1 > 0 ) && ( AV13requests.Count >= AV17GXV1 ) )
         {
            AV13requests.CurrentItem = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1));
         }
         /* 'Show Request' Routine */
         returnInSub = false;
         AV15shownUri = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPaymentRequest)(AV13requests.CurrentItem)).gxTpr_Uri);
         AssignAttri(sPrefix, false, "AV15shownUri", AV15shownUri);
         /* Execute user subroutine: 'SHOW REQUEST' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E173M2( )
      {
         AV17GXV1 = (int)(nGXsfl_34_idx+REQUESTSGRID_nFirstRecordOnPage);
         if ( ( AV17GXV1 > 0 ) && ( AV13requests.Count >= AV17GXV1 ) )
         {
            AV13requests.CurrentItem = ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1));
         }
         /* 'Cancel Request' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(((GeneXus.Programs.wallet.SdtPaymentRequest)(AV13requests.CurrentItem)).gxTpr_Status, "Pending") == 0 )
         {
            new GeneXus.Programs.wallet.cancelpaymentrequest(context ).execute(  AV16groupId,  ((GeneXus.Programs.wallet.SdtPaymentRequest)(AV13requests.CurrentItem)).gxTpr_Requestid, out  AV7error) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Payment request cancelled",(string)"Its address can be offered again"}, true);
               lblTbrequestqr_Caption = "";
               AssignProp(sPrefix, false, lblTbrequestqr_Internalname, "Caption", lblTbrequestqr_Caption, true);
               edtavRequesturi_Visible = 0;
               AssignProp(sPrefix, false, edtavRequesturi_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavRequesturi_Visible), 5, 0), true);
               context.DoAjaxRefreshCmp(sPrefix);
            }
            else
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The payment request was not cancelled",(string)AV7error}, true);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13requests", AV13requests);
         nGXsfl_34_bak_idx = nGXsfl_34_idx;
         gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
         nGXsfl_34_idx = nGXsfl_34_bak_idx;
         sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
         SubsflControlProps_342( ) ;
      }

      protected void E123M2( )
      {
         /* 'Reload' Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV34 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13requests", AV13requests);
            nGXsfl_34_bak_idx = nGXsfl_34_idx;
            gxgrRequestsgrid_refresh( subRequestsgrid_Rows, AV16groupId, AV13requests, sPrefix) ;
            nGXsfl_34_idx = nGXsfl_34_bak_idx;
            sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
            SubsflControlProps_342( ) ;
         }
      }

      protected void S112( )
      {
         /* 'SHOW REQUEST' Routine */
         returnInSub = false;
         AV14requestUri = StringUtil.Trim( AV15shownUri);
         AssignAttri(sPrefix, false, "AV14requestUri", AV14requestUri);
         GXt_char1 = AV5addresImageText;
         new GeneXus.Programs.qrcoder.generateqrcodestring(context ).execute(  StringUtil.Trim( AV15shownUri), out  GXt_char1) ;
         AV5addresImageText = GXt_char1;
         lblTbrequestqr_Caption = "<img src=\"data:image/png;base64, "+AV5addresImageText+"   \" style=\"width: 250px\" />";
         AssignProp(sPrefix, false, lblTbrequestqr_Internalname, "Caption", lblTbrequestqr_Caption, true);
         edtavRequesturi_Visible = 1;
         AssignProp(sPrefix, false, edtavRequesturi_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavRequesturi_Visible), 5, 0), true);
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV16groupId = (Guid)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV16groupId", AV16groupId.ToString());
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
         PA3M2( ) ;
         WS3M2( ) ;
         WE3M2( ) ;
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
         sCtrlAV16groupId = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA3M2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\paymentrequestswc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA3M2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV16groupId = (Guid)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV16groupId", AV16groupId.ToString());
         }
         wcpOAV16groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV16groupId"));
         if ( ! GetJustCreated( ) && ( ( AV16groupId != wcpOAV16groupId ) ) )
         {
            setjustcreated();
         }
         wcpOAV16groupId = AV16groupId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV16groupId = cgiGet( sPrefix+"AV16groupId_CTRL");
         if ( StringUtil.Len( sCtrlAV16groupId) > 0 )
         {
            AV16groupId = StringUtil.StrToGuid( cgiGet( sCtrlAV16groupId));
            AssignAttri(sPrefix, false, "AV16groupId", AV16groupId.ToString());
         }
         else
         {
            AV16groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"AV16groupId_PARM"));
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
         PA3M2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS3M2( ) ;
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
         WS3M2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV16groupId_PARM", AV16groupId.ToString());
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV16groupId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV16groupId_CTRL", StringUtil.RTrim( sCtrlAV16groupId));
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
         WE3M2( ) ;
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
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2026103013253321", true, true, false);
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
         context.AddJavascriptSource("wallet/paymentrequestswc.js", "?2026103013253321", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_342( )
      {
         edtavCtlcreatedat_Internalname = sPrefix+"CTLCREATEDAT_"+sGXsfl_34_idx;
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL_"+sGXsfl_34_idx;
         edtavCtlamount_Internalname = sPrefix+"CTLAMOUNT_"+sGXsfl_34_idx;
         edtavCtlstatus_Internalname = sPrefix+"CTLSTATUS_"+sGXsfl_34_idx;
         edtavCtlreceivedamount_Internalname = sPrefix+"CTLRECEIVEDAMOUNT_"+sGXsfl_34_idx;
         edtavCtlexpiresat_Internalname = sPrefix+"CTLEXPIRESAT_"+sGXsfl_34_idx;
         edtavGridaddress_Internalname = sPrefix+"vGRIDADDRESS_"+sGXsfl_34_idx;
         edtavCanceltext_Internalname = sPrefix+"vCANCELTEXT_"+sGXsfl_34_idx;
         edtavCtlrequestid_Internalname = sPrefix+"CTLREQUESTID_"+sGXsfl_34_idx;
      }

      protected void SubsflControlProps_fel_342( )
      {
         edtavCtlcreatedat_Internalname = sPrefix+"CTLCREATEDAT_"+sGXsfl_34_fel_idx;
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL_"+sGXsfl_34_fel_idx;
         edtavCtlamount_Internalname = sPrefix+"CTLAMOUNT_"+sGXsfl_34_fel_idx;
         edtavCtlstatus_Internalname = sPrefix+"CTLSTATUS_"+sGXsfl_34_fel_idx;
         edtavCtlreceivedamount_Internalname = sPrefix+"CTLRECEIVEDAMOUNT_"+sGXsfl_34_fel_idx;
         edtavCtlexpiresat_Internalname = sPrefix+"CTLEXPIRESAT_"+sGXsfl_34_fel_idx;
         edtavGridaddress_Internalname = sPrefix+"vGRIDADDRESS_"+sGXsfl_34_fel_idx;
         edtavCanceltext_Internalname = sPrefix+"vCANCELTEXT_"+sGXsfl_34_fel_idx;
         edtavCtlrequestid_Internalname = sPrefix+"CTLREQUESTID_"+sGXsfl_34_fel_idx;
      }

      protected void sendrow_342( )
      {
         sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
         SubsflControlProps_342( ) ;
         WB3M0( ) ;
         if ( ( 50 * 1 == 0 ) || ( nGXsfl_34_idx <= subRequestsgrid_fnc_Recordsperpage( ) * 1 ) )
         {
            RequestsgridRow = GXWebRow.GetNew(context,RequestsgridContainer);
            if ( subRequestsgrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subRequestsgrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subRequestsgrid_Class, "") != 0 )
               {
                  subRequestsgrid_Linesclass = subRequestsgrid_Class+"Odd";
               }
            }
            else if ( subRequestsgrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subRequestsgrid_Backstyle = 0;
               subRequestsgrid_Backcolor = subRequestsgrid_Allbackcolor;
               if ( StringUtil.StrCmp(subRequestsgrid_Class, "") != 0 )
               {
                  subRequestsgrid_Linesclass = subRequestsgrid_Class+"Uniform";
               }
            }
            else if ( subRequestsgrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subRequestsgrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subRequestsgrid_Class, "") != 0 )
               {
                  subRequestsgrid_Linesclass = subRequestsgrid_Class+"Odd";
               }
               subRequestsgrid_Backcolor = (int)(0x0);
            }
            else if ( subRequestsgrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subRequestsgrid_Backstyle = 1;
               if ( ((int)((nGXsfl_34_idx) % (2))) == 0 )
               {
                  subRequestsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subRequestsgrid_Class, "") != 0 )
                  {
                     subRequestsgrid_Linesclass = subRequestsgrid_Class+"Even";
                  }
               }
               else
               {
                  subRequestsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subRequestsgrid_Class, "") != 0 )
                  {
                     subRequestsgrid_Linesclass = subRequestsgrid_Class+"Odd";
                  }
               }
            }
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_34_idx+"\">") ;
            }
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',34)\"";
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcreatedat_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Createdat, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Createdat, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onblur(this,35);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcreatedat_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcreatedat_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)34,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',34)\"";
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtllabel_Internalname,((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Label,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,36);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtllabel_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtllabel_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)34,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',34)\"";
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlamount_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Amount, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlamount_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Amount, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Amount, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,37);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlamount_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlamount_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)34,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',34)\"";
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlstatus_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Status),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,38);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlstatus_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlstatus_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)34,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',34)\"";
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlreceivedamount_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Receivedamount, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlreceivedamount_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Receivedamount, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Receivedamount, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,39);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlreceivedamount_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlreceivedamount_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)34,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',34)\"";
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlexpiresat_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Expiresat, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Expiresat, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onblur(this,40);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlexpiresat_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlexpiresat_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)34,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 41,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',34)\"";
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGridaddress_Internalname,StringUtil.RTrim( AV9gridAddress),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,41);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SHOW REQUEST\\'."+sGXsfl_34_idx+"'",(string)"",(string)"",(string)edtavGridaddress_Tooltiptext,(string)"",(string)edtavGridaddress_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavGridaddress_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)34,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'" + sPrefix + "',false,'" + sGXsfl_34_idx + "',34)\"";
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCanceltext_Internalname,StringUtil.RTrim( AV6cancelText),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,42);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CANCEL REQUEST\\'."+sGXsfl_34_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCanceltext_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCanceltext_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)34,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( RequestsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            RequestsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlrequestid_Internalname,((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Requestid.ToString(),((GeneXus.Programs.wallet.SdtPaymentRequest)AV13requests.Item(AV17GXV1)).gxTpr_Requestid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlrequestid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlrequestid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)34,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
            send_integrity_lvl_hashes3M2( ) ;
            RequestsgridContainer.AddRow(RequestsgridRow);
            nGXsfl_34_idx = ((subRequestsgrid_Islastpage==1)&&(nGXsfl_34_idx+1>subRequestsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_34_idx+1);
            sGXsfl_34_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_34_idx), 4, 0), 4, "0");
            SubsflControlProps_342( ) ;
         }
         /* End function sendrow_342 */
      }

      protected void init_web_controls( )
      {
         cmbavExpiresinhours.Name = "vEXPIRESINHOURS";
         cmbavExpiresinhours.WebTags = "";
         if ( cmbavExpiresinhours.ItemCount > 0 )
         {
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl34( )
      {
         if ( RequestsgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"RequestsgridContainer"+"DivS\" data-gxgridid=\"34\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subRequestsgrid_Internalname, subRequestsgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subRequestsgrid_Backcolorstyle == 0 )
            {
               subRequestsgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subRequestsgrid_Class) > 0 )
               {
                  subRequestsgrid_Linesclass = subRequestsgrid_Class+"Title";
               }
            }
            else
            {
               subRequestsgrid_Titlebackstyle = 1;
               if ( subRequestsgrid_Backcolorstyle == 1 )
               {
                  subRequestsgrid_Titlebackcolor = subRequestsgrid_Allbackcolor;
                  if ( StringUtil.Len( subRequestsgrid_Class) > 0 )
                  {
                     subRequestsgrid_Linesclass = subRequestsgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subRequestsgrid_Class) > 0 )
                  {
                     subRequestsgrid_Linesclass = subRequestsgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Created") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Label") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Amount") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Status") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Received") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Expires") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Address (click for the QR)") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "request Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            RequestsgridContainer.AddObjectProperty("GridName", "Requestsgrid");
         }
         else
         {
            RequestsgridContainer.AddObjectProperty("GridName", "Requestsgrid");
            RequestsgridContainer.AddObjectProperty("Header", subRequestsgrid_Header);
            RequestsgridContainer.AddObjectProperty("Class", "Grid");
            RequestsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Backcolorstyle), 1, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("CmpContext", sPrefix);
            RequestsgridContainer.AddObjectProperty("InMasterPage", "false");
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcreatedat_Enabled), 5, 0, ".", "")));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtllabel_Enabled), 5, 0, ".", "")));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlamount_Enabled), 5, 0, ".", "")));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlstatus_Enabled), 5, 0, ".", "")));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlreceivedamount_Enabled), 5, 0, ".", "")));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlexpiresat_Enabled), 5, 0, ".", "")));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV9gridAddress)));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavGridaddress_Enabled), 5, 0, ".", "")));
            RequestsgridColumn.AddObjectProperty("Tooltiptext", StringUtil.RTrim( edtavGridaddress_Tooltiptext));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV6cancelText)));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCanceltext_Enabled), 5, 0, ".", "")));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            RequestsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlrequestid_Enabled), 5, 0, ".", "")));
            RequestsgridContainer.AddColumnProperties(RequestsgridColumn);
            RequestsgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Selectedindex), 4, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Allowselection), 1, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Selectioncolor), 9, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Allowhovering), 1, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Hoveringcolor), 9, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Allowcollapsing), 1, 0, ".", "")));
            RequestsgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subRequestsgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblTbrequestsinfo_Internalname = sPrefix+"TBREQUESTSINFO";
         edtavRequestlabel_Internalname = sPrefix+"vREQUESTLABEL";
         edtavRequestamount_Internalname = sPrefix+"vREQUESTAMOUNT";
         cmbavExpiresinhours_Internalname = sPrefix+"vEXPIRESINHOURS";
         bttCreaterequest_Internalname = sPrefix+"CREATEREQUEST";
         bttReload_Internalname = sPrefix+"RELOAD";
         edtavRequesturi_Internalname = sPrefix+"vREQUESTURI";
         lblTbrequestqr_Internalname = sPrefix+"TBREQUESTQR";
         edtavCtlcreatedat_Internalname = sPrefix+"CTLCREATEDAT";
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL";
         edtavCtlamount_Internalname = sPrefix+"CTLAMOUNT";
         edtavCtlstatus_Internalname = sPrefix+"CTLSTATUS";
         edtavCtlreceivedamount_Internalname = sPrefix+"CTLRECEIVEDAMOUNT";
         edtavCtlexpiresat_Internalname = sPrefix+"CTLEXPIRESAT";
         edtavGridaddress_Internalname = sPrefix+"vGRIDADDRESS";
         edtavCanceltext_Internalname = sPrefix+"vCANCELTEXT";
         edtavCtlrequestid_Internalname = sPrefix+"CTLREQUESTID";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subRequestsgrid_Internalname = sPrefix+"REQUESTSGRID";
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
         subRequestsgrid_Allowcollapsing = 0;
         subRequestsgrid_Allowselection = 0;
         subRequestsgrid_Header = "";
         edtavCtlrequestid_Jsonclick = "";
         edtavCtlrequestid_Enabled = 0;
         edtavCanceltext_Jsonclick = "";
         edtavCanceltext_Enabled = 1;
         edtavGridaddress_Jsonclick = "";
         edtavGridaddress_Tooltiptext = "";
         edtavGridaddress_Enabled = 1;
         edtavCtlexpiresat_Jsonclick = "";
         edtavCtlexpiresat_Enabled = 0;
         edtavCtlreceivedamount_Jsonclick = "";
         edtavCtlreceivedamount_Enabled = 0;
         edtavCtlstatus_Jsonclick = "";
         edtavCtlstatus_Enabled = 0;
         edtavCtlamount_Jsonclick = "";
         edtavCtlamount_Enabled = 0;
         edtavCtllabel_Jsonclick = "";
         edtavCtllabel_Enabled = 0;
         edtavCtlcreatedat_Jsonclick = "";
         edtavCtlcreatedat_Enabled = 0;
         subRequestsgrid_Class = "Grid";
         subRequestsgrid_Backcolorstyle = 0;
         lblTbrequestqr_Caption = "TBRequestQR";
         edtavRequesturi_Enabled = 1;
         edtavRequesturi_Visible = 1;
         cmbavExpiresinhours_Jsonclick = "";
         cmbavExpiresinhours.Enabled = 1;
         edtavRequestamount_Jsonclick = "";
         edtavRequestamount_Enabled = 1;
         edtavRequestlabel_Jsonclick = "";
         edtavRequestlabel_Enabled = 1;
         edtavCtlrequestid_Enabled = -1;
         edtavCtlexpiresat_Enabled = -1;
         edtavCtlreceivedamount_Enabled = -1;
         edtavCtlstatus_Enabled = -1;
         edtavCtlamount_Enabled = -1;
         edtavCtllabel_Enabled = -1;
         edtavCtlcreatedat_Enabled = -1;
         subRequestsgrid_Rows = 50;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"REQUESTSGRID_nEOF","type":"int"},{"av":"subRequestsgrid_Rows","ctrl":"REQUESTSGRID","prop":"Rows"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV16groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]}""");
         setEventMetadata("REQUESTSGRID.LOAD","""{"handler":"E153M2","iparms":[{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]""");
         setEventMetadata("REQUESTSGRID.LOAD",""","oparms":[{"av":"AV9gridAddress","fld":"vGRIDADDRESS","type":"char"},{"av":"edtavGridaddress_Tooltiptext","ctrl":"vGRIDADDRESS","prop":"Tooltiptext"},{"av":"AV6cancelText","fld":"vCANCELTEXT","type":"char"}]}""");
         setEventMetadata("'CREATE REQUEST'","""{"handler":"E113M2","iparms":[{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"REQUESTSGRID_nEOF","type":"int"},{"av":"subRequestsgrid_Rows","ctrl":"REQUESTSGRID","prop":"Rows"},{"av":"AV16groupId","fld":"vGROUPID","type":"guid"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV12requestLabel","fld":"vREQUESTLABEL","type":"svchar"},{"av":"AV11requestAmount","fld":"vREQUESTAMOUNT","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"cmbavExpiresinhours"},{"av":"AV8expiresInHours","fld":"vEXPIRESINHOURS","pic":"ZZZZ9","type":"int"},{"av":"AV15shownUri","fld":"vSHOWNURI","type":"svchar"}]""");
         setEventMetadata("'CREATE REQUEST'",""","oparms":[{"av":"AV15shownUri","fld":"vSHOWNURI","type":"svchar"},{"av":"AV12requestLabel","fld":"vREQUESTLABEL","type":"svchar"},{"av":"AV11requestAmount","fld":"vREQUESTAMOUNT","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"cmbavExpiresinhours"},{"av":"AV8expiresInHours","fld":"vEXPIRESINHOURS","pic":"ZZZZ9","type":"int"},{"av":"AV14requestUri","fld":"vREQUESTURI","type":"svchar"},{"av":"lblTbrequestqr_Caption","ctrl":"TBREQUESTQR","prop":"Caption"},{"av":"edtavRequesturi_Visible","ctrl":"vREQUESTURI","prop":"Visible"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]}""");
         setEventMetadata("'SHOW REQUEST'","""{"handler":"E163M2","iparms":[{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"AV15shownUri","fld":"vSHOWNURI","type":"svchar"}]""");
         setEventMetadata("'SHOW REQUEST'",""","oparms":[{"av":"AV15shownUri","fld":"vSHOWNURI","type":"svchar"},{"av":"AV14requestUri","fld":"vREQUESTURI","type":"svchar"},{"av":"lblTbrequestqr_Caption","ctrl":"TBREQUESTQR","prop":"Caption"},{"av":"edtavRequesturi_Visible","ctrl":"vREQUESTURI","prop":"Visible"}]}""");
         setEventMetadata("'CANCEL REQUEST'","""{"handler":"E173M2","iparms":[{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"REQUESTSGRID_nEOF","type":"int"},{"av":"subRequestsgrid_Rows","ctrl":"REQUESTSGRID","prop":"Rows"},{"av":"AV16groupId","fld":"vGROUPID","type":"guid"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'CANCEL REQUEST'",""","oparms":[{"av":"lblTbrequestqr_Caption","ctrl":"TBREQUESTQR","prop":"Caption"},{"av":"edtavRequesturi_Visible","ctrl":"vREQUESTURI","prop":"Visible"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]}""");
         setEventMetadata("'RELOAD'","""{"handler":"E123M2","iparms":[{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"REQUESTSGRID_nEOF","type":"int"},{"av":"subRequestsgrid_Rows","ctrl":"REQUESTSGRID","prop":"Rows"},{"av":"AV16groupId","fld":"vGROUPID","type":"guid"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'RELOAD'",""","oparms":[{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]}""");
         setEventMetadata("REQUESTSGRID_FIRSTPAGE","""{"handler":"subrequestsgrid_firstpage","iparms":[{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"REQUESTSGRID_nEOF","type":"int"},{"av":"subRequestsgrid_Rows","ctrl":"REQUESTSGRID","prop":"Rows"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV16groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("REQUESTSGRID_FIRSTPAGE",""","oparms":[{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]}""");
         setEventMetadata("REQUESTSGRID_PREVPAGE","""{"handler":"subrequestsgrid_previouspage","iparms":[{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"REQUESTSGRID_nEOF","type":"int"},{"av":"subRequestsgrid_Rows","ctrl":"REQUESTSGRID","prop":"Rows"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV16groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("REQUESTSGRID_PREVPAGE",""","oparms":[{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]}""");
         setEventMetadata("REQUESTSGRID_NEXTPAGE","""{"handler":"subrequestsgrid_nextpage","iparms":[{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"REQUESTSGRID_nEOF","type":"int"},{"av":"subRequestsgrid_Rows","ctrl":"REQUESTSGRID","prop":"Rows"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV16groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("REQUESTSGRID_NEXTPAGE",""","oparms":[{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]}""");
         setEventMetadata("REQUESTSGRID_LASTPAGE","""{"handler":"subrequestsgrid_lastpage","iparms":[{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"REQUESTSGRID_nEOF","type":"int"},{"av":"subRequestsgrid_Rows","ctrl":"REQUESTSGRID","prop":"Rows"},{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV16groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("REQUESTSGRID_LASTPAGE",""","oparms":[{"av":"AV13requests","fld":"vREQUESTS","grid":34,"type":""},{"av":"nGXsfl_34_idx","ctrl":"GRID","prop":"GridCurrRow","grid":34},{"av":"REQUESTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_34","ctrl":"REQUESTSGRID","prop":"GridRC","grid":34,"type":"int"}]}""");
         setEventMetadata("VALIDV_GXV8","""{"handler":"Validv_Gxv8","iparms":[]}""");
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
         wcpOAV16groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV13requests = new GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest>( context, "PaymentRequest", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV15shownUri = "";
         GX_FocusControl = "";
         lblTbrequestsinfo_Jsonclick = "";
         TempTags = "";
         AV12requestLabel = "";
         ClassString = "";
         StyleString = "";
         bttCreaterequest_Jsonclick = "";
         bttReload_Jsonclick = "";
         AV14requestUri = "";
         lblTbrequestqr_Jsonclick = "";
         RequestsgridContainer = new GXWebGrid( context);
         sStyleString = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV9gridAddress = "";
         AV6cancelText = "";
         AV7error = "";
         RequestsgridRow = new GXWebRow();
         AV10request = new GeneXus.Programs.wallet.SdtPaymentRequest(context);
         AV5addresImageText = "";
         GXt_char1 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV16groupId = "";
         subRequestsgrid_Linesclass = "";
         ROClassString = "";
         RequestsgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavRequesturi_Enabled = 0;
         edtavCtlcreatedat_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavCtlamount_Enabled = 0;
         edtavCtlstatus_Enabled = 0;
         edtavCtlreceivedamount_Enabled = 0;
         edtavCtlexpiresat_Enabled = 0;
         edtavGridaddress_Enabled = 0;
         edtavCanceltext_Enabled = 0;
         edtavCtlrequestid_Enabled = 0;
      }

      private short REQUESTSGRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subRequestsgrid_Backcolorstyle ;
      private short nGXWrapped ;
      private short subRequestsgrid_Backstyle ;
      private short subRequestsgrid_Titlebackstyle ;
      private short subRequestsgrid_Allowselection ;
      private short subRequestsgrid_Allowhovering ;
      private short subRequestsgrid_Allowcollapsing ;
      private short subRequestsgrid_Collapsed ;
      private int nRC_GXsfl_34 ;
      private int subRequestsgrid_Rows ;
      private int nGXsfl_34_idx=1 ;
      private int edtavRequesturi_Enabled ;
      private int edtavCtlcreatedat_Enabled ;
      private int edtavCtllabel_Enabled ;
      private int edtavCtlamount_Enabled ;
      private int edtavCtlstatus_Enabled ;
      private int edtavCtlreceivedamount_Enabled ;
      private int edtavCtlexpiresat_Enabled ;
      private int edtavGridaddress_Enabled ;
      private int edtavCanceltext_Enabled ;
      private int edtavCtlrequestid_Enabled ;
      private int edtavRequestlabel_Enabled ;
      private int edtavRequestamount_Enabled ;
      private int AV8expiresInHours ;
      private int edtavRequesturi_Visible ;
      private int AV17GXV1 ;
      private int subRequestsgrid_Islastpage ;
      private int REQUESTSGRID_nGridOutOfScope ;
      private int nGXsfl_34_fel_idx=1 ;
      private int nGXsfl_34_bak_idx=1 ;
      private int idxLst ;
      private int subRequestsgrid_Backcolor ;
      private int subRequestsgrid_Allbackcolor ;
      private int subRequestsgrid_Titlebackcolor ;
      private int subRequestsgrid_Selectedindex ;
      private int subRequestsgrid_Selectioncolor ;
      private int subRequestsgrid_Hoveringcolor ;
      private long REQUESTSGRID_nFirstRecordOnPage ;
      private long REQUESTSGRID_nCurrentRecord ;
      private long REQUESTSGRID_nRecordCount ;
      private decimal AV11requestAmount ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_34_idx="0001" ;
      private string edtavRequesturi_Internalname ;
      private string edtavCtlcreatedat_Internalname ;
      private string edtavCtllabel_Internalname ;
      private string edtavCtlamount_Internalname ;
      private string edtavCtlstatus_Internalname ;
      private string edtavCtlreceivedamount_Internalname ;
      private string edtavCtlexpiresat_Internalname ;
      private string edtavGridaddress_Internalname ;
      private string edtavCanceltext_Internalname ;
      private string edtavCtlrequestid_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string lblTbrequestsinfo_Internalname ;
      private string lblTbrequestsinfo_Jsonclick ;
      private string edtavRequestlabel_Internalname ;
      private string TempTags ;
      private string edtavRequestlabel_Jsonclick ;
      private string edtavRequestamount_Internalname ;
      private string edtavRequestamount_Jsonclick ;
      private string cmbavExpiresinhours_Internalname ;
      private string cmbavExpiresinhours_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string bttCreaterequest_Internalname ;
      private string bttCreaterequest_Jsonclick ;
      private string bttReload_Internalname ;
      private string bttReload_Jsonclick ;
      private string lblTbrequestqr_Internalname ;
      private string lblTbrequestqr_Caption ;
      private string lblTbrequestqr_Jsonclick ;
      private string sStyleString ;
      private string subRequestsgrid_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV9gridAddress ;
      private string AV6cancelText ;
      private string sGXsfl_34_fel_idx="0001" ;
      private string AV7error ;
      private string edtavGridaddress_Tooltiptext ;
      private string GXt_char1 ;
      private string sCtrlAV16groupId ;
      private string subRequestsgrid_Class ;
      private string subRequestsgrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtlcreatedat_Jsonclick ;
      private string edtavCtllabel_Jsonclick ;
      private string edtavCtlamount_Jsonclick ;
      private string edtavCtlstatus_Jsonclick ;
      private string edtavCtlreceivedamount_Jsonclick ;
      private string edtavCtlexpiresat_Jsonclick ;
      private string edtavGridaddress_Jsonclick ;
      private string edtavCanceltext_Jsonclick ;
      private string edtavCtlrequestid_Jsonclick ;
      private string subRequestsgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_34_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool gx_BV34 ;
      private string AV5addresImageText ;
      private string AV15shownUri ;
      private string AV12requestLabel ;
      private string AV14requestUri ;
      private Guid AV16groupId ;
      private Guid wcpOAV16groupId ;
      private GXWebGrid RequestsgridContainer ;
      private GXWebRow RequestsgridRow ;
      private GXWebColumn RequestsgridColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavExpiresinhours ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPaymentRequest> AV13requests ;
      private GeneXus.Programs.wallet.SdtPaymentRequest AV10request ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
