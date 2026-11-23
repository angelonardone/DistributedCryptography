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
namespace GeneXus.Programs.hsm {
   public class hsmconfig : GXDataArea
   {
      public hsmconfig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public hsmconfig( IGxContext context )
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
         chkavActivate = new GXCheckbox();
         chkavRequirehmac = new GXCheckbox();
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
         PA342( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START342( ) ;
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
         context.AddJavascriptSource("UserControls/Wallet.UC_Clipboard_v1Render.js", "", false, true, false);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("hsm.hsmconfig") +"\">") ;
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
         GxWebStd.gx_boolean_hidden_field( context, "vSAVEDACTIVATE", AV19savedActivate);
         GxWebStd.gx_boolean_hidden_field( context, "vHASAPIKEY", AV17hasApiKey);
         GxWebStd.gx_boolean_hidden_field( context, "vSAVEDREQUIREHMAC", AV20savedRequireHmac);
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
            WE342( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT342( ) ;
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
         return formatLink("hsm.hsmconfig")  ;
      }

      public override string GetPgmname( )
      {
         return "HSM.HSMconfig" ;
      }

      public override string GetPgmdesc( )
      {
         return "HSMconfig" ;
      }

      protected void WB340( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavHsmderivationpath_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavHsmderivationpath_Internalname, "HSM Derivation Path", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavHsmderivationpath_Internalname, StringUtil.RTrim( AV5hsmDerivationPath), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", 0, 1, edtavHsmderivationpath_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavActivate_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavActivate_Internalname, "Activate", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavActivate_Internalname, StringUtil.BoolToStr( AV6activate), "", "Activate", 1, chkavActivate.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(13, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,13);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavRequirehmac_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavRequirehmac_Internalname, "Accept HMAC-signed requests only (no Bearer)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRequirehmac_Internalname, StringUtil.BoolToStr( AV18requireHmac), "", "Accept HMAC-signed requests only (no Bearer)", 1, chkavRequirehmac.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(18, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,18);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSave_Internalname, "", "Save", bttSave_Jsonclick, 5, "Save", "", StyleString, ClassString, bttSave_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'SAVE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel", bttCancel_Jsonclick, 7, "Cancel", "", StyleString, ClassString, bttCancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"e11341_client"+"'", TempTags, "", 2, "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbnoapikey_Internalname, "This HSM has no API key yet, so it refuses every call. Click Regenerate API key.", "", "", lblTbnoapikey_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblTbnoapikey_Visible, 1, 0, 0, "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRegenerateapikey_Internalname, "", "Regenerate API key", bttRegenerateapikey_Jsonclick, 5, "Regenerate API key", "", StyleString, ClassString, bttRegenerateapikey_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'REGENERATE API KEY\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divApikeytable_Internalname, divApikeytable_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbapikey_Internalname, "The API key of the HSM. Copy it now and keep it with the program that calls the HSM: it is not shown again. If you lose it, regenerate it.", "", "", lblTbapikey_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavApikeyshown_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavApikeyshown_Internalname, "API key", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavApikeyshown_Internalname, StringUtil.RTrim( AV16apiKeyShown), StringUtil.RTrim( context.localUtil.Format( AV16apiKeyShown, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,40);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavApikeyshown_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavApikeyshown_Enabled, 0, "text", "", 64, "chr", 1, "row", 64, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 43,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCopyapikey_Internalname, "", "Copy API key", bttCopyapikey_Jsonclick, 7, "Copy API key", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e12341_client"+"'", TempTags, "", 2, "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 45,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttDone_Internalname, "", "Done", bttDone_Jsonclick, 7, "Done", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e13341_client"+"'", TempTags, "", 2, "HLP_HSM/HSMconfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucUc_clipboard_v11.Render(context, "wallet.uc_clipboard_v1", Uc_clipboard_v11_Internalname, "UC_CLIPBOARD_V11Container");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START342( )
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
         Form.Meta.addItem("description", "HSMconfig", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP340( ) ;
      }

      protected void WS342( )
      {
         START342( ) ;
         EVT342( ) ;
      }

      protected void EVT342( )
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
                              E14342 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Save' */
                              E15342 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'REGENERATE API KEY'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Regenerate API key' */
                              E16342 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E17342 ();
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

      protected void WE342( )
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

      protected void PA342( )
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
               GX_FocusControl = edtavHsmderivationpath_Internalname;
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
         AV6activate = StringUtil.StrToBool( StringUtil.BoolToStr( AV6activate));
         AssignAttri("", false, "AV6activate", AV6activate);
         AV18requireHmac = StringUtil.StrToBool( StringUtil.BoolToStr( AV18requireHmac));
         AssignAttri("", false, "AV18requireHmac", AV18requireHmac);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF342( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavHsmderivationpath_Enabled = 0;
         AssignProp("", false, edtavHsmderivationpath_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavHsmderivationpath_Enabled), 5, 0), true);
         edtavApikeyshown_Enabled = 0;
         AssignProp("", false, edtavApikeyshown_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavApikeyshown_Enabled), 5, 0), true);
      }

      protected void RF342( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E17342 ();
            WB340( ) ;
         }
      }

      protected void send_integrity_lvl_hashes342( )
      {
      }

      protected void before_start_formulas( )
      {
         edtavHsmderivationpath_Enabled = 0;
         AssignProp("", false, edtavHsmderivationpath_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavHsmderivationpath_Enabled), 5, 0), true);
         edtavApikeyshown_Enabled = 0;
         AssignProp("", false, edtavApikeyshown_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavApikeyshown_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP340( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E14342 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            AV20savedRequireHmac = StringUtil.StrToBool( cgiGet( "vSAVEDREQUIREHMAC"));
            AssignAttri("", false, "AV20savedRequireHmac", AV20savedRequireHmac);
            AV19savedActivate = StringUtil.StrToBool( cgiGet( "vSAVEDACTIVATE"));
            AssignAttri("", false, "AV19savedActivate", AV19savedActivate);
            /* Read variables values. */
            AV5hsmDerivationPath = cgiGet( edtavHsmderivationpath_Internalname);
            AssignAttri("", false, "AV5hsmDerivationPath", AV5hsmDerivationPath);
            AV6activate = StringUtil.StrToBool( cgiGet( chkavActivate_Internalname));
            AssignAttri("", false, "AV6activate", AV6activate);
            AV18requireHmac = StringUtil.StrToBool( cgiGet( chkavRequirehmac_Internalname));
            AssignAttri("", false, "AV18requireHmac", AV18requireHmac);
            AV16apiKeyShown = cgiGet( edtavApikeyshown_Internalname);
            AssignAttri("", false, "AV16apiKeyShown", AV16apiKeyShown);
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
         E14342 ();
         if (returnInSub) return;
      }

      protected void E14342( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV11wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV11wallet = GXt_SdtWallet1;
         bttSave_Visible = 0;
         AssignProp("", false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
         bttCancel_Visible = 0;
         AssignProp("", false, bttCancel_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCancel_Visible), 5, 0), true);
         divApikeytable_Visible = 0;
         AssignProp("", false, divApikeytable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divApikeytable_Visible), 5, 0), true);
         if ( StringUtil.StrCmp(AV11wallet.gxTpr_Networktype, "MainNet") == 0 )
         {
            AV5hsmDerivationPath = "m/86'/0'/0'" + "/" + "6000'";
            AssignAttri("", false, "AV5hsmDerivationPath", AV5hsmDerivationPath);
         }
         else if ( StringUtil.StrCmp(AV11wallet.gxTpr_Networktype, "TestNet") == 0 )
         {
            AV5hsmDerivationPath = "m/86'/1'/0'" + "/" + "6000'";
            AssignAttri("", false, "AV5hsmDerivationPath", AV5hsmDerivationPath);
         }
         else if ( StringUtil.StrCmp(AV11wallet.gxTpr_Networktype, "RegTest") == 0 )
         {
            AV5hsmDerivationPath = "m/86'/1'/0'" + "/" + "6000'";
            AssignAttri("", false, "AV5hsmDerivationPath", AV5hsmDerivationPath);
         }
         else
         {
            AV13error = "Network Type not sopported";
         }
         new GeneXus.Programs.hsm.gethsmsettings(context ).execute( out  AV6activate, out  AV18requireHmac, out  AV17hasApiKey, out  AV13error) ;
         AssignAttri("", false, "AV6activate", AV6activate);
         AssignAttri("", false, "AV18requireHmac", AV18requireHmac);
         AssignAttri("", false, "AV17hasApiKey", AV17hasApiKey);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            GX_msglist.addItem(AV13error);
         }
         AV19savedActivate = AV6activate;
         AssignAttri("", false, "AV19savedActivate", AV19savedActivate);
         AV20savedRequireHmac = AV18requireHmac;
         AssignAttri("", false, "AV20savedRequireHmac", AV20savedRequireHmac);
         /* Execute user subroutine: 'SHOW STATE' */
         S112 ();
         if (returnInSub) return;
      }

      protected void E15342( )
      {
         /* 'Save' Routine */
         returnInSub = false;
         new GeneXus.Programs.hsm.savehsmsettings(context ).execute(  AV6activate,  AV18requireHmac, out  AV16apiKeyShown, out  AV13error) ;
         AssignAttri("", false, "AV16apiKeyShown", AV16apiKeyShown);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV19savedActivate = AV6activate;
            AssignAttri("", false, "AV19savedActivate", AV19savedActivate);
            AV20savedRequireHmac = AV18requireHmac;
            AssignAttri("", false, "AV20savedRequireHmac", AV20savedRequireHmac);
            bttSave_Visible = 0;
            AssignProp("", false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
            bttCancel_Visible = 0;
            AssignProp("", false, bttCancel_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCancel_Visible), 5, 0), true);
            if ( AV6activate )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV16apiKeyShown)) )
               {
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"HSM saved",(string)""}, true);
               }
               else
               {
                  AV17hasApiKey = true;
                  AssignAttri("", false, "AV17hasApiKey", AV17hasApiKey);
                  divApikeytable_Visible = 1;
                  AssignProp("", false, divApikeytable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divApikeytable_Visible), 5, 0), true);
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"HSM Ready",(string)"Copy the API key, then open the wallet again to start the HSM"}, true);
               }
            }
            else
            {
               AV17hasApiKey = false;
               AssignAttri("", false, "AV17hasApiKey", AV17hasApiKey);
               AV16apiKeyShown = "";
               AssignAttri("", false, "AV16apiKeyShown", AV16apiKeyShown);
               divApikeytable_Visible = 0;
               AssignProp("", false, divApikeytable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divApikeytable_Visible), 5, 0), true);
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"HSM De-activated",(string)"De-Activated"}, true);
            }
            /* Execute user subroutine: 'SHOW STATE' */
            S112 ();
            if (returnInSub) return;
         }
         else
         {
            GX_msglist.addItem(AV13error);
         }
         /*  Sending Event outputs  */
      }

      protected void E16342( )
      {
         /* 'Regenerate API key' Routine */
         returnInSub = false;
         new GeneXus.Programs.hsm.regeneratehsmapikey(context ).execute( out  AV16apiKeyShown, out  AV13error) ;
         AssignAttri("", false, "AV16apiKeyShown", AV16apiKeyShown);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13error)) )
         {
            AV17hasApiKey = true;
            AssignAttri("", false, "AV17hasApiKey", AV17hasApiKey);
            divApikeytable_Visible = 1;
            AssignProp("", false, divApikeytable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divApikeytable_Visible), 5, 0), true);
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"New API key",(string)"The old API key no longer works"}, true);
            /* Execute user subroutine: 'SHOW STATE' */
            S112 ();
            if (returnInSub) return;
         }
         else
         {
            GX_msglist.addItem(AV13error);
         }
         /*  Sending Event outputs  */
      }

      protected void S112( )
      {
         /* 'SHOW STATE' Routine */
         returnInSub = false;
         bttRegenerateapikey_Visible = (AV19savedActivate ? 1 : 0);
         AssignProp("", false, bttRegenerateapikey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRegenerateapikey_Visible), 5, 0), true);
         lblTbnoapikey_Visible = (AV19savedActivate&&!AV17hasApiKey ? 1 : 0);
         AssignProp("", false, lblTbnoapikey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbnoapikey_Visible), 5, 0), true);
      }

      protected void nextLoad( )
      {
      }

      protected void E17342( )
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
         PA342( ) ;
         WS342( ) ;
         WE342( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202611238562485", true, true, false);
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
         context.AddJavascriptSource("hsm/hsmconfig.js", "?202611238562485", false, true, false);
         context.AddJavascriptSource("UserControls/Wallet.UC_Clipboard_v1Render.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         chkavActivate.Name = "vACTIVATE";
         chkavActivate.WebTags = "";
         chkavActivate.Caption = "Activate";
         AssignProp("", false, chkavActivate_Internalname, "TitleCaption", chkavActivate.Caption, true);
         chkavActivate.CheckedValue = "false";
         AV6activate = StringUtil.StrToBool( StringUtil.BoolToStr( AV6activate));
         AssignAttri("", false, "AV6activate", AV6activate);
         chkavRequirehmac.Name = "vREQUIREHMAC";
         chkavRequirehmac.WebTags = "";
         chkavRequirehmac.Caption = "Accept HMAC-signed requests only (no Bearer)";
         AssignProp("", false, chkavRequirehmac_Internalname, "TitleCaption", chkavRequirehmac.Caption, true);
         chkavRequirehmac.CheckedValue = "false";
         AV18requireHmac = StringUtil.StrToBool( StringUtil.BoolToStr( AV18requireHmac));
         AssignAttri("", false, "AV18requireHmac", AV18requireHmac);
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavHsmderivationpath_Internalname = "vHSMDERIVATIONPATH";
         chkavActivate_Internalname = "vACTIVATE";
         chkavRequirehmac_Internalname = "vREQUIREHMAC";
         bttSave_Internalname = "SAVE";
         bttCancel_Internalname = "CANCEL";
         lblTbnoapikey_Internalname = "TBNOAPIKEY";
         bttRegenerateapikey_Internalname = "REGENERATEAPIKEY";
         lblTbapikey_Internalname = "TBAPIKEY";
         edtavApikeyshown_Internalname = "vAPIKEYSHOWN";
         bttCopyapikey_Internalname = "COPYAPIKEY";
         bttDone_Internalname = "DONE";
         divApikeytable_Internalname = "APIKEYTABLE";
         Uc_clipboard_v11_Internalname = "UC_CLIPBOARD_V11";
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
         chkavRequirehmac.Caption = "Accept HMAC-signed requests only (no Bearer)";
         chkavActivate.Caption = "Activate";
         edtavApikeyshown_Jsonclick = "";
         edtavApikeyshown_Enabled = 1;
         divApikeytable_Visible = 1;
         bttRegenerateapikey_Visible = 1;
         lblTbnoapikey_Visible = 1;
         bttCancel_Visible = 1;
         bttSave_Visible = 1;
         chkavRequirehmac.Enabled = 1;
         chkavActivate.Enabled = 1;
         edtavHsmderivationpath_Enabled = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "HSMconfig";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV6activate","fld":"vACTIVATE","type":"boolean"},{"av":"AV18requireHmac","fld":"vREQUIREHMAC","type":"boolean"}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E15342","iparms":[{"av":"AV6activate","fld":"vACTIVATE","type":"boolean"},{"av":"AV18requireHmac","fld":"vREQUIREHMAC","type":"boolean"},{"av":"AV19savedActivate","fld":"vSAVEDACTIVATE","type":"boolean"},{"av":"AV17hasApiKey","fld":"vHASAPIKEY","type":"boolean"}]""");
         setEventMetadata("'SAVE'",""","oparms":[{"av":"AV16apiKeyShown","fld":"vAPIKEYSHOWN","type":"char"},{"av":"AV19savedActivate","fld":"vSAVEDACTIVATE","type":"boolean"},{"av":"AV20savedRequireHmac","fld":"vSAVEDREQUIREHMAC","type":"boolean"},{"ctrl":"SAVE","prop":"Visible"},{"ctrl":"CANCEL","prop":"Visible"},{"av":"AV17hasApiKey","fld":"vHASAPIKEY","type":"boolean"},{"av":"divApikeytable_Visible","ctrl":"APIKEYTABLE","prop":"Visible"},{"ctrl":"REGENERATEAPIKEY","prop":"Visible"},{"av":"lblTbnoapikey_Visible","ctrl":"TBNOAPIKEY","prop":"Visible"}]}""");
         setEventMetadata("'REGENERATE API KEY'","""{"handler":"E16342","iparms":[{"av":"AV19savedActivate","fld":"vSAVEDACTIVATE","type":"boolean"},{"av":"AV17hasApiKey","fld":"vHASAPIKEY","type":"boolean"}]""");
         setEventMetadata("'REGENERATE API KEY'",""","oparms":[{"av":"AV16apiKeyShown","fld":"vAPIKEYSHOWN","type":"char"},{"av":"AV17hasApiKey","fld":"vHASAPIKEY","type":"boolean"},{"av":"divApikeytable_Visible","ctrl":"APIKEYTABLE","prop":"Visible"},{"ctrl":"REGENERATEAPIKEY","prop":"Visible"},{"av":"lblTbnoapikey_Visible","ctrl":"TBNOAPIKEY","prop":"Visible"}]}""");
         setEventMetadata("'COPY API KEY'","""{"handler":"E12341","iparms":[{"av":"AV16apiKeyShown","fld":"vAPIKEYSHOWN","type":"char"}]}""");
         setEventMetadata("'DONE'","""{"handler":"E13341","iparms":[]""");
         setEventMetadata("'DONE'",""","oparms":[{"av":"AV16apiKeyShown","fld":"vAPIKEYSHOWN","type":"char"},{"av":"divApikeytable_Visible","ctrl":"APIKEYTABLE","prop":"Visible"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E11341","iparms":[{"av":"AV19savedActivate","fld":"vSAVEDACTIVATE","type":"boolean"},{"av":"AV20savedRequireHmac","fld":"vSAVEDREQUIREHMAC","type":"boolean"}]""");
         setEventMetadata("'CANCEL'",""","oparms":[{"av":"AV6activate","fld":"vACTIVATE","type":"boolean"},{"av":"AV18requireHmac","fld":"vREQUIREHMAC","type":"boolean"},{"ctrl":"SAVE","prop":"Visible"},{"ctrl":"CANCEL","prop":"Visible"}]}""");
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
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV5hsmDerivationPath = "";
         bttSave_Jsonclick = "";
         bttCancel_Jsonclick = "";
         lblTbnoapikey_Jsonclick = "";
         bttRegenerateapikey_Jsonclick = "";
         lblTbapikey_Jsonclick = "";
         AV16apiKeyShown = "";
         bttCopyapikey_Jsonclick = "";
         bttDone_Jsonclick = "";
         ucUc_clipboard_v11 = new GXUserControl();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV11wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV13error = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
         edtavHsmderivationpath_Enabled = 0;
         edtavApikeyshown_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int edtavHsmderivationpath_Enabled ;
      private int bttSave_Visible ;
      private int bttCancel_Visible ;
      private int lblTbnoapikey_Visible ;
      private int bttRegenerateapikey_Visible ;
      private int divApikeytable_Visible ;
      private int edtavApikeyshown_Enabled ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string edtavHsmderivationpath_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string AV5hsmDerivationPath ;
      private string chkavActivate_Internalname ;
      private string chkavRequirehmac_Internalname ;
      private string bttSave_Internalname ;
      private string bttSave_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string lblTbnoapikey_Internalname ;
      private string lblTbnoapikey_Jsonclick ;
      private string bttRegenerateapikey_Internalname ;
      private string bttRegenerateapikey_Jsonclick ;
      private string divApikeytable_Internalname ;
      private string lblTbapikey_Internalname ;
      private string lblTbapikey_Jsonclick ;
      private string edtavApikeyshown_Internalname ;
      private string AV16apiKeyShown ;
      private string edtavApikeyshown_Jsonclick ;
      private string bttCopyapikey_Internalname ;
      private string bttCopyapikey_Jsonclick ;
      private string bttDone_Internalname ;
      private string bttDone_Jsonclick ;
      private string Uc_clipboard_v11_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV13error ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV19savedActivate ;
      private bool AV17hasApiKey ;
      private bool AV20savedRequireHmac ;
      private bool wbLoad ;
      private bool AV6activate ;
      private bool AV18requireHmac ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private GXUserControl ucUc_clipboard_v11 ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavActivate ;
      private GXCheckbox chkavRequirehmac ;
      private GeneXus.Programs.wallet.SdtWallet AV11wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
