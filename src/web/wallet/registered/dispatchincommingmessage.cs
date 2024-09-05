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
   public class dispatchincommingmessage : GXWebComponent
   {
      public dispatchincommingmessage( )
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

      public dispatchincommingmessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_screen_name )
      {
         this.AV9screen_name = aP0_screen_name;
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
               gxfirstwebparm = GetFirstPar( "screen_name");
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
                  AV9screen_name = GetPar( "screen_name");
                  AssignAttri(sPrefix, false, "AV9screen_name", AV9screen_name);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(string)AV9screen_name});
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
                  gxfirstwebparm = GetFirstPar( "screen_name");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "screen_name");
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
            PA192( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               WS192( ) ;
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
            context.SendWebValue( "dispatch Incomming Message") ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1218140), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.dispatchincommingmessage.aspx", new object[] {UrlEncode(StringUtil.RTrim(AV9screen_name))}, new string[] {"screen_name"}) +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vNOTIFICATIONINFO", AV12notificationInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vNOTIFICATIONINFO", AV12notificationInfo);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vNOTIFICATIONINFO", GetSecureSignedToken( sPrefix, AV12notificationInfo, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV9screen_name", StringUtil.RTrim( wcpOAV9screen_name));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vNOTIFICATIONINFO", AV12notificationInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vNOTIFICATIONINFO", AV12notificationInfo);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vNOTIFICATIONINFO", GetSecureSignedToken( sPrefix, AV12notificationInfo, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vJSON_ENC", AV13json_enc);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vJSON_ENC", AV13json_enc);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vEXTERNALUSER", AV16externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vEXTERNALUSER", AV16externalUser);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vSCREEN_NAME", StringUtil.RTrim( AV9screen_name));
         GxWebStd.gx_hidden_field( context, sPrefix+"vLASTMESSAGEID", AV23lastMessageId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vMESSAGE", AV21message);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vMESSAGE", AV21message);
         }
      }

      protected void RenderHtmlCloseForm192( )
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
         return "Wallet.registered.dispatchIncommingMessage" ;
      }

      public override string GetPgmdesc( )
      {
         return "dispatch Incomming Message" ;
      }

      protected void WB190( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.dispatchincommingmessage.aspx");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START192( )
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
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
               }
            }
            Form.Meta.addItem("description", "dispatch Incomming Message", 0) ;
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
               STRUP190( ) ;
            }
         }
      }

      protected void WS192( )
      {
         START192( ) ;
         EVT192( ) ;
      }

      protected void EVT192( )
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
                                 STRUP190( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "GLOBALEVENTS.DISTPACHINCOMMINGMESSAGES") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP190( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E11192 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP190( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Load */
                                    E12192 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP190( ) ;
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
                                 STRUP190( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
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

      protected void WE192( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm192( ) ;
            }
         }
      }

      protected void PA192( )
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
         RF192( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF192( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E12192 ();
            WB190( ) ;
         }
      }

      protected void send_integrity_lvl_hashes192( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vNOTIFICATIONINFO", AV12notificationInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vNOTIFICATIONINFO", AV12notificationInfo);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vNOTIFICATIONINFO", GetSecureSignedToken( sPrefix, AV12notificationInfo, context));
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP190( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            wcpOAV9screen_name = cgiGet( sPrefix+"wcpOAV9screen_name");
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

      protected void E11192( )
      {
         /* GlobalEvents_Distpachincommingmessages Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV7wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV7wallet = GXt_SdtWallet1;
         GXt_SdtExternalUser2 = AV16externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser2) ;
         AV16externalUser = GXt_SdtExternalUser2;
         AV5queueDirectory.Source = "Comqueue";
         AV30GXV2 = 1;
         AV29GXV1 = AV5queueDirectory.GetFiles(".queue");
         while ( AV30GXV2 <= AV29GXV1.ItemCount )
         {
            AV6queueFile = AV29GXV1.Item(AV30GXV2);
            AV12notificationInfo.FromJSonFile(AV6queueFile, null);
            AV6queueFile.Delete();
            new GeneXus.Programs.nostr.processrecivedfromnostr(context ).execute(  AV12notificationInfo, out  AV26recFromNostr) ;
            if ( ( StringUtil.StrCmp(AV26recFromNostr.gxTpr_Responsetype, "EVENT") == 0 ) && ( AV26recFromNostr.gxTpr_Event.gxTpr_Kind == 4 ) )
            {
               GXt_char3 = AV28encText;
               new GeneXus.Programs.distcrypt.hextotext(context ).execute(  StringUtil.Trim( AV26recFromNostr.gxTpr_Event.gxTpr_Content), out  GXt_char3) ;
               AV28encText = GXt_char3;
               AV13json_enc.FromJSonString(AV28encText, null);
               /* Execute user subroutine: 'DISPATCH ONE MESSAGE' */
               S112 ();
               if (returnInSub) return;
            }
            else
            {
               if ( StringUtil.StrCmp(AV26recFromNostr.gxTpr_Responsetype, "EOSE") == 0 )
               {
               }
               else
               {
                  if ( StringUtil.StrCmp(AV26recFromNostr.gxTpr_Responsetype, "OK") == 0 )
                  {
                  }
                  else
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Nostr response: ",AV26recFromNostr.ToJSonString(false, true)}, true);
                  }
               }
            }
            AV30GXV2 = (int)(AV30GXV2+1);
         }
         GXt_char3 = AV14error;
         new GeneXus.Programs.wallet.registered.getmessages(context ).execute( out  AV19messages, out  GXt_char3) ;
         AV14error = GXt_char3;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            AV31GXV3 = 1;
            while ( AV31GXV3 <= AV19messages.gxTpr_Message.Count )
            {
               AV21message = ((SdtDesktopApp_services_SDT_Messages_Message_MessageItem)AV19messages.gxTpr_Message.Item(AV31GXV3));
               AV13json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
               AV13json_enc.gxTpr_Encryptedkey = AV21message.gxTpr_Messageencryptedkey;
               AV13json_enc.gxTpr_Iv = AV21message.gxTpr_Messageiv;
               AV13json_enc.gxTpr_Encryptedtext = AV21message.gxTpr_Messageencrypted;
               /* Execute user subroutine: 'DISPATCH ONE MESSAGE' */
               S112 ();
               if (returnInSub) return;
               AV31GXV3 = (int)(AV31GXV3+1);
            }
         }
         else
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"getMessages: ",(string)AV14error}, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV16externalUser", AV16externalUser);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13json_enc", AV13json_enc);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV21message", AV21message);
      }

      protected void S112( )
      {
         /* 'DISPATCH ONE MESSAGE' Routine */
         returnInSub = false;
         GXt_char3 = AV14error;
         new GeneXus.Programs.distributedcrypto.decryptjsonfor(context ).execute(  AV13json_enc.gxTpr_Encryptedtext,  AV13json_enc.gxTpr_Encryptedkey,  AV13json_enc.gxTpr_Iv,  AV16externalUser.gxTpr_Chatkeyinfo.gxTpr_Privatekey, out  AV15clearText, out  GXt_char3) ;
         AV14error = GXt_char3;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            GXt_char3 = AV14error;
            new GeneXus.Programs.distributedcrypto.decryptjsonfor(context ).execute(  AV13json_enc.gxTpr_Encryptedtext,  AV13json_enc.gxTpr_Encryptedkey,  AV13json_enc.gxTpr_Iv,  AV16externalUser.gxTpr_Keyinfo.gxTpr_Privatekey, out  AV15clearText, out  GXt_char3) ;
            AV14error = GXt_char3;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.distributedcrypto.decryptjsonfor(context ).execute(  AV13json_enc.gxTpr_Encryptedtext,  AV13json_enc.gxTpr_Encryptedkey,  AV13json_enc.gxTpr_Iv,  AV16externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV15clearText, out  GXt_char3) ;
               AV14error = GXt_char3;
            }
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
         {
            AV10sdt_message.FromJSonString(AV15clearText, null);
            if ( AV10sdt_message.gxTpr_Messagetype == 50 )
            {
               AV11sdt_chat.FromJSonString(AV10sdt_message.gxTpr_Message, null);
               GXt_guid4 = AV17contactId;
               new GeneXus.Programs.wallet.registered.getcontactid(context ).execute(  StringUtil.Trim( AV11sdt_chat.gxTpr_From), out  AV25userPrivateName, out  GXt_guid4) ;
               AV17contactId = GXt_guid4;
               if ( (Guid.Empty==AV17contactId) )
               {
               }
               else
               {
                  new GeneXus.Programs.wallet.registered.appendcontactchat(context ).execute(  AV17contactId,  AV11sdt_chat) ;
                  if ( StringUtil.StrCmp(StringUtil.Trim( AV9screen_name), "Wallet.registered.Chat") == 0 )
                  {
                     GXt_SdtContact_SDT5 = AV18contact;
                     new GeneXus.Programs.wallet.getcontact(context ).execute( out  GXt_SdtContact_SDT5) ;
                     AV18contact = GXt_SdtContact_SDT5;
                     if ( AV18contact.gxTpr_Contactrid == AV17contactId )
                     {
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ReceiveChat", new Object[] {}, true);
                     }
                     else
                     {
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"New Chat",(string)"you've received a chat from: "+AV25userPrivateName}, true);
                     }
                  }
                  else
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"New Chat",(string)"you've received a chat from: "+AV25userPrivateName}, true);
                  }
               }
            }
            else if ( AV10sdt_message.gxTpr_Messagetype == 30 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.registered.insertinvitationoncontact(context ).execute(  AV10sdt_message, out  AV24isContactDeclined, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV21message.gxTpr_Messageid, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error deleting invitation",(string)AV14error}, true);
                  }
                  else
                  {
                     if ( AV23lastMessageId == AV10sdt_message.gxTpr_Id )
                     {
                     }
                     else
                     {
                        if ( ! AV24isContactDeclined )
                        {
                           this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"There is a new Contact invitation",(string)"Please go to Contacts to accept or decline"}, true);
                        }
                        AV23lastMessageId = AV10sdt_message.gxTpr_Id;
                        AssignAttri(sPrefix, false, "AV23lastMessageId", AV23lastMessageId.ToString());
                     }
                  }
               }
            }
            else if ( AV10sdt_message.gxTpr_Messagetype == 40 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.registered.updateacceptedinvitation(context ).execute(  AV10sdt_message, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV21message.gxTpr_Messageid, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error deleting confirmation: ",(string)AV14error}, true);
                  }
                  else
                  {
                     if ( AV23lastMessageId == AV10sdt_message.gxTpr_Id )
                     {
                     }
                     else
                     {
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"There is a new Confirmation",(string)"One of you contact invitation was accepted"}, true);
                        AV23lastMessageId = AV10sdt_message.gxTpr_Id;
                        AssignAttri(sPrefix, false, "AV23lastMessageId", AV23lastMessageId.ToString());
                     }
                  }
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error updating User invitation: ",(string)AV14error}, true);
               }
            }
            else if ( AV10sdt_message.gxTpr_Messagetype == 70 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.registered.insertinvitationongroup(context ).execute(  AV10sdt_message, out  AV24isContactDeclined, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV21message.gxTpr_Messageid, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error deleting group invitation",(string)AV14error}, true);
                  }
                  else
                  {
                     if ( AV23lastMessageId == AV10sdt_message.gxTpr_Id )
                     {
                     }
                     else
                     {
                        if ( ! AV24isContactDeclined )
                        {
                           this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"There is a new Group invitation",(string)"Please go to SmartGroups to accept or decline"}, true);
                        }
                        AV23lastMessageId = AV10sdt_message.gxTpr_Id;
                        AssignAttri(sPrefix, false, "AV23lastMessageId", AV23lastMessageId.ToString());
                     }
                  }
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"We've received a group invitaton",(string)AV14error}, true);
               }
            }
            else if ( AV10sdt_message.gxTpr_Messagetype == 80 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.registered.updateacceptedinvitationongroup(context ).execute(  AV10sdt_message, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV21message.gxTpr_Messageid, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error deleting group invitation",(string)AV14error}, true);
                  }
                  else
                  {
                     if ( AV23lastMessageId == AV10sdt_message.gxTpr_Id )
                     {
                     }
                     else
                     {
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"There is a new Group Confirmation",(string)"Please go to SmartGroups to see which user group accepted the invitation"}, true);
                        AV23lastMessageId = AV10sdt_message.gxTpr_Id;
                        AssignAttri(sPrefix, false, "AV23lastMessageId", AV23lastMessageId.ToString());
                     }
                  }
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"We've received a group acceptance",(string)AV14error}, true);
               }
            }
            else if ( AV10sdt_message.gxTpr_Messagetype == 90 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.registered.updateactivatedgroup(context ).execute(  AV10sdt_message, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV21message.gxTpr_Messageid, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error deleting group message",(string)AV14error}, true);
                  }
                  else
                  {
                     if ( AV23lastMessageId == AV10sdt_message.gxTpr_Id )
                     {
                     }
                     else
                     {
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"There is a new Group Activation",(string)"Please go to SmartGroups to see which user group was activated"}, true);
                        AV23lastMessageId = AV10sdt_message.gxTpr_Id;
                        AssignAttri(sPrefix, false, "AV23lastMessageId", AV23lastMessageId.ToString());
                     }
                  }
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"We've received a group activation",(string)AV14error}, true);
               }
            }
            else if ( AV10sdt_message.gxTpr_Messagetype == 100 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.registered.updaterestoreongroup(context ).execute(  AV10sdt_message, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV21message.gxTpr_Messageid, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error deleting group message",(string)AV14error}, true);
                  }
                  else
                  {
                     if ( AV23lastMessageId == AV10sdt_message.gxTpr_Id )
                     {
                     }
                     else
                     {
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"There is a new Group Restore",(string)"Please go to SmartGroups to see which user group it is restoring"}, true);
                        AV23lastMessageId = AV10sdt_message.gxTpr_Id;
                        AssignAttri(sPrefix, false, "AV23lastMessageId", AV23lastMessageId.ToString());
                     }
                  }
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"We've received a group restore",(string)AV14error}, true);
               }
            }
            else if ( AV10sdt_message.gxTpr_Messagetype == 110 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.registered.receivedonemusign(context ).execute(  AV10sdt_message, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV21message.gxTpr_Messageid, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error deleting group message",(string)AV14error}, true);
                  }
                  else
                  {
                     if ( AV23lastMessageId == AV10sdt_message.gxTpr_Id )
                     {
                     }
                     else
                     {
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"There is a new MultiSignature Group event",(string)"Please go to SmartGroups to see which user group has received a signature"}, true);
                        AV23lastMessageId = AV10sdt_message.gxTpr_Id;
                        AssignAttri(sPrefix, false, "AV23lastMessageId", AV23lastMessageId.ToString());
                     }
                  }
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"We've received one partial signature",(string)AV14error}, true);
               }
            }
            else if ( AV10sdt_message.gxTpr_Messagetype == 120 )
            {
               GXt_char3 = AV14error;
               new GeneXus.Programs.wallet.registered.receivedfinishedmusig(context ).execute(  AV10sdt_message, out  GXt_char3) ;
               AV14error = GXt_char3;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
               {
                  GXt_char3 = AV14error;
                  new GeneXus.Programs.wallet.registered.deletemessage(context ).execute(  AV21message.gxTpr_Messageid, out  GXt_char3) ;
                  AV14error = GXt_char3;
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14error)) )
                  {
                     this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Error deleting group message",(string)AV14error}, true);
                  }
                  else
                  {
                     if ( AV23lastMessageId == AV10sdt_message.gxTpr_Id )
                     {
                     }
                     else
                     {
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"info",(string)"There is a new MultiSignature Group transaction",(string)"Please go to SmartGroups to see the completed transaction"}, true);
                        AV23lastMessageId = AV10sdt_message.gxTpr_Id;
                        AssignAttri(sPrefix, false, "AV23lastMessageId", AV23lastMessageId.ToString());
                     }
                  }
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"We've received a signature confirmation",(string)AV14error}, true);
               }
            }
            else
            {
            }
         }
         else
         {
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E12192( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV9screen_name = (string)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV9screen_name", AV9screen_name);
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
         PA192( ) ;
         WS192( ) ;
         WE192( ) ;
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
         sCtrlAV9screen_name = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA192( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\dispatchincommingmessage", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA192( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV9screen_name = (string)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV9screen_name", AV9screen_name);
         }
         wcpOAV9screen_name = cgiGet( sPrefix+"wcpOAV9screen_name");
         if ( ! GetJustCreated( ) && ( ( StringUtil.StrCmp(AV9screen_name, wcpOAV9screen_name) != 0 ) ) )
         {
            setjustcreated();
         }
         wcpOAV9screen_name = AV9screen_name;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV9screen_name = cgiGet( sPrefix+"AV9screen_name_CTRL");
         if ( StringUtil.Len( sCtrlAV9screen_name) > 0 )
         {
            AV9screen_name = cgiGet( sCtrlAV9screen_name);
            AssignAttri(sPrefix, false, "AV9screen_name", AV9screen_name);
         }
         else
         {
            AV9screen_name = cgiGet( sPrefix+"AV9screen_name_PARM");
         }
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
         PA192( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS192( ) ;
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
         WS192( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV9screen_name_PARM", StringUtil.RTrim( AV9screen_name));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV9screen_name)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV9screen_name_CTRL", StringUtil.RTrim( sCtrlAV9screen_name));
         }
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
         WE192( ) ;
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
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20249514183850", true, true);
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
         context.AddJavascriptSource("wallet/registered/dispatchincommingmessage.js", "?20249514183851", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV12notificationInfo","fld":"vNOTIFICATIONINFO","hsh":true}]}""");
         setEventMetadata("GLOBALEVENTS.DISTPACHINCOMMINGMESSAGES","""{"handler":"E11192","iparms":[{"av":"AV12notificationInfo","fld":"vNOTIFICATIONINFO","hsh":true},{"av":"AV13json_enc","fld":"vJSON_ENC"},{"av":"AV16externalUser","fld":"vEXTERNALUSER"},{"av":"AV9screen_name","fld":"vSCREEN_NAME"},{"av":"AV23lastMessageId","fld":"vLASTMESSAGEID"},{"av":"AV21message","fld":"vMESSAGE"}]""");
         setEventMetadata("GLOBALEVENTS.DISTPACHINCOMMINGMESSAGES",""","oparms":[{"av":"AV16externalUser","fld":"vEXTERNALUSER"},{"av":"AV13json_enc","fld":"vJSON_ENC"},{"av":"AV21message","fld":"vMESSAGE"},{"av":"AV23lastMessageId","fld":"vLASTMESSAGEID"}]}""");
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
         wcpOAV9screen_name = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         AV12notificationInfo = new GeneXus.Core.genexus.server.SdtNotificationInfo(context);
         GXKey = "";
         AV13json_enc = new GeneXus.Programs.wallet.SdtSDT_Json_Enc(context);
         AV16externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV23lastMessageId = Guid.Empty;
         AV21message = new SdtDesktopApp_services_SDT_Messages_Message_MessageItem(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV7wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtExternalUser2 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV5queueDirectory = new GxDirectory(context.GetPhysicalPath());
         AV29GXV1 = new GxFileCollection();
         AV6queueFile = new GxFile(context.GetPhysicalPath());
         AV26recFromNostr = new GeneXus.Programs.nostr.SdtRecFromNostr(context);
         AV28encText = "";
         AV14error = "";
         AV19messages = new SdtDesktopApp_services_SDT_Messages(context);
         AV15clearText = "";
         AV10sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV11sdt_chat = new GeneXus.Programs.nostr.SdtSDT_Chat(context);
         AV17contactId = Guid.Empty;
         GXt_guid4 = Guid.Empty;
         AV25userPrivateName = "";
         AV18contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_SdtContact_SDT5 = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_char3 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV9screen_name = "";
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
      private int AV30GXV2 ;
      private int AV31GXV3 ;
      private int idxLst ;
      private string AV9screen_name ;
      private string wcpOAV9screen_name ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV14error ;
      private string AV25userPrivateName ;
      private string GXt_char3 ;
      private string sCtrlAV9screen_name ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV24isContactDeclined ;
      private string AV28encText ;
      private string AV15clearText ;
      private Guid AV23lastMessageId ;
      private Guid AV17contactId ;
      private Guid GXt_guid4 ;
      private GXWebForm Form ;
      private GxFile AV6queueFile ;
      private GxDirectory AV5queueDirectory ;
      private GxFileCollection AV29GXV1 ;
      private IGxDataStore dsDefault ;
      private GeneXus.Core.genexus.server.SdtNotificationInfo AV12notificationInfo ;
      private GeneXus.Programs.wallet.SdtSDT_Json_Enc AV13json_enc ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV16externalUser ;
      private SdtDesktopApp_services_SDT_Messages_Message_MessageItem AV21message ;
      private GeneXus.Programs.wallet.SdtWallet AV7wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser2 ;
      private GeneXus.Programs.nostr.SdtRecFromNostr AV26recFromNostr ;
      private SdtDesktopApp_services_SDT_Messages AV19messages ;
      private GeneXus.Programs.nostr.SdtSDT_message AV10sdt_message ;
      private GeneXus.Programs.nostr.SdtSDT_Chat AV11sdt_chat ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV18contact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT GXt_SdtContact_SDT5 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
