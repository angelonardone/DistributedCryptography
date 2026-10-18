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
   public class walletconfig : GXDataArea
   {
      public walletconfig( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public walletconfig( IGxContext context )
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
         chkavCtlserversecure = new GXCheckbox();
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
         PA3J2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3J2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.walletconfig") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Electrumconfig", AV5electrumConfig);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Electrumconfig", AV5electrumConfig);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Serverparameters", AV8serverParameters);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Serverparameters", AV8serverParameters);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vELECTRUMCONFIG", AV5electrumConfig);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vELECTRUMCONFIG", AV5electrumConfig);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSERVERPARAMETERS", AV8serverParameters);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSERVERPARAMETERS", AV8serverParameters);
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
            WE3J2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3J2( ) ;
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
         return formatLink("wallet.walletconfig")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.WalletConfig" ;
      }

      public override string GetPgmdesc( )
      {
         return "Config" ;
      }

      protected void WB3J0( )
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
            GxWebStd.gx_label_ctrl( context, lblTbhelp_Internalname, lblTbhelp_Caption, "", "", lblTbhelp_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Control Group */
            GxWebStd.gx_group_start( context, grpGroupelectrum_Internalname, grpGroupelectrum_Caption, grpGroupelectrum_Visible, 0, "px", 0, "px", "Group", "", "HLP_Wallet/WalletConfig.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGroupelectrumtable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavCtlconnectiontype_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavCtlconnectiontype_Internalname, "Connection Type", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavCtlconnectiontype, cmbavCtlconnectiontype_Internalname, StringUtil.RTrim( AV5electrumConfig.gxTpr_Connectiontype), 1, cmbavCtlconnectiontype_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavCtlconnectiontype.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,15);\"", "", true, 0, "HLP_Wallet/WalletConfig.htm");
            cmbavCtlconnectiontype.CurrentValue = StringUtil.RTrim( AV5electrumConfig.gxTpr_Connectiontype);
            AssignProp("", false, cmbavCtlconnectiontype_Internalname, "Values", (string)(cmbavCtlconnectiontype.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavElectrumhost_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavElectrumhost_Internalname, "Host Name", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavElectrumhost_Internalname, AV10electrumHost, StringUtil.RTrim( context.localUtil.Format( AV10electrumHost, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,20);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavElectrumhost_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavElectrumhost_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlport_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlport_Internalname, "Port", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlport_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV5electrumConfig.gxTpr_Port), 6, 0, ".", "")), StringUtil.LTrim( ((edtavCtlport_Enabled!=0) ? context.localUtil.Format( (decimal)(AV5electrumConfig.gxTpr_Port), "ZZZZZ9") : context.localUtil.Format( (decimal)(AV5electrumConfig.gxTpr_Port), "ZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,25);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlport_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlport_Enabled, 0, "text", "1", 6, "chr", 1, "row", 6, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavCtlsecure_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavCtlsecure_Internalname, "Secure (TLS)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 30,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavCtlsecure_Internalname, StringUtil.BoolToStr( AV5electrumConfig.gxTpr_Secure), "", "Secure (TLS)", 1, chkavCtlsecure.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(30, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,30);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtltimeoutmiliseconds_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtltimeoutmiliseconds_Internalname, "Time Out (miliseconds)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtltimeoutmiliseconds_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV5electrumConfig.gxTpr_Timeoutmiliseconds), 6, 0, ".", "")), StringUtil.LTrim( ((edtavCtltimeoutmiliseconds_Enabled!=0) ? context.localUtil.Format( (decimal)(AV5electrumConfig.gxTpr_Timeoutmiliseconds), "ZZZZZ9") : context.localUtil.Format( (decimal)(AV5electrumConfig.gxTpr_Timeoutmiliseconds), "ZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,35);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtltimeoutmiliseconds_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtltimeoutmiliseconds_Enabled, 0, "text", "1", 6, "chr", 1, "row", 6, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSaveelectrum_Internalname, "", "Save Electrum server", bttSaveelectrum_Jsonclick, 5, "Save Electrum server", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'SAVE ELECTRUM SERVER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttDefaultelectrum_Internalname, "", "Restore default", bttDefaultelectrum_Jsonclick, 5, "Restore default", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DEFAULT ELECTRUM SERVER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</fieldset>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Control Group */
            GxWebStd.gx_group_start( context, grpGroupserver_Internalname, grpGroupserver_Caption, grpGroupserver_Visible, 0, "px", 0, "px", "Group", "", "HLP_Wallet/WalletConfig.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGroupservertable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlserverhost_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlserverhost_Internalname, "Host Name", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 49,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlserverhost_Internalname, AV8serverParameters.gxTpr_Serverhost, StringUtil.RTrim( context.localUtil.Format( AV8serverParameters.gxTpr_Serverhost, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,49);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlserverhost_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlserverhost_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlserverport_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlserverport_Internalname, "Port", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 54,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlserverport_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV8serverParameters.gxTpr_Serverport), 6, 0, ".", "")), StringUtil.LTrim( ((edtavCtlserverport_Enabled!=0) ? context.localUtil.Format( (decimal)(AV8serverParameters.gxTpr_Serverport), "ZZZZZ9") : context.localUtil.Format( (decimal)(AV8serverParameters.gxTpr_Serverport), "ZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,54);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlserverport_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlserverport_Enabled, 0, "text", "1", 6, "chr", 1, "row", 6, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavCtlserversecure_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavCtlserversecure_Internalname, "Secure (https)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 59,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavCtlserversecure_Internalname, StringUtil.BoolToStr( AV8serverParameters.gxTpr_Serversecure), "", "Secure (https)", 1, chkavCtlserversecure.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(59, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,59);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 62,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSaveserver_Internalname, "", "Save login server", bttSaveserver_Jsonclick, 5, "Save login server", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'SAVE LOGIN SERVER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 64,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttDefaultserver_Internalname, "", "Restore default", bttDefaultserver_Jsonclick, 5, "Restore default", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DEFAULT LOGIN SERVER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbservermsg_Internalname, lblTbservermsg_Caption, "", "", lblTbservermsg_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextblockMedium", 0, "", 1, 1, 0, 0, "HLP_Wallet/WalletConfig.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</fieldset>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START3J2( )
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
         Form.Meta.addItem("description", "Config", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3J0( ) ;
      }

      protected void WS3J2( )
      {
         START3J2( ) ;
         EVT3J2( ) ;
      }

      protected void EVT3J2( )
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
                              E113J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE ELECTRUM SERVER'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Save Electrum server' */
                              E123J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DEFAULT ELECTRUM SERVER'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Default Electrum server' */
                              E133J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE LOGIN SERVER'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Save login server' */
                              E143J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DEFAULT LOGIN SERVER'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Default login server' */
                              E153J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E163J2 ();
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

      protected void WE3J2( )
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

      protected void PA3J2( )
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
               GX_FocusControl = cmbavCtlconnectiontype_Internalname;
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
         if ( cmbavCtlconnectiontype.ItemCount > 0 )
         {
            AV5electrumConfig.gxTpr_Connectiontype = cmbavCtlconnectiontype.getValidValue(AV5electrumConfig.gxTpr_Connectiontype);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavCtlconnectiontype.CurrentValue = StringUtil.RTrim( AV5electrumConfig.gxTpr_Connectiontype);
            AssignProp("", false, cmbavCtlconnectiontype_Internalname, "Values", cmbavCtlconnectiontype.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3J2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF3J2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E163J2 ();
            WB3J0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3J2( )
      {
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3J0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E113J2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vELECTRUMCONFIG"), AV5electrumConfig);
            ajax_req_read_hidden_sdt(cgiGet( "vSERVERPARAMETERS"), AV8serverParameters);
            ajax_req_read_hidden_sdt(cgiGet( "Electrumconfig"), AV5electrumConfig);
            ajax_req_read_hidden_sdt(cgiGet( "Serverparameters"), AV8serverParameters);
            /* Read saved values. */
            /* Read variables values. */
            cmbavCtlconnectiontype.CurrentValue = cgiGet( cmbavCtlconnectiontype_Internalname);
            AV5electrumConfig.gxTpr_Connectiontype = cgiGet( cmbavCtlconnectiontype_Internalname);
            AV10electrumHost = cgiGet( edtavElectrumhost_Internalname);
            AssignAttri("", false, "AV10electrumHost", AV10electrumHost);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtlport_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtlport_Internalname), ".", ",") > Convert.ToDecimal( 999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLPORT");
               GX_FocusControl = edtavCtlport_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV5electrumConfig.gxTpr_Port = 0;
            }
            else
            {
               AV5electrumConfig.gxTpr_Port = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtlport_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            AV5electrumConfig.gxTpr_Secure = StringUtil.StrToBool( cgiGet( chkavCtlsecure_Internalname));
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtltimeoutmiliseconds_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtltimeoutmiliseconds_Internalname), ".", ",") > Convert.ToDecimal( 999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLTIMEOUTMILISECONDS");
               GX_FocusControl = edtavCtltimeoutmiliseconds_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV5electrumConfig.gxTpr_Timeoutmiliseconds = 0;
            }
            else
            {
               AV5electrumConfig.gxTpr_Timeoutmiliseconds = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtltimeoutmiliseconds_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            AV8serverParameters.gxTpr_Serverhost = cgiGet( edtavCtlserverhost_Internalname);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtlserverport_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtlserverport_Internalname), ".", ",") > Convert.ToDecimal( 999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLSERVERPORT");
               GX_FocusControl = edtavCtlserverport_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV8serverParameters.gxTpr_Serverport = 0;
            }
            else
            {
               AV8serverParameters.gxTpr_Serverport = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtlserverport_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            AV8serverParameters.gxTpr_Serversecure = StringUtil.StrToBool( cgiGet( chkavCtlserversecure_Internalname));
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
         E113J2 ();
         if (returnInSub) return;
      }

      protected void E113J2( )
      {
         /* Start Routine */
         returnInSub = false;
         if ( new GeneXus.Programs.wallet.iswalletopen(context).executeUdp( ) )
         {
            GXt_SdtWalletInfo1 = AV9walletInfo;
            new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
            AV9walletInfo = GXt_SdtWalletInfo1;
            grpGroupelectrum_Caption = "Electrum server ("+StringUtil.Trim( AV9walletInfo.gxTpr_Networktype)+")";
            AssignProp("", false, grpGroupelectrum_Internalname, "Caption", grpGroupelectrum_Caption, true);
            GXt_SdtConnectionParameters_ConnectionParametersItem2 = AV5electrumConfig;
            new GeneXus.Programs.electrum.getelectrumconfigfornetworktype(context ).execute( out  GXt_SdtConnectionParameters_ConnectionParametersItem2) ;
            AV5electrumConfig = GXt_SdtConnectionParameters_ConnectionParametersItem2;
            AV10electrumHost = StringUtil.Trim( AV5electrumConfig.gxTpr_Hostname);
            AssignAttri("", false, "AV10electrumHost", AV10electrumHost);
            if ( StringUtil.StrCmp(AV9walletInfo.gxTpr_Wallettype, "BIP86") == 0 )
            {
               grpGroupserver_Caption = "Login server ("+StringUtil.Trim( AV9walletInfo.gxTpr_Networktype)+")";
               AssignProp("", false, grpGroupserver_Internalname, "Caption", grpGroupserver_Caption, true);
               GXt_SdtServerParameters3 = AV8serverParameters;
               new GeneXus.Programs.distcrypt.getserverparameters(context ).execute( out  GXt_SdtServerParameters3) ;
               AV8serverParameters = GXt_SdtServerParameters3;
            }
            else
            {
               grpGroupserver_Visible = 0;
               AssignProp("", false, grpGroupserver_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(grpGroupserver_Visible), 5, 0), true);
            }
         }
         else
         {
            lblTbhelp_Caption = "Open a wallet first";
            AssignProp("", false, lblTbhelp_Internalname, "Caption", lblTbhelp_Caption, true);
            grpGroupelectrum_Visible = 0;
            AssignProp("", false, grpGroupelectrum_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(grpGroupelectrum_Visible), 5, 0), true);
            grpGroupserver_Visible = 0;
            AssignProp("", false, grpGroupserver_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(grpGroupserver_Visible), 5, 0), true);
         }
      }

      protected void E123J2( )
      {
         /* 'Save Electrum server' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'SAVE ELECTRUM' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5electrumConfig", AV5electrumConfig);
      }

      protected void E133J2( )
      {
         /* 'Default Electrum server' Routine */
         returnInSub = false;
         GXt_SdtConnectionParameters_ConnectionParametersItem2 = AV5electrumConfig;
         new GeneXus.Programs.electrum.getdefaultelectrumconfig(context ).execute( out  GXt_SdtConnectionParameters_ConnectionParametersItem2) ;
         AV5electrumConfig = GXt_SdtConnectionParameters_ConnectionParametersItem2;
         AV10electrumHost = StringUtil.Trim( AV5electrumConfig.gxTpr_Hostname);
         AssignAttri("", false, "AV10electrumHost", AV10electrumHost);
         /* Execute user subroutine: 'SAVE ELECTRUM' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5electrumConfig", AV5electrumConfig);
      }

      protected void E143J2( )
      {
         /* 'Save login server' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'SAVE SERVER' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E153J2( )
      {
         /* 'Default login server' Routine */
         returnInSub = false;
         GXt_SdtServerParameters3 = AV8serverParameters;
         new GeneXus.Programs.distcrypt.getdefaultserverparameters(context ).execute( out  GXt_SdtServerParameters3) ;
         AV8serverParameters = GXt_SdtServerParameters3;
         /* Execute user subroutine: 'SAVE SERVER' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV8serverParameters", AV8serverParameters);
      }

      protected void S112( )
      {
         /* 'SAVE ELECTRUM' Routine */
         returnInSub = false;
         AV5electrumConfig.gxTpr_Hostname = StringUtil.Trim( AV10electrumHost);
         GXt_char4 = AV6error;
         new GeneXus.Programs.electrum.saveelectrumconfig(context ).execute(  AV5electrumConfig, out  GXt_char4) ;
         AV6error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6error)) )
         {
            GXt_char4 = AV6error;
            new GeneXus.Programs.electrum.connect(context ).execute( out  GXt_char4) ;
            AV6error = GXt_char4;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6error)) )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Electrum server saved",(string)"Connected"}, true);
            }
            else
            {
               GX_msglist.addItem("The Electrum server was saved, but we cannot connect with it: "+AV6error);
            }
         }
         else
         {
            GX_msglist.addItem(AV6error);
         }
      }

      protected void S122( )
      {
         /* 'SAVE SERVER' Routine */
         returnInSub = false;
         new GeneXus.Programs.distcrypt.saveserverparameters(context ).execute(  AV8serverParameters, out  AV7loggedOut, out  AV6error) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6error)) )
         {
            if ( AV7loggedOut )
            {
               lblTbservermsg_Caption = "Login server saved. You were logged out: log in again to use the new server.";
               AssignProp("", false, lblTbservermsg_Internalname, "Caption", lblTbservermsg_Caption, true);
               this.executeExternalObjectMethod("", false, "GlobalEvents", "RefreshMasterPage", new Object[] {}, true);
            }
            else
            {
               lblTbservermsg_Caption = "";
               AssignProp("", false, lblTbservermsg_Internalname, "Caption", lblTbservermsg_Caption, true);
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Login server saved",(string)""}, true);
            }
         }
         else
         {
            GX_msglist.addItem(AV6error);
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E163J2( )
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
         PA3J2( ) ;
         WS3J2( ) ;
         WE3J2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610161784866", true, true, false);
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
         context.AddJavascriptSource("wallet/walletconfig.js", "?202610161784866", false, true, false);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         cmbavCtlconnectiontype.Name = "CTLCONNECTIONTYPE";
         cmbavCtlconnectiontype.WebTags = "";
         cmbavCtlconnectiontype.addItem("ws", "Web Socket", 0);
         cmbavCtlconnectiontype.addItem("tcp", "Tcp Socket", 0);
         if ( cmbavCtlconnectiontype.ItemCount > 0 )
         {
            AV5electrumConfig.gxTpr_Connectiontype = cmbavCtlconnectiontype.getValidValue(AV5electrumConfig.gxTpr_Connectiontype);
         }
         chkavCtlsecure.Name = "CTLSECURE";
         chkavCtlsecure.WebTags = "";
         chkavCtlsecure.Caption = "Secure (TLS)";
         AssignProp("", false, chkavCtlsecure_Internalname, "TitleCaption", chkavCtlsecure.Caption, true);
         chkavCtlsecure.CheckedValue = "false";
         chkavCtlserversecure.Name = "CTLSERVERSECURE";
         chkavCtlserversecure.WebTags = "";
         chkavCtlserversecure.Caption = "Secure (https)";
         AssignProp("", false, chkavCtlserversecure_Internalname, "TitleCaption", chkavCtlserversecure.Caption, true);
         chkavCtlserversecure.CheckedValue = "false";
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         lblTbhelp_Internalname = "TBHELP";
         cmbavCtlconnectiontype_Internalname = "CTLCONNECTIONTYPE";
         edtavElectrumhost_Internalname = "vELECTRUMHOST";
         edtavCtlport_Internalname = "CTLPORT";
         chkavCtlsecure_Internalname = "CTLSECURE";
         edtavCtltimeoutmiliseconds_Internalname = "CTLTIMEOUTMILISECONDS";
         bttSaveelectrum_Internalname = "SAVEELECTRUM";
         bttDefaultelectrum_Internalname = "DEFAULTELECTRUM";
         divGroupelectrumtable_Internalname = "GROUPELECTRUMTABLE";
         grpGroupelectrum_Internalname = "GROUPELECTRUM";
         edtavCtlserverhost_Internalname = "CTLSERVERHOST";
         edtavCtlserverport_Internalname = "CTLSERVERPORT";
         chkavCtlserversecure_Internalname = "CTLSERVERSECURE";
         bttSaveserver_Internalname = "SAVESERVER";
         bttDefaultserver_Internalname = "DEFAULTSERVER";
         lblTbservermsg_Internalname = "TBSERVERMSG";
         divGroupservertable_Internalname = "GROUPSERVERTABLE";
         grpGroupserver_Internalname = "GROUPSERVER";
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
         chkavCtlserversecure.Caption = "Secure (https)";
         chkavCtlsecure.Caption = "Secure (TLS)";
         lblTbservermsg_Caption = "";
         chkavCtlserversecure.Enabled = 1;
         edtavCtlserverport_Jsonclick = "";
         edtavCtlserverport_Enabled = 1;
         edtavCtlserverhost_Jsonclick = "";
         edtavCtlserverhost_Enabled = 1;
         grpGroupserver_Visible = 1;
         grpGroupserver_Caption = "Login server";
         edtavCtltimeoutmiliseconds_Jsonclick = "";
         edtavCtltimeoutmiliseconds_Enabled = 1;
         chkavCtlsecure.Enabled = 1;
         edtavCtlport_Jsonclick = "";
         edtavCtlport_Enabled = 1;
         edtavElectrumhost_Jsonclick = "";
         edtavElectrumhost_Enabled = 1;
         cmbavCtlconnectiontype_Jsonclick = "";
         cmbavCtlconnectiontype.Enabled = 1;
         grpGroupelectrum_Visible = 1;
         grpGroupelectrum_Caption = "Electrum server";
         lblTbhelp_Caption = "Servers used by this wallet. They are stored encrypted inside the wallet.";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Config";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GXV3","fld":"CTLSECURE","type":"boolean"},{"av":"GXV7","fld":"CTLSERVERSECURE","type":"boolean"}]}""");
         setEventMetadata("'SAVE ELECTRUM SERVER'","""{"handler":"E123J2","iparms":[{"av":"AV10electrumHost","fld":"vELECTRUMHOST","type":"svchar"},{"av":"AV5electrumConfig","fld":"vELECTRUMCONFIG","type":""}]""");
         setEventMetadata("'SAVE ELECTRUM SERVER'",""","oparms":[{"av":"AV5electrumConfig","fld":"vELECTRUMCONFIG","type":""}]}""");
         setEventMetadata("'DEFAULT ELECTRUM SERVER'","""{"handler":"E133J2","iparms":[{"av":"AV10electrumHost","fld":"vELECTRUMHOST","type":"svchar"},{"av":"AV5electrumConfig","fld":"vELECTRUMCONFIG","type":""}]""");
         setEventMetadata("'DEFAULT ELECTRUM SERVER'",""","oparms":[{"av":"AV5electrumConfig","fld":"vELECTRUMCONFIG","type":""},{"av":"AV10electrumHost","fld":"vELECTRUMHOST","type":"svchar"}]}""");
         setEventMetadata("'SAVE LOGIN SERVER'","""{"handler":"E143J2","iparms":[{"av":"AV8serverParameters","fld":"vSERVERPARAMETERS","type":""}]""");
         setEventMetadata("'SAVE LOGIN SERVER'",""","oparms":[{"av":"lblTbservermsg_Caption","ctrl":"TBSERVERMSG","prop":"Caption"}]}""");
         setEventMetadata("'DEFAULT LOGIN SERVER'","""{"handler":"E153J2","iparms":[{"av":"AV8serverParameters","fld":"vSERVERPARAMETERS","type":""}]""");
         setEventMetadata("'DEFAULT LOGIN SERVER'",""","oparms":[{"av":"AV8serverParameters","fld":"vSERVERPARAMETERS","type":""},{"av":"lblTbservermsg_Caption","ctrl":"TBSERVERMSG","prop":"Caption"}]}""");
         setEventMetadata("VALIDV_GXV1","""{"handler":"Validv_Gxv1","iparms":[]}""");
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
         AV5electrumConfig = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         AV8serverParameters = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         lblTbhelp_Jsonclick = "";
         TempTags = "";
         AV10electrumHost = "";
         ClassString = "";
         StyleString = "";
         bttSaveelectrum_Jsonclick = "";
         bttDefaultelectrum_Jsonclick = "";
         bttSaveserver_Jsonclick = "";
         bttDefaultserver_Jsonclick = "";
         lblTbservermsg_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV9walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtConnectionParameters_ConnectionParametersItem2 = new GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem(context);
         GXt_SdtServerParameters3 = new GeneXus.Programs.distcrypt.SdtServerParameters(context);
         AV6error = "";
         GXt_char4 = "";
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
      private short nGXWrapped ;
      private int grpGroupelectrum_Visible ;
      private int edtavElectrumhost_Enabled ;
      private int edtavCtlport_Enabled ;
      private int edtavCtltimeoutmiliseconds_Enabled ;
      private int grpGroupserver_Visible ;
      private int edtavCtlserverhost_Enabled ;
      private int edtavCtlserverport_Enabled ;
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
      private string lblTbhelp_Internalname ;
      private string lblTbhelp_Caption ;
      private string lblTbhelp_Jsonclick ;
      private string grpGroupelectrum_Internalname ;
      private string grpGroupelectrum_Caption ;
      private string divGroupelectrumtable_Internalname ;
      private string cmbavCtlconnectiontype_Internalname ;
      private string TempTags ;
      private string cmbavCtlconnectiontype_Jsonclick ;
      private string edtavElectrumhost_Internalname ;
      private string edtavElectrumhost_Jsonclick ;
      private string edtavCtlport_Internalname ;
      private string edtavCtlport_Jsonclick ;
      private string chkavCtlsecure_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string edtavCtltimeoutmiliseconds_Internalname ;
      private string edtavCtltimeoutmiliseconds_Jsonclick ;
      private string bttSaveelectrum_Internalname ;
      private string bttSaveelectrum_Jsonclick ;
      private string bttDefaultelectrum_Internalname ;
      private string bttDefaultelectrum_Jsonclick ;
      private string grpGroupserver_Internalname ;
      private string grpGroupserver_Caption ;
      private string divGroupservertable_Internalname ;
      private string edtavCtlserverhost_Internalname ;
      private string edtavCtlserverhost_Jsonclick ;
      private string edtavCtlserverport_Internalname ;
      private string edtavCtlserverport_Jsonclick ;
      private string chkavCtlserversecure_Internalname ;
      private string bttSaveserver_Internalname ;
      private string bttSaveserver_Jsonclick ;
      private string bttDefaultserver_Internalname ;
      private string bttDefaultserver_Jsonclick ;
      private string lblTbservermsg_Internalname ;
      private string lblTbservermsg_Caption ;
      private string lblTbservermsg_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV6error ;
      private string GXt_char4 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV7loggedOut ;
      private string AV10electrumHost ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavCtlconnectiontype ;
      private GXCheckbox chkavCtlsecure ;
      private GXCheckbox chkavCtlserversecure ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem AV5electrumConfig ;
      private GeneXus.Programs.distcrypt.SdtServerParameters AV8serverParameters ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV9walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private GeneXus.Programs.electrum.SdtConnectionParameters_ConnectionParametersItem GXt_SdtConnectionParameters_ConnectionParametersItem2 ;
      private GeneXus.Programs.distcrypt.SdtServerParameters GXt_SdtServerParameters3 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
