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
   public class encryptedfiles : GXDataArea
   {
      public encryptedfiles( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encryptedfiles( IGxContext context )
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridfiles") == 0 )
            {
               gxnrGridfiles_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridfiles") == 0 )
            {
               gxgrGridfiles_refresh_invoke( ) ;
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

      protected void gxnrGridfiles_newrow_invoke( )
      {
         nRC_GXsfl_9 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_9"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_9_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_9_idx = GetPar( "sGXsfl_9_idx");
         AV28deleteFile = GetPar( "deleteFile");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridfiles_newrow( ) ;
         /* End function gxnrGridfiles_newrow_invoke */
      }

      protected void gxgrGridfiles_refresh_invoke( )
      {
         AV28deleteFile = GetPar( "deleteFile");
         ajax_req_read_hidden_sdt(GetNextPar( ), AV23wallet);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV27keyInfo);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridfiles_refresh( AV28deleteFile, AV23wallet, AV27keyInfo) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridfiles_refresh_invoke */
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
         this.cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA112( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START112( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 2014200), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("FileUpload/fileupload.min.js", "", false, true);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.encryptedfiles.aspx") +"\">") ;
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
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV23wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV23wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV23wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vKEYINFO", AV27keyInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vKEYINFO", AV27keyInfo);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vKEYINFO", GetSecureSignedToken( "", AV27keyInfo, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Encryptedfiles", AV10encryptedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Encryptedfiles", AV10encryptedFiles);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_9", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_9), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vUPLOADEDFILES", AV21UploadedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vUPLOADEDFILES", AV21UploadedFiles);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vFAILEDFILES", AV14FailedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vFAILEDFILES", AV14FailedFiles);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV23wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV23wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV23wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vKEYINFO", AV27keyInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vKEYINFO", AV27keyInfo);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vKEYINFO", GetSecureSignedToken( "", AV27keyInfo, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vENCRYPTEDFILE", AV9encryptedFile);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vENCRYPTEDFILE", AV9encryptedFile);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vENCRYPTEDFILES", AV10encryptedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vENCRYPTEDFILES", AV10encryptedFiles);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vUSERRESPONSE", AV22UserResponse);
         GxWebStd.gx_boolean_hidden_field( context, "vFROMDELETEFILE", AV32fromDeleteFile);
         GxWebStd.gx_hidden_field( context, "vFILENAME", StringUtil.RTrim( AV30FileName));
         GxWebStd.gx_hidden_field( context, "vENCRYPTEDKEY", StringUtil.RTrim( AV11encryptedKey));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDOWNLOADENCRYPTEDFILE", AV7downloadEncryptedFile);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDOWNLOADENCRYPTEDFILE", AV7downloadEncryptedFile);
         }
         GxWebStd.gx_hidden_field( context, "FILEUPLOAD_Autoupload", StringUtil.BoolToStr( Fileupload_Autoupload));
         GxWebStd.gx_hidden_field( context, "FILEUPLOAD_Hideadditionalbuttons", StringUtil.BoolToStr( Fileupload_Hideadditionalbuttons));
         GxWebStd.gx_hidden_field( context, "FILEUPLOAD_Maxfilesize", StringUtil.LTrim( StringUtil.NToC( (decimal)(Fileupload_Maxfilesize), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "FILEUPLOAD_Maxnumberoffiles", StringUtil.LTrim( StringUtil.NToC( (decimal)(Fileupload_Maxnumberoffiles), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "FILEUPLOAD_Autodisableaddingfiles", StringUtil.BoolToStr( Fileupload_Autodisableaddingfiles));
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
            WE112( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT112( ) ;
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
         return formatLink("wallet.encryptedfiles.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.EncryptedFiles" ;
      }

      public override string GetPgmdesc( )
      {
         return "Encrypted Files" ;
      }

      protected void WB110( )
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
            /* User Defined Control */
            ucFileupload.SetProperty("AutoUpload", Fileupload_Autoupload);
            ucFileupload.SetProperty("HideAdditionalButtons", Fileupload_Hideadditionalbuttons);
            ucFileupload.SetProperty("TooltipText", Fileupload_Tooltiptext);
            ucFileupload.SetProperty("MaxNumberOfFiles", Fileupload_Maxnumberoffiles);
            ucFileupload.SetProperty("AutoDisableAddingFiles", Fileupload_Autodisableaddingfiles);
            ucFileupload.SetProperty("UploadedFiles", AV21UploadedFiles);
            ucFileupload.SetProperty("FailedFiles", AV14FailedFiles);
            ucFileupload.Render(context, "fileupload", Fileupload_Internalname, "FILEUPLOADContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridfilesContainer.SetWrapped(nGXWrapped);
            StartGridControl9( ) ;
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            nRC_GXsfl_9 = (int)(nGXsfl_9_idx-1);
            if ( GridfilesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV33GXV1 = nGXsfl_9_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridfilesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridfiles", GridfilesContainer, subGridfiles_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridfilesContainerData", GridfilesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridfilesContainerData"+"V", GridfilesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridfilesContainerData"+"V"+"\" value='"+GridfilesContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridfilesContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV33GXV1 = nGXsfl_9_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridfilesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridfiles", GridfilesContainer, subGridfiles_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridfilesContainerData", GridfilesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridfilesContainerData"+"V", GridfilesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridfilesContainerData"+"V"+"\" value='"+GridfilesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START112( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_8-180599", 0) ;
            }
         }
         Form.Meta.addItem("description", "Encrypted Files", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP110( ) ;
      }

      protected void WS112( )
      {
         START112( ) ;
         EVT112( ) ;
      }

      protected void EVT112( )
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
                           else if ( StringUtil.StrCmp(sEvt, "FILEUPLOAD.UPLOADCOMPLETE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Fileupload.Uploadcomplete */
                              E11112 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E12112 ();
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'DECRYPT AND DOWNLOAD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'DELETE FILE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "GRIDFILES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'DECRYPT AND DOWNLOAD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'DELETE FILE'") == 0 ) )
                           {
                              nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
                              SubsflControlProps_92( ) ;
                              AV33GXV1 = nGXsfl_9_idx;
                              if ( ( AV10encryptedFiles.Count >= AV33GXV1 ) && ( AV33GXV1 > 0 ) )
                              {
                                 AV10encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1));
                                 AV28deleteFile = cgiGet( edtavDeletefile_Internalname);
                                 AssignAttri("", false, edtavDeletefile_Internalname, AV28deleteFile);
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
                                    E13112 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DECRYPT AND DOWNLOAD'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Decrypt and download' */
                                    E14112 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE FILE'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Delete File' */
                                    E15112 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDFILES.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridfiles.Load */
                                    E16112 ();
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

      protected void WE112( )
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

      protected void PA112( )
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
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridfiles_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_92( ) ;
         while ( nGXsfl_9_idx <= nRC_GXsfl_9 )
         {
            sendrow_92( ) ;
            nGXsfl_9_idx = ((subGridfiles_Islastpage==1)&&(nGXsfl_9_idx+1>subGridfiles_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridfilesContainer)) ;
         /* End function gxnrGridfiles_newrow */
      }

      protected void gxgrGridfiles_refresh( string AV28deleteFile ,
                                            GeneXus.Programs.wallet.SdtWallet AV23wallet ,
                                            GeneXus.Programs.nbitcoin.SdtKeyInfo AV27keyInfo )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDFILES_nCurrentRecord = 0;
         RF112( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridfiles_refresh */
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
         RF112( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlfilename_Enabled = 0;
         AssignProp("", false, edtavCtlfilename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlfilename_Enabled), 5, 0), !bGXsfl_9_Refreshing);
         edtavCtlcreate_Enabled = 0;
         AssignProp("", false, edtavCtlcreate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcreate_Enabled), 5, 0), !bGXsfl_9_Refreshing);
         edtavDeletefile_Enabled = 0;
         AssignProp("", false, edtavDeletefile_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDeletefile_Enabled), 5, 0), !bGXsfl_9_Refreshing);
      }

      protected void RF112( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridfilesContainer.ClearRows();
         }
         wbStart = 9;
         nGXsfl_9_idx = 1;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         bGXsfl_9_Refreshing = true;
         GridfilesContainer.AddObjectProperty("GridName", "Gridfiles");
         GridfilesContainer.AddObjectProperty("CmpContext", "");
         GridfilesContainer.AddObjectProperty("InMasterPage", "false");
         GridfilesContainer.AddObjectProperty("Class", "Grid");
         GridfilesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridfilesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridfilesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Backcolorstyle), 1, 0, ".", "")));
         GridfilesContainer.PageSize = subGridfiles_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_92( ) ;
            /* Execute user event: Gridfiles.Load */
            E16112 ();
            wbEnd = 9;
            WB110( ) ;
         }
         bGXsfl_9_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes112( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV23wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV23wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV23wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vKEYINFO", AV27keyInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vKEYINFO", AV27keyInfo);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vKEYINFO", GetSecureSignedToken( "", AV27keyInfo, context));
      }

      protected int subGridfiles_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridfiles_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridfiles_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridfiles_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlfilename_Enabled = 0;
         AssignProp("", false, edtavCtlfilename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlfilename_Enabled), 5, 0), !bGXsfl_9_Refreshing);
         edtavCtlcreate_Enabled = 0;
         AssignProp("", false, edtavCtlcreate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcreate_Enabled), 5, 0), !bGXsfl_9_Refreshing);
         edtavDeletefile_Enabled = 0;
         AssignProp("", false, edtavDeletefile_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDeletefile_Enabled), 5, 0), !bGXsfl_9_Refreshing);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP110( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E13112 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Encryptedfiles"), AV10encryptedFiles);
            ajax_req_read_hidden_sdt(cgiGet( "vUPLOADEDFILES"), AV21UploadedFiles);
            ajax_req_read_hidden_sdt(cgiGet( "vFAILEDFILES"), AV14FailedFiles);
            ajax_req_read_hidden_sdt(cgiGet( "vENCRYPTEDFILES"), AV10encryptedFiles);
            /* Read saved values. */
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            Fileupload_Autoupload = StringUtil.StrToBool( cgiGet( "FILEUPLOAD_Autoupload"));
            Fileupload_Hideadditionalbuttons = StringUtil.StrToBool( cgiGet( "FILEUPLOAD_Hideadditionalbuttons"));
            Fileupload_Maxfilesize = (int)(Math.Round(context.localUtil.CToN( cgiGet( "FILEUPLOAD_Maxfilesize"), ".", ","), 18, MidpointRounding.ToEven));
            Fileupload_Maxnumberoffiles = (int)(Math.Round(context.localUtil.CToN( cgiGet( "FILEUPLOAD_Maxnumberoffiles"), ".", ","), 18, MidpointRounding.ToEven));
            Fileupload_Autodisableaddingfiles = StringUtil.StrToBool( cgiGet( "FILEUPLOAD_Autodisableaddingfiles"));
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_9_fel_idx = 0;
            while ( nGXsfl_9_fel_idx < nRC_GXsfl_9 )
            {
               nGXsfl_9_fel_idx = ((subGridfiles_Islastpage==1)&&(nGXsfl_9_fel_idx+1>subGridfiles_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_fel_idx+1);
               sGXsfl_9_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_92( ) ;
               AV33GXV1 = nGXsfl_9_fel_idx;
               if ( ( AV10encryptedFiles.Count >= AV33GXV1 ) && ( AV33GXV1 > 0 ) )
               {
                  AV10encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1));
                  AV28deleteFile = cgiGet( edtavDeletefile_Internalname);
               }
            }
            if ( nGXsfl_9_fel_idx == 0 )
            {
               nGXsfl_9_idx = 1;
               sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
               SubsflControlProps_92( ) ;
            }
            nGXsfl_9_fel_idx = 1;
            /* Read variables values. */
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
         E13112 ();
         if (returnInSub) return;
      }

      protected void E13112( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV23wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV23wallet = GXt_SdtWallet1;
         GXt_SdtKeyInfo2 = AV27keyInfo;
         new GeneXus.Programs.wallet.getfileenckey(context ).execute( out  GXt_SdtKeyInfo2) ;
         AV27keyInfo = GXt_SdtKeyInfo2;
         Fileupload_Autodisableaddingfiles = false;
         ucFileupload.SendProperty(context, "", false, Fileupload_Internalname, "AutoDisableAddingFiles", StringUtil.BoolToStr( Fileupload_Autodisableaddingfiles));
         AV6directory.Source = AV23wallet.gxTpr_Walletbasedirectory+"Files";
         if ( ! AV6directory.Exists() )
         {
            AV6directory.Create();
         }
         GXt_objcol_SdtEncryptedFile3 = AV10encryptedFiles;
         new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile3) ;
         AV10encryptedFiles = GXt_objcol_SdtEncryptedFile3;
         gx_BV9 = true;
         Fileupload_Maxfilesize = 250000000;
         ucFileupload.SendProperty(context, "", false, Fileupload_Internalname, "MaxFileSize", StringUtil.LTrimStr( (decimal)(Fileupload_Maxfilesize), 9, 0));
         AV28deleteFile = "Delete file";
         AssignAttri("", false, edtavDeletefile_Internalname, AV28deleteFile);
      }

      protected void E11112( )
      {
         AV33GXV1 = nGXsfl_9_idx;
         if ( ( AV33GXV1 > 0 ) && ( AV10encryptedFiles.Count >= AV33GXV1 ) )
         {
            AV10encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1));
         }
         /* Fileupload_Uploadcomplete Routine */
         returnInSub = false;
         AV36GXV4 = 1;
         while ( AV36GXV4 <= AV21UploadedFiles.Count )
         {
            AV18FileUploadData = ((SdtFileUploadData)AV21UploadedFiles.Item(AV36GXV4));
            AV20tempBlob = AV18FileUploadData.gxTpr_File;
            AV15File.Source = AV20tempBlob;
            AV26newRndName = Guid.NewGuid( ).ToString();
            AV6directory.Source = AV23wallet.gxTpr_Walletbasedirectory+"Files";
            GXt_boolean4 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
            GXt_boolean5 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
            AV8EncDestination = AV6directory.GetAbsoluteName() + (GXt_boolean5 ? "/" : "\\") + StringUtil.Trim( AV26newRndName);
            GXt_char6 = AV12error;
            new GeneXus.Programs.distributedcrypto.aesencryptfile(context ).execute(  AV15File.GetAbsoluteName(),  AV8EncDestination, out  AV5clearKey, out  AV19iv, out  GXt_char6) ;
            AV12error = GXt_char6;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
            {
               GXt_char6 = AV12error;
               new GeneXus.Programs.nbitcoin.eccenctrypt(context ).execute(  AV27keyInfo.gxTpr_Publickey,  AV5clearKey, out  AV11encryptedKey, out  GXt_char6) ;
               AssignAttri("", false, "AV11encryptedKey", AV11encryptedKey);
               AV12error = GXt_char6;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  AV9encryptedFile.gxTpr_Iv = AV19iv;
                  AV9encryptedFile.gxTpr_Encryptedkey = AV11encryptedKey;
                  AV9encryptedFile.gxTpr_Filename = AV18FileUploadData.gxTpr_Fullname;
                  AV9encryptedFile.gxTpr_Fullfilename = AV8EncDestination;
                  AV9encryptedFile.gxTpr_Create = DateTimeUtil.Now( context);
                  GXt_char6 = AV12error;
                  new GeneXus.Programs.wallet.insertintoallfiles(context ).execute(  AV9encryptedFile, out  GXt_char6) ;
                  AV12error = GXt_char6;
               }
               else
               {
                  GX_msglist.addItem(AV12error);
               }
               this.executeUsercontrolMethod("", false, "FILEUPLOADContainer", "Clear", "", new Object[] {});
               AV15File.Delete();
            }
            else
            {
               GX_msglist.addItem(AV12error);
            }
            GXt_objcol_SdtEncryptedFile3 = AV10encryptedFiles;
            new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile3) ;
            AV10encryptedFiles = GXt_objcol_SdtEncryptedFile3;
            gx_BV9 = true;
            gxgrGridfiles_refresh( AV28deleteFile, AV23wallet, AV27keyInfo) ;
            AV36GXV4 = (int)(AV36GXV4+1);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV9encryptedFile", AV9encryptedFile);
         if ( gx_BV9 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV10encryptedFiles", AV10encryptedFiles);
            nGXsfl_9_bak_idx = nGXsfl_9_idx;
            gxgrGridfiles_refresh( AV28deleteFile, AV23wallet, AV27keyInfo) ;
            nGXsfl_9_idx = nGXsfl_9_bak_idx;
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
      }

      protected void E14112( )
      {
         AV33GXV1 = nGXsfl_9_idx;
         if ( ( AV33GXV1 > 0 ) && ( AV10encryptedFiles.Count >= AV33GXV1 ) )
         {
            AV10encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1));
         }
         /* 'Decrypt and download' Routine */
         returnInSub = false;
         AV7downloadEncryptedFile = ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV10encryptedFiles.CurrentItem));
         AV32fromDeleteFile = false;
         AssignAttri("", false, "AV32fromDeleteFile", AV32fromDeleteFile);
         this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to decrypt and download "+AV7downloadEncryptedFile.gxTpr_Filename+"?"}, false);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV7downloadEncryptedFile", AV7downloadEncryptedFile);
      }

      protected void E15112( )
      {
         AV33GXV1 = nGXsfl_9_idx;
         if ( ( AV33GXV1 > 0 ) && ( AV10encryptedFiles.Count >= AV33GXV1 ) )
         {
            AV10encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1));
         }
         /* 'Delete File' Routine */
         returnInSub = false;
         AV32fromDeleteFile = true;
         AssignAttri("", false, "AV32fromDeleteFile", AV32fromDeleteFile);
         this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete "+((GeneXus.Programs.wallet.SdtEncryptedFile)(AV10encryptedFiles.CurrentItem)).gxTpr_Filename+" file?"}, false);
         AV11encryptedKey = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV10encryptedFiles.CurrentItem)).gxTpr_Encryptedkey);
         AssignAttri("", false, "AV11encryptedKey", AV11encryptedKey);
         AV30FileName = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV10encryptedFiles.CurrentItem)).gxTpr_Filename);
         AssignAttri("", false, "AV30FileName", AV30FileName);
         /*  Sending Event outputs  */
      }

      protected void E12112( )
      {
         AV33GXV1 = nGXsfl_9_idx;
         if ( ( AV33GXV1 > 0 ) && ( AV10encryptedFiles.Count >= AV33GXV1 ) )
         {
            AV10encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV22UserResponse )
         {
            if ( AV32fromDeleteFile )
            {
               AV37GXV5 = 1;
               while ( AV37GXV5 <= AV10encryptedFiles.Count )
               {
                  AV9encryptedFile = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV37GXV5));
                  if ( ( StringUtil.StrCmp(StringUtil.Trim( AV9encryptedFile.gxTpr_Filename), AV30FileName) == 0 ) && ( StringUtil.StrCmp(StringUtil.Trim( AV9encryptedFile.gxTpr_Encryptedkey), AV11encryptedKey) == 0 ) )
                  {
                     AV10encryptedFiles.RemoveItem(AV10encryptedFiles.IndexOf(AV9encryptedFile));
                     gx_BV9 = true;
                     GXt_char6 = "";
                     new GeneXus.Programs.wallet.deleteoneencryptelfiles(context ).execute(  AV9encryptedFile, out  GXt_char6) ;
                     GXt_objcol_SdtEncryptedFile3 = AV10encryptedFiles;
                     new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile3) ;
                     AV10encryptedFiles = GXt_objcol_SdtEncryptedFile3;
                     gx_BV9 = true;
                  }
                  AV37GXV5 = (int)(AV37GXV5+1);
               }
            }
            else
            {
               GXt_char6 = AV12error;
               new GeneXus.Programs.nbitcoin.eccdecrypt(context ).execute(  AV27keyInfo.gxTpr_Privatekey,  AV7downloadEncryptedFile.gxTpr_Encryptedkey, out  AV5clearKey, out  GXt_char6) ;
               AV12error = GXt_char6;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
               {
                  AV24DecSource = AV7downloadEncryptedFile.gxTpr_Fullfilename;
                  AV6directory.Source = "PublicTempStorage";
                  GXt_boolean5 = false;
                  new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
                  GXt_boolean4 = false;
                  new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
                  AV25DecDestination = AV6directory.GetAbsoluteName() + (GXt_boolean4 ? "/" : "\\") + AV7downloadEncryptedFile.gxTpr_Filename;
                  GXt_char6 = AV12error;
                  new GeneXus.Programs.distributedcrypto.aesdecryptfile(context ).execute(  AV24DecSource,  AV25DecDestination,  AV5clearKey,  AV7downloadEncryptedFile.gxTpr_Iv, out  GXt_char6) ;
                  AV12error = GXt_char6;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV12error)) )
                  {
                     GXt_boolean5 = false;
                     new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
                     GXt_boolean4 = false;
                     new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
                     AV25DecDestination = "PublicTempStorage" + (GXt_boolean4 ? "/" : "\\") + AV7downloadEncryptedFile.gxTpr_Filename;
                     this.executeExternalObjectMethod("", false, "gx.extensions.web.window", "open", new Object[] {(string)AV25DecDestination}, false);
                     new GeneXus.Programs.wallet.deletefilewithdelay(context).executeSubmit(  AV25DecDestination) ;
                  }
                  else
                  {
                     GX_msglist.addItem(AV12error);
                  }
               }
               else
               {
                  GX_msglist.addItem(AV12error);
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV9encryptedFile", AV9encryptedFile);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV10encryptedFiles", AV10encryptedFiles);
         nGXsfl_9_bak_idx = nGXsfl_9_idx;
         gxgrGridfiles_refresh( AV28deleteFile, AV23wallet, AV27keyInfo) ;
         nGXsfl_9_idx = nGXsfl_9_bak_idx;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
      }

      private void E16112( )
      {
         /* Gridfiles_Load Routine */
         returnInSub = false;
         AV33GXV1 = 1;
         while ( AV33GXV1 <= AV10encryptedFiles.Count )
         {
            AV10encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 9;
            }
            sendrow_92( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_9_Refreshing )
            {
               DoAjaxLoad(9, GridfilesRow);
            }
            AV33GXV1 = (int)(AV33GXV1+1);
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
         PA112( ) ;
         WS112( ) ;
         WE112( ) ;
         this.cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("FileUpload/fileupload.min.css", "");
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202461814262839", true, true);
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
         context.AddJavascriptSource("wallet/encryptedfiles.js", "?202461814262839", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("FileUpload/fileupload.min.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_92( )
      {
         edtavCtlfilename_Internalname = "CTLFILENAME_"+sGXsfl_9_idx;
         edtavCtlcreate_Internalname = "CTLCREATE_"+sGXsfl_9_idx;
         edtavDeletefile_Internalname = "vDELETEFILE_"+sGXsfl_9_idx;
      }

      protected void SubsflControlProps_fel_92( )
      {
         edtavCtlfilename_Internalname = "CTLFILENAME_"+sGXsfl_9_fel_idx;
         edtavCtlcreate_Internalname = "CTLCREATE_"+sGXsfl_9_fel_idx;
         edtavDeletefile_Internalname = "vDELETEFILE_"+sGXsfl_9_fel_idx;
      }

      protected void sendrow_92( )
      {
         SubsflControlProps_92( ) ;
         WB110( ) ;
         GridfilesRow = GXWebRow.GetNew(context,GridfilesContainer);
         if ( subGridfiles_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridfiles_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridfiles_Class, "") != 0 )
            {
               subGridfiles_Linesclass = subGridfiles_Class+"Odd";
            }
         }
         else if ( subGridfiles_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridfiles_Backstyle = 0;
            subGridfiles_Backcolor = subGridfiles_Allbackcolor;
            if ( StringUtil.StrCmp(subGridfiles_Class, "") != 0 )
            {
               subGridfiles_Linesclass = subGridfiles_Class+"Uniform";
            }
         }
         else if ( subGridfiles_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridfiles_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridfiles_Class, "") != 0 )
            {
               subGridfiles_Linesclass = subGridfiles_Class+"Odd";
            }
            subGridfiles_Backcolor = (int)(0x0);
         }
         else if ( subGridfiles_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridfiles_Backstyle = 1;
            if ( ((int)((nGXsfl_9_idx) % (2))) == 0 )
            {
               subGridfiles_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridfiles_Class, "") != 0 )
               {
                  subGridfiles_Linesclass = subGridfiles_Class+"Even";
               }
            }
            else
            {
               subGridfiles_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridfiles_Class, "") != 0 )
               {
                  subGridfiles_Linesclass = subGridfiles_Class+"Odd";
               }
            }
         }
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_9_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         GridfilesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlfilename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1)).gxTpr_Filename),(string)"",(string)"","'"+""+"'"+",false,"+"'"+"E\\'DECRYPT AND DOWNLOAD\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlfilename_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlfilename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         GridfilesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcreate_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1)).gxTpr_Create, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV10encryptedFiles.Item(AV33GXV1)).gxTpr_Create, "99/99/99 99:99"),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcreate_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcreate_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = " " + ((edtavDeletefile_Enabled!=0)&&(edtavDeletefile_Visible!=0) ? " onfocus=\"gx.evt.onfocus(this, 12,'',false,'"+sGXsfl_9_idx+"',9)\"" : " ");
         ROClassString = "Attribute";
         GridfilesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeletefile_Internalname,StringUtil.RTrim( AV28deleteFile),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+((edtavDeletefile_Enabled!=0)&&(edtavDeletefile_Visible!=0) ? " onblur=\""+""+";gx.evt.onblur(this,12);\"" : " "),"'"+""+"'"+",false,"+"'"+"E\\'DELETE FILE\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDeletefile_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavDeletefile_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes112( ) ;
         GridfilesContainer.AddRow(GridfilesRow);
         nGXsfl_9_idx = ((subGridfiles_Islastpage==1)&&(nGXsfl_9_idx+1>subGridfiles_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         /* End function sendrow_92 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl9( )
      {
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridfilesContainer"+"DivS\" data-gxgridid=\"9\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridfiles_Internalname, subGridfiles_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridfiles_Backcolorstyle == 0 )
            {
               subGridfiles_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridfiles_Class) > 0 )
               {
                  subGridfiles_Linesclass = subGridfiles_Class+"Title";
               }
            }
            else
            {
               subGridfiles_Titlebackstyle = 1;
               if ( subGridfiles_Backcolorstyle == 1 )
               {
                  subGridfiles_Titlebackcolor = subGridfiles_Allbackcolor;
                  if ( StringUtil.Len( subGridfiles_Class) > 0 )
                  {
                     subGridfiles_Linesclass = subGridfiles_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridfiles_Class) > 0 )
                  {
                     subGridfiles_Linesclass = subGridfiles_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "File Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Created / Modified") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "delete File") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridfilesContainer.AddObjectProperty("GridName", "Gridfiles");
         }
         else
         {
            GridfilesContainer.AddObjectProperty("GridName", "Gridfiles");
            GridfilesContainer.AddObjectProperty("Header", subGridfiles_Header);
            GridfilesContainer.AddObjectProperty("Class", "Grid");
            GridfilesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Backcolorstyle), 1, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("CmpContext", "");
            GridfilesContainer.AddObjectProperty("InMasterPage", "false");
            GridfilesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridfilesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlfilename_Enabled), 5, 0, ".", "")));
            GridfilesContainer.AddColumnProperties(GridfilesColumn);
            GridfilesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridfilesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcreate_Enabled), 5, 0, ".", "")));
            GridfilesContainer.AddColumnProperties(GridfilesColumn);
            GridfilesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridfilesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV28deleteFile)));
            GridfilesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeletefile_Enabled), 5, 0, ".", "")));
            GridfilesContainer.AddColumnProperties(GridfilesColumn);
            GridfilesContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Selectedindex), 4, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Allowselection), 1, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Selectioncolor), 9, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Allowhovering), 1, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Hoveringcolor), 9, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Allowcollapsing), 1, 0, ".", "")));
            GridfilesContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridfiles_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         Fileupload_Internalname = "FILEUPLOAD";
         edtavCtlfilename_Internalname = "CTLFILENAME";
         edtavCtlcreate_Internalname = "CTLCREATE";
         edtavDeletefile_Internalname = "vDELETEFILE";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGridfiles_Internalname = "GRIDFILES";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridfiles_Allowcollapsing = 0;
         subGridfiles_Allowselection = 0;
         subGridfiles_Header = "";
         edtavDeletefile_Jsonclick = "";
         edtavDeletefile_Visible = -1;
         edtavDeletefile_Enabled = 1;
         edtavCtlcreate_Jsonclick = "";
         edtavCtlcreate_Enabled = 0;
         edtavCtlfilename_Jsonclick = "";
         edtavCtlfilename_Enabled = 0;
         subGridfiles_Class = "Grid";
         subGridfiles_Backcolorstyle = 0;
         edtavCtlcreate_Enabled = -1;
         edtavCtlfilename_Enabled = -1;
         Fileupload_Tooltiptext = "";
         Fileupload_Autodisableaddingfiles = Convert.ToBoolean( -1);
         Fileupload_Maxnumberoffiles = 1;
         Fileupload_Maxfilesize = 134217728;
         Fileupload_Hideadditionalbuttons = Convert.ToBoolean( -1);
         Fileupload_Autoupload = Convert.ToBoolean( -1);
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Encrypted Files";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDFILES_nFirstRecordOnPage"},{"av":"GRIDFILES_nEOF"},{"av":"AV10encryptedFiles","fld":"vENCRYPTEDFILES","grid":9},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9},{"av":"AV28deleteFile","fld":"vDELETEFILE"},{"av":"AV23wallet","fld":"vWALLET","hsh":true},{"av":"AV27keyInfo","fld":"vKEYINFO","hsh":true}]}""");
         setEventMetadata("FILEUPLOAD.UPLOADCOMPLETE","""{"handler":"E11112","iparms":[{"av":"GRIDFILES_nFirstRecordOnPage"},{"av":"GRIDFILES_nEOF"},{"av":"AV10encryptedFiles","fld":"vENCRYPTEDFILES","grid":9},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9},{"av":"AV28deleteFile","fld":"vDELETEFILE"},{"av":"AV23wallet","fld":"vWALLET","hsh":true},{"av":"AV27keyInfo","fld":"vKEYINFO","hsh":true},{"av":"AV21UploadedFiles","fld":"vUPLOADEDFILES"},{"av":"AV9encryptedFile","fld":"vENCRYPTEDFILE"}]""");
         setEventMetadata("FILEUPLOAD.UPLOADCOMPLETE",""","oparms":[{"av":"AV11encryptedKey","fld":"vENCRYPTEDKEY"},{"av":"AV9encryptedFile","fld":"vENCRYPTEDFILE"},{"av":"AV10encryptedFiles","fld":"vENCRYPTEDFILES","grid":9},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9}]}""");
         setEventMetadata("'DECRYPT AND DOWNLOAD'","""{"handler":"E14112","iparms":[{"av":"AV10encryptedFiles","fld":"vENCRYPTEDFILES","grid":9},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9}]""");
         setEventMetadata("'DECRYPT AND DOWNLOAD'",""","oparms":[{"av":"AV7downloadEncryptedFile","fld":"vDOWNLOADENCRYPTEDFILE"},{"av":"AV32fromDeleteFile","fld":"vFROMDELETEFILE"}]}""");
         setEventMetadata("'DELETE FILE'","""{"handler":"E15112","iparms":[{"av":"AV10encryptedFiles","fld":"vENCRYPTEDFILES","grid":9},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9}]""");
         setEventMetadata("'DELETE FILE'",""","oparms":[{"av":"AV32fromDeleteFile","fld":"vFROMDELETEFILE"},{"av":"AV11encryptedKey","fld":"vENCRYPTEDKEY"},{"av":"AV30FileName","fld":"vFILENAME"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E12112","iparms":[{"av":"AV22UserResponse","fld":"vUSERRESPONSE"},{"av":"AV32fromDeleteFile","fld":"vFROMDELETEFILE"},{"av":"AV10encryptedFiles","fld":"vENCRYPTEDFILES","grid":9},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9},{"av":"AV30FileName","fld":"vFILENAME"},{"av":"AV11encryptedKey","fld":"vENCRYPTEDKEY"},{"av":"AV27keyInfo","fld":"vKEYINFO","hsh":true},{"av":"AV7downloadEncryptedFile","fld":"vDOWNLOADENCRYPTEDFILE"},{"av":"GRIDFILES_nEOF"},{"av":"AV28deleteFile","fld":"vDELETEFILE"},{"av":"AV23wallet","fld":"vWALLET","hsh":true}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV9encryptedFile","fld":"vENCRYPTEDFILE"},{"av":"AV10encryptedFiles","fld":"vENCRYPTEDFILES","grid":9},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Deletefile","iparms":[]}""");
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
         AV28deleteFile = "";
         AV23wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV27keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV10encryptedFiles = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         AV21UploadedFiles = new GXBaseCollection<SdtFileUploadData>( context, "FileUploadData", "distributedcryptography");
         AV14FailedFiles = new GXBaseCollection<SdtFileUploadData>( context, "FileUploadData", "distributedcryptography");
         AV9encryptedFile = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
         AV30FileName = "";
         AV11encryptedKey = "";
         AV7downloadEncryptedFile = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ucFileupload = new GXUserControl();
         GridfilesContainer = new GXWebGrid( context);
         sStyleString = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV6directory = new GxDirectory(context.GetPhysicalPath());
         AV18FileUploadData = new SdtFileUploadData(context);
         AV20tempBlob = "";
         AV15File = new GxFile(context.GetPhysicalPath());
         AV26newRndName = "";
         AV8EncDestination = "";
         AV12error = "";
         AV5clearKey = "";
         AV19iv = "";
         GXt_objcol_SdtEncryptedFile3 = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         AV24DecSource = "";
         AV25DecDestination = "";
         GXt_char6 = "";
         GridfilesRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridfiles_Linesclass = "";
         ROClassString = "";
         TempTags = "";
         GridfilesColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlfilename_Enabled = 0;
         edtavCtlcreate_Enabled = 0;
         edtavDeletefile_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridfiles_Backcolorstyle ;
      private short GRIDFILES_nEOF ;
      private short nGXWrapped ;
      private short subGridfiles_Backstyle ;
      private short subGridfiles_Titlebackstyle ;
      private short subGridfiles_Allowselection ;
      private short subGridfiles_Allowhovering ;
      private short subGridfiles_Allowcollapsing ;
      private short subGridfiles_Collapsed ;
      private int nRC_GXsfl_9 ;
      private int nGXsfl_9_idx=1 ;
      private int Fileupload_Maxfilesize ;
      private int Fileupload_Maxnumberoffiles ;
      private int AV33GXV1 ;
      private int subGridfiles_Islastpage ;
      private int edtavCtlfilename_Enabled ;
      private int edtavCtlcreate_Enabled ;
      private int edtavDeletefile_Enabled ;
      private int nGXsfl_9_fel_idx=1 ;
      private int AV36GXV4 ;
      private int nGXsfl_9_bak_idx=1 ;
      private int AV37GXV5 ;
      private int idxLst ;
      private int subGridfiles_Backcolor ;
      private int subGridfiles_Allbackcolor ;
      private int edtavDeletefile_Visible ;
      private int subGridfiles_Titlebackcolor ;
      private int subGridfiles_Selectedindex ;
      private int subGridfiles_Selectioncolor ;
      private int subGridfiles_Hoveringcolor ;
      private long GRIDFILES_nCurrentRecord ;
      private long GRIDFILES_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_9_idx="0001" ;
      private string AV28deleteFile ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV30FileName ;
      private string AV11encryptedKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string Fileupload_Tooltiptext ;
      private string Fileupload_Internalname ;
      private string sStyleString ;
      private string subGridfiles_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavDeletefile_Internalname ;
      private string edtavCtlfilename_Internalname ;
      private string edtavCtlcreate_Internalname ;
      private string sGXsfl_9_fel_idx="0001" ;
      private string AV26newRndName ;
      private string AV8EncDestination ;
      private string AV12error ;
      private string AV5clearKey ;
      private string AV19iv ;
      private string AV24DecSource ;
      private string AV25DecDestination ;
      private string GXt_char6 ;
      private string subGridfiles_Class ;
      private string subGridfiles_Linesclass ;
      private string ROClassString ;
      private string edtavCtlfilename_Jsonclick ;
      private string edtavCtlcreate_Jsonclick ;
      private string TempTags ;
      private string edtavDeletefile_Jsonclick ;
      private string subGridfiles_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV22UserResponse ;
      private bool AV32fromDeleteFile ;
      private bool Fileupload_Autoupload ;
      private bool Fileupload_Hideadditionalbuttons ;
      private bool Fileupload_Autodisableaddingfiles ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_9_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV9 ;
      private bool GXt_boolean5 ;
      private bool GXt_boolean4 ;
      private string AV20tempBlob ;
      private GXWebGrid GridfilesContainer ;
      private GXWebRow GridfilesRow ;
      private GXWebColumn GridfilesColumn ;
      private GXUserControl ucFileupload ;
      private IGxDataStore dsDefault ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> AV10encryptedFiles ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> GXt_objcol_SdtEncryptedFile3 ;
      private GXBaseCollection<SdtFileUploadData> AV21UploadedFiles ;
      private GXBaseCollection<SdtFileUploadData> AV14FailedFiles ;
      private GxFile AV15File ;
      private GxDirectory AV6directory ;
      private GXWebForm Form ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV9encryptedFile ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV7downloadEncryptedFile ;
      private SdtFileUploadData AV18FileUploadData ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV27keyInfo ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private GeneXus.Programs.wallet.SdtWallet AV23wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
   }

}
