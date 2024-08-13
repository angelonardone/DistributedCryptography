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
   public class sendcoinsmusig : GXHttpHandler
   {
      public sendcoinsmusig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendcoinsmusig( IGxContext context )
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
            PA1W2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavTotalbalance_Enabled = 0;
               AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
               WS1W2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  WE1W2( ) ;
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
         context.SendWebValue( "Send Coins using Multisignature") ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1218140), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1218140), false, true);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.sendcoinsmusig.aspx") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV28wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV28wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV28wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV31group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV31group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUP_SDT", GetSecureSignedToken( "", AV31group_sdt, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEMUSIGSIGNATURES", AV35oneMuSigSignatures);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEMUSIGSIGNATURES", AV35oneMuSigSignatures);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEMUSIGSIGNATURES", GetSecureSignedToken( "", AV35oneMuSigSignatures, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"SendCoinsMuSig");
         forbiddenHiddens.Add("totalBalance", context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\sendcoinsmusig:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV10error));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV28wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV28wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV28wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV31group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV31group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUP_SDT", GetSecureSignedToken( "", AV31group_sdt, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vTRANSACTIONSTOSEND", AV25transactionsToSend);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vTRANSACTIONSTOSEND", AV25transactionsToSend);
         }
         GxWebStd.gx_hidden_field( context, "vCHANGETO", StringUtil.RTrim( AV7changeTo));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEMUSIGSIGNATURES", AV35oneMuSigSignatures);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEMUSIGSIGNATURES", AV35oneMuSigSignatures);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEMUSIGSIGNATURES", GetSecureSignedToken( "", AV35oneMuSigSignatures, context));
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV16PopupName));
      }

      protected void RenderHtmlCloseForm1W2( )
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
         return "Wallet.registered.SendCoinsMuSig" ;
      }

      public override string GetPgmdesc( )
      {
         return "Send Coins using Multisignature" ;
      }

      protected void WB1W0( )
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
            GxWebStd.gx_single_line_edit( context, edtavTotalbalance_Internalname, StringUtil.LTrim( StringUtil.NToC( AV23totalBalance, 16, 8, ".", "")), StringUtil.LTrim( ((edtavTotalbalance_Enabled!=0) ? context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999") : context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,8);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotalbalance_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTotalbalance_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsMuSig.htm");
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
            GxWebStd.gx_checkbox_ctrl( context, chkavSendallcoins_Internalname, StringUtil.BoolToStr( AV17sendAllCoins), "", "Send total balance", 1, chkavSendallcoins.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(12, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,12);\"");
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
            GxWebStd.gx_single_line_edit( context, edtavSendcoins_Internalname, StringUtil.LTrim( StringUtil.NToC( AV18sendCoins, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV18sendCoins, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,17);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSendcoins_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSendcoins_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsMuSig.htm");
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
            GxWebStd.gx_html_textarea( context, edtavSendto_Internalname, StringUtil.RTrim( AV19sendTo), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,22);\"", 0, 1, edtavSendto_Enabled, 1, 80, "chr", 2, "row", 0, StyleString, ClassString, "", "", "250", 1, 0, "", "", -1, true, "NBitcoin\\scriptPubKey_address", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendCoinsMuSig.htm");
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
            GxWebStd.gx_html_textarea( context, edtavDescription_Internalname, AV33description, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,27);\"", 0, 1, edtavDescription_Enabled, 1, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendCoinsMuSig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavUserfee.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavUserfee_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavUserfee_Internalname, "Select  Fee", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUserfee, cmbavUserfee_Internalname, StringUtil.Trim( StringUtil.Str( AV26userFee, 16, 8)), 1, cmbavUserfee_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "decimal", "", cmbavUserfee.Visible, cmbavUserfee.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"", "", true, 0, "HLP_Wallet/registered/SendCoinsMuSig.htm");
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV26userFee, 16, 8));
            AssignProp("", false, cmbavUserfee_Internalname, "Values", (string)(cmbavUserfee.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttNext_Internalname, "", "Next", bttNext_Jsonclick, 5, "Next", "", StyleString, ClassString, bttNext_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'NEXT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsMuSig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSendcoins_Internalname, "", "Send Coins", bttSendcoins_Jsonclick, 5, "Send Coins", "", StyleString, ClassString, bttSendcoins_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'SEND COINS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsMuSig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 41,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel", bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsMuSig.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START1W2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
            }
         }
         Form.Meta.addItem("description", "Send Coins using Multisignature", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP1W0( ) ;
      }

      protected void WS1W2( )
      {
         START1W2( ) ;
         EVT1W2( ) ;
      }

      protected void EVT1W2( )
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
                           E111W2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'NEXT'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Next' */
                           E121W2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'SEND COINS'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Send Coins' */
                           E131W2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Cancel' */
                           E141W2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           E151W2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Load */
                           E161W2 ();
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

      protected void WE1W2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm1W2( ) ;
            }
         }
      }

      protected void PA1W2( )
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
         AV17sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV17sendAllCoins));
         AssignAttri("", false, "AV17sendAllCoins", AV17sendAllCoins);
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV26userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV26userFee, 16, 8))), ".");
            AssignAttri("", false, "AV26userFee", StringUtil.LTrimStr( AV26userFee, 16, 8));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV26userFee, 16, 8));
            AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF1W2( ) ;
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
      }

      protected void RF1W2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E161W2 ();
            WB1W0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes1W2( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV28wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV28wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV28wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV31group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV31group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUP_SDT", GetSecureSignedToken( "", AV31group_sdt, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONEMUSIGSIGNATURES", AV35oneMuSigSignatures);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONEMUSIGSIGNATURES", AV35oneMuSigSignatures);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONEMUSIGSIGNATURES", GetSecureSignedToken( "", AV35oneMuSigSignatures, context));
      }

      protected void before_start_formulas( )
      {
         edtavTotalbalance_Enabled = 0;
         AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1W0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E111W2 ();
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
               AV23totalBalance = 0;
               AssignAttri("", false, "AV23totalBalance", StringUtil.LTrimStr( AV23totalBalance, 16, 8));
               GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"), context));
            }
            else
            {
               AV23totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
               AssignAttri("", false, "AV23totalBalance", StringUtil.LTrimStr( AV23totalBalance, 16, 8));
               GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"), context));
            }
            AV17sendAllCoins = StringUtil.StrToBool( cgiGet( chkavSendallcoins_Internalname));
            AssignAttri("", false, "AV17sendAllCoins", AV17sendAllCoins);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vSENDCOINS");
               GX_FocusControl = edtavSendcoins_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV18sendCoins = 0;
               AssignAttri("", false, "AV18sendCoins", StringUtil.LTrimStr( AV18sendCoins, 16, 8));
            }
            else
            {
               AV18sendCoins = context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",");
               AssignAttri("", false, "AV18sendCoins", StringUtil.LTrimStr( AV18sendCoins, 16, 8));
            }
            AV19sendTo = cgiGet( edtavSendto_Internalname);
            AssignAttri("", false, "AV19sendTo", AV19sendTo);
            AV33description = cgiGet( edtavDescription_Internalname);
            AssignAttri("", false, "AV33description", AV33description);
            cmbavUserfee.CurrentValue = cgiGet( cmbavUserfee_Internalname);
            AV26userFee = NumberUtil.Val( cgiGet( cmbavUserfee_Internalname), ".");
            AssignAttri("", false, "AV26userFee", StringUtil.LTrimStr( AV26userFee, 16, 8));
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"SendCoinsMuSig");
            AV23totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
            AssignAttri("", false, "AV23totalBalance", StringUtil.LTrimStr( AV23totalBalance, 16, 8));
            GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"), context));
            forbiddenHiddens.Add("totalBalance", context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("wallet\\registered\\sendcoinsmusig:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
         E111W2 ();
         if (returnInSub) return;
      }

      protected void E111W2( )
      {
         /* Start Routine */
         returnInSub = false;
         bttSendcoins_Visible = 0;
         AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
         cmbavUserfee.Visible = 0;
         AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
         GXt_SdtWallet1 = AV28wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV28wallet = GXt_SdtWallet1;
         GXt_SdtKeyInfo2 = AV15keyInfo;
         new GeneXus.Programs.wallet.getkey(context ).execute( out  GXt_SdtKeyInfo2) ;
         AV15keyInfo = GXt_SdtKeyInfo2;
         GXt_objcol_SdtSDTAddressHistory3 = AV14historyWithBalance;
         new GeneXus.Programs.wallet.gethistorywithbalance(context ).execute( out  GXt_objcol_SdtSDTAddressHistory3) ;
         AV14historyWithBalance = GXt_objcol_SdtSDTAddressHistory3;
         GXt_decimal4 = AV23totalBalance;
         new GeneXus.Programs.wallet.getbalancefromhistorywithbalance(context ).execute( out  GXt_decimal4) ;
         AV23totalBalance = GXt_decimal4;
         AssignAttri("", false, "AV23totalBalance", StringUtil.LTrimStr( AV23totalBalance, 16, 8));
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV23totalBalance, "ZZZZZZ9.99999999"), context));
         new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
         AV31group_sdt.FromJSonString(AV30websession.Get("Group_EDIT"), null);
         AV35oneMuSigSignatures.FromJSonString(AV30websession.Get("MuSign_ONE"), null);
         if ( ! (Guid.Empty==AV35oneMuSigSignatures.gxTpr_Id) )
         {
            AV17sendAllCoins = AV35oneMuSigSignatures.gxTpr_Sendallcoins;
            AssignAttri("", false, "AV17sendAllCoins", AV17sendAllCoins);
            chkavSendallcoins.Enabled = 0;
            AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
            AV18sendCoins = AV35oneMuSigSignatures.gxTpr_Sendcoins;
            AssignAttri("", false, "AV18sendCoins", StringUtil.LTrimStr( AV18sendCoins, 16, 8));
            edtavSendcoins_Enabled = 0;
            AssignProp("", false, edtavSendcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendcoins_Enabled), 5, 0), true);
            AV19sendTo = StringUtil.Trim( AV35oneMuSigSignatures.gxTpr_Sendto);
            AssignAttri("", false, "AV19sendTo", AV19sendTo);
            edtavSendto_Enabled = 0;
            AssignProp("", false, edtavSendto_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendto_Enabled), 5, 0), true);
            AV33description = StringUtil.Trim( AV35oneMuSigSignatures.gxTpr_Description);
            AssignAttri("", false, "AV33description", AV33description);
            edtavDescription_Enabled = 0;
            AssignProp("", false, edtavDescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDescription_Enabled), 5, 0), true);
            AV7changeTo = StringUtil.Trim( AV35oneMuSigSignatures.gxTpr_Changeto);
            AssignAttri("", false, "AV7changeTo", AV7changeTo);
            AV25transactionsToSend = AV35oneMuSigSignatures.gxTpr_Transactions;
         }
      }

      protected void E121W2( )
      {
         /* 'Next' Routine */
         returnInSub = false;
         if ( ( AV18sendCoins >= AV23totalBalance ) && ! AV17sendAllCoins )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You don't have enough balance",(string)AV10error}, true);
         }
         else
         {
            if ( (Convert.ToDecimal(0)==AV18sendCoins) )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You have to select a amount to send",(string)AV10error}, true);
            }
            else
            {
               GXt_char5 = AV10error;
               new GeneXus.Programs.nbitcoin.isaddressvalid(context ).execute(  AV19sendTo,  AV28wallet.gxTpr_Networktype, out  GXt_char5) ;
               AV10error = GXt_char5;
               AssignAttri("", false, "AV10error", AV10error);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Please check the Send to address: ",(string)AV10error}, true);
               }
               else
               {
                  if ( ! AV31group_sdt.gxTpr_Amigroupowner && String.IsNullOrEmpty(StringUtil.RTrim( AV33description)) )
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"You have to enter a description",(string)"The description is mandatory on multisugnature transactions"}, true);
                  }
                  else
                  {
                     context.PopUp(formatLink("wallet.approvespending.aspx") , new Object[] {});
                  }
               }
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E131W2( )
      {
         /* 'Send Coins' Routine */
         returnInSub = false;
         if ( ! (Convert.ToDecimal(0)==AV26userFee) )
         {
            GXt_char5 = AV10error;
            new GeneXus.Programs.wallet.buildtransactionmusig(context ).execute(  true,  AV31group_sdt,  AV17sendAllCoins,  AV26userFee,  AV28wallet.gxTpr_Networktype, ref  AV25transactionsToSend,  AV18sendCoins,  AV19sendTo,  AV7changeTo, out  AV27virtualSize, out  AV13hexTransaction, out  AV34verified, out  GXt_char5) ;
            AV10error = GXt_char5;
            AssignAttri("", false, "AV10error", AV10error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
            {
               new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
               GXt_char5 = AV10error;
               new GeneXus.Programs.wallet.sendrawtransaction(context ).execute(  AV13hexTransaction, out  AV29TransactionId, out  GXt_char5) ;
               AV10error = GXt_char5;
               AssignAttri("", false, "AV10error", AV10error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
               {
                  bttSendcoins_Visible = 0;
                  AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
                  cmbavUserfee.Enabled = 0;
                  AssignProp("", false, cmbavUserfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Enabled), 5, 0), true);
                  AV32transactionFileName = StringUtil.Trim( AV31group_sdt.gxTpr_Groupid.ToString()) + ".gtrn";
                  GXt_char5 = AV10error;
                  new GeneXus.Programs.wallet.updatetransactionsaftercoinsent(context ).execute(  AV32transactionFileName,  AV29TransactionId, ref  AV25transactionsToSend, out  GXt_char5) ;
                  AV10error = GXt_char5;
                  AssignAttri("", false, "AV10error", AV10error);
                  if ( ! (Guid.Empty==AV35oneMuSigSignatures.gxTpr_Id) )
                  {
                     GXt_char5 = AV10error;
                     new GeneXus.Programs.wallet.registered.updategroupandsendsignature(context ).execute(  AV31group_sdt,  AV17sendAllCoins,  AV25transactionsToSend,  AV18sendCoins,  AV19sendTo,  AV7changeTo,  AV35oneMuSigSignatures.gxTpr_Id,  AV34verified, out  GXt_char5) ;
                     AV10error = GXt_char5;
                     AssignAttri("", false, "AV10error", AV10error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Multisignature transaction/s completed: ",(string)"The transaction/s is/are properly finished and we've notified all the participants"}, true);
                        AV30websession.Set("MuSign_ONE", "");
                        context.setWebReturnParms(new Object[] {});
                        context.setWebReturnParmsMetadata(new Object[] {});
                        context.wjLocDisableFrm = 1;
                        context.nUserReturn = 1;
                        returnInSub = true;
                        if (true) return;
                     }
                     else
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem sending signatures to the group: ",(string)AV10error}, true);
                     }
                  }
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Coins submitted succesfuly",(string)AV10error}, true);
                     context.setWebReturnParms(new Object[] {});
                     context.setWebReturnParmsMetadata(new Object[] {});
                     context.wjLocDisableFrm = 1;
                     context.nUserReturn = 1;
                     returnInSub = true;
                     if (true) return;
                  }
                  else
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"",(string)AV10error}, true);
                  }
               }
               else
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem submiting the transaction: ",(string)AV10error}, true);
               }
            }
            else
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem building the final transaction: ",(string)AV10error}, true);
            }
         }
         else
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Plese select an Estimated Transaction Fee to pay: ",(string)AV10error}, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV25transactionsToSend", AV25transactionsToSend);
      }

      protected void E141W2( )
      {
         /* 'Cancel' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
         AV30websession.Set("MuSign_ONE", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      protected void E151W2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV6ApproveSpendingPopupName = "Wallet.ApproveSpending";
         AV22strFound = (short)(StringUtil.StringSearch( AV16PopupName, StringUtil.Lower( AV6ApproveSpendingPopupName), 1));
         if ( AV22strFound > 0 )
         {
            GXt_SdtKeyInfo2 = AV15keyInfo;
            new GeneXus.Programs.wallet.getkey(context ).execute( out  GXt_SdtKeyInfo2) ;
            AV15keyInfo = GXt_SdtKeyInfo2;
            GXt_SdtExtKeyInfo6 = AV5extKeyInfo;
            new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo6) ;
            AV5extKeyInfo = GXt_SdtExtKeyInfo6;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15keyInfo.gxTpr_Privatekey)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV5extKeyInfo.gxTpr_Privatekey)) )
            {
               if ( (Guid.Empty==AV35oneMuSigSignatures.gxTpr_Id) )
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
                  GXt_objcol_SdtSDTAddressHistory3 = AV25transactionsToSend;
                  new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV33description,  AV18sendCoins, out  GXt_objcol_SdtSDTAddressHistory3) ;
                  AV25transactionsToSend = GXt_objcol_SdtSDTAddressHistory3;
                  GXt_char5 = AV10error;
                  new GeneXus.Programs.wallet.getrawtransfromcoinstosend(context ).execute( ref  AV25transactionsToSend, out  GXt_char5) ;
                  AV10error = GXt_char5;
                  AssignAttri("", false, "AV10error", AV10error);
                  GXt_char5 = AV7changeTo;
                  new GeneXus.Programs.wallet.pulloneaddress(context ).execute(  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), out  GXt_char5) ;
                  AV7changeTo = GXt_char5;
                  AssignAttri("", false, "AV7changeTo", AV7changeTo);
               }
               else
               {
                  GXt_boolean7 = false;
                  if ( ! new GeneXus.Programs.wallet.doesaddressbelongtowallet(context).executeUdp(  StringUtil.Trim( AV7changeTo),  (short)(Math.Round(NumberUtil.Val( "3", "."), 18, MidpointRounding.ToEven)), ref  GXt_boolean7) )
                  {
                     AV10error = "The 'change address' does not belong to this wallet or it has already been used";
                     AssignAttri("", false, "AV10error", AV10error);
                     new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
                  }
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
               {
                  AV24transactionFee = NumberUtil.Val( "0.00001000", ".");
                  GXt_char5 = AV10error;
                  new GeneXus.Programs.wallet.buildtransactionmusig(context ).execute(  false,  AV31group_sdt,  AV17sendAllCoins,  AV24transactionFee,  AV28wallet.gxTpr_Networktype, ref  AV25transactionsToSend,  AV18sendCoins,  AV19sendTo,  AV7changeTo, out  AV27virtualSize, out  AV13hexTransaction, out  AV34verified, out  GXt_char5) ;
                  AV10error = GXt_char5;
                  AssignAttri("", false, "AV10error", AV10error);
                  if ( ! AV34verified && String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) && ! AV31group_sdt.gxTpr_Amigroupowner )
                  {
                     GXt_char5 = AV10error;
                     new GeneXus.Programs.wallet.registered.updategroupandsendsignature(context ).execute(  AV31group_sdt,  AV17sendAllCoins,  AV25transactionsToSend,  AV18sendCoins,  AV19sendTo,  AV7changeTo,  AV35oneMuSigSignatures.gxTpr_Id,  AV34verified, out  GXt_char5) ;
                     AV10error = GXt_char5;
                     AssignAttri("", false, "AV10error", AV10error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Multisignature transaction/s signed: ",(string)"The transaction/s is/are properly signed and we've notified all the participants"}, true);
                        context.setWebReturnParms(new Object[] {});
                        context.setWebReturnParmsMetadata(new Object[] {});
                        context.wjLocDisableFrm = 1;
                        context.nUserReturn = 1;
                        returnInSub = true;
                        if (true) return;
                     }
                     else
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem sending signatures to the group: ",(string)AV10error}, true);
                     }
                  }
                  else
                  {
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                     {
                        bttNext_Visible = 0;
                        AssignProp("", false, bttNext_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttNext_Visible), 5, 0), true);
                        bttSendcoins_Visible = 1;
                        AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
                        GXt_char5 = AV10error;
                        new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV27virtualSize,  60,  "economical", out  AV9economicalFee, out  AV8economicalBlocks, out  GXt_char5) ;
                        AV10error = GXt_char5;
                        AssignAttri("", false, "AV10error", AV10error);
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                        {
                           GXt_char5 = AV10error;
                           new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV27virtualSize,  6,  "conservative", out  AV21standardFee, out  AV20standarBlocks, out  GXt_char5) ;
                           AV10error = GXt_char5;
                           AssignAttri("", false, "AV10error", AV10error);
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                           {
                              GXt_char5 = AV10error;
                              new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV27virtualSize,  1,  "conservative", out  AV12fastestFee, out  AV11fastestBlocks, out  GXt_char5) ;
                              AV10error = GXt_char5;
                              AssignAttri("", false, "AV10error", AV10error);
                              if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
                              {
                                 cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(0), 16, 8)), "Select Estimated Transaction Fee", 0);
                                 cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV9economicalFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV9economicalFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV8economicalBlocks), 4, 0)+" Blocks", 0);
                                 cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV21standardFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV21standardFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV20standarBlocks), 4, 0)+" Blocks", 0);
                                 cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV12fastestFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV12fastestFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV11fastestBlocks), 4, 0)+" Blocks", 0);
                                 cmbavUserfee.Visible = 1;
                                 AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
                              }
                              else
                              {
                                 this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculatin fastest fee: ",(string)AV10error}, true);
                                 new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
                              }
                           }
                           else
                           {
                              this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculatin standar fee: ",(string)AV10error}, true);
                              new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
                           }
                        }
                        else
                        {
                           this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculatin economical fee: ",(string)AV10error}, true);
                           new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
                        }
                     }
                     else
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem building the estimate fee transaction: ",(string)AV10error}, true);
                        new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
                     }
                  }
               }
               else
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem getting the Raw Transaction from the server: ",(string)AV10error}, true);
                  new GeneXus.Programs.wallet.cleanextkey(context ).execute( ) ;
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV25transactionsToSend", AV25transactionsToSend);
         cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV26userFee, 16, 8));
         AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
      }

      protected void nextLoad( )
      {
      }

      protected void E161W2( )
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
         PA1W2( ) ;
         WS1W2( ) ;
         WE1W2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20248131334741", true, true);
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
         context.AddJavascriptSource("gxdec.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("wallet/registered/sendcoinsmusig.js", "?20248131334741", false, true);
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
         AV17sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV17sendAllCoins));
         AssignAttri("", false, "AV17sendAllCoins", AV17sendAllCoins);
         cmbavUserfee.Name = "vUSERFEE";
         cmbavUserfee.WebTags = "";
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV26userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV26userFee, 16, 8))), ".");
            AssignAttri("", false, "AV26userFee", StringUtil.LTrimStr( AV26userFee, 16, 8));
         }
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
         chkavSendallcoins.Caption = "Send total balance";
         bttSendcoins_Visible = 1;
         bttNext_Visible = 1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV17sendAllCoins","fld":"vSENDALLCOINS"},{"av":"AV28wallet","fld":"vWALLET","hsh":true},{"av":"AV31group_sdt","fld":"vGROUP_SDT","hsh":true},{"av":"AV35oneMuSigSignatures","fld":"vONEMUSIGSIGNATURES","hsh":true},{"av":"AV23totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","hsh":true}]}""");
         setEventMetadata("'NEXT'","""{"handler":"E121W2","iparms":[{"av":"AV18sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999"},{"av":"AV23totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","hsh":true},{"av":"AV17sendAllCoins","fld":"vSENDALLCOINS"},{"av":"AV10error","fld":"vERROR"},{"av":"AV19sendTo","fld":"vSENDTO"},{"av":"AV28wallet","fld":"vWALLET","hsh":true},{"av":"AV31group_sdt","fld":"vGROUP_SDT","hsh":true},{"av":"AV33description","fld":"vDESCRIPTION"}]""");
         setEventMetadata("'NEXT'",""","oparms":[{"av":"AV10error","fld":"vERROR"}]}""");
         setEventMetadata("'SEND COINS'","""{"handler":"E131W2","iparms":[{"av":"cmbavUserfee"},{"av":"AV26userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999"},{"av":"AV31group_sdt","fld":"vGROUP_SDT","hsh":true},{"av":"AV17sendAllCoins","fld":"vSENDALLCOINS"},{"av":"AV28wallet","fld":"vWALLET","hsh":true},{"av":"AV25transactionsToSend","fld":"vTRANSACTIONSTOSEND"},{"av":"AV18sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999"},{"av":"AV19sendTo","fld":"vSENDTO"},{"av":"AV7changeTo","fld":"vCHANGETO"},{"av":"AV35oneMuSigSignatures","fld":"vONEMUSIGSIGNATURES","hsh":true},{"av":"AV10error","fld":"vERROR"}]""");
         setEventMetadata("'SEND COINS'",""","oparms":[{"av":"AV10error","fld":"vERROR"},{"av":"AV25transactionsToSend","fld":"vTRANSACTIONSTOSEND"},{"ctrl":"SENDCOINS","prop":"Visible"},{"av":"cmbavUserfee"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E141W2","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E151W2","iparms":[{"av":"AV16PopupName","fld":"vPOPUPNAME"},{"av":"AV35oneMuSigSignatures","fld":"vONEMUSIGSIGNATURES","hsh":true},{"av":"AV33description","fld":"vDESCRIPTION"},{"av":"AV18sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999"},{"av":"AV7changeTo","fld":"vCHANGETO"},{"av":"AV10error","fld":"vERROR"},{"av":"AV31group_sdt","fld":"vGROUP_SDT","hsh":true},{"av":"AV17sendAllCoins","fld":"vSENDALLCOINS"},{"av":"AV28wallet","fld":"vWALLET","hsh":true},{"av":"AV25transactionsToSend","fld":"vTRANSACTIONSTOSEND"},{"av":"AV19sendTo","fld":"vSENDTO"},{"av":"cmbavUserfee"},{"av":"AV26userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"edtavSendcoins_Enabled","ctrl":"vSENDCOINS","prop":"Enabled"},{"av":"edtavSendto_Enabled","ctrl":"vSENDTO","prop":"Enabled"},{"av":"edtavDescription_Enabled","ctrl":"vDESCRIPTION","prop":"Enabled"},{"ctrl":"NEXT","prop":"Visible"},{"ctrl":"SENDCOINS","prop":"Visible"},{"av":"AV25transactionsToSend","fld":"vTRANSACTIONSTOSEND"},{"av":"AV10error","fld":"vERROR"},{"av":"AV7changeTo","fld":"vCHANGETO"},{"av":"cmbavUserfee"},{"av":"AV26userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999"}]}""");
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
         AV28wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV31group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV35oneMuSigSignatures = new GeneXus.Programs.wallet.registered.SdtMuSigSignatures(context);
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV10error = "";
         AV25transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV7changeTo = "";
         AV16PopupName = "";
         GX_FocusControl = "";
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV19sendTo = "";
         AV33description = "";
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
         AV15keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV14historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV30websession = context.GetSession();
         AV13hexTransaction = "";
         AV29TransactionId = "";
         AV32transactionFileName = "";
         AV6ApproveSpendingPopupName = "";
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV5extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo6 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_objcol_SdtSDTAddressHistory3 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_char5 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
         edtavTotalbalance_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short AV22strFound ;
      private short AV8economicalBlocks ;
      private short AV20standarBlocks ;
      private short AV11fastestBlocks ;
      private short nGXWrapped ;
      private int edtavTotalbalance_Enabled ;
      private int edtavSendcoins_Enabled ;
      private int edtavSendto_Enabled ;
      private int edtavDescription_Enabled ;
      private int bttNext_Visible ;
      private int bttSendcoins_Visible ;
      private int idxLst ;
      private long AV27virtualSize ;
      private decimal AV23totalBalance ;
      private decimal AV18sendCoins ;
      private decimal AV26userFee ;
      private decimal GXt_decimal4 ;
      private decimal AV24transactionFee ;
      private decimal AV9economicalFee ;
      private decimal AV21standardFee ;
      private decimal AV12fastestFee ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string edtavTotalbalance_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV10error ;
      private string AV7changeTo ;
      private string AV16PopupName ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string edtavTotalbalance_Jsonclick ;
      private string chkavSendallcoins_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string edtavSendcoins_Internalname ;
      private string edtavSendcoins_Jsonclick ;
      private string edtavSendto_Internalname ;
      private string AV19sendTo ;
      private string edtavDescription_Internalname ;
      private string cmbavUserfee_Internalname ;
      private string cmbavUserfee_Jsonclick ;
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
      private string AV29TransactionId ;
      private string AV32transactionFileName ;
      private string AV6ApproveSpendingPopupName ;
      private string GXt_char5 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool AV17sendAllCoins ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV34verified ;
      private bool GXt_boolean7 ;
      private string AV13hexTransaction ;
      private string AV33description ;
      private IGxSession AV30websession ;
      private GXProperties forbiddenHiddens ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavSendallcoins ;
      private GXCombobox cmbavUserfee ;
      private GeneXus.Programs.wallet.SdtWallet AV28wallet ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV31group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtMuSigSignatures AV35oneMuSigSignatures ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV25transactionsToSend ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV15keyInfo ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV14historyWithBalance ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV5extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo6 ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory3 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
