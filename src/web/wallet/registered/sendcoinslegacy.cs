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
   public class sendcoinslegacy : GXDataArea
   {
      public sendcoinslegacy( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendcoinslegacy( IGxContext context )
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
         PA3C2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3C2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.sendcoinslegacy") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vPENDINGSPENDID", AV63pendingSpendId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV63pendingSpendId, context));
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"), context));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV62networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV62networkType, "")), context));
         GxWebStd.gx_hidden_field( context, "vMINSIG", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV45minSig), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vMINSIG", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV45minSig), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vNUMMEMBERS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV66numMembers), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vNUMMEMBERS", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV66numMembers), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, "vAMIGROUPOWNER", AV69amIgroupOwner);
         GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV69amIgroupOwner, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISLASTSIGNER", AV71isLastSigner);
         GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV71isLastSigner, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"SendCoinsLegacy");
         forbiddenHiddens.Add("totalBalance", context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\sendcoinslegacy:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vPENDINGSPENDID", AV63pendingSpendId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV63pendingSpendId, context));
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV12error));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV62networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV62networkType, "")), context));
         GxWebStd.gx_hidden_field( context, "vMINSIG", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV45minSig), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vMINSIG", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV45minSig), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vNUMMEMBERS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV66numMembers), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vNUMMEMBERS", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV66numMembers), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, "vAMIGROUPOWNER", AV69amIgroupOwner);
         GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV69amIgroupOwner, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISLASTSIGNER", AV71isLastSigner);
         GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV71isLastSigner, context));
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV25PopupName));
         GxWebStd.gx_hidden_field( context, "vPERCENTAGES", AV84percentages);
         GxWebStd.gx_hidden_field( context, "vPERCENTTOSEND", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV68percentToSend), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vPERCENTCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV67percentCount), 4, 0, ".", "")));
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
            WE3C2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3C2( ) ;
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
         return formatLink("wallet.registered.sendcoinslegacy")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.SendCoinsLegacy" ;
      }

      public override string GetPgmdesc( )
      {
         return "Send Coins from a Legacy multisignature group" ;
      }

      protected void WB3C0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavTotalbalance_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTotalbalance_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTotalbalance_Internalname, "Group balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTotalbalance_Internalname, StringUtil.LTrim( StringUtil.NToC( AV33totalBalance, 16, 8, ".", "")), StringUtil.LTrim( ((edtavTotalbalance_Enabled!=0) ? context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999") : context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,8);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotalbalance_Jsonclick, 0, "Attribute", "", "", "", "", edtavTotalbalance_Visible, edtavTotalbalance_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsLegacy.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavSendallcoins.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSendallcoins_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSendallcoins_Internalname, "Send total balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSendallcoins_Internalname, StringUtil.BoolToStr( AV28sendAllCoins), "", "Send total balance", chkavSendallcoins.Visible, chkavSendallcoins.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(13, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,13);\"");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSendcoins_Internalname, StringUtil.LTrim( StringUtil.NToC( AV29sendCoins, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV29sendCoins, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,18);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSendcoins_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSendcoins_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsLegacy.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavSendto_Internalname, StringUtil.RTrim( AV30sendTo), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,23);\"", 0, 1, edtavSendto_Enabled, 1, 80, "chr", 2, "row", 0, StyleString, ClassString, "", "", "250", 1, 0, "", "", -1, true, "NBitcoin\\scriptPubKey_address", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendCoinsLegacy.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavDescription_Internalname, AV11description, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,28);\"", 0, 1, edtavDescription_Enabled, 1, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendCoinsLegacy.htm");
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
            GxWebStd.gx_label_element( context, cmbavUserfee_Internalname, "Select Fee", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUserfee, cmbavUserfee_Internalname, StringUtil.Trim( StringUtil.Str( AV37userFee, 16, 8)), 1, cmbavUserfee_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "decimal", "", cmbavUserfee.Visible, cmbavUserfee.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,33);\"", "", true, 0, "HLP_Wallet/registered/SendCoinsLegacy.htm");
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV37userFee, 16, 8));
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavActivatemanaulfee_Internalname, StringUtil.BoolToStr( AV6activateManaulFee), "", "Manually select Fee", chkavActivatemanaulfee.Visible, chkavActivatemanaulfee.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(40, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,40);\"");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 44,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavManaulfee_Internalname, StringUtil.LTrim( StringUtil.NToC( AV21manaulFee, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV21manaulFee, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,44);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavManaulfee_Jsonclick, 0, "Attribute", "", "", "", "", edtavManaulfee_Visible, edtavManaulfee_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendCoinsLegacy.htm");
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
            GxWebStd.gx_label_ctrl( context, lblTbextrafees_Internalname, "Extra fee options for the last signer (optional, at most 3). Each one is a percentage ABOVE the fee selected above. You and the other signers sign every option; the last signer chooses which one to pay.", "", "", lblTbextrafees_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/SendCoinsLegacy.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct10_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct10_Internalname, "+10%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 55,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct10_Internalname, StringUtil.BoolToStr( AV74pct10), "", "+10%", 1, chkavPct10.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(55, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,55);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct20_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct20_Internalname, "+20%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 59,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct20_Internalname, StringUtil.BoolToStr( AV76pct20), "", "+20%", 1, chkavPct20.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(59, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,59);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct30_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct30_Internalname, "+30%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 63,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct30_Internalname, StringUtil.BoolToStr( AV77pct30), "", "+30%", 1, chkavPct30.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(63, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,63);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct40_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct40_Internalname, "+40%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 67,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct40_Internalname, StringUtil.BoolToStr( AV78pct40), "", "+40%", 1, chkavPct40.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(67, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,67);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct50_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct50_Internalname, "+50%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 71,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct50_Internalname, StringUtil.BoolToStr( AV79pct50), "", "+50%", 1, chkavPct50.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(71, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,71);\"");
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
            GxWebStd.gx_label_element( context, chkavPct60_Internalname, "+60%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 76,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct60_Internalname, StringUtil.BoolToStr( AV80pct60), "", "+60%", 1, chkavPct60.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(76, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,76);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct70_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct70_Internalname, "+70%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 80,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct70_Internalname, StringUtil.BoolToStr( AV81pct70), "", "+70%", 1, chkavPct70.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(80, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,80);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct80_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct80_Internalname, "+80%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 84,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct80_Internalname, StringUtil.BoolToStr( AV82pct80), "", "+80%", 1, chkavPct80.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(84, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,84);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct90_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct90_Internalname, "+90%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 88,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct90_Internalname, StringUtil.BoolToStr( AV83pct90), "", "+90%", 1, chkavPct90.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(88, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,88);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavPct100_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavPct100_Internalname, "+100%", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 92,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavPct100_Internalname, StringUtil.BoolToStr( AV75pct100), "", "+100%", 1, chkavPct100.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(92, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,92);\"");
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
            GxWebStd.gx_label_ctrl( context, lblTbfeeoptions_Internalname, lblTbfeeoptions_Caption, "", "", lblTbfeeoptions_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblTbfeeoptions_Visible, 1, 0, 0, "HLP_Wallet/registered/SendCoinsLegacy.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 100,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavFinalpercent, cmbavFinalpercent_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV65finalPercent), 4, 0)), 1, cmbavFinalpercent_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", cmbavFinalpercent.Visible, cmbavFinalpercent.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,100);\"", "", true, 0, "HLP_Wallet/registered/SendCoinsLegacy.htm");
            cmbavFinalpercent.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV65finalPercent), 4, 0));
            AssignProp("", false, cmbavFinalpercent_Internalname, "Values", (string)(cmbavFinalpercent.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 103,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttNext_Internalname, "", "Next", bttNext_Jsonclick, 5, "Next", "", StyleString, ClassString, bttNext_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'NEXT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsLegacy.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 106,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSendcoins_Internalname, "", "Send Coins", bttSendcoins_Jsonclick, 7, "Send Coins", "", StyleString, ClassString, bttSendcoins_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"e113c1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/SendCoinsLegacy.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 109,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel", bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendCoinsLegacy.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START3C2( )
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
         Form.Meta.addItem("description", "Send Coins from a Legacy multisignature group", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3C0( ) ;
      }

      protected void WS3C2( )
      {
         START3C2( ) ;
         EVT3C2( ) ;
      }

      protected void EVT3C2( )
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
                              E123C2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'NEXT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Next' */
                              E133C2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel' */
                              E143C2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E153C2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E163C2 ();
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

      protected void WE3C2( )
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

      protected void PA3C2( )
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
         AV28sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV28sendAllCoins));
         AssignAttri("", false, "AV28sendAllCoins", AV28sendAllCoins);
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV37userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV37userFee, 16, 8))), ".");
            AssignAttri("", false, "AV37userFee", StringUtil.LTrimStr( AV37userFee, 16, 8));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV37userFee, 16, 8));
            AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
         }
         AV6activateManaulFee = StringUtil.StrToBool( StringUtil.BoolToStr( AV6activateManaulFee));
         AssignAttri("", false, "AV6activateManaulFee", AV6activateManaulFee);
         AV74pct10 = StringUtil.StrToBool( StringUtil.BoolToStr( AV74pct10));
         AssignAttri("", false, "AV74pct10", AV74pct10);
         AV76pct20 = StringUtil.StrToBool( StringUtil.BoolToStr( AV76pct20));
         AssignAttri("", false, "AV76pct20", AV76pct20);
         AV77pct30 = StringUtil.StrToBool( StringUtil.BoolToStr( AV77pct30));
         AssignAttri("", false, "AV77pct30", AV77pct30);
         AV78pct40 = StringUtil.StrToBool( StringUtil.BoolToStr( AV78pct40));
         AssignAttri("", false, "AV78pct40", AV78pct40);
         AV79pct50 = StringUtil.StrToBool( StringUtil.BoolToStr( AV79pct50));
         AssignAttri("", false, "AV79pct50", AV79pct50);
         AV80pct60 = StringUtil.StrToBool( StringUtil.BoolToStr( AV80pct60));
         AssignAttri("", false, "AV80pct60", AV80pct60);
         AV81pct70 = StringUtil.StrToBool( StringUtil.BoolToStr( AV81pct70));
         AssignAttri("", false, "AV81pct70", AV81pct70);
         AV82pct80 = StringUtil.StrToBool( StringUtil.BoolToStr( AV82pct80));
         AssignAttri("", false, "AV82pct80", AV82pct80);
         AV83pct90 = StringUtil.StrToBool( StringUtil.BoolToStr( AV83pct90));
         AssignAttri("", false, "AV83pct90", AV83pct90);
         AV75pct100 = StringUtil.StrToBool( StringUtil.BoolToStr( AV75pct100));
         AssignAttri("", false, "AV75pct100", AV75pct100);
         if ( cmbavFinalpercent.ItemCount > 0 )
         {
            AV65finalPercent = (short)(Math.Round(NumberUtil.Val( cmbavFinalpercent.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV65finalPercent), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV65finalPercent", StringUtil.LTrimStr( (decimal)(AV65finalPercent), 4, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavFinalpercent.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV65finalPercent), 4, 0));
            AssignProp("", false, cmbavFinalpercent_Internalname, "Values", cmbavFinalpercent.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3C2( ) ;
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

      protected void RF3C2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E163C2 ();
            WB3C0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3C2( )
      {
         GxWebStd.gx_hidden_field( context, "vPENDINGSPENDID", AV63pendingSpendId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV63pendingSpendId, context));
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"), context));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV62networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV62networkType, "")), context));
         GxWebStd.gx_hidden_field( context, "vMINSIG", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV45minSig), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vMINSIG", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV45minSig), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vNUMMEMBERS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV66numMembers), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vNUMMEMBERS", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV66numMembers), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, "vAMIGROUPOWNER", AV69amIgroupOwner);
         GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV69amIgroupOwner, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISLASTSIGNER", AV71isLastSigner);
         GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV71isLastSigner, context));
      }

      protected void before_start_formulas( )
      {
         edtavTotalbalance_Enabled = 0;
         AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3C0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E123C2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            AV68percentToSend = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vPERCENTTOSEND"), ".", ","), 18, MidpointRounding.ToEven));
            AV71isLastSigner = StringUtil.StrToBool( cgiGet( "vISLASTSIGNER"));
            AssignAttri("", false, "AV71isLastSigner", AV71isLastSigner);
            GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV71isLastSigner, context));
            AV69amIgroupOwner = StringUtil.StrToBool( cgiGet( "vAMIGROUPOWNER"));
            AssignAttri("", false, "AV69amIgroupOwner", AV69amIgroupOwner);
            GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV69amIgroupOwner, context));
            AV63pendingSpendId = StringUtil.StrToGuid( cgiGet( "vPENDINGSPENDID"));
            AssignAttri("", false, "AV63pendingSpendId", AV63pendingSpendId.ToString());
            GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV63pendingSpendId, context));
            AV67percentCount = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vPERCENTCOUNT"), ".", ","), 18, MidpointRounding.ToEven));
            AV84percentages = cgiGet( "vPERCENTAGES");
            AV12error = cgiGet( "vERROR");
            AssignAttri("", false, "AV12error", AV12error);
            /* Read variables values. */
            if ( ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vTOTALBALANCE");
               GX_FocusControl = edtavTotalbalance_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV33totalBalance = 0;
               AssignAttri("", false, "AV33totalBalance", StringUtil.LTrimStr( AV33totalBalance, 16, 8));
               GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"), context));
            }
            else
            {
               AV33totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
               AssignAttri("", false, "AV33totalBalance", StringUtil.LTrimStr( AV33totalBalance, 16, 8));
               GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"), context));
            }
            AV28sendAllCoins = StringUtil.StrToBool( cgiGet( chkavSendallcoins_Internalname));
            AssignAttri("", false, "AV28sendAllCoins", AV28sendAllCoins);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vSENDCOINS");
               GX_FocusControl = edtavSendcoins_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV29sendCoins = 0;
               AssignAttri("", false, "AV29sendCoins", StringUtil.LTrimStr( AV29sendCoins, 16, 8));
            }
            else
            {
               AV29sendCoins = context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",");
               AssignAttri("", false, "AV29sendCoins", StringUtil.LTrimStr( AV29sendCoins, 16, 8));
            }
            AV30sendTo = cgiGet( edtavSendto_Internalname);
            AssignAttri("", false, "AV30sendTo", AV30sendTo);
            AV11description = cgiGet( edtavDescription_Internalname);
            AssignAttri("", false, "AV11description", AV11description);
            cmbavUserfee.CurrentValue = cgiGet( cmbavUserfee_Internalname);
            AV37userFee = NumberUtil.Val( cgiGet( cmbavUserfee_Internalname), ".");
            AssignAttri("", false, "AV37userFee", StringUtil.LTrimStr( AV37userFee, 16, 8));
            AV6activateManaulFee = StringUtil.StrToBool( cgiGet( chkavActivatemanaulfee_Internalname));
            AssignAttri("", false, "AV6activateManaulFee", AV6activateManaulFee);
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
            AV74pct10 = StringUtil.StrToBool( cgiGet( chkavPct10_Internalname));
            AssignAttri("", false, "AV74pct10", AV74pct10);
            AV76pct20 = StringUtil.StrToBool( cgiGet( chkavPct20_Internalname));
            AssignAttri("", false, "AV76pct20", AV76pct20);
            AV77pct30 = StringUtil.StrToBool( cgiGet( chkavPct30_Internalname));
            AssignAttri("", false, "AV77pct30", AV77pct30);
            AV78pct40 = StringUtil.StrToBool( cgiGet( chkavPct40_Internalname));
            AssignAttri("", false, "AV78pct40", AV78pct40);
            AV79pct50 = StringUtil.StrToBool( cgiGet( chkavPct50_Internalname));
            AssignAttri("", false, "AV79pct50", AV79pct50);
            AV80pct60 = StringUtil.StrToBool( cgiGet( chkavPct60_Internalname));
            AssignAttri("", false, "AV80pct60", AV80pct60);
            AV81pct70 = StringUtil.StrToBool( cgiGet( chkavPct70_Internalname));
            AssignAttri("", false, "AV81pct70", AV81pct70);
            AV82pct80 = StringUtil.StrToBool( cgiGet( chkavPct80_Internalname));
            AssignAttri("", false, "AV82pct80", AV82pct80);
            AV83pct90 = StringUtil.StrToBool( cgiGet( chkavPct90_Internalname));
            AssignAttri("", false, "AV83pct90", AV83pct90);
            AV75pct100 = StringUtil.StrToBool( cgiGet( chkavPct100_Internalname));
            AssignAttri("", false, "AV75pct100", AV75pct100);
            cmbavFinalpercent.CurrentValue = cgiGet( cmbavFinalpercent_Internalname);
            AV65finalPercent = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavFinalpercent_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV65finalPercent", StringUtil.LTrimStr( (decimal)(AV65finalPercent), 4, 0));
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"SendCoinsLegacy");
            AV33totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
            AssignAttri("", false, "AV33totalBalance", StringUtil.LTrimStr( AV33totalBalance, 16, 8));
            GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"), context));
            forbiddenHiddens.Add("totalBalance", context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("wallet\\registered\\sendcoinslegacy:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
         E123C2 ();
         if (returnInSub) return;
      }

      protected void E123C2( )
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
         GXt_decimal1 = AV33totalBalance;
         new GeneXus.Programs.wallet.getbalancefromhistorywithbalance(context ).execute( out  GXt_decimal1) ;
         AV33totalBalance = GXt_decimal1;
         AssignAttri("", false, "AV33totalBalance", StringUtil.LTrimStr( AV33totalBalance, 16, 8));
         GxWebStd.gx_hidden_field( context, "gxhash_vTOTALBALANCE", GetSecureSignedToken( "", context.localUtil.Format( AV33totalBalance, "ZZZZZZ9.99999999"), context));
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         new GeneXus.Programs.wallet.registered.getlegacyspendcontext(context ).execute( out  AV62networkType, out  AV69amIgroupOwner, out  AV45minSig, out  AV66numMembers, out  AV63pendingSpendId, out  AV71isLastSigner, out  AV28sendAllCoins, out  AV29sendCoins, out  AV30sendTo, out  AV11description, out  AV21manaulFee, out  AV85spendInfo, out  AV12error) ;
         AssignAttri("", false, "AV62networkType", AV62networkType);
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV62networkType, "")), context));
         AssignAttri("", false, "AV69amIgroupOwner", AV69amIgroupOwner);
         GxWebStd.gx_hidden_field( context, "gxhash_vAMIGROUPOWNER", GetSecureSignedToken( "", AV69amIgroupOwner, context));
         AssignAttri("", false, "AV45minSig", StringUtil.LTrimStr( (decimal)(AV45minSig), 4, 0));
         GxWebStd.gx_hidden_field( context, "gxhash_vMINSIG", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV45minSig), "ZZZ9"), context));
         AssignAttri("", false, "AV66numMembers", StringUtil.LTrimStr( (decimal)(AV66numMembers), 4, 0));
         GxWebStd.gx_hidden_field( context, "gxhash_vNUMMEMBERS", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV66numMembers), "ZZZ9"), context));
         AssignAttri("", false, "AV63pendingSpendId", AV63pendingSpendId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vPENDINGSPENDID", GetSecureSignedToken( "", AV63pendingSpendId, context));
         AssignAttri("", false, "AV71isLastSigner", AV71isLastSigner);
         GxWebStd.gx_hidden_field( context, "gxhash_vISLASTSIGNER", GetSecureSignedToken( "", AV71isLastSigner, context));
         AssignAttri("", false, "AV28sendAllCoins", AV28sendAllCoins);
         AssignAttri("", false, "AV29sendCoins", StringUtil.LTrimStr( AV29sendCoins, 16, 8));
         AssignAttri("", false, "AV30sendTo", AV30sendTo);
         AssignAttri("", false, "AV11description", AV11description);
         AssignAttri("", false, "AV21manaulFee", StringUtil.LTrimStr( AV21manaulFee, 16, 8));
         AssignAttri("", false, "AV12error", AV12error);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"This payment can not be signed",(string)AV12error}, true);
            bttNext_Visible = 0;
            AssignProp("", false, bttNext_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttNext_Visible), 5, 0), true);
         }
         if ( ! (Guid.Empty==AV63pendingSpendId) )
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
            AV70feeOptionsText = "";
            AV86GXV1 = 1;
            while ( AV86GXV1 <= AV85spendInfo.gxTpr_Levels.Count )
            {
               AV73oneLevel = ((GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem)AV85spendInfo.gxTpr_Levels.Item(AV86GXV1));
               if ( AV73oneLevel.gxTpr_Percent == 0 )
               {
                  AV72levelCaption = "Calculated fee: " + StringUtil.Trim( StringUtil.Str( AV73oneLevel.gxTpr_Feebtc, 16, 8)) + " BTC";
               }
               else
               {
                  AV72levelCaption = "+" + StringUtil.Trim( StringUtil.Str( (decimal)(AV73oneLevel.gxTpr_Percent), 4, 0)) + " %: " + StringUtil.Trim( StringUtil.Str( AV73oneLevel.gxTpr_Feebtc, 16, 8)) + " BTC";
               }
               cmbavFinalpercent.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(AV73oneLevel.gxTpr_Percent), 4, 0)), AV72levelCaption, 0);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV70feeOptionsText)) )
               {
                  AV70feeOptionsText += " | ";
               }
               AV70feeOptionsText += AV72levelCaption;
               AV86GXV1 = (int)(AV86GXV1+1);
            }
            if ( AV71isLastSigner )
            {
               AV65finalPercent = 0;
               AssignAttri("", false, "AV65finalPercent", StringUtil.LTrimStr( (decimal)(AV65finalPercent), 4, 0));
               cmbavFinalpercent.Visible = 1;
               AssignProp("", false, cmbavFinalpercent_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavFinalpercent.Visible), 5, 0), true);
               lblTbfeeoptions_Caption = "You are the last signer: choose the fee to pay. You sign only that transaction and it is sent to the network.";
               AssignProp("", false, lblTbfeeoptions_Internalname, "Caption", lblTbfeeoptions_Caption, true);
            }
            else
            {
               lblTbfeeoptions_Caption = "Fee options set by the first signer (the last signer chooses one): "+AV70feeOptionsText;
               AssignProp("", false, lblTbfeeoptions_Internalname, "Caption", lblTbfeeoptions_Caption, true);
            }
            lblTbfeeoptions_Visible = 1;
            AssignProp("", false, lblTbfeeoptions_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbfeeoptions_Visible), 5, 0), true);
         }
      }

      protected void E133C2( )
      {
         /* 'Next' Routine */
         returnInSub = false;
         if ( ! (Guid.Empty==AV63pendingSpendId) )
         {
            chkavSendallcoins.Enabled = 0;
            AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
            /* Execute user subroutine: 'SHOW SEND' */
            S112 ();
            if (returnInSub) return;
            edtavManaulfee_Enabled = 0;
            AssignProp("", false, edtavManaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Enabled), 5, 0), true);
         }
         else
         {
            if ( ( AV29sendCoins >= AV33totalBalance ) && ! AV28sendAllCoins )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You don't have enough balance",(string)AV12error}, true);
            }
            else
            {
               if ( (Convert.ToDecimal(0)==AV29sendCoins) && ! AV28sendAllCoins )
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You have to select an amount to send",(string)AV12error}, true);
               }
               else
               {
                  GXt_char2 = AV12error;
                  new GeneXus.Programs.nbitcoin.isaddressvalid(context ).execute(  AV30sendTo,  AV62networkType, out  GXt_char2) ;
                  AV12error = GXt_char2;
                  AssignAttri("", false, "AV12error", AV12error);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Please check the Send to address: ",(string)AV12error}, true);
                  }
                  else
                  {
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11description)) )
                     {
                        this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"You have to enter a description",(string)"The description is mandatory on multisignature transactions"}, true);
                     }
                     else
                     {
                        chkavSendallcoins.Enabled = 0;
                        AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
                        /* Execute user subroutine: 'ESTIMATE FEES' */
                        S122 ();
                        if (returnInSub) return;
                     }
                  }
               }
            }
         }
         /*  Sending Event outputs  */
         cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV37userFee, 16, 8));
         AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
      }

      protected void E143C2( )
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
      }

      protected void E153C2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV7ApproveSpendingPopupName = "Wallet.ApproveSpending";
         AV5strFound = (short)(StringUtil.StringSearch( AV25PopupName, StringUtil.Lower( AV7ApproveSpendingPopupName), 1));
         if ( AV5strFound > 0 )
         {
            GXt_boolean3 = AV61keyAvailable;
            new GeneXus.Programs.wallet.registered.hassigningkeybip48(context ).execute( out  GXt_boolean3) ;
            AV61keyAvailable = GXt_boolean3;
            if ( ! AV61keyAvailable )
            {
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Not signed",(string)"The payment was not signed: the password was not approved"}, true);
            }
            else
            {
               GXt_char2 = AV12error;
               new GeneXus.Programs.wallet.registered.sendlegacyspend(context ).execute(  AV28sendAllCoins,  AV29sendCoins,  AV30sendTo,  AV11description,  AV21manaulFee,  AV84percentages,  AV68percentToSend, out  AV59broadcast, out  AV60errorTitle, out  GXt_char2) ;
               AV12error = GXt_char2;
               AssignAttri("", false, "AV12error", AV12error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) && String.IsNullOrEmpty(StringUtil.RTrim( AV60errorTitle)) )
               {
                  if ( AV59broadcast )
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Transaction broadcast",(string)"The transaction was signed and submitted to the network"}, true);
                  }
                  else
                  {
                     this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Signature added",(string)"Your signature was added and forwarded to the other signers"}, true);
                  }
                  AV39websession.Set("MuSign_ONE", "");
                  context.setWebReturnParms(new Object[] {});
                  context.setWebReturnParmsMetadata(new Object[] {});
                  context.wjLocDisableFrm = 1;
                  context.nUserReturn = 1;
                  returnInSub = true;
                  if (true) return;
               }
               else
               {
                  new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)AV60errorTitle,(string)AV12error}, true);
               }
            }
         }
         /*  Sending Event outputs  */
      }

      protected void S112( )
      {
         /* 'SHOW SEND' Routine */
         returnInSub = false;
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
      }

      protected void S122( )
      {
         /* 'ESTIMATE FEES' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'SHOW SEND' */
         S112 ();
         if (returnInSub) return;
         AV54transactionFee = NumberUtil.Val( "0.00001000", ".");
         GXt_objcol_SdtSDTAddressHistory4 = AV36transactionsToSend;
         new GeneXus.Programs.wallet.selectcoinstosend(context ).execute(  AV11description,  AV29sendCoins,  AV54transactionFee, out  GXt_objcol_SdtSDTAddressHistory4) ;
         AV36transactionsToSend = GXt_objcol_SdtSDTAddressHistory4;
         AV46numInputs = (short)(AV36transactionsToSend.Count);
         if ( AV28sendAllCoins )
         {
            AV48numOutputs = 1;
         }
         else
         {
            AV48numOutputs = 2;
         }
         new GeneXus.Programs.wallet.registered.estimatelegacyvsize(context ).execute(  AV46numInputs,  AV48numOutputs,  AV45minSig,  AV66numMembers, out  AV55virtualSize) ;
         GXt_char2 = AV12error;
         GXt_int5 = (short)(AV40economicalBlocks);
         new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV55virtualSize,  60,  "economical", out  AV41economicalFee, out  GXt_int5, out  GXt_char2) ;
         AV40economicalBlocks = GXt_int5;
         AV12error = GXt_char2;
         AssignAttri("", false, "AV12error", AV12error);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
         {
            GXt_char2 = AV12error;
            GXt_int5 = (short)(AV52standarBlocks);
            new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV55virtualSize,  6,  "conservative", out  AV53standardFee, out  GXt_int5, out  GXt_char2) ;
            AV52standarBlocks = GXt_int5;
            AV12error = GXt_char2;
            AssignAttri("", false, "AV12error", AV12error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               GXt_char2 = AV12error;
               GXt_int5 = (short)(AV43fastestBlocks);
               new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV55virtualSize,  1,  "conservative", out  AV44fastestFee, out  GXt_int5, out  GXt_char2) ;
               AV43fastestBlocks = GXt_int5;
               AV12error = GXt_char2;
               AssignAttri("", false, "AV12error", AV12error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(0), 16, 8)), "Select Estimated Transaction Fee", 0);
                  cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV41economicalFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV41economicalFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV40economicalBlocks), 10, 0)+" Blocks", 0);
                  cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV53standardFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV53standardFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV52standarBlocks), 10, 0)+" Blocks", 0);
                  cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV44fastestFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV44fastestFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV43fastestBlocks), 10, 0)+" Blocks", 0);
                  cmbavUserfee.Visible = 1;
                  AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
                  edtavManaulfee_Enabled = 0;
                  AssignProp("", false, edtavManaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Enabled), 5, 0), true);
                  chkavActivatemanaulfee.Visible = 1;
                  AssignProp("", false, chkavActivatemanaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavActivatemanaulfee.Visible), 5, 0), true);
                  if ( ! AV69amIgroupOwner && ! AV71isLastSigner )
                  {
                     divFeeoptionstable_Visible = 1;
                     AssignProp("", false, divFeeoptionstable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divFeeoptionstable_Visible), 5, 0), true);
                  }
               }
               else
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculating the fastest fee: ",(string)AV12error}, true);
               }
            }
            else
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculating the standard fee: ",(string)AV12error}, true);
            }
         }
         else
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculating the economical fee: ",(string)AV12error}, true);
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E163C2( )
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
         PA3C2( ) ;
         WS3C2( ) ;
         WE3C2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610161785085", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/sendcoinslegacy.js", "?202610161785085", false, true, false);
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
         AV28sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV28sendAllCoins));
         AssignAttri("", false, "AV28sendAllCoins", AV28sendAllCoins);
         cmbavUserfee.Name = "vUSERFEE";
         cmbavUserfee.WebTags = "";
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV37userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV37userFee, 16, 8))), ".");
            AssignAttri("", false, "AV37userFee", StringUtil.LTrimStr( AV37userFee, 16, 8));
         }
         chkavActivatemanaulfee.Name = "vACTIVATEMANAULFEE";
         chkavActivatemanaulfee.WebTags = "";
         chkavActivatemanaulfee.Caption = "Manually select Fee";
         AssignProp("", false, chkavActivatemanaulfee_Internalname, "TitleCaption", chkavActivatemanaulfee.Caption, true);
         chkavActivatemanaulfee.CheckedValue = "false";
         AV6activateManaulFee = StringUtil.StrToBool( StringUtil.BoolToStr( AV6activateManaulFee));
         AssignAttri("", false, "AV6activateManaulFee", AV6activateManaulFee);
         chkavPct10.Name = "vPCT10";
         chkavPct10.WebTags = "";
         chkavPct10.Caption = "+10%";
         AssignProp("", false, chkavPct10_Internalname, "TitleCaption", chkavPct10.Caption, true);
         chkavPct10.CheckedValue = "false";
         AV74pct10 = StringUtil.StrToBool( StringUtil.BoolToStr( AV74pct10));
         AssignAttri("", false, "AV74pct10", AV74pct10);
         chkavPct20.Name = "vPCT20";
         chkavPct20.WebTags = "";
         chkavPct20.Caption = "+20%";
         AssignProp("", false, chkavPct20_Internalname, "TitleCaption", chkavPct20.Caption, true);
         chkavPct20.CheckedValue = "false";
         AV76pct20 = StringUtil.StrToBool( StringUtil.BoolToStr( AV76pct20));
         AssignAttri("", false, "AV76pct20", AV76pct20);
         chkavPct30.Name = "vPCT30";
         chkavPct30.WebTags = "";
         chkavPct30.Caption = "+30%";
         AssignProp("", false, chkavPct30_Internalname, "TitleCaption", chkavPct30.Caption, true);
         chkavPct30.CheckedValue = "false";
         AV77pct30 = StringUtil.StrToBool( StringUtil.BoolToStr( AV77pct30));
         AssignAttri("", false, "AV77pct30", AV77pct30);
         chkavPct40.Name = "vPCT40";
         chkavPct40.WebTags = "";
         chkavPct40.Caption = "+40%";
         AssignProp("", false, chkavPct40_Internalname, "TitleCaption", chkavPct40.Caption, true);
         chkavPct40.CheckedValue = "false";
         AV78pct40 = StringUtil.StrToBool( StringUtil.BoolToStr( AV78pct40));
         AssignAttri("", false, "AV78pct40", AV78pct40);
         chkavPct50.Name = "vPCT50";
         chkavPct50.WebTags = "";
         chkavPct50.Caption = "+50%";
         AssignProp("", false, chkavPct50_Internalname, "TitleCaption", chkavPct50.Caption, true);
         chkavPct50.CheckedValue = "false";
         AV79pct50 = StringUtil.StrToBool( StringUtil.BoolToStr( AV79pct50));
         AssignAttri("", false, "AV79pct50", AV79pct50);
         chkavPct60.Name = "vPCT60";
         chkavPct60.WebTags = "";
         chkavPct60.Caption = "+60%";
         AssignProp("", false, chkavPct60_Internalname, "TitleCaption", chkavPct60.Caption, true);
         chkavPct60.CheckedValue = "false";
         AV80pct60 = StringUtil.StrToBool( StringUtil.BoolToStr( AV80pct60));
         AssignAttri("", false, "AV80pct60", AV80pct60);
         chkavPct70.Name = "vPCT70";
         chkavPct70.WebTags = "";
         chkavPct70.Caption = "+70%";
         AssignProp("", false, chkavPct70_Internalname, "TitleCaption", chkavPct70.Caption, true);
         chkavPct70.CheckedValue = "false";
         AV81pct70 = StringUtil.StrToBool( StringUtil.BoolToStr( AV81pct70));
         AssignAttri("", false, "AV81pct70", AV81pct70);
         chkavPct80.Name = "vPCT80";
         chkavPct80.WebTags = "";
         chkavPct80.Caption = "+80%";
         AssignProp("", false, chkavPct80_Internalname, "TitleCaption", chkavPct80.Caption, true);
         chkavPct80.CheckedValue = "false";
         AV82pct80 = StringUtil.StrToBool( StringUtil.BoolToStr( AV82pct80));
         AssignAttri("", false, "AV82pct80", AV82pct80);
         chkavPct90.Name = "vPCT90";
         chkavPct90.WebTags = "";
         chkavPct90.Caption = "+90%";
         AssignProp("", false, chkavPct90_Internalname, "TitleCaption", chkavPct90.Caption, true);
         chkavPct90.CheckedValue = "false";
         AV83pct90 = StringUtil.StrToBool( StringUtil.BoolToStr( AV83pct90));
         AssignAttri("", false, "AV83pct90", AV83pct90);
         chkavPct100.Name = "vPCT100";
         chkavPct100.WebTags = "";
         chkavPct100.Caption = "+100%";
         AssignProp("", false, chkavPct100_Internalname, "TitleCaption", chkavPct100.Caption, true);
         chkavPct100.CheckedValue = "false";
         AV75pct100 = StringUtil.StrToBool( StringUtil.BoolToStr( AV75pct100));
         AssignAttri("", false, "AV75pct100", AV75pct100);
         cmbavFinalpercent.Name = "vFINALPERCENT";
         cmbavFinalpercent.WebTags = "";
         if ( cmbavFinalpercent.ItemCount > 0 )
         {
            AV65finalPercent = (short)(Math.Round(NumberUtil.Val( cmbavFinalpercent.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV65finalPercent), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV65finalPercent", StringUtil.LTrimStr( (decimal)(AV65finalPercent), 4, 0));
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
         Form.Caption = "Send Coins from a Legacy multisignature group";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV28sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV6activateManaulFee","fld":"vACTIVATEMANAULFEE","type":"boolean"},{"av":"AV74pct10","fld":"vPCT10","type":"boolean"},{"av":"AV76pct20","fld":"vPCT20","type":"boolean"},{"av":"AV77pct30","fld":"vPCT30","type":"boolean"},{"av":"AV78pct40","fld":"vPCT40","type":"boolean"},{"av":"AV79pct50","fld":"vPCT50","type":"boolean"},{"av":"AV80pct60","fld":"vPCT60","type":"boolean"},{"av":"AV81pct70","fld":"vPCT70","type":"boolean"},{"av":"AV82pct80","fld":"vPCT80","type":"boolean"},{"av":"AV83pct90","fld":"vPCT90","type":"boolean"},{"av":"AV75pct100","fld":"vPCT100","type":"boolean"},{"av":"AV63pendingSpendId","fld":"vPENDINGSPENDID","hsh":true,"type":"guid"},{"av":"AV62networkType","fld":"vNETWORKTYPE","hsh":true,"type":"char"},{"av":"AV45minSig","fld":"vMINSIG","pic":"ZZZ9","hsh":true,"type":"int"},{"av":"AV66numMembers","fld":"vNUMMEMBERS","pic":"ZZZ9","hsh":true,"type":"int"},{"av":"AV69amIgroupOwner","fld":"vAMIGROUPOWNER","hsh":true,"type":"boolean"},{"av":"AV71isLastSigner","fld":"vISLASTSIGNER","hsh":true,"type":"boolean"},{"av":"AV33totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","hsh":true,"type":"decimal"}]}""");
         setEventMetadata("'NEXT'","""{"handler":"E133C2","iparms":[{"av":"AV63pendingSpendId","fld":"vPENDINGSPENDID","hsh":true,"type":"guid"},{"av":"AV29sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV33totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","hsh":true,"type":"decimal"},{"av":"AV28sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"AV30sendTo","fld":"vSENDTO","type":"char"},{"av":"AV62networkType","fld":"vNETWORKTYPE","hsh":true,"type":"char"},{"av":"AV11description","fld":"vDESCRIPTION","type":"svchar"},{"av":"AV45minSig","fld":"vMINSIG","pic":"ZZZ9","hsh":true,"type":"int"},{"av":"AV66numMembers","fld":"vNUMMEMBERS","pic":"ZZZ9","hsh":true,"type":"int"},{"av":"cmbavUserfee"},{"av":"AV37userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV69amIgroupOwner","fld":"vAMIGROUPOWNER","hsh":true,"type":"boolean"},{"av":"AV71isLastSigner","fld":"vISLASTSIGNER","hsh":true,"type":"boolean"}]""");
         setEventMetadata("'NEXT'",""","oparms":[{"av":"chkavSendallcoins.Enabled","ctrl":"vSENDALLCOINS","prop":"Enabled"},{"av":"edtavManaulfee_Enabled","ctrl":"vMANAULFEE","prop":"Enabled"},{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"edtavSendcoins_Enabled","ctrl":"vSENDCOINS","prop":"Enabled"},{"av":"edtavSendto_Enabled","ctrl":"vSENDTO","prop":"Enabled"},{"av":"edtavDescription_Enabled","ctrl":"vDESCRIPTION","prop":"Enabled"},{"ctrl":"NEXT","prop":"Visible"},{"ctrl":"SENDCOINS","prop":"Visible"},{"av":"edtavManaulfee_Visible","ctrl":"vMANAULFEE","prop":"Visible"},{"av":"cmbavUserfee"},{"av":"AV37userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"chkavActivatemanaulfee.Visible","ctrl":"vACTIVATEMANAULFEE","prop":"Visible"},{"av":"divFeeoptionstable_Visible","ctrl":"FEEOPTIONSTABLE","prop":"Visible"}]}""");
         setEventMetadata("'SEND COINS'","""{"handler":"E113C1","iparms":[{"av":"AV21manaulFee","fld":"vMANAULFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV12error","fld":"vERROR","type":"char"},{"av":"AV63pendingSpendId","fld":"vPENDINGSPENDID","hsh":true,"type":"guid"},{"av":"AV69amIgroupOwner","fld":"vAMIGROUPOWNER","hsh":true,"type":"boolean"},{"av":"AV71isLastSigner","fld":"vISLASTSIGNER","hsh":true,"type":"boolean"},{"av":"AV74pct10","fld":"vPCT10","type":"boolean"},{"av":"AV76pct20","fld":"vPCT20","type":"boolean"},{"av":"AV77pct30","fld":"vPCT30","type":"boolean"},{"av":"AV78pct40","fld":"vPCT40","type":"boolean"},{"av":"AV79pct50","fld":"vPCT50","type":"boolean"},{"av":"AV80pct60","fld":"vPCT60","type":"boolean"},{"av":"AV81pct70","fld":"vPCT70","type":"boolean"},{"av":"AV82pct80","fld":"vPCT80","type":"boolean"},{"av":"AV83pct90","fld":"vPCT90","type":"boolean"},{"av":"AV75pct100","fld":"vPCT100","type":"boolean"},{"av":"cmbavFinalpercent"},{"av":"AV65finalPercent","fld":"vFINALPERCENT","pic":"ZZZ9","type":"int"}]""");
         setEventMetadata("'SEND COINS'",""","oparms":[{"av":"AV84percentages","fld":"vPERCENTAGES","type":"svchar"},{"av":"AV68percentToSend","fld":"vPERCENTTOSEND","pic":"ZZZ9","type":"int"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E143C2","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E153C2","iparms":[{"av":"AV25PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV28sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV29sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV30sendTo","fld":"vSENDTO","type":"char"},{"av":"AV11description","fld":"vDESCRIPTION","type":"svchar"},{"av":"AV21manaulFee","fld":"vMANAULFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV84percentages","fld":"vPERCENTAGES","type":"svchar"},{"av":"AV68percentToSend","fld":"vPERCENTTOSEND","pic":"ZZZ9","type":"int"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV12error","fld":"vERROR","type":"char"}]}""");
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
         AV63pendingSpendId = Guid.Empty;
         AV62networkType = "";
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV12error = "";
         AV25PopupName = "";
         AV84percentages = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV30sendTo = "";
         AV11description = "";
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
         AV85spendInfo = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo(context);
         AV70feeOptionsText = "";
         AV73oneLevel = new GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem(context);
         AV72levelCaption = "";
         AV39websession = context.GetSession();
         AV7ApproveSpendingPopupName = "";
         AV60errorTitle = "";
         AV36transactionsToSend = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_objcol_SdtSDTAddressHistory4 = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         GXt_char2 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
         edtavTotalbalance_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short AV45minSig ;
      private short AV66numMembers ;
      private short AV68percentToSend ;
      private short AV67percentCount ;
      private short wbEnd ;
      private short wbStart ;
      private short AV65finalPercent ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short AV5strFound ;
      private short AV46numInputs ;
      private short AV48numOutputs ;
      private short GXt_int5 ;
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
      private int AV86GXV1 ;
      private int idxLst ;
      private long AV55virtualSize ;
      private long AV40economicalBlocks ;
      private long AV52standarBlocks ;
      private long AV43fastestBlocks ;
      private decimal AV33totalBalance ;
      private decimal AV29sendCoins ;
      private decimal AV37userFee ;
      private decimal AV21manaulFee ;
      private decimal GXt_decimal1 ;
      private decimal AV54transactionFee ;
      private decimal AV41economicalFee ;
      private decimal AV53standardFee ;
      private decimal AV44fastestFee ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string AV62networkType ;
      private string GXKey ;
      private string AV12error ;
      private string AV25PopupName ;
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
      private string AV30sendTo ;
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
      private string AV7ApproveSpendingPopupName ;
      private string AV60errorTitle ;
      private string GXt_char2 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV69amIgroupOwner ;
      private bool AV71isLastSigner ;
      private bool wbLoad ;
      private bool AV28sendAllCoins ;
      private bool AV6activateManaulFee ;
      private bool AV74pct10 ;
      private bool AV76pct20 ;
      private bool AV77pct30 ;
      private bool AV78pct40 ;
      private bool AV79pct50 ;
      private bool AV80pct60 ;
      private bool AV81pct70 ;
      private bool AV82pct80 ;
      private bool AV83pct90 ;
      private bool AV75pct100 ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV61keyAvailable ;
      private bool GXt_boolean3 ;
      private bool AV59broadcast ;
      private string AV84percentages ;
      private string AV11description ;
      private string AV70feeOptionsText ;
      private string AV72levelCaption ;
      private Guid AV63pendingSpendId ;
      private GXProperties forbiddenHiddens ;
      private IGxSession AV39websession ;
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
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo AV85spendInfo ;
      private GeneXus.Programs.wallet.registered.SdtDelegatedSpendInfo_levelsItem AV73oneLevel ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV36transactionsToSend ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> GXt_objcol_SdtSDTAddressHistory4 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
