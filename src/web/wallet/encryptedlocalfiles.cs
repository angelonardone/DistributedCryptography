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
   public class encryptedlocalfiles : GXWebComponent
   {
      public encryptedlocalfiles( )
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
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrGridfiles_newrow_invoke( )
      {
         nRC_GXsfl_9 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_9"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_9_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_9_idx = GetPar( "sGXsfl_9_idx");
         sPrefix = GetPar( "sPrefix");
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
         ajax_req_read_hidden_sdt(GetNextPar( ), AV25wallet);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV20keyInfo);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridfiles_refresh( AV25wallet, AV20keyInfo, sPrefix) ;
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
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA2A2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlfilename_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlfilename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlfilename_Enabled), 5, 0), !bGXsfl_9_Refreshing);
               edtavCtlcreate_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcreate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcreate_Enabled), 5, 0), !bGXsfl_9_Refreshing);
               edtavDeletefile_Enabled = 0;
               AssignProp(sPrefix, false, edtavDeletefile_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDeletefile_Enabled), 5, 0), !bGXsfl_9_Refreshing);
               WS2A2( ) ;
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
            context.SendWebValue( "Encrypted Local Files") ;
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
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 123260), false, true);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.encryptedlocalfiles") +"\">") ;
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
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV25wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV25wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV25wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vKEYINFO", AV20keyInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vKEYINFO", AV20keyInfo);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vKEYINFO", GetSecureSignedToken( sPrefix, AV20keyInfo, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Encryptedfiles", AV13encryptedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Encryptedfiles", AV13encryptedFiles);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_9", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_9), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vUPLOADEDFILES", AV23UploadedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vUPLOADEDFILES", AV23UploadedFiles);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vFAILEDFILES", AV16FailedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vFAILEDFILES", AV16FailedFiles);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV25wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV25wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV25wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vKEYINFO", AV20keyInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vKEYINFO", AV20keyInfo);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vKEYINFO", GetSecureSignedToken( sPrefix, AV20keyInfo, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vENCRYPTEDFILE", AV12encryptedFile);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vENCRYPTEDFILE", AV12encryptedFile);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vENCRYPTEDFILES", AV13encryptedFiles);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vENCRYPTEDFILES", AV13encryptedFiles);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vUSERRESPONSE", AV24UserResponse);
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vFROMDELETEFILE", AV27fromDeleteFile);
         GxWebStd.gx_hidden_field( context, sPrefix+"vFILENAME", StringUtil.RTrim( AV26FileName));
         GxWebStd.gx_hidden_field( context, sPrefix+"vENCRYPTEDKEY", StringUtil.RTrim( AV14encryptedKey));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vDOWNLOADENCRYPTEDFILE", AV10downloadEncryptedFile);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vDOWNLOADENCRYPTEDFILE", AV10downloadEncryptedFile);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Autoupload", StringUtil.BoolToStr( Fileupload_Autoupload));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Hideadditionalbuttons", StringUtil.BoolToStr( Fileupload_Hideadditionalbuttons));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Maxfilesize", StringUtil.LTrim( StringUtil.NToC( (decimal)(Fileupload_Maxfilesize), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Maxnumberoffiles", StringUtil.LTrim( StringUtil.NToC( (decimal)(Fileupload_Maxnumberoffiles), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"FILEUPLOAD_Autodisableaddingfiles", StringUtil.BoolToStr( Fileupload_Autodisableaddingfiles));
      }

      protected void RenderHtmlCloseForm2A2( )
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
         return "Wallet.EncryptedLocalFiles" ;
      }

      public override string GetPgmdesc( )
      {
         return "Encrypted Local Files" ;
      }

      protected void WB2A0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.encryptedlocalfiles");
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
            /* User Defined Control */
            ucFileupload.SetProperty("AutoUpload", Fileupload_Autoupload);
            ucFileupload.SetProperty("HideAdditionalButtons", Fileupload_Hideadditionalbuttons);
            ucFileupload.SetProperty("TooltipText", Fileupload_Tooltiptext);
            ucFileupload.SetProperty("MaxNumberOfFiles", Fileupload_Maxnumberoffiles);
            ucFileupload.SetProperty("AutoDisableAddingFiles", Fileupload_Autodisableaddingfiles);
            ucFileupload.SetProperty("UploadedFiles", AV23UploadedFiles);
            ucFileupload.SetProperty("FailedFiles", AV16FailedFiles);
            ucFileupload.Render(context, "fileupload", Fileupload_Internalname, sPrefix+"FILEUPLOADContainer");
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
               AV28GXV1 = nGXsfl_9_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridfilesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridfiles", GridfilesContainer, subGridfiles_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridfilesContainerData", GridfilesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridfilesContainerData"+"V", GridfilesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridfilesContainerData"+"V"+"\" value='"+GridfilesContainer.GridValuesHidden()+"'/>") ;
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
                  AV28GXV1 = nGXsfl_9_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridfilesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridfiles", GridfilesContainer, subGridfiles_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridfilesContainerData", GridfilesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridfilesContainerData"+"V", GridfilesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridfilesContainerData"+"V"+"\" value='"+GridfilesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START2A2( )
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
            Form.Meta.addItem("description", "Encrypted Local Files", 0) ;
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
               STRUP2A0( ) ;
            }
         }
      }

      protected void WS2A2( )
      {
         START2A2( ) ;
         EVT2A2( ) ;
      }

      protected void EVT2A2( )
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
                                 STRUP2A0( ) ;
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
                                 STRUP2A0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Fileupload.Uploadcomplete */
                                    E112A2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2A0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E122A2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2A0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlfilename_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'DECRYPT AND DOWNLOAD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'DELETE FILE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "GRIDFILES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'DECRYPT AND DOWNLOAD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'DELETE FILE'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2A0( ) ;
                              }
                              nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
                              SubsflControlProps_92( ) ;
                              AV28GXV1 = nGXsfl_9_idx;
                              if ( ( AV13encryptedFiles.Count >= AV28GXV1 ) && ( AV28GXV1 > 0 ) )
                              {
                                 AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1));
                                 AV8deleteFile = cgiGet( edtavDeletefile_Internalname);
                                 AssignAttri(sPrefix, false, edtavDeletefile_Internalname, AV8deleteFile);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E132A2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E142A2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DECRYPT AND DOWNLOAD'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Decrypt and download' */
                                          E152A2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE FILE'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Delete File' */
                                          E162A2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDFILES.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridfiles.Load */
                                          E172A2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
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
                                       STRUP2A0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
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

      protected void WE2A2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm2A2( ) ;
            }
         }
      }

      protected void PA2A2( )
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

      protected void gxgrGridfiles_refresh( GeneXus.Programs.wallet.SdtWallet AV25wallet ,
                                            GeneXus.Programs.nbitcoin.SdtKeyInfo AV20keyInfo ,
                                            string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDFILES_nCurrentRecord = 0;
         RF2A2( ) ;
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
         RF2A2( ) ;
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
         edtavDeletefile_Enabled = 0;
      }

      protected void RF2A2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridfilesContainer.ClearRows();
         }
         wbStart = 9;
         /* Execute user event: Refresh */
         E142A2 ();
         nGXsfl_9_idx = 1;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         bGXsfl_9_Refreshing = true;
         GridfilesContainer.AddObjectProperty("GridName", "Gridfiles");
         GridfilesContainer.AddObjectProperty("CmpContext", sPrefix);
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
            E172A2 ();
            wbEnd = 9;
            WB2A0( ) ;
         }
         bGXsfl_9_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2A2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV25wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV25wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV25wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vKEYINFO", AV20keyInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vKEYINFO", AV20keyInfo);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vKEYINFO", GetSecureSignedToken( sPrefix, AV20keyInfo, context));
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
         edtavDeletefile_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2A0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E132A2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Encryptedfiles"), AV13encryptedFiles);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vUPLOADEDFILES"), AV23UploadedFiles);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vFAILEDFILES"), AV16FailedFiles);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vENCRYPTEDFILES"), AV13encryptedFiles);
            /* Read saved values. */
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            Fileupload_Autoupload = StringUtil.StrToBool( cgiGet( sPrefix+"FILEUPLOAD_Autoupload"));
            Fileupload_Hideadditionalbuttons = StringUtil.StrToBool( cgiGet( sPrefix+"FILEUPLOAD_Hideadditionalbuttons"));
            Fileupload_Maxfilesize = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"FILEUPLOAD_Maxfilesize"), ".", ","), 18, MidpointRounding.ToEven));
            Fileupload_Maxnumberoffiles = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"FILEUPLOAD_Maxnumberoffiles"), ".", ","), 18, MidpointRounding.ToEven));
            Fileupload_Autodisableaddingfiles = StringUtil.StrToBool( cgiGet( sPrefix+"FILEUPLOAD_Autodisableaddingfiles"));
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_9_fel_idx = 0;
            while ( nGXsfl_9_fel_idx < nRC_GXsfl_9 )
            {
               nGXsfl_9_fel_idx = ((subGridfiles_Islastpage==1)&&(nGXsfl_9_fel_idx+1>subGridfiles_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_fel_idx+1);
               sGXsfl_9_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_92( ) ;
               AV28GXV1 = nGXsfl_9_fel_idx;
               if ( ( AV13encryptedFiles.Count >= AV28GXV1 ) && ( AV28GXV1 > 0 ) )
               {
                  AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1));
                  AV8deleteFile = cgiGet( edtavDeletefile_Internalname);
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
         E132A2 ();
         if (returnInSub) return;
      }

      protected void E132A2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV16FailedFiles.Clear();
         GXt_SdtWallet1 = AV25wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV25wallet = GXt_SdtWallet1;
         GXt_SdtKeyInfo2 = AV20keyInfo;
         new GeneXus.Programs.wallet.getfileenckey(context ).execute( out  GXt_SdtKeyInfo2) ;
         AV20keyInfo = GXt_SdtKeyInfo2;
         Fileupload_Autodisableaddingfiles = false;
         ucFileupload.SendProperty(context, sPrefix, false, Fileupload_Internalname, "AutoDisableAddingFiles", StringUtil.BoolToStr( Fileupload_Autodisableaddingfiles));
         AV9directory.Source = AV25wallet.gxTpr_Walletbasedirectory+"Files";
         if ( ! AV9directory.Exists() )
         {
            AV9directory.Create();
         }
         Fileupload_Maxfilesize = 250000000;
         ucFileupload.SendProperty(context, sPrefix, false, Fileupload_Internalname, "MaxFileSize", StringUtil.LTrimStr( (decimal)(Fileupload_Maxfilesize), 9, 0));
      }

      protected void E142A2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         AV8deleteFile = "Delete";
         AssignAttri(sPrefix, false, edtavDeletefile_Internalname, AV8deleteFile);
         GXt_objcol_SdtEncryptedFile3 = AV13encryptedFiles;
         new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile3) ;
         AV13encryptedFiles = GXt_objcol_SdtEncryptedFile3;
         gx_BV9 = true;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13encryptedFiles", AV13encryptedFiles);
      }

      protected void E112A2( )
      {
         AV28GXV1 = nGXsfl_9_idx;
         if ( ( AV28GXV1 > 0 ) && ( AV13encryptedFiles.Count >= AV28GXV1 ) )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1));
         }
         /* Fileupload_Uploadcomplete Routine */
         returnInSub = false;
         AV31GXV4 = 1;
         while ( AV31GXV4 <= AV23UploadedFiles.Count )
         {
            AV18FileUploadData = ((SdtFileUploadData)AV23UploadedFiles.Item(AV31GXV4));
            AV22tempBlob = AV18FileUploadData.gxTpr_File;
            AV17File.Source = AV22tempBlob;
            AV21newRndName = Guid.NewGuid( ).ToString();
            AV9directory.Source = AV25wallet.gxTpr_Walletbasedirectory+"Files";
            GXt_boolean4 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
            GXt_boolean5 = false;
            new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
            AV11EncDestination = AV9directory.GetAbsoluteName() + (GXt_boolean5 ? "/" : "\\") + StringUtil.Trim( AV21newRndName);
            GXt_char6 = AV15error;
            new GeneXus.Programs.distributedcrypto.aesencryptfile(context ).execute(  AV17File.GetAbsoluteName(),  AV11EncDestination, out  AV5clearKey, out  AV19iv, out  GXt_char6) ;
            AV15error = GXt_char6;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
            {
               GXt_char6 = AV15error;
               new GeneXus.Programs.nbitcoin.eccenctrypt(context ).execute(  AV20keyInfo.gxTpr_Publickey,  AV5clearKey, out  AV14encryptedKey, out  GXt_char6) ;
               AssignAttri(sPrefix, false, "AV14encryptedKey", AV14encryptedKey);
               AV15error = GXt_char6;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
               {
                  AV12encryptedFile.gxTpr_Iv = AV19iv;
                  AV12encryptedFile.gxTpr_Encryptedkey = AV14encryptedKey;
                  AV12encryptedFile.gxTpr_Filename = AV18FileUploadData.gxTpr_Fullname;
                  AV12encryptedFile.gxTpr_Fullfilename = AV11EncDestination;
                  AV12encryptedFile.gxTpr_Create = DateTimeUtil.Now( context);
                  GXt_char6 = AV15error;
                  new GeneXus.Programs.wallet.insertintoallfiles(context ).execute(  AV12encryptedFile, out  GXt_char6) ;
                  AV15error = GXt_char6;
               }
               else
               {
                  GX_msglist.addItem(AV15error);
               }
               this.executeUsercontrolMethod(sPrefix, false, "FILEUPLOADContainer", "Clear", "", new Object[] {});
               AV17File.Delete();
            }
            else
            {
               GX_msglist.addItem(AV15error);
            }
            GXt_objcol_SdtEncryptedFile3 = AV13encryptedFiles;
            new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile3) ;
            AV13encryptedFiles = GXt_objcol_SdtEncryptedFile3;
            gx_BV9 = true;
            gxgrGridfiles_refresh( AV25wallet, AV20keyInfo, sPrefix) ;
            AV31GXV4 = (int)(AV31GXV4+1);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV12encryptedFile", AV12encryptedFile);
         if ( gx_BV9 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13encryptedFiles", AV13encryptedFiles);
            nGXsfl_9_bak_idx = nGXsfl_9_idx;
            gxgrGridfiles_refresh( AV25wallet, AV20keyInfo, sPrefix) ;
            nGXsfl_9_idx = nGXsfl_9_bak_idx;
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
      }

      protected void E152A2( )
      {
         AV28GXV1 = nGXsfl_9_idx;
         if ( ( AV28GXV1 > 0 ) && ( AV13encryptedFiles.Count >= AV28GXV1 ) )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1));
         }
         /* 'Decrypt and download' Routine */
         returnInSub = false;
         AV10downloadEncryptedFile = ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV13encryptedFiles.CurrentItem));
         AV27fromDeleteFile = false;
         AssignAttri(sPrefix, false, "AV27fromDeleteFile", AV27fromDeleteFile);
         this.executeExternalObjectMethod(sPrefix, false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to decrypt and download "+AV10downloadEncryptedFile.gxTpr_Filename+"?"}, false);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV10downloadEncryptedFile", AV10downloadEncryptedFile);
      }

      protected void E162A2( )
      {
         AV28GXV1 = nGXsfl_9_idx;
         if ( ( AV28GXV1 > 0 ) && ( AV13encryptedFiles.Count >= AV28GXV1 ) )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1));
         }
         /* 'Delete File' Routine */
         returnInSub = false;
         AV27fromDeleteFile = true;
         AssignAttri(sPrefix, false, "AV27fromDeleteFile", AV27fromDeleteFile);
         this.executeExternalObjectMethod(sPrefix, false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete "+((GeneXus.Programs.wallet.SdtEncryptedFile)(AV13encryptedFiles.CurrentItem)).gxTpr_Filename+" file?"}, false);
         AV14encryptedKey = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV13encryptedFiles.CurrentItem)).gxTpr_Encryptedkey);
         AssignAttri(sPrefix, false, "AV14encryptedKey", AV14encryptedKey);
         AV26FileName = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtEncryptedFile)(AV13encryptedFiles.CurrentItem)).gxTpr_Filename);
         AssignAttri(sPrefix, false, "AV26FileName", AV26FileName);
         /*  Sending Event outputs  */
      }

      protected void E122A2( )
      {
         AV28GXV1 = nGXsfl_9_idx;
         if ( ( AV28GXV1 > 0 ) && ( AV13encryptedFiles.Count >= AV28GXV1 ) )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV24UserResponse )
         {
            if ( AV27fromDeleteFile )
            {
               AV32GXV5 = 1;
               while ( AV32GXV5 <= AV13encryptedFiles.Count )
               {
                  AV12encryptedFile = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV32GXV5));
                  if ( ( StringUtil.StrCmp(StringUtil.Trim( AV12encryptedFile.gxTpr_Filename), AV26FileName) == 0 ) && ( StringUtil.StrCmp(StringUtil.Trim( AV12encryptedFile.gxTpr_Encryptedkey), AV14encryptedKey) == 0 ) )
                  {
                     AV13encryptedFiles.RemoveItem(AV13encryptedFiles.IndexOf(AV12encryptedFile));
                     gx_BV9 = true;
                     GXt_char6 = AV15error;
                     new GeneXus.Programs.wallet.deleteoneencryptelfiles(context ).execute(  AV12encryptedFile, out  GXt_char6) ;
                     AV15error = GXt_char6;
                     GXt_objcol_SdtEncryptedFile3 = AV13encryptedFiles;
                     new GeneXus.Programs.wallet.readallfiles(context ).execute( out  GXt_objcol_SdtEncryptedFile3) ;
                     AV13encryptedFiles = GXt_objcol_SdtEncryptedFile3;
                     gx_BV9 = true;
                  }
                  AV32GXV5 = (int)(AV32GXV5+1);
               }
            }
            else
            {
               GXt_char6 = AV15error;
               new GeneXus.Programs.nbitcoin.eccdecrypt(context ).execute(  AV20keyInfo.gxTpr_Privatekey,  AV10downloadEncryptedFile.gxTpr_Encryptedkey, out  AV5clearKey, out  GXt_char6) ;
               AV15error = GXt_char6;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
               {
                  AV7DecSource = AV10downloadEncryptedFile.gxTpr_Fullfilename;
                  AV9directory.Source = "PublicTempStorage";
                  GXt_boolean5 = false;
                  new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
                  GXt_boolean4 = false;
                  new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
                  AV6DecDestination = AV9directory.GetAbsoluteName() + (GXt_boolean4 ? "/" : "\\") + AV10downloadEncryptedFile.gxTpr_Filename;
                  GXt_char6 = AV15error;
                  new GeneXus.Programs.distributedcrypto.aesdecryptfile(context ).execute(  AV7DecSource,  AV6DecDestination,  AV5clearKey,  AV10downloadEncryptedFile.gxTpr_Iv, out  GXt_char6) ;
                  AV15error = GXt_char6;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV15error)) )
                  {
                     GXt_boolean5 = false;
                     new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean5) ;
                     GXt_boolean4 = false;
                     new GeneXus.Programs.wallet.isosunix(context ).execute( out  GXt_boolean4) ;
                     AV6DecDestination = "PublicTempStorage" + (GXt_boolean4 ? "/" : "\\") + AV10downloadEncryptedFile.gxTpr_Filename;
                     this.executeExternalObjectMethod(sPrefix, false, "gx.extensions.web.window", "open", new Object[] {(string)AV6DecDestination}, false);
                     new GeneXus.Programs.wallet.deletefilewithdelay(context).executeSubmit(  AV6DecDestination) ;
                  }
                  else
                  {
                     GX_msglist.addItem(AV15error);
                  }
               }
               else
               {
                  GX_msglist.addItem(AV15error);
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV12encryptedFile", AV12encryptedFile);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13encryptedFiles", AV13encryptedFiles);
         nGXsfl_9_bak_idx = nGXsfl_9_idx;
         gxgrGridfiles_refresh( AV25wallet, AV20keyInfo, sPrefix) ;
         nGXsfl_9_idx = nGXsfl_9_bak_idx;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
      }

      private void E172A2( )
      {
         /* Gridfiles_Load Routine */
         returnInSub = false;
         AV28GXV1 = 1;
         while ( AV28GXV1 <= AV13encryptedFiles.Count )
         {
            AV13encryptedFiles.CurrentItem = ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1));
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
            AV28GXV1 = (int)(AV28GXV1+1);
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
         PA2A2( ) ;
         WS2A2( ) ;
         WE2A2( ) ;
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
         PA2A2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\encryptedlocalfiles", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA2A2( ) ;
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
         PA2A2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS2A2( ) ;
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
         WS2A2( ) ;
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
         WE2A2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202541513131342", true, true);
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
         context.AddJavascriptSource("wallet/encryptedlocalfiles.js", "?202541513131342", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("FileUpload/fileupload.min.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_92( )
      {
         edtavCtlfilename_Internalname = sPrefix+"CTLFILENAME_"+sGXsfl_9_idx;
         edtavCtlcreate_Internalname = sPrefix+"CTLCREATE_"+sGXsfl_9_idx;
         edtavDeletefile_Internalname = sPrefix+"vDELETEFILE_"+sGXsfl_9_idx;
      }

      protected void SubsflControlProps_fel_92( )
      {
         edtavCtlfilename_Internalname = sPrefix+"CTLFILENAME_"+sGXsfl_9_fel_idx;
         edtavCtlcreate_Internalname = sPrefix+"CTLCREATE_"+sGXsfl_9_fel_idx;
         edtavDeletefile_Internalname = sPrefix+"vDELETEFILE_"+sGXsfl_9_fel_idx;
      }

      protected void sendrow_92( )
      {
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         WB2A0( ) ;
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
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridfilesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlfilename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1)).gxTpr_Filename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,10);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DECRYPT AND DOWNLOAD\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlfilename_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlfilename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridfilesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcreate_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1)).gxTpr_Create, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtEncryptedFile)AV13encryptedFiles.Item(AV28GXV1)).gxTpr_Create, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,11);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcreate_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcreate_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridfilesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridfilesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeletefile_Internalname,StringUtil.RTrim( AV8deleteFile),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,12);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DELETE FILE\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDeletefile_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavDeletefile_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes2A2( ) ;
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
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridfilesContainer"+"DivS\" data-gxgridid=\"9\">") ;
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
            GridfilesContainer.AddObjectProperty("CmpContext", sPrefix);
            GridfilesContainer.AddObjectProperty("InMasterPage", "false");
            GridfilesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridfilesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlfilename_Enabled), 5, 0, ".", "")));
            GridfilesContainer.AddColumnProperties(GridfilesColumn);
            GridfilesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridfilesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcreate_Enabled), 5, 0, ".", "")));
            GridfilesContainer.AddColumnProperties(GridfilesColumn);
            GridfilesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridfilesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV8deleteFile)));
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
         Fileupload_Internalname = sPrefix+"FILEUPLOAD";
         edtavCtlfilename_Internalname = sPrefix+"CTLFILENAME";
         edtavCtlcreate_Internalname = sPrefix+"CTLCREATE";
         edtavDeletefile_Internalname = sPrefix+"vDELETEFILE";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGridfiles_Internalname = sPrefix+"GRIDFILES";
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
         subGridfiles_Allowcollapsing = 0;
         subGridfiles_Allowselection = 0;
         subGridfiles_Header = "";
         edtavDeletefile_Jsonclick = "";
         edtavDeletefile_Enabled = 1;
         edtavCtlcreate_Jsonclick = "";
         edtavCtlcreate_Enabled = 0;
         edtavCtlfilename_Jsonclick = "";
         edtavCtlfilename_Enabled = 0;
         subGridfiles_Class = "Grid";
         subGridfiles_Backcolorstyle = 0;
         Fileupload_Tooltiptext = "";
         Fileupload_Autodisableaddingfiles = Convert.ToBoolean( -1);
         Fileupload_Maxnumberoffiles = 1;
         Fileupload_Maxfilesize = 134217728;
         Fileupload_Hideadditionalbuttons = Convert.ToBoolean( -1);
         Fileupload_Autoupload = Convert.ToBoolean( -1);
         edtavCtlcreate_Enabled = -1;
         edtavCtlfilename_Enabled = -1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"GRIDFILES_nEOF","type":"int"},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV25wallet","fld":"vWALLET","hsh":true,"type":""},{"av":"AV20keyInfo","fld":"vKEYINFO","hsh":true,"type":""}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV8deleteFile","fld":"vDELETEFILE","type":"char"},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("FILEUPLOAD.UPLOADCOMPLETE","""{"handler":"E112A2","iparms":[{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"GRIDFILES_nEOF","type":"int"},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV25wallet","fld":"vWALLET","hsh":true,"type":""},{"av":"AV20keyInfo","fld":"vKEYINFO","hsh":true,"type":""},{"av":"sPrefix","type":"char"},{"av":"AV23UploadedFiles","fld":"vUPLOADEDFILES","type":""},{"av":"AV12encryptedFile","fld":"vENCRYPTEDFILE","type":""}]""");
         setEventMetadata("FILEUPLOAD.UPLOADCOMPLETE",""","oparms":[{"av":"AV14encryptedKey","fld":"vENCRYPTEDKEY","type":"char"},{"av":"AV12encryptedFile","fld":"vENCRYPTEDFILE","type":""},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV8deleteFile","fld":"vDELETEFILE","type":"char"}]}""");
         setEventMetadata("'DECRYPT AND DOWNLOAD'","""{"handler":"E152A2","iparms":[{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"}]""");
         setEventMetadata("'DECRYPT AND DOWNLOAD'",""","oparms":[{"av":"AV10downloadEncryptedFile","fld":"vDOWNLOADENCRYPTEDFILE","type":""},{"av":"AV27fromDeleteFile","fld":"vFROMDELETEFILE","type":"boolean"}]}""");
         setEventMetadata("'DELETE FILE'","""{"handler":"E162A2","iparms":[{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"}]""");
         setEventMetadata("'DELETE FILE'",""","oparms":[{"av":"AV27fromDeleteFile","fld":"vFROMDELETEFILE","type":"boolean"},{"av":"AV14encryptedKey","fld":"vENCRYPTEDKEY","type":"char"},{"av":"AV26FileName","fld":"vFILENAME","type":"char"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E122A2","iparms":[{"av":"AV24UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV27fromDeleteFile","fld":"vFROMDELETEFILE","type":"boolean"},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"},{"av":"AV26FileName","fld":"vFILENAME","type":"char"},{"av":"AV14encryptedKey","fld":"vENCRYPTEDKEY","type":"char"},{"av":"AV20keyInfo","fld":"vKEYINFO","hsh":true,"type":""},{"av":"AV10downloadEncryptedFile","fld":"vDOWNLOADENCRYPTEDFILE","type":""},{"av":"GRIDFILES_nEOF","type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV25wallet","fld":"vWALLET","hsh":true,"type":""}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV12encryptedFile","fld":"vENCRYPTEDFILE","type":""},{"av":"AV13encryptedFiles","fld":"vENCRYPTEDFILES","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDFILES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDFILES","prop":"GridRC","grid":9,"type":"int"}]}""");
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
         sPrefix = "";
         AV25wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV20keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV13encryptedFiles = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         AV23UploadedFiles = new GXBaseCollection<SdtFileUploadData>( context, "FileUploadData", "distributedcryptography");
         AV16FailedFiles = new GXBaseCollection<SdtFileUploadData>( context, "FileUploadData", "distributedcryptography");
         AV12encryptedFile = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
         AV26FileName = "";
         AV14encryptedKey = "";
         AV10downloadEncryptedFile = new GeneXus.Programs.wallet.SdtEncryptedFile(context);
         GX_FocusControl = "";
         ucFileupload = new GXUserControl();
         GridfilesContainer = new GXWebGrid( context);
         sStyleString = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV8deleteFile = "";
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV9directory = new GxDirectory(context.GetPhysicalPath());
         AV18FileUploadData = new SdtFileUploadData(context);
         AV22tempBlob = "";
         AV17File = new GxFile(context.GetPhysicalPath());
         AV21newRndName = "";
         AV11EncDestination = "";
         AV15error = "";
         AV5clearKey = "";
         AV19iv = "";
         GXt_objcol_SdtEncryptedFile3 = new GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile>( context, "EncryptedFile", "distributedcryptography");
         AV7DecSource = "";
         AV6DecDestination = "";
         GXt_char6 = "";
         GridfilesRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridfiles_Linesclass = "";
         TempTags = "";
         ROClassString = "";
         GridfilesColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlfilename_Enabled = 0;
         edtavCtlcreate_Enabled = 0;
         edtavDeletefile_Enabled = 0;
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
      private int edtavCtlfilename_Enabled ;
      private int edtavCtlcreate_Enabled ;
      private int edtavDeletefile_Enabled ;
      private int Fileupload_Maxfilesize ;
      private int Fileupload_Maxnumberoffiles ;
      private int AV28GXV1 ;
      private int subGridfiles_Islastpage ;
      private int nGXsfl_9_fel_idx=1 ;
      private int AV31GXV4 ;
      private int nGXsfl_9_bak_idx=1 ;
      private int AV32GXV5 ;
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
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_9_idx="0001" ;
      private string edtavCtlfilename_Internalname ;
      private string edtavCtlcreate_Internalname ;
      private string edtavDeletefile_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV26FileName ;
      private string AV14encryptedKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string Fileupload_Tooltiptext ;
      private string Fileupload_Internalname ;
      private string sStyleString ;
      private string subGridfiles_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV8deleteFile ;
      private string sGXsfl_9_fel_idx="0001" ;
      private string AV21newRndName ;
      private string AV11EncDestination ;
      private string AV15error ;
      private string AV5clearKey ;
      private string AV19iv ;
      private string AV7DecSource ;
      private string AV6DecDestination ;
      private string GXt_char6 ;
      private string subGridfiles_Class ;
      private string subGridfiles_Linesclass ;
      private string TempTags ;
      private string ROClassString ;
      private string edtavCtlfilename_Jsonclick ;
      private string edtavCtlcreate_Jsonclick ;
      private string edtavDeletefile_Jsonclick ;
      private string subGridfiles_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_9_Refreshing=false ;
      private bool AV24UserResponse ;
      private bool AV27fromDeleteFile ;
      private bool Fileupload_Autoupload ;
      private bool Fileupload_Hideadditionalbuttons ;
      private bool Fileupload_Autodisableaddingfiles ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool gx_BV9 ;
      private bool GXt_boolean5 ;
      private bool GXt_boolean4 ;
      private string AV22tempBlob ;
      private GXWebGrid GridfilesContainer ;
      private GXWebRow GridfilesRow ;
      private GXWebColumn GridfilesColumn ;
      private GXUserControl ucFileupload ;
      private GXWebForm Form ;
      private GxFile AV17File ;
      private GxDirectory AV9directory ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.SdtWallet AV25wallet ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV20keyInfo ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> AV13encryptedFiles ;
      private GXBaseCollection<SdtFileUploadData> AV23UploadedFiles ;
      private GXBaseCollection<SdtFileUploadData> AV16FailedFiles ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV12encryptedFile ;
      private GeneXus.Programs.wallet.SdtEncryptedFile AV10downloadEncryptedFile ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private SdtFileUploadData AV18FileUploadData ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtEncryptedFile> GXt_objcol_SdtEncryptedFile3 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
