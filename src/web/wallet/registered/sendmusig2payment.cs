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
   public class sendmusig2payment : GXDataArea
   {
      public sendmusig2payment( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public sendmusig2payment( IGxContext context )
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
         PA3S2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3S2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.sendmusig2payment") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV15groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV15groupId, context));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV17networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV17networkType, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_boolean_hidden_field( context, "vCHECKED", AV7checked);
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV15groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV15groupId, context));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV17networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV17networkType, "")), context));
         GxWebStd.gx_hidden_field( context, "vSTANDARDFEE", StringUtil.LTrim( StringUtil.NToC( AV27standardFee, 16, 8, ".", "")));
         GxWebStd.gx_hidden_field( context, "vSTANDARBLOCKS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV26standarBlocks), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vFASTESTFEE", StringUtil.LTrim( StringUtil.NToC( AV14fastestFee, 16, 8, ".", "")));
         GxWebStd.gx_hidden_field( context, "vFASTESTBLOCKS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13fastestBlocks), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vSELECTEDCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV22selectedCount), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV19PopupName));
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
            WE3S2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3S2( ) ;
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
         return formatLink("wallet.registered.sendmusig2payment")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.SendMuSig2Payment" ;
      }

      public override string GetPgmdesc( )
      {
         return "Send Coins from a MuSig2 group wallet: the owner of a Delegated MultiSig 2 pays alone; a member opens a signing room" ;
      }

      protected void WB3S0( )
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbmode_Internalname, lblTbmode_Caption, "", "", lblTbmode_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/SendMuSig2Payment.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTotalbalance_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTotalbalance_Internalname, "Group balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTotalbalance_Internalname, StringUtil.LTrim( StringUtil.NToC( AV28totalBalance, 16, 8, ".", "")), StringUtil.LTrim( ((edtavTotalbalance_Enabled!=0) ? context.localUtil.Format( AV28totalBalance, "ZZZZZZ9.99999999") : context.localUtil.Format( AV28totalBalance, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,11);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotalbalance_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTotalbalance_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendMuSig2Payment.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table1_14_3S2( true) ;
         }
         else
         {
            wb_table1_14_3S2( false) ;
         }
         return  ;
      }

      protected void wb_table1_14_3S2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSendallcoins_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSendallcoins_Internalname, "Send total balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSendallcoins_Internalname, StringUtil.BoolToStr( AV23sendAllCoins), "", "Send total balance", 1, chkavSendallcoins.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(24, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,24);\"");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSendcoins_Internalname, StringUtil.LTrim( StringUtil.NToC( AV24sendCoins, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV24sendCoins, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,29);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSendcoins_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSendcoins_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendMuSig2Payment.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 34,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavSendto_Internalname, StringUtil.RTrim( AV25sendTo), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,34);\"", 0, 1, edtavSendto_Enabled, 1, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "NBitcoin\\scriptPubKey_address", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendMuSig2Payment.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavDescription_Internalname, AV8description, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,39);\"", 0, 1, edtavDescription_Enabled, 1, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SendMuSig2Payment.htm");
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
            GxWebStd.gx_label_element( context, cmbavUserfee_Internalname, "Select Fee", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 44,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUserfee, cmbavUserfee_Internalname, StringUtil.Trim( StringUtil.Str( AV29userFee, 16, 8)), 1, cmbavUserfee_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "decimal", "", cmbavUserfee.Visible, cmbavUserfee.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,44);\"", "", true, 0, "HLP_Wallet/registered/SendMuSig2Payment.htm");
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV29userFee, 16, 8));
            AssignProp("", false, cmbavUserfee_Internalname, "Values", (string)(cmbavUserfee.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavActivatemanaulfee.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavActivatemanaulfee_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavActivatemanaulfee_Internalname, "Manually select Fee", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 49,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavActivatemanaulfee_Internalname, StringUtil.BoolToStr( AV5activateManaulFee), "", "Manually select Fee", chkavActivatemanaulfee.Visible, chkavActivatemanaulfee.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(49, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,49);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavManaulfee_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavManaulfee_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavManaulfee_Internalname, "Fee (BTC)", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 54,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavManaulfee_Internalname, StringUtil.LTrim( StringUtil.NToC( AV16manaulFee, 16, 8, ".", "")), StringUtil.LTrim( context.localUtil.Format( AV16manaulFee, "ZZZZZZ9.99999999")), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,54);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavManaulfee_Jsonclick, 0, "Attribute", "", "", "", "", edtavManaulfee_Visible, edtavManaulfee_Enabled, 1, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/SendMuSig2Payment.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table2_57_3S2( true) ;
         }
         else
         {
            wb_table2_57_3S2( false) ;
         }
         return  ;
      }

      protected void wb_table2_57_3S2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START3S2( )
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
         Form.Meta.addItem("description", "Send Coins from a MuSig2 group wallet: the owner of a Delegated MultiSig 2 pays alone; a member opens a signing room", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3S0( ) ;
      }

      protected void WS3S2( )
      {
         START3S2( ) ;
         EVT3S2( ) ;
      }

      protected void EVT3S2( )
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
                              E113S2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'OPEN ROOM'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Open room' */
                              E123S2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'NEXT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Next' */
                              E133S2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel' */
                              E143S2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E153S2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E163S2 ();
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

      protected void WE3S2( )
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

      protected void PA3S2( )
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
         AV23sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV23sendAllCoins));
         AssignAttri("", false, "AV23sendAllCoins", AV23sendAllCoins);
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV29userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV29userFee, 16, 8))), ".");
            AssignAttri("", false, "AV29userFee", StringUtil.LTrimStr( AV29userFee, 16, 8));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV29userFee, 16, 8));
            AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
         }
         AV5activateManaulFee = StringUtil.StrToBool( StringUtil.BoolToStr( AV5activateManaulFee));
         AssignAttri("", false, "AV5activateManaulFee", AV5activateManaulFee);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3S2( ) ;
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

      protected void RF3S2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E163S2 ();
            WB3S0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3S2( )
      {
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV15groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV15groupId, context));
         GxWebStd.gx_hidden_field( context, "vNETWORKTYPE", StringUtil.RTrim( AV17networkType));
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV17networkType, "")), context));
      }

      protected void before_start_formulas( )
      {
         edtavTotalbalance_Enabled = 0;
         AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3S0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E113S2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            AV22selectedCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vSELECTEDCOUNT"), ".", ","), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV22selectedCount", StringUtil.LTrimStr( (decimal)(AV22selectedCount), 10, 0));
            /* Read variables values. */
            if ( ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vTOTALBALANCE");
               GX_FocusControl = edtavTotalbalance_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV28totalBalance = 0;
               AssignAttri("", false, "AV28totalBalance", StringUtil.LTrimStr( AV28totalBalance, 16, 8));
            }
            else
            {
               AV28totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
               AssignAttri("", false, "AV28totalBalance", StringUtil.LTrimStr( AV28totalBalance, 16, 8));
            }
            AV23sendAllCoins = StringUtil.StrToBool( cgiGet( chkavSendallcoins_Internalname));
            AssignAttri("", false, "AV23sendAllCoins", AV23sendAllCoins);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vSENDCOINS");
               GX_FocusControl = edtavSendcoins_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV24sendCoins = 0;
               AssignAttri("", false, "AV24sendCoins", StringUtil.LTrimStr( AV24sendCoins, 16, 8));
            }
            else
            {
               AV24sendCoins = context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",");
               AssignAttri("", false, "AV24sendCoins", StringUtil.LTrimStr( AV24sendCoins, 16, 8));
            }
            AV25sendTo = cgiGet( edtavSendto_Internalname);
            AssignAttri("", false, "AV25sendTo", AV25sendTo);
            AV8description = cgiGet( edtavDescription_Internalname);
            AssignAttri("", false, "AV8description", AV8description);
            cmbavUserfee.CurrentValue = cgiGet( cmbavUserfee_Internalname);
            AV29userFee = NumberUtil.Val( cgiGet( cmbavUserfee_Internalname), ".");
            AssignAttri("", false, "AV29userFee", StringUtil.LTrimStr( AV29userFee, 16, 8));
            AV5activateManaulFee = StringUtil.StrToBool( cgiGet( chkavActivatemanaulfee_Internalname));
            AssignAttri("", false, "AV5activateManaulFee", AV5activateManaulFee);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vMANAULFEE");
               GX_FocusControl = edtavManaulfee_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV16manaulFee = 0;
               AssignAttri("", false, "AV16manaulFee", StringUtil.LTrimStr( AV16manaulFee, 16, 8));
            }
            else
            {
               AV16manaulFee = context.localUtil.CToN( cgiGet( edtavManaulfee_Internalname), ".", ",");
               AssignAttri("", false, "AV16manaulFee", StringUtil.LTrimStr( AV16manaulFee, 16, 8));
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
         E113S2 ();
         if (returnInSub) return;
      }

      protected void E113S2( )
      {
         /* Start Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         new GeneXus.Programs.wallet.clearselectedcoins(context ).execute( ) ;
         GXt_decimal1 = AV28totalBalance;
         new GeneXus.Programs.wallet.getbalancefromhistorywithbalance(context ).execute( out  GXt_decimal1) ;
         AV28totalBalance = GXt_decimal1;
         AssignAttri("", false, "AV28totalBalance", StringUtil.LTrimStr( AV28totalBalance, 16, 8));
         new GeneXus.Programs.wallet.registered.getmusig2sendcontext(context ).execute( out  AV15groupId, out  AV18ownerPaysAlone, out  AV17networkType) ;
         AssignAttri("", false, "AV15groupId", AV15groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV15groupId, context));
         AssignAttri("", false, "AV17networkType", AV17networkType);
         GxWebStd.gx_hidden_field( context, "gxhash_vNETWORKTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV17networkType, "")), context));
         bttSendcoins_Visible = 0;
         AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
         cmbavUserfee.Visible = 0;
         AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
         chkavActivatemanaulfee.Visible = 0;
         AssignProp("", false, chkavActivatemanaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavActivatemanaulfee.Visible), 5, 0), true);
         edtavManaulfee_Visible = 0;
         AssignProp("", false, edtavManaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Visible), 5, 0), true);
         if ( AV18ownerPaysAlone )
         {
            lblTbmode_Caption = "You are the owner of this Delegated MultiSig 2 group: you pay alone, with one signature.";
            AssignProp("", false, lblTbmode_Internalname, "Caption", lblTbmode_Caption, true);
            bttOpenroom_Visible = 0;
            AssignProp("", false, bttOpenroom_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttOpenroom_Visible), 5, 0), true);
         }
         else
         {
            lblTbmode_Caption = "Open a signing room: the other members see it in the Signing Rooms tab of the group and press I'm here. When enough of them are here, press Sign now there; every signer then signs with his password within 3 minutes. The fee is the network's at that moment.";
            AssignProp("", false, lblTbmode_Internalname, "Caption", lblTbmode_Caption, true);
            bttNext_Visible = 0;
            AssignProp("", false, bttNext_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttNext_Visible), 5, 0), true);
            bttChoosecoins_Visible = 0;
            AssignProp("", false, bttChoosecoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttChoosecoins_Visible), 5, 0), true);
            lblTbcoinselection_Visible = 0;
            AssignProp("", false, lblTbcoinselection_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbcoinselection_Visible), 5, 0), true);
         }
         /* Execute user subroutine: 'SHOW COIN SELECTION' */
         S112 ();
         if (returnInSub) return;
      }

      protected void E123S2( )
      {
         /* 'Open room' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'CHECK PAYMENT' */
         S122 ();
         if (returnInSub) return;
         if ( AV7checked )
         {
            new GeneXus.Programs.wallet.registered.startmusig2room(context ).execute(  AV15groupId,  AV23sendAllCoins,  AV24sendCoins,  AV25sendTo,  AV8description, out  AV21roomId, out  AV11error) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Signing room opened",(string)"The members see it in the Signing Rooms tab of the group for 15 minutes"}, true);
               context.setWebReturnParms(new Object[] {});
               context.setWebReturnParmsMetadata(new Object[] {});
               context.wjLocDisableFrm = 1;
               context.nUserReturn = 1;
               returnInSub = true;
               if (true) return;
            }
            else
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The signing room could not be opened",(string)AV11error}, true);
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E133S2( )
      {
         /* 'Next' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'CHECK PAYMENT' */
         S122 ();
         if (returnInSub) return;
         if ( AV7checked )
         {
            chkavSendallcoins.Enabled = 0;
            AssignProp("", false, chkavSendallcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSendallcoins.Enabled), 5, 0), true);
            edtavSendcoins_Enabled = 0;
            AssignProp("", false, edtavSendcoins_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendcoins_Enabled), 5, 0), true);
            edtavSendto_Enabled = 0;
            AssignProp("", false, edtavSendto_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSendto_Enabled), 5, 0), true);
            edtavDescription_Enabled = 0;
            AssignProp("", false, edtavDescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDescription_Enabled), 5, 0), true);
            bttChoosecoins_Visible = 0;
            AssignProp("", false, bttChoosecoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttChoosecoins_Visible), 5, 0), true);
            new GeneXus.Programs.wallet.registered.estimatemusig2vsize(context ).execute(  AV23sendAllCoins,  AV24sendCoins,  AV25sendTo,  AV8description, out  AV30virtualSize, out  AV11error) ;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem estimating the size of the transaction: ",(string)AV11error}, true);
            }
            else
            {
               GXt_char2 = AV11error;
               GXt_int3 = (short)(AV9economicalBlocks);
               new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV30virtualSize,  60,  "economical", out  AV10economicalFee, out  GXt_int3, out  GXt_char2) ;
               AV9economicalBlocks = GXt_int3;
               AV11error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  GXt_char2 = AV11error;
                  GXt_int3 = (short)(AV26standarBlocks);
                  new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV30virtualSize,  6,  "conservative", out  AV27standardFee, out  GXt_int3, out  GXt_char2) ;
                  AV26standarBlocks = GXt_int3;
                  AssignAttri("", false, "AV27standardFee", StringUtil.LTrimStr( AV27standardFee, 16, 8));
                  AssignAttri("", false, "AV26standarBlocks", StringUtil.LTrimStr( (decimal)(AV26standarBlocks), 10, 0));
                  AV11error = GXt_char2;
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  GXt_char2 = AV11error;
                  GXt_int3 = (short)(AV13fastestBlocks);
                  new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV30virtualSize,  1,  "conservative", out  AV14fastestFee, out  GXt_int3, out  GXt_char2) ;
                  AV13fastestBlocks = GXt_int3;
                  AssignAttri("", false, "AV14fastestFee", StringUtil.LTrimStr( AV14fastestFee, 16, 8));
                  AssignAttri("", false, "AV13fastestBlocks", StringUtil.LTrimStr( (decimal)(AV13fastestBlocks), 10, 0));
                  AV11error = GXt_char2;
               }
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"There was a problem calculating the fee: ",(string)AV11error}, true);
               }
               else
               {
                  cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(0), 16, 8)), "Select Estimated Transaction Fee", 0);
                  cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV10economicalFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV10economicalFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV9economicalBlocks), 10, 0)+" Blocks", 0);
                  cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV27standardFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV27standardFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV26standarBlocks), 10, 0)+" Blocks", 0);
                  cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV14fastestFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV14fastestFee, 16, 8))+" in about "+StringUtil.Str( (decimal)(AV13fastestBlocks), 10, 0)+" Blocks", 0);
                  cmbavUserfee.Visible = 1;
                  AssignProp("", false, cmbavUserfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUserfee.Visible), 5, 0), true);
                  chkavActivatemanaulfee.Visible = 1;
                  AssignProp("", false, chkavActivatemanaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavActivatemanaulfee.Visible), 5, 0), true);
                  edtavManaulfee_Visible = 1;
                  AssignProp("", false, edtavManaulfee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Visible), 5, 0), true);
                  edtavManaulfee_Enabled = 0;
                  AssignProp("", false, edtavManaulfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavManaulfee_Enabled), 5, 0), true);
                  bttNext_Visible = 0;
                  AssignProp("", false, bttNext_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttNext_Visible), 5, 0), true);
                  bttSendcoins_Visible = 1;
                  AssignProp("", false, bttSendcoins_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendcoins_Visible), 5, 0), true);
               }
            }
         }
         /*  Sending Event outputs  */
         cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV29userFee, 16, 8));
         AssignProp("", false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
      }

      protected void E143S2( )
      {
         /* 'Cancel' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         new GeneXus.Programs.wallet.clearselectedcoins(context ).execute( ) ;
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void E153S2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV20popupType = "Wallet.ApproveSpending";
         if ( StringUtil.StringSearch( AV19PopupName, StringUtil.Lower( AV20popupType), 1) > 0 )
         {
            if ( ! new GeneXus.Programs.wallet.registered.hassigningkeytaproot(context).executeUdp( ) )
            {
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Not sent",(string)"The password was not approved"}, true);
            }
            else
            {
               new GeneXus.Programs.wallet.registered.ownerspendmusig2(context ).execute(  AV23sendAllCoins,  AV24sendCoins,  AV25sendTo,  AV8description,  AV16manaulFee, out  AV6broadcast, out  AV12errorTitle, out  AV11error) ;
               new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
               if ( AV6broadcast )
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Transaction broadcast",(string)"The transaction was signed and submitted to the network"}, true);
                  new GeneXus.Programs.wallet.clearselectedcoins(context ).execute( ) ;
                  context.setWebReturnParms(new Object[] {});
                  context.setWebReturnParmsMetadata(new Object[] {});
                  context.wjLocDisableFrm = 1;
                  context.nUserReturn = 1;
                  returnInSub = true;
                  if (true) return;
               }
               else
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)AV12errorTitle,(string)AV11error}, true);
               }
            }
         }
         else
         {
            AV20popupType = "Wallet.CoinControl";
            if ( StringUtil.StringSearch( AV19PopupName, StringUtil.Lower( AV20popupType), 1) > 0 )
            {
               /* Execute user subroutine: 'SHOW COIN SELECTION' */
               S112 ();
               if (returnInSub) return;
            }
         }
         /*  Sending Event outputs  */
      }

      protected void S112( )
      {
         /* 'SHOW COIN SELECTION' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.getspendablebalance(context ).execute( out  AV22selectedCount, out  AV28totalBalance) ;
         AssignAttri("", false, "AV22selectedCount", StringUtil.LTrimStr( (decimal)(AV22selectedCount), 10, 0));
         AssignAttri("", false, "AV28totalBalance", StringUtil.LTrimStr( AV28totalBalance, 16, 8));
         if ( AV22selectedCount == 0 )
         {
            lblTbcoinselection_Caption = "Coins: automatic selection";
            AssignProp("", false, lblTbcoinselection_Internalname, "Caption", lblTbcoinselection_Caption, true);
         }
         else
         {
            lblTbcoinselection_Caption = "Coins: "+StringUtil.Trim( StringUtil.Str( (decimal)(AV22selectedCount), 10, 0))+" selected by you (the balance shown is theirs)";
            AssignProp("", false, lblTbcoinselection_Internalname, "Caption", lblTbcoinselection_Caption, true);
         }
         if ( AV23sendAllCoins )
         {
            AV24sendCoins = AV28totalBalance;
            AssignAttri("", false, "AV24sendCoins", StringUtil.LTrimStr( AV24sendCoins, 16, 8));
         }
      }

      protected void S122( )
      {
         /* 'CHECK PAYMENT' Routine */
         returnInSub = false;
         AV7checked = false;
         AssignAttri("", false, "AV7checked", AV7checked);
         if ( ( AV28totalBalance <= Convert.ToDecimal( 0 )) )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"The group has no balance in your wallet yet",(string)"Press Rescan Balance in the Wallet Balance tab of the group first: the one who starts a payment chooses its coins"}, true);
         }
         else if ( ( AV24sendCoins >= AV28totalBalance ) && ! AV23sendAllCoins )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You don't have enough balance",(string)""}, true);
         }
         else if ( ( (Convert.ToDecimal(0)==AV24sendCoins) || ( AV24sendCoins <= Convert.ToDecimal( 0 )) ) && ! AV23sendAllCoins )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You have to select an amount to send",(string)""}, true);
         }
         else if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV8description))) )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"You have to enter a description",(string)"The description is mandatory on multisignature transactions"}, true);
         }
         else
         {
            GXt_char2 = AV11error;
            new GeneXus.Programs.nbitcoin.isaddressvalid(context ).execute(  AV25sendTo,  AV17networkType, out  GXt_char2) ;
            AV11error = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Please check the Send to address: ",(string)AV11error}, true);
            }
            else
            {
               AV7checked = true;
               AssignAttri("", false, "AV7checked", AV7checked);
            }
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E163S2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      protected void wb_table2_57_3S2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblActionstable_Internalname, tblActionstable_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 60,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttOpenroom_Internalname, "", "Open signing room", bttOpenroom_Jsonclick, 5, "Open signing room", "", StyleString, ClassString, bttOpenroom_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'OPEN ROOM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendMuSig2Payment.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 62,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttNext_Internalname, "", "Next", bttNext_Jsonclick, 5, "Next", "", StyleString, ClassString, bttNext_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'NEXT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendMuSig2Payment.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 64,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSendcoins_Internalname, "", "Send Coins", bttSendcoins_Jsonclick, 7, "Send Coins", "", StyleString, ClassString, bttSendcoins_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"e173s1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/SendMuSig2Payment.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 66,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel", bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SendMuSig2Payment.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table2_57_3S2e( true) ;
         }
         else
         {
            wb_table2_57_3S2e( false) ;
         }
      }

      protected void wb_table1_14_3S2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblCoinstable_Internalname, tblCoinstable_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttChoosecoins_Internalname, "", "Choose coins", bttChoosecoins_Jsonclick, 7, "Choose coins", "", StyleString, ClassString, bttChoosecoins_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"e183s1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/SendMuSig2Payment.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbcoinselection_Internalname, lblTbcoinselection_Caption, "", "", lblTbcoinselection_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblTbcoinselection_Visible, 1, 0, 0, "HLP_Wallet/registered/SendMuSig2Payment.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_14_3S2e( true) ;
         }
         else
         {
            wb_table1_14_3S2e( false) ;
         }
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
         PA3S2( ) ;
         WS3S2( ) ;
         WE3S2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2026103013274613", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/sendmusig2payment.js", "?2026103013274613", false, true, false);
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
         AV23sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV23sendAllCoins));
         AssignAttri("", false, "AV23sendAllCoins", AV23sendAllCoins);
         cmbavUserfee.Name = "vUSERFEE";
         cmbavUserfee.WebTags = "";
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV29userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV29userFee, 16, 8))), ".");
            AssignAttri("", false, "AV29userFee", StringUtil.LTrimStr( AV29userFee, 16, 8));
         }
         chkavActivatemanaulfee.Name = "vACTIVATEMANAULFEE";
         chkavActivatemanaulfee.WebTags = "";
         chkavActivatemanaulfee.Caption = "Manually select Fee";
         AssignProp("", false, chkavActivatemanaulfee_Internalname, "TitleCaption", chkavActivatemanaulfee.Caption, true);
         chkavActivatemanaulfee.CheckedValue = "false";
         AV5activateManaulFee = StringUtil.StrToBool( StringUtil.BoolToStr( AV5activateManaulFee));
         AssignAttri("", false, "AV5activateManaulFee", AV5activateManaulFee);
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         lblTbmode_Internalname = "TBMODE";
         edtavTotalbalance_Internalname = "vTOTALBALANCE";
         bttChoosecoins_Internalname = "CHOOSECOINS";
         lblTbcoinselection_Internalname = "TBCOINSELECTION";
         tblCoinstable_Internalname = "COINSTABLE";
         chkavSendallcoins_Internalname = "vSENDALLCOINS";
         edtavSendcoins_Internalname = "vSENDCOINS";
         edtavSendto_Internalname = "vSENDTO";
         edtavDescription_Internalname = "vDESCRIPTION";
         cmbavUserfee_Internalname = "vUSERFEE";
         chkavActivatemanaulfee_Internalname = "vACTIVATEMANAULFEE";
         edtavManaulfee_Internalname = "vMANAULFEE";
         bttOpenroom_Internalname = "OPENROOM";
         bttNext_Internalname = "NEXT";
         bttSendcoins_Internalname = "SENDCOINS";
         bttCancel_Internalname = "CANCEL";
         tblActionstable_Internalname = "ACTIONSTABLE";
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
         chkavActivatemanaulfee.Caption = "Manually select Fee";
         chkavSendallcoins.Caption = "Send total balance";
         lblTbcoinselection_Visible = 1;
         bttChoosecoins_Visible = 1;
         bttSendcoins_Visible = 1;
         bttNext_Visible = 1;
         bttOpenroom_Visible = 1;
         lblTbcoinselection_Caption = "Coins: automatic selection";
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
         lblTbmode_Caption = "TBMode";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Send Coins from a MuSig2 group wallet: the owner of a Delegated MultiSig 2 pays alone; a member opens a signing room";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV23sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV5activateManaulFee","fld":"vACTIVATEMANAULFEE","type":"boolean"},{"av":"AV15groupId","fld":"vGROUPID","hsh":true,"type":"guid"},{"av":"AV17networkType","fld":"vNETWORKTYPE","hsh":true,"type":"char"}]}""");
         setEventMetadata("'OPEN ROOM'","""{"handler":"E123S2","iparms":[{"av":"AV7checked","fld":"vCHECKED","type":"boolean"},{"av":"AV15groupId","fld":"vGROUPID","hsh":true,"type":"guid"},{"av":"AV23sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV24sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV25sendTo","fld":"vSENDTO","type":"char"},{"av":"AV8description","fld":"vDESCRIPTION","type":"svchar"},{"av":"AV28totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV17networkType","fld":"vNETWORKTYPE","hsh":true,"type":"char"}]""");
         setEventMetadata("'OPEN ROOM'",""","oparms":[{"av":"AV7checked","fld":"vCHECKED","type":"boolean"}]}""");
         setEventMetadata("'NEXT'","""{"handler":"E133S2","iparms":[{"av":"AV7checked","fld":"vCHECKED","type":"boolean"},{"av":"AV23sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV24sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV25sendTo","fld":"vSENDTO","type":"char"},{"av":"AV8description","fld":"vDESCRIPTION","type":"svchar"},{"av":"cmbavUserfee"},{"av":"AV29userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV27standardFee","fld":"vSTANDARDFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV26standarBlocks","fld":"vSTANDARBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV14fastestFee","fld":"vFASTESTFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV13fastestBlocks","fld":"vFASTESTBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV28totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV17networkType","fld":"vNETWORKTYPE","hsh":true,"type":"char"}]""");
         setEventMetadata("'NEXT'",""","oparms":[{"av":"chkavSendallcoins.Enabled","ctrl":"vSENDALLCOINS","prop":"Enabled"},{"av":"edtavSendcoins_Enabled","ctrl":"vSENDCOINS","prop":"Enabled"},{"av":"edtavSendto_Enabled","ctrl":"vSENDTO","prop":"Enabled"},{"av":"edtavDescription_Enabled","ctrl":"vDESCRIPTION","prop":"Enabled"},{"ctrl":"CHOOSECOINS","prop":"Visible"},{"av":"AV26standarBlocks","fld":"vSTANDARBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV27standardFee","fld":"vSTANDARDFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV13fastestBlocks","fld":"vFASTESTBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV14fastestFee","fld":"vFASTESTFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"cmbavUserfee"},{"av":"AV29userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"chkavActivatemanaulfee.Visible","ctrl":"vACTIVATEMANAULFEE","prop":"Visible"},{"av":"edtavManaulfee_Visible","ctrl":"vMANAULFEE","prop":"Visible"},{"av":"edtavManaulfee_Enabled","ctrl":"vMANAULFEE","prop":"Enabled"},{"ctrl":"NEXT","prop":"Visible"},{"ctrl":"SENDCOINS","prop":"Visible"},{"av":"AV7checked","fld":"vCHECKED","type":"boolean"}]}""");
         setEventMetadata("'SEND COINS'","""{"handler":"E173S1","iparms":[{"av":"AV16manaulFee","fld":"vMANAULFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV22selectedCount","fld":"vSELECTEDCOUNT","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV23sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV24sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV28totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","type":"decimal"}]}""");
         setEventMetadata("'CHOOSE COINS'","""{"handler":"E183S1","iparms":[]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E143S2","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E153S2","iparms":[{"av":"AV19PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV23sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV24sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV25sendTo","fld":"vSENDTO","type":"char"},{"av":"AV8description","fld":"vDESCRIPTION","type":"svchar"},{"av":"AV16manaulFee","fld":"vMANAULFEE","pic":"ZZZZZZ9.99999999","type":"decimal"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV28totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV22selectedCount","fld":"vSELECTEDCOUNT","pic":"ZZZZZZZZZ9","type":"int"},{"av":"lblTbcoinselection_Caption","ctrl":"TBCOINSELECTION","prop":"Caption"},{"av":"AV24sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"}]}""");
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
         AV15groupId = Guid.Empty;
         AV17networkType = "";
         GXKey = "";
         AV19PopupName = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         lblTbmode_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV25sendTo = "";
         AV8description = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV21roomId = Guid.Empty;
         AV11error = "";
         AV20popupType = "";
         AV12errorTitle = "";
         GXt_char2 = "";
         sStyleString = "";
         bttOpenroom_Jsonclick = "";
         bttNext_Jsonclick = "";
         bttSendcoins_Jsonclick = "";
         bttCancel_Jsonclick = "";
         bttChoosecoins_Jsonclick = "";
         lblTbcoinselection_Jsonclick = "";
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
      private short nDonePA ;
      private short gxcookieaux ;
      private short GXt_int3 ;
      private short nGXWrapped ;
      private int edtavTotalbalance_Enabled ;
      private int edtavSendcoins_Enabled ;
      private int edtavSendto_Enabled ;
      private int edtavDescription_Enabled ;
      private int edtavManaulfee_Visible ;
      private int edtavManaulfee_Enabled ;
      private int bttSendcoins_Visible ;
      private int bttOpenroom_Visible ;
      private int bttNext_Visible ;
      private int bttChoosecoins_Visible ;
      private int lblTbcoinselection_Visible ;
      private int idxLst ;
      private long AV26standarBlocks ;
      private long AV13fastestBlocks ;
      private long AV22selectedCount ;
      private long AV30virtualSize ;
      private long AV9economicalBlocks ;
      private decimal AV27standardFee ;
      private decimal AV14fastestFee ;
      private decimal AV28totalBalance ;
      private decimal AV24sendCoins ;
      private decimal AV29userFee ;
      private decimal AV16manaulFee ;
      private decimal GXt_decimal1 ;
      private decimal AV10economicalFee ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string AV17networkType ;
      private string GXKey ;
      private string AV19PopupName ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string lblTbmode_Internalname ;
      private string lblTbmode_Caption ;
      private string lblTbmode_Jsonclick ;
      private string edtavTotalbalance_Internalname ;
      private string TempTags ;
      private string edtavTotalbalance_Jsonclick ;
      private string chkavSendallcoins_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string edtavSendcoins_Internalname ;
      private string edtavSendcoins_Jsonclick ;
      private string edtavSendto_Internalname ;
      private string AV25sendTo ;
      private string edtavDescription_Internalname ;
      private string cmbavUserfee_Internalname ;
      private string cmbavUserfee_Jsonclick ;
      private string chkavActivatemanaulfee_Internalname ;
      private string edtavManaulfee_Internalname ;
      private string edtavManaulfee_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string bttSendcoins_Internalname ;
      private string bttOpenroom_Internalname ;
      private string bttNext_Internalname ;
      private string bttChoosecoins_Internalname ;
      private string lblTbcoinselection_Internalname ;
      private string AV11error ;
      private string AV20popupType ;
      private string AV12errorTitle ;
      private string lblTbcoinselection_Caption ;
      private string GXt_char2 ;
      private string sStyleString ;
      private string tblActionstable_Internalname ;
      private string bttOpenroom_Jsonclick ;
      private string bttNext_Jsonclick ;
      private string bttSendcoins_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string tblCoinstable_Internalname ;
      private string bttChoosecoins_Jsonclick ;
      private string lblTbcoinselection_Jsonclick ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV7checked ;
      private bool wbLoad ;
      private bool AV23sendAllCoins ;
      private bool AV5activateManaulFee ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV18ownerPaysAlone ;
      private bool AV6broadcast ;
      private string AV8description ;
      private Guid AV15groupId ;
      private Guid AV21roomId ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavSendallcoins ;
      private GXCombobox cmbavUserfee ;
      private GXCheckbox chkavActivatemanaulfee ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
