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
namespace GeneXus.Programs.wallet.registered {
   public class largefiles : GXWebComponent
   {
      public largefiles( )
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

      public largefiles( IGxContext context )
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridreceived") == 0 )
               {
                  gxnrGridreceived_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridreceived") == 0 )
               {
                  gxgrGridreceived_refresh_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridtoencrypt") == 0 )
               {
                  gxnrGridtoencrypt_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridtoencrypt") == 0 )
               {
                  gxgrGridtoencrypt_refresh_invoke( ) ;
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

      protected void gxnrGridreceived_newrow_invoke( )
      {
         nRC_GXsfl_16 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_16"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_16_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_16_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_16_idx = GetPar( "sGXsfl_16_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridreceived_newrow( ) ;
         /* End function gxnrGridreceived_newrow_invoke */
      }

      protected void gxgrGridreceived_refresh_invoke( )
      {
         AV9help = GetPar( "help");
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridreceived_refresh( AV9help, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridreceived_refresh_invoke */
      }

      protected void gxnrGridtoencrypt_newrow_invoke( )
      {
         nRC_GXsfl_32 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_32"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_32_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_32_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_32_idx = GetPar( "sGXsfl_32_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridtoencrypt_newrow( ) ;
         /* End function gxnrGridtoencrypt_newrow_invoke */
      }

      protected void gxgrGridtoencrypt_refresh_invoke( )
      {
         AV9help = GetPar( "help");
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridtoencrypt_refresh( AV9help, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridtoencrypt_refresh_invoke */
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
            PA3G2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlrecfilename_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlrecfilename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlrecfilename_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtlrecfilesize_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlrecfilesize_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlrecfilesize_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtlrecmodified_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlrecmodified_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlrecmodified_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavDecryptaction_Enabled = 0;
               AssignProp(sPrefix, false, edtavDecryptaction_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDecryptaction_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtlencfilename_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlencfilename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlencfilename_Enabled), 5, 0), !bGXsfl_32_Refreshing);
               edtavCtlencfilesize_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlencfilesize_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlencfilesize_Enabled), 5, 0), !bGXsfl_32_Refreshing);
               edtavCtlencmodified_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlencmodified_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlencmodified_Enabled), 5, 0), !bGXsfl_32_Refreshing);
               edtavEncryptaction_Enabled = 0;
               AssignProp(sPrefix, false, edtavEncryptaction_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEncryptaction_Enabled), 5, 0), !bGXsfl_32_Refreshing);
               WS3G2( ) ;
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
            context.SendWebValue( "Large Files") ;
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.largefiles") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"vHELP", AV9help);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vHELP", GetSecureSignedToken( sPrefix, AV9help, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Received", AV11received);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Received", AV11received);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Toencrypt", AV14toEncrypt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Toencrypt", AV14toEncrypt);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_16", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_16), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_32", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_32), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vHELP", AV9help);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vHELP", GetSecureSignedToken( sPrefix, AV9help, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vRECEIVED", AV11received);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vRECEIVED", AV11received);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vTOENCRYPT", AV14toEncrypt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vTOENCRYPT", AV14toEncrypt);
         }
      }

      protected void RenderHtmlCloseForm3G2( )
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
         return "Wallet.registered.LargeFiles" ;
      }

      public override string GetPgmdesc( )
      {
         return "Large Files" ;
      }

      protected void WB3G0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.largefiles");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbhelp_Internalname, lblTbhelp_Caption, "", "", lblTbhelp_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_Wallet/registered/LargeFiles.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 9,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRefreshlists_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Refresh lists", bttRefreshlists_Jsonclick, 5, "Refresh lists", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'REFRESH LISTS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/LargeFiles.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Control Group */
            GxWebStd.gx_group_start( context, grpGroupreceived_Internalname, "Received - files to decrypt", 1, 0, "px", 0, "px", "Group", "", "HLP_Wallet/registered/LargeFiles.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGroupreceivedtable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridreceivedContainer.SetWrapped(nGXWrapped);
            StartGridControl16( ) ;
         }
         if ( wbEnd == 16 )
         {
            wbEnd = 0;
            nRC_GXsfl_16 = (int)(nGXsfl_16_idx-1);
            if ( GridreceivedContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV15GXV1 = nGXsfl_16_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridreceivedContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridreceived", GridreceivedContainer, subGridreceived_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridreceivedContainerData", GridreceivedContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridreceivedContainerData"+"V", GridreceivedContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridreceivedContainerData"+"V"+"\" value='"+GridreceivedContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            GxWebStd.gx_group_start( context, grpGrouptoencrypt_Internalname, "To encrypt - files to send", 1, 0, "px", 0, "px", "Group", "", "HLP_Wallet/registered/LargeFiles.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGrouptoencrypttable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRecipientusername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRecipientusername_Internalname, "Recipient user name", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavRecipientusername_Internalname, AV12recipientUserName, StringUtil.RTrim( context.localUtil.Format( AV12recipientUserName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavRecipientusername_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavRecipientusername_Enabled, 0, "text", "", 80, "chr", 1, "row", 80, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/registered/LargeFiles.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridtoencryptContainer.SetWrapped(nGXWrapped);
            StartGridControl32( ) ;
         }
         if ( wbEnd == 32 )
         {
            wbEnd = 0;
            nRC_GXsfl_32 = (int)(nGXsfl_32_idx-1);
            if ( GridtoencryptContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV19GXV5 = nGXsfl_32_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridtoencryptContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridtoencrypt", GridtoencryptContainer, subGridtoencrypt_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridtoencryptContainerData", GridtoencryptContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridtoencryptContainerData"+"V", GridtoencryptContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridtoencryptContainerData"+"V"+"\" value='"+GridtoencryptContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            GxWebStd.gx_group_start( context, grpGroupfolder_Internalname, "File exchange folder of this wallet", 1, 0, "px", 0, "px", "Group", "", "HLP_Wallet/registered/LargeFiles.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGroupfoldertable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavNewexchangedir_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavNewexchangedir_Internalname, "Folder", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 45,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavNewexchangedir_Internalname, AV10newExchangeDir, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,45);\"", 0, 1, edtavNewexchangedir_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "1024", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/LargeFiles.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 48,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttChangefolder_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Change folder", bttChangefolder_Jsonclick, 5, "Change folder", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CHANGE FOLDER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/LargeFiles.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 50,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttDefaultfolder_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Use the default folder", bttDefaultfolder_Jsonclick, 5, "Use the default folder", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DEFAULT FOLDER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/LargeFiles.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</fieldset>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 16 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridreceivedContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV15GXV1 = nGXsfl_16_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridreceivedContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridreceived", GridreceivedContainer, subGridreceived_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridreceivedContainerData", GridreceivedContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridreceivedContainerData"+"V", GridreceivedContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridreceivedContainerData"+"V"+"\" value='"+GridreceivedContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         if ( wbEnd == 32 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridtoencryptContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV19GXV5 = nGXsfl_32_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridtoencryptContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridtoencrypt", GridtoencryptContainer, subGridtoencrypt_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridtoencryptContainerData", GridtoencryptContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridtoencryptContainerData"+"V", GridtoencryptContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridtoencryptContainerData"+"V"+"\" value='"+GridtoencryptContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3G2( )
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
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
               }
            }
            Form.Meta.addItem("description", "Large Files", 0) ;
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
               STRUP3G0( ) ;
            }
         }
      }

      protected void WS3G2( )
      {
         START3G2( ) ;
         EVT3G2( ) ;
      }

      protected void EVT3G2( )
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
                                 STRUP3G0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "'REFRESH LISTS'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3G0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Refresh lists' */
                                    E113G2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CHANGE FOLDER'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3G0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Change folder' */
                                    E123G2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DEFAULT FOLDER'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3G0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Default folder' */
                                    E133G2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3G0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlrecfilename_Internalname;
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "GRIDRECEIVED.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'DECRYPT FILE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'DECRYPT FILE'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3G0( ) ;
                              }
                              nGXsfl_16_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
                              SubsflControlProps_162( ) ;
                              AV15GXV1 = nGXsfl_16_idx;
                              if ( ( AV11received.Count >= AV15GXV1 ) && ( AV15GXV1 > 0 ) )
                              {
                                 AV11received.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1));
                                 AV5decryptAction = cgiGet( edtavDecryptaction_Internalname);
                                 AssignAttri(sPrefix, false, edtavDecryptaction_Internalname, AV5decryptAction);
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
                                          GX_FocusControl = edtavCtlrecfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E143G2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDRECEIVED.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlrecfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridreceived.Load */
                                          E153G2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DECRYPT FILE'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlrecfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Decrypt file' */
                                          E163G2 ();
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
                                       STRUP3G0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlrecfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
                                 }
                              }
                              else
                              {
                              }
                           }
                           else if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "GRIDTOENCRYPT.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'ENCRYPT FILE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'ENCRYPT FILE'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3G0( ) ;
                              }
                              nGXsfl_32_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
                              SubsflControlProps_323( ) ;
                              AV19GXV5 = nGXsfl_32_idx;
                              if ( ( AV14toEncrypt.Count >= AV19GXV5 ) && ( AV19GXV5 > 0 ) )
                              {
                                 AV14toEncrypt.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5));
                                 AV6encryptAction = cgiGet( edtavEncryptaction_Internalname);
                                 AssignAttri(sPrefix, false, edtavEncryptaction_Internalname, AV6encryptAction);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "GRIDTOENCRYPT.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlencfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridtoencrypt.Load */
                                          E173G3 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'ENCRYPT FILE'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlencfilename_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Encrypt file' */
                                          E183G2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP3G0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlrecfilename_Internalname;
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

      protected void WE3G2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3G2( ) ;
            }
         }
      }

      protected void PA3G2( )
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
               GX_FocusControl = edtavRecipientusername_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridreceived_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_162( ) ;
         while ( nGXsfl_16_idx <= nRC_GXsfl_16 )
         {
            sendrow_162( ) ;
            nGXsfl_16_idx = ((subGridreceived_Islastpage==1)&&(nGXsfl_16_idx+1>subGridreceived_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_idx+1);
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridreceivedContainer)) ;
         /* End function gxnrGridreceived_newrow */
      }

      protected void gxnrGridtoencrypt_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_323( ) ;
         while ( nGXsfl_32_idx <= nRC_GXsfl_32 )
         {
            sendrow_323( ) ;
            nGXsfl_32_idx = ((subGridtoencrypt_Islastpage==1)&&(nGXsfl_32_idx+1>subGridtoencrypt_fnc_Recordsperpage( )) ? 1 : nGXsfl_32_idx+1);
            sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
            SubsflControlProps_323( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridtoencryptContainer)) ;
         /* End function gxnrGridtoencrypt_newrow */
      }

      protected void gxgrGridreceived_refresh( string AV9help ,
                                               string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDRECEIVED_nCurrentRecord = 0;
         RF3G2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridreceived_refresh */
      }

      protected void gxgrGridtoencrypt_refresh( string AV9help ,
                                                string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDTOENCRYPT_nCurrentRecord = 0;
         RF3G3( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridtoencrypt_refresh */
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
         RF3G2( ) ;
         RF3G3( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlrecfilename_Enabled = 0;
         edtavCtlrecfilesize_Enabled = 0;
         edtavCtlrecmodified_Enabled = 0;
         edtavDecryptaction_Enabled = 0;
         edtavCtlencfilename_Enabled = 0;
         edtavCtlencfilesize_Enabled = 0;
         edtavCtlencmodified_Enabled = 0;
         edtavEncryptaction_Enabled = 0;
      }

      protected void RF3G2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridreceivedContainer.ClearRows();
         }
         wbStart = 16;
         nGXsfl_16_idx = 1;
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         bGXsfl_16_Refreshing = true;
         GridreceivedContainer.AddObjectProperty("GridName", "Gridreceived");
         GridreceivedContainer.AddObjectProperty("CmpContext", sPrefix);
         GridreceivedContainer.AddObjectProperty("InMasterPage", "false");
         GridreceivedContainer.AddObjectProperty("Class", "Grid");
         GridreceivedContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridreceivedContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridreceivedContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Backcolorstyle), 1, 0, ".", "")));
         GridreceivedContainer.PageSize = subGridreceived_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_162( ) ;
            /* Execute user event: Gridreceived.Load */
            E153G2 ();
            wbEnd = 16;
            WB3G0( ) ;
         }
         bGXsfl_16_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3G2( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vHELP", AV9help);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vHELP", GetSecureSignedToken( sPrefix, AV9help, context));
      }

      protected void RF3G3( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridtoencryptContainer.ClearRows();
         }
         wbStart = 32;
         nGXsfl_32_idx = 1;
         sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
         SubsflControlProps_323( ) ;
         bGXsfl_32_Refreshing = true;
         GridtoencryptContainer.AddObjectProperty("GridName", "Gridtoencrypt");
         GridtoencryptContainer.AddObjectProperty("CmpContext", sPrefix);
         GridtoencryptContainer.AddObjectProperty("InMasterPage", "false");
         GridtoencryptContainer.AddObjectProperty("Class", "Grid");
         GridtoencryptContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridtoencryptContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridtoencryptContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Backcolorstyle), 1, 0, ".", "")));
         GridtoencryptContainer.PageSize = subGridtoencrypt_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_323( ) ;
            /* Execute user event: Gridtoencrypt.Load */
            E173G3 ();
            wbEnd = 32;
            WB3G0( ) ;
         }
         bGXsfl_32_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3G3( )
      {
      }

      protected int subGridreceived_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridreceived_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridreceived_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridreceived_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridtoencrypt_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridtoencrypt_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridtoencrypt_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridtoencrypt_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlrecfilename_Enabled = 0;
         edtavCtlrecfilesize_Enabled = 0;
         edtavCtlrecmodified_Enabled = 0;
         edtavDecryptaction_Enabled = 0;
         edtavCtlencfilename_Enabled = 0;
         edtavCtlencfilesize_Enabled = 0;
         edtavCtlencmodified_Enabled = 0;
         edtavEncryptaction_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3G0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E143G2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Received"), AV11received);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Toencrypt"), AV14toEncrypt);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vRECEIVED"), AV11received);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vTOENCRYPT"), AV14toEncrypt);
            /* Read saved values. */
            nRC_GXsfl_16 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_16"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_32 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_32"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_16 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_16"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_16_fel_idx = 0;
            while ( nGXsfl_16_fel_idx < nRC_GXsfl_16 )
            {
               nGXsfl_16_fel_idx = ((subGridreceived_Islastpage==1)&&(nGXsfl_16_fel_idx+1>subGridreceived_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_fel_idx+1);
               sGXsfl_16_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_162( ) ;
               AV15GXV1 = nGXsfl_16_fel_idx;
               if ( ( AV11received.Count >= AV15GXV1 ) && ( AV15GXV1 > 0 ) )
               {
                  AV11received.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1));
                  AV5decryptAction = cgiGet( edtavDecryptaction_Internalname);
               }
            }
            if ( nGXsfl_16_fel_idx == 0 )
            {
               nGXsfl_16_idx = 1;
               sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
               SubsflControlProps_162( ) ;
            }
            nGXsfl_16_fel_idx = 1;
            nRC_GXsfl_32 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_32"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_32_fel_idx = 0;
            while ( nGXsfl_32_fel_idx < nRC_GXsfl_32 )
            {
               nGXsfl_32_fel_idx = ((subGridtoencrypt_Islastpage==1)&&(nGXsfl_32_fel_idx+1>subGridtoencrypt_fnc_Recordsperpage( )) ? 1 : nGXsfl_32_fel_idx+1);
               sGXsfl_32_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_323( ) ;
               AV19GXV5 = nGXsfl_32_fel_idx;
               if ( ( AV14toEncrypt.Count >= AV19GXV5 ) && ( AV19GXV5 > 0 ) )
               {
                  AV14toEncrypt.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5));
                  AV6encryptAction = cgiGet( edtavEncryptaction_Internalname);
               }
            }
            if ( nGXsfl_32_fel_idx == 0 )
            {
               nGXsfl_32_idx = 1;
               sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
               SubsflControlProps_323( ) ;
            }
            nGXsfl_32_fel_idx = 1;
            /* Read variables values. */
            AV12recipientUserName = cgiGet( edtavRecipientusername_Internalname);
            AssignAttri(sPrefix, false, "AV12recipientUserName", AV12recipientUserName);
            AV10newExchangeDir = cgiGet( edtavNewexchangedir_Internalname);
            AssignAttri(sPrefix, false, "AV10newExchangeDir", AV10newExchangeDir);
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
         E143G2 ();
         if (returnInSub) return;
      }

      protected void E143G2( )
      {
         /* Start Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOAD' */
         S112 ();
         if (returnInSub) return;
      }

      protected void E113G2( )
      {
         AV15GXV1 = nGXsfl_16_idx;
         if ( ( AV15GXV1 > 0 ) && ( AV11received.Count >= AV15GXV1 ) )
         {
            AV11received.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1));
         }
         AV19GXV5 = nGXsfl_32_idx;
         if ( ( AV19GXV5 > 0 ) && ( AV14toEncrypt.Count >= AV19GXV5 ) )
         {
            AV14toEncrypt.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5));
         }
         /* 'Refresh lists' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOAD' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         if ( gx_BV16 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11received", AV11received);
            nGXsfl_16_bak_idx = nGXsfl_16_idx;
            gxgrGridreceived_refresh( AV9help, sPrefix) ;
            nGXsfl_16_idx = nGXsfl_16_bak_idx;
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
         if ( gx_BV32 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14toEncrypt", AV14toEncrypt);
            nGXsfl_32_bak_idx = nGXsfl_32_idx;
            gxgrGridtoencrypt_refresh( AV9help, sPrefix) ;
            nGXsfl_32_idx = nGXsfl_32_bak_idx;
            sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
            SubsflControlProps_323( ) ;
         }
      }

      private void E153G2( )
      {
         /* Gridreceived_Load Routine */
         returnInSub = false;
         AV15GXV1 = 1;
         while ( AV15GXV1 <= AV11received.Count )
         {
            AV11received.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1));
            AV5decryptAction = "Decrypt";
            AssignAttri(sPrefix, false, edtavDecryptaction_Internalname, AV5decryptAction);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 16;
            }
            sendrow_162( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_16_Refreshing )
            {
               DoAjaxLoad(16, GridreceivedRow);
            }
            AV15GXV1 = (int)(AV15GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E163G2( )
      {
         AV19GXV5 = nGXsfl_32_idx;
         if ( ( AV19GXV5 > 0 ) && ( AV14toEncrypt.Count >= AV19GXV5 ) )
         {
            AV14toEncrypt.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5));
         }
         AV15GXV1 = nGXsfl_16_idx;
         if ( ( AV15GXV1 > 0 ) && ( AV11received.Count >= AV15GXV1 ) )
         {
            AV11received.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1));
         }
         /* 'Decrypt file' Routine */
         returnInSub = false;
         GXt_char1 = AV7error;
         new GeneXus.Programs.wallet.decryptexchangefile(context ).execute(  ((GeneXus.Programs.wallet.SdtExchangeFile)(AV11received.CurrentItem)).gxTpr_Filename, out  AV13resultPath, out  GXt_char1) ;
         AV7error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"File decrypted: ",(string)AV13resultPath}, true);
         }
         else
         {
            GX_msglist.addItem(AV7error);
         }
         /* Execute user subroutine: 'LOAD' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11received", AV11received);
         nGXsfl_16_bak_idx = nGXsfl_16_idx;
         gxgrGridreceived_refresh( AV9help, sPrefix) ;
         nGXsfl_16_idx = nGXsfl_16_bak_idx;
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         if ( gx_BV32 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14toEncrypt", AV14toEncrypt);
            nGXsfl_32_bak_idx = nGXsfl_32_idx;
            gxgrGridtoencrypt_refresh( AV9help, sPrefix) ;
            nGXsfl_32_idx = nGXsfl_32_bak_idx;
            sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
            SubsflControlProps_323( ) ;
         }
      }

      protected void E183G2( )
      {
         AV15GXV1 = nGXsfl_16_idx;
         if ( ( AV15GXV1 > 0 ) && ( AV11received.Count >= AV15GXV1 ) )
         {
            AV11received.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1));
         }
         AV19GXV5 = nGXsfl_32_idx;
         if ( ( AV19GXV5 > 0 ) && ( AV14toEncrypt.Count >= AV19GXV5 ) )
         {
            AV14toEncrypt.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5));
         }
         /* 'Encrypt file' Routine */
         returnInSub = false;
         GXt_char1 = AV7error;
         new GeneXus.Programs.wallet.encryptexchangefile(context ).execute(  ((GeneXus.Programs.wallet.SdtExchangeFile)(AV14toEncrypt.CurrentItem)).gxTpr_Filename,  AV12recipientUserName, out  AV13resultPath, out  GXt_char1) ;
         AV7error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"File encrypted: ",(string)AV13resultPath}, true);
         }
         else
         {
            GX_msglist.addItem(AV7error);
         }
         /* Execute user subroutine: 'LOAD' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         if ( gx_BV16 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11received", AV11received);
            nGXsfl_16_bak_idx = nGXsfl_16_idx;
            gxgrGridreceived_refresh( AV9help, sPrefix) ;
            nGXsfl_16_idx = nGXsfl_16_bak_idx;
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14toEncrypt", AV14toEncrypt);
         nGXsfl_32_bak_idx = nGXsfl_32_idx;
         gxgrGridtoencrypt_refresh( AV9help, sPrefix) ;
         nGXsfl_32_idx = nGXsfl_32_bak_idx;
         sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
         SubsflControlProps_323( ) ;
      }

      protected void E123G2( )
      {
         AV15GXV1 = nGXsfl_16_idx;
         if ( ( AV15GXV1 > 0 ) && ( AV11received.Count >= AV15GXV1 ) )
         {
            AV11received.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1));
         }
         AV19GXV5 = nGXsfl_32_idx;
         if ( ( AV19GXV5 > 0 ) && ( AV14toEncrypt.Count >= AV19GXV5 ) )
         {
            AV14toEncrypt.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5));
         }
         /* 'Change folder' Routine */
         returnInSub = false;
         GXt_char1 = AV7error;
         new GeneXus.Programs.wallet.setexchangedir(context ).execute(  AV10newExchangeDir, out  GXt_char1) ;
         AV7error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Folder changed",(string)""}, true);
         }
         else
         {
            GX_msglist.addItem(AV7error);
         }
         /* Execute user subroutine: 'LOAD' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         if ( gx_BV16 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11received", AV11received);
            nGXsfl_16_bak_idx = nGXsfl_16_idx;
            gxgrGridreceived_refresh( AV9help, sPrefix) ;
            nGXsfl_16_idx = nGXsfl_16_bak_idx;
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
         if ( gx_BV32 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14toEncrypt", AV14toEncrypt);
            nGXsfl_32_bak_idx = nGXsfl_32_idx;
            gxgrGridtoencrypt_refresh( AV9help, sPrefix) ;
            nGXsfl_32_idx = nGXsfl_32_bak_idx;
            sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
            SubsflControlProps_323( ) ;
         }
      }

      protected void E133G2( )
      {
         AV15GXV1 = nGXsfl_16_idx;
         if ( ( AV15GXV1 > 0 ) && ( AV11received.Count >= AV15GXV1 ) )
         {
            AV11received.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1));
         }
         AV19GXV5 = nGXsfl_32_idx;
         if ( ( AV19GXV5 > 0 ) && ( AV14toEncrypt.Count >= AV19GXV5 ) )
         {
            AV14toEncrypt.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5));
         }
         /* 'Default folder' Routine */
         returnInSub = false;
         GXt_char1 = AV7error;
         new GeneXus.Programs.wallet.setexchangedir(context ).execute(  "", out  GXt_char1) ;
         AV7error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            GX_msglist.addItem(AV7error);
         }
         /* Execute user subroutine: 'LOAD' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         if ( gx_BV16 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11received", AV11received);
            nGXsfl_16_bak_idx = nGXsfl_16_idx;
            gxgrGridreceived_refresh( AV9help, sPrefix) ;
            nGXsfl_16_idx = nGXsfl_16_bak_idx;
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
         if ( gx_BV32 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14toEncrypt", AV14toEncrypt);
            nGXsfl_32_bak_idx = nGXsfl_32_idx;
            gxgrGridtoencrypt_refresh( AV9help, sPrefix) ;
            nGXsfl_32_idx = nGXsfl_32_bak_idx;
            sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
            SubsflControlProps_323( ) ;
         }
      }

      protected void S112( )
      {
         /* 'LOAD' Routine */
         returnInSub = false;
         AV8exchangeDir = "";
         new GeneXus.Programs.wallet.getexchangedir(context ).execute( out  AV8exchangeDir, out  AV7error) ;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            GX_msglist.addItem(AV7error);
         }
         AV10newExchangeDir = AV8exchangeDir;
         AssignAttri(sPrefix, false, "AV10newExchangeDir", AV10newExchangeDir);
         GXt_objcol_SdtExchangeFile2 = AV11received;
         new GeneXus.Programs.wallet.listexchangefiles(context ).execute(  "Received", out  GXt_objcol_SdtExchangeFile2) ;
         AV11received = GXt_objcol_SdtExchangeFile2;
         gx_BV16 = true;
         GXt_objcol_SdtExchangeFile2 = AV14toEncrypt;
         new GeneXus.Programs.wallet.listexchangefiles(context ).execute(  "ToEncrypt", out  GXt_objcol_SdtExchangeFile2) ;
         AV14toEncrypt = GXt_objcol_SdtExchangeFile2;
         gx_BV32 = true;
         /* User Code */
          string d = AV8exchangeDir.Trim(), sep = System.IO.Path.DirectorySeparatorChar.ToString();
         /* User Code */
          System.Func<string, string> b = s => "<b>" + System.Net.WebUtility.HtmlEncode(d + sep + s) + "</b>";
         /* User Code */
          AV9help = "<p>Use this page for files <b>over 1 GB</b> (or whenever you prefer not to upload them): the files are read and written directly in the file exchange folder of this wallet.</p>" +
         /* User Code */
              "<p><b>To open a file you received</b>: copy the .dcf file to " + b("Received") + ", press <i>Refresh lists</i>, then <i>Decrypt</i> on its row. The original file appears in " + b("Decrypted") + ".</p>" +
         /* User Code */
              "<p><b>To send a file</b>: copy it to " + b("To encrypt") + ", type the user name of the recipient, press <i>Refresh lists</i>, then <i>Encrypt</i> on its row. The encrypted .dcf appears in " + b("To send") + ": send it to the recipient by any channel; only the recipient can open it.</p>";
         lblTbhelp_Caption = AV9help;
         AssignProp(sPrefix, false, lblTbhelp_Internalname, "Caption", lblTbhelp_Caption, true);
      }

      private void E173G3( )
      {
         /* Gridtoencrypt_Load Routine */
         returnInSub = false;
         AV19GXV5 = 1;
         while ( AV19GXV5 <= AV14toEncrypt.Count )
         {
            AV14toEncrypt.CurrentItem = ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5));
            AV6encryptAction = "Encrypt";
            AssignAttri(sPrefix, false, edtavEncryptaction_Internalname, AV6encryptAction);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 32;
            }
            sendrow_323( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_32_Refreshing )
            {
               DoAjaxLoad(32, GridtoencryptRow);
            }
            AV19GXV5 = (int)(AV19GXV5+1);
         }
         /*  Sending Event outputs  */
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
         PA3G2( ) ;
         WS3G2( ) ;
         WE3G2( ) ;
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
         PA3G2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\largefiles", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA3G2( ) ;
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
         PA3G2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS3G2( ) ;
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
         WS3G2( ) ;
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
         WE3G2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610714151666", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/largefiles.js", "?202610714151666", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_162( )
      {
         edtavCtlrecfilename_Internalname = sPrefix+"CTLRECFILENAME_"+sGXsfl_16_idx;
         edtavCtlrecfilesize_Internalname = sPrefix+"CTLRECFILESIZE_"+sGXsfl_16_idx;
         edtavCtlrecmodified_Internalname = sPrefix+"CTLRECMODIFIED_"+sGXsfl_16_idx;
         edtavDecryptaction_Internalname = sPrefix+"vDECRYPTACTION_"+sGXsfl_16_idx;
      }

      protected void SubsflControlProps_fel_162( )
      {
         edtavCtlrecfilename_Internalname = sPrefix+"CTLRECFILENAME_"+sGXsfl_16_fel_idx;
         edtavCtlrecfilesize_Internalname = sPrefix+"CTLRECFILESIZE_"+sGXsfl_16_fel_idx;
         edtavCtlrecmodified_Internalname = sPrefix+"CTLRECMODIFIED_"+sGXsfl_16_fel_idx;
         edtavDecryptaction_Internalname = sPrefix+"vDECRYPTACTION_"+sGXsfl_16_fel_idx;
      }

      protected void sendrow_162( )
      {
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         WB3G0( ) ;
         GridreceivedRow = GXWebRow.GetNew(context,GridreceivedContainer);
         if ( subGridreceived_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridreceived_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridreceived_Class, "") != 0 )
            {
               subGridreceived_Linesclass = subGridreceived_Class+"Odd";
            }
         }
         else if ( subGridreceived_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridreceived_Backstyle = 0;
            subGridreceived_Backcolor = subGridreceived_Allbackcolor;
            if ( StringUtil.StrCmp(subGridreceived_Class, "") != 0 )
            {
               subGridreceived_Linesclass = subGridreceived_Class+"Uniform";
            }
         }
         else if ( subGridreceived_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridreceived_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridreceived_Class, "") != 0 )
            {
               subGridreceived_Linesclass = subGridreceived_Class+"Odd";
            }
            subGridreceived_Backcolor = (int)(0x0);
         }
         else if ( subGridreceived_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridreceived_Backstyle = 1;
            if ( ((int)((nGXsfl_16_idx) % (2))) == 0 )
            {
               subGridreceived_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridreceived_Class, "") != 0 )
               {
                  subGridreceived_Linesclass = subGridreceived_Class+"Even";
               }
            }
            else
            {
               subGridreceived_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridreceived_Class, "") != 0 )
               {
                  subGridreceived_Linesclass = subGridreceived_Class+"Odd";
               }
            }
         }
         if ( GridreceivedContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_16_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridreceivedContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridreceivedRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlrecfilename_Internalname,((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1)).gxTpr_Filename,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlrecfilename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlrecfilename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridreceivedContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridreceivedRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlrecfilesize_Internalname,((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1)).gxTpr_Filesize,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlrecfilesize_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlrecfilesize_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)30,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridreceivedContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridreceivedRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlrecmodified_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1)).gxTpr_Modified, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtExchangeFile)AV11received.Item(AV15GXV1)).gxTpr_Modified, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,19);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlrecmodified_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlrecmodified_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridreceivedContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridreceivedRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDecryptaction_Internalname,StringUtil.RTrim( AV5decryptAction),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,20);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DECRYPT FILE\\'."+sGXsfl_16_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDecryptaction_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavDecryptaction_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes3G2( ) ;
         GridreceivedContainer.AddRow(GridreceivedRow);
         nGXsfl_16_idx = ((subGridreceived_Islastpage==1)&&(nGXsfl_16_idx+1>subGridreceived_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_idx+1);
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         /* End function sendrow_162 */
      }

      protected void SubsflControlProps_323( )
      {
         edtavCtlencfilename_Internalname = sPrefix+"CTLENCFILENAME_"+sGXsfl_32_idx;
         edtavCtlencfilesize_Internalname = sPrefix+"CTLENCFILESIZE_"+sGXsfl_32_idx;
         edtavCtlencmodified_Internalname = sPrefix+"CTLENCMODIFIED_"+sGXsfl_32_idx;
         edtavEncryptaction_Internalname = sPrefix+"vENCRYPTACTION_"+sGXsfl_32_idx;
      }

      protected void SubsflControlProps_fel_323( )
      {
         edtavCtlencfilename_Internalname = sPrefix+"CTLENCFILENAME_"+sGXsfl_32_fel_idx;
         edtavCtlencfilesize_Internalname = sPrefix+"CTLENCFILESIZE_"+sGXsfl_32_fel_idx;
         edtavCtlencmodified_Internalname = sPrefix+"CTLENCMODIFIED_"+sGXsfl_32_fel_idx;
         edtavEncryptaction_Internalname = sPrefix+"vENCRYPTACTION_"+sGXsfl_32_fel_idx;
      }

      protected void sendrow_323( )
      {
         sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
         SubsflControlProps_323( ) ;
         WB3G0( ) ;
         GridtoencryptRow = GXWebRow.GetNew(context,GridtoencryptContainer);
         if ( subGridtoencrypt_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridtoencrypt_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridtoencrypt_Class, "") != 0 )
            {
               subGridtoencrypt_Linesclass = subGridtoencrypt_Class+"Odd";
            }
         }
         else if ( subGridtoencrypt_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridtoencrypt_Backstyle = 0;
            subGridtoencrypt_Backcolor = subGridtoencrypt_Allbackcolor;
            if ( StringUtil.StrCmp(subGridtoencrypt_Class, "") != 0 )
            {
               subGridtoencrypt_Linesclass = subGridtoencrypt_Class+"Uniform";
            }
         }
         else if ( subGridtoencrypt_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridtoencrypt_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridtoencrypt_Class, "") != 0 )
            {
               subGridtoencrypt_Linesclass = subGridtoencrypt_Class+"Odd";
            }
            subGridtoencrypt_Backcolor = (int)(0x0);
         }
         else if ( subGridtoencrypt_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridtoencrypt_Backstyle = 1;
            if ( ((int)((nGXsfl_32_idx) % (2))) == 0 )
            {
               subGridtoencrypt_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridtoencrypt_Class, "") != 0 )
               {
                  subGridtoencrypt_Linesclass = subGridtoencrypt_Class+"Even";
               }
            }
            else
            {
               subGridtoencrypt_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridtoencrypt_Class, "") != 0 )
               {
                  subGridtoencrypt_Linesclass = subGridtoencrypt_Class+"Odd";
               }
            }
         }
         if ( GridtoencryptContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_32_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridtoencryptContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'" + sPrefix + "',false,'" + sGXsfl_32_idx + "',32)\"";
         ROClassString = "Attribute";
         GridtoencryptRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlencfilename_Internalname,((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5)).gxTpr_Filename,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,33);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlencfilename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlencfilename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)32,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridtoencryptContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 34,'" + sPrefix + "',false,'" + sGXsfl_32_idx + "',32)\"";
         ROClassString = "Attribute";
         GridtoencryptRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlencfilesize_Internalname,((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5)).gxTpr_Filesize,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,34);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlencfilesize_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlencfilesize_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)30,(short)0,(short)0,(short)32,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridtoencryptContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'" + sPrefix + "',false,'" + sGXsfl_32_idx + "',32)\"";
         ROClassString = "Attribute";
         GridtoencryptRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlencmodified_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5)).gxTpr_Modified, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtExchangeFile)AV14toEncrypt.Item(AV19GXV5)).gxTpr_Modified, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,35);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlencmodified_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlencmodified_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)32,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridtoencryptContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'" + sPrefix + "',false,'" + sGXsfl_32_idx + "',32)\"";
         ROClassString = "Attribute";
         GridtoencryptRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavEncryptaction_Internalname,StringUtil.RTrim( AV6encryptAction),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,36);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'ENCRYPT FILE\\'."+sGXsfl_32_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavEncryptaction_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavEncryptaction_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)32,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes3G3( ) ;
         GridtoencryptContainer.AddRow(GridtoencryptRow);
         nGXsfl_32_idx = ((subGridtoencrypt_Islastpage==1)&&(nGXsfl_32_idx+1>subGridtoencrypt_fnc_Recordsperpage( )) ? 1 : nGXsfl_32_idx+1);
         sGXsfl_32_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_32_idx), 4, 0), 4, "0");
         SubsflControlProps_323( ) ;
         /* End function sendrow_323 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl16( )
      {
         if ( GridreceivedContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridreceivedContainer"+"DivS\" data-gxgridid=\"16\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridreceived_Internalname, subGridreceived_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridreceived_Backcolorstyle == 0 )
            {
               subGridreceived_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridreceived_Class) > 0 )
               {
                  subGridreceived_Linesclass = subGridreceived_Class+"Title";
               }
            }
            else
            {
               subGridreceived_Titlebackstyle = 1;
               if ( subGridreceived_Backcolorstyle == 1 )
               {
                  subGridreceived_Titlebackcolor = subGridreceived_Allbackcolor;
                  if ( StringUtil.Len( subGridreceived_Class) > 0 )
                  {
                     subGridreceived_Linesclass = subGridreceived_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridreceived_Class) > 0 )
                  {
                     subGridreceived_Linesclass = subGridreceived_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "File") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Size") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Modified") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridreceivedContainer.AddObjectProperty("GridName", "Gridreceived");
         }
         else
         {
            GridreceivedContainer.AddObjectProperty("GridName", "Gridreceived");
            GridreceivedContainer.AddObjectProperty("Header", subGridreceived_Header);
            GridreceivedContainer.AddObjectProperty("Class", "Grid");
            GridreceivedContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Backcolorstyle), 1, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("CmpContext", sPrefix);
            GridreceivedContainer.AddObjectProperty("InMasterPage", "false");
            GridreceivedColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridreceivedColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlrecfilename_Enabled), 5, 0, ".", "")));
            GridreceivedContainer.AddColumnProperties(GridreceivedColumn);
            GridreceivedColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridreceivedColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlrecfilesize_Enabled), 5, 0, ".", "")));
            GridreceivedContainer.AddColumnProperties(GridreceivedColumn);
            GridreceivedColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridreceivedColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlrecmodified_Enabled), 5, 0, ".", "")));
            GridreceivedContainer.AddColumnProperties(GridreceivedColumn);
            GridreceivedColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridreceivedColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV5decryptAction)));
            GridreceivedColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDecryptaction_Enabled), 5, 0, ".", "")));
            GridreceivedContainer.AddColumnProperties(GridreceivedColumn);
            GridreceivedContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Selectedindex), 4, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Allowselection), 1, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Selectioncolor), 9, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Allowhovering), 1, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Hoveringcolor), 9, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Allowcollapsing), 1, 0, ".", "")));
            GridreceivedContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridreceived_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void StartGridControl32( )
      {
         if ( GridtoencryptContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridtoencryptContainer"+"DivS\" data-gxgridid=\"32\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridtoencrypt_Internalname, subGridtoencrypt_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridtoencrypt_Backcolorstyle == 0 )
            {
               subGridtoencrypt_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridtoencrypt_Class) > 0 )
               {
                  subGridtoencrypt_Linesclass = subGridtoencrypt_Class+"Title";
               }
            }
            else
            {
               subGridtoencrypt_Titlebackstyle = 1;
               if ( subGridtoencrypt_Backcolorstyle == 1 )
               {
                  subGridtoencrypt_Titlebackcolor = subGridtoencrypt_Allbackcolor;
                  if ( StringUtil.Len( subGridtoencrypt_Class) > 0 )
                  {
                     subGridtoencrypt_Linesclass = subGridtoencrypt_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridtoencrypt_Class) > 0 )
                  {
                     subGridtoencrypt_Linesclass = subGridtoencrypt_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "File") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Size") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Modified") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridtoencryptContainer.AddObjectProperty("GridName", "Gridtoencrypt");
         }
         else
         {
            GridtoencryptContainer.AddObjectProperty("GridName", "Gridtoencrypt");
            GridtoencryptContainer.AddObjectProperty("Header", subGridtoencrypt_Header);
            GridtoencryptContainer.AddObjectProperty("Class", "Grid");
            GridtoencryptContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Backcolorstyle), 1, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("CmpContext", sPrefix);
            GridtoencryptContainer.AddObjectProperty("InMasterPage", "false");
            GridtoencryptColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridtoencryptColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlencfilename_Enabled), 5, 0, ".", "")));
            GridtoencryptContainer.AddColumnProperties(GridtoencryptColumn);
            GridtoencryptColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridtoencryptColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlencfilesize_Enabled), 5, 0, ".", "")));
            GridtoencryptContainer.AddColumnProperties(GridtoencryptColumn);
            GridtoencryptColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridtoencryptColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlencmodified_Enabled), 5, 0, ".", "")));
            GridtoencryptContainer.AddColumnProperties(GridtoencryptColumn);
            GridtoencryptColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridtoencryptColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV6encryptAction)));
            GridtoencryptColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavEncryptaction_Enabled), 5, 0, ".", "")));
            GridtoencryptContainer.AddColumnProperties(GridtoencryptColumn);
            GridtoencryptContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Selectedindex), 4, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Allowselection), 1, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Selectioncolor), 9, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Allowhovering), 1, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Hoveringcolor), 9, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Allowcollapsing), 1, 0, ".", "")));
            GridtoencryptContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridtoencrypt_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblTbhelp_Internalname = sPrefix+"TBHELP";
         bttRefreshlists_Internalname = sPrefix+"REFRESHLISTS";
         edtavCtlrecfilename_Internalname = sPrefix+"CTLRECFILENAME";
         edtavCtlrecfilesize_Internalname = sPrefix+"CTLRECFILESIZE";
         edtavCtlrecmodified_Internalname = sPrefix+"CTLRECMODIFIED";
         edtavDecryptaction_Internalname = sPrefix+"vDECRYPTACTION";
         divGroupreceivedtable_Internalname = sPrefix+"GROUPRECEIVEDTABLE";
         grpGroupreceived_Internalname = sPrefix+"GROUPRECEIVED";
         edtavRecipientusername_Internalname = sPrefix+"vRECIPIENTUSERNAME";
         edtavCtlencfilename_Internalname = sPrefix+"CTLENCFILENAME";
         edtavCtlencfilesize_Internalname = sPrefix+"CTLENCFILESIZE";
         edtavCtlencmodified_Internalname = sPrefix+"CTLENCMODIFIED";
         edtavEncryptaction_Internalname = sPrefix+"vENCRYPTACTION";
         divGrouptoencrypttable_Internalname = sPrefix+"GROUPTOENCRYPTTABLE";
         grpGrouptoencrypt_Internalname = sPrefix+"GROUPTOENCRYPT";
         edtavNewexchangedir_Internalname = sPrefix+"vNEWEXCHANGEDIR";
         bttChangefolder_Internalname = sPrefix+"CHANGEFOLDER";
         bttDefaultfolder_Internalname = sPrefix+"DEFAULTFOLDER";
         divGroupfoldertable_Internalname = sPrefix+"GROUPFOLDERTABLE";
         grpGroupfolder_Internalname = sPrefix+"GROUPFOLDER";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGridreceived_Internalname = sPrefix+"GRIDRECEIVED";
         subGridtoencrypt_Internalname = sPrefix+"GRIDTOENCRYPT";
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
         subGridtoencrypt_Allowcollapsing = 0;
         subGridtoencrypt_Allowselection = 0;
         subGridtoencrypt_Header = "";
         subGridreceived_Allowcollapsing = 0;
         subGridreceived_Allowselection = 0;
         subGridreceived_Header = "";
         edtavEncryptaction_Jsonclick = "";
         edtavEncryptaction_Enabled = 1;
         edtavCtlencmodified_Jsonclick = "";
         edtavCtlencmodified_Enabled = 0;
         edtavCtlencfilesize_Jsonclick = "";
         edtavCtlencfilesize_Enabled = 0;
         edtavCtlencfilename_Jsonclick = "";
         edtavCtlencfilename_Enabled = 0;
         subGridtoencrypt_Class = "Grid";
         subGridtoencrypt_Backcolorstyle = 0;
         edtavDecryptaction_Jsonclick = "";
         edtavDecryptaction_Enabled = 1;
         edtavCtlrecmodified_Jsonclick = "";
         edtavCtlrecmodified_Enabled = 0;
         edtavCtlrecfilesize_Jsonclick = "";
         edtavCtlrecfilesize_Enabled = 0;
         edtavCtlrecfilename_Jsonclick = "";
         edtavCtlrecfilename_Enabled = 0;
         subGridreceived_Class = "Grid";
         subGridreceived_Backcolorstyle = 0;
         edtavNewexchangedir_Enabled = 1;
         edtavRecipientusername_Jsonclick = "";
         edtavRecipientusername_Enabled = 1;
         lblTbhelp_Caption = "Large files";
         edtavCtlencmodified_Enabled = -1;
         edtavCtlencfilesize_Enabled = -1;
         edtavCtlencfilename_Enabled = -1;
         edtavCtlrecmodified_Enabled = -1;
         edtavCtlrecfilesize_Enabled = -1;
         edtavCtlrecfilename_Enabled = -1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"GRIDRECEIVED_nEOF","type":"int"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"GRIDTOENCRYPT_nEOF","type":"int"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV9help","fld":"vHELP","hsh":true,"type":"vchar"}]}""");
         setEventMetadata("'REFRESH LISTS'","""{"handler":"E113G2","iparms":[{"av":"AV9help","fld":"vHELP","hsh":true,"type":"vchar"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"GRIDRECEIVED_nEOF","type":"int"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"GRIDTOENCRYPT_nEOF","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'REFRESH LISTS'",""","oparms":[{"av":"AV10newExchangeDir","fld":"vNEWEXCHANGEDIR","type":"svchar"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"lblTbhelp_Caption","ctrl":"TBHELP","prop":"Caption"}]}""");
         setEventMetadata("GRIDRECEIVED.LOAD","""{"handler":"E153G2","iparms":[]""");
         setEventMetadata("GRIDRECEIVED.LOAD",""","oparms":[{"av":"AV5decryptAction","fld":"vDECRYPTACTION","type":"char"}]}""");
         setEventMetadata("GRIDTOENCRYPT.LOAD","""{"handler":"E173G3","iparms":[]""");
         setEventMetadata("GRIDTOENCRYPT.LOAD",""","oparms":[{"av":"AV6encryptAction","fld":"vENCRYPTACTION","type":"char"}]}""");
         setEventMetadata("'DECRYPT FILE'","""{"handler":"E163G2","iparms":[{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"AV9help","fld":"vHELP","hsh":true,"type":"vchar"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"GRIDRECEIVED_nEOF","type":"int"},{"av":"GRIDTOENCRYPT_nEOF","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'DECRYPT FILE'",""","oparms":[{"av":"AV10newExchangeDir","fld":"vNEWEXCHANGEDIR","type":"svchar"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"lblTbhelp_Caption","ctrl":"TBHELP","prop":"Caption"}]}""");
         setEventMetadata("'ENCRYPT FILE'","""{"handler":"E183G2","iparms":[{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"AV12recipientUserName","fld":"vRECIPIENTUSERNAME","type":"svchar"},{"av":"AV9help","fld":"vHELP","hsh":true,"type":"vchar"},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"GRIDRECEIVED_nEOF","type":"int"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"GRIDTOENCRYPT_nEOF","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'ENCRYPT FILE'",""","oparms":[{"av":"AV10newExchangeDir","fld":"vNEWEXCHANGEDIR","type":"svchar"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"lblTbhelp_Caption","ctrl":"TBHELP","prop":"Caption"}]}""");
         setEventMetadata("'CHANGE FOLDER'","""{"handler":"E123G2","iparms":[{"av":"AV10newExchangeDir","fld":"vNEWEXCHANGEDIR","type":"svchar"},{"av":"AV9help","fld":"vHELP","hsh":true,"type":"vchar"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"GRIDRECEIVED_nEOF","type":"int"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"GRIDTOENCRYPT_nEOF","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'CHANGE FOLDER'",""","oparms":[{"av":"AV10newExchangeDir","fld":"vNEWEXCHANGEDIR","type":"svchar"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"lblTbhelp_Caption","ctrl":"TBHELP","prop":"Caption"}]}""");
         setEventMetadata("'DEFAULT FOLDER'","""{"handler":"E133G2","iparms":[{"av":"AV9help","fld":"vHELP","hsh":true,"type":"vchar"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"GRIDRECEIVED_nEOF","type":"int"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"GRIDTOENCRYPT_nEOF","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'DEFAULT FOLDER'",""","oparms":[{"av":"AV10newExchangeDir","fld":"vNEWEXCHANGEDIR","type":"svchar"},{"av":"AV11received","fld":"vRECEIVED","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDRECEIVED_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDRECEIVED","prop":"GridRC","grid":16,"type":"int"},{"av":"AV14toEncrypt","fld":"vTOENCRYPT","grid":32,"type":""},{"av":"nGXsfl_32_idx","ctrl":"GRID","prop":"GridCurrRow","grid":32},{"av":"GRIDTOENCRYPT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_32","ctrl":"GRIDTOENCRYPT","prop":"GridRC","grid":32,"type":"int"},{"av":"lblTbhelp_Caption","ctrl":"TBHELP","prop":"Caption"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Decryptaction","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Encryptaction","iparms":[]}""");
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
         sPrefix = "";
         AV9help = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV11received = new GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile>( context, "ExchangeFile", "distributedcryptography");
         AV14toEncrypt = new GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile>( context, "ExchangeFile", "distributedcryptography");
         GX_FocusControl = "";
         lblTbhelp_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttRefreshlists_Jsonclick = "";
         GridreceivedContainer = new GXWebGrid( context);
         sStyleString = "";
         AV12recipientUserName = "";
         GridtoencryptContainer = new GXWebGrid( context);
         AV10newExchangeDir = "";
         bttChangefolder_Jsonclick = "";
         bttDefaultfolder_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV5decryptAction = "";
         AV6encryptAction = "";
         GridreceivedRow = new GXWebRow();
         AV7error = "";
         AV13resultPath = "";
         GXt_char1 = "";
         AV8exchangeDir = "";
         GXt_objcol_SdtExchangeFile2 = new GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile>( context, "ExchangeFile", "distributedcryptography");
         GridtoencryptRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridreceived_Linesclass = "";
         ROClassString = "";
         subGridtoencrypt_Linesclass = "";
         GridreceivedColumn = new GXWebColumn();
         GridtoencryptColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlrecfilename_Enabled = 0;
         edtavCtlrecfilesize_Enabled = 0;
         edtavCtlrecmodified_Enabled = 0;
         edtavDecryptaction_Enabled = 0;
         edtavCtlencfilename_Enabled = 0;
         edtavCtlencfilesize_Enabled = 0;
         edtavCtlencmodified_Enabled = 0;
         edtavEncryptaction_Enabled = 0;
      }

      private short nRcdExists_3 ;
      private short nIsMod_3 ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridreceived_Backcolorstyle ;
      private short subGridtoencrypt_Backcolorstyle ;
      private short GRIDRECEIVED_nEOF ;
      private short GRIDTOENCRYPT_nEOF ;
      private short nGXWrapped ;
      private short subGridreceived_Backstyle ;
      private short subGridtoencrypt_Backstyle ;
      private short subGridreceived_Titlebackstyle ;
      private short subGridreceived_Allowselection ;
      private short subGridreceived_Allowhovering ;
      private short subGridreceived_Allowcollapsing ;
      private short subGridreceived_Collapsed ;
      private short subGridtoencrypt_Titlebackstyle ;
      private short subGridtoencrypt_Allowselection ;
      private short subGridtoencrypt_Allowhovering ;
      private short subGridtoencrypt_Allowcollapsing ;
      private short subGridtoencrypt_Collapsed ;
      private int nRC_GXsfl_16 ;
      private int nRC_GXsfl_32 ;
      private int nGXsfl_16_idx=1 ;
      private int nGXsfl_32_idx=1 ;
      private int edtavCtlrecfilename_Enabled ;
      private int edtavCtlrecfilesize_Enabled ;
      private int edtavCtlrecmodified_Enabled ;
      private int edtavDecryptaction_Enabled ;
      private int edtavCtlencfilename_Enabled ;
      private int edtavCtlencfilesize_Enabled ;
      private int edtavCtlencmodified_Enabled ;
      private int edtavEncryptaction_Enabled ;
      private int AV15GXV1 ;
      private int edtavRecipientusername_Enabled ;
      private int AV19GXV5 ;
      private int edtavNewexchangedir_Enabled ;
      private int subGridreceived_Islastpage ;
      private int subGridtoencrypt_Islastpage ;
      private int nGXsfl_16_fel_idx=1 ;
      private int nGXsfl_32_fel_idx=1 ;
      private int nGXsfl_16_bak_idx=1 ;
      private int nGXsfl_32_bak_idx=1 ;
      private int idxLst ;
      private int subGridreceived_Backcolor ;
      private int subGridreceived_Allbackcolor ;
      private int subGridtoencrypt_Backcolor ;
      private int subGridtoencrypt_Allbackcolor ;
      private int subGridreceived_Titlebackcolor ;
      private int subGridreceived_Selectedindex ;
      private int subGridreceived_Selectioncolor ;
      private int subGridreceived_Hoveringcolor ;
      private int subGridtoencrypt_Titlebackcolor ;
      private int subGridtoencrypt_Selectedindex ;
      private int subGridtoencrypt_Selectioncolor ;
      private int subGridtoencrypt_Hoveringcolor ;
      private long GRIDRECEIVED_nCurrentRecord ;
      private long GRIDTOENCRYPT_nCurrentRecord ;
      private long GRIDRECEIVED_nFirstRecordOnPage ;
      private long GRIDTOENCRYPT_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_16_idx="0001" ;
      private string sGXsfl_32_idx="0001" ;
      private string edtavCtlrecfilename_Internalname ;
      private string edtavCtlrecfilesize_Internalname ;
      private string edtavCtlrecmodified_Internalname ;
      private string edtavDecryptaction_Internalname ;
      private string edtavCtlencfilename_Internalname ;
      private string edtavCtlencfilesize_Internalname ;
      private string edtavCtlencmodified_Internalname ;
      private string edtavEncryptaction_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string lblTbhelp_Internalname ;
      private string lblTbhelp_Caption ;
      private string lblTbhelp_Jsonclick ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttRefreshlists_Internalname ;
      private string bttRefreshlists_Jsonclick ;
      private string grpGroupreceived_Internalname ;
      private string divGroupreceivedtable_Internalname ;
      private string sStyleString ;
      private string subGridreceived_Internalname ;
      private string grpGrouptoencrypt_Internalname ;
      private string divGrouptoencrypttable_Internalname ;
      private string edtavRecipientusername_Internalname ;
      private string edtavRecipientusername_Jsonclick ;
      private string subGridtoencrypt_Internalname ;
      private string grpGroupfolder_Internalname ;
      private string divGroupfoldertable_Internalname ;
      private string edtavNewexchangedir_Internalname ;
      private string bttChangefolder_Internalname ;
      private string bttChangefolder_Jsonclick ;
      private string bttDefaultfolder_Internalname ;
      private string bttDefaultfolder_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV5decryptAction ;
      private string AV6encryptAction ;
      private string sGXsfl_16_fel_idx="0001" ;
      private string sGXsfl_32_fel_idx="0001" ;
      private string GXt_char1 ;
      private string subGridreceived_Class ;
      private string subGridreceived_Linesclass ;
      private string ROClassString ;
      private string edtavCtlrecfilename_Jsonclick ;
      private string edtavCtlrecfilesize_Jsonclick ;
      private string edtavCtlrecmodified_Jsonclick ;
      private string edtavDecryptaction_Jsonclick ;
      private string subGridtoencrypt_Class ;
      private string subGridtoencrypt_Linesclass ;
      private string edtavCtlencfilename_Jsonclick ;
      private string edtavCtlencfilesize_Jsonclick ;
      private string edtavCtlencmodified_Jsonclick ;
      private string edtavEncryptaction_Jsonclick ;
      private string subGridreceived_Header ;
      private string subGridtoencrypt_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_16_Refreshing=false ;
      private bool bGXsfl_32_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV16 ;
      private bool gx_BV32 ;
      private string AV9help ;
      private string AV12recipientUserName ;
      private string AV10newExchangeDir ;
      private string AV7error ;
      private string AV13resultPath ;
      private string AV8exchangeDir ;
      private GXWebGrid GridreceivedContainer ;
      private GXWebGrid GridtoencryptContainer ;
      private GXWebRow GridreceivedRow ;
      private GXWebRow GridtoencryptRow ;
      private GXWebColumn GridreceivedColumn ;
      private GXWebColumn GridtoencryptColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile> AV11received ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile> AV14toEncrypt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtExchangeFile> GXt_objcol_SdtExchangeFile2 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
