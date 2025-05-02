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
namespace GeneXus.Programs.wallet {
   public class distcryptlogin : GXDataArea
   {
      public distcryptlogin( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public distcryptlogin( IGxContext context )
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
         cmbavCtlconnectiontype = new GXCombobox();
         chkavCtlsecure = new GXCheckbox();
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridnostrservers") == 0 )
            {
               gxnrGridnostrservers_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridnostrservers") == 0 )
            {
               gxgrGridnostrservers_refresh_invoke( ) ;
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
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrGridnostrservers_newrow_invoke( )
      {
         nRC_GXsfl_35 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_35"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_35_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_35_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_35_idx = GetPar( "sGXsfl_35_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridnostrservers_newrow( ) ;
         /* End function gxnrGridnostrservers_newrow_invoke */
      }

      protected void gxgrGridnostrservers_refresh_invoke( )
      {
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridnostrservers_refresh( ) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridnostrservers_refresh_invoke */
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
         PA132( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START132( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 123260), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.distcryptlogin") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Externaluser", AV6externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Externaluser", AV6externalUser);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Connectionparameters", AV42ConnectionParameters);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Connectionparameters", AV42ConnectionParameters);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_35", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_35), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV55PopupName));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCONNECTIONPARAMETERS", AV42ConnectionParameters);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCONNECTIONPARAMETERS", AV42ConnectionParameters);
         }
         GxWebStd.gx_hidden_field( context, "vCALLERURL", StringUtil.RTrim( AV5callerURL));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTERNALUSER", AV6externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTERNALUSER", AV6externalUser);
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
            WE132( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT132( ) ;
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
         return formatLink("wallet.distcryptlogin")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.DistCryptLogin" ;
      }

      public override string GetPgmdesc( )
      {
         return "Dist Crypt Login" ;
      }

      protected void WB130( )
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 6,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttLoginwithsso_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(35), 2, 0)+","+"null"+");", "Login with SSO", bttLoginwithsso_Jsonclick, 7, "Login with SSO", "", StyleString, ClassString, bttLoginwithsso_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"e11131_client"+"'", TempTags, "", 2, "HLP_Wallet/DistCryptLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 9,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttLoginanonymously_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(35), 2, 0)+","+"null"+");", "Login anonymously", bttLoginanonymously_Jsonclick, 5, "Login anonymously", "", StyleString, ClassString, bttLoginanonymously_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'LOGIN ANONYMOUSLY\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/DistCryptLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttLogout_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(35), 2, 0)+","+"null"+");", "Logout", bttLogout_Jsonclick, 5, "Logout", "", StyleString, ClassString, bttLogout_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'LOGOUT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/DistCryptLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblLoggedmessage_Internalname, "You are logged in as:", "", "", lblLoggedmessage_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblLoggedmessage_Visible, 1, 0, 0, "HLP_Wallet/DistCryptLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavCtlusername_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlusername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlusername_Internalname, "Your User Name", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'" + sGXsfl_35_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlusername_Internalname, AV6externalUser.gxTpr_Userinfo.gxTpr_Username, StringUtil.RTrim( context.localUtil.Format( AV6externalUser.gxTpr_Userinfo.gxTpr_Username, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlusername_Jsonclick, 0, "Attribute", "", "", "", "", edtavCtlusername_Visible, edtavCtlusername_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, 0, true, "", "start", true, "", "HLP_Wallet/DistCryptLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavOnerole_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOnerole_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOnerole_Internalname, "Subscription", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'',false,'" + sGXsfl_35_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOnerole_Internalname, AV39oneRole, StringUtil.RTrim( context.localUtil.Format( AV39oneRole, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,24);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOnerole_Jsonclick, 0, "Attribute", "", "", "", "", edtavOnerole_Visible, edtavOnerole_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/DistCryptLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 20, "px", "col-xs-12", "start", "top", "", "", "div");
            context.WriteHtmlText( "<hr/>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockoptional_Internalname, "Optionaly", "", "", lblTextblockoptional_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextblockMedium", 0, "", lblTextblockoptional_Visible, 1, 0, 0, "HLP_Wallet/DistCryptLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttConfigureyournostrservers_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(35), 2, 0)+","+"null"+");", "Configure your Nostr servers", bttConfigureyournostrservers_Jsonclick, 7, "Configure your Nostr servers", "", StyleString, ClassString, bttConfigureyournostrservers_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"e12131_client"+"'", TempTags, "", 2, "HLP_Wallet/DistCryptLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridnostrserversContainer.SetWrapped(nGXWrapped);
            StartGridControl35( ) ;
         }
         if ( wbEnd == 35 )
         {
            wbEnd = 0;
            nRC_GXsfl_35 = (int)(nGXsfl_35_idx-1);
            if ( GridnostrserversContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV65GXV2 = nGXsfl_35_idx;
               if ( subGridnostrservers_Visible != 0 )
               {
                  sStyleString = "";
               }
               else
               {
                  sStyleString = " style=\"display:none;\"";
               }
               context.WriteHtmlText( "<div id=\""+"GridnostrserversContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridnostrservers", GridnostrserversContainer, subGridnostrservers_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridnostrserversContainerData", GridnostrserversContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridnostrserversContainerData"+"V", GridnostrserversContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridnostrserversContainerData"+"V"+"\" value='"+GridnostrserversContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 35 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridnostrserversContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV65GXV2 = nGXsfl_35_idx;
                  if ( subGridnostrservers_Visible != 0 )
                  {
                     sStyleString = "";
                  }
                  else
                  {
                     sStyleString = " style=\"display:none;\"";
                  }
                  context.WriteHtmlText( "<div id=\""+"GridnostrserversContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridnostrservers", GridnostrserversContainer, subGridnostrservers_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridnostrserversContainerData", GridnostrserversContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridnostrserversContainerData"+"V", GridnostrserversContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridnostrserversContainerData"+"V"+"\" value='"+GridnostrserversContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START132( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_12-186073", 0) ;
            }
         }
         Form.Meta.addItem("description", "Dist Crypt Login", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP130( ) ;
      }

      protected void WS132( )
      {
         START132( ) ;
         EVT132( ) ;
      }

      protected void EVT132( )
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
                           else if ( StringUtil.StrCmp(sEvt, "'LOGIN ANONYMOUSLY'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Login anonymously' */
                              E13132 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'LOGOUT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Logout' */
                              E14132 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E15132 ();
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
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 21), "GRIDNOSTRSERVERS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              nGXsfl_35_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
                              SubsflControlProps_352( ) ;
                              AV65GXV2 = nGXsfl_35_idx;
                              if ( ( AV42ConnectionParameters.Count >= AV65GXV2 ) && ( AV65GXV2 > 0 ) )
                              {
                                 AV42ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2));
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E16132 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDNOSTRSERVERS.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridnostrservers.Load */
                                    E17132 ();
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

      protected void WE132( )
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

      protected void PA132( )
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
               GX_FocusControl = edtavCtlusername_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridnostrservers_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_352( ) ;
         while ( nGXsfl_35_idx <= nRC_GXsfl_35 )
         {
            sendrow_352( ) ;
            nGXsfl_35_idx = ((subGridnostrservers_Islastpage==1)&&(nGXsfl_35_idx+1>subGridnostrservers_fnc_Recordsperpage( )) ? 1 : nGXsfl_35_idx+1);
            sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
            SubsflControlProps_352( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridnostrserversContainer)) ;
         /* End function gxnrGridnostrservers_newrow */
      }

      protected void gxgrGridnostrservers_refresh( )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDNOSTRSERVERS_nCurrentRecord = 0;
         RF132( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridnostrservers_refresh */
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
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF132( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlusername_Enabled = 0;
         AssignProp("", false, edtavCtlusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlusername_Enabled), 5, 0), true);
         edtavOnerole_Enabled = 0;
         AssignProp("", false, edtavOnerole_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOnerole_Enabled), 5, 0), true);
         cmbavCtlconnectiontype.Enabled = 0;
         edtavCtlhostname_Enabled = 0;
         edtavCtlport_Enabled = 0;
         chkavCtlsecure.Enabled = 0;
         edtavCtltimeoutmiliseconds_Enabled = 0;
      }

      protected void RF132( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridnostrserversContainer.ClearRows();
         }
         wbStart = 35;
         nGXsfl_35_idx = 1;
         sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
         SubsflControlProps_352( ) ;
         bGXsfl_35_Refreshing = true;
         GridnostrserversContainer.AddObjectProperty("GridName", "Gridnostrservers");
         GridnostrserversContainer.AddObjectProperty("CmpContext", "");
         GridnostrserversContainer.AddObjectProperty("InMasterPage", "false");
         GridnostrserversContainer.AddObjectProperty("Class", "Grid");
         GridnostrserversContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridnostrserversContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridnostrserversContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Backcolorstyle), 1, 0, ".", "")));
         GridnostrserversContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Visible), 5, 0, ".", "")));
         GridnostrserversContainer.PageSize = subGridnostrservers_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_352( ) ;
            /* Execute user event: Gridnostrservers.Load */
            E17132 ();
            wbEnd = 35;
            WB130( ) ;
         }
         bGXsfl_35_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes132( )
      {
      }

      protected int subGridnostrservers_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridnostrservers_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridnostrservers_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridnostrservers_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlusername_Enabled = 0;
         AssignProp("", false, edtavCtlusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlusername_Enabled), 5, 0), true);
         edtavOnerole_Enabled = 0;
         AssignProp("", false, edtavOnerole_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOnerole_Enabled), 5, 0), true);
         cmbavCtlconnectiontype.Enabled = 0;
         edtavCtlhostname_Enabled = 0;
         edtavCtlport_Enabled = 0;
         chkavCtlsecure.Enabled = 0;
         edtavCtltimeoutmiliseconds_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP130( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E16132 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vEXTERNALUSER"), AV6externalUser);
            ajax_req_read_hidden_sdt(cgiGet( "Externaluser"), AV6externalUser);
            ajax_req_read_hidden_sdt(cgiGet( "Connectionparameters"), AV42ConnectionParameters);
            ajax_req_read_hidden_sdt(cgiGet( "vCONNECTIONPARAMETERS"), AV42ConnectionParameters);
            /* Read saved values. */
            nRC_GXsfl_35 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_35"), ".", ","), 18, MidpointRounding.ToEven));
            AV5callerURL = cgiGet( "vCALLERURL");
            nRC_GXsfl_35 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_35"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_35_fel_idx = 0;
            while ( nGXsfl_35_fel_idx < nRC_GXsfl_35 )
            {
               nGXsfl_35_fel_idx = ((subGridnostrservers_Islastpage==1)&&(nGXsfl_35_fel_idx+1>subGridnostrservers_fnc_Recordsperpage( )) ? 1 : nGXsfl_35_fel_idx+1);
               sGXsfl_35_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_352( ) ;
               AV65GXV2 = nGXsfl_35_fel_idx;
               if ( ( AV42ConnectionParameters.Count >= AV65GXV2 ) && ( AV65GXV2 > 0 ) )
               {
                  AV42ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2));
               }
            }
            if ( nGXsfl_35_fel_idx == 0 )
            {
               nGXsfl_35_idx = 1;
               sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
               SubsflControlProps_352( ) ;
            }
            nGXsfl_35_fel_idx = 1;
            /* Read variables values. */
            AV6externalUser.gxTpr_Userinfo.gxTpr_Username = cgiGet( edtavCtlusername_Internalname);
            AV39oneRole = cgiGet( edtavOnerole_Internalname);
            AssignAttri("", false, "AV39oneRole", AV39oneRole);
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
         E16132 ();
         if (returnInSub) return;
      }

      protected void E16132( )
      {
         /* Start Routine */
         returnInSub = false;
         bttLoginwithsso_Visible = 0;
         AssignProp("", false, bttLoginwithsso_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttLoginwithsso_Visible), 5, 0), true);
         GXt_SdtExternalUser1 = AV6externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV6externalUser = GXt_SdtExternalUser1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6externalUser.gxTpr_Externaltoken)) )
         {
            bttLoginanonymously_Visible = 1;
            AssignProp("", false, bttLoginanonymously_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttLoginanonymously_Visible), 5, 0), true);
            bttLogout_Visible = 0;
            AssignProp("", false, bttLogout_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttLogout_Visible), 5, 0), true);
            edtavCtlusername_Visible = 0;
            AssignProp("", false, edtavCtlusername_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCtlusername_Visible), 5, 0), true);
            lblLoggedmessage_Visible = 0;
            AssignProp("", false, lblLoggedmessage_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblLoggedmessage_Visible), 5, 0), true);
            edtavOnerole_Visible = 0;
            AssignProp("", false, edtavOnerole_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavOnerole_Visible), 5, 0), true);
            subGridnostrservers_Visible = 0;
            AssignProp("", false, "GridnostrserversContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subGridnostrservers_Visible), 5, 0), true);
            bttConfigureyournostrservers_Visible = 0;
            AssignProp("", false, bttConfigureyournostrservers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttConfigureyournostrservers_Visible), 5, 0), true);
            lblTextblockoptional_Visible = 0;
            AssignProp("", false, lblTextblockoptional_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTextblockoptional_Visible), 5, 0), true);
         }
         else
         {
            AV71GXV8 = 1;
            while ( AV71GXV8 <= AV6externalUser.gxTpr_Userinfo.gxTpr_Roles.Count )
            {
               AV38role = ((string)AV6externalUser.gxTpr_Userinfo.gxTpr_Roles.Item(AV71GXV8));
               AV37roles.Add(AV38role, 0);
               AV71GXV8 = (int)(AV71GXV8+1);
            }
            AV39oneRole = ((string)AV37roles.Item(1));
            AssignAttri("", false, "AV39oneRole", AV39oneRole);
            bttLoginanonymously_Visible = 0;
            AssignProp("", false, bttLoginanonymously_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttLoginanonymously_Visible), 5, 0), true);
            edtavCtlusername_Visible = 1;
            AssignProp("", false, edtavCtlusername_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCtlusername_Visible), 5, 0), true);
            lblLoggedmessage_Visible = 1;
            AssignProp("", false, lblLoggedmessage_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblLoggedmessage_Visible), 5, 0), true);
            edtavOnerole_Visible = 1;
            AssignProp("", false, edtavOnerole_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavOnerole_Visible), 5, 0), true);
            bttLogout_Visible = 1;
            AssignProp("", false, bttLogout_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttLogout_Visible), 5, 0), true);
            subGridnostrservers_Visible = 1;
            AssignProp("", false, "GridnostrserversContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subGridnostrservers_Visible), 5, 0), true);
            bttConfigureyournostrservers_Visible = 1;
            AssignProp("", false, bttConfigureyournostrservers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttConfigureyournostrservers_Visible), 5, 0), true);
            lblTextblockoptional_Visible = 1;
            AssignProp("", false, lblTextblockoptional_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTextblockoptional_Visible), 5, 0), true);
         }
         GXt_char2 = AV8error;
         new GeneXus.Programs.nostr.getnostrconfigservers(context ).execute( out  AV42ConnectionParameters, out  GXt_char2) ;
         gx_BV35 = true;
         AV8error = GXt_char2;
      }

      protected void E13132( )
      {
         /* 'Login anonymously' Routine */
         returnInSub = false;
         AV5callerURL = formatLink("wallet.distcryptlogin") ;
         AV36WebSession.Set("LoginCaller", AV5callerURL);
         CallWebObject(formatLink("distcrypt.sso.webloginanonymous") );
         context.wjLocDisableFrm = 1;
      }

      protected void E14132( )
      {
         /* 'Logout' Routine */
         returnInSub = false;
         AV6externalUser.FromJSonString("", null);
         new GeneXus.Programs.distcrypt.setexternaluser(context ).execute(  AV6externalUser) ;
         GXt_char2 = "";
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "distcrypt.conf",  AV6externalUser.ToJSonString(false, true), out  GXt_char2) ;
         CallWebObject(formatLink("wallet.distcryptlogin") );
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6externalUser", AV6externalUser);
      }

      protected void E15132( )
      {
         AV65GXV2 = nGXsfl_35_idx;
         if ( ( AV65GXV2 > 0 ) && ( AV42ConnectionParameters.Count >= AV65GXV2 ) )
         {
            AV42ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2));
         }
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV61sendCoinPopupName = "Wallet.ConfigreuNostrServers";
         AV63strFound = (short)(StringUtil.StringSearch( AV55PopupName, StringUtil.Lower( AV61sendCoinPopupName), 1));
         if ( AV63strFound > 0 )
         {
            GXt_char2 = AV8error;
            new GeneXus.Programs.nostr.getnostrconfigservers(context ).execute( out  AV42ConnectionParameters, out  GXt_char2) ;
            gx_BV35 = true;
            AV8error = GXt_char2;
         }
         /*  Sending Event outputs  */
         if ( gx_BV35 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV42ConnectionParameters", AV42ConnectionParameters);
            nGXsfl_35_bak_idx = nGXsfl_35_idx;
            gxgrGridnostrservers_refresh( ) ;
            nGXsfl_35_idx = nGXsfl_35_bak_idx;
            sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
            SubsflControlProps_352( ) ;
         }
      }

      private void E17132( )
      {
         /* Gridnostrservers_Load Routine */
         returnInSub = false;
         AV65GXV2 = 1;
         while ( AV65GXV2 <= AV42ConnectionParameters.Count )
         {
            AV42ConnectionParameters.CurrentItem = ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 35;
            }
            sendrow_352( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_35_Refreshing )
            {
               DoAjaxLoad(35, GridnostrserversRow);
            }
            AV65GXV2 = (int)(AV65GXV2+1);
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
         PA132( ) ;
         WS132( ) ;
         WE132( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20255213174061", true, true);
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
         context.AddJavascriptSource("wallet/distcryptlogin.js", "?20255213174061", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_352( )
      {
         cmbavCtlconnectiontype_Internalname = "CTLCONNECTIONTYPE_"+sGXsfl_35_idx;
         edtavCtlhostname_Internalname = "CTLHOSTNAME_"+sGXsfl_35_idx;
         edtavCtlport_Internalname = "CTLPORT_"+sGXsfl_35_idx;
         chkavCtlsecure_Internalname = "CTLSECURE_"+sGXsfl_35_idx;
         edtavCtltimeoutmiliseconds_Internalname = "CTLTIMEOUTMILISECONDS_"+sGXsfl_35_idx;
      }

      protected void SubsflControlProps_fel_352( )
      {
         cmbavCtlconnectiontype_Internalname = "CTLCONNECTIONTYPE_"+sGXsfl_35_fel_idx;
         edtavCtlhostname_Internalname = "CTLHOSTNAME_"+sGXsfl_35_fel_idx;
         edtavCtlport_Internalname = "CTLPORT_"+sGXsfl_35_fel_idx;
         chkavCtlsecure_Internalname = "CTLSECURE_"+sGXsfl_35_fel_idx;
         edtavCtltimeoutmiliseconds_Internalname = "CTLTIMEOUTMILISECONDS_"+sGXsfl_35_fel_idx;
      }

      protected void sendrow_352( )
      {
         sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
         SubsflControlProps_352( ) ;
         WB130( ) ;
         GridnostrserversRow = GXWebRow.GetNew(context,GridnostrserversContainer);
         if ( subGridnostrservers_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridnostrservers_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridnostrservers_Class, "") != 0 )
            {
               subGridnostrservers_Linesclass = subGridnostrservers_Class+"Odd";
            }
         }
         else if ( subGridnostrservers_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridnostrservers_Backstyle = 0;
            subGridnostrservers_Backcolor = subGridnostrservers_Allbackcolor;
            if ( StringUtil.StrCmp(subGridnostrservers_Class, "") != 0 )
            {
               subGridnostrservers_Linesclass = subGridnostrservers_Class+"Uniform";
            }
         }
         else if ( subGridnostrservers_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridnostrservers_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridnostrservers_Class, "") != 0 )
            {
               subGridnostrservers_Linesclass = subGridnostrservers_Class+"Odd";
            }
            subGridnostrservers_Backcolor = (int)(0x0);
         }
         else if ( subGridnostrservers_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridnostrservers_Backstyle = 1;
            if ( ((int)((nGXsfl_35_idx) % (2))) == 0 )
            {
               subGridnostrservers_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridnostrservers_Class, "") != 0 )
               {
                  subGridnostrservers_Linesclass = subGridnostrservers_Class+"Even";
               }
            }
            else
            {
               subGridnostrservers_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridnostrservers_Class, "") != 0 )
               {
                  subGridnostrservers_Linesclass = subGridnostrservers_Class+"Odd";
               }
            }
         }
         if ( GridnostrserversContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_35_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridnostrserversContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'',false,'" + sGXsfl_35_idx + "',35)\"";
         if ( ( cmbavCtlconnectiontype.ItemCount == 0 ) && isAjaxCallMode( ) )
         {
            GXCCtl = "CTLCONNECTIONTYPE_" + sGXsfl_35_idx;
            cmbavCtlconnectiontype.Name = GXCCtl;
            cmbavCtlconnectiontype.WebTags = "";
            cmbavCtlconnectiontype.addItem("ws", "Web Socket", 0);
            cmbavCtlconnectiontype.addItem("tcp", "Tcp Socket", 0);
            if ( cmbavCtlconnectiontype.ItemCount > 0 )
            {
               if ( ( AV65GXV2 > 0 ) && ( AV42ConnectionParameters.Count >= AV65GXV2 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Connectiontype)) )
               {
                  ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Connectiontype = cmbavCtlconnectiontype.getValidValue(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Connectiontype);
               }
            }
         }
         /* ComboBox */
         GridnostrserversRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavCtlconnectiontype,(string)cmbavCtlconnectiontype_Internalname,StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Connectiontype),(short)1,(string)cmbavCtlconnectiontype_Jsonclick,(short)0,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"char",(string)"",(short)-1,cmbavCtlconnectiontype.Enabled,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"Attribute",(string)"",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,36);\"",(string)"",(bool)true,(short)0});
         cmbavCtlconnectiontype.CurrentValue = StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Connectiontype);
         AssignProp("", false, cmbavCtlconnectiontype_Internalname, "Values", (string)(cmbavCtlconnectiontype.ToJavascriptSource()), !bGXsfl_35_Refreshing);
         /* Subfile cell */
         if ( GridnostrserversContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'" + sGXsfl_35_idx + "',35)\"";
         ROClassString = "Attribute";
         GridnostrserversRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlhostname_Internalname,StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Hostname),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlhostname_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlhostname_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)35,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridnostrserversContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'',false,'" + sGXsfl_35_idx + "',35)\"";
         ROClassString = "Attribute";
         GridnostrserversRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlport_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Port), 6, 0, ".", "")),StringUtil.LTrim( ((edtavCtlport_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Port), "ZZZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Port), "ZZZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,38);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlport_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlport_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)6,(short)0,(short)0,(short)35,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridnostrserversContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Check box */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'',false,'" + sGXsfl_35_idx + "',35)\"";
         ClassString = "Attribute";
         StyleString = "";
         GXCCtl = "CTLSECURE_" + sGXsfl_35_idx;
         chkavCtlsecure.Name = GXCCtl;
         chkavCtlsecure.WebTags = "";
         chkavCtlsecure.Caption = "";
         AssignProp("", false, chkavCtlsecure_Internalname, "TitleCaption", chkavCtlsecure.Caption, !bGXsfl_35_Refreshing);
         chkavCtlsecure.CheckedValue = "false";
         GridnostrserversRow.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkavCtlsecure_Internalname,StringUtil.BoolToStr( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Secure),(string)"",(string)"",(short)-1,chkavCtlsecure.Enabled,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",TempTags+" onclick="+"\"gx.fn.checkboxClick(39, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,39);\""});
         /* Subfile cell */
         if ( GridnostrserversContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'" + sGXsfl_35_idx + "',35)\"";
         ROClassString = "Attribute";
         GridnostrserversRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtltimeoutmiliseconds_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Timeoutmiliseconds), 6, 0, ".", "")),StringUtil.LTrim( ((edtavCtltimeoutmiliseconds_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Timeoutmiliseconds), "ZZZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Timeoutmiliseconds), "ZZZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,40);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtltimeoutmiliseconds_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtltimeoutmiliseconds_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)6,(short)0,(short)0,(short)35,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         send_integrity_lvl_hashes132( ) ;
         GridnostrserversContainer.AddRow(GridnostrserversRow);
         nGXsfl_35_idx = ((subGridnostrservers_Islastpage==1)&&(nGXsfl_35_idx+1>subGridnostrservers_fnc_Recordsperpage( )) ? 1 : nGXsfl_35_idx+1);
         sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
         SubsflControlProps_352( ) ;
         /* End function sendrow_352 */
      }

      protected void init_web_controls( )
      {
         GXCCtl = "CTLCONNECTIONTYPE_" + sGXsfl_35_idx;
         cmbavCtlconnectiontype.Name = GXCCtl;
         cmbavCtlconnectiontype.WebTags = "";
         cmbavCtlconnectiontype.addItem("ws", "Web Socket", 0);
         cmbavCtlconnectiontype.addItem("tcp", "Tcp Socket", 0);
         if ( cmbavCtlconnectiontype.ItemCount > 0 )
         {
            if ( ( AV65GXV2 > 0 ) && ( AV42ConnectionParameters.Count >= AV65GXV2 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Connectiontype)) )
            {
               ((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Connectiontype = cmbavCtlconnectiontype.getValidValue(((GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem)AV42ConnectionParameters.Item(AV65GXV2)).gxTpr_Connectiontype);
            }
         }
         GXCCtl = "CTLSECURE_" + sGXsfl_35_idx;
         chkavCtlsecure.Name = GXCCtl;
         chkavCtlsecure.WebTags = "";
         chkavCtlsecure.Caption = "";
         AssignProp("", false, chkavCtlsecure_Internalname, "TitleCaption", chkavCtlsecure.Caption, !bGXsfl_35_Refreshing);
         chkavCtlsecure.CheckedValue = "false";
         /* End function init_web_controls */
      }

      protected void StartGridControl35( )
      {
         if ( GridnostrserversContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridnostrserversContainer"+"DivS\" data-gxgridid=\"35\">") ;
            sStyleString = "";
            if ( subGridnostrservers_Visible == 0 )
            {
               sStyleString += "display:none;";
            }
            GxWebStd.gx_table_start( context, subGridnostrservers_Internalname, subGridnostrservers_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridnostrservers_Backcolorstyle == 0 )
            {
               subGridnostrservers_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridnostrservers_Class) > 0 )
               {
                  subGridnostrservers_Linesclass = subGridnostrservers_Class+"Title";
               }
            }
            else
            {
               subGridnostrservers_Titlebackstyle = 1;
               if ( subGridnostrservers_Backcolorstyle == 1 )
               {
                  subGridnostrservers_Titlebackcolor = subGridnostrservers_Allbackcolor;
                  if ( StringUtil.Len( subGridnostrservers_Class) > 0 )
                  {
                     subGridnostrservers_Linesclass = subGridnostrservers_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridnostrservers_Class) > 0 )
                  {
                     subGridnostrservers_Linesclass = subGridnostrservers_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Connection Type") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Host Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Port") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Secure") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Time Out (miliseconds)") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridnostrserversContainer.AddObjectProperty("GridName", "Gridnostrservers");
         }
         else
         {
            GridnostrserversContainer.AddObjectProperty("GridName", "Gridnostrservers");
            GridnostrserversContainer.AddObjectProperty("Header", subGridnostrservers_Header);
            GridnostrserversContainer.AddObjectProperty("Class", "Grid");
            GridnostrserversContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Backcolorstyle), 1, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Visible), 5, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("CmpContext", "");
            GridnostrserversContainer.AddObjectProperty("InMasterPage", "false");
            GridnostrserversColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridnostrserversColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbavCtlconnectiontype.Enabled), 5, 0, ".", "")));
            GridnostrserversContainer.AddColumnProperties(GridnostrserversColumn);
            GridnostrserversColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridnostrserversColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlhostname_Enabled), 5, 0, ".", "")));
            GridnostrserversContainer.AddColumnProperties(GridnostrserversColumn);
            GridnostrserversColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridnostrserversColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlport_Enabled), 5, 0, ".", "")));
            GridnostrserversContainer.AddColumnProperties(GridnostrserversColumn);
            GridnostrserversColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridnostrserversColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkavCtlsecure.Enabled), 5, 0, ".", "")));
            GridnostrserversContainer.AddColumnProperties(GridnostrserversColumn);
            GridnostrserversColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridnostrserversColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtltimeoutmiliseconds_Enabled), 5, 0, ".", "")));
            GridnostrserversContainer.AddColumnProperties(GridnostrserversColumn);
            GridnostrserversContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Selectedindex), 4, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Allowselection), 1, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Selectioncolor), 9, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Allowhovering), 1, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Hoveringcolor), 9, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Allowcollapsing), 1, 0, ".", "")));
            GridnostrserversContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridnostrservers_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttLoginwithsso_Internalname = "LOGINWITHSSO";
         bttLoginanonymously_Internalname = "LOGINANONYMOUSLY";
         bttLogout_Internalname = "LOGOUT";
         lblLoggedmessage_Internalname = "LOGGEDMESSAGE";
         edtavCtlusername_Internalname = "CTLUSERNAME";
         edtavOnerole_Internalname = "vONEROLE";
         lblTextblockoptional_Internalname = "TEXTBLOCKOPTIONAL";
         bttConfigureyournostrservers_Internalname = "CONFIGUREYOURNOSTRSERVERS";
         cmbavCtlconnectiontype_Internalname = "CTLCONNECTIONTYPE";
         edtavCtlhostname_Internalname = "CTLHOSTNAME";
         edtavCtlport_Internalname = "CTLPORT";
         chkavCtlsecure_Internalname = "CTLSECURE";
         edtavCtltimeoutmiliseconds_Internalname = "CTLTIMEOUTMILISECONDS";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGridnostrservers_Internalname = "GRIDNOSTRSERVERS";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridnostrservers_Allowcollapsing = 0;
         subGridnostrservers_Allowselection = 0;
         subGridnostrservers_Header = "";
         edtavCtltimeoutmiliseconds_Jsonclick = "";
         edtavCtltimeoutmiliseconds_Enabled = 0;
         chkavCtlsecure.Caption = "";
         chkavCtlsecure.Enabled = 0;
         edtavCtlport_Jsonclick = "";
         edtavCtlport_Enabled = 0;
         edtavCtlhostname_Jsonclick = "";
         edtavCtlhostname_Enabled = 0;
         cmbavCtlconnectiontype_Jsonclick = "";
         cmbavCtlconnectiontype.Enabled = 0;
         subGridnostrservers_Class = "Grid";
         subGridnostrservers_Backcolorstyle = 0;
         edtavCtltimeoutmiliseconds_Enabled = -1;
         chkavCtlsecure.Enabled = -1;
         edtavCtlport_Enabled = -1;
         edtavCtlhostname_Enabled = -1;
         cmbavCtlconnectiontype.Enabled = -1;
         edtavCtlusername_Enabled = -1;
         subGridnostrservers_Visible = 1;
         bttConfigureyournostrservers_Visible = 1;
         lblTextblockoptional_Visible = 1;
         edtavOnerole_Jsonclick = "";
         edtavOnerole_Enabled = 1;
         edtavOnerole_Visible = 1;
         edtavCtlusername_Jsonclick = "";
         edtavCtlusername_Enabled = 0;
         edtavCtlusername_Visible = 1;
         lblLoggedmessage_Visible = 1;
         bttLogout_Visible = 1;
         bttLoginanonymously_Visible = 1;
         bttLoginwithsso_Visible = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Dist Crypt Login";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDNOSTRSERVERS_nFirstRecordOnPage","type":"int"},{"av":"GRIDNOSTRSERVERS_nEOF","type":"int"},{"av":"AV42ConnectionParameters","fld":"vCONNECTIONPARAMETERS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"nRC_GXsfl_35","ctrl":"GRIDNOSTRSERVERS","prop":"GridRC","grid":35,"type":"int"}]}""");
         setEventMetadata("'LOGIN WITH SSO'","""{"handler":"E11131","iparms":[]}""");
         setEventMetadata("'LOGIN ANONYMOUSLY'","""{"handler":"E13132","iparms":[]}""");
         setEventMetadata("'LOGOUT'","""{"handler":"E14132","iparms":[]""");
         setEventMetadata("'LOGOUT'",""","oparms":[{"av":"AV6externalUser","fld":"vEXTERNALUSER","type":""}]}""");
         setEventMetadata("'CONFIGURE YOUR NOSTR SERVERS'","""{"handler":"E12131","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E15132","iparms":[{"av":"AV55PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV42ConnectionParameters","fld":"vCONNECTIONPARAMETERS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"GRIDNOSTRSERVERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_35","ctrl":"GRIDNOSTRSERVERS","prop":"GridRC","grid":35,"type":"int"},{"av":"GRIDNOSTRSERVERS_nEOF","type":"int"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV42ConnectionParameters","fld":"vCONNECTIONPARAMETERS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"GRIDNOSTRSERVERS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_35","ctrl":"GRIDNOSTRSERVERS","prop":"GridRC","grid":35,"type":"int"}]}""");
         setEventMetadata("VALIDV_GXV3","""{"handler":"Validv_Gxv3","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv7","iparms":[]}""");
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
         AV6externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV42ConnectionParameters = new GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem>( context, "ConnectionParametersItem", "distributedcryptography");
         AV55PopupName = "";
         AV5callerURL = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttLoginwithsso_Jsonclick = "";
         bttLoginanonymously_Jsonclick = "";
         bttLogout_Jsonclick = "";
         lblLoggedmessage_Jsonclick = "";
         AV39oneRole = "";
         lblTextblockoptional_Jsonclick = "";
         bttConfigureyournostrservers_Jsonclick = "";
         GridnostrserversContainer = new GXWebGrid( context);
         sStyleString = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV38role = "";
         AV37roles = new GxSimpleCollection<string>();
         AV8error = "";
         AV36WebSession = context.GetSession();
         AV61sendCoinPopupName = "";
         GXt_char2 = "";
         GridnostrserversRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridnostrservers_Linesclass = "";
         GXCCtl = "";
         ROClassString = "";
         GridnostrserversColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlusername_Enabled = 0;
         edtavOnerole_Enabled = 0;
         cmbavCtlconnectiontype.Enabled = 0;
         edtavCtlhostname_Enabled = 0;
         edtavCtlport_Enabled = 0;
         chkavCtlsecure.Enabled = 0;
         edtavCtltimeoutmiliseconds_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridnostrservers_Backcolorstyle ;
      private short AV63strFound ;
      private short GRIDNOSTRSERVERS_nEOF ;
      private short nGXWrapped ;
      private short subGridnostrservers_Backstyle ;
      private short subGridnostrservers_Titlebackstyle ;
      private short subGridnostrservers_Allowselection ;
      private short subGridnostrservers_Allowhovering ;
      private short subGridnostrservers_Allowcollapsing ;
      private short subGridnostrservers_Collapsed ;
      private int nRC_GXsfl_35 ;
      private int nGXsfl_35_idx=1 ;
      private int bttLoginwithsso_Visible ;
      private int bttLoginanonymously_Visible ;
      private int bttLogout_Visible ;
      private int lblLoggedmessage_Visible ;
      private int edtavCtlusername_Visible ;
      private int edtavCtlusername_Enabled ;
      private int edtavOnerole_Visible ;
      private int edtavOnerole_Enabled ;
      private int lblTextblockoptional_Visible ;
      private int bttConfigureyournostrservers_Visible ;
      private int AV65GXV2 ;
      private int subGridnostrservers_Visible ;
      private int subGridnostrservers_Islastpage ;
      private int edtavCtlhostname_Enabled ;
      private int edtavCtlport_Enabled ;
      private int edtavCtltimeoutmiliseconds_Enabled ;
      private int nGXsfl_35_fel_idx=1 ;
      private int AV71GXV8 ;
      private int nGXsfl_35_bak_idx=1 ;
      private int idxLst ;
      private int subGridnostrservers_Backcolor ;
      private int subGridnostrservers_Allbackcolor ;
      private int subGridnostrservers_Titlebackcolor ;
      private int subGridnostrservers_Selectedindex ;
      private int subGridnostrservers_Selectioncolor ;
      private int subGridnostrservers_Hoveringcolor ;
      private long GRIDNOSTRSERVERS_nCurrentRecord ;
      private long GRIDNOSTRSERVERS_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_35_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV55PopupName ;
      private string AV5callerURL ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttLoginwithsso_Internalname ;
      private string bttLoginwithsso_Jsonclick ;
      private string bttLoginanonymously_Internalname ;
      private string bttLoginanonymously_Jsonclick ;
      private string bttLogout_Internalname ;
      private string bttLogout_Jsonclick ;
      private string lblLoggedmessage_Internalname ;
      private string lblLoggedmessage_Jsonclick ;
      private string edtavCtlusername_Internalname ;
      private string edtavCtlusername_Jsonclick ;
      private string edtavOnerole_Internalname ;
      private string edtavOnerole_Jsonclick ;
      private string lblTextblockoptional_Internalname ;
      private string lblTextblockoptional_Jsonclick ;
      private string bttConfigureyournostrservers_Internalname ;
      private string bttConfigureyournostrservers_Jsonclick ;
      private string sStyleString ;
      private string subGridnostrservers_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string sGXsfl_35_fel_idx="0001" ;
      private string AV8error ;
      private string AV61sendCoinPopupName ;
      private string GXt_char2 ;
      private string cmbavCtlconnectiontype_Internalname ;
      private string edtavCtlhostname_Internalname ;
      private string edtavCtlport_Internalname ;
      private string chkavCtlsecure_Internalname ;
      private string edtavCtltimeoutmiliseconds_Internalname ;
      private string subGridnostrservers_Class ;
      private string subGridnostrservers_Linesclass ;
      private string GXCCtl ;
      private string cmbavCtlconnectiontype_Jsonclick ;
      private string ROClassString ;
      private string edtavCtlhostname_Jsonclick ;
      private string edtavCtlport_Jsonclick ;
      private string edtavCtltimeoutmiliseconds_Jsonclick ;
      private string subGridnostrservers_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_35_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV35 ;
      private string AV39oneRole ;
      private string AV38role ;
      private GXWebGrid GridnostrserversContainer ;
      private GXWebRow GridnostrserversRow ;
      private GXWebColumn GridnostrserversColumn ;
      private IGxSession AV36WebSession ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavCtlconnectiontype ;
      private GXCheckbox chkavCtlsecure ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV6externalUser ;
      private GXBaseCollection<GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem> AV42ConnectionParameters ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GxSimpleCollection<string> AV37roles ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
