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
   public class sendcoinsbounty : GXHttpHandler
   {
      public sendcoinsbounty( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendcoinsbounty( IGxContext context )
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

      protected override void createObjects( )
      {
         chkavSendallcoins = new GXCheckbox();
         cmbavUserfee = new GXCombobox();
         chkavActivatemanaulfee = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
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
            ValidateSpaRequest();
            PA2S2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavTotalbalance_Enabled = 0;
               AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
               chkavSendallcoins.Enabled = 0;
               AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
               edtavSendcoins_Enabled = 0;
               AssignProp("", false, edtavSendcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendcoins_Enabled), 5, 0), true);
               WS2S2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  WE2S2( ) ;
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
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( "Send Coins Bounty") ;
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
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 310420), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 310420), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 310420), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.sendcoinsbounty") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV38wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV38wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV38wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV15group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV15group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUP_SDT", GetSecureSignedToken( "", AV15group_sdt, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"SendCoinsBounty");
         forbiddenHiddens.Add("totalBalance", context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\sendcoinsbounty:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV12error));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV38wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV38wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV38wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV15group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV15group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUP_SDT", GetSecureSignedToken( "", AV15group_sdt, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vTRANSACTIONSTOSEND", AV34transactionsToSend);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vTRANSACTIONSTOSEND", AV34transactionsToSend);
         }
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV23PopupName));
      }

      protected void RenderHtmlCloseForm2S2( )
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
         context.WriteHtmlTextNl( "</body>") ;
         context.WriteHtmlTextNl( "</html>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.SendCoinsBounty" ;
      }

      public override string GetPgmdesc( )
      {
         return "Send Coins Bounty" ;
      }

      protected void WB2S0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            RenderHtmlHeaders( ) ;
            RenderHtmlOpenForm( ) ;
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTotalbalance_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTotalbalance_Internalname, "Your total Balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTotalbalance_Internalname, StringUtil.LTrim( StringUtil.NToC( AV30totalBalance, 16, 8, ".", "")), StringUtil.LTrim( ((edtavTotalbalance_Enabled!=0) ? context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999") : context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,8);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotalbalance_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTotalbalance_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsBounty.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSendallcoins_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSendallcoins_Internalname, "Send total balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSendallcoins_Internalname, StringUtil.BoolToStr( AV24sendAllCoins), "", "Send total balance", 1, chkavSendallcoins.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(12, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,12);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSendcoins_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSendcoins_Internalname, "Amount to send (in BTC)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSendcoins_Internalname, StringUtil.LTrim( StringUtil.NToC( AV25sendCoins, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV25sendCoins, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,17);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSendcoins_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSendcoins_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsBounty.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSendto_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSendto_Internalname, "Send to address", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavSendto_Internalname, StringUtil.RTrim( AV26sendTo), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,22);\"", 0, 1, edtavSendto_Enabled, 1, 80, "chr", 2, "row", 0, StyleString, ClassString, "", "", "250", 1, 0, "", "", -1, true, "NBitcoin\\scriptPubKey_address", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendCoinsBounty.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDescription_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDescription_Internalname, "Description", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 27,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavDescription_Internalname, AV9description, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,27);\"", 0, 1, edtavDescription_Enabled, 1, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendCoinsBounty.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavUserfee.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavUserfee_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavUserfee_Internalname, "Select  Fee", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUserfee, cmbavUserfee_Internalname, StringUtil.Trim( StringUtil.Str( AV35userFee, 16, 8)), 1, cmbavUserfee_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "decimal", "", cmbavUserfee.Visible, cmbavUserfee.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"", "", true, 0, "HLP_Wallet/registered/SendCoinsBounty.htm");
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV35userFee, 16, 8));
            AssignProp("", false, cmbavUserfee_Internalname, "Values", (string)(cmbavUserfee.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavActivatemanaulfee.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavActivatemanaulfee_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavActivatemanaulfee_Internalname, "Manauly select Fee", "col-sm-9 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-3 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavActivatemanaulfee_Internalname, StringUtil.BoolToStr( AV6activateManaulFee), "", "Manauly select Fee", chkavActivatemanaulfee.Visible, chkavActivatemanaulfee.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(39, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,39);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavManaulfee_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavManaulfee_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 43,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavManaulfee_Internalname, StringUtil.LTrim( StringUtil.NToC( AV19manaulFee, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV19manaulFee, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,43);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavManaulfee_Jsonclick, 0, "Attribute", "", "", "", "", edtavManaulfee_Visible, edtavManaulfee_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsBounty.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 46,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttNext_Internalname, "", "Next", bttNext_Jsonclick, 5, "Next", "", StyleString, ClassString, bttNext_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'NEXT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsBounty.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 49,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSendcoins_Internalname, "", "Send Coins", bttSendcoins_Jsonclick, 5, "Send Coins", "", StyleString, ClassString, bttSendcoins_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'SEND COINS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsBounty.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 52,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel", bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsBounty.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
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
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_13-186676", 0) ;
            }
         }
         Form.Meta.addItem("description", "Send Coins Bounty", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP2S0( ) ;
      }

      protected void WS2S2( )
      {
         START2S2( ) ;
         EVT2S2( ) ;
      }

      protected void EVT2S2( )
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
                           E112S2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'NEXT'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Next' */
                           E122S2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'SEND COINS'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Send Coins' */
                           E132S2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Cancel' */
                           E142S2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           E152S2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Load */
                           E162S2 ();
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
                  context.wbHandled = 1;
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
               GX_FocusControl = edtavTotalbalance_Internalname;
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
         AV24sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV24sendAllCoins));
         AssignAttri("", false, "AV24sendAllCoins", AV24sendAllCoins);
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV35userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV35userFee, 16, 8))), ".");
            AssignAttri("", false, "AV35userFee", StringUtil.LTrimStr( AV35userFee, 16, 8));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV35userFee, 16, 8));
            AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
         }
         AV6activateManaulFee = StringUtil.StrToBool( StringUtil.BoolToStr( AV6activateManaulFee));
         AssignAttri("", false, "AV6activateManaulFee", AV6activateManaulFee);
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
         edtavTotalbalance_Enabled = 0;
         AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         chkavSendallcoins.Enabled = 0;
         AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
         edtavSendcoins_Enabled = 0;
         AssignProp("", false, edtavSendcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendcoins_Enabled), 5, 0), true);
      }

      protected void RF2S2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E162S2 ();
            WB2S0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes2S2( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV38wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV38wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV38wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV15group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV15group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUP_SDT", GetSecureSignedToken( "", AV15group_sdt, context));
      }

      protected void before_start_formulas( )
      {
         edtavTotalbalance_Enabled = 0;
         AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         chkavSendallcoins.Enabled = 0;
         AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
         edtavSendcoins_Enabled = 0;
         AssignProp("", false, edtavSendcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendcoins_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2S0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E112S2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            if ( ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vTOTALBALANCE");
               GX_FocusControl = edtavTotalbalance_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV30totalBalance = 0;
               AssignAttri("", false, "AV30totalBalance", StringUtil.LTrimStr( AV30totalBalance, 16, 8));
               GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"), context));
            }
            else
            {
               AV30totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
               AssignAttri("", false, "AV30totalBalance", StringUtil.LTrimStr( AV30totalBalance, 16, 8));
               GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"), context));
            }
            AV24sendAllCoins = StringUtil.StrToBool( cgiGet( chkavSendallcoins_Internalname));
            AssignAttri("", false, "AV24sendAllCoins", AV24sendAllCoins);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vSENDCOINS");
               GX_FocusControl = edtavSendcoins_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV25sendCoins = 0;
               AssignAttri("", false, "AV25sendCoins", StringUtil.LTrimStr( AV25sendCoins, 16, 8));
            }
            else
            {
               AV25sendCoins = context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",");
               AssignAttri("", false, "AV25sendCoins", StringUtil.LTrimStr( AV25sendCoins, 16, 8));
            }
            AV26sendTo = cgiGet( edtavSendto_Internalname);
            AssignAttri("", false, "AV26sendTo", AV26sendTo);
            AV9description = cgiGet( edtavDescription_Internalname);
            AssignAttri("", false, "AV9description", AV9description);
            cmbavUserfee.CurrentValue = cgiGet( cmbavUserfee_Internalname);
            AV35userFee = NumberUtil.Val( cgiGet( cmbavUserfee_Internalname), ".");
            AssignAttri("", false, "AV35userFee", StringUtil.LTrimStr( AV35userFee, 16, 8));
            AV6activateManaulFee = StringUtil.StrToBool( cgiGet( chkavActivatemanaulfee_Internalname));
            AssignAttri("", false, "AV6activateManaulFee", AV6activateManaulFee);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vMANAULFEE");
               GX_FocusControl = edtavManaulfee_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV19manaulFee = 0;
               AssignAttri("", false, "AV19manaulFee", StringUtil.LTrimStr( AV19manaulFee, 16, 8));
            }
            else
            {
               AV19manaulFee = context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",");
               AssignAttri("", false, "AV19manaulFee", StringUtil.LTrimStr( AV19manaulFee, 16, 8));
            }
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"SendCoinsBounty");
            AV30totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
            AssignAttri("", false, "AV30totalBalance", StringUtil.LTrimStr( AV30totalBalance, 16, 8));
            GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"), context));
            forbiddenHiddens.Add("totalBalance", context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("wallet\\registered\\sendcoinsbounty:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
               GxWebError = 1;
               context.HttpContext.Response.StatusCode = 403;
               context.WriteHtmlText( "<title>403 Forbidden</title>") ;
               context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
               context.WriteHtmlText( "<p /><hr />") ;
               GXUtil.WriteLog("send_http_error_code " + 403.ToString());
               return  ;
            }
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E112S2 ();
         if (returnInSub) return;
      }

      protected void E112S2( )
      {
         /* Start Routine */
         returnInSub = false;
         edtavManaulfee_Visible = 0;
         AssignProp("", false, edtavManaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Visible), 5, 0), true);
         chkavActivatemanaulfee.Visible = 0;
         AssignProp("", false, chkavActivatemanaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavActivatemanaulfee.Visible), 5, 0), true);
         bttSendcoins_Visible = 0;
         AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
         cmbavUserfee.Visible = 0;
         AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
         GXt_SdtWallet1 = AV38wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV38wallet = GXt_SdtWallet1;
         GXt_SdtKeyInfo2 = AV18keyInfo;
         new GeneXus.Programs.wallet.getkey(context ).execute( out  GXt_SdtKeyInfo2) ;
         AV18keyInfo = GXt_SdtKeyInfo2;
         GXt_objcol_SdtSDTAddressHistory3 = AV17historyWithBalance;
         new GeneXus.Programs.wallet.gethistorywithbalance(context ).execute( out  GXt_objcol_SdtSDTAddressHistory3) ;
         AV17historyWithBalance = GXt_objcol_SdtSDTAddressHistory3;
         GXt_decimal4 = AV30totalBalance;
         new GeneXus.Programs.wallet.getbalancefromhistorywithbalance(context ).execute( out  GXt_decimal4) ;
         AV30totalBalance = GXt_decimal4;
         AssignAttri("", false, "AV30totalBalance", StringUtil.LTrimStr( AV30totalBalance, 16, 8));
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV30totalBalance, "ZZZZZZ9.99999999"), context));
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         AV15group_sdt.FromJSonString(AV39websession.Get("Group_EDIT_WALLET"), null);
         chkavSendallcoins.Enabled = 0;
         AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
         AV24sendAllCoins = true;
         AssignAttri("", false, "AV24sendAllCoins", AV24sendAllCoins);
         AV25sendCoins = AV30totalBalance;
         AssignAttri("", false, "AV25sendCoins", StringUtil.LTrimStr( AV25sendCoins, 16, 8));
      }

      protected void E122S2( )
      {
         /* 'Next' Routine */
         returnInSub = false;
         if ( ( AV25sendCoins >= AV30totalBalance ) && ! AV24sendAllCoins )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You don't have enough balance",(string)AV12error}, true);
         }
         else
         {
            if ( (Convert.ToDecimal(0)==AV25sendCoins) )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You have to select a amount to send",(string)AV12error}, true);
            }
            else
            {
               GXt_char5 = AV12error;
               new GeneXus.Programs.nbitcoin.isaddressvalid(context ).execute(  AV26sendTo,  AV38wallet.gxTpr_Networktype, out  GXt_char5) ;
               AV12error = GXt_char5;
               AssignAttri("", false, "AV12error", AV12error);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Please check the Send to address: ",(string)AV12error}, true);
               }
               else
               {
                  context.PopUp(formatLink("wallet.approvespending") , new Object[] {});
               }
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E132S2( )
      {
         /* 'Send Coins' Routine */
         returnInSub = false;
         if ( ! (Convert.ToDecimal(0)==AV19manaulFee) )
         {
            GXt_char5 = AV12error;
            new GeneXus.Programs.wallet.registered.buildtransactiontimebackup(context ).execute(  AV15group_sdt,  AV19manaulFee,  AV38wallet.gxTpr_Networktype,  AV25sendCoins,  AV26sendTo, ref  AV34transactionsToSend, out  AV37virtualSize, out  AV16hexTransaction, out  AV36verified, out  GXt_char5) ;
            AV12error = GXt_char5;
            AssignAttri("", false, "AV12error", AV12error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
               if ( ! AV36verified )
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem verifying the transaction: ",(string)AV12error}, true);
               }
               else
               {
                  GXt_char5 = AV12error;
                  new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV16hexTransaction, out  AV33TransactionId, out  GXt_char5) ;
                  AV12error = GXt_char5;
                  AssignAttri("", false, "AV12error", AV12error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                  {
                     bttSendcoins_Visible = 0;
                     AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
                     cmbavUserfee.Enabled = 0;
                     AssignProp("", false, cmbavUserfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Enabled), 5, 0), true);
                     edtavManaulfee_Enabled = 0;
                     AssignProp("", false, edtavManaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Enabled), 5, 0), true);
                     chkavActivatemanaulfee.Enabled = 0;
                     AssignProp("", false, chkavActivatemanaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavActivatemanaulfee.Enabled), 5, 0), true);
                     AV32transactionFileName = StringUtil.Trim( AV15group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
                     GXt_char5 = AV12error;
                     new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV32transactionFileName,  AV33TransactionId,  AV34transactionsToSend, out  GXt_char5) ;
                     AV12error = GXt_char5;
                     AssignAttri("", false, "AV12error", AV12error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Coins submitted succesfuly",(string)AV12error}, true);
                        context.setWebReturnParms(new Object[] {});
                        context.setWebReturnParmsMetadata(new Object[] {});
                        context.wjLocDisableFrm = 1;
                        context.nUserReturn = 1;
                        returnInSub = true;
                        if (true) return;
                     }
                     else
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"",(string)AV12error}, true);
                     }
                  }
                  else
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem submiting the transaction: ",(string)AV12error}, true);
                  }
               }
            }
            else
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem building the final transaction: ",(string)AV12error}, true);
            }
         }
         else
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Plese select an Estimated Transaction Fee to pay: ",(string)AV12error}, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV34transactionsToSend", AV34transactionsToSend);
      }

      protected void E142S2( )
      {
         /* 'Cancel' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         AV39websession.Set("MuSign_ONE", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      protected void E152S2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV7ApproveSpendingPopupName = "Wallet.ApproveSpending";
         AV29strFound = (short)(StringUtil.StringSearch( AV23PopupName, StringUtil.Lower( AV7ApproveSpendingPopupName), 1));
         if ( AV29strFound > 0 )
         {
            GXt_SdtKeyInfo2 = AV18keyInfo;
            new GeneXus.Programs.wallet.getkey(context ).execute( out  GXt_SdtKeyInfo2) ;
            AV18keyInfo = GXt_SdtKeyInfo2;
            GXt_SdtExtKeyInfo6 = AV5extKeyInfo;
            new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo6) ;
            AV5extKeyInfo = GXt_SdtExtKeyInfo6;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV18keyInfo.gxTpr_Privatekey)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV5extKeyInfo.gxTpr_Privatekey)) )
            {
               edtavSendcoins_Enabled = 0;
               AssignProp("", false, edtavSendcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendcoins_Enabled), 5, 0), true);
               edtavSendto_Enabled = 0;
               AssignProp("", false, edtavSendto_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendto_Enabled), 5, 0), true);
               edtavDescription_Enabled = 0;
               AssignProp("", false, edtavDescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDescription_Enabled), 5, 0), true);
               bttNext_Visible = 0;
               AssignProp("", false, bttNext_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttNext_Visible), 5, 0), true);
               bttSendcoins_Visible = 1;
               AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
               AV31transactionFee = NumberUtil.Val( "0.00001000", ".");
               GXt_objcol_SdtSDTAddressHistory3 = AV34transactionsToSend;
               new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV9description,  AV25sendCoins,  AV31transactionFee, out  GXt_objcol_SdtSDTAddressHistory3) ;
               AV34transactionsToSend = GXt_objcol_SdtSDTAddressHistory3;
               GXt_char5 = AV12error;
               new GeneXus.Programs.wallet.getrawtransfromcoinstosend(context ).execute( ref  AV34transactionsToSend, out  GXt_char5) ;
               AV12error = GXt_char5;
               AssignAttri("", false, "AV12error", AV12error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  AV31transactionFee = 0;
                  GXt_char5 = AV12error;
                  new GeneXus.Programs.wallet.registered.buildtransactiontimebackup(context ).execute(  AV15group_sdt,  AV31transactionFee,  AV38wallet.gxTpr_Networktype,  AV25sendCoins,  AV26sendTo, ref  AV34transactionsToSend, out  AV37virtualSize, out  AV16hexTransaction, out  AV36verified, out  GXt_char5) ;
                  AV12error = GXt_char5;
                  AssignAttri("", false, "AV12error", AV12error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                  {
                     bttNext_Visible = 0;
                     AssignProp("", false, bttNext_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttNext_Visible), 5, 0), true);
                     bttSendcoins_Visible = 1;
                     AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
                     GXt_char5 = AV12error;
                     new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV37virtualSize,  60,  "economical", out  AV11economicalFee, out  AV10economicalBlocks, out  GXt_char5) ;
                     AV12error = GXt_char5;
                     AssignAttri("", false, "AV12error", AV12error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                     {
                        GXt_char5 = AV12error;
                        new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV37virtualSize,  6,  "conservative", out  AV28standardFee, out  AV27standarBlocks, out  GXt_char5) ;
                        AV12error = GXt_char5;
                        AssignAttri("", false, "AV12error", AV12error);
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                        {
                           GXt_char5 = AV12error;
                           new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV37virtualSize,  1,  "conservative", out  AV14fastestFee, out  AV13fastestBlocks, out  GXt_char5) ;
                           AV12error = GXt_char5;
                           AssignAttri("", false, "AV12error", AV12error);
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                           {
                              cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(0), 16, 8)), "Select Estimated Transaction Fee", 0);
                              cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV11economicalFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV11economicalFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV10economicalBlocks), 4, 0)+" Blocks", 0);
                              cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV28standardFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV28standardFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV27standarBlocks), 4, 0)+" Blocks", 0);
                              cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV14fastestFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV14fastestFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV13fastestBlocks), 4, 0)+" Blocks", 0);
                              cmbavUserfee.Visible = 1;
                              AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
                              edtavManaulfee_Visible = 1;
                              AssignProp("", false, edtavManaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Visible), 5, 0), true);
                              edtavManaulfee_Enabled = 0;
                              AssignProp("", false, edtavManaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Enabled), 5, 0), true);
                              chkavActivatemanaulfee.Visible = 1;
                              AssignProp("", false, chkavActivatemanaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavActivatemanaulfee.Visible), 5, 0), true);
                           }
                           else
                           {
                              this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculatin fastest fee: ",(string)AV12error}, true);
                              new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                           }
                        }
                        else
                        {
                           this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculatin standar fee: ",(string)AV12error}, true);
                           new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                        }
                     }
                     else
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculatin economical fee: ",(string)AV12error}, true);
                        new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                     }
                  }
                  else
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem building the estimated transaction fee: ",(string)AV12error}, true);
                     new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                  }
               }
               else
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem getting the Raw Transaction from the server: ",(string)AV12error}, true);
                  new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV34transactionsToSend", AV34transactionsToSend);
         cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV35userFee, 16, 8));
         AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
      }

      protected void nextLoad( )
      {
      }

      protected void E162S2( )
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
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20257241623367", true, true);
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
         context.AddJavascriptSource("gxdec.js", "?"+context.GetBuildNumber( 310420), false, true);
         context.AddJavascriptSource("wallet/registered/sendcoinsbounty.js", "?20257241623367", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         chkavSendallcoins.Name = "vSENDALLCOINS";
         chkavSendallcoins.WebTags = "";
         chkavSendallcoins.Caption = "Send total balance";
         AssignProp("", false, chkavSendallcoins_Internalname, "TitleCaption", chkavSendallcoins.Caption, true);
         chkavSendallcoins.CheckedValue = "false";
         AV24sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV24sendAllCoins));
         AssignAttri("", false, "AV24sendAllCoins", AV24sendAllCoins);
         cmbavUserfee.Name = "vUSERFEE";
         cmbavUserfee.WebTags = "";
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV35userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV35userFee, 16, 8))), ".");
            AssignAttri("", false, "AV35userFee", StringUtil.LTrimStr( AV35userFee, 16, 8));
         }
         chkavActivatemanaulfee.Name = "vACTIVATEMANAULFEE";
         chkavActivatemanaulfee.WebTags = "";
         chkavActivatemanaulfee.Caption = "Manauly select Fee";
         AssignProp("", false, chkavActivatemanaulfee_Internalname, "TitleCaption", chkavActivatemanaulfee.Caption, true);
         chkavActivatemanaulfee.CheckedValue = "false";
         AV6activateManaulFee = StringUtil.StrToBool( StringUtil.BoolToStr( AV6activateManaulFee));
         AssignAttri("", false, "AV6activateManaulFee", AV6activateManaulFee);
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavTotalbalance_Internalname = "vTOTALBALANCE";
         chkavSendallcoins_Internalname = "vSENDALLCOINS";
         edtavSendcoins_Internalname = "vSENDCOINS";
         edtavSendto_Internalname = "vSENDTO";
         edtavDescription_Internalname = "vDESCRIPTION";
         cmbavUserfee_Internalname = "vUSERFEE";
         chkavActivatemanaulfee_Internalname = "vACTIVATEMANAULFEE";
         edtavManaulfee_Internalname = "vMANAULFEE";
         divTable1_Internalname = "TABLE1";
         bttNext_Internalname = "NEXT";
         bttSendcoins_Internalname = "SENDCOINS";
         bttCancel_Internalname = "CANCEL";
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
         chkavActivatemanaulfee.Caption = "Manauly select Fee";
         chkavSendallcoins.Caption = "Send total balance";
         bttSendcoins_Visible = 1;
         bttNext_Visible = 1;
         edtavManaulfee_Jsonclick = "";
         edtavManaulfee_Enabled = 1;
         edtavManaulfee_Visible = 1;
         chkavActivatemanaulfee.Enabled = 1;
         chkavActivatemanaulfee.Visible = 1;
         cmbavUserfee_Jsonclick = "";
         cmbavUserfee.Enabled = 1;
         cmbavUserfee.Visible = 1;
         edtavDescription_Enabled = 1;
         edtavSendto_Enabled = 1;
         edtavSendcoins_Jsonclick = "";
         edtavSendcoins_Enabled = 1;
         chkavSendallcoins.Enabled = 1;
         edtavTotalbalance_Jsonclick = "";
         edtavTotalbalance_Enabled = 1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV24sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV6activateManaulFee","fld":"vACTIVATEMANAULFEE","type":"boolean"},{"av":"AV38wallet","fld":"vWALLET","hsh":true,"type":""},{"av":"AV15group_sdt","fld":"vGROUP_SDT","hsh":true,"type":""},{"av":"AV30totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","hsh":true,"type":"decimal"}]}""");
         setEventMetadata("'NEXT'","""{"handler":"E122S2","iparms":[{"av":"AV25sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV30totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","hsh":true,"type":"decimal"},{"av":"AV24sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"AV26sendTo","fld":"vSENDTO","type":"char"},{"av":"AV38wallet","fld":"vWALLET","hsh":true,"type":""}]""");
         setEventMetadata("'NEXT'",""","oparms":[{"av":"AV12error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'SEND COINS'","""{"handler":"E132S2","iparms":[{"av":"AV19manaulFee","fld":"vMANAULFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV15group_sdt","fld":"vGROUP_SDT","hsh":true,"type":""},{"av":"AV38wallet","fld":"vWALLET","hsh":true,"type":""},{"av":"AV25sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV26sendTo","fld":"vSENDTO","type":"char"},{"av":"AV34transactionsToSend","fld":"vTRANSACTIONSTOSEND","type":""},{"av":"AV12error","fld":"vERROR","type":"char"}]""");
         setEventMetadata("'SEND COINS'",""","oparms":[{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"AV34transactionsToSend","fld":"vTRANSACTIONSTOSEND","type":""},{"ctrl":"SENDCOINS","prop":"Visible"},{"av":"cmbavUserfee"},{"av":"edtavManaulfee_Enabled","ctrl":"vMANAULFEE","prop":"Enabled"},{"av":"chkavActivatemanaulfee.Enabled","ctrl":"vACTIVATEMANAULFEE","prop":"Enabled"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E142S2","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E152S2","iparms":[{"av":"AV23PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV9description","fld":"vDESCRIPTION","type":"svchar"},{"av":"AV25sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV15group_sdt","fld":"vGROUP_SDT","hsh":true,"type":""},{"av":"AV38wallet","fld":"vWALLET","hsh":true,"type":""},{"av":"AV26sendTo","fld":"vSENDTO","type":"char"},{"av":"cmbavUserfee"},{"av":"AV35userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"edtavSendcoins_Enabled","ctrl":"vSENDCOINS","prop":"Enabled"},{"av":"edtavSendto_Enabled","ctrl":"vSENDTO","prop":"Enabled"},{"av":"edtavDescription_Enabled","ctrl":"vDESCRIPTION","prop":"Enabled"},{"ctrl":"NEXT","prop":"Visible"},{"ctrl":"SENDCOINS","prop":"Visible"},{"av":"AV34transactionsToSend","fld":"vTRANSACTIONSTOSEND","type":""},{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"cmbavUserfee"},{"av":"AV35userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"edtavManaulfee_Visible","ctrl":"vMANAULFEE","prop":"Visible"},{"av":"edtavManaulfee_Enabled","ctrl":"vMANAULFEE","prop":"Enabled"},{"av":"chkavActivatemanaulfee.Visible","ctrl":"vACTIVATEMANAULFEE","prop":"Visible"}]}""");
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
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         AV38wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV15group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV12error = "";
         AV34transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV23PopupName = "";
         GX_FocusControl = "";
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV26sendTo = "";
         AV9description = "";
         bttNext_Jsonclick = "";
         bttSendcoins_Jsonclick = "";
         bttCancel_Jsonclick = "";
         Form = new GXWebForm();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         hsh = "";
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV18keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV17historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV39websession = context.GetSession();
         AV16hexTransaction = "";
         AV33TransactionId = "";
         AV32transactionFileName = "";
         AV7ApproveSpendingPopupName = "";
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV5extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo6 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_objcol_SdtSDTAddressHistory3 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_char5 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
         edtavTotalbalance_Enabled = 0;
         chkavSendallcoins.Enabled = 0;
         edtavSendcoins_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short AV29strFound ;
      private short AV10economicalBlocks ;
      private short AV27standarBlocks ;
      private short AV13fastestBlocks ;
      private short nGXWrapped ;
      private int edtavTotalbalance_Enabled ;
      private int edtavSendcoins_Enabled ;
      private int edtavSendto_Enabled ;
      private int edtavDescription_Enabled ;
      private int edtavManaulfee_Visible ;
      private int edtavManaulfee_Enabled ;
      private int bttNext_Visible ;
      private int bttSendcoins_Visible ;
      private int idxLst ;
      private long AV37virtualSize ;
      private decimal AV30totalBalance ;
      private decimal AV25sendCoins ;
      private decimal AV35userFee ;
      private decimal AV19manaulFee ;
      private decimal GXt_decimal4 ;
      private decimal AV31transactionFee ;
      private decimal AV11economicalFee ;
      private decimal AV28standardFee ;
      private decimal AV14fastestFee ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string edtavTotalbalance_Internalname ;
      private string chkavSendallcoins_Internalname ;
      private string edtavSendcoins_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV12error ;
      private string AV23PopupName ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string edtavTotalbalance_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string edtavSendcoins_Jsonclick ;
      private string edtavSendto_Internalname ;
      private string AV26sendTo ;
      private string edtavDescription_Internalname ;
      private string cmbavUserfee_Internalname ;
      private string cmbavUserfee_Jsonclick ;
      private string divTable1_Internalname ;
      private string chkavActivatemanaulfee_Internalname ;
      private string edtavManaulfee_Internalname ;
      private string edtavManaulfee_Jsonclick ;
      private string bttNext_Internalname ;
      private string bttNext_Jsonclick ;
      private string bttSendcoins_Internalname ;
      private string bttSendcoins_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string hsh ;
      private string AV33TransactionId ;
      private string AV32transactionFileName ;
      private string AV7ApproveSpendingPopupName ;
      private string GXt_char5 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool AV24sendAllCoins ;
      private bool AV6activateManaulFee ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV36verified ;
      private string AV16hexTransaction ;
      private string AV9description ;
      private IGxSession AV39websession ;
      private GXProperties forbiddenHiddens ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavSendallcoins ;
      private GXCombobox cmbavUserfee ;
      private GXCheckbox chkavActivatemanaulfee ;
      private GeneXus.Programs.wallet.SdtWallet AV38wallet ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV34transactionsToSend ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV18keyInfo ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV17historyWithBalance ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV5extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo6 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory3 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
