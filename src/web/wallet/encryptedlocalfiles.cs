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
   public class encryptedlocalfiles : GXDataArea
   {
      public encryptedlocalfiles( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public encryptedlocalfiles( IGxContext context )
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
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridfiles_refresh( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("FileUpload/fileupload.min.js", "", false, true, false);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.encryptedlocalfiles") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Encryptedfiles", AV13encryptedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Encryptedfiles", AV13encryptedFiles);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_9", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_9), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vUPLOADEDFILES", AV23UploadedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vUPLOADEDFILES", AV23UploadedFiles);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vFAILEDFILES", AV28FailedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vFAILEDFILES", AV28FailedFiles);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vENCRYPTEDFILES", AV13encryptedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vENCRYPTEDFILES", AV13encryptedFiles);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vUSERRESPONSE", AV24UserResponse);
         GxWebStd.gx_boolean_hidden_field( context, "vFROMDELETEFILE", AV27fromDeleteFile);
         GxWebStd.gx_hidden_field( context, "vFILENAME", StringUtil.RTrim( AV26FileName));
         GxWebStd.gx_hidden_field( context, "vENCRYPTEDKEY", StringUtil.RTrim( AV14encryptedKey));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDOWNLOADENCRYPTEDFILE", AV10downloadEncryptedFile);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDOWNLOADENCRYPTEDFILE", AV10downloadEncryptedFile);
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
         return formatLink("wallet.encryptedlocalfiles")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.EncryptedLocalFiles" ;
      }

      public override string GetPgmdesc( )
      {
         return "Encrypted Local Files" ;
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
            /* User Defined Control */
            ucFileupload.SetProperty("AutoUpload", Fileupload_Autoupload);
            ucFileupload.SetProperty("HideAdditionalButtons", Fileupload_Hideadditionalbuttons);
            ucFileupload.SetProperty("TooltipText", Fileupload_Tooltiptext);
            ucFileupload.SetProperty("MaxNumberOfFiles", Fileupload_Maxnumberoffiles);
            ucFileupload.SetProperty("AutoDisableAddingFiles", Fileupload_Autodisableaddingfiles);
            ucFileupload.SetProperty("UploadedFiles", AV23UploadedFiles);
            ucFileupload.SetProperty("FailedFiles", AV28FailedFiles);
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
               AV32GXV1 = nGXsfl_9_idx;
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
                  AV32GXV1 = nGXsfl_9_idx;
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

      protected void START2E2( )
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
         Form.Meta.addItem("description", "Encrypted Local Files", 0) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "FILEUPLOAD.UPLOADCOMPLETE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Fileupload.Uploadcomplete */
                              E112E2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E122E2 ();
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'DECRYPT AND DOWNLOAD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'DELETE FILE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "GRIDFILES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'DECRYPT AND DOWNLOAD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'DELETE FILE'") == 0 ) )
                           {
                              nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
                              SubsflControlProps_92( ) ;
                              AV32GXV1 = nGXsfl_9_idx;
                              if ( ( AV13encryptedFiles.Count >= AV32GXV1 ) && ( AV32GXV1 > 0 ) )
                              {
                                 AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1));
                                 AV30deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV30deleteImage)) ? AV35Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV30deleteImage))), !bGXsfl_9_Refreshing);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV30deleteImage), true);
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
                                    E132E2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E142E2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DECRYPT AND DOWNLOAD'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Decrypt and download' */
                                    E152E2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE FILE'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Delete File' */
                                    E162E2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDFILES.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridfiles.Load */
                                    E172E2 ();
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

      protected void gxgrGridfiles_refresh( )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDFILES_nCurrentRecord = 0;
         RF2E2( ) ;
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
         RF2E2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlfilename_Enabled = 0;
         edtavCtlcreate_Enabled = 0;
      }

      protected void RF2E2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridfilesContainer.ClearRows();
         }
         wbStart = 9;
         /* Execute user event: Refresh */
         E142E2 ();
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
            E172E2 ();
            wbEnd = 9;
            WB2E0( ) ;
         }
         bGXsfl_9_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2E2( )
      {
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
         edtavCtlcreate_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2E0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E132E2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Encryptedfiles"), AV13encryptedFiles);
            ajax_req_read_hidden_sdt(cgiGet( "vUPLOADEDFILES"), AV23UploadedFiles);
            ajax_req_read_hidden_sdt(cgiGet( "vFAILEDFILES"), AV28FailedFiles);
            ajax_req_read_hidden_sdt(cgiGet( "vENCRYPTEDFILES"), AV13encryptedFiles);
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
               AV32GXV1 = nGXsfl_9_fel_idx;
               if ( ( AV13encryptedFiles.Count >= AV32GXV1 ) && ( AV32GXV1 > 0 ) )
               {
                  AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1));
                  AV30deleteImage = cgiGet( edtavDeleteimage_Internalname);
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
         E132E2 ();
         if (returnInSub) return;
      }

      protected void E132E2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV28FailedFiles.Clear();
         Fileupload_Maxfilesize = 999999999;
         ucFileupload.SendProperty(context, "", false, Fileupload_Internalname, "MaxFileSize", StringUtil.LTrimStr( (decimal)(Fileupload_Maxfilesize), 9, 0));
         Fileupload_Autodisableaddingfiles = false;
         ucFileupload.SendProperty(context, "", false, Fileupload_Internalname, "AutoDisableAddingFiles", StringUtil.BoolToStr( Fileupload_Autodisableaddingfiles));
      }

      protected void E142E2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         AV8deleteFile = "Delete";
         GXt_objcol_SdtEncryptedFile1 = AV13encryptedFiles;
         new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile1) ;
         AV13encryptedFiles = GXt_objcol_SdtEncryptedFile1;
         gx_BV9 = true;
         edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
         AssignProp("", false, edtavDeleteimage_Internalname, "gximage", edtavDeleteimage_gximage, !bGXsfl_9_Refreshing);
         AV30deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
         AssignProp("", false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV30deleteImage)) ? AV35Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV30deleteImage))), !bGXsfl_9_Refreshing);
         AssignProp("", false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV30deleteImage), true);
         AV35Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
         AssignProp("", false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV30deleteImage)) ? AV35Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV30deleteImage))), !bGXsfl_9_Refreshing);
         AssignProp("", false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV30deleteImage), true);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV13encryptedFiles", AV13encryptedFiles);
      }

      protected void E112E2( )
      {
         AV32GXV1 = nGXsfl_9_idx;
         if ( ( AV32GXV1 > 0 ) && ( AV13encryptedFiles.Count >= AV32GXV1 ) )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1));
         }
         /* Fileupload_Uploadcomplete Routine */
         returnInSub = false;
         AV36GXV4 = 1;
         while ( AV36GXV4 <= AV23UploadedFiles.Count )
         {
            AV18FileUploadData = ((SdtFileUploadData)AV23UploadedFiles.Item(AV36GXV4));
            AV22tempBlob = AV18FileUploadData.gxTpr_File;
            AV17File.Source = AV22tempBlob;
            GXt_char2 = AV15error;
            new GeneXus.Programs.wallet.storelocalfile(context ).execute(  AV17File.GetAbsoluteName(),  AV18FileUploadData.gxTpr_Fullname, out  GXt_char2) ;
            AV15error = GXt_char2;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
            {
               GX_msglist.addItem(AV15error);
            }
            this.executeUsercontrolMethod("", false, "FILEUPLOADContainer", "Clear", "", new Object[] {});
            GXt_objcol_SdtEncryptedFile1 = AV13encryptedFiles;
            new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile1) ;
            AV13encryptedFiles = GXt_objcol_SdtEncryptedFile1;
            gx_BV9 = true;
            gxgrGridfiles_refresh( ) ;
            AV36GXV4 = (int)(AV36GXV4+1);
         }
         /*  Sending Event outputs  */
         if ( gx_BV9 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV13encryptedFiles", AV13encryptedFiles);
            nGXsfl_9_bak_idx = nGXsfl_9_idx;
            gxgrGridfiles_refresh( ) ;
            nGXsfl_9_idx = nGXsfl_9_bak_idx;
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
      }

      protected void E152E2( )
      {
         AV32GXV1 = nGXsfl_9_idx;
         if ( ( AV32GXV1 > 0 ) && ( AV13encryptedFiles.Count >= AV32GXV1 ) )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1));
         }
         /* 'Decrypt and download' Routine */
         returnInSub = false;
         AV10downloadEncryptedFile = ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV13encryptedFiles.CurrentItem));
         AV27fromDeleteFile = false;
         AssignAttri("", false, "AV27fromDeleteFile", AV27fromDeleteFile);
         this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to decrypt and download "+AV10downloadEncryptedFile.gxTpr_Filename+"?"}, false);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV10downloadEncryptedFile", AV10downloadEncryptedFile);
      }

      protected void E162E2( )
      {
         AV32GXV1 = nGXsfl_9_idx;
         if ( ( AV32GXV1 > 0 ) && ( AV13encryptedFiles.Count >= AV32GXV1 ) )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1));
         }
         /* 'Delete File' Routine */
         returnInSub = false;
         AV27fromDeleteFile = true;
         AssignAttri("", false, "AV27fromDeleteFile", AV27fromDeleteFile);
         this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete "+((GeneXus.Programs.wallet.SdtEncryptedFile)(AV13encryptedFiles.CurrentItem)).gxTpr_Filename+" file?"}, false);
         AV14encryptedKey = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV13encryptedFiles.CurrentItem)).gxTpr_Encryptedkey);
         AssignAttri("", false, "AV14encryptedKey", AV14encryptedKey);
         AV26FileName = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV13encryptedFiles.CurrentItem)).gxTpr_Filename);
         AssignAttri("", false, "AV26FileName", AV26FileName);
         /*  Sending Event outputs  */
      }

      protected void E122E2( )
      {
         AV32GXV1 = nGXsfl_9_idx;
         if ( ( AV32GXV1 > 0 ) && ( AV13encryptedFiles.Count >= AV32GXV1 ) )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV24UserResponse )
         {
            if ( AV27fromDeleteFile )
            {
               AV37GXV5 = 1;
               while ( AV37GXV5 <= AV13encryptedFiles.Count )
               {
                  AV12encryptedFile = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV37GXV5));
                  if ( ( StringUtil.StrCmp(StringUtil.Trim( AV12encryptedFile.gxTpr_Filename), AV26FileName) == 0 ) && ( StringUtil.StrCmp(StringUtil.Trim( AV12encryptedFile.gxTpr_Encryptedkey), AV14encryptedKey) == 0 ) )
                  {
                     AV13encryptedFiles.RemoveItem(AV13encryptedFiles.IndexOf(AV12encryptedFile));
                     gx_BV9 = true;
                     GXt_char2 = AV15error;
                     new GeneXus.Programs.wallet.deleteoneencryptelfiles(context ).execute(  AV12encryptedFile, out  GXt_char2) ;
                     AV15error = GXt_char2;
                     GXt_objcol_SdtEncryptedFile1 = AV13encryptedFiles;
                     new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile1) ;
                     AV13encryptedFiles = GXt_objcol_SdtEncryptedFile1;
                     gx_BV9 = true;
                  }
                  AV37GXV5 = (int)(AV37GXV5+1);
               }
            }
            else
            {
               GXt_char2 = AV15error;
               new GeneXus.Programs.wallet.preparelocalfiledownload(context ).execute(  AV10downloadEncryptedFile.gxTpr_Fullfilename, out  AV31token, out  GXt_char2) ;
               AV15error = GXt_char2;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
               {
                  this.executeExternalObjectMethod("", false, "gx.extensions.web.window", "open", new Object[] {formatLink("wallet.adownloadfile", new object[] {UrlEncode(StringUtil.RTrim(AV31token))}, new string[] {"token"}) }, false);
                  this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"File decrypted!: ",(string)"Check your download folder"}, true);
               }
               else
               {
                  GX_msglist.addItem(AV15error);
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV13encryptedFiles", AV13encryptedFiles);
         nGXsfl_9_bak_idx = nGXsfl_9_idx;
         gxgrGridfiles_refresh( ) ;
         nGXsfl_9_idx = nGXsfl_9_bak_idx;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
      }

      private void E172E2( )
      {
         /* Gridfiles_Load Routine */
         returnInSub = false;
         AV32GXV1 = 1;
         while ( AV32GXV1 <= AV13encryptedFiles.Count )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1));
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
            AV32GXV1 = (int)(AV32GXV1+1);
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20261071417258", true, true, false);
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
         context.AddJavascriptSource("wallet/encryptedlocalfiles.js", "?20261071417259", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         context.AddJavascriptSource("FileUpload/fileupload.min.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_92( )
      {
         edtavCtlfilename_Internalname = "CTLFILENAME_"+sGXsfl_9_idx;
         edtavCtlcreate_Internalname = "CTLCREATE_"+sGXsfl_9_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_9_idx;
      }

      protected void SubsflControlProps_fel_92( )
      {
         edtavCtlfilename_Internalname = "CTLFILENAME_"+sGXsfl_9_fel_idx;
         edtavCtlcreate_Internalname = "CTLCREATE_"+sGXsfl_9_fel_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_9_fel_idx;
      }

      protected void sendrow_92( )
      {
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         WB2E0( ) ;
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
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridfilesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlfilename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1)).gxTpr_Filename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,10);\"","'"+""+"'"+",false,"+"'"+"E\\'DECRYPT AND DOWNLOAD\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlfilename_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlfilename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridfilesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcreate_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1)).gxTpr_Create, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV1)).gxTpr_Create, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,11);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcreate_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcreate_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'',false,'',9)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV30deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV30deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV35Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV30deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV30deleteImage)) ? AV35Deleteimage_GXI : context.PathToRelativeUrl( AV30deleteImage));
         GridfilesRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+""+"'"+",false,"+"'"+"E\\'DELETE FILE\\'."+sGXsfl_9_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV30deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         send_integrity_lvl_hashes2E2( ) ;
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
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
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
            GridfilesColumn.AddObjectProperty("Value", context.convertURL( AV30deleteImage));
            GridfilesColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavDeleteimage_Link));
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
         edtavDeleteimage_Internalname = "vDELETEIMAGE";
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
         edtavDeleteimage_Link = "";
         subGridfiles_Header = "";
         edtavDeleteimage_Jsonclick = "";
         edtavCtlcreate_Jsonclick = "";
         edtavCtlcreate_Enabled = 0;
         edtavCtlfilename_Jsonclick = "";
         edtavCtlfilename_Enabled = 0;
         subGridfiles_Class = "Grid";
         subGridfiles_Backcolorstyle = 0;
         edtavDeleteimage_gximage = "";
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
         Form.Caption = "Encrypted Local Files";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"GRIDFILES_nEOF","type":"int"},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV30deleteImage","fld":"vDELETEIMAGE","type":"bits"}]}""");
         setEventMetadata("FILEUPLOAD.UPLOADCOMPLETE","""{"handler":"E112E2","iparms":[{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"GRIDFILES_nEOF","type":"int"},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV23UploadedFiles","fld":"vUPLOADEDFILES","type":""}]""");
         setEventMetadata("FILEUPLOAD.UPLOADCOMPLETE",""","oparms":[{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV30deleteImage","fld":"vDELETEIMAGE","type":"bits"}]}""");
         setEventMetadata("'DECRYPT AND DOWNLOAD'","""{"handler":"E152E2","iparms":[{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"}]""");
         setEventMetadata("'DECRYPT AND DOWNLOAD'",""","oparms":[{"av":"AV10downloadEncryptedFile","fld":"vDOWNLOADENCRYPTEDFILE","type":""},{"av":"AV27fromDeleteFile","fld":"vFROMDELETEFILE","type":"boolean"}]}""");
         setEventMetadata("'DELETE FILE'","""{"handler":"E162E2","iparms":[{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"}]""");
         setEventMetadata("'DELETE FILE'",""","oparms":[{"av":"AV27fromDeleteFile","fld":"vFROMDELETEFILE","type":"boolean"},{"av":"AV14encryptedKey","fld":"vENCRYPTEDKEY","type":"char"},{"av":"AV26FileName","fld":"vFILENAME","type":"char"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E122E2","iparms":[{"av":"AV24UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV27fromDeleteFile","fld":"vFROMDELETEFILE","type":"boolean"},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV26FileName","fld":"vFILENAME","type":"char"},{"av":"AV14encryptedKey","fld":"vENCRYPTEDKEY","type":"char"},{"av":"AV10downloadEncryptedFile","fld":"vDOWNLOADENCRYPTEDFILE","type":""},{"av":"GRIDFILES_nEOF","type":"int"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Deleteimage","iparms":[]}""");
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
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV13encryptedFiles = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         AV23UploadedFiles = new GXBaseCollection<SdtFileUploadData>( context, "FileUploadData", "distributedcryptography");
         AV28FailedFiles = new GXBaseCollection<SdtFileUploadData>( context, "FileUploadData", "distributedcryptography");
         AV26FileName = "";
         AV14encryptedKey = "";
         AV10downloadEncryptedFile = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
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
         AV30deleteImage = "";
         AV35Deleteimage_GXI = "";
         AV8deleteFile = "";
         AV18FileUploadData = new SdtFileUploadData(context);
         AV22tempBlob = "";
         AV17File = new GxFile(context.GetPhysicalPath());
         AV15error = "";
         AV12encryptedFile = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
         GXt_objcol_SdtEncryptedFile1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         GXt_char2 = "";
         AV31token = "";
         GridfilesRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridfiles_Linesclass = "";
         TempTags = "";
         ROClassString = "";
         ClassString = "";
         StyleString = "";
         sImgUrl = "";
         GridfilesColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlfilename_Enabled = 0;
         edtavCtlcreate_Enabled = 0;
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
      private int AV32GXV1 ;
      private int subGridfiles_Islastpage ;
      private int edtavCtlfilename_Enabled ;
      private int edtavCtlcreate_Enabled ;
      private int nGXsfl_9_fel_idx=1 ;
      private int AV36GXV4 ;
      private int nGXsfl_9_bak_idx=1 ;
      private int AV37GXV5 ;
      private int idxLst ;
      private int subGridfiles_Backcolor ;
      private int subGridfiles_Allbackcolor ;
      private int subGridfiles_Titlebackcolor ;
      private int subGridfiles_Selectedindex ;
      private int subGridfiles_Selectioncolor ;
      private int subGridfiles_Hoveringcolor ;
      private long GRIDFILES_nCurrentRecord ;
      private long GRIDFILES_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_9_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV26FileName ;
      private string AV14encryptedKey ;
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
      private string edtavDeleteimage_Internalname ;
      private string sGXsfl_9_fel_idx="0001" ;
      private string AV8deleteFile ;
      private string edtavDeleteimage_gximage ;
      private string AV15error ;
      private string GXt_char2 ;
      private string edtavCtlfilename_Internalname ;
      private string edtavCtlcreate_Internalname ;
      private string subGridfiles_Class ;
      private string subGridfiles_Linesclass ;
      private string TempTags ;
      private string ROClassString ;
      private string edtavCtlfilename_Jsonclick ;
      private string edtavCtlcreate_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string sImgUrl ;
      private string edtavDeleteimage_Jsonclick ;
      private string subGridfiles_Header ;
      private string edtavDeleteimage_Link ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV24UserResponse ;
      private bool AV27fromDeleteFile ;
      private bool Fileupload_Autoupload ;
      private bool Fileupload_Hideadditionalbuttons ;
      private bool Fileupload_Autodisableaddingfiles ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_9_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool gx_BV9 ;
      private bool AV30deleteImage_IsBlob ;
      private string AV35Deleteimage_GXI ;
      private string AV31token ;
      private string AV30deleteImage ;
      private string AV22tempBlob ;
      private GXWebGrid GridfilesContainer ;
      private GXWebRow GridfilesRow ;
      private GXWebColumn GridfilesColumn ;
      private GXUserControl ucFileupload ;
      private GxFile AV17File ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> AV13encryptedFiles ;
      private GXBaseCollection<SdtFileUploadData> AV23UploadedFiles ;
      private GXBaseCollection<SdtFileUploadData> AV28FailedFiles ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV10downloadEncryptedFile ;
      private SdtFileUploadData AV18FileUploadData ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV12encryptedFile ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> GXt_objcol_SdtEncryptedFile1 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
