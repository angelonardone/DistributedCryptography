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
   public class showextendedkey : GXDataArea
   {
      public showextendedkey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public showextendedkey( IGxContext context )
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
         chkavCtlishardended = new GXCheckbox();
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
         PA0O2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START0O2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1759500), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1759500), false, true);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.showextendedkey") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Extkeyinfo", AV5extKeyInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Extkeyinfo", AV5extKeyInfo);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTKEYINFO", AV5extKeyInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTKEYINFO", AV5extKeyInfo);
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
            WE0O2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT0O2( ) ;
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
         return formatLink("wallet.showextendedkey")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.ShowExtendedKey" ;
      }

      public override string GetPgmdesc( )
      {
         return "Show Extended Key" ;
      }

      protected void WB0O0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavMasterpublickey_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMasterpublickey_Internalname, "Master Public Key", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavMasterpublickey_Internalname, StringUtil.RTrim( AV7masterPublicKey), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", 0, 1, edtavMasterpublickey_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/ShowExtendedKey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlfingerprint_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlfingerprint_Internalname, "Fingerprint", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlfingerprint_Internalname, StringUtil.RTrim( AV5extKeyInfo.gxTpr_Extended.gxTpr_Fingerprint), StringUtil.RTrim( context.localUtil.Format( AV5extKeyInfo.gxTpr_Extended.gxTpr_Fingerprint, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlfingerprint_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlfingerprint_Enabled, 0, "text", "", 20, "chr", 1, "row", 20, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/ShowExtendedKey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlparentfingerprint_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlparentfingerprint_Internalname, "Parent Fingerprint", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlparentfingerprint_Internalname, StringUtil.RTrim( AV5extKeyInfo.gxTpr_Extended.gxTpr_Parentfingerprint), StringUtil.RTrim( context.localUtil.Format( AV5extKeyInfo.gxTpr_Extended.gxTpr_Parentfingerprint, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlparentfingerprint_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlparentfingerprint_Enabled, 0, "text", "", 20, "chr", 1, "row", 20, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/ShowExtendedKey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtldepth_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtldepth_Internalname, "Depth", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtldepth_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV5extKeyInfo.gxTpr_Extended.gxTpr_Depth), 4, 0, ".", "")), StringUtil.LTrim( ((edtavCtldepth_Enabled!=0) ? context.localUtil.Format( (decimal)(AV5extKeyInfo.gxTpr_Extended.gxTpr_Depth), "ZZZ9") : context.localUtil.Format( (decimal)(AV5extKeyInfo.gxTpr_Extended.gxTpr_Depth), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,23);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtldepth_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtldepth_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/ShowExtendedKey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlchild_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlchild_Internalname, "Child", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlchild_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV5extKeyInfo.gxTpr_Extended.gxTpr_Child), 10, 0, ".", "")), StringUtil.LTrim( ((edtavCtlchild_Enabled!=0) ? context.localUtil.Format( (decimal)(AV5extKeyInfo.gxTpr_Extended.gxTpr_Child), "ZZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV5extKeyInfo.gxTpr_Extended.gxTpr_Child), "ZZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,28);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlchild_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlchild_Enabled, 0, "text", "1", 10, "chr", 1, "row", 10, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/ShowExtendedKey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavCtlishardended_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavCtlishardended_Internalname, "is Hardended", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavCtlishardended_Internalname, StringUtil.BoolToStr( AV5extKeyInfo.gxTpr_Extended.gxTpr_Ishardended), "", "is Hardended", 1, chkavCtlishardended.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(33, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,33);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttClose_Internalname, "", "Close", bttClose_Jsonclick, 5, "Close", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/ShowExtendedKey.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START0O2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_13-186702", 0) ;
            }
         }
         Form.Meta.addItem("description", "Show Extended Key", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP0O0( ) ;
      }

      protected void WS0O2( )
      {
         START0O2( ) ;
         EVT0O2( ) ;
      }

      protected void EVT0O2( )
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
                              E110O2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CLOSE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Close' */
                              E120O2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E130O2 ();
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

      protected void WE0O2( )
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

      protected void PA0O2( )
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
               GX_FocusControl = edtavMasterpublickey_Internalname;
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
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF0O2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavMasterpublickey_Enabled = 0;
         AssignProp("", false, edtavMasterpublickey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMasterpublickey_Enabled), 5, 0), true);
         edtavCtlfingerprint_Enabled = 0;
         AssignProp("", false, edtavCtlfingerprint_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlfingerprint_Enabled), 5, 0), true);
         edtavCtlparentfingerprint_Enabled = 0;
         AssignProp("", false, edtavCtlparentfingerprint_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlparentfingerprint_Enabled), 5, 0), true);
         edtavCtldepth_Enabled = 0;
         AssignProp("", false, edtavCtldepth_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldepth_Enabled), 5, 0), true);
         edtavCtlchild_Enabled = 0;
         AssignProp("", false, edtavCtlchild_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlchild_Enabled), 5, 0), true);
         chkavCtlishardended.Enabled = 0;
         AssignProp("", false, chkavCtlishardended_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavCtlishardended.Enabled), 5, 0), true);
      }

      protected void RF0O2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E130O2 ();
            WB0O0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes0O2( )
      {
      }

      protected void before_start_formulas( )
      {
         edtavMasterpublickey_Enabled = 0;
         AssignProp("", false, edtavMasterpublickey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMasterpublickey_Enabled), 5, 0), true);
         edtavCtlfingerprint_Enabled = 0;
         AssignProp("", false, edtavCtlfingerprint_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlfingerprint_Enabled), 5, 0), true);
         edtavCtlparentfingerprint_Enabled = 0;
         AssignProp("", false, edtavCtlparentfingerprint_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlparentfingerprint_Enabled), 5, 0), true);
         edtavCtldepth_Enabled = 0;
         AssignProp("", false, edtavCtldepth_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldepth_Enabled), 5, 0), true);
         edtavCtlchild_Enabled = 0;
         AssignProp("", false, edtavCtlchild_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlchild_Enabled), 5, 0), true);
         chkavCtlishardended.Enabled = 0;
         AssignProp("", false, chkavCtlishardended_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavCtlishardended.Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0O0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E110O2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vEXTKEYINFO"), AV5extKeyInfo);
            ajax_req_read_hidden_sdt(cgiGet( "Extkeyinfo"), AV5extKeyInfo);
            /* Read saved values. */
            /* Read variables values. */
            AV7masterPublicKey = cgiGet( edtavMasterpublickey_Internalname);
            AssignAttri("", false, "AV7masterPublicKey", AV7masterPublicKey);
            AV5extKeyInfo.gxTpr_Extended.gxTpr_Fingerprint = cgiGet( edtavCtlfingerprint_Internalname);
            AV5extKeyInfo.gxTpr_Extended.gxTpr_Parentfingerprint = cgiGet( edtavCtlparentfingerprint_Internalname);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtldepth_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtldepth_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLDEPTH");
               GX_FocusControl = edtavCtldepth_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV5extKeyInfo.gxTpr_Extended.gxTpr_Depth = 0;
            }
            else
            {
               AV5extKeyInfo.gxTpr_Extended.gxTpr_Depth = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtldepth_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtlchild_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtlchild_Internalname), ".", ",") > Convert.ToDecimal( 9999999999L )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLCHILD");
               GX_FocusControl = edtavCtlchild_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV5extKeyInfo.gxTpr_Extended.gxTpr_Child = 0;
            }
            else
            {
               AV5extKeyInfo.gxTpr_Extended.gxTpr_Child = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtlchild_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            AV5extKeyInfo.gxTpr_Extended.gxTpr_Ishardended = StringUtil.StrToBool( cgiGet( chkavCtlishardended_Internalname));
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
         E110O2 ();
         if (returnInSub) return;
      }

      protected void E110O2( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtExtKeyInfo1 = AV5extKeyInfo;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV5extKeyInfo = GXt_SdtExtKeyInfo1;
         GXt_SdtWallet2 = AV6wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV6wallet = GXt_SdtWallet2;
         if ( StringUtil.StrCmp(AV6wallet.gxTpr_Wallettype, "BIP44") == 0 )
         {
            AV7masterPublicKey = AV5extKeyInfo.gxTpr_Extended.gxTpr_Nuterpublickey;
            AssignAttri("", false, "AV7masterPublicKey", AV7masterPublicKey);
         }
         else if ( StringUtil.StrCmp(AV6wallet.gxTpr_Wallettype, "BIP49") == 0 )
         {
            AV7masterPublicKey = AV5extKeyInfo.gxTpr_Extended.gxTpr_Nuterpublickeysegwitp2sh;
            AssignAttri("", false, "AV7masterPublicKey", AV7masterPublicKey);
         }
         else if ( StringUtil.StrCmp(AV6wallet.gxTpr_Wallettype, "BIP84") == 0 )
         {
            AV7masterPublicKey = AV5extKeyInfo.gxTpr_Extended.gxTpr_Nuterpublickeysegwit;
            AssignAttri("", false, "AV7masterPublicKey", AV7masterPublicKey);
         }
         else if ( StringUtil.StrCmp(AV6wallet.gxTpr_Wallettype, "BIP86") == 0 )
         {
            AV7masterPublicKey = AV5extKeyInfo.gxTpr_Extended.gxTpr_Nuterpublickeytaproot;
            AssignAttri("", false, "AV7masterPublicKey", AV7masterPublicKey);
         }
         else
         {
            GX_msglist.addItem("We couldn't find the this type of wallet addresses");
         }
      }

      protected void E120O2( )
      {
         /* 'Close' Routine */
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

      protected void E130O2( )
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
         PA0O2( ) ;
         WS0O2( ) ;
         WE0O2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202573011564495", true, true);
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
         context.AddJavascriptSource("wallet/showextendedkey.js", "?202573011564495", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         chkavCtlishardended.Name = "CTLISHARDENDED";
         chkavCtlishardended.WebTags = "";
         chkavCtlishardended.Caption = "is Hardended";
         AssignProp("", false, chkavCtlishardended_Internalname, "TitleCaption", chkavCtlishardended.Caption, true);
         chkavCtlishardended.CheckedValue = "false";
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavMasterpublickey_Internalname = "vMASTERPUBLICKEY";
         edtavCtlfingerprint_Internalname = "CTLFINGERPRINT";
         edtavCtlparentfingerprint_Internalname = "CTLPARENTFINGERPRINT";
         edtavCtldepth_Internalname = "CTLDEPTH";
         edtavCtlchild_Internalname = "CTLCHILD";
         chkavCtlishardended_Internalname = "CTLISHARDENDED";
         bttClose_Internalname = "CLOSE";
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
         chkavCtlishardended.Caption = "is Hardended";
         chkavCtlishardended.Enabled = -1;
         edtavCtlchild_Enabled = -1;
         edtavCtldepth_Enabled = -1;
         edtavCtlparentfingerprint_Enabled = -1;
         edtavCtlfingerprint_Enabled = -1;
         chkavCtlishardended.Enabled = 0;
         edtavCtlchild_Jsonclick = "";
         edtavCtlchild_Enabled = 0;
         edtavCtldepth_Jsonclick = "";
         edtavCtldepth_Enabled = 0;
         edtavCtlparentfingerprint_Jsonclick = "";
         edtavCtlparentfingerprint_Enabled = 0;
         edtavCtlfingerprint_Jsonclick = "";
         edtavCtlfingerprint_Enabled = 0;
         edtavMasterpublickey_Enabled = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Show Extended Key";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GXV5","fld":"CTLISHARDENDED","type":"boolean"}]}""");
         setEventMetadata("'CLOSE'","""{"handler":"E120O2","iparms":[]}""");
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
         AV5extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV7masterPublicKey = "";
         bttClose_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV6wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
         edtavMasterpublickey_Enabled = 0;
         edtavCtlfingerprint_Enabled = 0;
         edtavCtlparentfingerprint_Enabled = 0;
         edtavCtldepth_Enabled = 0;
         edtavCtlchild_Enabled = 0;
         chkavCtlishardended.Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int edtavMasterpublickey_Enabled ;
      private int edtavCtlfingerprint_Enabled ;
      private int edtavCtlparentfingerprint_Enabled ;
      private int edtavCtldepth_Enabled ;
      private int edtavCtlchild_Enabled ;
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
      private string edtavMasterpublickey_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string AV7masterPublicKey ;
      private string edtavCtlfingerprint_Internalname ;
      private string edtavCtlfingerprint_Jsonclick ;
      private string edtavCtlparentfingerprint_Internalname ;
      private string edtavCtlparentfingerprint_Jsonclick ;
      private string edtavCtldepth_Internalname ;
      private string edtavCtldepth_Jsonclick ;
      private string edtavCtlchild_Internalname ;
      private string edtavCtlchild_Jsonclick ;
      private string chkavCtlishardended_Internalname ;
      private string bttClose_Internalname ;
      private string bttClose_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavCtlishardended ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV5extKeyInfo ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWallet AV6wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
