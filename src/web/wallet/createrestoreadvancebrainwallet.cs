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
   public class createrestoreadvancebrainwallet : GXDataArea
   {
      public createrestoreadvancebrainwallet( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public createrestoreadvancebrainwallet( IGxContext context )
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
         PA2E2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START2E2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.createrestoreadvancebrainwallet") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTKEYCREATE", AV11extKeyCreate);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTKEYCREATE", AV11extKeyCreate);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV26wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV26wallet);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTENDESECRETANDAUTHENTICATOR", AV10extendeSecretAndAuthenticator);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTENDESECRETANDAUTHENTICATOR", AV10extendeSecretAndAuthenticator);
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
            WE2E2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT2E2( ) ;
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
         return formatLink("wallet.createrestoreadvancebrainwallet")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.CreateRestoreAdvanceBrainWallet" ;
      }

      public override string GetPgmdesc( )
      {
         return "Create Restore Advance Brain Wallet" ;
      }

      protected void WB2E0( )
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
            GxWebStd.gx_label_ctrl( context, lblWarning_Internalname, lblWarning_Caption, "", "", lblWarning_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavWalletname_Internalname, StringUtil.RTrim( AV27walletName), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,11);\"", 0, 1, edtavWalletname_Enabled, 0, 80, "chr", 3, "row", 0, StyleString, ClassString, "", "", "200", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavNetworktype, cmbavNetworktype_Internalname, StringUtil.RTrim( AV18networkType), 1, cmbavNetworktype_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavNetworktype.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,16);\"", "", true, 0, "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            cmbavNetworktype.CurrentValue = StringUtil.RTrim( AV18networkType);
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
            GxWebStd.gx_label_element( context, edtavNewpass_Internalname, "Password / Phrase (the longer the better)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNewpass_Internalname, StringUtil.RTrim( AV19newPass), StringUtil.RTrim( context.localUtil.Format( AV19newPass, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,21);\""+" "+"data-gx-password-reveal"+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNewpass_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNewpass_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
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
            GxWebStd.gx_label_element( context, edtavNewpassconfirm_Internalname, "Confirm Password / Phrase", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNewpassconfirm_Internalname, StringUtil.RTrim( AV20newPassConfirm), StringUtil.RTrim( context.localUtil.Format( AV20newPassConfirm, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,26);\""+" "+"data-gx-password-reveal"+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNewpassconfirm_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNewpassconfirm_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Control Group */
            GxWebStd.gx_group_start( context, grpGroup1_Internalname, "Select a Memorable Date and number", 1, 0, "px", 0, "px", "Group", "", "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGroup1table_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTable1_Internalname, 1, 100, "%", 0, "px", "Table", "start", "top", " "+"data-gx-smarttable"+" ", "grid-template-columns:33fr 33fr 34fr;grid-template-rows:auto;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-smarttable-cell"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group gx-label-top", "start", "top", ""+" data-gx-for=\""+edtavYear_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavYear_Internalname, "Year", "gx-form-item AttributeLabel", 1, true, "width: 100%;");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 100, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavYear_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV30year), 4, 0, ".", "")), StringUtil.LTrim( ((edtavYear_Enabled!=0) ? context.localUtil.Format( (decimal)(AV30year), "ZZZ9") : context.localUtil.Format( (decimal)(AV30year), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,37);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavYear_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavYear_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-smarttable-cell"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group gx-label-top", "start", "top", ""+" data-gx-for=\""+edtavMonth_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMonth_Internalname, "Month", "gx-form-item AttributeLabel", 1, true, "width: 100%;");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 100, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 41,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMonth_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV31month), 4, 0, ".", "")), StringUtil.LTrim( ((edtavMonth_Enabled!=0) ? context.localUtil.Format( (decimal)(AV31month), "ZZZ9") : context.localUtil.Format( (decimal)(AV31month), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,41);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMonth_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavMonth_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-smarttable-cell"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group gx-label-top", "start", "top", ""+" data-gx-for=\""+edtavDay_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDay_Internalname, "Day", "gx-form-item AttributeLabel", 1, true, "width: 100%;");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 100, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 45,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDay_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV34day), 4, 0, ".", "")), StringUtil.LTrim( ((edtavDay_Enabled!=0) ? context.localUtil.Format( (decimal)(AV34day), "ZZZ9") : context.localUtil.Format( (decimal)(AV34day), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,45);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDay_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDay_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavChosenpin_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavChosenpin_Internalname, "Number (the longer the better)", "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 50,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavChosenpin_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV33chosenPIN), 10, 0, ".", "")), StringUtil.LTrim( ((edtavChosenpin_Enabled!=0) ? context.localUtil.Format( (decimal)(AV33chosenPIN), "ZZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV33chosenPIN), "ZZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,50);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavChosenpin_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavChosenpin_Enabled, 0, "text", "1", 10, "chr", 1, "row", 10, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</fieldset>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 100, "px", "col-xs-12 col-sm-6", "start", "Bottom", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 53,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreatewallet_Internalname, "", "Create Wallet", bttCreatewallet_Jsonclick, 5, "Create Wallet", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CREATE WALLET\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            GxWebStd.gx_div_end( context, "start", "Bottom", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 100, "px", "col-xs-12 col-sm-6", "end", "Bottom", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 55,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "", "Cancel and close", bttCancel_Jsonclick, 5, "Cancel and close", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL AND CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/CreateRestoreAdvanceBrainWallet.htm");
            GxWebStd.gx_div_end( context, "end", "Bottom", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START2E2( )
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
         Form.Meta.addItem("description", "Create Restore Advance Brain Wallet", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP2E0( ) ;
      }

      protected void WS2E2( )
      {
         START2E2( ) ;
         EVT2E2( ) ;
      }

      protected void EVT2E2( )
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
                              E112E2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CREATE WALLET'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Create Wallet' */
                              E122E2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL AND CLOSE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel and close' */
                              E132E2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E142E2 ();
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

      protected void WE2E2( )
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

      protected void PA2E2( )
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
            AV18networkType = cmbavNetworktype.getValidValue(AV18networkType);
            AssignAttri("", false, "AV18networkType", AV18networkType);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavNetworktype.CurrentValue = StringUtil.RTrim( AV18networkType);
            AssignProp("", false, cmbavNetworktype_Internalname, "Values", cmbavNetworktype.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF2E2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF2E2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E142E2 ();
            WB2E0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes2E2( )
      {
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2E0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E112E2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            AV27walletName = cgiGet( edtavWalletname_Internalname);
            AssignAttri("", false, "AV27walletName", AV27walletName);
            cmbavNetworktype.CurrentValue = cgiGet( cmbavNetworktype_Internalname);
            AV18networkType = cgiGet( cmbavNetworktype_Internalname);
            AssignAttri("", false, "AV18networkType", AV18networkType);
            AV19newPass = cgiGet( edtavNewpass_Internalname);
            AssignAttri("", false, "AV19newPass", AV19newPass);
            AV20newPassConfirm = cgiGet( edtavNewpassconfirm_Internalname);
            AssignAttri("", false, "AV20newPassConfirm", AV20newPassConfirm);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavYear_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavYear_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vYEAR");
               GX_FocusControl = edtavYear_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV30year = 0;
               AssignAttri("", false, "AV30year", StringUtil.LTrimStr( (decimal)(AV30year), 4, 0));
            }
            else
            {
               AV30year = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavYear_Internalname), ".", ","), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV30year", StringUtil.LTrimStr( (decimal)(AV30year), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavMonth_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavMonth_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vMONTH");
               GX_FocusControl = edtavMonth_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV31month = 0;
               AssignAttri("", false, "AV31month", StringUtil.LTrimStr( (decimal)(AV31month), 4, 0));
            }
            else
            {
               AV31month = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavMonth_Internalname), ".", ","), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV31month", StringUtil.LTrimStr( (decimal)(AV31month), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavDay_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavDay_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vDAY");
               GX_FocusControl = edtavDay_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV34day = 0;
               AssignAttri("", false, "AV34day", StringUtil.LTrimStr( (decimal)(AV34day), 4, 0));
            }
            else
            {
               AV34day = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavDay_Internalname), ".", ","), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV34day", StringUtil.LTrimStr( (decimal)(AV34day), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavChosenpin_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavChosenpin_Internalname), ".", ",") > Convert.ToDecimal( 9999999999L )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vCHOSENPIN");
               GX_FocusControl = edtavChosenpin_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV33chosenPIN = 0;
               AssignAttri("", false, "AV33chosenPIN", StringUtil.LTrimStr( (decimal)(AV33chosenPIN), 10, 0));
            }
            else
            {
               AV33chosenPIN = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavChosenpin_Internalname), ".", ","), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV33chosenPIN", StringUtil.LTrimStr( (decimal)(AV33chosenPIN), 10, 0));
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
         E112E2 ();
         if (returnInSub) return;
      }

      protected void E112E2( )
      {
         /* Start Routine */
         returnInSub = false;
         lblWarning_Caption = "We <b>strongly</b> advise against relying solely on memorization (like a 'brain wallet') for security.</br>"+"Why? Because the phrases people can easily remember are often not random enough, making them easier for attackers to guess compared to truly random keys.</br></br>"+"That said, sometimes you need to rely only on your memory, without saving your <b>KEYs</b> anywhere else.</br></br>"+"To address this, we've engineered a method using advanced security technology.</br>"+"This process takes your chosen phrase, a memorable date and number, then mathematically transforms it into a much more secure key, providing strong protection while still originating from your memory.</br></br>"+"Furthermore, we've included a <b>novel</b> backup mechanism to help you regain access should you ever forget your memorized information, offering extra security and peace of mind.</br></br></br>";
         AssignProp("", false, lblWarning_Internalname, "Caption", lblWarning_Caption, true);
      }

      protected void E122E2( )
      {
         /* 'Create Wallet' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV27walletName)) )
         {
            GX_msglist.addItem("The Wallet Name is mandatory");
         }
         else
         {
            if ( GxRegex.IsMatch(AV27walletName,"[~#&@*\\\\[\\]{}/:?\\“+%]+") )
            {
               GX_msglist.addItem("The following characters are not allowed on the name: ~#&@*\\\\[\\]{}/:?\\“+%");
            }
            else
            {
               if ( GxRegex.IsMatch(AV27walletName,"^[ ]") )
               {
                  GX_msglist.addItem("Spaces at the beginning of the name are not allowed");
               }
               else
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV19newPass)) )
                  {
                     GX_msglist.addItem("The password/phrase cannot be empty");
                  }
                  else
                  {
                     if ( ! ( StringUtil.StrCmp(AV19newPass, AV20newPassConfirm) == 0 ) )
                     {
                        GX_msglist.addItem("The password/phrase and password/phrase confirm are different, please make sure they are the same");
                     }
                     else
                     {
                        if ( (0==AV30year) || (0==AV31month) || (0==AV34day) || (0==AV33chosenPIN) )
                        {
                           GX_msglist.addItem("The Memorable Date or number cannot be empty");
                        }
                        else
                        {
                           /* Execute user subroutine: 'CREATE WALLET' */
                           S112 ();
                           if (returnInSub) return;
                        }
                     }
                  }
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11extKeyCreate", AV11extKeyCreate);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV26wallet", AV26wallet);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV10extendeSecretAndAuthenticator", AV10extendeSecretAndAuthenticator);
      }

      protected void S112( )
      {
         /* 'CREATE WALLET' Routine */
         returnInSub = false;
         AV36salt = StringUtil.Trim( StringUtil.Str( (decimal)(AV30year), 4, 0)) + StringUtil.Trim( StringUtil.Str( (decimal)(AV31month), 4, 0)) + StringUtil.Trim( StringUtil.Str( (decimal)(AV34day), 4, 0)) + StringUtil.Trim( StringUtil.Str( (decimal)(AV33chosenPIN), 10, 0));
         GXt_char1 = AV9error;
         new GeneXus.Programs.distributedcrypto.argon2derivekey512(context ).execute(  AV19newPass,  AV36salt, out  AV35extKeySeed, out  GXt_char1) ;
         AV9error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            AV11extKeyCreate.gxTpr_Networktype = AV18networkType;
            AV11extKeyCreate.gxTpr_Createextkeytype = 50;
            AV11extKeyCreate.gxTpr_Createtext = StringUtil.Trim( AV35extKeySeed);
            if ( StringUtil.StrCmp(AV18networkType, "MainNet") == 0 )
            {
               AV11extKeyCreate.gxTpr_Keypath = "m/86'/0'/0'";
            }
            else if ( StringUtil.StrCmp(AV18networkType, "TestNet") == 0 )
            {
               AV11extKeyCreate.gxTpr_Keypath = "m/86'/1'/0'";
            }
            else if ( StringUtil.StrCmp(AV18networkType, "RegTest") == 0 )
            {
               AV11extKeyCreate.gxTpr_Keypath = "m/86'/1'/0'";
            }
            else
            {
               AV9error = "Network Type not sopported";
            }
            GXt_char1 = AV9error;
            new GeneXus.Programs.nbitcoin.createextkey(context ).execute(  AV11extKeyCreate,  AV19newPass, out  AV12extKeyInfo, out  GXt_char1) ;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               AV26wallet.gxTpr_Walletname = AV27walletName;
               AV26wallet.gxTpr_Networktype = AV18networkType;
               AV26wallet.gxTpr_Useauthenticator = false;
               AV26wallet.gxTpr_Wallettype = "BIP86";
               AV10extendeSecretAndAuthenticator.gxTpr_Networktype = AV18networkType;
               AV10extendeSecretAndAuthenticator.gxTpr_Extendedprivatekey = StringUtil.Trim( AV12extKeyInfo.gxTpr_Extended.gxTpr_Privatekey);
               AV10extendeSecretAndAuthenticator.gxTpr_Authenticatorbase32 = "";
               GXt_char1 = AV9error;
               GXt_char2 = AV26wallet.gxTpr_Encryptedsecret;
               new GeneXus.Programs.distributedcrypto.argon2encryption(context ).execute(  10,  AV19newPass,  AV10extendeSecretAndAuthenticator.ToJSonString(false, true), out  GXt_char2, ref  GXt_char1) ;
               AV26wallet.gxTpr_Encryptedsecret = GXt_char2;
               AV9error = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
               {
                  new GeneXus.Programs.wallet.createwalletfiles(context ).execute(  AV26wallet) ;
                  context.setWebReturnParms(new Object[] {});
                  context.setWebReturnParmsMetadata(new Object[] {});
                  context.wjLocDisableFrm = 1;
                  context.nUserReturn = 1;
                  returnInSub = true;
                  if (true) return;
               }
               else
               {
                  GX_msglist.addItem("There was a problem encrypting the Extended Key: "+AV9error);
               }
            }
            else
            {
               GX_msglist.addItem(AV9error);
            }
         }
         else
         {
            GX_msglist.addItem(AV9error);
         }
      }

      protected void E132E2( )
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

      protected void nextLoad( )
      {
      }

      protected void E142E2( )
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
         PA2E2( ) ;
         WS2E2( ) ;
         WE2E2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202551216232789", true, true);
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
         context.AddJavascriptSource("wallet/createrestoreadvancebrainwallet.js", "?202551216232789", false, true);
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
            AV18networkType = cmbavNetworktype.getValidValue(AV18networkType);
            AssignAttri("", false, "AV18networkType", AV18networkType);
         }
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         lblWarning_Internalname = "WARNING";
         edtavWalletname_Internalname = "vWALLETNAME";
         cmbavNetworktype_Internalname = "vNETWORKTYPE";
         edtavNewpass_Internalname = "vNEWPASS";
         edtavNewpassconfirm_Internalname = "vNEWPASSCONFIRM";
         edtavYear_Internalname = "vYEAR";
         edtavMonth_Internalname = "vMONTH";
         edtavDay_Internalname = "vDAY";
         divTable1_Internalname = "TABLE1";
         edtavChosenpin_Internalname = "vCHOSENPIN";
         divGroup1table_Internalname = "GROUP1TABLE";
         grpGroup1_Internalname = "GROUP1";
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
         edtavChosenpin_Jsonclick = "";
         edtavChosenpin_Enabled = 1;
         edtavDay_Jsonclick = "";
         edtavDay_Enabled = 1;
         edtavMonth_Jsonclick = "";
         edtavMonth_Enabled = 1;
         edtavYear_Jsonclick = "";
         edtavYear_Enabled = 1;
         edtavNewpassconfirm_Jsonclick = "";
         edtavNewpassconfirm_Enabled = 1;
         edtavNewpass_Jsonclick = "";
         edtavNewpass_Enabled = 1;
         cmbavNetworktype_Jsonclick = "";
         cmbavNetworktype.Enabled = 1;
         edtavWalletname_Enabled = 1;
         lblWarning_Caption = "Text Block";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Create Restore Advance Brain Wallet";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[]}""");
         setEventMetadata("'CREATE WALLET'","""{"handler":"E122E2","iparms":[{"av":"AV27walletName","fld":"vWALLETNAME","type":"char"},{"av":"AV19newPass","fld":"vNEWPASS","type":"char"},{"av":"AV20newPassConfirm","fld":"vNEWPASSCONFIRM","type":"char"},{"av":"AV30year","fld":"vYEAR","pic":"ZZZ9","type":"int"},{"av":"AV31month","fld":"vMONTH","pic":"ZZZ9","type":"int"},{"av":"AV34day","fld":"vDAY","pic":"ZZZ9","type":"int"},{"av":"AV33chosenPIN","fld":"vCHOSENPIN","pic":"ZZZZZZZZZ9","type":"int"},{"av":"cmbavNetworktype"},{"av":"AV18networkType","fld":"vNETWORKTYPE","type":"char"},{"av":"AV11extKeyCreate","fld":"vEXTKEYCREATE","type":""},{"av":"AV26wallet","fld":"vWALLET","type":""},{"av":"AV10extendeSecretAndAuthenticator","fld":"vEXTENDESECRETANDAUTHENTICATOR","type":""}]""");
         setEventMetadata("'CREATE WALLET'",""","oparms":[{"av":"AV11extKeyCreate","fld":"vEXTKEYCREATE","type":""},{"av":"AV26wallet","fld":"vWALLET","type":""},{"av":"AV10extendeSecretAndAuthenticator","fld":"vEXTENDESECRETANDAUTHENTICATOR","type":""}]}""");
         setEventMetadata("'CANCEL AND CLOSE'","""{"handler":"E132E2","iparms":[]}""");
         setEventMetadata("VALIDV_NETWORKTYPE","""{"handler":"Validv_Networktype","iparms":[]}""");
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
         AV11extKeyCreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
         AV26wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV10extendeSecretAndAuthenticator = new GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         lblWarning_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV27walletName = "";
         AV18networkType = "";
         AV19newPass = "";
         AV20newPassConfirm = "";
         bttCreatewallet_Jsonclick = "";
         bttCancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV36salt = "";
         AV9error = "";
         AV35extKeySeed = "";
         AV12extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_char1 = "";
         GXt_char2 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV30year ;
      private short AV31month ;
      private short AV34day ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int edtavWalletname_Enabled ;
      private int edtavNewpass_Enabled ;
      private int edtavNewpassconfirm_Enabled ;
      private int edtavYear_Enabled ;
      private int edtavMonth_Enabled ;
      private int edtavDay_Enabled ;
      private int edtavChosenpin_Enabled ;
      private int idxLst ;
      private long AV33chosenPIN ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string lblWarning_Internalname ;
      private string lblWarning_Caption ;
      private string lblWarning_Jsonclick ;
      private string edtavWalletname_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string AV27walletName ;
      private string cmbavNetworktype_Internalname ;
      private string AV18networkType ;
      private string cmbavNetworktype_Jsonclick ;
      private string edtavNewpass_Internalname ;
      private string AV19newPass ;
      private string edtavNewpass_Jsonclick ;
      private string edtavNewpassconfirm_Internalname ;
      private string AV20newPassConfirm ;
      private string edtavNewpassconfirm_Jsonclick ;
      private string grpGroup1_Internalname ;
      private string divGroup1table_Internalname ;
      private string divTable1_Internalname ;
      private string edtavYear_Internalname ;
      private string edtavYear_Jsonclick ;
      private string edtavMonth_Internalname ;
      private string edtavMonth_Jsonclick ;
      private string edtavDay_Internalname ;
      private string edtavDay_Jsonclick ;
      private string edtavChosenpin_Internalname ;
      private string edtavChosenpin_Jsonclick ;
      private string bttCreatewallet_Internalname ;
      private string bttCreatewallet_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV36salt ;
      private string AV9error ;
      private string AV35extKeySeed ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavNetworktype ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyCreate AV11extKeyCreate ;
      private GeneXus.Programs.wallet.SdtWallet AV26wallet ;
      private GeneXus.Programs.wallet.SdtExtendeSecretAndAuthenticator AV10extendeSecretAndAuthenticator ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV12extKeyInfo ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
