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
   public class createwallet : GXDataArea
   {
      public createwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createwallet( IGxContext context )
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
         cmbavNetworktype = new GXCombobox();
         chkavAuthenticator = new GXCheckbox();
         chkavShowauthenticatorcode = new GXCheckbox();
         cmbavMnemoniclanguage = new GXCombobox();
         cmbavMnemonicnumberwords = new GXCombobox();
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
         PA0K2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START0K2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.createwallet") +"\">") ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTKEYCREATE", AV8extKeyCreate);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTKEYCREATE", AV8extKeyCreate);
         }
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV7error));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV19wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV19wallet);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTENDESECRETANDAUTHENTICATOR", AV29extendeSecretAndAuthenticator);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTENDESECRETANDAUTHENTICATOR", AV29extendeSecretAndAuthenticator);
         }
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
            WE0K2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT0K2( ) ;
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
         return formatLink("wallet.createwallet")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.CreateWallet" ;
      }

      public override string GetPgmdesc( )
      {
         return "Create Wallet" ;
      }

      protected void WB0K0( )
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
            GxWebStd.gx_html_textarea( context, edtavWalletname_Internalname, StringUtil.RTrim( AV20walletName), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", 0, 1, edtavWalletname_Enabled, 0, 80, "chr", 3, "row", 0, StyleString, ClassString, "", "", "200", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/CreateWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavNetworktype_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavNetworktype_Internalname, "Select Network Type", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavNetworktype, cmbavNetworktype_Internalname, StringUtil.RTrim( AV15networkType), 1, cmbavNetworktype_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavNetworktype.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"", "", true, 0, "HLP_Wallet/CreateWallet.htm");
            cmbavNetworktype.CurrentValue = StringUtil.RTrim( AV15networkType);
            AssignProp("", false, cmbavNetworktype_Internalname, "Values", (string)(cmbavNetworktype.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavNewpass_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavNewpass_Internalname, "New Password", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNewpass_Internalname, StringUtil.RTrim( AV16newPass), StringUtil.RTrim( context.localUtil.Format( AV16newPass, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\""+" "+"data-gx-password-reveal"+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNewpass_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNewpass_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/CreateWallet.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNewpassconfirm_Internalname, StringUtil.RTrim( AV17newPassConfirm), StringUtil.RTrim( context.localUtil.Format( AV17newPassConfirm, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,23);\""+" "+"data-gx-password-reveal"+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNewpassconfirm_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNewpassconfirm_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/CreateWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAuthenticator_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAuthenticator_Internalname, "Use 2FA (authenticator) for extra security", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAuthenticator_Internalname, StringUtil.BoolToStr( AV23authenticator), "", "Use 2FA (authenticator) for extra security", 1, chkavAuthenticator.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(28, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,28);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", imgavQrcode_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+imgavQrcode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, "", "Scan this code with your Authenticator's App", "col-xs-12 ImageLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Static Bitmap Variable */
            ClassString = "Image" + " " + ((StringUtil.StrCmp(imgavQrcode_gximage, "")==0) ? "" : "GX_Image_"+imgavQrcode_gximage+"_Class");
            StyleString = "";
            AV24qrcode_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV24qrcode))&&String.IsNullOrEmpty(StringUtil.RTrim( AV30Qrcode_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV24qrcode)));
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV24qrcode)) ? AV30Qrcode_GXI : context.PathToRelativeUrl( AV24qrcode));
            GxWebStd.gx_bitmap( context, imgavQrcode_Internalname, sImgUrl, "", "", "", context.GetTheme( ), imgavQrcode_Visible, 0, "", "", 0, -1, 0, "", 0, "", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", "", "", "", 1, AV24qrcode_IsBlob, false, context.GetImageSrcSet( sImgUrl), "none", "HLP_Wallet/CreateWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavShowauthenticatorcode.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavShowauthenticatorcode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavShowauthenticatorcode_Internalname, "Show Authenticator Code", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavShowauthenticatorcode_Internalname, StringUtil.BoolToStr( AV28showAuthenticatorCode), "", "Show Authenticator Code", chkavShowauthenticatorcode.Visible, chkavShowauthenticatorcode.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(38, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,38);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavBase32string_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavBase32string_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 43,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavBase32string_Internalname, StringUtil.RTrim( AV27base32String), StringUtil.RTrim( context.localUtil.Format( AV27base32String, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,43);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavBase32string_Jsonclick, 0, "Attribute", "", "", "", "", edtavBase32string_Visible, edtavBase32string_Enabled, 0, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/CreateWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavMnemoniclanguage_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavMnemoniclanguage_Internalname, "Select you Language", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 48,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavMnemoniclanguage, cmbavMnemoniclanguage_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV12mnemonicLanguage), 4, 0)), 1, cmbavMnemoniclanguage_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", 1, cmbavMnemoniclanguage.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,48);\"", "", true, 0, "HLP_Wallet/CreateWallet.htm");
            cmbavMnemoniclanguage.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV12mnemonicLanguage), 4, 0));
            AssignProp("", false, cmbavMnemoniclanguage_Internalname, "Values", (string)(cmbavMnemoniclanguage.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavMnemonicnumberwords_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavMnemonicnumberwords_Internalname, "Number of Mnemonic Words", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 53,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavMnemonicnumberwords, cmbavMnemonicnumberwords_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV13mnemonicNumberWords), 4, 0)), 1, cmbavMnemonicnumberwords_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", 1, cmbavMnemonicnumberwords.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,53);\"", "", true, 0, "HLP_Wallet/CreateWallet.htm");
            cmbavMnemonicnumberwords.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV13mnemonicNumberWords), 4, 0));
            AssignProp("", false, cmbavMnemonicnumberwords_Internalname, "Values", (string)(cmbavMnemonicnumberwords.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavMnemonictext_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMnemonictext_Internalname, "Mnemonic Text (seed)", "col-sm-3 AttSubTitleLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 58,'',false,'',0)\"";
            ClassString = "AttSubTitle";
            StyleString = "";
            ClassString = "AttSubTitle";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavMnemonictext_Internalname, AV14MnemonicText, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,58);\"", 0, 1, edtavMnemonictext_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "2097152", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/CreateWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTxt1_Internalname, lblTxt1_Caption, "", "", lblTxt1_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblTxt1_Visible, 1, 0, 1, "HLP_Wallet/CreateWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 64,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreatewallet_Internalname, "", "Create Wallet", bttCreatewallet_Jsonclick, 5, "Create Wallet", "", StyleString, ClassString, bttCreatewallet_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CREATE WALLET\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/CreateWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 66,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel and close", bttCancel_Jsonclick, 5, "Cancel and close", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL AND CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/CreateWallet.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START0K2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_14-187820", 0) ;
            }
         }
         Form.Meta.addItem("description", "Create Wallet", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP0K0( ) ;
      }

      protected void WS0K2( )
      {
         START0K2( ) ;
         EVT0K2( ) ;
      }

      protected void EVT0K2( )
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
                              E110K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VMNEMONICNUMBERWORDS.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E120K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CREATE WALLET'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Create Wallet' */
                              E130K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL AND CLOSE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel and close' */
                              E140K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VAUTHENTICATOR.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E150K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E160K2 ();
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

      protected void WE0K2( )
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

      protected void PA0K2( )
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
         if ( cmbavNetworktype.ItemCount > 0 )
         {
            AV15networkType = cmbavNetworktype.getValidValue(AV15networkType);
            AssignAttri("", false, "AV15networkType", AV15networkType);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavNetworktype.CurrentValue = StringUtil.RTrim( AV15networkType);
            AssignProp("", false, cmbavNetworktype_Internalname, "Values", cmbavNetworktype.ToJavascriptSource(), true);
         }
         AV23authenticator = StringUtil.StrToBool( StringUtil.BoolToStr( AV23authenticator));
         AssignAttri("", false, "AV23authenticator", AV23authenticator);
         AV28showAuthenticatorCode = StringUtil.StrToBool( StringUtil.BoolToStr( AV28showAuthenticatorCode));
         AssignAttri("", false, "AV28showAuthenticatorCode", AV28showAuthenticatorCode);
         if ( cmbavMnemoniclanguage.ItemCount > 0 )
         {
            AV12mnemonicLanguage = (short)(Math.Round(NumberUtil.Val( cmbavMnemoniclanguage.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV12mnemonicLanguage), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV12mnemonicLanguage", StringUtil.LTrimStr( (decimal)(AV12mnemonicLanguage), 4, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavMnemoniclanguage.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV12mnemonicLanguage), 4, 0));
            AssignProp("", false, cmbavMnemoniclanguage_Internalname, "Values", cmbavMnemoniclanguage.ToJavascriptSource(), true);
         }
         if ( cmbavMnemonicnumberwords.ItemCount > 0 )
         {
            AV13mnemonicNumberWords = (short)(Math.Round(NumberUtil.Val( cmbavMnemonicnumberwords.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV13mnemonicNumberWords), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV13mnemonicNumberWords", StringUtil.LTrimStr( (decimal)(AV13mnemonicNumberWords), 4, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavMnemonicnumberwords.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV13mnemonicNumberWords), 4, 0));
            AssignProp("", false, cmbavMnemonicnumberwords_Internalname, "Values", cmbavMnemonicnumberwords.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF0K2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavBase32string_Enabled = 0;
         AssignProp("", false, edtavBase32string_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavBase32string_Enabled), 5, 0), true);
         edtavMnemonictext_Enabled = 0;
         AssignProp("", false, edtavMnemonictext_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMnemonictext_Enabled), 5, 0), true);
      }

      protected void RF0K2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E160K2 ();
            WB0K0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes0K2( )
      {
      }

      protected void before_start_formulas( )
      {
         edtavBase32string_Enabled = 0;
         AssignProp("", false, edtavBase32string_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavBase32string_Enabled), 5, 0), true);
         edtavMnemonictext_Enabled = 0;
         AssignProp("", false, edtavMnemonictext_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMnemonictext_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0K0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E110K2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            AV20walletName = cgiGet( edtavWalletname_Internalname);
            AssignAttri("", false, "AV20walletName", AV20walletName);
            cmbavNetworktype.CurrentValue = cgiGet( cmbavNetworktype_Internalname);
            AV15networkType = cgiGet( cmbavNetworktype_Internalname);
            AssignAttri("", false, "AV15networkType", AV15networkType);
            AV16newPass = cgiGet( edtavNewpass_Internalname);
            AssignAttri("", false, "AV16newPass", AV16newPass);
            AV17newPassConfirm = cgiGet( edtavNewpassconfirm_Internalname);
            AssignAttri("", false, "AV17newPassConfirm", AV17newPassConfirm);
            AV23authenticator = StringUtil.StrToBool( cgiGet( chkavAuthenticator_Internalname));
            AssignAttri("", false, "AV23authenticator", AV23authenticator);
            AV24qrcode = cgiGet( imgavQrcode_Internalname);
            AV28showAuthenticatorCode = StringUtil.StrToBool( cgiGet( chkavShowauthenticatorcode_Internalname));
            AssignAttri("", false, "AV28showAuthenticatorCode", AV28showAuthenticatorCode);
            AV27base32String = cgiGet( edtavBase32string_Internalname);
            AssignAttri("", false, "AV27base32String", AV27base32String);
            cmbavMnemoniclanguage.CurrentValue = cgiGet( cmbavMnemoniclanguage_Internalname);
            AV12mnemonicLanguage = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavMnemoniclanguage_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV12mnemonicLanguage", StringUtil.LTrimStr( (decimal)(AV12mnemonicLanguage), 4, 0));
            cmbavMnemonicnumberwords.CurrentValue = cgiGet( cmbavMnemonicnumberwords_Internalname);
            AV13mnemonicNumberWords = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavMnemonicnumberwords_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV13mnemonicNumberWords", StringUtil.LTrimStr( (decimal)(AV13mnemonicNumberWords), 4, 0));
            AV14MnemonicText = cgiGet( edtavMnemonictext_Internalname);
            AssignAttri("", false, "AV14MnemonicText", AV14MnemonicText);
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
         E110K2 ();
         if (returnInSub) return;
      }

      protected void E110K2( )
      {
         /* Start Routine */
         returnInSub = false;
         lblTxt1_Visible = 0;
         AssignProp("", false, lblTxt1_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTxt1_Visible), 5, 0), true);
         imgavQrcode_Visible = 0;
         AssignProp("", false, imgavQrcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavQrcode_Visible), 5, 0), true);
         bttCreatewallet_Visible = 0;
         AssignProp("", false, bttCreatewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreatewallet_Visible), 5, 0), true);
         AV23authenticator = false;
         AssignAttri("", false, "AV23authenticator", AV23authenticator);
         chkavShowauthenticatorcode.Visible = 0;
         AssignProp("", false, chkavShowauthenticatorcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavShowauthenticatorcode.Visible), 5, 0), true);
         edtavBase32string_Visible = 0;
         AssignProp("", false, edtavBase32string_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavBase32string_Visible), 5, 0), true);
      }

      protected void E120K2( )
      {
         /* Mnemonicnumberwords_Controlvaluechanged Routine */
         returnInSub = false;
         AV8extKeyCreate.gxTpr_Networktype = AV15networkType;
         AV8extKeyCreate.gxTpr_Createextkeytype = 20;
         AV8extKeyCreate.gxTpr_Mnemonicnumberwords = AV13mnemonicNumberWords;
         AV8extKeyCreate.gxTpr_Mnemoniclanguage = AV12mnemonicLanguage;
         if ( StringUtil.StrCmp(AV15networkType, "MainNet") == 0 )
         {
            AV8extKeyCreate.gxTpr_Keypath = "m/86'/0'/0'";
         }
         else if ( StringUtil.StrCmp(AV15networkType, "TestNet") == 0 )
         {
            AV8extKeyCreate.gxTpr_Keypath = "m/86'/1'/0'";
         }
         else if ( StringUtil.StrCmp(AV15networkType, "RegTest") == 0 )
         {
            AV8extKeyCreate.gxTpr_Keypath = "m/86'/1'/0'";
         }
         else
         {
            AV7error = "Network Type not sopported";
            AssignAttri("", false, "AV7error", AV7error);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            GXt_char1 = AV7error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV8extKeyCreate,  AV16newPass, out  AV9extKeyInfo, out  GXt_char1) ;
            AV7error = GXt_char1;
            AssignAttri("", false, "AV7error", AV7error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
            {
               AV19wallet.gxTpr_Networktype = AV15networkType;
               AV19wallet.gxTpr_Walletname = AV20walletName;
               AV29extendeSecretAndAuthenticator.gxTpr_Networktype = AV15networkType;
               AV29extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey = StringUtil.Trim( AV9extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
               AV29extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = "";
               if ( AV23authenticator )
               {
                  AV29extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = StringUtil.Trim( AV27base32String);
                  AV19wallet.gxTpr_Useauthenticator = true;
               }
               GXt_char1 = AV7error;
               GXt_char2 = AV19wallet.gxTpr_Encryptedsecret;
               new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV16newPass,  AV29extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char2, ref  GXt_char1) ;
               AV19wallet.gxTpr_Encryptedsecret = GXt_char2;
               AV7error = GXt_char1;
               AssignAttri("", false, "AV7error", AV7error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
               {
                  AV19wallet.gxTpr_Wallettype = "BIP86";
                  AV19wallet.gxTpr_Walletname = AV20walletName;
                  AV14MnemonicText = AV9extKeyInfo.gxTpr_Mnemonic;
                  AssignAttri("", false, "AV14MnemonicText", AV14MnemonicText);
                  lblTxt1_Visible = 1;
                  AssignProp("", false, lblTxt1_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTxt1_Visible), 5, 0), true);
                  bttCreatewallet_Visible = 1;
                  AssignProp("", false, bttCreatewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreatewallet_Visible), 5, 0), true);
                  lblTxt1_Caption = "Please save these"+StringUtil.Str( (decimal)(AV13mnemonicNumberWords), 4, 0)+" words on paper (order is important). This seed will allow you to recover your wallet in case of computer failure.<br>"+"<b>WARNING:</b><br>"+"<ul type=\"circle\">"+"<li>Never disclose your seed.</li>"+"<li>Never type it on a website.</li>"+"<li>Do not store it electronically.</li>"+"<li>The password is part of your \"seed\" and you need it in order to recover the wallet.</li>"+"</ul>";
                  AssignProp("", false, lblTxt1_Internalname, "Caption", lblTxt1_Caption, true);
               }
               else
               {
                  GX_msglist.addItem("There was a problem encrypting the Extended Key: "+AV7error);
               }
            }
            else
            {
               GX_msglist.addItem(AV7error);
            }
         }
         else
         {
            GX_msglist.addItem(AV7error);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV8extKeyCreate", AV8extKeyCreate);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV19wallet", AV19wallet);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV29extendeSecretAndAuthenticator", AV29extendeSecretAndAuthenticator);
      }

      protected void E130K2( )
      {
         /* 'Create Wallet' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV20walletName)) )
         {
            GX_msglist.addItem("The Wallet Name is mandatory");
         }
         else
         {
            if ( GxRegex.IsMatch(AV20walletName,"[~#&@*\\\\[\\]{}/:?\\“+%]+") )
            {
               GX_msglist.addItem("The following characters are not allowed on the name: ~#&@*\\\\[\\]{}/:?\\“+%");
            }
            else
            {
               if ( GxRegex.IsMatch(AV20walletName,"^[ ]") )
               {
                  GX_msglist.addItem("Spaces at the beginning of the name are not allowed");
               }
               else
               {
                  if ( ! ( StringUtil.StrCmp(AV16newPass, AV17newPassConfirm) == 0 ) )
                  {
                     GX_msglist.addItem("The password and password confirm are different, please make sure they are the same");
                  }
                  else
                  {
                     if ( (0==AV12mnemonicLanguage) )
                     {
                        GX_msglist.addItem("Please select your Language");
                     }
                     else
                     {
                        if ( (0==AV13mnemonicNumberWords) )
                        {
                           GX_msglist.addItem("Please select number of Words for your Mnemonic");
                        }
                        else
                        {
                           new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV19wallet) ;
                           context.setWebReturnParms(new Object[] {});
                           context.setWebReturnParmsMetadata(new Object[] {});
                           context.wjLocDisableFrm = 1;
                           context.nUserReturn = 1;
                           returnInSub = true;
                           if (true) return;
                        }
                     }
                  }
               }
            }
         }
      }

      protected void E140K2( )
      {
         /* 'Cancel and close' Routine */
         returnInSub = false;
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void E150K2( )
      {
         /* Authenticator_Controlvaluechanged Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV20walletName)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV16newPass)) )
         {
            if ( AV23authenticator )
            {
               GXt_char2 = AV7error;
               new GeneXus.Programs.nbitcoin.createrandomkey(context ).execute( out  AV11keyInfo, out  GXt_char2) ;
               AV7error = GXt_char2;
               AssignAttri("", false, "AV7error", AV7error);
               GXt_char2 = AV7error;
               new GeneXus.Programs.nbitcoin.hextobase32(context ).execute(  StringUtil.Trim( AV11keyInfo.gxTpr_Privatekey), out  AV27base32String, out  GXt_char2) ;
               AssignAttri("", false, "AV27base32String", AV27base32String);
               AV7error = GXt_char2;
               AssignAttri("", false, "AV7error", AV7error);
               AV25SetupCode = AV26TwoFactorAuthenticator.generatesetupcode("Distributed Cryptography", AV20walletName, StringUtil.Trim( AV27base32String), true, 3);
               AV24qrcode = AV25SetupCode.gxTpr_Qrcodesetupimageurl;
               AssignProp("", false, imgavQrcode_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV24qrcode)) ? AV30Qrcode_GXI : context.convertURL( context.PathToRelativeUrl( AV24qrcode))), true);
               AssignProp("", false, imgavQrcode_Internalname, "SrcSet", context.GetImageSrcSet( AV24qrcode), true);
               AV30Qrcode_GXI = GXDbFile.PathToUrl( AV25SetupCode.gxTpr_Qrcodesetupimageurl, context);
               AssignProp("", false, imgavQrcode_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV24qrcode)) ? AV30Qrcode_GXI : context.convertURL( context.PathToRelativeUrl( AV24qrcode))), true);
               AssignProp("", false, imgavQrcode_Internalname, "SrcSet", context.GetImageSrcSet( AV24qrcode), true);
               imgavQrcode_Visible = 1;
               AssignProp("", false, imgavQrcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavQrcode_Visible), 5, 0), true);
               chkavShowauthenticatorcode.Visible = 1;
               AssignProp("", false, chkavShowauthenticatorcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavShowauthenticatorcode.Visible), 5, 0), true);
            }
            else
            {
               imgavQrcode_Visible = 0;
               AssignProp("", false, imgavQrcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavQrcode_Visible), 5, 0), true);
               chkavShowauthenticatorcode.Visible = 0;
               AssignProp("", false, chkavShowauthenticatorcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavShowauthenticatorcode.Visible), 5, 0), true);
               AV28showAuthenticatorCode = false;
               AssignAttri("", false, "AV28showAuthenticatorCode", AV28showAuthenticatorCode);
               edtavBase32string_Visible = 0;
               AssignProp("", false, edtavBase32string_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavBase32string_Visible), 5, 0), true);
            }
         }
         else
         {
            AV23authenticator = false;
            AssignAttri("", false, "AV23authenticator", AV23authenticator);
         }
         /*  Sending Event outputs  */
      }

      protected void nextLoad( )
      {
      }

      protected void E160K2( )
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
         PA0K2( ) ;
         WS0K2( ) ;
         WE0K2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20261301630955", true, true, false);
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
         context.AddJavascriptSource("wallet/createwallet.js", "?20261301630955", false, true, false);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         cmbavNetworktype.Name = "vNETWORKTYPE";
         cmbavNetworktype.WebTags = "";
         cmbavNetworktype.addItem("MainNet", "MainNet", 0);
         cmbavNetworktype.addItem("TestNet", "TestNet (for testing only)", 0);
         cmbavNetworktype.addItem("RegTest", "RegTest (for testing only)", 0);
         if ( cmbavNetworktype.ItemCount > 0 )
         {
            AV15networkType = cmbavNetworktype.getValidValue(AV15networkType);
            AssignAttri("", false, "AV15networkType", AV15networkType);
         }
         chkavAuthenticator.Name = "vAUTHENTICATOR";
         chkavAuthenticator.WebTags = "";
         chkavAuthenticator.Caption = "Use 2FA (authenticator) for extra security";
         AssignProp("", false, chkavAuthenticator_Internalname, "TitleCaption", chkavAuthenticator.Caption, true);
         chkavAuthenticator.CheckedValue = "false";
         AV23authenticator = StringUtil.StrToBool( StringUtil.BoolToStr( AV23authenticator));
         AssignAttri("", false, "AV23authenticator", AV23authenticator);
         chkavShowauthenticatorcode.Name = "vSHOWAUTHENTICATORCODE";
         chkavShowauthenticatorcode.WebTags = "";
         chkavShowauthenticatorcode.Caption = "Show Authenticator Code";
         AssignProp("", false, chkavShowauthenticatorcode_Internalname, "TitleCaption", chkavShowauthenticatorcode.Caption, true);
         chkavShowauthenticatorcode.CheckedValue = "false";
         AV28showAuthenticatorCode = StringUtil.StrToBool( StringUtil.BoolToStr( AV28showAuthenticatorCode));
         AssignAttri("", false, "AV28showAuthenticatorCode", AV28showAuthenticatorCode);
         cmbavMnemoniclanguage.Name = "vMNEMONICLANGUAGE";
         cmbavMnemoniclanguage.WebTags = "";
         cmbavMnemoniclanguage.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(0), 4, 0)), "(None)", 0);
         cmbavMnemoniclanguage.addItem("10", "English", 0);
         cmbavMnemoniclanguage.addItem("20", "Spanish", 0);
         if ( cmbavMnemoniclanguage.ItemCount > 0 )
         {
            AV12mnemonicLanguage = (short)(Math.Round(NumberUtil.Val( cmbavMnemoniclanguage.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV12mnemonicLanguage), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV12mnemonicLanguage", StringUtil.LTrimStr( (decimal)(AV12mnemonicLanguage), 4, 0));
         }
         cmbavMnemonicnumberwords.Name = "vMNEMONICNUMBERWORDS";
         cmbavMnemonicnumberwords.WebTags = "";
         cmbavMnemonicnumberwords.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(0), 4, 0)), "(None)", 0);
         cmbavMnemonicnumberwords.addItem("12", "12", 0);
         cmbavMnemonicnumberwords.addItem("15", "15", 0);
         cmbavMnemonicnumberwords.addItem("18", "18", 0);
         cmbavMnemonicnumberwords.addItem("21", "21", 0);
         cmbavMnemonicnumberwords.addItem("24", "24", 0);
         if ( cmbavMnemonicnumberwords.ItemCount > 0 )
         {
            AV13mnemonicNumberWords = (short)(Math.Round(NumberUtil.Val( cmbavMnemonicnumberwords.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV13mnemonicNumberWords), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV13mnemonicNumberWords", StringUtil.LTrimStr( (decimal)(AV13mnemonicNumberWords), 4, 0));
         }
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavWalletname_Internalname = "vWALLETNAME";
         cmbavNetworktype_Internalname = "vNETWORKTYPE";
         edtavNewpass_Internalname = "vNEWPASS";
         edtavNewpassconfirm_Internalname = "vNEWPASSCONFIRM";
         chkavAuthenticator_Internalname = "vAUTHENTICATOR";
         imgavQrcode_Internalname = "vQRCODE";
         chkavShowauthenticatorcode_Internalname = "vSHOWAUTHENTICATORCODE";
         edtavBase32string_Internalname = "vBASE32STRING";
         cmbavMnemoniclanguage_Internalname = "vMNEMONICLANGUAGE";
         cmbavMnemonicnumberwords_Internalname = "vMNEMONICNUMBERWORDS";
         edtavMnemonictext_Internalname = "vMNEMONICTEXT";
         lblTxt1_Internalname = "TXT1";
         bttCreatewallet_Internalname = "CREATEWALLET";
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
         chkavShowauthenticatorcode.Caption = "Show Authenticator Code";
         chkavAuthenticator.Caption = "Use 2FA (authenticator) for extra security";
         bttCreatewallet_Visible = 1;
         lblTxt1_Caption = "This seed will allow you to recover your wallet in case of computer failure.";
         lblTxt1_Visible = 1;
         edtavMnemonictext_Enabled = 1;
         cmbavMnemonicnumberwords_Jsonclick = "";
         cmbavMnemonicnumberwords.Enabled = 1;
         cmbavMnemoniclanguage_Jsonclick = "";
         cmbavMnemoniclanguage.Enabled = 1;
         edtavBase32string_Jsonclick = "";
         edtavBase32string_Enabled = 1;
         edtavBase32string_Visible = 1;
         chkavShowauthenticatorcode.Enabled = 1;
         chkavShowauthenticatorcode.Visible = 1;
         imgavQrcode_gximage = "";
         imgavQrcode_Visible = 1;
         chkavAuthenticator.Enabled = 1;
         edtavNewpassconfirm_Jsonclick = "";
         edtavNewpassconfirm_Enabled = 1;
         edtavNewpass_Jsonclick = "";
         edtavNewpass_Enabled = 1;
         cmbavNetworktype_Jsonclick = "";
         cmbavNetworktype.Enabled = 1;
         edtavWalletname_Enabled = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Create Wallet";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV23authenticator","fld":"vAUTHENTICATOR","type":"boolean"},{"av":"AV28showAuthenticatorCode","fld":"vSHOWAUTHENTICATORCODE","type":"boolean"}]}""");
         setEventMetadata("VMNEMONICNUMBERWORDS.CONTROLVALUECHANGED","""{"handler":"E120K2","iparms":[{"av":"cmbavNetworktype"},{"av":"AV15networkType","fld":"vNETWORKTYPE","type":"char"},{"av":"AV8extKeyCreate","fld":"vEXTKEYCREATE","type":""},{"av":"cmbavMnemonicnumberwords"},{"av":"AV13mnemonicNumberWords","fld":"vMNEMONICNUMBERWORDS","pic":"ZZZ9","type":"int"},{"av":"cmbavMnemoniclanguage"},{"av":"AV12mnemonicLanguage","fld":"vMNEMONICLANGUAGE","pic":"ZZZ9","type":"int"},{"av":"AV7error","fld":"vERROR","type":"char"},{"av":"AV16newPass","fld":"vNEWPASS","type":"char"},{"av":"AV19wallet","fld":"vWALLET","type":""},{"av":"AV20walletName","fld":"vWALLETNAME","type":"char"},{"av":"AV29extendeSecretAndAuthenticator","fld":"vEXTENDESECRETANDAUTHENTICATOR","type":""},{"av":"AV23authenticator","fld":"vAUTHENTICATOR","type":"boolean"},{"av":"AV27base32String","fld":"vBASE32STRING","type":"char"}]""");
         setEventMetadata("VMNEMONICNUMBERWORDS.CONTROLVALUECHANGED",""","oparms":[{"av":"AV8extKeyCreate","fld":"vEXTKEYCREATE","type":""},{"av":"AV7error","fld":"vERROR","type":"char"},{"av":"AV19wallet","fld":"vWALLET","type":""},{"av":"AV29extendeSecretAndAuthenticator","fld":"vEXTENDESECRETANDAUTHENTICATOR","type":""},{"av":"AV14MnemonicText","fld":"vMNEMONICTEXT","type":"vchar"},{"av":"lblTxt1_Visible","ctrl":"TXT1","prop":"Visible"},{"ctrl":"CREATEWALLET","prop":"Visible"},{"av":"lblTxt1_Caption","ctrl":"TXT1","prop":"Caption"}]}""");
         setEventMetadata("'CREATE WALLET'","""{"handler":"E130K2","iparms":[{"av":"AV20walletName","fld":"vWALLETNAME","type":"char"},{"av":"AV16newPass","fld":"vNEWPASS","type":"char"},{"av":"AV17newPassConfirm","fld":"vNEWPASSCONFIRM","type":"char"},{"av":"cmbavMnemoniclanguage"},{"av":"AV12mnemonicLanguage","fld":"vMNEMONICLANGUAGE","pic":"ZZZ9","type":"int"},{"av":"cmbavMnemonicnumberwords"},{"av":"AV13mnemonicNumberWords","fld":"vMNEMONICNUMBERWORDS","pic":"ZZZ9","type":"int"},{"av":"AV19wallet","fld":"vWALLET","type":""}]}""");
         setEventMetadata("'CANCEL AND CLOSE'","""{"handler":"E140K2","iparms":[]}""");
         setEventMetadata("VAUTHENTICATOR.CONTROLVALUECHANGED","""{"handler":"E150K2","iparms":[{"av":"AV20walletName","fld":"vWALLETNAME","type":"char"},{"av":"AV16newPass","fld":"vNEWPASS","type":"char"},{"av":"AV23authenticator","fld":"vAUTHENTICATOR","type":"boolean"}]""");
         setEventMetadata("VAUTHENTICATOR.CONTROLVALUECHANGED",""","oparms":[{"av":"AV7error","fld":"vERROR","type":"char"},{"av":"AV27base32String","fld":"vBASE32STRING","type":"char"},{"av":"AV24qrcode","fld":"vQRCODE","type":"bits"},{"av":"AV28showAuthenticatorCode","fld":"vSHOWAUTHENTICATORCODE","type":"boolean"},{"av":"edtavBase32string_Visible","ctrl":"vBASE32STRING","prop":"Visible"},{"av":"imgavQrcode_Visible","ctrl":"vQRCODE","prop":"Visible"},{"av":"chkavShowauthenticatorcode.Visible","ctrl":"vSHOWAUTHENTICATORCODE","prop":"Visible"},{"av":"AV23authenticator","fld":"vAUTHENTICATOR","type":"boolean"}]}""");
         setEventMetadata("VALIDV_NETWORKTYPE","""{"handler":"Validv_Networktype","iparms":[]}""");
         setEventMetadata("VALIDV_MNEMONICLANGUAGE","""{"handler":"Validv_Mnemoniclanguage","iparms":[]}""");
         setEventMetadata("VALIDV_MNEMONICNUMBERWORDS","""{"handler":"Validv_Mnemonicnumberwords","iparms":[]}""");
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
         GXKey = "";
         AV8extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV7error = "";
         AV19wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV29extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV20walletName = "";
         AV15networkType = "";
         AV16newPass = "";
         AV17newPassConfirm = "";
         AV24qrcode = "";
         AV30Qrcode_GXI = "";
         sImgUrl = "";
         AV27base32String = "";
         AV14MnemonicText = "";
         lblTxt1_Jsonclick = "";
         bttCreatewallet_Jsonclick = "";
         bttCancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV9extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_char1 = "";
         AV11keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         GXt_char2 = "";
         AV25SetupCode = new GeneXus.Programs.googleauthenticator.SdtSetupCode(context);
         AV26TwoFactorAuthenticator = new GeneXus.Programs.googleauthenticator.SdtTwoFactorAuthenticator(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
         edtavBase32string_Enabled = 0;
         edtavMnemonictext_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV12mnemonicLanguage ;
      private short AV13mnemonicNumberWords ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int edtavWalletname_Enabled ;
      private int edtavNewpass_Enabled ;
      private int edtavNewpassconfirm_Enabled ;
      private int imgavQrcode_Visible ;
      private int edtavBase32string_Visible ;
      private int edtavBase32string_Enabled ;
      private int edtavMnemonictext_Enabled ;
      private int lblTxt1_Visible ;
      private int bttCreatewallet_Visible ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV7error ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string edtavWalletname_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string AV20walletName ;
      private string cmbavNetworktype_Internalname ;
      private string AV15networkType ;
      private string cmbavNetworktype_Jsonclick ;
      private string edtavNewpass_Internalname ;
      private string AV16newPass ;
      private string edtavNewpass_Jsonclick ;
      private string edtavNewpassconfirm_Internalname ;
      private string AV17newPassConfirm ;
      private string edtavNewpassconfirm_Jsonclick ;
      private string chkavAuthenticator_Internalname ;
      private string imgavQrcode_Internalname ;
      private string imgavQrcode_gximage ;
      private string sImgUrl ;
      private string chkavShowauthenticatorcode_Internalname ;
      private string edtavBase32string_Internalname ;
      private string AV27base32String ;
      private string edtavBase32string_Jsonclick ;
      private string cmbavMnemoniclanguage_Internalname ;
      private string cmbavMnemoniclanguage_Jsonclick ;
      private string cmbavMnemonicnumberwords_Internalname ;
      private string cmbavMnemonicnumberwords_Jsonclick ;
      private string edtavMnemonictext_Internalname ;
      private string lblTxt1_Internalname ;
      private string lblTxt1_Caption ;
      private string lblTxt1_Jsonclick ;
      private string bttCreatewallet_Internalname ;
      private string bttCreatewallet_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool AV23authenticator ;
      private bool AV24qrcode_IsBlob ;
      private bool AV28showAuthenticatorCode ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private string AV14MnemonicText ;
      private string AV30Qrcode_GXI ;
      private string AV24qrcode ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavNetworktype ;
      private GXCheckbox chkavAuthenticator ;
      private GXCheckbox chkavShowauthenticatorcode ;
      private GXCombobox cmbavMnemoniclanguage ;
      private GXCombobox cmbavMnemonicnumberwords ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV8extKeyCreate ;
      private GeneXus.Programs.wallet.SdtWallet AV19wallet ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV29extendeSecretAndAuthenticator ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV9extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV11keyInfo ;
      private GeneXus.Programs.googleauthenticator.SdtSetupCode AV25SetupCode ;
      private GeneXus.Programs.googleauthenticator.SdtTwoFactorAuthenticator AV26TwoFactorAuthenticator ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
