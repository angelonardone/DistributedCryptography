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
   public class resotregroupwallet : GXDataArea
   {
      public resotregroupwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public resotregroupwallet( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId )
      {
         this.AV30groupId = aP0_groupId;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         radavKeystorage = new GXRadio();
         cmbavRubydevice = new GXCombobox();
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
               AV30groupId = StringUtil.StrToGuid( gxfirstwebparm);
               AssignAttri("", false, "AV30groupId", AV30groupId.ToString());
               GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV30groupId, context));
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
         PA1M2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START1M2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1450520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1450520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1450520), false, true, false);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.resotregroupwallet", new object[] {UrlEncode(AV30groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV30groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV30groupId, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vGROUPID", AV30groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV30groupId, context));
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV6error));
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
            WE1M2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT1M2( ) ;
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
         return formatLink("wallet.registered.resotregroupwallet", new object[] {UrlEncode(AV30groupId.ToString())}, new string[] {"groupId"})  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.ResotreGroupWallet" ;
      }

      public override string GetPgmdesc( )
      {
         return "Resotre Group Wallet" ;
      }

      protected void WB1M0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavWalletname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWalletname_Internalname, "Wallet Name", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavWalletname_Internalname, StringUtil.RTrim( AV17walletName), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", 0, 1, edtavWalletname_Enabled, 0, 80, "chr", 3, "row", 0, StyleString, ClassString, "", "", "200", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+radavKeystorage_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, "", "Where the keys of the new wallet live", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Radio button */
            ClassString = "Attribute";
            StyleString = "";
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'',0)\"";
            GxWebStd.gx_radio_ctrl( context, radavKeystorage, radavKeystorage_Internalname, StringUtil.RTrim( AV33keyStorage), "", 1, 1, 1, 1, StyleString, ClassString, "", "", 0, radavKeystorage_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", TempTags+" onclick="+"\""+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,13);\"", "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavRubydevice.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavRubydevice_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavRubydevice_Internalname, "Ruby device", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavRubydevice, cmbavRubydevice_Internalname, StringUtil.RTrim( AV34rubyDevice), 1, cmbavRubydevice_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "svchar", "", cmbavRubydevice.Visible, cmbavRubydevice.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"", "", true, 0, "HLP_Wallet/registered/ResotreGroupWallet.htm");
            cmbavRubydevice.CurrentValue = StringUtil.RTrim( AV34rubyDevice);
            AssignProp("", false, cmbavRubydevice_Internalname, "Values", (string)(cmbavRubydevice.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttFindruby_Internalname, "", "Find Ruby", bttFindruby_Jsonclick, 5, "Find Ruby", "", StyleString, ClassString, bttFindruby_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'FIND RUBY\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblRubyhelp_Internalname, "When you press Restore Wallet, the recovered wallet goes to Ruby. Use a new Ruby or one reset: a Ruby holds one wallet. Ruby shows the fingerprint of the recovered wallet: accept it only if it is the same as the one below, then choose its PIN on Ruby. The password protects this wallet file.", "", "", lblRubyhelp_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblRubyhelp_Visible, 1, 0, 0, "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblRubyfingerprint_Internalname, lblRubyfingerprint_Caption, "", "", lblRubyfingerprint_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblRubyfingerprint_Visible, 1, 0, 0, "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavNewpass_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavNewpass_Internalname, "Password", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNewpass_Internalname, StringUtil.RTrim( AV13newPass), StringUtil.RTrim( context.localUtil.Format( AV13newPass, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,31);\""+" "+"data-gx-password-reveal"+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNewpass_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNewpass_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavNewpassconfirm_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavNewpassconfirm_Internalname, "Confirm Password", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNewpassconfirm_Internalname, StringUtil.RTrim( AV14newPassConfirm), StringUtil.RTrim( context.localUtil.Format( AV14newPassConfirm, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,36);\""+" "+"data-gx-password-reveal"+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNewpassconfirm_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNewpassconfirm_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRestorewallet_Internalname, "", "Restore Wallet", bttRestorewallet_Jsonclick, 5, "Restore Wallet", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'RESTORE WALLET\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 41,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel and Close", bttCancel_Jsonclick, 5, "Cancel and Close", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL AND CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/ResotreGroupWallet.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START1M2( )
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
         Form.Meta.addItem("description", "Resotre Group Wallet", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP1M0( ) ;
      }

      protected void WS1M2( )
      {
         START1M2( ) ;
         EVT1M2( ) ;
      }

      protected void EVT1M2( )
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
                              E111M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VKEYSTORAGE.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E121M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'FIND RUBY'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Find Ruby' */
                              E131M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'RESTORE WALLET'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Restore Wallet' */
                              E141M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL AND CLOSE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel and Close' */
                              E151M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E161M2 ();
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

      protected void WE1M2( )
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

      protected void PA1M2( )
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
               GX_FocusControl = edtavWalletname_Internalname;
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
         AV33keyStorage = StringUtil.RTrim( AV33keyStorage);
         AssignAttri("", false, "AV33keyStorage", AV33keyStorage);
         if ( cmbavRubydevice.ItemCount > 0 )
         {
            AV34rubyDevice = cmbavRubydevice.getValidValue(AV34rubyDevice);
            AssignAttri("", false, "AV34rubyDevice", AV34rubyDevice);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavRubydevice.CurrentValue = StringUtil.RTrim( AV34rubyDevice);
            AssignProp("", false, cmbavRubydevice_Internalname, "Values", cmbavRubydevice.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF1M2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF1M2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E161M2 ();
            WB1M0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes1M2( )
      {
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1M0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E111M2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            AV17walletName = cgiGet( edtavWalletname_Internalname);
            AssignAttri("", false, "AV17walletName", AV17walletName);
            AV33keyStorage = cgiGet( radavKeystorage_Internalname);
            AssignAttri("", false, "AV33keyStorage", AV33keyStorage);
            cmbavRubydevice.CurrentValue = cgiGet( cmbavRubydevice_Internalname);
            AV34rubyDevice = cgiGet( cmbavRubydevice_Internalname);
            AssignAttri("", false, "AV34rubyDevice", AV34rubyDevice);
            AV13newPass = cgiGet( edtavNewpass_Internalname);
            AssignAttri("", false, "AV13newPass", AV13newPass);
            AV14newPassConfirm = cgiGet( edtavNewpassconfirm_Internalname);
            AssignAttri("", false, "AV14newPassConfirm", AV14newPassConfirm);
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
         E111M2 ();
         if (returnInSub) return;
      }

      protected void E111M2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV33keyStorage = "Software";
         AssignAttri("", false, "AV33keyStorage", AV33keyStorage);
         /* Execute user subroutine: 'SHOW KEY STORAGE' */
         S112 ();
         if (returnInSub) return;
      }

      protected void E121M2( )
      {
         /* Keystorage_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'SHOW KEY STORAGE' */
         S112 ();
         if (returnInSub) return;
         if ( StringUtil.StrCmp(AV33keyStorage, "Ruby") == 0 )
         {
            /* Execute user subroutine: 'FIND RUBY' */
            S122 ();
            if (returnInSub) return;
            new GeneXus.Programs.wallet.registered.getgroupbackupfingerprint(context ).execute(  AV30groupId, out  AV31fingerprint, out  AV6error) ;
            AssignAttri("", false, "AV6error", AV6error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6error)) )
            {
               lblRubyfingerprint_Caption = "Ruby will show the fingerprint "+StringUtil.Trim( AV31fingerprint)+". Accept it on Ruby only if it is the same.";
               AssignProp("", false, lblRubyfingerprint_Internalname, "Caption", lblRubyfingerprint_Caption, true);
            }
            else
            {
               lblRubyfingerprint_Caption = "";
               AssignProp("", false, lblRubyfingerprint_Internalname, "Caption", lblRubyfingerprint_Caption, true);
               GX_msglist.addItem(AV6error);
            }
         }
         /*  Sending Event outputs  */
         cmbavRubydevice.CurrentValue = StringUtil.RTrim( AV34rubyDevice);
         AssignProp("", false, cmbavRubydevice_Internalname, "Values", cmbavRubydevice.ToJavascriptSource(), true);
      }

      protected void E131M2( )
      {
         /* 'Find Ruby' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'FIND RUBY' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         cmbavRubydevice.CurrentValue = StringUtil.RTrim( AV34rubyDevice);
         AssignProp("", false, cmbavRubydevice_Internalname, "Values", cmbavRubydevice.ToJavascriptSource(), true);
      }

      protected void S112( )
      {
         /* 'SHOW KEY STORAGE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV33keyStorage, "Ruby") == 0 )
         {
            cmbavRubydevice.Visible = 1;
            AssignProp("", false, cmbavRubydevice_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavRubydevice.Visible), 5, 0), true);
            bttFindruby_Visible = 1;
            AssignProp("", false, bttFindruby_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttFindruby_Visible), 5, 0), true);
            lblRubyhelp_Visible = 1;
            AssignProp("", false, lblRubyhelp_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblRubyhelp_Visible), 5, 0), true);
            lblRubyfingerprint_Visible = 1;
            AssignProp("", false, lblRubyfingerprint_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblRubyfingerprint_Visible), 5, 0), true);
         }
         else
         {
            cmbavRubydevice.Visible = 0;
            AssignProp("", false, cmbavRubydevice_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavRubydevice.Visible), 5, 0), true);
            bttFindruby_Visible = 0;
            AssignProp("", false, bttFindruby_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttFindruby_Visible), 5, 0), true);
            lblRubyhelp_Visible = 0;
            AssignProp("", false, lblRubyhelp_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblRubyhelp_Visible), 5, 0), true);
            lblRubyfingerprint_Visible = 0;
            AssignProp("", false, lblRubyfingerprint_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblRubyfingerprint_Visible), 5, 0), true);
         }
      }

      protected void S122( )
      {
         /* 'FIND RUBY' Routine */
         returnInSub = false;
         cmbavRubydevice.removeAllItems();
         AV36rubyResult.FromJSonString(AV35rubyProcessor.finddevices(), null);
         AV32i = 1;
         while ( AV32i <= AV36rubyResult.gxTpr_Devices.Count )
         {
            cmbavRubydevice.addItem(StringUtil.Trim( ((GeneXus.Programs.wallet.SdtRubyResult_devicesItem)AV36rubyResult.gxTpr_Devices.Item(AV32i)).gxTpr_Device), "Ruby on "+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtRubyResult_devicesItem)AV36rubyResult.gxTpr_Devices.Item(AV32i)).gxTpr_Device)+" ("+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtRubyResult_devicesItem)AV36rubyResult.gxTpr_Devices.Item(AV32i)).gxTpr_State)+")", 0);
            AV32i = (short)(AV32i+1);
         }
         cmbavRubydevice.addItem("tcp:127.0.0.1:30121", "Ruby emulator (tcp:127.0.0.1:30121)", 0);
         if ( AV36rubyResult.gxTpr_Devices.Count == 0 )
         {
            AV34rubyDevice = "tcp:127.0.0.1:30121";
            AssignAttri("", false, "AV34rubyDevice", AV34rubyDevice);
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"No Ruby found on USB",(string)"Connect a new Ruby (or one reset) and press Find Ruby, or use the emulator"}, true);
         }
         else
         {
            AV34rubyDevice = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtRubyResult_devicesItem)AV36rubyResult.gxTpr_Devices.Item(1)).gxTpr_Device);
            AssignAttri("", false, "AV34rubyDevice", AV34rubyDevice);
         }
      }

      protected void E141M2( )
      {
         /* 'Restore Wallet' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV17walletName)) )
         {
            GX_msglist.addItem("The Wallet Name is mandatory");
         }
         else
         {
            if ( GxRegex.IsMatch(AV17walletName,"[~#&@*\\\\[\\]{}/:?\\“+%]+") )
            {
               GX_msglist.addItem("The following characters are not allowed on the name: ~#&@*\\\\[\\]{}/:?\\“+%");
            }
            else
            {
               if ( GxRegex.IsMatch(AV17walletName,"^[ ]") )
               {
                  GX_msglist.addItem("Spaces at the beginning of the name are not allowed");
               }
               else
               {
                  if ( ! ( StringUtil.StrCmp(AV13newPass, AV14newPassConfirm) == 0 ) )
                  {
                     GX_msglist.addItem("The password and password confirm are different, please make sure they are the same");
                  }
                  else
                  {
                     if ( StringUtil.StrCmp(AV33keyStorage, "Ruby") == 0 )
                     {
                        /* Execute user subroutine: 'RESTORE ON RUBY' */
                        S132 ();
                        if (returnInSub) return;
                     }
                     else
                     {
                        GXt_char1 = AV6error;
                        new GeneXus.Programs.wallet.registered.createwalletfromgroupbackup(context ).execute(  AV30groupId,  AV17walletName,  AV13newPass, out  GXt_char1) ;
                        AV6error = GXt_char1;
                        AssignAttri("", false, "AV6error", AV6error);
                     }
                     AV13newPass = "";
                     AssignAttri("", false, "AV13newPass", AV13newPass);
                     AV14newPassConfirm = "";
                     AssignAttri("", false, "AV14newPassConfirm", AV14newPassConfirm);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6error)) )
                     {
                        AV26walletRestored = true;
                        AV24webSession.Set("Group_Restore_Confirmed", StringUtil.BoolToStr( AV26walletRestored));
                        context.setWebReturnParms(new Object[] {});
                        context.setWebReturnParmsMetadata(new Object[] {});
                        context.wjLocDisableFrm = 1;
                        context.nUserReturn = 1;
                        returnInSub = true;
                        if (true) return;
                     }
                     else
                     {
                        GX_msglist.addItem(AV6error);
                     }
                  }
               }
            }
         }
         AV13newPass = "";
         AssignAttri("", false, "AV13newPass", AV13newPass);
         AV14newPassConfirm = "";
         AssignAttri("", false, "AV14newPassConfirm", AV14newPassConfirm);
         /*  Sending Event outputs  */
      }

      protected void S132( )
      {
         /* 'RESTORE ON RUBY' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13newPass)) )
         {
            AV6error = "The password is mandatory for a Ruby wallet: it protects the wallet file and the oracle of your PIN";
            AssignAttri("", false, "AV6error", AV6error);
         }
         else
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV34rubyDevice)) )
            {
               AV6error = "Please choose the Ruby device (press Find Ruby)";
               AssignAttri("", false, "AV6error", AV6error);
            }
            else
            {
               GXt_char1 = AV6error;
               new GeneXus.Programs.wallet.registered.createrubywalletfromgroupbackup(context ).execute(  AV30groupId,  AV17walletName,  AV34rubyDevice,  AV13newPass, out  GXt_char1) ;
               AV6error = GXt_char1;
               AssignAttri("", false, "AV6error", AV6error);
            }
         }
      }

      protected void E151M2( )
      {
         /* 'Cancel and Close' Routine */
         returnInSub = false;
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void nextLoad( )
      {
      }

      protected void E161M2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV30groupId = (Guid)getParm(obj,0);
         AssignAttri("", false, "AV30groupId", AV30groupId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vGROUPID", GetSecureSignedToken( "", AV30groupId, context));
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
         PA1M2( ) ;
         WS1M2( ) ;
         WE1M2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202611238561989", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/resotregroupwallet.js", "?202611238561989", false, true, false);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         radavKeystorage.Name = "vKEYSTORAGE";
         radavKeystorage.WebTags = "";
         radavKeystorage.addItem("Software", "Software (the password protects the keys)", 0);
         radavKeystorage.addItem("Ruby", "Ruby hardware key", 0);
         cmbavRubydevice.Name = "vRUBYDEVICE";
         cmbavRubydevice.WebTags = "";
         if ( cmbavRubydevice.ItemCount > 0 )
         {
            AV34rubyDevice = cmbavRubydevice.getValidValue(AV34rubyDevice);
            AssignAttri("", false, "AV34rubyDevice", AV34rubyDevice);
         }
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavWalletname_Internalname = "vWALLETNAME";
         radavKeystorage_Internalname = "vKEYSTORAGE";
         cmbavRubydevice_Internalname = "vRUBYDEVICE";
         bttFindruby_Internalname = "FINDRUBY";
         lblRubyhelp_Internalname = "RUBYHELP";
         lblRubyfingerprint_Internalname = "RUBYFINGERPRINT";
         edtavNewpass_Internalname = "vNEWPASS";
         edtavNewpassconfirm_Internalname = "vNEWPASSCONFIRM";
         bttRestorewallet_Internalname = "RESTOREWALLET";
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
         edtavNewpassconfirm_Jsonclick = "";
         edtavNewpassconfirm_Enabled = 1;
         edtavNewpass_Jsonclick = "";
         edtavNewpass_Enabled = 1;
         lblRubyfingerprint_Caption = " ";
         lblRubyfingerprint_Visible = 1;
         lblRubyhelp_Visible = 1;
         bttFindruby_Visible = 1;
         cmbavRubydevice_Jsonclick = "";
         cmbavRubydevice.Enabled = 1;
         cmbavRubydevice.Visible = 1;
         radavKeystorage_Jsonclick = "";
         edtavWalletname_Enabled = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Resotre Group Wallet";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"radavKeystorage"},{"av":"AV33keyStorage","fld":"vKEYSTORAGE","type":"char"},{"av":"AV30groupId","fld":"vGROUPID","hsh":true,"type":"guid"}]}""");
         setEventMetadata("VKEYSTORAGE.CONTROLVALUECHANGED","""{"handler":"E121M2","iparms":[{"av":"radavKeystorage"},{"av":"AV33keyStorage","fld":"vKEYSTORAGE","type":"char"},{"av":"AV30groupId","fld":"vGROUPID","hsh":true,"type":"guid"}]""");
         setEventMetadata("VKEYSTORAGE.CONTROLVALUECHANGED",""","oparms":[{"av":"AV6error","fld":"vERROR","type":"char"},{"av":"lblRubyfingerprint_Caption","ctrl":"RUBYFINGERPRINT","prop":"Caption"},{"av":"cmbavRubydevice"},{"ctrl":"FINDRUBY","prop":"Visible"},{"av":"lblRubyhelp_Visible","ctrl":"RUBYHELP","prop":"Visible"},{"av":"lblRubyfingerprint_Visible","ctrl":"RUBYFINGERPRINT","prop":"Visible"},{"av":"AV34rubyDevice","fld":"vRUBYDEVICE","type":"svchar"}]}""");
         setEventMetadata("'FIND RUBY'","""{"handler":"E131M2","iparms":[]""");
         setEventMetadata("'FIND RUBY'",""","oparms":[{"av":"cmbavRubydevice"},{"av":"AV34rubyDevice","fld":"vRUBYDEVICE","type":"svchar"}]}""");
         setEventMetadata("'RESTORE WALLET'","""{"handler":"E141M2","iparms":[{"av":"AV17walletName","fld":"vWALLETNAME","type":"char"},{"av":"AV13newPass","fld":"vNEWPASS","type":"char"},{"av":"AV14newPassConfirm","fld":"vNEWPASSCONFIRM","type":"char"},{"av":"radavKeystorage"},{"av":"AV33keyStorage","fld":"vKEYSTORAGE","type":"char"},{"av":"AV30groupId","fld":"vGROUPID","hsh":true,"type":"guid"},{"av":"AV6error","fld":"vERROR","type":"char"},{"av":"cmbavRubydevice"},{"av":"AV34rubyDevice","fld":"vRUBYDEVICE","type":"svchar"}]""");
         setEventMetadata("'RESTORE WALLET'",""","oparms":[{"av":"AV6error","fld":"vERROR","type":"char"},{"av":"AV13newPass","fld":"vNEWPASS","type":"char"},{"av":"AV14newPassConfirm","fld":"vNEWPASSCONFIRM","type":"char"}]}""");
         setEventMetadata("'CANCEL AND CLOSE'","""{"handler":"E151M2","iparms":[]}""");
         setEventMetadata("VALIDV_KEYSTORAGE","""{"handler":"Validv_Keystorage","iparms":[]}""");
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
         wcpOAV30groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV6error = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV17walletName = "";
         AV33keyStorage = "";
         AV34rubyDevice = "";
         bttFindruby_Jsonclick = "";
         lblRubyhelp_Jsonclick = "";
         lblRubyfingerprint_Jsonclick = "";
         AV13newPass = "";
         AV14newPassConfirm = "";
         bttRestorewallet_Jsonclick = "";
         bttCancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV31fingerprint = "";
         AV36rubyResult = new GeneXus.Programs.wallet.SdtRubyResult(context);
         AV35rubyProcessor = new GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor(context);
         AV24webSession = context.GetSession();
         GXt_char1 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short AV32i ;
      private short nGXWrapped ;
      private int edtavWalletname_Enabled ;
      private int bttFindruby_Visible ;
      private int lblRubyhelp_Visible ;
      private int lblRubyfingerprint_Visible ;
      private int edtavNewpass_Enabled ;
      private int edtavNewpassconfirm_Enabled ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV6error ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string edtavWalletname_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string AV17walletName ;
      private string radavKeystorage_Internalname ;
      private string AV33keyStorage ;
      private string radavKeystorage_Jsonclick ;
      private string cmbavRubydevice_Internalname ;
      private string cmbavRubydevice_Jsonclick ;
      private string bttFindruby_Internalname ;
      private string bttFindruby_Jsonclick ;
      private string lblRubyhelp_Internalname ;
      private string lblRubyhelp_Jsonclick ;
      private string lblRubyfingerprint_Internalname ;
      private string lblRubyfingerprint_Caption ;
      private string lblRubyfingerprint_Jsonclick ;
      private string edtavNewpass_Internalname ;
      private string AV13newPass ;
      private string edtavNewpass_Jsonclick ;
      private string edtavNewpassconfirm_Internalname ;
      private string AV14newPassConfirm ;
      private string edtavNewpassconfirm_Jsonclick ;
      private string bttRestorewallet_Internalname ;
      private string bttRestorewallet_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV31fingerprint ;
      private string GXt_char1 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV26walletRestored ;
      private string AV34rubyDevice ;
      private Guid AV30groupId ;
      private Guid wcpOAV30groupId ;
      private IGxSession AV24webSession ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXRadio radavKeystorage ;
      private GXCombobox cmbavRubydevice ;
      private GeneXus.Programs.wallet.SdtRubyResult AV36rubyResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtRubyProcessor AV35rubyProcessor ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
