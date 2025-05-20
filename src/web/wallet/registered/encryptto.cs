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
namespace GeneXus.Programs.wallet.registered {
   public class encryptto : GXWebComponent
   {
      public encryptto( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
      }

      public encryptto( IGxContext context )
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

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
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
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA2B2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               WS2B2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
               }
            }
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

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( "Encrypt To") ;
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
         }
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
         context.AddJavascriptSource("FileUpload/fileupload.min.js", "", false, true);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
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
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.encryptto") +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vUPLOADEDFILES", AV15UploadedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vUPLOADEDFILES", AV15UploadedFiles);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vFAILEDFILES", AV9FailedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vFAILEDFILES", AV9FailedFiles);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vPUBKEY", StringUtil.RTrim( AV13PubKey));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Autoupload", StringUtil.BoolToStr( Fileupload_Autoupload));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Hideadditionalbuttons", StringUtil.BoolToStr( Fileupload_Hideadditionalbuttons));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Maxfilesize", StringUtil.LTrim( StringUtil.NToC( (decimal)(Fileupload_Maxfilesize), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Maxnumberoffiles", StringUtil.LTrim( StringUtil.NToC( (decimal)(Fileupload_Maxnumberoffiles), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Autodisableaddingfiles", StringUtil.BoolToStr( Fileupload_Autodisableaddingfiles));
      }

      protected void RenderHtmlCloseForm2B2( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
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
            context.WriteHtmlTextNl( "</body>") ;
            context.WriteHtmlTextNl( "</html>") ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.EncryptTo" ;
      }

      public override string GetPgmdesc( )
      {
         return "Encrypt To" ;
      }

      protected void WB2B0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.encryptto");
               context.AddJavascriptSource("FileUpload/fileupload.min.js", "", false, true);
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavContactusername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavContactusername_Internalname, "User Name", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavContactusername_Internalname, AV5ContactUserName, StringUtil.RTrim( context.localUtil.Format( AV5ContactUserName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavContactusername_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavContactusername_Enabled, 1, "text", "", 80, "chr", 1, "row", 80, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/registered/EncryptTo.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttValidateuserformat_Internalname, "", "Validate user", bttValidateuserformat_Jsonclick, 5, "Validate user", "", StyleString, ClassString, bttValidateuserformat_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'VALIDATE USER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/EncryptTo.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbvalidate_Internalname, "When you click \"Validate user\" we will check that the user exist in our server. The rest of the encryption process is done off-line", "", "", lblTbvalidate_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", lblTbvalidate_Visible, 1, 0, 0, "HLP_Wallet/registered/EncryptTo.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divCanvas_Internalname, divCanvas_Visible, 100, "%", 200, "px", "Table", "start", "top", " "+"data-gx-canvas"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-canvas-cell"+" ", "position:absolute;z-index:0;display:inline-block;right:0px;left:0px;top:0px;", "div");
            /* User Defined Control */
            ucFileupload.SetProperty("AutoUpload", Fileupload_Autoupload);
            ucFileupload.SetProperty("HideAdditionalButtons", Fileupload_Hideadditionalbuttons);
            ucFileupload.SetProperty("TooltipText", Fileupload_Tooltiptext);
            ucFileupload.SetProperty("MaxNumberOfFiles", Fileupload_Maxnumberoffiles);
            ucFileupload.SetProperty("AutoDisableAddingFiles", Fileupload_Autodisableaddingfiles);
            ucFileupload.SetProperty("UploadedFiles", AV15UploadedFiles);
            ucFileupload.SetProperty("FailedFiles", AV9FailedFiles);
            ucFileupload.Render(context, "fileupload", Fileupload_Internalname, sPrefix+"FILEUPLOADContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START2B2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_12-186073", 0) ;
               }
            }
            Form.Meta.addItem("description", "Encrypt To", 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUP2B0( ) ;
            }
         }
      }

      protected void WS2B2( )
      {
         START2B2( ) ;
         EVT2B2( ) ;
      }

      protected void EVT2B2( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
                  if ( StringUtil.Len( sEvt) > 0 )
                  {
                     sEvtType = StringUtil.Left( sEvt, 1);
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2B0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "FILEUPLOAD.UPLOADCOMPLETE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2B0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Fileupload.Uploadcomplete */
                                    E112B2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2B0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E122B2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'VALIDATE USER'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2B0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Validate user' */
                                    E132B2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2B0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Load */
                                    E142B2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2B0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       if ( ! Rfr0gs )
                                       {
                                       }
                                       dynload_actions( ) ;
                                    }
                                 }
                              }
                              /* No code required for Cancel button. It is implemented as the Reset button. */
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2B0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavContactusername_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
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

      protected void WE2B2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm2B2( ) ;
            }
         }
      }

      protected void PA2B2( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavContactusername_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
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
         RF2B2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF2B2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E142B2 ();
            WB2B0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes2B2( )
      {
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2B0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E122B2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vUPLOADEDFILES"), AV15UploadedFiles);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vFAILEDFILES"), AV9FailedFiles);
            /* Read saved values. */
            Fileupload_Autoupload = StringUtil.StrToBool( cgiGet( sPrefix+"FILEUPLOAD_Autoupload"));
            Fileupload_Hideadditionalbuttons = StringUtil.StrToBool( cgiGet( sPrefix+"FILEUPLOAD_Hideadditionalbuttons"));
            Fileupload_Maxfilesize = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"FILEUPLOAD_Maxfilesize"), ".", ","), 18, MidpointRounding.ToEven));
            Fileupload_Maxnumberoffiles = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"FILEUPLOAD_Maxnumberoffiles"), ".", ","), 18, MidpointRounding.ToEven));
            Fileupload_Autodisableaddingfiles = StringUtil.StrToBool( cgiGet( sPrefix+"FILEUPLOAD_Autodisableaddingfiles"));
            /* Read variables values. */
            AV5ContactUserName = cgiGet( edtavContactusername_Internalname);
            AssignAttri(sPrefix, false, "AV5ContactUserName", AV5ContactUserName);
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
         E122B2 ();
         if (returnInSub) return;
      }

      protected void E122B2( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV16wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV16wallet = GXt_SdtWallet1;
         Fileupload_Autodisableaddingfiles = false;
         ucFileupload.SendProperty(context, sPrefix, false, Fileupload_Internalname, "AutoDisableAddingFiles", StringUtil.BoolToStr( Fileupload_Autodisableaddingfiles));
         Fileupload_Maxfilesize = 999999999;
         ucFileupload.SendProperty(context, sPrefix, false, Fileupload_Internalname, "MaxFileSize", StringUtil.LTrimStr( (decimal)(Fileupload_Maxfilesize), 9, 0));
         divCanvas_Visible = 0;
         AssignProp(sPrefix, false, divCanvas_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divCanvas_Visible), 5, 0), true);
      }

      protected void E112B2( )
      {
         /* Fileupload_Uploadcomplete Routine */
         returnInSub = false;
         AV20GXV1 = 1;
         while ( AV20GXV1 <= AV15UploadedFiles.Count )
         {
            AV11FileUploadData = ((SdtFileUploadData)AV15UploadedFiles.Item(AV20GXV1));
            AV14tempBlob = AV11FileUploadData.gxTpr_File;
            AV10File.Source = AV14tempBlob;
            AV12newRndName = Guid.NewGuid( ).ToString();
            AV6directory.Source = StringUtil.StringReplace( AV10File.GetPath(), "PrivateTempStorage", "PublicTempStorage");
            GXt_boolean2 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
            GXt_boolean3 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
            AV7EncDestination = AV6directory.GetAbsoluteName() + (GXt_boolean3 ? "/" : "\\") + StringUtil.Trim( AV12newRndName);
            AV19realFileName = AV11FileUploadData.gxTpr_Name + "." + AV11FileUploadData.gxTpr_Extension;
            GXt_char4 = AV8error;
            GXt_char5 = "";
            GXt_char6 = "";
            GXt_char7 = "";
            new GeneXus.Programs.distributedcrypto.aesencryptionlargefile(context ).execute(  10,  AV10File.GetAbsoluteName(),  AV7EncDestination,  AV13PubKey,  "", ref  AV19realFileName, ref  GXt_char5, ref  GXt_char6, ref  GXt_char7, out  GXt_char4) ;
            AV8error = GXt_char4;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               AV17newFile.Source = AV7EncDestination;
               GXt_boolean3 = false;
               new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
               GXt_boolean2 = false;
               new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
               AV18renamedFile = AV6directory.GetAbsoluteName() + (GXt_boolean2 ? "/" : "\\") + AV19realFileName;
               AV17newFile.Rename(AV18renamedFile);
               GXt_boolean3 = false;
               new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
               GXt_boolean2 = false;
               new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
               AV7EncDestination = "PublicTempStorage" + (GXt_boolean2 ? "/" : "\\") + AV19realFileName;
               this.executeExternalObjectMethod(sPrefix, false, "gx.extensions.web.window", "open", new Object[] {(string)AV7EncDestination}, false);
               new GeneXus.Programs.wallet.deletefilewithdelay(context).executeSubmit(  AV7EncDestination) ;
               GXt_boolean3 = false;
               new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean3) ;
               GXt_boolean2 = false;
               new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean2) ;
               AV7EncDestination = "PublicTempStorage" + (GXt_boolean2 ? "/" : "\\") + StringUtil.Trim( AV12newRndName);
               new GeneXus.Programs.wallet.deletefilewithdelay(context).executeSubmit(  AV7EncDestination) ;
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"File encrypted!: ",(string)"Check your download folder"}, true);
               AV5ContactUserName = "";
               AssignAttri(sPrefix, false, "AV5ContactUserName", AV5ContactUserName);
               edtavContactusername_Enabled = 1;
               AssignProp(sPrefix, false, edtavContactusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavContactusername_Enabled), 5, 0), true);
               bttValidateuserformat_Visible = 1;
               AssignProp(sPrefix, false, bttValidateuserformat_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttValidateuserformat_Visible), 5, 0), true);
               lblTbvalidate_Visible = 1;
               AssignProp(sPrefix, false, lblTbvalidate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbvalidate_Visible), 5, 0), true);
               divCanvas_Visible = 0;
               AssignProp(sPrefix, false, divCanvas_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divCanvas_Visible), 5, 0), true);
               this.executeUsercontrolMethod(sPrefix, false, "FILEUPLOADContainer", "Clear", "", new Object[] {});
            }
            else
            {
               GX_msglist.addItem(AV8error);
            }
            AV20GXV1 = (int)(AV20GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E132B2( )
      {
         /* 'Validate user' Routine */
         returnInSub = false;
         GXt_char7 = AV8error;
         new GeneXus.Programs.wallet.registered.getuserpubkey(context ).execute(  AV5ContactUserName, out  AV13PubKey, out  GXt_char7) ;
         AssignAttri(sPrefix, false, "AV13PubKey", AV13PubKey);
         AV8error = GXt_char7;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            divCanvas_Visible = 1;
            AssignProp(sPrefix, false, divCanvas_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divCanvas_Visible), 5, 0), true);
            edtavContactusername_Enabled = 0;
            AssignProp(sPrefix, false, edtavContactusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavContactusername_Enabled), 5, 0), true);
            bttValidateuserformat_Visible = 0;
            AssignProp(sPrefix, false, bttValidateuserformat_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttValidateuserformat_Visible), 5, 0), true);
            lblTbvalidate_Visible = 0;
            AssignProp(sPrefix, false, lblTbvalidate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbvalidate_Visible), 5, 0), true);
         }
         else
         {
            GX_msglist.addItem(AV8error);
         }
         /*  Sending Event outputs  */
      }

      protected void nextLoad( )
      {
      }

      protected void E142B2( )
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
         PA2B2( ) ;
         WS2B2( ) ;
         WE2B2( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA2B2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\encryptto", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA2B2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
         }
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PA2B2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS2B2( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WS2B2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WE2B2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("FileUpload/fileupload.min.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202552012584279", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("wallet/registered/encryptto.js", "?202552012584279", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("FileUpload/fileupload.min.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavContactusername_Internalname = sPrefix+"vCONTACTUSERNAME";
         bttValidateuserformat_Internalname = sPrefix+"VALIDATEUSERFORMAT";
         lblTbvalidate_Internalname = sPrefix+"TBVALIDATE";
         Fileupload_Internalname = sPrefix+"FILEUPLOAD";
         divCanvas_Internalname = sPrefix+"CANVAS";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         Fileupload_Tooltiptext = "";
         divCanvas_Visible = 1;
         lblTbvalidate_Visible = 1;
         bttValidateuserformat_Visible = 1;
         edtavContactusername_Jsonclick = "";
         edtavContactusername_Enabled = 1;
         Fileupload_Autodisableaddingfiles = Convert.ToBoolean( -1);
         Fileupload_Maxnumberoffiles = 1;
         Fileupload_Maxfilesize = 134217728;
         Fileupload_Hideadditionalbuttons = Convert.ToBoolean( -1);
         Fileupload_Autoupload = Convert.ToBoolean( -1);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[]}""");
         setEventMetadata("FILEUPLOAD.UPLOADCOMPLETE","""{"handler":"E112B2","iparms":[{"av":"AV15UploadedFiles","fld":"vUPLOADEDFILES","type":""},{"av":"AV13PubKey","fld":"vPUBKEY","type":"char"}]""");
         setEventMetadata("FILEUPLOAD.UPLOADCOMPLETE",""","oparms":[{"av":"AV5ContactUserName","fld":"vCONTACTUSERNAME","type":"svchar"},{"av":"edtavContactusername_Enabled","ctrl":"vCONTACTUSERNAME","prop":"Enabled"},{"ctrl":"VALIDATEUSERFORMAT","prop":"Visible"},{"av":"lblTbvalidate_Visible","ctrl":"TBVALIDATE","prop":"Visible"},{"av":"divCanvas_Visible","ctrl":"CANVAS","prop":"Visible"}]}""");
         setEventMetadata("'VALIDATE USER'","""{"handler":"E132B2","iparms":[{"av":"AV5ContactUserName","fld":"vCONTACTUSERNAME","type":"svchar"}]""");
         setEventMetadata("'VALIDATE USER'",""","oparms":[{"av":"AV13PubKey","fld":"vPUBKEY","type":"char"},{"av":"divCanvas_Visible","ctrl":"CANVAS","prop":"Visible"},{"av":"edtavContactusername_Enabled","ctrl":"vCONTACTUSERNAME","prop":"Enabled"},{"ctrl":"VALIDATEUSERFORMAT","prop":"Visible"},{"av":"lblTbvalidate_Visible","ctrl":"TBVALIDATE","prop":"Visible"}]}""");
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

      public override bool UploadEnabled( )
      {
         return true ;
      }

      public override void initialize( )
      {
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV15UploadedFiles = new GXBaseCollection<SdtFileUploadData>( context, "FileUploadData", "distributedcryptography");
         AV9FailedFiles = new GXBaseCollection<SdtFileUploadData>( context, "FileUploadData", "distributedcryptography");
         AV13PubKey = "";
         GX_FocusControl = "";
         TempTags = "";
         AV5ContactUserName = "";
         ClassString = "";
         StyleString = "";
         bttValidateuserformat_Jsonclick = "";
         lblTbvalidate_Jsonclick = "";
         ucFileupload = new GXUserControl();
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV16wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV11FileUploadData = new SdtFileUploadData(context);
         AV14tempBlob = "";
         AV10File = new GxFile(context.GetPhysicalPath());
         AV12newRndName = "";
         AV6directory = new GxDirectory(context.GetPhysicalPath());
         AV7EncDestination = "";
         AV19realFileName = "";
         AV8error = "";
         GXt_char4 = "";
         GXt_char5 = "";
         GXt_char6 = "";
         AV17newFile = new GxFile(context.GetPhysicalPath());
         AV18renamedFile = "";
         GXt_char7 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int Fileupload_Maxfilesize ;
      private int Fileupload_Maxnumberoffiles ;
      private int edtavContactusername_Enabled ;
      private int bttValidateuserformat_Visible ;
      private int lblTbvalidate_Visible ;
      private int divCanvas_Visible ;
      private int AV20GXV1 ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV13PubKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string edtavContactusername_Internalname ;
      private string TempTags ;
      private string edtavContactusername_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string bttValidateuserformat_Internalname ;
      private string bttValidateuserformat_Jsonclick ;
      private string lblTbvalidate_Internalname ;
      private string lblTbvalidate_Jsonclick ;
      private string divCanvas_Internalname ;
      private string Fileupload_Tooltiptext ;
      private string Fileupload_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV12newRndName ;
      private string AV7EncDestination ;
      private string AV8error ;
      private string GXt_char4 ;
      private string GXt_char5 ;
      private string GXt_char6 ;
      private string AV18renamedFile ;
      private string GXt_char7 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool Fileupload_Autoupload ;
      private bool Fileupload_Hideadditionalbuttons ;
      private bool Fileupload_Autodisableaddingfiles ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool GXt_boolean3 ;
      private bool GXt_boolean2 ;
      private string AV5ContactUserName ;
      private string AV19realFileName ;
      private string AV14tempBlob ;
      private GXUserControl ucFileupload ;
      private GXWebForm Form ;
      private GxFile AV10File ;
      private GxFile AV17newFile ;
      private GxDirectory AV6directory ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<SdtFileUploadData> AV15UploadedFiles ;
      private GXBaseCollection<SdtFileUploadData> AV9FailedFiles ;
      private GeneXus.Programs.wallet.SdtWallet AV16wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private SdtFileUploadData AV11FileUploadData ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
