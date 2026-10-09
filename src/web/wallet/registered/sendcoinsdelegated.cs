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
   public class sendcoinsdelegated : GXDataArea
   {
      public sendcoinsdelegated( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendcoinsdelegated( IGxContext context )
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
         chkavPct10 = new GXCheckbox();
         chkavPct20 = new GXCheckbox();
         chkavPct30 = new GXCheckbox();
         chkavPct40 = new GXCheckbox();
         chkavPct50 = new GXCheckbox();
         chkavPct60 = new GXCheckbox();
         chkavPct70 = new GXCheckbox();
         chkavPct80 = new GXCheckbox();
         chkavPct90 = new GXCheckbox();
         chkavPct100 = new GXCheckbox();
         cmbavFinalpercent = new GXCombobox();
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
         cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA3I2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3I2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.sendcoinsdelegated") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vPENDINGSPENDID", AV36pendingSpendId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV36pendingSpendId, context));
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"), context));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV23networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23networkType, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vAMIGROUPOWNER", AV6amIgroupOwner);
         GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV6amIgroupOwner, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISLASTSIGNER", AV18isLastSigner);
         GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV18isLastSigner, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"SendCoinsDelegated");
         forbiddenHiddens.Add("totalBalance", context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\sendcoinsdelegated:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vPENDINGSPENDID", AV36pendingSpendId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV36pendingSpendId, context));
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV12error));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV23networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23networkType, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vAMIGROUPOWNER", AV6amIgroupOwner);
         GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV6amIgroupOwner, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISLASTSIGNER", AV18isLastSigner);
         GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV18isLastSigner, context));
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV40PopupName));
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
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE3I2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3I2( ) ;
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
         return formatLink("wallet.registered.sendcoinsdelegated")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.SendCoinsDelegated" ;
      }

      public override string GetPgmdesc( )
      {
         return "Send Coins from a Delegated (Taproot) multisignature group" ;
      }

      protected void WB3I0( )
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
            GxWebStd.gx_div_start( context, "", edtavTotalbalance_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTotalbalance_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTotalbalance_Internalname, "Group balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTotalbalance_Internalname, StringUtil.LTrim( StringUtil.NToC( AV48totalBalance, 16, 8, ".", "")), StringUtil.LTrim( ((edtavTotalbalance_Enabled!=0) ? context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999") : context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,8);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotalbalance_Jsonclick, 0, "Attribute", "", "", "", "", edtavTotalbalance_Visible, edtavTotalbalance_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsDelegated.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavSendallcoins.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSendallcoins_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSendallcoins_Internalname, "Send total balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSendallcoins_Internalname, StringUtil.BoolToStr( AV41sendAllCoins), "", "Send total balance", chkavSendallcoins.Visible, chkavSendallcoins.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(12, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,12);\"");
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
            GxWebStd.gx_single_line_edit( context, edtavSendcoins_Internalname, StringUtil.LTrim( StringUtil.NToC( AV42sendCoins, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV42sendCoins, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,17);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSendcoins_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSendcoins_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsDelegated.htm");
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
            GxWebStd.gx_html_textarea( context, edtavSendto_Internalname, StringUtil.RTrim( AV43sendTo), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,22);\"", 0, 1, edtavSendto_Enabled, 1, 80, "chr", 2, "row", 0, StyleString, ClassString, "", "", "250", 1, 0, "", "", -1, true, "NBitcoin\\scriptPubKey_address", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendCoinsDelegated.htm");
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
            GxWebStd.gx_html_textarea( context, edtavDescription_Internalname, AV9description, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,27);\"", 0, 1, edtavDescription_Enabled, 1, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendCoinsDelegated.htm");
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
            GxWebStd.gx_combobox_ctrl1( context, cmbavUserfee, cmbavUserfee_Internalname, StringUtil.Trim( StringUtil.Str( AV49userFee, 16, 8)), 1, cmbavUserfee_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "decimal", "", cmbavUserfee.Visible, cmbavUserfee.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"", "", true, 0, "HLP_Wallet/registered/SendCoinsDelegated.htm");
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV49userFee, 16, 8));
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
            GxWebStd.gx_label_element( context, chkavActivatemanaulfee_Internalname, "Manually select Fee", "col-sm-9 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-3 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavActivatemanaulfee_Internalname, StringUtil.BoolToStr( AV5activateManaulFee), "", "Manually select Fee", chkavActivatemanaulfee.Visible, chkavActivatemanaulfee.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(39, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,39);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavManaulfee_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavManaulfee_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavManaulfee_Internalname, edtavManaulfee_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 43,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavManaulfee_Internalname, StringUtil.LTrim( StringUtil.NToC( AV21manaulFee, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV21manaulFee, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,43);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavManaulfee_Jsonclick, 0, "Attribute", "", "", "", "", edtavManaulfee_Visible, edtavManaulfee_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsDelegated.htm");
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
            /* Div Control */
            GxWebStd.gx_div_start( context, divFeeoptionstable_Internalname, divFeeoptionstable_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbextrafees_Internalname, "Extra fee options for the last signer (optional, at most 3). Each one is a percentage ABOVE the fee selected above. You and the other signers sign every option; the last signer chooses which one to pay.", "", "", lblTbextrafees_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/SendCoinsDelegated.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct10_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct10_Internalname, "+10%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 54,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct10_Internalname, StringUtil.BoolToStr( AV26pct10), "", "+10%", 1, chkavPct10.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(54, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,54);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct20_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct20_Internalname, "+20%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 58,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct20_Internalname, StringUtil.BoolToStr( AV28pct20), "", "+20%", 1, chkavPct20.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(58, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,58);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct30_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct30_Internalname, "+30%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 62,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct30_Internalname, StringUtil.BoolToStr( AV29pct30), "", "+30%", 1, chkavPct30.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(62, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,62);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct40_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct40_Internalname, "+40%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 66,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct40_Internalname, StringUtil.BoolToStr( AV30pct40), "", "+40%", 1, chkavPct40.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(66, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,66);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct50_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct50_Internalname, "+50%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 70,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct50_Internalname, StringUtil.BoolToStr( AV31pct50), "", "+50%", 1, chkavPct50.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(70, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,70);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct60_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct60_Internalname, "+60%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 75,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct60_Internalname, StringUtil.BoolToStr( AV32pct60), "", "+60%", 1, chkavPct60.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(75, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,75);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct70_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct70_Internalname, "+70%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 79,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct70_Internalname, StringUtil.BoolToStr( AV33pct70), "", "+70%", 1, chkavPct70.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(79, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,79);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct80_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct80_Internalname, "+80%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 83,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct80_Internalname, StringUtil.BoolToStr( AV34pct80), "", "+80%", 1, chkavPct80.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(83, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,83);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct90_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct90_Internalname, "+90%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 87,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct90_Internalname, StringUtil.BoolToStr( AV35pct90), "", "+90%", 1, chkavPct90.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(87, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,87);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct100_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct100_Internalname, "+100%", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 91,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct100_Internalname, StringUtil.BoolToStr( AV27pct100), "", "+100%", 1, chkavPct100.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(91, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,91);\"");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbfeeoptions_Internalname, lblTbfeeoptions_Caption, "", "", lblTbfeeoptions_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblTbfeeoptions_Visible, 1, 0, 0, "HLP_Wallet/registered/SendCoinsDelegated.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavFinalpercent.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavFinalpercent_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavFinalpercent_Internalname, "Fee to pay", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 99,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavFinalpercent, cmbavFinalpercent_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV17finalPercent), 4, 0)), 1, cmbavFinalpercent_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", cmbavFinalpercent.Visible, cmbavFinalpercent.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,99);\"", "", true, 0, "HLP_Wallet/registered/SendCoinsDelegated.htm");
            cmbavFinalpercent.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV17finalPercent), 4, 0));
            AssignProp("", false, cmbavFinalpercent_Internalname, "Values", (string)(cmbavFinalpercent.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 102,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttNext_Internalname, "", "Next", bttNext_Jsonclick, 5, "Next", "", StyleString, ClassString, bttNext_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'NEXT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsDelegated.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 105,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSendcoins_Internalname, "", "Send Coins", bttSendcoins_Jsonclick, 5, "Send Coins", "", StyleString, ClassString, bttSendcoins_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'SEND COINS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsDelegated.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 108,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel", bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsDelegated.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START3I2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
            }
         }
         Form.Meta.addItem("description", "Send Coins from a Delegated (Taproot) multisignature group", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3I0( ) ;
      }

      protected void WS3I2( )
      {
         START3I2( ) ;
         EVT3I2( ) ;
      }

      protected void EVT3I2( )
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
                              E113I2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'NEXT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Next' */
                              E123I2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SEND COINS'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Send Coins' */
                              E133I2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel' */
                              E143I2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E153I2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E163I2 ();
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
      }

      protected void WE3I2( )
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

      protected void PA3I2( )
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
         AV41sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV41sendAllCoins));
         AssignAttri("", false, "AV41sendAllCoins", AV41sendAllCoins);
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV49userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV49userFee, 16, 8))), ".");
            AssignAttri("", false, "AV49userFee", StringUtil.LTrimStr( AV49userFee, 16, 8));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV49userFee, 16, 8));
            AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
         }
         AV5activateManaulFee = StringUtil.StrToBool( StringUtil.BoolToStr( AV5activateManaulFee));
         AssignAttri("", false, "AV5activateManaulFee", AV5activateManaulFee);
         AV26pct10 = StringUtil.StrToBool( StringUtil.BoolToStr( AV26pct10));
         AssignAttri("", false, "AV26pct10", AV26pct10);
         AV28pct20 = StringUtil.StrToBool( StringUtil.BoolToStr( AV28pct20));
         AssignAttri("", false, "AV28pct20", AV28pct20);
         AV29pct30 = StringUtil.StrToBool( StringUtil.BoolToStr( AV29pct30));
         AssignAttri("", false, "AV29pct30", AV29pct30);
         AV30pct40 = StringUtil.StrToBool( StringUtil.BoolToStr( AV30pct40));
         AssignAttri("", false, "AV30pct40", AV30pct40);
         AV31pct50 = StringUtil.StrToBool( StringUtil.BoolToStr( AV31pct50));
         AssignAttri("", false, "AV31pct50", AV31pct50);
         AV32pct60 = StringUtil.StrToBool( StringUtil.BoolToStr( AV32pct60));
         AssignAttri("", false, "AV32pct60", AV32pct60);
         AV33pct70 = StringUtil.StrToBool( StringUtil.BoolToStr( AV33pct70));
         AssignAttri("", false, "AV33pct70", AV33pct70);
         AV34pct80 = StringUtil.StrToBool( StringUtil.BoolToStr( AV34pct80));
         AssignAttri("", false, "AV34pct80", AV34pct80);
         AV35pct90 = StringUtil.StrToBool( StringUtil.BoolToStr( AV35pct90));
         AssignAttri("", false, "AV35pct90", AV35pct90);
         AV27pct100 = StringUtil.StrToBool( StringUtil.BoolToStr( AV27pct100));
         AssignAttri("", false, "AV27pct100", AV27pct100);
         if ( cmbavFinalpercent.ItemCount > 0 )
         {
            AV17finalPercent = (short)(Math.Round(NumberUtil.Val( cmbavFinalpercent.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV17finalPercent), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV17finalPercent", StringUtil.LTrimStr( (decimal)(AV17finalPercent), 4, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavFinalpercent.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV17finalPercent), 4, 0));
            AssignProp("", false, cmbavFinalpercent_Internalname, "Values", cmbavFinalpercent.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3I2( ) ;
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

      protected void RF3I2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E163I2 ();
            WB3I0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3I2( )
      {
         GxWebStd.gx_hidden_field( context, "vPENDINGSPENDID", AV36pendingSpendId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV36pendingSpendId, context));
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"), context));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV23networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23networkType, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vAMIGROUPOWNER", AV6amIgroupOwner);
         GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV6amIgroupOwner, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISLASTSIGNER", AV18isLastSigner);
         GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV18isLastSigner, context));
      }

      protected void before_start_formulas( )
      {
         edtavTotalbalance_Enabled = 0;
         AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3I0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E113I2 ();
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
               AV48totalBalance = 0;
               AssignAttri("", false, "AV48totalBalance", StringUtil.LTrimStr( AV48totalBalance, 16, 8));
               GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"), context));
            }
            else
            {
               AV48totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
               AssignAttri("", false, "AV48totalBalance", StringUtil.LTrimStr( AV48totalBalance, 16, 8));
               GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"), context));
            }
            AV41sendAllCoins = StringUtil.StrToBool( cgiGet( chkavSendallcoins_Internalname));
            AssignAttri("", false, "AV41sendAllCoins", AV41sendAllCoins);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vSENDCOINS");
               GX_FocusControl = edtavSendcoins_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV42sendCoins = 0;
               AssignAttri("", false, "AV42sendCoins", StringUtil.LTrimStr( AV42sendCoins, 16, 8));
            }
            else
            {
               AV42sendCoins = context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",");
               AssignAttri("", false, "AV42sendCoins", StringUtil.LTrimStr( AV42sendCoins, 16, 8));
            }
            AV43sendTo = cgiGet( edtavSendto_Internalname);
            AssignAttri("", false, "AV43sendTo", AV43sendTo);
            AV9description = cgiGet( edtavDescription_Internalname);
            AssignAttri("", false, "AV9description", AV9description);
            cmbavUserfee.CurrentValue = cgiGet( cmbavUserfee_Internalname);
            AV49userFee = NumberUtil.Val( cgiGet( cmbavUserfee_Internalname), ".");
            AssignAttri("", false, "AV49userFee", StringUtil.LTrimStr( AV49userFee, 16, 8));
            AV5activateManaulFee = StringUtil.StrToBool( cgiGet( chkavActivatemanaulfee_Internalname));
            AssignAttri("", false, "AV5activateManaulFee", AV5activateManaulFee);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vMANAULFEE");
               GX_FocusControl = edtavManaulfee_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV21manaulFee = 0;
               AssignAttri("", false, "AV21manaulFee", StringUtil.LTrimStr( AV21manaulFee, 16, 8));
            }
            else
            {
               AV21manaulFee = context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",");
               AssignAttri("", false, "AV21manaulFee", StringUtil.LTrimStr( AV21manaulFee, 16, 8));
            }
            AV26pct10 = StringUtil.StrToBool( cgiGet( chkavPct10_Internalname));
            AssignAttri("", false, "AV26pct10", AV26pct10);
            AV28pct20 = StringUtil.StrToBool( cgiGet( chkavPct20_Internalname));
            AssignAttri("", false, "AV28pct20", AV28pct20);
            AV29pct30 = StringUtil.StrToBool( cgiGet( chkavPct30_Internalname));
            AssignAttri("", false, "AV29pct30", AV29pct30);
            AV30pct40 = StringUtil.StrToBool( cgiGet( chkavPct40_Internalname));
            AssignAttri("", false, "AV30pct40", AV30pct40);
            AV31pct50 = StringUtil.StrToBool( cgiGet( chkavPct50_Internalname));
            AssignAttri("", false, "AV31pct50", AV31pct50);
            AV32pct60 = StringUtil.StrToBool( cgiGet( chkavPct60_Internalname));
            AssignAttri("", false, "AV32pct60", AV32pct60);
            AV33pct70 = StringUtil.StrToBool( cgiGet( chkavPct70_Internalname));
            AssignAttri("", false, "AV33pct70", AV33pct70);
            AV34pct80 = StringUtil.StrToBool( cgiGet( chkavPct80_Internalname));
            AssignAttri("", false, "AV34pct80", AV34pct80);
            AV35pct90 = StringUtil.StrToBool( cgiGet( chkavPct90_Internalname));
            AssignAttri("", false, "AV35pct90", AV35pct90);
            AV27pct100 = StringUtil.StrToBool( cgiGet( chkavPct100_Internalname));
            AssignAttri("", false, "AV27pct100", AV27pct100);
            cmbavFinalpercent.CurrentValue = cgiGet( cmbavFinalpercent_Internalname);
            AV17finalPercent = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavFinalpercent_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV17finalPercent", StringUtil.LTrimStr( (decimal)(AV17finalPercent), 4, 0));
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"SendCoinsDelegated");
            AV48totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
            AssignAttri("", false, "AV48totalBalance", StringUtil.LTrimStr( AV48totalBalance, 16, 8));
            GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"), context));
            forbiddenHiddens.Add("totalBalance", context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("wallet\\registered\\sendcoinsdelegated:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
         E113I2 ();
         if (returnInSub) return;
      }

      protected void E113I2( )
      {
         /* Start Routine */
         returnInSub = false;
         bttSendcoins_Visible = 0;
         AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
         cmbavUserfee.Visible = 0;
         AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
         chkavActivatemanaulfee.Visible = 0;
         AssignProp("", false, chkavActivatemanaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavActivatemanaulfee.Visible), 5, 0), true);
         edtavManaulfee_Visible = 0;
         AssignProp("", false, edtavManaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Visible), 5, 0), true);
         divFeeoptionstable_Visible = 0;
         AssignProp("", false, divFeeoptionstable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divFeeoptionstable_Visible), 5, 0), true);
         lblTbfeeoptions_Visible = 0;
         AssignProp("", false, lblTbfeeoptions_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbfeeoptions_Visible), 5, 0), true);
         cmbavFinalpercent.Visible = 0;
         AssignProp("", false, cmbavFinalpercent_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavFinalpercent.Visible), 5, 0), true);
         GXt_decimal1 = AV48totalBalance;
         new GeneXus.Programs.wallet.getbalancefromhistorywithbalance(context ).execute( out  GXt_decimal1) ;
         AV48totalBalance = GXt_decimal1;
         AssignAttri("", false, "AV48totalBalance", StringUtil.LTrimStr( AV48totalBalance, 16, 8));
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV48totalBalance, "ZZZZZZ9.99999999"), context));
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         new GeneXus.Programs.wallet.registered.getdelegatedspendcontext(context ).execute( out  AV23networkType, out  AV6amIgroupOwner, out  AV22minSig, out  AV24numMembers, out  AV36pendingSpendId, out  AV18isLastSigner, out  AV41sendAllCoins, out  AV42sendCoins, out  AV43sendTo, out  AV9description, out  AV21manaulFee, out  AV44spendInfo, out  AV12error) ;
         AssignAttri("", false, "AV23networkType", AV23networkType);
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23networkType, "")), context));
         AssignAttri("", false, "AV6amIgroupOwner", AV6amIgroupOwner);
         GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV6amIgroupOwner, context));
         AssignAttri("", false, "AV36pendingSpendId", AV36pendingSpendId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV36pendingSpendId, context));
         AssignAttri("", false, "AV18isLastSigner", AV18isLastSigner);
         GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV18isLastSigner, context));
         AssignAttri("", false, "AV41sendAllCoins", AV41sendAllCoins);
         AssignAttri("", false, "AV42sendCoins", StringUtil.LTrimStr( AV42sendCoins, 16, 8));
         AssignAttri("", false, "AV43sendTo", AV43sendTo);
         AssignAttri("", false, "AV9description", AV9description);
         AssignAttri("", false, "AV21manaulFee", StringUtil.LTrimStr( AV21manaulFee, 16, 8));
         AssignAttri("", false, "AV12error", AV12error);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"This payment can not be signed",(string)AV12error}, true);
            bttNext_Visible = 0;
            AssignProp("", false, bttNext_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttNext_Visible), 5, 0), true);
         }
         if ( ! (Guid.Empty==AV36pendingSpendId) )
         {
            edtavTotalbalance_Visible = 0;
            AssignProp("", false, edtavTotalbalance_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Visible), 5, 0), true);
            chkavSendallcoins.Visible = 0;
            AssignProp("", false, chkavSendallcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Visible), 5, 0), true);
            chkavSendallcoins.Enabled = 0;
            AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
            edtavSendcoins_Enabled = 0;
            AssignProp("", false, edtavSendcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendcoins_Enabled), 5, 0), true);
            edtavSendto_Enabled = 0;
            AssignProp("", false, edtavSendto_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendto_Enabled), 5, 0), true);
            edtavDescription_Enabled = 0;
            AssignProp("", false, edtavDescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDescription_Enabled), 5, 0), true);
            edtavManaulfee_Enabled = 0;
            AssignProp("", false, edtavManaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Enabled), 5, 0), true);
            edtavManaulfee_Caption = "Calculated fee (BTC):";
            AssignProp("", false, edtavManaulfee_Internalname, "Caption", edtavManaulfee_Caption, true);
            edtavManaulfee_Visible = 1;
            AssignProp("", false, edtavManaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Visible), 5, 0), true);
            AV16feeOptionsText = "";
            AV52GXV1 = 1;
            while ( AV52GXV1 <= AV44spendInfo.gxTpr_Levels.Count )
            {
               AV25oneLevel = ((GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem)AV44spendInfo.gxTpr_Levels.Item(AV52GXV1));
               if ( AV25oneLevel.gxTpr_Percent == 0 )
               {
                  AV20levelCaption = "Calculated fee: " + StringUtil.Trim( StringUtil.Str( AV25oneLevel.gxTpr_Feebtc, 16, 8)) + " BTC";
               }
               else
               {
                  AV20levelCaption = "+" + StringUtil.Trim( StringUtil.Str( (decimal)(AV25oneLevel.gxTpr_Percent), 4, 0)) + " %: " + StringUtil.Trim( StringUtil.Str( AV25oneLevel.gxTpr_Feebtc, 16, 8)) + " BTC";
               }
               cmbavFinalpercent.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(AV25oneLevel.gxTpr_Percent), 4, 0)), AV20levelCaption, 0);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV16feeOptionsText)) )
               {
                  AV16feeOptionsText += " | ";
               }
               AV16feeOptionsText += AV20levelCaption;
               AV52GXV1 = (int)(AV52GXV1+1);
            }
            if ( AV18isLastSigner )
            {
               AV17finalPercent = 0;
               AssignAttri("", false, "AV17finalPercent", StringUtil.LTrimStr( (decimal)(AV17finalPercent), 4, 0));
               cmbavFinalpercent.Visible = 1;
               AssignProp("", false, cmbavFinalpercent_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavFinalpercent.Visible), 5, 0), true);
               lblTbfeeoptions_Caption = "You are the last signer: choose the fee to pay. You sign only that transaction and it is sent to the network.";
               AssignProp("", false, lblTbfeeoptions_Internalname, "Caption", lblTbfeeoptions_Caption, true);
            }
            else
            {
               lblTbfeeoptions_Caption = "Fee options set by the first signer (the last signer chooses one): "+AV16feeOptionsText;
               AssignProp("", false, lblTbfeeoptions_Internalname, "Caption", lblTbfeeoptions_Caption, true);
            }
            lblTbfeeoptions_Visible = 1;
            AssignProp("", false, lblTbfeeoptions_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbfeeoptions_Visible), 5, 0), true);
         }
      }

      protected void E123I2( )
      {
         /* 'Next' Routine */
         returnInSub = false;
         if ( ! (Guid.Empty==AV36pendingSpendId) )
         {
            chkavSendallcoins.Enabled = 0;
            AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
            context.PopUp(formatLink("wallet.approvespending") , new Object[] {});
         }
         else
         {
            if ( ( AV42sendCoins >= AV48totalBalance ) && ! AV41sendAllCoins )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You don't have enough balance",(string)AV12error}, true);
            }
            else
            {
               if ( (Convert.ToDecimal(0)==AV42sendCoins) && ! AV41sendAllCoins )
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You have to select an amount to send",(string)AV12error}, true);
               }
               else
               {
                  GXt_char2 = AV12error;
                  new GeneXus.Programs.nbitcoin.isaddressvalid(context ).execute(  AV43sendTo,  AV23networkType, out  GXt_char2) ;
                  AV12error = GXt_char2;
                  AssignAttri("", false, "AV12error", AV12error);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Please check the Send to address: ",(string)AV12error}, true);
                  }
                  else
                  {
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9description)) )
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"You have to enter a description",(string)"The description is mandatory on multisignature transactions"}, true);
                     }
                     else
                     {
                        chkavSendallcoins.Enabled = 0;
                        AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
                        context.PopUp(formatLink("wallet.approvespending") , new Object[] {});
                     }
                  }
               }
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E133I2( )
      {
         /* 'Send Coins' Routine */
         returnInSub = false;
         if ( (Convert.ToDecimal(0)==AV21manaulFee) || ( AV21manaulFee <= Convert.ToDecimal( 0 )) )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Please select an Estimated Transaction Fee to pay",(string)AV12error}, true);
         }
         else
         {
            AV37percentages = "";
            AV38percentCount = 0;
            if ( (Guid.Empty==AV36pendingSpendId) && ! AV6amIgroupOwner && ! AV18isLastSigner )
            {
               if ( AV26pct10 )
               {
                  AV37percentages += "10,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV28pct20 )
               {
                  AV37percentages += "20,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV29pct30 )
               {
                  AV37percentages += "30,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV30pct40 )
               {
                  AV37percentages += "40,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV31pct50 )
               {
                  AV37percentages += "50,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV32pct60 )
               {
                  AV37percentages += "60,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV33pct70 )
               {
                  AV37percentages += "70,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV34pct80 )
               {
                  AV37percentages += "80,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV35pct90 )
               {
                  AV37percentages += "90,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
               if ( AV27pct100 )
               {
                  AV37percentages += "100,";
                  AV38percentCount = (short)(AV38percentCount+1);
               }
            }
            if ( AV38percentCount > 3 )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Too many extra fee options",(string)"Choose at most 3 extra fee options"}, true);
            }
            else
            {
               if ( AV18isLastSigner )
               {
                  AV39percentToSend = AV17finalPercent;
               }
               else
               {
                  AV39percentToSend = -1;
               }
               GXt_char2 = AV12error;
               new GeneXus.Programs.wallet.registered.senddelegatedspend(context ).execute(  AV41sendAllCoins,  AV42sendCoins,  AV43sendTo,  AV9description,  AV21manaulFee,  AV37percentages,  AV39percentToSend, out  AV8broadcast, out  AV13errorTitle, out  GXt_char2) ;
               AV12error = GXt_char2;
               AssignAttri("", false, "AV12error", AV12error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) && String.IsNullOrEmpty(StringUtil.RTrim( AV13errorTitle)) )
               {
                  if ( AV8broadcast )
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Transaction broadcast",(string)"The transaction was signed and submitted to the network"}, true);
                  }
                  else
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Signature added",(string)"Your signature was added and forwarded to the other signers"}, true);
                  }
                  AV51websession.Set("MuSign_ONE", "");
                  context.setWebReturnParms(new Object[] {});
                  context.setWebReturnParmsMetadata(new Object[] {});
                  context.wjLocDisableFrm = 1;
                  context.nUserReturn = 1;
                  returnInSub = true;
                  if (true) return;
               }
               else
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)AV13errorTitle,(string)AV12error}, true);
               }
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E143I2( )
      {
         /* 'Cancel' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         AV51websession.Set("MuSign_ONE", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void E153I2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV7ApproveSpendingPopupName = "Wallet.ApproveSpending";
         AV47strFound = (short)(StringUtil.StringSearch( AV40PopupName, StringUtil.Lower( AV7ApproveSpendingPopupName), 1));
         if ( AV47strFound > 0 )
         {
            GXt_boolean3 = AV19keyAvailable;
            new GeneXus.Programs.wallet.registered.hassigningkeytaproot(context ).execute( out  GXt_boolean3) ;
            AV19keyAvailable = GXt_boolean3;
            if ( ! AV19keyAvailable )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Signing key not available",(string)"We couldn't unlock the signing key with that password"}, true);
            }
            else
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
               edtavManaulfee_Visible = 1;
               AssignProp("", false, edtavManaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Visible), 5, 0), true);
               if ( (Guid.Empty==AV36pendingSpendId) )
               {
                  GXt_char2 = AV12error;
                  new GeneXus.Programs.wallet.registered.estimatedelegatedvsize(context ).execute(  AV41sendAllCoins,  AV42sendCoins,  AV43sendTo,  AV9description, out  AV50virtualSize, out  GXt_char2) ;
                  AV12error = GXt_char2;
                  AssignAttri("", false, "AV12error", AV12error);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem estimating the size of the transaction: ",(string)AV12error}, true);
                     new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                  }
                  else
                  {
                     GXt_char2 = AV12error;
                     GXt_int4 = (short)(AV10economicalBlocks);
                     new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV50virtualSize,  60,  "economical", out  AV11economicalFee, out  GXt_int4, out  GXt_char2) ;
                     AV10economicalBlocks = GXt_int4;
                     AV12error = GXt_char2;
                     AssignAttri("", false, "AV12error", AV12error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                     {
                        GXt_char2 = AV12error;
                        GXt_int4 = (short)(AV45standarBlocks);
                        new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV50virtualSize,  6,  "conservative", out  AV46standardFee, out  GXt_int4, out  GXt_char2) ;
                        AV45standarBlocks = GXt_int4;
                        AV12error = GXt_char2;
                        AssignAttri("", false, "AV12error", AV12error);
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                        {
                           GXt_char2 = AV12error;
                           GXt_int4 = (short)(AV14fastestBlocks);
                           new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV50virtualSize,  1,  "conservative", out  AV15fastestFee, out  GXt_int4, out  GXt_char2) ;
                           AV14fastestBlocks = GXt_int4;
                           AV12error = GXt_char2;
                           AssignAttri("", false, "AV12error", AV12error);
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                           {
                              cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(0), 16, 8)), "Select Estimated Transaction Fee", 0);
                              cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV11economicalFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV11economicalFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV10economicalBlocks), 10, 0)+" Blocks", 0);
                              cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV46standardFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV46standardFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV45standarBlocks), 10, 0)+" Blocks", 0);
                              cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV15fastestFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV15fastestFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV14fastestBlocks), 10, 0)+" Blocks", 0);
                              cmbavUserfee.Visible = 1;
                              AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
                              edtavManaulfee_Enabled = 0;
                              AssignProp("", false, edtavManaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Enabled), 5, 0), true);
                              chkavActivatemanaulfee.Visible = 1;
                              AssignProp("", false, chkavActivatemanaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavActivatemanaulfee.Visible), 5, 0), true);
                              if ( ! AV6amIgroupOwner && ! AV18isLastSigner )
                              {
                                 divFeeoptionstable_Visible = 1;
                                 AssignProp("", false, divFeeoptionstable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divFeeoptionstable_Visible), 5, 0), true);
                              }
                           }
                           else
                           {
                              this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculating the fastest fee: ",(string)AV12error}, true);
                              new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                           }
                        }
                        else
                        {
                           this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculating the standard fee: ",(string)AV12error}, true);
                           new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                        }
                     }
                     else
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculating the economical fee: ",(string)AV12error}, true);
                        new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                     }
                  }
               }
               else
               {
                  edtavManaulfee_Enabled = 0;
                  AssignProp("", false, edtavManaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Enabled), 5, 0), true);
               }
            }
         }
         /*  Sending Event outputs  */
         cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV49userFee, 16, 8));
         AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
      }

      protected void nextLoad( )
      {
      }

      protected void E163I2( )
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
         PA3I2( ) ;
         WS3I2( ) ;
         WE3I2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20261082244479", true, true, false);
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
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         context.AddJavascriptSource("gxdec.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("wallet/registered/sendcoinsdelegated.js", "?20261082244479", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         chkavSendallcoins.Name = "vSENDALLCOINS";
         chkavSendallcoins.WebTags = "";
         chkavSendallcoins.Caption = "Send total balance";
         AssignProp("", false, chkavSendallcoins_Internalname, "TitleCaption", chkavSendallcoins.Caption, true);
         chkavSendallcoins.CheckedValue = "false";
         AV41sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV41sendAllCoins));
         AssignAttri("", false, "AV41sendAllCoins", AV41sendAllCoins);
         cmbavUserfee.Name = "vUSERFEE";
         cmbavUserfee.WebTags = "";
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV49userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV49userFee, 16, 8))), ".");
            AssignAttri("", false, "AV49userFee", StringUtil.LTrimStr( AV49userFee, 16, 8));
         }
         chkavActivatemanaulfee.Name = "vACTIVATEMANAULFEE";
         chkavActivatemanaulfee.WebTags = "";
         chkavActivatemanaulfee.Caption = "Manually select Fee";
         AssignProp("", false, chkavActivatemanaulfee_Internalname, "TitleCaption", chkavActivatemanaulfee.Caption, true);
         chkavActivatemanaulfee.CheckedValue = "false";
         AV5activateManaulFee = StringUtil.StrToBool( StringUtil.BoolToStr( AV5activateManaulFee));
         AssignAttri("", false, "AV5activateManaulFee", AV5activateManaulFee);
         chkavPct10.Name = "vPCT10";
         chkavPct10.WebTags = "";
         chkavPct10.Caption = "+10%";
         AssignProp("", false, chkavPct10_Internalname, "TitleCaption", chkavPct10.Caption, true);
         chkavPct10.CheckedValue = "false";
         AV26pct10 = StringUtil.StrToBool( StringUtil.BoolToStr( AV26pct10));
         AssignAttri("", false, "AV26pct10", AV26pct10);
         chkavPct20.Name = "vPCT20";
         chkavPct20.WebTags = "";
         chkavPct20.Caption = "+20%";
         AssignProp("", false, chkavPct20_Internalname, "TitleCaption", chkavPct20.Caption, true);
         chkavPct20.CheckedValue = "false";
         AV28pct20 = StringUtil.StrToBool( StringUtil.BoolToStr( AV28pct20));
         AssignAttri("", false, "AV28pct20", AV28pct20);
         chkavPct30.Name = "vPCT30";
         chkavPct30.WebTags = "";
         chkavPct30.Caption = "+30%";
         AssignProp("", false, chkavPct30_Internalname, "TitleCaption", chkavPct30.Caption, true);
         chkavPct30.CheckedValue = "false";
         AV29pct30 = StringUtil.StrToBool( StringUtil.BoolToStr( AV29pct30));
         AssignAttri("", false, "AV29pct30", AV29pct30);
         chkavPct40.Name = "vPCT40";
         chkavPct40.WebTags = "";
         chkavPct40.Caption = "+40%";
         AssignProp("", false, chkavPct40_Internalname, "TitleCaption", chkavPct40.Caption, true);
         chkavPct40.CheckedValue = "false";
         AV30pct40 = StringUtil.StrToBool( StringUtil.BoolToStr( AV30pct40));
         AssignAttri("", false, "AV30pct40", AV30pct40);
         chkavPct50.Name = "vPCT50";
         chkavPct50.WebTags = "";
         chkavPct50.Caption = "+50%";
         AssignProp("", false, chkavPct50_Internalname, "TitleCaption", chkavPct50.Caption, true);
         chkavPct50.CheckedValue = "false";
         AV31pct50 = StringUtil.StrToBool( StringUtil.BoolToStr( AV31pct50));
         AssignAttri("", false, "AV31pct50", AV31pct50);
         chkavPct60.Name = "vPCT60";
         chkavPct60.WebTags = "";
         chkavPct60.Caption = "+60%";
         AssignProp("", false, chkavPct60_Internalname, "TitleCaption", chkavPct60.Caption, true);
         chkavPct60.CheckedValue = "false";
         AV32pct60 = StringUtil.StrToBool( StringUtil.BoolToStr( AV32pct60));
         AssignAttri("", false, "AV32pct60", AV32pct60);
         chkavPct70.Name = "vPCT70";
         chkavPct70.WebTags = "";
         chkavPct70.Caption = "+70%";
         AssignProp("", false, chkavPct70_Internalname, "TitleCaption", chkavPct70.Caption, true);
         chkavPct70.CheckedValue = "false";
         AV33pct70 = StringUtil.StrToBool( StringUtil.BoolToStr( AV33pct70));
         AssignAttri("", false, "AV33pct70", AV33pct70);
         chkavPct80.Name = "vPCT80";
         chkavPct80.WebTags = "";
         chkavPct80.Caption = "+80%";
         AssignProp("", false, chkavPct80_Internalname, "TitleCaption", chkavPct80.Caption, true);
         chkavPct80.CheckedValue = "false";
         AV34pct80 = StringUtil.StrToBool( StringUtil.BoolToStr( AV34pct80));
         AssignAttri("", false, "AV34pct80", AV34pct80);
         chkavPct90.Name = "vPCT90";
         chkavPct90.WebTags = "";
         chkavPct90.Caption = "+90%";
         AssignProp("", false, chkavPct90_Internalname, "TitleCaption", chkavPct90.Caption, true);
         chkavPct90.CheckedValue = "false";
         AV35pct90 = StringUtil.StrToBool( StringUtil.BoolToStr( AV35pct90));
         AssignAttri("", false, "AV35pct90", AV35pct90);
         chkavPct100.Name = "vPCT100";
         chkavPct100.WebTags = "";
         chkavPct100.Caption = "+100%";
         AssignProp("", false, chkavPct100_Internalname, "TitleCaption", chkavPct100.Caption, true);
         chkavPct100.CheckedValue = "false";
         AV27pct100 = StringUtil.StrToBool( StringUtil.BoolToStr( AV27pct100));
         AssignAttri("", false, "AV27pct100", AV27pct100);
         cmbavFinalpercent.Name = "vFINALPERCENT";
         cmbavFinalpercent.WebTags = "";
         if ( cmbavFinalpercent.ItemCount > 0 )
         {
            AV17finalPercent = (short)(Math.Round(NumberUtil.Val( cmbavFinalpercent.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV17finalPercent), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV17finalPercent", StringUtil.LTrimStr( (decimal)(AV17finalPercent), 4, 0));
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
         chkavActivatemanaulfee_Internalname = "vACTIVATEMANAULFEE";
         edtavManaulfee_Internalname = "vMANAULFEE";
         divTable1_Internalname = "TABLE1";
         lblTbextrafees_Internalname = "TBEXTRAFEES";
         chkavPct10_Internalname = "vPCT10";
         chkavPct20_Internalname = "vPCT20";
         chkavPct30_Internalname = "vPCT30";
         chkavPct40_Internalname = "vPCT40";
         chkavPct50_Internalname = "vPCT50";
         chkavPct60_Internalname = "vPCT60";
         chkavPct70_Internalname = "vPCT70";
         chkavPct80_Internalname = "vPCT80";
         chkavPct90_Internalname = "vPCT90";
         chkavPct100_Internalname = "vPCT100";
         divFeeoptionstable_Internalname = "FEEOPTIONSTABLE";
         lblTbfeeoptions_Internalname = "TBFEEOPTIONS";
         cmbavFinalpercent_Internalname = "vFINALPERCENT";
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
         chkavPct100.Caption = "+100%";
         chkavPct90.Caption = "+90%";
         chkavPct80.Caption = "+80%";
         chkavPct70.Caption = "+70%";
         chkavPct60.Caption = "+60%";
         chkavPct50.Caption = "+50%";
         chkavPct40.Caption = "+40%";
         chkavPct30.Caption = "+30%";
         chkavPct20.Caption = "+20%";
         chkavPct10.Caption = "+10%";
         chkavActivatemanaulfee.Caption = "Manually select Fee";
         chkavSendallcoins.Caption = "Send total balance";
         bttSendcoins_Visible = 1;
         bttNext_Visible = 1;
         cmbavFinalpercent_Jsonclick = "";
         cmbavFinalpercent.Enabled = 1;
         cmbavFinalpercent.Visible = 1;
         lblTbfeeoptions_Caption = "Fee options";
         lblTbfeeoptions_Visible = 1;
         chkavPct100.Enabled = 1;
         chkavPct90.Enabled = 1;
         chkavPct80.Enabled = 1;
         chkavPct70.Enabled = 1;
         chkavPct60.Enabled = 1;
         chkavPct50.Enabled = 1;
         chkavPct40.Enabled = 1;
         chkavPct30.Enabled = 1;
         chkavPct20.Enabled = 1;
         chkavPct10.Enabled = 1;
         divFeeoptionstable_Visible = 1;
         edtavManaulfee_Jsonclick = "";
         edtavManaulfee_Enabled = 1;
         edtavManaulfee_Caption = "";
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
         chkavSendallcoins.Visible = 1;
         edtavTotalbalance_Jsonclick = "";
         edtavTotalbalance_Enabled = 1;
         edtavTotalbalance_Visible = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Send Coins from a Delegated (Taproot) multisignature group";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV41sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV5activateManaulFee","fld":"vACTIVATEMANAULFEE","type":"boolean"},{"av":"AV26pct10","fld":"vPCT10","type":"boolean"},{"av":"AV28pct20","fld":"vPCT20","type":"boolean"},{"av":"AV29pct30","fld":"vPCT30","type":"boolean"},{"av":"AV30pct40","fld":"vPCT40","type":"boolean"},{"av":"AV31pct50","fld":"vPCT50","type":"boolean"},{"av":"AV32pct60","fld":"vPCT60","type":"boolean"},{"av":"AV33pct70","fld":"vPCT70","type":"boolean"},{"av":"AV34pct80","fld":"vPCT80","type":"boolean"},{"av":"AV35pct90","fld":"vPCT90","type":"boolean"},{"av":"AV27pct100","fld":"vPCT100","type":"boolean"},{"av":"AV36pendingSpendId","fld":"vPENDINGSPENDID","hsh":true,"type":"guid"},{"av":"AV23networkType","fld":"vNETWORKTYPE","hsh":true,"type":"char"},{"av":"AV6amIgroupOwner","fld":"vAMIGROUPOWNER","hsh":true,"type":"boolean"},{"av":"AV18isLastSigner","fld":"vISLASTSIGNER","hsh":true,"type":"boolean"},{"av":"AV48totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","hsh":true,"type":"decimal"}]}""");
         setEventMetadata("'NEXT'","""{"handler":"E123I2","iparms":[{"av":"AV36pendingSpendId","fld":"vPENDINGSPENDID","hsh":true,"type":"guid"},{"av":"AV42sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV48totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","hsh":true,"type":"decimal"},{"av":"AV41sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"AV43sendTo","fld":"vSENDTO","type":"char"},{"av":"AV23networkType","fld":"vNETWORKTYPE","hsh":true,"type":"char"},{"av":"AV9description","fld":"vDESCRIPTION","type":"svchar"}]""");
         setEventMetadata("'NEXT'",""","oparms":[{"av":"chkavSendallcoins.Enabled","ctrl":"vSENDALLCOINS","prop":"Enabled"},{"av":"AV12error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'SEND COINS'","""{"handler":"E133I2","iparms":[{"av":"AV21manaulFee","fld":"vMANAULFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"AV36pendingSpendId","fld":"vPENDINGSPENDID","hsh":true,"type":"guid"},{"av":"AV6amIgroupOwner","fld":"vAMIGROUPOWNER","hsh":true,"type":"boolean"},{"av":"AV18isLastSigner","fld":"vISLASTSIGNER","hsh":true,"type":"boolean"},{"av":"AV26pct10","fld":"vPCT10","type":"boolean"},{"av":"AV28pct20","fld":"vPCT20","type":"boolean"},{"av":"AV29pct30","fld":"vPCT30","type":"boolean"},{"av":"AV30pct40","fld":"vPCT40","type":"boolean"},{"av":"AV31pct50","fld":"vPCT50","type":"boolean"},{"av":"AV32pct60","fld":"vPCT60","type":"boolean"},{"av":"AV33pct70","fld":"vPCT70","type":"boolean"},{"av":"AV34pct80","fld":"vPCT80","type":"boolean"},{"av":"AV35pct90","fld":"vPCT90","type":"boolean"},{"av":"AV27pct100","fld":"vPCT100","type":"boolean"},{"av":"cmbavFinalpercent"},{"av":"AV17finalPercent","fld":"vFINALPERCENT","pic":"ZZZ9","type":"int"},{"av":"AV41sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV42sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV43sendTo","fld":"vSENDTO","type":"char"},{"av":"AV9description","fld":"vDESCRIPTION","type":"svchar"}]""");
         setEventMetadata("'SEND COINS'",""","oparms":[{"av":"AV12error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E143I2","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E153I2","iparms":[{"av":"AV40PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV36pendingSpendId","fld":"vPENDINGSPENDID","hsh":true,"type":"guid"},{"av":"AV41sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV42sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV43sendTo","fld":"vSENDTO","type":"char"},{"av":"AV9description","fld":"vDESCRIPTION","type":"svchar"},{"av":"cmbavUserfee"},{"av":"AV49userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV6amIgroupOwner","fld":"vAMIGROUPOWNER","hsh":true,"type":"boolean"},{"av":"AV18isLastSigner","fld":"vISLASTSIGNER","hsh":true,"type":"boolean"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"edtavSendcoins_Enabled","ctrl":"vSENDCOINS","prop":"Enabled"},{"av":"edtavSendto_Enabled","ctrl":"vSENDTO","prop":"Enabled"},{"av":"edtavDescription_Enabled","ctrl":"vDESCRIPTION","prop":"Enabled"},{"ctrl":"NEXT","prop":"Visible"},{"ctrl":"SENDCOINS","prop":"Visible"},{"av":"edtavManaulfee_Visible","ctrl":"vMANAULFEE","prop":"Visible"},{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"cmbavUserfee"},{"av":"AV49userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"edtavManaulfee_Enabled","ctrl":"vMANAULFEE","prop":"Enabled"},{"av":"chkavActivatemanaulfee.Visible","ctrl":"vACTIVATEMANAULFEE","prop":"Visible"},{"av":"divFeeoptionstable_Visible","ctrl":"FEEOPTIONSTABLE","prop":"Visible"}]}""");
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
         AV36pendingSpendId = Guid.Empty;
         AV23networkType = "";
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV12error = "";
         AV40PopupName = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV43sendTo = "";
         AV9description = "";
         lblTbextrafees_Jsonclick = "";
         lblTbfeeoptions_Jsonclick = "";
         bttNext_Jsonclick = "";
         bttSendcoins_Jsonclick = "";
         bttCancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         hsh = "";
         AV44spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context);
         AV16feeOptionsText = "";
         AV25oneLevel = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem(context);
         AV20levelCaption = "";
         AV37percentages = "";
         AV13errorTitle = "";
         AV51websession = context.GetSession();
         AV7ApproveSpendingPopupName = "";
         GXt_char2 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
         edtavTotalbalance_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV17finalPercent ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short AV22minSig ;
      private short AV24numMembers ;
      private short AV38percentCount ;
      private short AV39percentToSend ;
      private short AV47strFound ;
      private short GXt_int4 ;
      private short nGXWrapped ;
      private int edtavTotalbalance_Visible ;
      private int edtavTotalbalance_Enabled ;
      private int edtavSendcoins_Enabled ;
      private int edtavSendto_Enabled ;
      private int edtavDescription_Enabled ;
      private int edtavManaulfee_Visible ;
      private int edtavManaulfee_Enabled ;
      private int divFeeoptionstable_Visible ;
      private int lblTbfeeoptions_Visible ;
      private int bttNext_Visible ;
      private int bttSendcoins_Visible ;
      private int AV52GXV1 ;
      private int idxLst ;
      private long AV50virtualSize ;
      private long AV10economicalBlocks ;
      private long AV45standarBlocks ;
      private long AV14fastestBlocks ;
      private decimal AV48totalBalance ;
      private decimal AV42sendCoins ;
      private decimal AV49userFee ;
      private decimal AV21manaulFee ;
      private decimal GXt_decimal1 ;
      private decimal AV11economicalFee ;
      private decimal AV46standardFee ;
      private decimal AV15fastestFee ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string AV23networkType ;
      private string GXKey ;
      private string AV12error ;
      private string AV40PopupName ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string edtavTotalbalance_Internalname ;
      private string TempTags ;
      private string edtavTotalbalance_Jsonclick ;
      private string chkavSendallcoins_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string edtavSendcoins_Internalname ;
      private string edtavSendcoins_Jsonclick ;
      private string edtavSendto_Internalname ;
      private string AV43sendTo ;
      private string edtavDescription_Internalname ;
      private string cmbavUserfee_Internalname ;
      private string cmbavUserfee_Jsonclick ;
      private string divTable1_Internalname ;
      private string chkavActivatemanaulfee_Internalname ;
      private string edtavManaulfee_Internalname ;
      private string edtavManaulfee_Caption ;
      private string edtavManaulfee_Jsonclick ;
      private string divFeeoptionstable_Internalname ;
      private string lblTbextrafees_Internalname ;
      private string lblTbextrafees_Jsonclick ;
      private string chkavPct10_Internalname ;
      private string chkavPct20_Internalname ;
      private string chkavPct30_Internalname ;
      private string chkavPct40_Internalname ;
      private string chkavPct50_Internalname ;
      private string chkavPct60_Internalname ;
      private string chkavPct70_Internalname ;
      private string chkavPct80_Internalname ;
      private string chkavPct90_Internalname ;
      private string chkavPct100_Internalname ;
      private string lblTbfeeoptions_Internalname ;
      private string lblTbfeeoptions_Caption ;
      private string lblTbfeeoptions_Jsonclick ;
      private string cmbavFinalpercent_Internalname ;
      private string cmbavFinalpercent_Jsonclick ;
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
      private string AV13errorTitle ;
      private string AV7ApproveSpendingPopupName ;
      private string GXt_char2 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV6amIgroupOwner ;
      private bool AV18isLastSigner ;
      private bool wbLoad ;
      private bool AV41sendAllCoins ;
      private bool AV5activateManaulFee ;
      private bool AV26pct10 ;
      private bool AV28pct20 ;
      private bool AV29pct30 ;
      private bool AV30pct40 ;
      private bool AV31pct50 ;
      private bool AV32pct60 ;
      private bool AV33pct70 ;
      private bool AV34pct80 ;
      private bool AV35pct90 ;
      private bool AV27pct100 ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV8broadcast ;
      private bool AV19keyAvailable ;
      private bool GXt_boolean3 ;
      private string AV9description ;
      private string AV16feeOptionsText ;
      private string AV20levelCaption ;
      private string AV37percentages ;
      private Guid AV36pendingSpendId ;
      private GXProperties forbiddenHiddens ;
      private IGxSession AV51websession ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavSendallcoins ;
      private GXCombobox cmbavUserfee ;
      private GXCheckbox chkavActivatemanaulfee ;
      private GXCheckbox chkavPct10 ;
      private GXCheckbox chkavPct20 ;
      private GXCheckbox chkavPct30 ;
      private GXCheckbox chkavPct40 ;
      private GXCheckbox chkavPct50 ;
      private GXCheckbox chkavPct60 ;
      private GXCheckbox chkavPct70 ;
      private GXCheckbox chkavPct80 ;
      private GXCheckbox chkavPct90 ;
      private GXCheckbox chkavPct100 ;
      private GXCombobox cmbavFinalpercent ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo AV44spendInfo ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem AV25oneLevel ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
